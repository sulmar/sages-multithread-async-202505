using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitExample;

internal class PrinterService
{
    private readonly Printer printer;

    public PrinterService(Printer printer)
    {
        this.printer = printer;
    }

    public async Task PrintAndCalculateAsync()
    {
        CostCalculator calculator = new CostCalculator();

        // var printer = lazyPrinter.Value;

        // Asynchroniczny z async-await
        await printer.PrintAsync("Document #1");
        decimal cost2 = await calculator.CalculateAsync("Document #1");
        Console.WriteLine($"Cost: {cost2:C2}");
    }
}
