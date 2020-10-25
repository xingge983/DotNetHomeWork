using System;
namespace Shapes
{
	class Square : IShape
	{
		private double length;
		public Square(double length)
		{
			this.length = length;
			if (!verify()) throw new InvalidShapeException("invalid square");
		}

		public bool verify()
		{
			return length > 0;
		}

		public double Area =>length * length;
		
	}
}