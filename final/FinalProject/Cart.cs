using System;
using System.Collections.Generic;

class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public float Subtotal => Product.Price * Quantity;
}

class Cart
{
    private List<CartItem> cartItems;

    public Cart()
    {
        cartItems = new List<CartItem>();
    }

    public void AddProduct(Product product, int quantity)
    {
        CartItem existingItem = cartItems.Find(item => item.Product == product);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cartItems.Add(new CartItem { Product = product, Quantity = quantity });
        }
    }

    public void RemoveProduct(Product product)
    {
        CartItem existingItem = cartItems.Find(item => item.Product == product);
        if (existingItem != null)
        {
            cartItems.Remove(existingItem);
        }
    }

    public List<CartItem> GetCartItems()
    {
        return cartItems;
    }

    public float GetTotal()
    {
        float total = 0;
        foreach (CartItem cartItem in cartItems)
        {
            total += cartItem.Subtotal;
        }
        return total;
    }

    public void ClearCart()
    {
        cartItems.Clear();
    }

    public CartItem GetCartItemByProduct(Product product)
    {
        return cartItems.FirstOrDefault(item => item.Product == product);
    }
}