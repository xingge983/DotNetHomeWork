using System;
using System.Collections.Generic;

namespace Shapes
{
    interface IShape
    {
        double Area { get; }
        bool verify();
    }
}