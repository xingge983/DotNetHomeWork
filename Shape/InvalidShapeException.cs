using System;
namespace Shapes
{
	class InvalidShapeException : Exception
	{
		public InvalidShapeException(String message)
		{
			Console.WriteLine(message);
		}
	}
}
