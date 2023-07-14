class ECommerceApp
{
    private UserSystem userSystem;
    private ManagementSystem managementSystem;

    public ECommerceApp()
    {
        userSystem = new UserSystem();
        managementSystem = new ManagementSystem();
    }

    public void RunUserSystem()
    {
        // User System Menu
        Console.Clear();
        Console.WriteLine("Marco Antonio E-commerce - Front Office");
    }

    public void RunAdminSystem()
    {
        // Admin System Menu
        Console.Clear();
        Console.WriteLine("Marco Antonio E-commerce - Back Office");
    }
}