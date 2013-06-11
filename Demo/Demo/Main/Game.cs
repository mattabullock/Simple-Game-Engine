using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Demo.Interfaces;
using Demo.Screens;

namespace Demo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {

        public enum GameState
        {
            MainMenu,
            Settings,
            InGame
        }

        public GraphicsDeviceManager graphics {get; private set;}
        public Level level {get; set;}
        public GameState gState {get; set;}
        public GameState prevGState;
        public Screen screen;
        public Rectangle viewport { get; private set; }
        public Boolean debugMode { get; private set; }

        KeyboardState oldState;


        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            this.graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            viewport = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            oldState = Keyboard.GetState();
            gState = GameState.MainMenu;
            screen = new MainMenu(Content, graphics);
            screen.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            screen.LoadContent(GraphicsDevice);
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            //turning on debug mode
            if (Keyboard.GetState().GetPressedKeys().Contains(Keys.D) && !oldState.IsKeyDown(Keys.D))
            {
                debugMode = !debugMode;
            }
            //going back to main menu
            if (Keyboard.GetState().GetPressedKeys().Contains(Keys.M) && !oldState.IsKeyDown(Keys.M))
            {
                gState = GameState.MainMenu;
            }
            //changing stuff for button pressing
            oldState = Keyboard.GetState();
            //game screen updating
            if (prevGState != gState)
            {
                screen.UnloadContent();
                if (gState == GameState.MainMenu)
                {
                    screen = new MainMenu(Content, graphics);
                }
                else if (gState == GameState.InGame)
                {
                    screen = new InGameScreen(Content, graphics);
                }
                screen.Initialize();
                screen.LoadContent(GraphicsDevice);
            }
            prevGState = gState;

            screen.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            screen.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}