using Game1.Class;
using Game1.Class.Entity;
using Game1.Class.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu;

public class HUD
{
    public Player _player;
    private Fps _fps;
    
    public HUD(Player player)
    {
        _player = player;
        _fps = new Fps();
    }

    public void Update(GameTime gameTime)
    {
        _fps.Update(gameTime);
    }

    public void Draw(SpriteBatch _spriteBatch,SpriteFont _font)
    {
        if (Game1.Game1._state == State.Playing)
        {
            _spriteBatch.DrawString(_font, $"HP: {_player._hp}", new Vector2(Game1.Game1._screenWidth - 200, 20), Color.Red);
            _spriteBatch.DrawString(_font, $"Pos: {_player._hitBox.X}  {_player._hitBox.Y}", new Vector2(10,100), Color.Black);
            _fps.DrawFps(_spriteBatch, _font,new Vector2(10, 10), Color.Black );
        }
    }
}