using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VendingMachineSimulator
{
    class VendingItem
    {
        private string item;
        private double price;
        private byte quantity;
        public VendingItem(string _item=null,double _price=0,byte _quantity=0)
        {
            item = _item;
            price = _price;
            quantity = _quantity;
        }
        public string _item
        {
            get { return item; }
            set { item = value; }
        }
        public double _price
        {
            get { return price; }
            set { price = value; }
        }
        public byte _quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        
        public static List<VendingItem>ReadVendingItems(string file_name)
        {
            List<VendingItem> list = new List<VendingItem>();
            FileStream fs = null;
            StreamReader sr = null;
            string tempItem = null;
            double tempPrice = 0;
            byte tempQuantity = 0;

            try
            {
                fs = new FileStream(file_name, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);
                for(; ; )
                {
                    string line = sr.ReadLine();
                    if (line == null) break;
                    line = line.Substring(line.IndexOf(" ")+2);
                    tempItem=line.Substring(0,line.IndexOf("\""));
                    line = line.Substring(line.IndexOf(":") + 2);
                    tempPrice = Convert.ToDouble(line.Substring(0, line.IndexOf("$")));
                    line = line.Substring(line.IndexOf("\"") + 1);
                    tempQuantity=Convert.ToByte(line.Substring(0,line.IndexOf("\"")));
                    VendingItem vItem = new VendingItem(tempItem, tempPrice, tempQuantity);
                    list.Add(vItem);
                }
            }
            catch (System.Exception) { }
            finally
            {
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();
            }
            return list;
        }
        public void ReduceInventory(int Q)
        {
            List<VendingItem> list = new List<VendingItem>();
            list = ReadVendingItems("Items.txt");
            FileStream fs = null;
            StreamReader sr = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream("Items.txt", FileMode.Create, FileAccess.ReadWrite);
                sr = new StreamReader(fs);
                sw = new StreamWriter(fs);
                foreach(VendingItem VI in list)
                {
                    if (VI._item==this._item)
                    sw.WriteLine("{Item: \"" + VI.item + "\", Price: " + VI.price + "$, Quantity: \"" + (VI.quantity-Q) + "\"}");
                    else
                        sw.WriteLine("{Item: \"" + VI.item + "\", Price: " + VI.price + "$, Quantity: \"" + VI.quantity + "\"}");

                }
            }
            catch (System.Exception) { }
            finally
            {
                if (sw != null) sw.Flush();
                if (sw != null) sw.Close();
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();
            }

        }
    }
}
