using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Користувач здійснює планування своїх покупок. Для цього він може додавати
//певну кількість Товару (Назва, виробник, ціна) до кошика та вилучати його.
//Забезпечити можливість відміни декількох останніх  дій користувача з кошиком. 
interface Commands
{
    void Add();
    void removeCommand();
}
class ComandsRealization
{
    protected List<string> products = new List<string>();
    protected Stack<Commands> commandHistory = new Stack<Commands>();
    public void addProduct(string prod)
    {
        products.Add(prod);
    }
    public void ShowProducts()
    {
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
    }
    public void removeProduct(string prod)
    {
        if(products.Count > 0)
        {
            products.Remove(prod);
        }
    }
    public void addCommand(Commands command)
    {
        commandHistory.Push(command);
    }
    public void removeCommand()
    {
        if (commandHistory.Count > 0)
        {
            var commandToRemuve = commandHistory.Pop();
            commandToRemuve.removeCommand();
        }
    }
}
class CommandAdd:Commands
{
    protected ComandsRealization command;
    protected string product;

    public CommandAdd(ComandsRealization command, string product)
    {
        this.command = command;
        this.product = product;
    }
    public void Add()
    {
        command.addProduct(product);
        command.addCommand(this);
    }
    public void removeCommand()
    {
        command.removeCommand();
    }
}

class CommandDel : Commands
{
    protected ComandsRealization command;
    protected string product;

    public CommandDel(ComandsRealization command, string product)
    {
        this.command = command;
        this.product = product;
    }
    public void Add()
    {
        command.removeProduct(product);
        command.addCommand(this);
    }
    public void removeCommand()
    {
        command.addProduct(product);
    }
}
namespace practiceA.css
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ComandsRealization prodInf = new ComandsRealization();

            var addApple = new CommandAdd(prodInf, "Яблуко");
            addApple.Add();

            var addBanana = new CommandAdd(prodInf, "Банан");
            addBanana.Add();

            prodInf.ShowProducts(); 
            var removeApple = new CommandDel(prodInf, "Яблуко");
            removeApple.Add();
            prodInf.ShowProducts(); 
            prodInf.removeCommand(); 
            prodInf.ShowProducts(); 
            prodInf.removeCommand(); 
            prodInf.ShowProducts();
        }
    }
}
