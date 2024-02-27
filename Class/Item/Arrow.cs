using System;
using System.Linq;
using Data;
using Game1.Class.Entity;
using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Class.Item;

public class Arrow
{
    private RectangleF _hitbox;
    private int _speed = 22;
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
        if (Globals.gameState == State.State.Playing || Globals.gameState == State.State.Inventory)
        {
            Globals.spriteBatch.Draw(_sprite, _hitbox.ToRectangle(), Color.White);
        }
    }

    public bool IntersectsEnemy()
    {
        foreach (var enemy in Entities.entities.Where(npc => npc.GetType().Equals(typeof(Enemy))).ToList())
        {
            if (_hitbox.Intersects(enemy._hitBox) && ((Enemy)enemy).canBeDamaged)
            {
                ((Enemy)enemy).TakeDamage(20);
                return true;
            }
        }
        return false;
    }
}