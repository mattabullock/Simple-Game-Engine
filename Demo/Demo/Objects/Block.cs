using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.Interfaces;
using Demo.Collisions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demo.Objects
{
    class Block : CollidableObject
    {

        public BoundingShape b { get; set; }

        Texture2D mSpriteTexture;
        String AssetName = "block";
        Vector2 position, offset, originalPos;
        public bool draw { get; set; }
        public static int count;

        public Block(int x, int y) {
            position = new Vector2(x, y);
            originalPos = position;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(AssetName);
            //bb = new Rectangle((int) position.X, (int) position.Y, mSpriteTexture.Width, mSpriteTexture.Height);
            b = new BoundingShape(new Vector2[] {position, new Vector2(position.X, position.Y + mSpriteTexture.Height), 
                new Vector2(position.X + mSpriteTexture.Width, position.Y + mSpriteTexture.Height), new Vector2(position.X + mSpriteTexture.Width, position.Y)});

        }

        public void Update(GameTime theGameTime, Vector2 offset)
        {
            this.offset = offset;
            b = new BoundingShape(new Vector2[] {position, new Vector2(position.X, position.Y + mSpriteTexture.Height), 
                new Vector2(position.X + mSpriteTexture.Width, position.Y + mSpriteTexture.Height), new Vector2(position.X + mSpriteTexture.Width, position.Y)});


            //checking stuff
            draw = false;
            foreach (Vector2 point in b.vertices)
            {
                //Console.WriteLine((point + offset).X + " " + (point + offset).Y);
                if (Program.g.viewport.Contains(new Point((int)(point + offset).X, (int)(point + offset).Y)))
                {
                    draw = true;
                    count++;
                    break;
                }
            }



            //this is for updating their position based on camera offset (shit)
            //position = new Vector2(x, y);
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            //if((position + offset).X + mSpriteTexture.Width < Program.g.graphics.PreferredBackBufferWidth) && position
            if (draw)
            {
                theSpriteBatch.Draw(mSpriteTexture, position + offset,
                    new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
                    Color.White);
            }
        }
    }
}