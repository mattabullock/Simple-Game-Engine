using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.Interfaces;

namespace Demo.Comparer
{
    class ShapeComparer : IComparer<CollidableObject>
    {
        public int Compare(CollidableObject a, CollidableObject b)
        {
            if (a.draw && !b.draw)
            {
                return -1;
            }
            if ((a.draw && b.draw) || (!a.draw && !b.draw))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
