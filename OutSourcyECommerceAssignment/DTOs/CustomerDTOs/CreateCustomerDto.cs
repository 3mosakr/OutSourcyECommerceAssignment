﻿namespace OutSourcyECommerceAssignment.DTOs.CustomerDTOs
{
    public class CreateCustomerDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
