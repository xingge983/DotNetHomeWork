using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    [Serializable]
    public class Order:IComparable<Order>
    {
        public uint Id
        {
            get;
            set;
        }

        public Customer Customer
        {
            get;
            set;
        }

        private List<Purchase> purchaseList = new List<Purchase>();

        public List<Purchase> PurchaseList
        {
            get => this.purchaseList;
        }

        public float TotalAmount
        {
            get => PurchaseList.Sum(p => p.Amount);
        }

        public Order(uint i_id, Customer i_customer)
        {
            this.Id = i_id;
            this.Customer = i_customer;
        }

        public Order()
        {

        }

        public Purchase GetPurchase(string i_goodName)
        {
            IEnumerable<Purchase> query = PurchaseList.Where(p => p.Good.Name == i_goodName);
            if (query != null)
            {
                return query.ToList()[0];
            }
            else
            {
                throw new Exception($"the purchase of good:{i_goodName} doesn't exist in the order.");
            }
        }

        public void AddPurchase(Purchase i_purchase)
        {
            if(this.PurchaseList.Contains(i_purchase))
            {
                throw new Exception($"Purchase of good:{i_purchase.Good.Name} already exist in the order.");
            }
            else
            {
                PurchaseList.Add(i_purchase);
            }
        }

        
        public void RemovePurchase(Purchase i_purchase)
        {
            if(PurchaseList.Contains(i_purchase))
            {
                PurchaseList.Remove(i_purchase);
            }
            else
            {
                throw new Exception($"the purchase of good:{i_purchase.Good.Name} doesn't exist in the order.");
            }
        }

        public void ReplacePurchase(Purchase newPurchase, Purchase oldPurchase)
        {
            RemovePurchase(oldPurchase);
            AddPurchase(newPurchase);
        }

        public int CompareTo(Order i_order)
        {
            if(this.Id>i_order.Id)
            {
                return 1;
            }
            else if(this.Id == i_order.Id)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public override bool Equals(object i_obj)
        {
            var i_order = i_obj as Order;
            if(i_order!=null)
            {
                return i_order.Id == this.Id;
            }
            else
            {
                return false;
            }

        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"OrederId:{Id}, Customer:{Customer}");
            PurchaseList.ForEach(p => result.Append("\n\t" + p));
            return result.ToString();
        }
    }
}
