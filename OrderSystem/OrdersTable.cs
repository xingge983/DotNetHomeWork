using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace OrderSystem
{
    public class OrdersTable
    {
        private List<Order> orderList;

        public OrdersTable()
        {
            orderList = new List<Order>();
        }

        public Order GetOrder(uint orderId)
        {
            IEnumerable<Order> query = orderList.Where(o => o.Id == orderId);
            if (query != null)
            {
                return query.ToList()[0];
            }
            else
            {
                throw new Exception($"the Order of Id:{orderId} doesn't exist in the OrdersTable.");
            }
        }

        public void AddOrder(Order order)
        {
            if (orderList.Contains(order))
            {
                throw new ApplicationException($"the orderList contains an order with ID {order.Id} !");
            }
            orderList.Add(order);
        }



        public void RemoveOrder(Order order)
        {
            orderList.Remove(order);
        }

        public void Update(Order order)
        {
            orderList.Remove(GetOrder(order.Id));
            orderList.Add(order);
        }

        public List<Order> QueryAll()
        {
            return orderList;
        }

        public List<Order> QueryByCustomerName(string customerName)
        {
            var query = orderList.Where(o => o.Customer.Name == customerName);
            return query.ToList();
        }

        public List<Order> QueryByTotalAmount(float totalAmount)
        {
            var query = orderList.Where(o => o.TotalAmount >= totalAmount);
            return query.ToList();
        }

        public List<Order> QueryByGoodName(string goodName)
        {
            var query = orderList.Where(o => o.PurchaseList.Exists(d => d.Good.Name == goodName));
            return query.ToList();
        }

        public void Sort(Comparison<Order> comparison)
        {
            orderList.Sort(comparison);
        }

        public void Export(String fileName)
        {
            if(Path.GetExtension(fileName)!=".xml")
            {
                throw new ArgumentException("the exported file must be a xml file!");
            }
            else
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    xs.Serialize(fs, this.orderList);
                }
            }
        }

        public List<Order> Import(string path)
        {
            if (Path.GetExtension(path) != ".xml")
                throw new ArgumentException("the imported file must be a xml file!");
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            List<Order> result = new List<Order>();
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    return (List<Order>)xs.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("import error:" + e.Message);
            }

        }

    }

}

