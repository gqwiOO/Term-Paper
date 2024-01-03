using System;
using System.Collections.Generic;
using Game1.Class.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Button = Menu.Button;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;

        public int _screenWidth;
        public int _screenHeight;
        public State _state;
        public static MouseState _mouseState;
        private SpriteFont _font;

        public Menu.Menu _mainMenu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _state = State.Menu;
            _screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;

            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("mainFont");

            // Creating Main Menu 
            _mainMenu = new Menu.Menu(new List<Button>
            {
                // Buttons
                new Button(_font, "Quit", new Vector2(_screenWidth / 2,_screenHeight / 2 + 100))
                {
                    _onClick = () => { Exit(); },
                },
                new Button(_font, "Print in Console", new Vector2(_screenWidth / 2,_screenHeight / 2))
                {
                    _onClick = () => { Console.WriteLine("Pressed"); },
                }
            });
        }

        protected override void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState(); // gives _mouseState state each frame
            _mainMenu.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            if (_state == State.Menu)
            {
                _mainMenu.Draw();
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}