using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace Game1.Class;

public class Animation
{
    private Texture2D animationTexture;
    private Rectangle rec;
    private int _frames;
    private int _frame;
    private Vector2 res;
    private bool isActive = true;
    private List<Rectangle> rectangles;
    private readonly float _frameTime = 0.2f;
    private float _frameTimeLeft;

    public Animation(Texture2D texture,Vector2 res, int count)
    {
        this.animationTexture = texture;
        this._frames = count;
        this.res = res;
        rectangles = new List<Rectangle>();
        for (int i = 0; i < _frames; i++)
        {
            rectangles.Add(new Rectangle(i*(int)this.res.X,0, (int)this.res.X, (int)this.res.Y));
        }
    }

    public void Start()
    {
        isActive = true;
    }

    public void Stop()
    {
        isActive = false;
    }

    public void Update()
    {
        if (!isActive) return;
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

}