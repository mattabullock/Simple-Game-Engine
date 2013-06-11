using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.Interfaces;
using Demo.Objects;
using Demo.Collisions;
using Demo.Handlers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demo.Screens
{
    class InGameScreen : Screen
    {

        SpriteBatch spriteBatch;
        SpriteFont testText;
        Camera camera;
        Vector2 center;
        Square s;
        ContentManager Content;
        GraphicsDeviceManager graphics;
        KeyboardState oldState;

        public InGameScreen(ContentManager Content, GraphicsDeviceManager graphics) 
        {
            this.Content = Content;
            this.graphics = graphics;

        }

        public void Initialize()
        {
            Program.g.IsMouseVisible = false;
            center = new Vector2(graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight / 2);
            s = new Square(graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight / 2);
            Program.g.level = new Level3();
            camera = new Camera(center, s, Program.g.viewport);
            oldState = Keyboard.GetState();
        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            testText = Content.Load<SpriteFont>("text");

            s.LoadContent(this.Content);
            Program.g.level.LoadContent(this.Content);
        }

        public void UnloadContent()
        {
            Content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            //something like this can make a pause menu
            //if (s.position.X < 0)
            //{
            //    Program.g.gState = Game.GameState.MainMenu;
            //}
            s.Update(gameTime, -camera.cPosition);
            Program.g.level.Update(gameTime, -camera.cPosition);
            camera.Update(gameTime);
            //base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            Program.g.level.Draw(spriteBatch);
            s.Draw(spriteBatch);

            Vector2 pos = new Vector2(0, 0);
            Vector2 pos2 = new Vector2(0, 20);
            Boolean collide = false;
            foreach (Boolean b in s.check)
            {
                if (b) { collide = true; break; }
            }
            if (Program.g.debugMode)
            {
                //spriteBatch.DrawString(testText, "FPS: " + 1 / gameTime.ElapsedGameTime.TotalSeconds, pos, Color.Black);
                spriteBatch.DrawString(testText, "Speed: " + s.mh.mSpeed.X + " " + s.mh.mSpeed.Y, pos, Color.Black);
                spriteBatch.DrawString(testText, "cPosition: " + camera.cPosition.X + " " + camera.cPosition.Y, pos2, Color.Black);
                spriteBatch.DrawString(testText, "Square: " + s.position.X + " " + s.position.Y, pos2 * 2, Color.Black);
                spriteBatch.DrawString(testText, "Block Count: " + Block.count, pos2 * 3 , Color.Black);
                spriteBatch.DrawString(testText, "Draw Index: " + Program.g.level.drawIndex, pos2 * 4, Color.Black);
                spriteBatch.DrawString(testText, "Bool: " + (Block.count == Program.g.level.drawIndex), pos2 * 5, Color.Black);
                spriteBatch.DrawString(testText, "Collide?: " + collide, pos2 * 6, Color.Black);
                //spriteBatch.DrawString(testText, "Block position: " + Block.position, pos2 * 7, Color.Black);

            }
            spriteBatch.End();
        }
    }
}
