public async Task<(bool Success, string Message)> PlaceOrderAsync(int productId, int quantity) {
    using var transaction = await _context.Database.BeginTransactionAsync();
    try {
        var product = await _context.Products.FindAsync(productId);
        if (product == null || product.Stock < quantity) return (false, "Invalid stock/product");

        product.Stock -= quantity;
        _context.Orders.Add(new Order { ProductId = productId, Quantity = quantity, TotalPrice = product.Price * quantity });
        
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return (true, "Order successful");
    } catch {
        await transaction.RollbackAsync();
        return (false, "Transaction failed");
    }
}