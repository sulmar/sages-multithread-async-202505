using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitExample;

internal class PrinterService
{
    public async Task PrintAndCalculateAsync()
    {
        CostCalculator calculator = new CostCalculator();
        var printer = new Printer();

        // Asynchroniczny z async-await
        await printer.PrintAsync("Document #1");
        decimal cost2 = await calculator.CalculateAsync("Document #1");
        Console.WriteLine($"Cost: {cost2:C2}");
    }
}
