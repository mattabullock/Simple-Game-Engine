using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Demo.Interfaces;


namespace Demo.Handlers
{
    class MovementHandler
    {

        Tracker t;

        const int START_POSITION_X = 960;
        const int START_POSITION_Y = 540;
        const int SPEED = 500;
        const int ACCEL = 50;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;

        enum State
        {
            Walking
        }
        State mCurrentState = State.Walking;

        public Vector2 mDirection = Vector2.Zero;
        public Vector2 mSpeed = Vector2.Zero;
        public Vector2 mVector { get; set; }

        KeyboardState mPreviousKeyboardState;

        Rectangle box;

        public MovementHandler(Tracker t) 
        {
            this.t = t;
            mVector = Vector2.Zero;
            box = new Rectangle((int) t.position.X, (int) t.position.Y, t.width, t.height);
        }

        public void Update(GameTime theGameTime, Vector2 offset)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            UpdateMovement(aCurrentKeyboardState);

            mPreviousKeyboardState = aCurrentKeyboardState;

            mVector = mDirection * mSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;

            //collisionhandler rescale goes here

            t.position += mVector;
            box.X = (int) t.position.X;
            box.Y = (int) t.position.Y;
        }

        public void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Walking)
            {

                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
                {
                    mSpeed.X += ACCEL;
                    mDirection.X = MOVE_LEFT;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
                {
                    mSpeed.X += ACCEL;
                    mDirection.X = MOVE_RIGHT;
                }

                if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true)
                {
                    mSpeed.Y += ACCEL;
                    mDirection.Y = MOVE_UP;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Down) == true)
                {
                    mSpeed.Y += ACCEL;
                    mDirection.Y = MOVE_DOWN;
                }

                if (aCurrentKeyboardState.IsKeyUp(Keys.Left) == true && mDirection.X == MOVE_LEFT)
                {
                    mSpeed.X -= ACCEL;
                    //mDirection.X = MOVE_LEFT;
                }
                else if (aCurrentKeyboardState.IsKeyUp(Keys.Right) == true && mDirection.X == MOVE_RIGHT)
                {
                    mSpeed.X -= ACCEL;
                    //mDirection.X = MOVE_RIGHT;
                }

                if (aCurrentKeyboardState.IsKeyUp(Keys.Up) == true && mDirection.Y == MOVE_UP)
                {
                    mSpeed.Y -= ACCEL;
                    //mDirection.Y == MOVE_UP;
                }
                else if (aCurrentKeyboardState.IsKeyUp(Keys.Down) == true && mDirection.Y == MOVE_DOWN)
                {
                    mSpeed.Y -= ACCEL;
                    //mDirection.Y = MOVE_DOWN;
                }

                if (mSpeed.X > SPEED)
                {
                    mSpeed.X = SPEED;
                }
                if (mSpeed.Y > SPEED)
                {
                    mSpeed.Y = SPEED;
                }
                if (mSpeed.X < 0)
                {
                    mSpeed.X = 0;
                }
                if (mSpeed.Y < 0)
                {
                    mSpeed.Y = 0;
                }
            }
        }
    }
}
