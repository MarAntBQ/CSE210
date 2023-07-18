using System;

class Product
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }

    public string GetName()
    {
        return Name;
    }

    public int GetQuantity()
    {
        return Quantity;
    }

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
    }

    public string GetDescription()
    {
        return Description;
    }

    public float GetPrice()
    {
        return Price;
    }

    public void ReduceStock(int quantityToReduce)
    {
        if (quantityToReduce <= Quantity)
        {
            Quantity -= quantityToReduce;
            Console.WriteLine($"Reduced {quantityToReduce} units of {Name} from stock.");
        }
        else
        {
            Console.WriteLine($"Not enough stock for {Name}. Stock available: {Quantity}");
        }
    }
}