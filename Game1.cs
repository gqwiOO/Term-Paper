﻿using System;
using System.Collections.Generic;
using Game1.Class;
using Game1.Class.Camera;
using Game1.Class.Entity;
using Game1.Class.Item;
using Game1.Class.State;
using Game1.Level;
using Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledSharp;
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
        public static bool isLeftMouseButtonPressed = false;
        
        private SpriteFont _font;

        public Menu.MainMenu _mainMenu;
        public Menu.Menu _menu;
        public Menu.Menu _settingsMenu;
        public Menu.Menu _resolutionMenu;
        public Menu.Menu _restartMenu;
        
        public Player _player;
        public Enemy _enemy;
        public Camera _camera;
        public Fps _fps;
        public HUD _hud;
        public Texture2D inventorySlot;
        public Texture2D currentInventorySlot;

        public Map map;
        
        
        public static Dictionary<int, Item> allItems;
        

        public List<Entity> _entities;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            _state = State.MainMenu;
            _camera = new Camera();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // FPS
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);
            _fps = new Fps();
            
            _screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            
            base.Initialize();
        }
        protected override void LoadContent()
        {
            LoadItems();
            _font = Content.Load<SpriteFont>("Fonts/Minecraft");

            
            _player = new Player(Content.Load<Texture2D>("Hero"));
            _entities = new List<Entity>()
            {
                new Enemy(Content.Load<Texture2D>("Enemy"))
            };
            
            inventorySlot = Content.Load<Texture2D>("inventorySlot");
            currentInventorySlot = Content.Load<Texture2D>("HUD/currentInventorySlot");
            _hud = new HUD(_player);

            map = new Map(new TmxMap("Content/map1.tmx"), Content.Load<Texture2D>("TX Tileset Grass"), _player);
            
            // Creating Main Menu 
            _menu = new Menu.Menu(new List<Button>
            {
                new Button(_font, "Start", new Vector2(_screenWidth / 2, _screenHeight / 2 - 200))
                {
                    _onClick = () =>
                    {
                        _state = State.Playing;
                        _player.Revive();
                    }
                },
                new Button(_font, "Settings", new Vector2(_screenWidth / 2, _screenHeight / 2 - 100))
                {
                    _onClick = () => { _state = State.Settings; }
                },
                new Button(_font, "Quit", new Vector2(_screenWidth / 2, _screenHeight / 2))
                {
                    _onClick = () => { Exit(); }
                },
            }, State.MainMenu);  
            
            // Creating Settings Menu
            _settingsMenu = new Menu.Menu(new List<Button>
            {
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
                }
            }, State.Settings);
            
            // Creating Resolution Menu
            _resolutionMenu = new Menu.Menu(new List<Button>
            {
                new Button(_font, "1920x1080", new Vector2(_screenWidth / 2,_screenHeight / 2 - 300))
                {
                    _onClick = () =>
                    {
                        _screenWidth = 1920;
                        _screenHeight = 1080;

                        _graphics.PreferredBackBufferWidth = _screenWidth;
                        _graphics.PreferredBackBufferHeight = _screenHeight;
                        _graphics.IsFullScreen = true;
                        _graphics.ApplyChanges();
                        LoadContent();
                    }
                },
                new Button(_font, "1600x900", new Vector2(_screenWidth / 2,_screenHeight / 2 - 200))
                {
                    _onClick = () =>
                    {
                        _screenWidth = 1600;
                        _screenHeight = 900;

                        _graphics.PreferredBackBufferWidth = _screenWidth;
                        _graphics.PreferredBackBufferHeight = _screenHeight;
                        _graphics.IsFullScreen = true;
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
                        _graphics.IsFullScreen = true;
                        _graphics.ApplyChanges();
                        LoadContent();
                    },
                },
                new Button(_font, "Back", new Vector2(_screenWidth / 2,_screenHeight / 2))
                {
                    _onClick = () => { _state = State.Settings; },
                }
            }, State.Resolution);
            
            _restartMenu = new Menu.Menu(new List<Button>
            {
                new Button(_font, "Restart", new Vector2(_screenWidth / 2,_screenHeight / 2 - 300))
                {
                    _onClick = () =>
                    {
                        _state = State.Playing;
                        _player.Revive();
                    }
                },
                new Button(_font, "Main menu", new Vector2(_screenWidth / 2,_screenHeight / 2 - 200))
                {
                    _onClick = () => 
                    {
                        _state = State.MainMenu;
                        _player._isDead = false;
                    }
                }
            },State.Playing);

            _mainMenu = new MainMenu
            {
                _menus = new List<Menu.Menu>
                { 
                  _menu,
                  _settingsMenu,
                  _resolutionMenu
                },
            };
        }
        protected override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            _mouseState = Mouse.GetState(); // gives _mouseState state each frame
            if (_mouseState.LeftButton == ButtonState.Released) isLeftMouseButtonPressed = false;
            
            _entities.ForEach(entity => entity.Update(gameTime, _player));
            _hud.Update(gameTime, _state);
            _player.Update(gameTime);
            if (_player._isDead)
            {
                _restartMenu.Update();
            }
            _mainMenu.Update();
            _camera.Follow(_player);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin(transformMatrix: _camera.Transform);
            map.Draw(_spriteBatch);
            _entities.ForEach(entity => entity.Draw(_spriteBatch));
            _player.Draw(_spriteBatch);
            
            _spriteBatch.End();
            
            _spriteBatch.Begin(SpriteSortMode.Deferred, null,SamplerState.PointClamp);
            _hud.Draw(_spriteBatch,_font, inventorySlot, currentInventorySlot);
            if (_player._isDead) _restartMenu.Draw();
            _mainMenu.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public void LoadItems()
        {
            allItems = new Dictionary<int, Item>();
            allItems[0] = new Item("Start Sword", Content.Load<Texture2D>("Items/Sword"),  false, 1);
            
        }
    }
}