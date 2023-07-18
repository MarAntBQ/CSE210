using System;
using System.Collections.Generic;

class ManagementSystem
{
    private Database database;

    public ManagementSystem(Database database)
    {
        this.database = database;
    }

    public void AddProductWithUserInput()
    {
        Console.WriteLine("Add New Product:");

        string name;
        bool productExists;

        do
        {
            Console.Write("Enter product name: ");
            name = Console.ReadLine();
            productExists = database.GetProductByName(name) != null;

            if (productExists)
            {
                Console.WriteLine("Product already exists. Please insert a different name.");
            }
        } while (productExists);

        Console.Write("Enter product quantity: ");
        int quantity = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter product description: ");
        string description = Console.ReadLine();

        Console.Write("Enter product price: ");
        float price = Convert.ToSingle(Console.ReadLine());

        Product newProduct = new Product
        {
            Name = name,
            Quantity = quantity,
            Description = description,
            Price = price
        };

        AddProduct(newProduct);
        Console.WriteLine("Product added successfully.");
    }

    public void AddProduct(Product product)
    {
        database.SaveProduct(product);
    }

    public void UpdateStock()
    {
        List<Product> products = database.GetProducts();

        if (products.Count == 0)
        {
            Console.WriteLine("No products found.");
            return;
        }

        ShowProductList();
        int productIndex;

        do
        {
            productIndex = GetProductIndex();
        } while (productIndex < 0 || productIndex >= products.Count);

        int additionalQuantity = GetAdditionalQuantityFromUser();

        Product selectedProduct = products[productIndex];
        database.SaveProduct(new Product
        {
            Name = selectedProduct.Name,
            Quantity = additionalQuantity
        });

        Console.WriteLine("Stock quantity updated successfully.");
    }

    public int GetAdditionalQuantityFromUser()
    {
        Console.Write("Enter new quantity to be added: ");
        if (int.TryParse(Console.ReadLine(), out int newQuantity))
        {
            return newQuantity;
        }

        Console.WriteLine("Invalid quantity. Please enter a valid number.");
        return 0;
    }

    public void ShowProductList()
    {
        List<Product> products = database.GetProducts();
        Console.WriteLine("Product List:");
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i].Name} ({products[i].Quantity})");
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

    public int GetNewQuantity()
    {
        Console.Write("Enter new quantity to be added: ");
        if (int.TryParse(Console.ReadLine(), out int newQuantity))
        {
            return newQuantity;
        }

        Console.WriteLine("Invalid quantity. Please enter a valid number.");
        return 0;
    }

    public void RemoveProduct()
    {
        List<Product> products = database.GetProducts();
    
        if (products.Count == 0)
        {
            Console.WriteLine("No products found.");
            return;
        }
    
        ShowProductList();
        int productIndex;
    
        do
        {
            productIndex = GetProductIndex();
        } while (productIndex < 0 || productIndex >= products.Count);
    
        Product selectedProduct = products[productIndex];
    
        Console.Write($"Do you want to delete {selectedProduct.Name}? Type 'YES' or 'NOT': ");
        string userInput = Console.ReadLine().Trim().ToUpper();
    
        if (userInput == "YES")
        {
            database.RemoveProduct(selectedProduct);
            Console.WriteLine("Product removed successfully.");
        }
        else
        {
            Console.WriteLine("Product deletion canceled.");
        }
    }

}