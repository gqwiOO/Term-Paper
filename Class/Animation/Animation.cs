using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Class;

public class Animation
{
    private Texture2D animationTexture;
    private int _frames;
    private int _frame;
    private List<Rectangle> rectangles;
    private readonly float _frameTime;
    private float _frameTimeLeft;

    public Animation(Texture2D texture,Vector2 res, int count, float frameTime)
    {
        this.animationTexture = texture;
        this._frames = count;
        rectangles = new List<Rectangle>();
        this._frameTime = frameTime;
        for (int i = 0; i < _frames; i++)
        {
            rectangles.Add(new Rectangle(i*(int)res.X,0, (int)res.X, (int)res.Y));
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