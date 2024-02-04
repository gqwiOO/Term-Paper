using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Button = Menu.Button;
using Movement;
using Newtonsoft.Json;
namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        public static int _screenWidth;
        public static int _screenHeight;
        
        private SpriteFont _font;

        public MainMenu _mainMenu;
        public Menu.Menu _menu;
        public Menu.Menu _settingsMenu;
        public Menu.Menu _resolutionMenu;
        public Menu.Menu _restartMenu;
        public Menu.Menu _pauseMenu;
        
        public Enemy _enemy;
        public Camera _camera;
        public Fps _fps;
        public HUD _hud;
        public Texture2D inventorySlot;
        public Texture2D currentInventorySlot;
        public Texture2D _helmetFrame;
        public Texture2D _chestPlateFrame;
        public Texture2D _bootsFrame;
        public Texture2D _arrowFrame;
        
        public Map map;
        
        public static Dictionary<int, Item> allItems;
        public Song sweden;

        public List<Entity> _entities;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            Globals.Content = Content;
            Globals.project_path =  Directory.GetParent(
                Directory.GetParent(
                    Directory.GetParent(
                        Environment.CurrentDirectory).ToString()).ToString()).ToString();
            
            Globals.gameState = State.MainMenu;
            
            _camera = new Camera();
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // FPS
            _fps = new Fps();
            
            // Screen properties
            _screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;  
            _screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.HardwareModeSwitch = false;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            
            base.Initialize();
        }
        protected override void LoadContent()
        {
            LoadItems();
            LoadHUD();
            _font = Content.Load<SpriteFont>("Fonts/Minecraft");
            
            Globals.player = new Player(Content.Load<Texture2D>("Player/DOWN_WALK"),
                Content.Load<Texture2D>("Player/UP_WALK"),
                Content.Load<Texture2D>("Player/LEFT_WALK"),
                Content.Load<Texture2D>("Player/RIGHT_WALK"),
                Content.Load<Texture2D>("Player/IDLE"));

            _entities = new List<Entity>()
            {
                new Enemy(Content.Load<Texture2D>("Enemy/Axolot/AxolotWalk"))
            };
            
            inventorySlot = Content.Load<Texture2D>("HUD/inventorySlot");
            currentInventorySlot = Content.Load<Texture2D>("HUD/currentInventorySlot");

            _hud = new HUD()
            {
                _fullHp = Content.Load<Texture2D>("HUD/HeartBar"),
                _halfHp = Content.Load<Texture2D>("HUD/HeartBarDamaged"),
                _emptyHp = Content.Load<Texture2D>("HUD/HeartBarEmpty"),
                _fullStam = Content.Load<Texture2D>("HUD/staminaBar"),
                _emptyStam = Content.Load<Texture2D>("HUD/staminaBarUsed")
            };
            
            _helmetFrame = Content.Load<Texture2D>("HUD/HelmetFrame");
            _chestPlateFrame = Content.Load<Texture2D>("HUD/ChestPlateFrame");
            _bootsFrame = Content.Load<Texture2D>("HUD/BootsFrame");
            _arrowFrame = Content.Load<Texture2D>("HUD/ArrowFrame");
            
            sweden = Content.Load<Song>("Sound/Sweden");
                
            MediaPlayer.Volume = 0.1f;
            SoundEffect.MasterVolume = 0.5f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(sweden);

            TmxMap mapObject = new TmxMap("Content/NewMap.tmx");
            map = new Map(mapObject, Content.Load<Texture2D>("Map/" + mapObject.Tilesets[0].Name));
            
            LoadMenu();
        }

        private void LoadMenu()
        {
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
                        _graphics.HardwareModeSwitch = true;
                        _graphics.PreferredBackBufferWidth = _screenWidth;
                        _graphics.PreferredBackBufferHeight = _screenHeight;
                        _graphics.IsFullScreen = true;
                        _graphics.ApplyChanges();
                        LoadContent();
                        _graphics.HardwareModeSwitch = false;
                        _graphics.ApplyChanges();


                    }
                },
                new Button(_font, "1600x900", new Vector2(_screenWidth / 2,_screenHeight / 2 - 200))
                {
                    _onClick = () =>
                    {
                        _screenWidth = 1600;
                        _screenHeight = 900;
                        _graphics.HardwareModeSwitch = true;
                        _graphics.PreferredBackBufferWidth = _screenWidth;
                        _graphics.PreferredBackBufferHeight = _screenHeight;
                        LoadMenu();

                        _graphics.IsFullScreen = true;
                        _graphics.ApplyChanges();

                    },
                },
                new Button(_font, "1024x768", new Vector2(_screenWidth / 2,_screenHeight / 2 - 100))
                {
                    _onClick = () =>
                    {
                        _screenWidth = 1024;
                        _screenHeight = 768;

                        _graphics.HardwareModeSwitch = true;
                        _graphics.PreferredBackBufferWidth = _screenWidth;
                        _graphics.PreferredBackBufferHeight = _screenHeight;
                        LoadMenu();

                        _graphics.IsFullScreen = true;
                        _graphics.ApplyChanges();
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
                        Globals.player.isDead = false;
                    }
                }
            },State.Playing);
            
            _pauseMenu = new Menu.Menu(new List<Button>
            {
                new Button(_font, "Back", new Vector2(_screenWidth / 2,_screenHeight / 2 - 300))
                {
                    _onClick = () =>
                    {
                        Globals.gameState = State.Playing;
                    }
                },
                new Button(_font, "Save", new Vector2(_screenWidth / 2,_screenHeight / 2 - 200))
                {
                    _onClick = () => 
                    {
                        // TODO: Save player and world info 
                    }
                },
                new Button(_font, "Main menu", new Vector2(_screenWidth / 2, _screenHeight / 2 - 100))
                {
                    _onClick = () =>
                    {
                        Globals.gameState = State.MainMenu;
                    }
                }
            },State.Pause);

            _mainMenu = new MainMenu
            {
                _menus = new List<Menu.Menu>
                { 
                    _menu,
                    _settingsMenu,
                    _resolutionMenu,
                    _pauseMenu
                },
            };
        }
        private void LoadHUD()
        {
            inventorySlot = Content.Load<Texture2D>("HUD/inventorySlot");
            currentInventorySlot = Content.Load<Texture2D>("HUD/currentInventorySlot");
            _hud = new HUD()
            {
                _fullHp = Content.Load<Texture2D>("HUD/HeartBar"),
                _halfHp = Content.Load<Texture2D>("HUD/HeartBarDamaged"),
                _emptyHp = Content.Load<Texture2D>("HUD/HeartBarEmpty"),
                
                _fullStam = Content.Load<Texture2D>("HUD/staminaBar"),
                _emptyStam = Content.Load<Texture2D>("HUD/staminaBarUsed"),
            };
            
        }

        protected override void Update(GameTime gameTime)
        {
            
            Globals.gameTime = gameTime;
            Input.GetKeyboardState();
            Input.GetMouseState();
            
            // Pause or Exit button
            if(Input.hasBeenPressed(Keys.Escape))
            {
                if (Globals.gameState == State.Pause) Globals.gameState = State.Playing;
                else if(Globals.gameState == State.Playing || Globals.gameState == State.Inventory)Globals.gameState = State.Pause;
                else Exit();
            }
            // Gives _mouseState state each frame
            Globals.mouseState = Mouse.GetState(); 
            
            _entities.ForEach(entity => entity.Update());
            _hud.Update();
            Globals.player.Update();
            if (Globals.player.isDead)
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
            _hud.Draw(_font, inventorySlot, currentInventorySlot, _helmetFrame, _chestPlateFrame, _bootsFrame, _arrowFrame);
            if (Globals.player.isDead) _restartMenu.Draw();
            
            _mainMenu.Draw();
            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
        
        public void LoadItems()
        {
            using StreamReader reader = new StreamReader(Path.Combine(Globals.project_path + "/data/items.json"));
            var json = reader.ReadToEnd();
            Data.Items.Weapons = JsonConvert.DeserializeObject<List<Weapon>>(json);

        }
    }
}