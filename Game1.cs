using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
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
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
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

        public TmxMap _map;
        public Texture2D _tileSet;
        public int _tileWidth;
        public int _tileHeight;
        public int tileSetTilesSize;
        
        public HUD _hud;
        public Texture2D inventorySlot;

        public Dictionary<int, Item> allItems;
        

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
            _font = Content.Load<SpriteFont>("Fonts/Minecraft");
            _map = new TmxMap("Content/map1.tmx");
            _tileSet = Content.Load<Texture2D>(_map.Tilesets[0].Name.ToString());
            _tileWidth = _map.Tilesets[0].TileWidth;
            _tileHeight = _map.Tilesets[0].TileHeight;
            tileSetTilesSize = _tileSet.Width / _tileWidth;
            
            _player = new Player(Content.Load<Texture2D>("Hero"));
            _entities = new List<Entity>()
            {
                new Enemy(Content.Load<Texture2D>("Enemy"))
            };
            
            _hud = new HUD(_player);
            inventorySlot = Content.Load<Texture2D>("inventorySlot");
            
            
            
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
            if(Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            _mouseState = Mouse.GetState(); // gives _mouseState state each frame
            if (_mouseState.LeftButton == ButtonState.Released) isLeftMouseButtonPressed = false;
            
            _entities.ForEach(entity => entity.Update(gameTime, _player));
            _hud.Update(gameTime);
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
             for (var i = 0; i < _map.Layers[0].Tiles.Count ; i++)
             {
                 int gid = _map.Layers[0].Tiles[i].Gid;
                 if (!(gid == 0))
                 {
                     float x = (i % _map.Width) * _map.TileWidth;
                     float y = (float)Math.Floor(i / (double)_map.Width) * _map.TileHeight;
                     if (Math.Abs(_player._hitBox.X - x) < 1200 && Math.Abs(_player._hitBox.Y - y) < 1200)
                     {
                         int tileFrame = gid - 1;
                         int column = tileFrame % tileSetTilesSize;
                         int row = (int)Math.Floor((double)tileFrame / (double)tileSetTilesSize);
            
                         _spriteBatch.Draw(_tileSet,
                             new Rectangle((int)x, (int)y, _tileWidth, _tileHeight),
                             new Rectangle(_tileWidth * column, _tileHeight * row , _tileWidth, _tileHeight),
                             Color.White);
                     }
                 }
            }
            _entities.ForEach(entity => entity.Draw(_spriteBatch));
            _player.Draw(_spriteBatch);
            
            _spriteBatch.End();
            
            _spriteBatch.Begin(SpriteSortMode.Deferred, null,SamplerState.PointClamp);
            _hud.Draw(_spriteBatch,_font, inventorySlot);
            if (_player._isDead) _restartMenu.Draw();
            _mainMenu.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        public void LoadItems()
        {
            allItems = new Dictionary<int, Item>();

            var sword = new Item(false, 1, Content.Load<Texture2D>(""));
            
        }
    }
}