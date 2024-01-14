using System;
using System.Collections.Generic;
using Game1.Class;
using Game1.Class.Camera;
using Game1.Class.Entity;
using Game1.Class.Item;
using Game1.Class.State;
using Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledSharp;
using Game1.Level;
using Button = Menu.Button;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        public static int _screenWidth;
        public static int _screenHeight;
        public static bool isLeftMouseButtonPressed = false;
        
        private SpriteFont _font;

        public Menu.MainMenu _mainMenu;
        public Menu.Menu _menu;
        public Menu.Menu _settingsMenu;
        public Menu.Menu _resolutionMenu;
        public Menu.Menu _restartMenu;
        
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
            Globals.gameState = State.MainMenu;
            _camera = new Camera();
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            
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

            
            Globals.player = new Player(Content.Load<Texture2D>("Player/DOWN_WALK"),
                                 Content.Load<Texture2D>("Player/UP_WALK"),
                                 Content.Load<Texture2D>("Player/LEFT_WALK"),
                                 Content.Load<Texture2D>("Player/RIGHT_WALK"));
            _entities = new List<Entity>()
            {
                new Enemy(Content.Load<Texture2D>("Enemy/Axolot/AxolotWalk"))
            };
            
            inventorySlot = Content.Load<Texture2D>("inventorySlot");
            currentInventorySlot = Content.Load<Texture2D>("HUD/currentInventorySlot");
            _hud = new HUD()
            {
                _fullHp = Content.Load<Texture2D>("HUD/HeartBar"),
                _halfHp = Content.Load<Texture2D>("HUD/HeartBarDamaged"),
                _emptyHp = Content.Load<Texture2D>("HUD/HeartBarEmpty"),
            };
                

            TmxMap mapObject = new TmxMap("Content/NewMap.tmx");
            map = new Map(mapObject, Content.Load<Texture2D>(mapObject.Tilesets[0].Name));
            
            // Creating Main Menu 
            _menu = new Menu.Menu(new List<Button>
            {
                new Button(_font, "Start", new Vector2(_screenWidth / 2, _screenHeight / 2 - 200))
                {
                    _onClick = () =>
                    {
                        Globals.gameState = State.Playing;
                        Globals.player.Revive();
                    }
                },
                new Button(_font, "Settings", new Vector2(_screenWidth / 2, _screenHeight / 2 - 100))
                {
                    _onClick = () => { Globals.gameState = State.Settings; }
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
                    _onClick = () => { Globals.gameState = State.Resolution; },
                },
                new Button(_font, "Sound", new Vector2(_screenWidth / 2,_screenHeight / 2 - 100))
                {
                    _onClick = () => {  },
                },
                new Button(_font, "Back", new Vector2(_screenWidth / 2,_screenHeight / 2))
                {
                    _onClick = () => { Globals.gameState = State.MainMenu; },
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
                    _onClick = () => { Globals.gameState = State.Settings; },
                }
            }, State.Resolution);
            
            _restartMenu = new Menu.Menu(new List<Button>
            {
                new Button(_font, "Restart", new Vector2(_screenWidth / 2,_screenHeight / 2 - 300))
                {
                    _onClick = () =>
                    {
                        Globals.gameState = State.Playing;
                        Globals.player.Revive();
                    }
                },
                new Button(_font, "Main menu", new Vector2(_screenWidth / 2,_screenHeight / 2 - 200))
                {
                    _onClick = () => 
                    {
                        Globals.gameState = State.MainMenu;
                        Globals.player._isDead = false;
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
            Globals.gameTime = gameTime;
            if(Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            Globals.mouseState = Mouse.GetState(); // gives _mouseState state each frame
            if (Globals.mouseState.LeftButton == ButtonState.Released) isLeftMouseButtonPressed = false;
            
            _entities.ForEach(entity => entity.Update());
            _hud.Update();
            Globals.player.Update();
            if (Globals.player._isDead)
            {
                _restartMenu.Update();
            }
            _mainMenu.Update();
            _camera.Follow(Globals.player);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, null,SamplerState.PointClamp,transformMatrix: _camera.Transform);
            map.Draw();
            _entities.ForEach(entity => entity.Draw());
            Globals.player.Draw();
            
            Globals.spriteBatch.End();
            
            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, null,SamplerState.PointClamp);
            _hud.Draw(_font, inventorySlot, currentInventorySlot);
            if (Globals.player._isDead) _restartMenu.Draw();
            _mainMenu.Draw();
            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }


        public void LoadItems()
        {
            allItems = new Dictionary<int, Item>();
            allItems[0] = new Weapon("Start Sword", Content.Load<Texture2D>("Items/Sword"),  false, 1);
            allItems[1] = new Weapon("Redeemer", Content.Load<Texture2D>("Items/Redeemer"),  false, 1);
            allItems[3] = new Potion("Healing Potion", Content.Load<Texture2D>("Items/HealingPotion"),  false, 1);
        }
    }
}