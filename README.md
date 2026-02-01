# Mini Grocery Order System
A full-stack grocery ordering system built with .NET 8 and Ionic 6/Angular 18.

## 🚀 How to Run
1. **Backend**: Navigate to `GroceryAPI` and run `dotnet run`. (Server listens on Port 5079)
2. **Frontend**: Navigate to `GroceryUI` and run `ionic serve`.

## 🏗 Architecture
I followed a **Layered Architecture** to ensure clean separation of concerns:
- **Controllers**: Restricted to exactly 2 endpoints as per requirements. Handles request/response only.
- **Services**: The core business logic layer where order validation and transactions occur.
- **Repositories**: Dedicated to data access using Entity Framework Core and SQLite.
- **Models**: Defines the database schema and Data Transfer Objects (DTOs).

## 🛡 Business Logic & Data Integrity
The system handles all order operations within a **single database transaction** in the `OrderService.cs` file using `IDbContextTransaction`.

**The transaction ensures:**
1. **Product Validation**: Checking if the product exists and has sufficient stock.
2. **Stock Reduction**: Atomically decreasing the inventory count.
3. **Order Creation**: Generating the receipt record.

If any of these steps fail, the entire process rolls back to prevent data corruption or "phantom" stock issues.