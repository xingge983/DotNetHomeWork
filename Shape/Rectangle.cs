using System;

namespace Shapes
{
	class Rectangle : IShape
	{
		private double width;
		private double height;
		public Rectangle(double width, double height)
		{
			this.width = width;
			this.height = height;
			if (!verify()) throw new InvalidShapeException("invalid rectangle");
		}

		public bool verify()
		{
			return width > 0 && height > 0;
		}

		public double Area => width * height;
	}
	
}
