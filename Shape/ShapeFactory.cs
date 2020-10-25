using System;
namespace Shapes
{
	 class ShapeFactory
	{

		public static IShape CreateRandomShape()
		{

			Random random = new Random();
			IShape result = null;
			int type = random.Next(4);
			switch (type)
			{
				case 0:
					result = new Square(random.Next(100));
					break;
				case 1:
					result = new Rectangle(random.Next(100), random.Next(100));
					break;
				case 2:
					result = new Triangle(random.Next(100), random.Next(100), random.Next(100));
					break;
				case 3:
					result = new Circle(random.Next(100));
					break;
			}
			return result;
		}
	}
}