using System;
using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Class.Item;

public class Arrow
{
    private RectangleF _hitbox;
    private int _speed = 5;
    private Texture2D _sprite = Globals.Content.Load<Texture2D>("Items/Weapon/Bow/Arrow2");
    private Vector2 _direction;
    
    public Arrow(Vector2 direction)
    {
        _hitbox = Globals.player._hitBox;
        _direction = direction;
    }

    
    public void Update()
    {
        _hitbox = Globals.player._hitBox;
    }

    
    public void MoveArrow()
    {
        _hitbox.X += _speed * _direction.X;
        _hitbox.Y += _speed * _direction.Y;
    }

    
    public void Draw()
    {
        Globals.spriteBatch.Draw(_sprite, _hitbox.ToRectangle(), Color.White);
    }
}