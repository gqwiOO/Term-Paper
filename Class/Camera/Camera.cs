using Game1.Class.Entity;
using Microsoft.Xna.Framework;

namespace Game1.Class.Camera;

public class Camera
{
    public Matrix Transform { get; private set; }
    public Matrix position;

    public void Follow(Player _player)
    {
        position = Matrix.CreateTranslation(
            -_player._hitBox.X - (_player._hitBox.Width / 2),
            -_player._hitBox.Y - (_player._hitBox.Height / 2),
            0);

        var offset = Matrix.CreateTranslation(
            Game1._screenWidth / 2,
            Game1._screenHeight / 2,
            0);

        Transform = position * offset;
    
    }
}