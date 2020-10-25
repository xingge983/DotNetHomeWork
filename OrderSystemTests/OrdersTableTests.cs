using Microsoft.VisualStudio.TestTools.UnitTesting;
using L3_OrderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace L3_OrderSystem.Tests
{
    [TestClass()]
    public class OrdersTableTests
    {
        OrdersTable ordersTable = new OrdersTable();

        [TestMethod()]
        public void GetOrderTest()
        {
            Good book = new Good(1, "book", 9.9f);
            Customer customer5 = new Customer(5, "muji");
            Order order5 = new Order(4, customer5);
            order5.AddPurchase(new Purchase(book, 1));
            ordersTable.AddOrder(order5);
            Order getOrder = ordersTable.GetOrder(4);
            Assert.AreEqual(order5, getOrder);

        }

        [TestMethod()]
        public void AddOrderTest()
        {
            Good book = new Good(1, "book", 9.9f);
            Customer customer4 = new Customer(4, "muji");
            Order order4 = new Order(4, customer4);
            order4.AddPurchase(new Purchase(book, 1));
            ordersTable.AddOrder(order4);
            List<Order> allOrders = ordersTable.QueryAll();
            Assert.AreEqual(1, allOrders.Count);
            CollectionAssert.Contains(allOrders, order4);
        }

        [TestMethod()]
        public void RemoveOrderTest()
        {
            Customer customer1 = new Customer(1, "FBL");
            Customer customer2 = new Customer(2, "LZM");

            Good good1 = new Good(1, "good1", 330.5f);
            Good good2 = new Good(2, "good2", 220.6f);
            Good good3 = new Good(3, "good3", 500.3f);

            Order order1 = new Order(1, customer1);
            order1.AddPurchase(new Purchase(good1, 40));
            order1.AddPurchase(new Purchase(good2, 80));
            order1.AddPurchase(new Purchase(good3, 50));


            Order order2 = new Order(2, customer2);
            order2.AddPurchase(new Purchase(good1, 40));
            order2.AddPurchase(new Purchase(good3, 50));

            Order order3 = new Order(3, customer1);
            order3.AddPurchase(new Purchase(good1, 450));

            ordersTable.AddOrder(order1);
            ordersTable.AddOrder(order2);
            ordersTable.AddOrder(order3);

            ordersTable.RemoveOrder(ordersTable.GetOrder(3));
            List<Order> allOrders = ordersTable.QueryAll();
            Assert.AreEqual(allOrders.Count, 2);
            Assert.IsNotNull(ordersTable.GetOrder(1));
            Assert.IsNotNull(ordersTable.GetOrder(2));

        }

        [TestMethod()]
        public void UpdateTest()
        {
            Good book = new Good(1, "book", 9.9f);
            Good pen = new Good(1, "pen", 9.9f);
            Customer customer4 = new Customer(4, "jams");
            Order order6 = new Order(6, customer4);
            order6.AddPurchase(new Purchase(book, 1));
            ordersTable.AddOrder(order6);
            order6.AddPurchase(new Purchase(pen, 1));
            ordersTable.Update(order6);
            Assert.AreEqual(order6, ordersTable.GetOrder(6));

        }

        [TestMethod()]
        public void QueryByCustomerNameTest()
        {

            Customer customer1 = new Customer(1, "FBL");
            Customer customer2 = new Customer(2, "LZM");

            Good good1 = new Good(1, "good1", 330.5f);
            Good good2 = new Good(2, "good2", 220.6f);
            Good good3 = new Good(3, "good3", 500.3f);

            Order order1 = new Order(1, customer1);
            order1.AddPurchase(new Purchase(good1, 40));
            order1.AddPurchase(new Purchase(good2, 80));
            order1.AddPurchase(new Purchase(good3, 50));


            Order order2 = new Order(2, customer2);
            order2.AddPurchase(new Purchase(good1, 40));
            order2.AddPurchase(new Purchase(good3, 50));

            Order order3 = new Order(3, customer1);
            order3.AddPurchase(new Purchase(good1, 450));

            ordersTable.AddOrder(order1);
            ordersTable.AddOrder(order2);
            ordersTable.AddOrder(order3);

            Assert.AreEqual(ordersTable.QueryByCustomerName("FBL").Count, 2);
            Assert.AreEqual(ordersTable.QueryByCustomerName("LZM").Count, 1);
           
        }

        [TestMethod()]
        public void QueryByTotalAmountTest()
        {
            Customer customer1 = new Customer(1, "FBL");
            Customer customer2 = new Customer(2, "LZM");

            Good good1 = new Good(1, "good1", 330.5f);
            Good good2 = new Good(2, "good2", 220.6f);
            Good good3 = new Good(3, "good3", 500.3f);

            Order order1 = new Order(1, customer1);
            order1.AddPurchase(new Purchase(good1, 40));
            order1.AddPurchase(new Purchase(good2, 80));
            order1.AddPurchase(new Purchase(good3, 50));


            Order order2 = new Order(2, customer2);
            order2.AddPurchase(new Purchase(good1, 40));
            order2.AddPurchase(new Purchase(good3, 50));

            Order order3 = new Order(3, customer1);
            order3.AddPurchase(new Purchase(good1, 450));

            ordersTable.AddOrder(order1);
            ordersTable.AddOrder(order2);
            ordersTable.AddOrder(order3);

            Assert.AreEqual(ordersTable.QueryByTotalAmount(20000).Count, 3);
            Assert.AreEqual(ordersTable.QueryByTotalAmount(50000).Count, 2);
        }

        [TestMethod()]
        public void QueryByGoodNameTest()
        {
            Customer customer1 = new Customer(1, "FBL");
            Customer customer2 = new Customer(2, "LZM");

            Good good1 = new Good(1, "good1", 330.5f);
            Good good2 = new Good(2, "good2", 220.6f);
            Good good3 = new Good(3, "good3", 500.3f);

            Order order1 = new Order(1, customer1);
            order1.AddPurchase(new Purchase(good1, 40));
            order1.AddPurchase(new Purchase(good2, 80));
            order1.AddPurchase(new Purchase(good3, 50));


            Order order2 = new Order(2, customer2);
            order2.AddPurchase(new Purchase(good1, 40));
            order2.AddPurchase(new Purchase(good3, 50));

            Order order3 = new Order(3, customer1);
            order3.AddPurchase(new Purchase(good1, 450));

            ordersTable.AddOrder(order1);
            ordersTable.AddOrder(order2);
            ordersTable.AddOrder(order3);

            Assert.AreEqual(ordersTable.QueryByGoodName("good1").Count, 3);
        }

        [TestMethod()]
        public void ExportTest()
        {
            Customer customer1 = new Customer(1, "FBL");
            Customer customer2 = new Customer(2, "LZM");

            Good good1 = new Good(1, "good1", 330.5f);
            Good good2 = new Good(2, "good2", 220.6f);
            Good good3 = new Good(3, "good3", 500.3f);

            Order order1 = new Order(1, customer1);
            order1.AddPurchase(new Purchase(good1, 40));
            order1.AddPurchase(new Purchase(good2, 80));
            order1.AddPurchase(new Purchase(good3, 50));


            Order order2 = new Order(2, customer2);
            order2.AddPurchase(new Purchase(good1, 40));
            order2.AddPurchase(new Purchase(good3, 50));

            Order order3 = new Order(3, customer1);
            order3.AddPurchase(new Purchase(good1, 450));

            ordersTable.AddOrder(order1);
            ordersTable.AddOrder(order2);
            ordersTable.AddOrder(order3);

            String expectXMLFile = "../../ordersTarget.xml";
            ordersTable.Export(expectXMLFile);

            String outputXMLFile = "../../ordersTarget2.xml";
            ordersTable.Export(outputXMLFile);

            Assert.IsTrue(File.Exists(outputXMLFile));

            string[] expectStr = File.ReadAllLines(expectXMLFile);
            string[] outputStr = File.ReadAllLines(outputXMLFile);
            Assert.AreEqual(expectStr.Length, outputStr.Length);

            for (int i = 0; i < expectStr.Length; i++)
            {
                Assert.AreEqual(expectStr[i].Trim(), outputStr[i].Trim());
            }

        }

        [TestMethod()]
        public void ImportTest()
        {
            Customer customer1 = new Customer(1, "FBL");
            Customer customer2 = new Customer(2, "LZM");

            Good good1 = new Good(1, "good1", 330.5f);
            Good good2 = new Good(2, "good2", 220.6f);
            Good good3 = new Good(3, "good3", 500.3f);

            Order order1 = new Order(1, customer1);
            order1.AddPurchase(new Purchase(good1, 40));
            order1.AddPurchase(new Purchase(good2, 80));
            order1.AddPurchase(new Purchase(good3, 50));


            Order order2 = new Order(2, customer2);
            order2.AddPurchase(new Purchase(good1, 40));
            order2.AddPurchase(new Purchase(good3, 50));

            Order order3 = new Order(3, customer1);
            order3.AddPurchase(new Purchase(good1, 450));

            ordersTable.AddOrder(order1);
            ordersTable.AddOrder(order2);
            ordersTable.AddOrder(order3);

            String expectXMLFile = "../../ordersTarget.xml";
            ordersTable.Export(expectXMLFile);

            OrdersTable os = new OrdersTable();
            List<Order> list = os.Import("../../ordersTarget.xml");
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 3);

        }
    }
}