using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

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
    public RectangleF(float x, float y, float width, float height)
    {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
    }

    public bool Intersects(RectangleF value)
    {
        return value.Left < this.Right && this.Left < value.Right && value.Top < this.Bottom && this.Top < value.Bottom;
    }

    public Rectangle ToRectangle()
    {
        return new Rectangle((int)this.X, (int)this.Y, (int)this.Width, (int)this.Height);
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
    public bool isDistanceXMoreZero(RectangleF rec)
    {
        return rec.X - X > 1f;
    }
    
    public bool isDistanceXLessZero(RectangleF rec)
    {
        return rec.X - X < 1f;
    }
    public bool isDistanceYMoreZero(RectangleF rec)
    {
        return rec.Y - Y > 1f;
    }
    public bool isDistanceYLessZero(RectangleF rec)
    {
        return rec.Y - Y < 1f;
    }
}

public static class Circle
{
    public static float getYInCircle(float x, float radius)
    {
        return (float)Math.Pow(Math.Pow(radius, 2) - (float)Math.Pow(x, 2), 1/2);
    }
}
