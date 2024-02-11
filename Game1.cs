using System;
using System.Collections.Generic;
using System.IO;
using Data;
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
using TermPaper.Class.Font;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        public static int _screenWidth;
        public static int _screenHeight;
        
        public MainMenu _mainMenu;
        public Menu.Menu _menu;
        public Menu.Menu _settingsMenu;
        public Menu.Menu _resolutionMenu;
        public Menu.Menu _restartMenu;
        public Menu.Menu _pauseMenu;
        
        public Enemy _enemy;
        public List<Entity> _entities;
        

        public HUD _hud;
        
        public Map map;
        public Song sweden;

        
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
            
            Globals._camera = new Camera();
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            
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
            LoadEntities();
            
            Globals.player = new Player();

            _entities = new List<Entity>()
            {
                new Enemy(Content.Load<Texture2D>("Enemy/Axolot/AxolotWalk"))
            };
            
            _hud = new HUD();
            
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
                new Button(Font.fonts["MainFont-24"], "Start", new Vector2(_screenWidth / 2, _screenHeight / 2 - 200))
                {
                    _onClick = () =>
                    {
                        Globals.gameState = State.Playing;
                        Globals.player.Revive();
                    }
                },
                new Button( "Settings", new Vector2(_screenWidth / 2, _screenHeight / 2 - 100))
                {
                    _onClick = () => { Globals.gameState = State.Settings; }
                },
                new Button( "Quit", new Vector2(_screenWidth / 2, _screenHeight / 2))
                {
                    _onClick = () => { Exit(); }
                },
            }, State.MainMenu);  
            
            // Creating Settings Menu
            _settingsMenu = new Menu.Menu(new List<Button>
            {
                new Button( "Resolution", new Vector2(_screenWidth / 2,_screenHeight / 2 - 200))
                {
                    _onClick = () => { Globals.gameState = State.Resolution; },
                },
                new Button( "Sound", new Vector2(_screenWidth / 2,_screenHeight / 2 - 100))
                {
                    _onClick = () => {  },
                },
                new Button( "Back", new Vector2(_screenWidth / 2,_screenHeight / 2))
                {
                    _onClick = () => { Globals.gameState = State.MainMenu; },
                }
            }, State.Settings);
            
            // Creating Resolution Menu
            _resolutionMenu = new Menu.Menu(new List<Button>
            {
                new Button( "1920x1080", new Vector2(_screenWidth / 2,_screenHeight / 2 - 300))
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
                new Button("1600x900", new Vector2(_screenWidth / 2,_screenHeight / 2 - 200))
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
                new Button( "1024x768", new Vector2(_screenWidth / 2,_screenHeight / 2 - 100))
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
                new Button( "Back", new Vector2(_screenWidth / 2,_screenHeight / 2))
                {
                    _onClick = () => { Globals.gameState = State.Settings; },
                }
            }, State.Resolution);
            
            _restartMenu = new Menu.Menu(new List<Button>
            {
                new Button( "Restart", new Vector2(_screenWidth / 2,_screenHeight / 2 - 300))
                {
                    _onClick = () =>
                    {
                        Globals.gameState = State.Playing;
                        Globals.player.Revive();
                    }
                },
                new Button( "Main menu", new Vector2(_screenWidth / 2,_screenHeight / 2 - 200))
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
                new Button( "Back", new Vector2(_screenWidth / 2,_screenHeight / 2 - 300))
                {
                    _onClick = () =>
                    {
                        Globals.gameState = State.Playing;
                    }
                },
                new Button( "Save", new Vector2(_screenWidth / 2,_screenHeight / 2 - 200))
                {
                    _onClick = () => 
                    {
                        // TODO: Save player and world info 
                    }
                },
                new Button( "Main menu", new Vector2(_screenWidth / 2, _screenHeight / 2 - 100))
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

        protected override void Update(GameTime gameTime)
        {
            Globals.gameTime = gameTime;
            Input.GetKeyboardState();
            Input.GetMouseState();
            
            // Pause or Exit button
            if (Input.hasBeenPressed(Keys.Escape))
            {
                switch (Globals.gameState)
                {
                    case State.Pause:
                        Globals.gameState = State.Playing;
                        break;
                    case State.Playing:
                        Globals.gameState = State.Pause;
                        break;
                    case State.MainMenu:
                        Exit();
                        break;
                    case State.Inventory:
                        Globals.gameState = State.Playing;
                        break;
                    case State.InShop:
                        Globals.gameState = State.Playing;
                        break;
                }
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
            Entities.entities.ForEach(npc => npc.Update());
            _mainMenu.Update();
            
            Globals._camera.Follow(Globals.player);

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, null,SamplerState.PointClamp,transformMatrix: Globals._camera.Transform);
            map.Draw();
            _entities.ForEach(entity => entity.Draw());
            Globals.player.Draw();
            Entities.entities.ForEach(npc => npc.Draw());
            Globals.spriteBatch.End();
            
            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, null,SamplerState.PointClamp);
            _hud.Draw();
            Entities.entities.ForEach(npc => npc.DrawHUD());

            if (Globals.player.isDead) _restartMenu.Draw();
            
            _mainMenu.Draw();
            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
        public void LoadItems()
        {
            using StreamReader reader = new StreamReader(Path.Combine(Globals.project_path + "/data/sword.json"));
            var json = reader.ReadToEnd();
            Data.Items.Weapons = JsonConvert.DeserializeObject<List<Weapon>>(json);

        }

        public void LoadEntities()
        {
            using StreamReader entReader = new StreamReader(Path.Combine(Globals.project_path + "/data/NPC.json"));
            var json = entReader.ReadToEnd();
            Data.Entities.entities = JsonConvert.DeserializeObject<List<NPC>>(json);
            Entities.LoadAnimation();
        }
    }
}