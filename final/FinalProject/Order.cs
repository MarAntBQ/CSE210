class Order
{
    public DateTime Date { get; set; }
    public List<Product> Products { get; set; }
    public float Subtotal { get; set; }
    public float Taxes { get; set; }
    public float TotalValue { get; set; }

    public void AddOrder(Order order)
    {
        // Add the order to the system
    }

    public string GenerateOrderString()
    {
        // Generate a string representation of the order
        return "";
    }

    public float CalculateSubtotal()
    {
        // Calculate the subtotal based on the selected products
        return 0;
    }

    public float CalculateTaxes()
    {
        // Calculate the taxes based on the subtotal
        return 0;
    }

    public float CalculateTotalValue()
    {
        // Calculate the total value including taxes
        return 0;
    }
}