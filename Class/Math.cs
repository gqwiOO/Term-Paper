using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1;

namespace MathL;

public struct RectangleF
{
    public float X;
    public float Y;
    public float Width;
    public float Height;
    public float Left => this.X;
    public float Right => this.X + this.Width;
    public float Top => this.Y;
    public float Bottom => this.Y + this.Height;
    public Vector2 Center => new Vector2(this.X + this.Width / 2, this.Y + this.Height / 2);
    public Vector2 BottomLeft => new Vector2(this.X, this.Y + this.Height);
    
    public Rectangle ToRectangle() => new Rectangle((int)this.X, (int)this.Y, (int)this.Width, (int)this.Height);

    public Rectangle BottomLeftRectangle => new Rectangle((int)this.X, (int)this.Y - (int)this.Height,
                                                          (int)this.Width, (int)this.Height);
    public RectangleF(float x, float y, float width, float height)
    {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
    }
    /// <summary>
    /// Works only for no rotated rectangles
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool Intersects(RectangleF value)
    {
        return value.Left < this.Right && this.Left < value.Right && value.Top < this.Bottom && this.Top < value.Bottom;
    }
    public float getDistance(RectangleF rec)
    {
        return (float)Math.Abs(Math.Pow(Math.Pow(rec.X - X, 2) + Math.Pow(rec.Y - Y, 2), 0.5f));
    }
    
    /// <summary>
    /// Checks if hitboxes close enough, not zero for better perform
    /// </summary>
    /// <param name="rec">object the distance to which is calculated</param>
    /// <returns></returns>
    public bool isDistanceXMoreZero(RectangleF rec) => rec.X - X > 1f;
    
    /// <summary>
    /// Checks if hitboxes close enough, not zero for better perform
    /// </summary>
    /// <param name="rec">object the distance to which is calculated</param>
    /// <returns></returns>
    public bool isDistanceXLessZero(RectangleF rec) => rec.X - X < 1f;
    
    /// <summary>
    /// Checks if hitboxes close enough, not zero for better perform
    /// </summary>
    /// <param name="rec">object the distance to which is calculated</param>
    /// <returns></returns>
    public bool isDistanceYMoreZero(RectangleF rec) => rec.Y - Y > 1f;
    
    /// <summary>
    /// Checks if hitboxes close enough, not zero for better perform
    /// </summary>
    /// <param name="rec">object the distance to which is calculated</param>
    /// <returns></returns>
    public bool isDistanceYLessZero(RectangleF rec) => rec.Y - Y < 1f;
}

public static class Circle
{
    public static Vector2 MoveInCircle(Rectangle circleCenter, int center, float yLimitMin)
    {
        double time = Globals.gameTime.TotalGameTime.TotalSeconds * 5f;

        float x = (float)Math.Cos(time);
        float y = (float)Math.Sin(time);
        return new Vector2(circleCenter.X +x * center, circleCenter.Y +y* center);
    }
}

class SwordVector
{
    private Vector2 _pos;
    private float _length;
    private float _startDegree = -90;
    private float  _leftSideDegree = -270;
    private float  _rightSideDegree = 90;
    private float _currentDegree = -90;
    public SwordVector(float length)
    {
        _length = length;
    }
    public float Thickness => 10f;
    public void UpdatePosition()
    {
        _pos = Globals.player._hitBox.Center;
    }
    public void RightSideUpdate()
    {
        _currentDegree = _currentDegree % 360;
        if (_currentDegree < _rightSideDegree)
        {
            _currentDegree += 8;
        }
    }
    public void LeftSideUpdate()
    {
        if (_currentDegree > _leftSideDegree)
        {
            _currentDegree -= 5;
        }
    }
    public void Reset()
    {
        _currentDegree = _startDegree;
    }

    public Vector2 getSecondPointVector()
    {
        float a = (float)Math.Sin(-MathHelper.ToRadians(_currentDegree)) * _length;
        float b = (float)Math.Cos(-MathHelper.ToRadians(_currentDegree)) * _length;
        return new Vector2(_pos.X + b, _pos.Y - a);
    }
    
    public void Draw()
    {
        Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("Debug/blackRectangle"),
            new Rectangle((int)_pos.X,(int)_pos.Y,(int)_length,(int)Thickness),
            null, Color.Black, MathHelper.ToRadians(_currentDegree),
            new Vector2(0,0), SpriteEffects.None, 0f);
    }
}
