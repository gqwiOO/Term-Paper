using Game1.Class.Entity;
using Game1.Class.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Keyboard = Movement.Keyboard;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Game1;

public static class Globals
{
    public static Player player;
    public static SpriteBatch spriteBatch;
    public static GameTime gameTime;
    public static State gameState;
    public static MouseState mouseState;
    public static Keyboard keyboard;
}