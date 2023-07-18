using System;
using System.Collections.Generic;

class Order
{
    public Order()
    {
    }
    public DateTime Date { get; }
    public List<Product> Products { get; }
    public float Subtotal { get; }
    public float Taxes { get; }
    public float TotalValue { get; }

    public Order(DateTime date, List<CartItem> cartItems, float subtotal, float taxes)
    {
        Date = date;
        Products = new List<Product>();
        foreach (var cartItem in cartItems)
        {
            Products.Add(cartItem.Product);
        }
        Subtotal = subtotal;
        Taxes = taxes;
        TotalValue = Subtotal + Taxes;
    }

    private float CalculateTaxes(float subtotal)
    {
        TaxCalculator taxCalculator = new TaxCalculator();
        return taxCalculator.CalculateTaxes(subtotal);
    }

    public string GenerateOrderString()
    {
        string orderString = "";
        orderString += $"Order Date: {Date}\n";
        orderString += "Ordered Products:\n";

        foreach (Product product in Products)
        {
            orderString += $"{product.Name} - {product.Price:C} x {product.Quantity} = {product.Price * product.Quantity:C}\n";
        }

        orderString += $"Subtotal: {Subtotal:C}\n";
        orderString += $"Taxes: {Taxes:C}\n";
        orderString += $"Total Value: {TotalValue:C}\n";

        return orderString;
    }
}