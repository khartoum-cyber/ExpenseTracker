namespace ExpenseTracker
{
    internal class App
    {
        public void Run()
        {
            var isRunning = true;

            Console.Write("Enter command: ");
            var command = Console.ReadLine() ?? string.Empty;

            while (isRunning)
            {
                isRunning = command switch
                {
                    "exit" => false,
                    _ => isRunning
                };
            }
        }
    }
}
