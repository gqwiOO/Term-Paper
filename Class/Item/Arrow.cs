using System;
using System.Linq;
using Data;
using Game1.Class.Entity;
using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Movement;

namespace Game1.Class.Item;

public class Arrow
{
    private RectangleF _hitbox = new RectangleF(0, 0, 100, 50); 
    readonly private int _speed = 22;
    private Texture2D _sprite = Globals.Content.Load<Texture2D>("Items/Weapon/Bow/Arrow2");
    private Vector2 _direction;
    private Vector2 _directionVector;
    private float _arrowRotation;
    
    public Arrow(Vector2 direction)
    {
        _hitbox = Globals.player._hitBox.CenterRec;
        _direction = direction;
        _directionVector = new Vector2(Input.GetAbsoluteMousePos().X - _hitbox.X, Input.GetAbsoluteMousePos().Y - _hitbox.Y);
        _arrowRotation = (float)Math.Atan2(_directionVector.Y, _directionVector.X);
    }
    
    public void Update()
    {
        MoveArrow();
    }
    
    
    private void MoveArrow()
    {
        _hitbox.X += _speed * _direction.X;
        _hitbox.Y += _speed * _direction.Y;
    }

    
    public void Draw()
    {
        if (Globals.gameState == State.State.Playing || Globals.gameState == State.State.Inventory)
        {
            Globals.spriteBatch.Draw(_sprite,_hitbox.ToRectangle(),
                null, Color.White,_arrowRotation, new Vector2(_sprite.Width / 2, _sprite.Height / 2), SpriteEffects.None, 0f);
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