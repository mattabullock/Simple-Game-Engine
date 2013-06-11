using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework;

using Demo.Interfaces;

namespace Demo
{
    class Camera
    {
        Vector2 center, tracker, cDirection, cSpeed;
        public Vector2 cPosition { get; set; }
        Rectangle deadZone, screen;
        Vector2 movement;
        Tracker t;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        const int SPEED = 600;

        public Camera(Vector2 center, Tracker t, Rectangle r)
        {
            cPosition = new Vector2(0, 0);
            deadZone = new Rectangle(r.Right * 9 / 20, r.Bottom * 9 / 20, r.Right / 10, r.Bottom / 10);
            //deadZone = new Rectangle(480,270,960,540);
            screen = r;
            this.t = t;
            tracker = t.position;
            this.center = center;
            movement = new Vector2(0,0);
        }

        public void Update(GameTime theGameTime)
        {
            tracker = t.position + -cPosition;
            check();
            cPosition += cDirection * cSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
        }

        public void check()
        {
            cDirection = Vector2.Zero;
            cSpeed = Vector2.Zero;
            if (!deadZone.Contains((int)tracker.X, (int)tracker.Y))
            {
                if (tracker.X > deadZone.Right)
                {
                    cSpeed.X = (tracker.X - deadZone.Right) * SPEED / deadZone.X;
                    cDirection.X = MOVE_RIGHT;
                }
                if (tracker.X < deadZone.Left)
                {
                    cSpeed.X = (deadZone.Left - tracker.X) * SPEED / deadZone.X;
                    cDirection.X = MOVE_LEFT;
                }
                if (tracker.Y > deadZone.Bottom)
                {
                    cSpeed.Y = (tracker.Y - deadZone.Bottom) * SPEED / deadZone.Y;
                    cDirection.Y = MOVE_DOWN;
                }
                if (tracker.Y < deadZone.Top)
                {
                    cSpeed.Y = (deadZone.Top - tracker.Y) * SPEED / deadZone.Y;
                    cDirection.Y = MOVE_UP;
                }
                if (cSpeed.X < 10)
                {
                    cSpeed.X = 10;
                }
                if (cSpeed.Y < 10)
                {
                    cSpeed.Y = 10;
                }
            }
        }
    }
}
