using System;

class UserSystem
{
    private Database database;

    public UserSystem(Database database)
    {
        this.database = database;
    }

    public void ViewProducts()
    {
        var products = database.GetProducts();

        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        Console.WriteLine("Available Products:");
        foreach (var product in products)
        {
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine($"Stock Available: {product.Quantity}");
            Console.WriteLine($"Price: ${product.Price}");
            Console.WriteLine();
        }
    }


    public void AddProductToCart()
    {
        List<Product> products = database.GetProducts();

        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        Console.WriteLine("Available Products:");
        Cart cart = database.GetCartData();
        for (int i = 0; i < products.Count; i++)
        {
            Product product = products[i];
            int availableStock = product.Quantity;

            if (cart != null)
            {
                CartItem cartItem = cart.GetCartItemByProduct(product);
                if (cartItem != null)
                {
                    availableStock -= cartItem.Quantity;
                }
            }

            Console.WriteLine($"{i + 1}. {product.Name} | ${product.Price} | Available: {product.Quantity} | In your Cart: {GetQuantityInCart(cart, product)}");
        }

        int productIndex;
        do
        {
            productIndex = GetProductIndex();
        } while (productIndex < 0 || productIndex >= products.Count);

        Product selectedProduct = products[productIndex];
        if (selectedProduct.Quantity == 0)
        {
            Console.WriteLine("Not Stock Available.");
            return;
        }

        int quantityToAdd;
        do
        {
            Console.Write("Enter the quantity: ");
            if (!int.TryParse(Console.ReadLine(), out quantityToAdd) || quantityToAdd <= 0 || quantityToAdd > selectedProduct.Quantity)
            {
                Console.WriteLine("Invalid quantity. Please enter a valid quantity.");
            }
        } while (quantityToAdd <= 0 || quantityToAdd > selectedProduct.Quantity);

        int totalQuantityInCart = GetQuantityInCart(cart, selectedProduct);
        int remainingStock = selectedProduct.Quantity - totalQuantityInCart;

        if (quantityToAdd > remainingStock)
        {
            Console.WriteLine($"Cannot add {quantityToAdd} to cart. Only {remainingStock} available.");
            return;
        }

        Cart userCart = cart ?? new Cart();
        userCart.AddProduct(selectedProduct, quantityToAdd);
        database.SaveCart(userCart);

        Console.WriteLine($"{quantityToAdd} of {selectedProduct.Name} has been added to your cart.");
    }

    private int GetQuantityInCart(Cart cart, Product product)
    {
        if (cart == null)
        {
            return 0;
        }

        CartItem cartItem = cart.GetCartItemByProduct(product);
        return cartItem != null ? cartItem.Quantity : 0;
    }


    public void ViewCart()
    {
        Cart cart = database.GetCartData();
        List<CartItem> cartItems = cart?.GetCartItems();

        if (cartItems == null || cartItems.Count == 0)
        {
            Console.WriteLine("Your cart is empty.");
            return;
        }

        Console.WriteLine("Your Cart:");
        foreach (var item in cartItems)
        {
            Console.WriteLine($"{item.Product.Name} - Quantity: {item.Quantity} - Price: ${item.Product.Price}");
        }
        Console.WriteLine($"Total: ${cart.GetTotal()}");

        ShowCartSubMenu(cart);
    }

    public void ShowCartSubMenu(Cart cart)
    {
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Cart Options:");
        Console.WriteLine("1. Remove Product");
        Console.WriteLine("2. Clear Cart");
        Console.WriteLine("3. Return to Menu");
        Console.WriteLine("--------------------------------");
        Console.Write("Enter your choice: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                RemoveProductFromCart(cart);
                break;
            case "2":
                cart.ClearCart();
                database.SaveCart(cart);
                Console.WriteLine("Your cart has been cleared.");
                break;
            case "3":
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    public void DisplayCartItems(List<CartItem> cartItems)
    {
        for (int i = 0; i < cartItems.Count; i++)
        {
            CartItem cartItem = cartItems[i];
            Console.WriteLine($"{i + 1}. {cartItem.Product.Name} - Quantity: {cartItem.Quantity} - Price: ${cartItem.Product.Price}");
        }
    }

    public void RemoveProductFromCart(Cart cart)
    {
        List<CartItem> cartItems = cart.GetCartItems();

        Console.WriteLine("Your Cart Items:");
        DisplayCartItems(cartItems);

        int productIndex;
        do
        {
            Console.Write("Enter the index of the product to remove: ");
        } while (!int.TryParse(Console.ReadLine(), out productIndex) || productIndex <= 0 || productIndex > cartItems.Count);

        var removedProduct = cartItems[productIndex - 1].Product;
        cart.RemoveProduct(removedProduct);
        database.SaveCart(cart);

        Console.WriteLine($"{removedProduct.Name} has been removed from your cart.");
    }

    public void Checkout()
    {
        Cart cart = database.GetCartData();
    
        if (cart == null || cart.GetCartItems().Count == 0)
        {
            Console.WriteLine("Your cart is empty. Nothing to checkout.");
            return;
        }
    
        Console.WriteLine("Your Cart:");
        foreach (var item in cart.GetCartItems())
        {
            Console.WriteLine($"{item.Product.Name} - Quantity: {item.Quantity} - Price: ${item.Product.Price}");
        }
        Console.WriteLine($"Total: ${cart.GetTotal()}");
    
        TaxCalculator taxCalculator = new TaxCalculator();
        float totalWithTaxes = taxCalculator.CalculateTaxes(cart.GetTotal());
    
        float orderTotal = cart.GetTotal() + totalWithTaxes;
    
        Console.WriteLine($"Cart Total: ${cart.GetTotal()}");
        Console.WriteLine($"Tax: ${totalWithTaxes}");
        Console.WriteLine($"Order Total: ${orderTotal}");
    
        Console.Write("Do you want to place the order? (YES or NO): ");
        string answer = Console.ReadLine().ToUpper();
    
        if (answer == "YES")
        {
            Order order = new Order(DateTime.Now, cart.GetCartItems(), cart.GetTotal(), totalWithTaxes);
            string orderString = order.GenerateOrderString();
            File.AppendAllText("orders.txt", orderString + "\n");
    
            List<CartItem> cartItems = cart.GetCartItems();
            foreach (CartItem cartItem in cartItems)
            {
                Product product = cartItem.Product;
                product.Quantity -= cartItem.Quantity;
            }
            database.SaveProducts(cartItems.ConvertAll(cartItem => cartItem.Product));
    
            cart.ClearCart();
            database.SaveCart(cart);
    
            Console.WriteLine("Order placed successfully!");
        }
        else
        {
            Console.WriteLine("Order has been cancelled.");
        }
    }

    public int GetProductIndex()
    {
        Console.Write("Enter Product Index: ");
        if (int.TryParse(Console.ReadLine(), out int productIndex))
        {
            if (productIndex > 0 && productIndex <= database.GetProducts().Count)
            {
                return productIndex - 1;
            }
        }

        Console.WriteLine("Invalid product index.");
        return -1;
    }
    public void ViewMyOrders()
    {
        if (!File.Exists("orders.txt"))
        {
            Console.WriteLine("You have no orders.");
            return;
        }

        Console.WriteLine("Your Orders:");
        Console.WriteLine("--------------------------------");

        using (StreamReader reader = new StreamReader("orders.txt"))
        {
            string orderLine;
            while ((orderLine = reader.ReadLine()) != null)
            {
                Console.WriteLine(orderLine);
                Console.WriteLine("--------------------------------");
            }
        }
    }
}