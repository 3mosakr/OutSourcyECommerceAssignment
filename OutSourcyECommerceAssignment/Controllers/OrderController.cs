using Microsoft.AspNetCore.Mvc;
using OutSourcyECommerceAssignment.DTOs.OrderDTOs;
using OutSourcyECommerceAssignment.Models;
using OutSourcyECommerceAssignment.Services.Interfaces;

namespace OutSourcyECommerceAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var projection = orders.Select(o => new {
                o.Id,
                CustomerName = o.Customer?.Name,
                o.Status,
                NumberOfProducts = o.OrderProducts?.Sum(op => op.Quantity) ?? 0,
                o.TotalPrice
            });
            return Ok(projection);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound(new { message = "Order not found." });

            return Ok(new
            {
                order.Id,
                CustomerName = order.Customer?.Name,
                order.Status,
                NumberOfProducts = order.OrderProducts?.Sum(op => op.Quantity) ?? 0,
                order.TotalPrice
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderProducts = dto.OrderProducts.Select(p => new OrderProduc
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList()
            };

            try
            {
                await _orderService.AddOrderAsync(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, new { order.Id });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
            try
            {
                await _orderService.UpdateOrderStatusAsync(id, status);
                return Ok("Order is Delivered.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
