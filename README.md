# OutSourcyECommerceAssignment

# 🛒 E-Commerce 

This project is a simple **E-Commerce REST API** built with **.NET 9, Entity Framework Core, and SQL Server**.  
It covers basic e-commerce features such as managing **Customers, Products, and Orders**, with proper **validation, seeding, and error handling**.

---

## 🚀 Features
- **Customers**
  - Add new customer (with **unique email** validation).
  - Get all customers / get customer by Id.
- **Products**
  - Seeded with sample data (Laptop, Smartphone, etc.).
  - Stock management (automatically reduced on order delivery).
- **Orders**
  - Create new order (validates stock).
  - Get all orders / get order by Id.
  - Update order status (e.g., `Pending` → `Delivered`).
- **Validation**
  - DTOs with **FluentValidation**.
  - Returns detailed validation error messages.
- **Error Handling**
  - Unified JSON error responses with proper HTTP status codes:
    - `400` for bad requests (validation or business logic errors).
    - `404` for not found.
    - `500` for server errors.

---

## 🛠️ Tech Stack
- **.NET 9**
- **Entity Framework Core**
- **SQL Server**
- **FluentValidation**
- **OpenAPI**

---

## 📦 Database Seeding
The application seeds initial data on migration:
- **Products:** Laptop, Smartphone, Headphones, Smartwatch, Tablet.
- **Customers:** Alice, Bob, Charlie, Diana.
- **Orders:** 4 sample orders with related products and total prices.

---

## ▶️ How to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/3mosakr/OutSourcyECommerceAssignment.git
   cd OutSourcyECommerceAssignment/OutSourcyECommerceAssignment
   ```
2. Apply migrations & update database:
  ```bash
  dotnet ef database update
  ```
3. Run the project:
  ```bash
  dotnet run
  ```
4. Open Postman for testing

---
## 📌 Example API Requests
1. ➕ Create Customer
  ```bash
    POST /api/customer
    Content-Type: application/json
    
    {
      "name": "Ali Ahmed",
      "email": "ali@example.com",
      "phone": "0100000000"
    }
  ```
# ✅ Response (201):
  ```bash
    {
    "id": 5,
    "name": "Ali Ahmed",
    "email": "ali@example.com",
    "phone": "0100000000"
    }
  ```
# ❌ Create Customer (duplicate email)
  ```bash
    {
      "success": false,
      "error": "Email already exists.",
      "statusCode": 400
    }
  ```
2. ➕ Create Order
   ```bash
    POST /api/order
    Content-Type: application/json
    {
      "customerId": 1,
      "orderProducts": [
        { "productId": 1, "quantity": 2 },
        { "productId": 2, "quantity": 1 }
      ]
    }
   ```
# ✅ Update Order Status
  ```bash
    PUT /api/order/1/status
    Content-Type: application/json
    
    "Delivered"

  ```
# ❌ If stock is insufficient in add or update:
  ```bash
    {
      "success": false,
      "error": "Insufficient stock for product Laptop (id=1)",
      "statusCode": 400
    }
  ```
