using System;
namespace Shapes
{
	class Triangle : IShape
	{
		private double[] edges = new double[3];
		public Triangle(double a, double b, double c)
		{
			double[] edges = new double[3] { a, b, c };
			this.edges = edges;
			if (!verify()) throw new InvalidShapeException("invalid triangle");
		}

		public bool verify()
		{
			double a = edges[0], b = edges[1], c = edges[2];
			return (a > 0 && b > 0 && c > 0 && a + b > c && a + c > b && b + c > a);
		}

		public double Area
        {
            get{
				double p = (edges[0] + edges[1] + edges[2]) / 2;
				return Math.Sqrt(p * (p - edges[0]) * (p - edges[1]) * (p - edges[2])); 
			}
		}

	}
}