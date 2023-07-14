class Cart
{
    public List<Product> CartData { get; set; }

    public void AddProduct(Product product, int quantity)
    {
        // Add the product to the cart with the specified quantity
    }

    public void RemoveProduct(Product product)
    {
        // Remove the product from the cart
    }

    public void CleanCart()
    {
        // Remove all products from the cart
    }

    public List<Product> GetCartData()
    {
        return CartData;
    }
}