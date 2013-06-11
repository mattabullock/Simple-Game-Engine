using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.Interfaces;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demo.Screens
{
    class MainMenu : Screen
    {

        Vector2 startButtonPosition, exitButtonPosition;
        Vector2 mousePos;
        SpriteBatch spriteBatch;
        SpriteFont testText;
        Texture2D startButton, exitButton;
        ContentManager Content;
        GraphicsDeviceManager graphics;
        MouseState mouseState, previousMouseState;

        public MainMenu(ContentManager Content, GraphicsDeviceManager graphics) 
        {
            this.Content = Content;
            this.graphics = graphics;

        }
        public void Initialize()
        {
            
            Program.g.IsMouseVisible = true;
            mouseState = Mouse.GetState();
            previousMouseState = mouseState;
            startButtonPosition = new Vector2((graphics.PreferredBackBufferWidth / 2) - 50, 200);
            exitButtonPosition = new Vector2((graphics.PreferredBackBufferWidth / 2) - 50, 250);
        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startButton = Content.Load<Texture2D>("start");
            exitButton = Content.Load<Texture2D>("exit");
            testText = Content.Load<SpriteFont>("text");
            //pointer = Content.Load<Texture2D>("pointer");
        }

        public void UnloadContent()
        {
            Content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            //wait for mouseclick
            mouseState = Mouse.GetState();
            if (previousMouseState.LeftButton == ButtonState.Pressed && 
                mouseState.LeftButton == ButtonState.Released)
            {
                MouseClicked(mouseState.X, mouseState.Y);
            }
    
            previousMouseState = mouseState;
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(startButton, startButtonPosition, Color.White);
            spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
            //spriteBatch.Draw(pointer, mousePos, Color.White);
            if (Program.g.debugMode)
            {
                spriteBatch.DrawString(testText, "Exit Button Position: " + exitButtonPosition.X + " " + exitButtonPosition.Y, new Vector2(0, 20), Color.Black);
                spriteBatch.DrawString(testText, "Mouse Position: " + Mouse.GetState().X + " " + Mouse.GetState().Y, new Vector2(0, 0), Color.Black);
            }
            spriteBatch.End();
        }

        void MouseClicked(int x, int y)
        {
            //creates a rectangle of 10x10 around the place where the mouse was clicked
            Rectangle mouseClickRect = new Rectangle(x, y, 5, 5);
        
            ////check the startmenu
            //if (gameState == GameState.StartMenu)
            //{
            Rectangle startButtonRect = new Rectangle((int)startButtonPosition.X,
                                        (int)startButtonPosition.Y, 100, 20);
            Rectangle exitButtonRect = new Rectangle((int)exitButtonPosition.X,
                                        (int)exitButtonPosition.Y, 100, 20);

            if (mouseClickRect.Intersects(startButtonRect)) //player clicked start button
            {
                //gameState = GameState.Loading;
                Program.g.gState = Game.GameState.InGame;
                
            }
            else if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
            {
                Program.g.Exit();
            }
            //}
        }
    }
}
