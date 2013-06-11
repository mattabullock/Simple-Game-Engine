using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demo.Interfaces
{
    public interface Level
    {
        CollidableObject[] levelObjects { get; set; }
        int drawIndex { get; set; }
        void LoadContent(ContentManager theContentManager);
        void Update(GameTime theGameTime, Vector2 offset);
        void Draw(SpriteBatch theSpriteBatch);
    }
}