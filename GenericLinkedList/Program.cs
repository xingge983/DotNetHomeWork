using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericLinkedList
{
    public class Node<T>
    {
        public Node(T t){
            Next = null;
            Data = t;
        }
        public Node<T> Next { get; set; }
        public T Data { get; set; }

    }

    public class  GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public GenericList()
        {
            head = tail = null;
        }

        public Node<T> Head
        {
            get { return head; }
        }
        public void Add(T t)
        {
            Node<T> node = new Node<T>(t);
            if (tail == null)
            {
                head = tail = node;
            }
            else
            {
                tail.Next = node;
                tail = node;
            }
        }

        public void ForEach(Action<T> action)
        {
            for(Node<T> node = head; node != tail; node = node.Next)
            {
                action(node.Data);
            }
        }
    }
    class Program
    {
        private const int V = 0;

        static void Main(string[] args)
        {
            double sum = 0;
            double max , min;
            GenericList<int> genericList = new GenericList<int>();
            Random random = new Random();
            for(int i = 0; i < 30; i++)
            {
                genericList.Add(random.Next(100));
            }
            max = min = genericList.Head.Data;

            Action<int> p = m =>
            {
                Console.Write(m + "\t");
                if (max < m) max = m;
                if (min > m) min = m;
                sum += m;
            };
            genericList.ForEach(p);
            Console.WriteLine($"max:{max},min:{min},sum:{sum}");
        }
    }
}
