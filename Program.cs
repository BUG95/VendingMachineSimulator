using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace VendingMachineSimulator
{
    class Program
    {
         static List<VendingItem> Items;
         static void CheckItems() //FillMachine
        {
            Items = VendingItem.ReadVendingItems("Items.txt");
        }
        static void DisplayMachine()
        {
            byte i = 1;
            foreach (var VI in Items)
            {
                Console.WriteLine(i+ ". " + VI._item + ", Price: " + VI._price + "$, Quantity: " + VI._quantity);
                i++;
            } 
        }
       
        static bool ItemSelect(VendingItem item)
        {
            if (item._quantity == 0)
            {
                Console.WriteLine("This product is out of stock!");
                return true;
            }
            double paidAmount = 0, partialPay = 0;
            int quantity = 0,finalChoice=0;
            string testInput = null;
            string exception = null;
            Console.WriteLine("You choose " +item._item +"\nQuantity: ");
            try { quantity = Convert.ToInt32(Console.ReadLine()); }
            catch (System.Exception exc) { exception = exc.ToString(); }
            while(exception!=null)
            {
                Console.WriteLine("Invalid input\nQuantity:");
                exception = null;
                try { quantity = Convert.ToInt32(Console.ReadLine()); }
                catch (System.Exception exc) { exception = exc.ToString(); }
            }
            if(quantity<=0||quantity>item._quantity)
            {
                Console.WriteLine("The quantity is not avalaible!");
                return true;
            }
            Console.WriteLine("You choose "+quantity+" "+item._item+ "\nMake Payment: ("+quantity*item._price+"$)");
            while(paidAmount<quantity*item._price)
            {
                try { partialPay = Convert.ToDouble(Console.ReadLine()); }
                catch (System.Exception exc) { exception = exc.ToString(); }
                
                while(exception!=null)
                {
                    Console.WriteLine("Invalid input\nAmount due: "+Math.Round((quantity*item._price-paidAmount),2)+"$");
                    exception = null;
                    try { partialPay = Convert.ToDouble(Console.ReadLine()); }
                    catch (System.Exception exc) { exception = exc.ToString(); }
                }
                testInput = Convert.ToString(partialPay);
                while(partialPay<0.01 || partialPay >10 || testInput.Count()>4)
                {
                    if (partialPay < 0.01) Console.WriteLine("You cannot pay less than 0.01$");
                    else if (partialPay > 10) Console.WriteLine("You cannot pay over than 10$");
                    else Console.WriteLine("Invalid input\nAmount due: " + Math.Round((quantity * item._price - paidAmount),2) + "$");//
                    try { partialPay = Convert.ToDouble(Console.ReadLine()); }
                    catch (System.Exception exc) { exception = exc.ToString(); }
                    while(exception!=null)
                    {
                        Console.WriteLine("Invalid input\nMake Payment: " + Math.Round((quantity * item._price - paidAmount),2) + "$");//
                        exception = null;
                        try { partialPay = Convert.ToDouble(Console.ReadLine()); }
                        catch (System.Exception exc) { exception = exc.ToString(); }
                    }
                    testInput = Convert.ToString(partialPay);
                }
                paidAmount += partialPay;
                if (paidAmount < quantity * item._price)
                    Console.WriteLine("Amount due: " + Math.Round((quantity * item._price-paidAmount),2)+"$");
            }
            Console.Clear();
            Console.WriteLine("You bought " + quantity + " " + item._item + " for " + quantity * item._price + "$");
            item.ReduceInventory(quantity);
            if (paidAmount > quantity * item._price)
                Console.WriteLine("Your change is " + Math.Round((paidAmount-quantity * item._price ),2)+"$ (out of "+paidAmount+"$)");
            Console.WriteLine("\nPress 1 if you want to buy something else");
            Console.WriteLine("or press 2 for exit.");
            try { finalChoice = Convert.ToInt32(Console.ReadLine()); }
            catch (System.Exception exc) { exception = exc.ToString(); }
            while(exception!=null)
            {
                exception = null;
                Console.WriteLine("Invalid input");
                Console.WriteLine("\nPress 1 if you want to buy something else");
                Console.WriteLine("or press 2 for exit.");
                try { int choice = Convert.ToInt32(Console.ReadLine()); }
                catch (System.Exception exc) { exception = exc.ToString(); }
            } 
            if(finalChoice == 1)
            {
                Console.Clear();
                CheckItems();
                DisplayMachine();
                return true;
            }
            return false;
        }
        static void Main(string[] args)
        {
            CheckItems();
            DisplayMachine();
            int caseSwitch = 0;
            string exception = null;
            bool choose = true;
            while (choose)
            {
                
                Console.WriteLine("\nSelect one item: ");
                try { caseSwitch = Convert.ToByte(Console.ReadLine()); }
                catch (System.Exception exc) { exception = exc.ToString(); }
                while(exception!=null)
                {
                    exception = null;
                    Console.WriteLine("Invalid input");
                    try { caseSwitch = Convert.ToByte(Console.ReadLine()); }
                    catch (System.Exception exc) { exception = exc.ToString(); }
                }
                while(caseSwitch<=0)
                {
                    Console.WriteLine("\nSelect an item: ");
                    try { caseSwitch = Convert.ToByte(Console.ReadLine()); }
                    catch (System.Exception) { }
                }
                switch (caseSwitch)
                {
                    case 1:
                        {
                            VendingItem item = new VendingItem();
                            foreach(VendingItem vItem in Items)
                            {
                                if (vItem._item == "Pepsi")
                                {
                                    if (ItemSelect(vItem) == false) choose = false;
                                    break;
                                }    
                            }
                            break;
                        }
                    case 2:
                        {
                            VendingItem item = new VendingItem();
                            foreach (VendingItem vItem in Items)
                            {
                                if (vItem._item == "Diet Pepsi")
                                {
                                    if(ItemSelect(vItem)==false) choose=false;
                                    break;
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            VendingItem item = new VendingItem();
                            foreach (VendingItem vItem in Items)
                            {
                                if (vItem._item == "Coke")
                                {
                                    if (ItemSelect(vItem) == false)
                                        choose = false;
                                    
                                    break;
                                }
                            }
                            break;
                        }   
                    case 4:
                        {
                            VendingItem item = new VendingItem();
                            foreach (VendingItem vItem in Items)
                            {
                                if (vItem._item == "Diet Coke")
                                {
                                    if (ItemSelect(vItem) == false) choose=false;
                                    break;
                                }
                            }
                            break;
                        }
                    case 5:
                        {
                            VendingItem item = new VendingItem();
                            foreach (VendingItem vItem in Items)
                            {
                                if (vItem._item == "Kit-Kat")
                                {
                                    if (ItemSelect(vItem) == false) choose = false;
                                    break;
                                }
                            }
                            break;
                        }
                    case 6:
                        {
                            VendingItem item = new VendingItem();
                            foreach (VendingItem vItem in Items)
                            {
                                if (vItem._item == "Twix")
                                {
                                    if (ItemSelect(vItem) == false) choose = false;
                                    break;
                                }
                            }
                            break;
                        }
                    case 7:
                        {
                            VendingItem item = new VendingItem();
                            foreach (VendingItem vItem in Items)
                            {
                                if (vItem._item == "Lays Potato Chips")
                                {
                                    if (ItemSelect(vItem) == false) choose = false;
                                    break;
                                }
                            }
                            break;
                        }
                    case 8:
                        {
                            VendingItem item = new VendingItem();
                            foreach (VendingItem vItem in Items)
                            {
                                if (vItem._item == "Cheetos")
                                {
                                    if (ItemSelect(vItem) == false) choose = false;
                                    break;
                                }
                            }
                            break;
                        } 
                }
            }
        }
    }
}
