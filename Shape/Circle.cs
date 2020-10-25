using System;

namespace Shapes
{
	class Circle : IShape
	{
		private double radius;
		public Circle(double radius)
		{
			this.radius = radius;
			if (!verify()) throw new InvalidShapeException("invalid circle");
		}

		public bool verify()
		{
			return radius > 0;
		}

		public double Area => Math.PI * radius * radius;
	}
}
