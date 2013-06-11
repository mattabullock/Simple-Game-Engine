using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Demo.Collisions;

namespace Demo.Comparer
{
    class BoundingBoxComparer : IComparer<BoundingShape>
    {

        public int Compare(BoundingShape a, BoundingShape b)
        {
            float minX1 = a.vertices[0].X;
            float minX2 = b.vertices[0].X;

            foreach (Vector2 v in a.vertices)
            {
                if (v.X < minX1)
                {
                    minX1 = v.X;
                }
            }

            foreach (Vector2 v in b.vertices)
            {
                if (v.X < minX1)
                {
                    minX2 = v.X;
                }
            }

            if (minX1 < minX2)
            {
                return -1;
            }
            else if (minX1 > minX2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}