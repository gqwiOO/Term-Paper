using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Class;

public class Animation
{
    public Texture2D animationTexture;
    public Rectangle rec;
    public int _frames;
    public int _frame;
    public Vector2 res;
    public List<Rectangle> rectangles;
    public readonly float _frameTime = 0.2f;
    public float _frameTimeLeft;

    public Animation(Texture2D texture,Vector2 res, int count, float frameTime)
    {
        this.animationTexture = texture;
        this._frames = count;
        this.res = res;
        rectangles = new List<Rectangle>();
        this._frameTime = frameTime;
        for (int i = 0; i < _frames; i++)
        {
            rectangles.Add(new Rectangle(i*(int)this.res.X,0, (int)this.res.X, (int)this.res.Y));
        }
    }
    
    public void Update()
    {
        _frameTimeLeft -=(float)Globals.gameTime.ElapsedGameTime.TotalSeconds;

        if (_frameTimeLeft <= 0)
        {
            _frameTimeLeft += _frameTime;
            _frame = (_frame + 1) % _frames;
        }
    }

    public void Draw(Rectangle position)
    {
        Globals.spriteBatch.Draw(animationTexture,position,rectangles[_frame],
             Color.White);
    }

    public int getFrame()
    {
        return _frame;
    }

}