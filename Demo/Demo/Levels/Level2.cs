using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.Interfaces;
using Demo.Objects;
using Demo.Comparer;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demo
{
    public class Level2 : Level
    {
        public CollidableObject[] levelObjects { get; set; }
        public int drawIndex { get; set; }

        public Level2()
        {
            Random r = new Random();
            levelObjects = new CollidableObject[10];
            for (int i = 0; i < levelObjects.Length; i++)
            {
                int x = r.Next(100, 1920);
                int y = r.Next(100, 1080);
                levelObjects[i] = new Block(x, y);
            }
        }

        public void LoadContent(ContentManager theContentManager)
        {
            for (int i = 0; i < levelObjects.Length; i++)
            {
                levelObjects[i].LoadContent(theContentManager);
            }
        }

        public void Update(GameTime theGameTime, Vector2 offset)
        {
            Block.count = 0;
            foreach (CollidableObject c in levelObjects)
            {
                c.Update(theGameTime, offset);
            }
            Array.Sort(levelObjects, new ShapeComparer());
            drawIndex = Array.FindIndex(levelObjects, item => !item.draw);
            if (drawIndex == -1)
            {
                if (levelObjects[0].draw)
                {
                    drawIndex = levelObjects.Length;
                }
                else
                {
                    drawIndex = 1;
                }
            }
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            for (int i = 0; i < levelObjects.Length; i++)
            {
                levelObjects[i].Draw(theSpriteBatch);
            }
        }

    }
}
