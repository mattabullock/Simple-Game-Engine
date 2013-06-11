using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Demo.Interfaces;
using Demo.Collisions;

namespace Demo.Handlers
{
    class CollisionHandler
    {

        public static double shortestCollision { get; set; }
        public static Vector2 axisCollision { get; set; }

        public static Boolean[] checkCollisions(Tracker t) {
            Boolean[] collide = new Boolean[Program.g.level.drawIndex];
            axisCollision = Vector2.Zero;
            shortestCollision = double.PositiveInfinity;
            for (int i = 0; i < Program.g.level.drawIndex; i++)
            {
                BoundingShape bShape = Program.g.level.levelObjects[i].b;
                Vector2[] checkedVectors = new Vector2[bShape.vectors.Length];
                collide[i] = true;
                for (int j = 0; j < bShape.vectors.Length; j++)
                {
                    Vector2 leftNormal = Vector2.Normalize(new Vector2(-bShape.vectors[j].Y, bShape.vectors[j].X));
                    if (!checkedVectors.Contains(Vector2.Negate(leftNormal)))
                    {
                        if (!t.b.axisIntersects(bShape, leftNormal))
                        {
                            collide[i] = false;
                            break;
                        }
                        checkedVectors[j] = leftNormal;
                    }
                }
                if (collide[i])
                {
                    Console.WriteLine("CHandler: " + CollisionHandler.axisCollision);
                    Console.WriteLine("CLength: " + CollisionHandler.shortestCollision);
                }
            }
            return collide;
        }
    }
}
