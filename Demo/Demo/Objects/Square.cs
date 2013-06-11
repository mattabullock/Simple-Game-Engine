using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.Interfaces;
using Demo.Handlers;
using Demo.Collisions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demo
{
    class Square : Tracker
    {
        public BoundingShape b { get; set; }
        public Vector2 position { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        Boolean draw { get; set; }
        public Boolean[] check { get; set; }

        private Texture2D mSpriteTexture;
        private SpriteFont testText;
        public string AssetName = "square";

        public MovementHandler mh { get; private set; }
        //CollisionHandler ch { get; set; }
        Vector2 offset;

        public Rectangle Size;

        public Square(int x, int y)
        {
            position = new Vector2(x, y);
            mh = new MovementHandler(this);
            //ch = new CollisionHandler(this);
        }

        public void LoadContent(ContentManager theContentManager)
        {
            //position = new Vector2(START_POSITION_X, START_POSITION_Y);
            mSpriteTexture = theContentManager.Load<Texture2D>(AssetName);
            b = new BoundingShape(new Vector2[] {position, new Vector2(position.X, position.Y + mSpriteTexture.Height), 
                new Vector2(position.X + mSpriteTexture.Width, position.Y), new Vector2(position.X + mSpriteTexture.Width, position.Y + mSpriteTexture.Height)});
            testText = theContentManager.Load<SpriteFont>("text");
            Size = new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height);
        }

        public void Update(GameTime theGameTime, Vector2 offset)
        {
            this.offset = offset;
            mh.Update(theGameTime, offset);
            b = new BoundingShape(new Vector2[] {position, new Vector2(position.X, position.Y + mSpriteTexture.Height), 
                new Vector2(position.X + mSpriteTexture.Width, position.Y), new Vector2(position.X + mSpriteTexture.Width, position.Y + mSpriteTexture.Height)});
            check = CollisionHandler.checkCollisions(this);

        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, position + offset, Color.White);
        }
    }
}