using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MathL;

public struct RectangleF
{
    public float X;
    public float Y;
    public float Width;
    public float Height;

    public float Angle;
    
    public Vector2 x1y1;
    public Vector2 x2y2 = new Vector2(0,0);
    public Vector2 x3y3 = new Vector2(0,0);
    public Vector2 x4y4 = new Vector2(0,0);
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
        x1y1 = new Vector2(x,y);
        x2y2 = new Vector2(x + width,y);
        x3y3 = new Vector2(x + width,y + height);
        x4y4 = new Vector2(x,y + height);
        Angle = 0;
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

public class SwordVector
{
    private int startDegree = -90;
    private int length;
    private int angle;
    private Vector2 startPoint;
    private Vector2 endPoint;
    public SwordVector(int length, int angle, Vector2 startPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = new Vector2(this.startPoint.X, this.startPoint.Y + length);
        this.length = length;
        this.angle = angle;
    }
    public void Update(Vector2 pos)
    {
        startPoint = pos;
        this.endPoint = new Vector2(this.startPoint.X, this.startPoint.Y + length);
    }

    public void Reset()
    {
        angle = startDegree;
        endPoint = new Vector2(this.startPoint.X, this.startPoint.Y - length);
    }

    public Vector2 getEndPoint()
    {
        return new Vector2(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
    }

    public void Rotate(int Angle)
    {
        // Convert position end point to 
        Vector2 endPointConverted = new Vector2(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
        float x = endPointConverted.X; 
        float y = endPointConverted.Y;
        endPointConverted.X = x * (float)Math.Cos(MathHelper.ToRadians(Angle)) -
                              y * (float)Math.Sin(MathHelper.ToRadians(Angle));
        endPointConverted.Y = x * (float)Math.Sin(MathHelper.ToRadians(Angle)) +
                              y * (float)Math.Cos(MathHelper.ToRadians(Angle));
        endPoint.X = endPointConverted.X + startPoint.X;
        endPoint.Y = endPointConverted.Y + startPoint.Y;
        angle = Angle;
    }
    public void Draw()
    {
        Globals.spriteBatch.Draw(GetTexture(Globals.spriteBatch), new Vector2(startPoint.X, startPoint.Y), null,
            Color.Black, MathHelper.ToRadians(angle), new Vector2(0, 0), new Vector2(length, 5f), SpriteEffects.None,
            0);
    }

    public void DrawEndPoint()
    {
        Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("Debug/blackRectangle"),endPoint,Color.Black);
    }
    
    private static Texture2D GetTexture(SpriteBatch spriteBatch)
    {
        Texture2D _texture;
        _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        _texture.SetData(new[] {Color.White});
        return _texture;
    }
    
    
    public bool CollisionWithRectangle(float rx, float ry, float rw, float rh) {

        // check if the line has hit any of the rectangle's sides
        // uses the Line/Line function below
        bool left =   lineLine(startPoint.X,startPoint.Y,endPoint.X,endPoint.Y, rx,ry,rx, ry+rh);
        bool right =  lineLine(startPoint.X,startPoint.Y,endPoint.X,endPoint.Y, rx+rw,ry, rx+rw,ry+rh);
        bool top =    lineLine(startPoint.X,startPoint.Y,endPoint.X,endPoint.Y, rx,ry, rx+rw,ry);
        bool bottom = lineLine(startPoint.X,startPoint.Y,endPoint.X,endPoint.Y, rx,ry+rh, rx+rw,ry+rh);

        // if ANY of the above are true, the line
        // has hit the rectangle
        if (left || right || top || bottom) {
            return true;
        }
        return false;
    }

    
    private bool lineLine(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4) {

        // calculate the direction of the lines
        float uA = ((x4-x3)*(y1-y3) - (y4-y3)*(x1-x3)) / ((y4-y3)*(x2-x1) - (x4-x3)*(y2-y1));
        float uB = ((x2-x1)*(y1-y3) - (y2-y1)*(x1-x3)) / ((y4-y3)*(x2-x1) - (x4-x3)*(y2-y1));

        // if uA and uB are between 0-1, lines are colliding
        if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1) {

            // optionally, draw a circle where the lines meet
            float intersectionX = x1 + (uA * (x2-x1));
            float intersectionY = y1 + (uA * (y2-y1));

            return true;
        }
        return false;
    }
    
    
}
class SwordHandVector
{
    private Vector2 _pos;
    private float _length;
    private float _startDegree = -90;
    private float  _leftSideDegree = -270;
    private float  _rightSideDegree = 90;
    private float _currentDegree = -90;
    public SwordHandVector(float length)
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
            _currentDegree += 5;
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
