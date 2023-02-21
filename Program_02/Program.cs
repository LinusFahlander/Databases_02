using Program_2.Services;

var menu = new MenuService();

while (true)
{
    Console.Clear();
    Console.WriteLine("Välj ett av följande alternativ:");
    Console.WriteLine("1. Lägg till en anställd");
    Console.WriteLine("2. Visa alla anställda");
    Console.WriteLine("3. Visa en specifik anställd");
    Console.Write("Val: ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            menu.CreateNewEmployee();
            break;

        case "2":
            Console.Clear();
            menu.ViewAllEmployees();
            break;

        case "3":
            Console.Clear();
            menu.ViewSpecificEmployee();
            break;
    }

    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
    Console.ReadKey();
}