using Game1.Class.Entity;
using Game1.Class.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Movement;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Game1.Class.Camera;

namespace Game1;

public static class Globals
{
    public static Player player;
    public static SpriteBatch spriteBatch;
    
    public static GameTime gameTime;
    
    public static State gameState;
    public static MouseState mouseState;

    public static ContentManager Content;
    
    public static Camera _camera;
    public static string project_path;
}