using System;
using System.Collections.Generic;
using Game1.Class.Entity;
using Game1.Class.State;
using Menu;
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

        public static int _screenWidth;
        public static int _screenHeight;
        public static State _state;
        public static MouseState _mouseState;
        private SpriteFont _font;

        public Menu.MainMenu _mainMenu;
        public Menu.Menu _menu;
        public Menu.Menu _settingsMenu;
        public Menu.Menu _resolutionMenu ;
        
        public static bool isLeftMouseButtonPressed = false;
        private static Game1 _instance;
        public Texture2D _hero  ;
        public Player _player;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }
        protected override void Initialize()
        {
            _state = State.MainMenu;
            

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
            _hero = Content.Load<Texture2D>("Hero");
            _player = new Player(_hero);
            
            // Creating Main Menu 
            _menu = new Menu.Menu(new List<Button>
            {
                // Buttons
                new Button(_font, "Start", new Vector2(_screenWidth / 2, _screenHeight / 2 - 200))
                {
                    _onClick = () => { _state = State.Playing; },
                },

                new Button(_font, "Settings", new Vector2(_screenWidth / 2, _screenHeight / 2 - 100))
                {
                    _onClick = () => { _state = State.Settings; },
                },


                new Button(_font, "Quit", new Vector2(_screenWidth / 2, _screenHeight / 2))
                {
                    _onClick = () => { Exit(); },
                },

            }, State.MainMenu);  
            
            // Creating Settings Menu
            
            _settingsMenu = new Menu.Menu(new List<Button>
            {
                // Buttons
                new Button(_font, "Resolution", new Vector2(_screenWidth / 2,_screenHeight / 2 - 200))
                {
                    _onClick = () => { _state = State.Resolution; },
                },
                
                new Button(_font, "Sound", new Vector2(_screenWidth / 2,_screenHeight / 2 - 100))
                {
                    _onClick = () => {  },
                },

                
                new Button(_font, "Back", new Vector2(_screenWidth / 2,_screenHeight / 2))
                {
                    _onClick = () => { _state = State.MainMenu; },
                },
                
            }, State.Settings);
            
            
            _resolutionMenu = new Menu.Menu(new List<Button>
            {
                // Buttons
                new Button(_font, "1600x900", new Vector2(_screenWidth / 2,_screenHeight / 2 - 200))
                {
                    _onClick = () =>
                    {
                        _screenWidth = 1600;
                        _screenHeight = 900;

                        _graphics.PreferredBackBufferWidth = _screenWidth;
                        _graphics.PreferredBackBufferHeight = _screenHeight;
                        _graphics.IsFullScreen = false;
                        _graphics.ApplyChanges();
                        LoadContent();
                    },
                },
                
                new Button(_font, "1024x768", new Vector2(_screenWidth / 2,_screenHeight / 2 - 100))
                {
                    _onClick = () =>
                    {
                        _screenWidth = 1024;
                        _screenHeight = 768;

                        _graphics.PreferredBackBufferWidth = _screenWidth;
                        _graphics.PreferredBackBufferHeight = _screenHeight;
                        _graphics.IsFullScreen = false;
                        _graphics.ApplyChanges();
                        LoadContent();

                    },
                },
                
                new Button(_font, "Back", new Vector2(_screenWidth / 2,_screenHeight / 2))
                {
                    _onClick = () => { _state = State.Settings; },
                },
            }, State.Resolution);

            _mainMenu = new MainMenu
            {
                _menus = new List<Menu.Menu>
                {
                    _menu,
                    _settingsMenu,
                    _resolutionMenu,
                }
            };
        }
        protected override void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState(); // gives _mouseState state each frame
            if (_mouseState.LeftButton == ButtonState.Released) isLeftMouseButtonPressed = false;
            _mainMenu.Update();
            _player.Update();

            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();
            _mainMenu.Draw();
            _player.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}