using System;
using System.Collections.Generic;

class Database
{
    private List<Product> products;
    private List<Cart> carts;
    private List<Order> orders;

    public Database()
    {
        products = new List<Product>();
        carts = new List<Cart>();
        orders = new List<Order>();
    }

    public void SaveProduct(Product product)
    {
        Product existingProduct = GetProductByName(product.Name);
        if (existingProduct != null)
        {
            existingProduct.Quantity += product.Quantity;
        }
        else
        {
            products.Add(product);
        }
    }

    public void SaveProducts(List<Product> updatedProducts)
    {
        foreach (Product updatedProduct in updatedProducts)
        {
            Product existingProduct = GetProductByName(updatedProduct.Name);
            if (existingProduct != null)
            {
                existingProduct.Quantity = updatedProduct.Quantity;
            }
        }
    }

    public List<Product> GetProducts()
    {
        return products;
    }

    public Product GetProductByName(string name)
    {
        return products.Find(product => product.Name == name);
    }

    public void SaveCart(Cart cart)
    {
        carts.Add(cart);
    }

    public Cart GetCartData()
    {
        return carts.Count > 0 ? carts[0] : null;
    }

    public void SaveOrder(Order order)
    {
        orders.Add(order);
    }

    public List<Order> GetOrderData()
    {
        return orders;
    }

    public void RemoveProduct(Product product)
    {
        products.Remove(product);
    }
}