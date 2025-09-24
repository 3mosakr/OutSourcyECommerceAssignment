using Microsoft.EntityFrameworkCore;
using OutSourcyECommerceAssignment.Data;
using OutSourcyECommerceAssignment.Models;
using OutSourcyECommerceAssignment.Repositories.Interfaces;

namespace OutSourcyECommerceAssignment.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddOrderAsync(Order order)
        {
            if (order.OrderProducts != null)
            {
                decimal total = 0m;
                foreach (var op in order.OrderProducts)
                {
                    var product = await _context.Products.FindAsync(op.ProductId);
                    if (product == null)
                        throw new InvalidOperationException($"Product with id {op.ProductId} not found.");

                    if (product.Stock < op.Quantity)
                        throw new InvalidOperationException($"Insufficient stock for product {product.Name} (id={product.Id}).");

                    op.EachPrice = product.Price;
                    total += op.EachPrice * op.Quantity;
                }
                order.TotalPrice = total;
            }

            order.OrderDate = DateTime.Now;
            order.Status ??= "Pending";

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatusAsync(int id, string newStatus)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder == null)
                throw new InvalidOperationException("Order not found.");

            bool changingToDelivered = existingOrder.Status != "Delivered" && newStatus == "Delivered";

            if (changingToDelivered && existingOrder.OrderProducts != null)
            {
                foreach (var item in existingOrder.OrderProducts)
                {
                    var prod = await _context.Products.FindAsync(item.ProductId);
                    if (prod == null)
                        throw new InvalidOperationException($"Product {item.ProductId} not found.");
                    if (prod.Stock < item.Quantity)
                        throw new InvalidOperationException($"Insufficient stock for product {prod.Name} (id={prod.Id}).");

                    prod.Stock -= item.Quantity;
                }
            }

            existingOrder.Status = newStatus;
            _context.Orders.Update(existingOrder);
            await _context.SaveChangesAsync();
        }
    }
}
