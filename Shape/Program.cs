using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shapes;
namespace Shapes
{
    class Program
    {
        static void Main(string[] args)
        {
            double area=0;
            List<IShape> shapes=new List<IShape>();
            for(int i=0;i<10;i++){
                shapes.Add(ShapeFactory.CreateRandomShape());
            }
            foreach(IShape shape in shapes){
                area+=shape.Area;
            }
            Console.WriteLine(area);
        }
    }
}
