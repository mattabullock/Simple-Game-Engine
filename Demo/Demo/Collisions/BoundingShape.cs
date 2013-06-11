using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Demo.Handlers;

namespace Demo.Collisions
{
    public class BoundingShape
    {

        public Vector2[] vertices { get; set; }
        public Vector2[] vectors { get; set; }

        public BoundingShape(Vector2[] vertices)
        {
            this.vertices = vertices;
            vectors = new Vector2[vertices.Length];
            
            for (int i = 0; i < vertices.Length; i++)
            {
                int index1 = i;
                int index2 = i + 1;
                if (index2 == vertices.Length)
                {
                    index2 = 0;
                }
                vectors[i] = new Vector2(vertices[index2].X - vertices[index1].X, vertices[index2].Y - vertices[index1].Y);
            }
        }




        //only works for X axis right now -- now for any axis?
        //maybe make this one generic for any axis
        //then have another method that implements it for each axis?
        //...excellent.
        public Boolean axisIntersects(BoundingShape b, Vector2 axis)
        {
            axis = Vector2.Normalize(axis);
            int minDot1 = 0;
            float minProj1 = Vector2.Dot(axis, vertices[0]);
            int maxDot1 = 0;
            float maxProj1 = Vector2.Dot(axis, vertices[0]);

            int i = 0;
            for (i = 1; i < vertices.Length; i++)
            {
                float currProj = Vector2.Dot(axis, vertices[i]);

                if (minProj1 > currProj)
                {
                    minProj1 = currProj;
                    minDot1 = i;
                }
                if (currProj > maxProj1)
                {
                    maxProj1 = currProj;
                    maxDot1 = i;
                }
            }

            int minDot2 = 0;
            float minProj2 = Vector2.Dot(axis, b.vertices[0]);
            int maxDot2 = 0;
            float maxProj2 = Vector2.Dot(axis, b.vertices[0]);

            int j = 0;
            for (j = 1; j < b.vertices.Length; j++)
            {
                float currProj = Vector2.Dot(axis, b.vertices[j]);

                if (minProj2 > currProj)
                {
                    minProj2 = currProj;
                    minDot2 = j;
                }
                if (currProj > maxProj2)
                {
                    maxProj2 = currProj;
                    maxDot2 = j;
                }
            }

            Boolean collided = maxProj2 > minProj1 && maxProj1 > minProj2;

            if(collided) {
                float min = maxProj1 - minProj2 < maxProj2 - minProj1 ? maxProj1 - minProj2 : maxProj2 - minProj1;
                if (min < CollisionHandler.shortestCollision)
                {
                    CollisionHandler.shortestCollision = min;
                    CollisionHandler.axisCollision = axis;
                }
            }

            //Console.WriteLine(maxProj2 + " > " + minProj1 + " && " + maxProj1 + " > " + minProj2);
            //Console.WriteLine(minDot1 + " " + maxDot1 + " " + minDot2 + " " + maxDot2);
            //Console.WriteLine(i + " " + j);

            //Console.WriteLine(maxProj2 > minProj1 && maxProj1 > minProj2);

            return collided;
        }
    }
}