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
    public interface Screen
    {
        void Initialize();
        void LoadContent(GraphicsDevice graphics);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void UnloadContent();
    }
}
