using System;
using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Class;

public class EntityAnimation
{
    private Animation _upWalk;
    private Animation _downWalk;
    private Animation _leftWalk;
    private Animation _rightWalk;
    private Texture2D _idle;

    private Entity.Direction _direction;

    public EntityAnimation(Animation upWalk,Animation downWalk,Animation leftWalk,Animation rightWalk, Texture2D idle)
    {
        _upWalk = upWalk;
        _downWalk = downWalk;
        _leftWalk = leftWalk;
        _rightWalk = rightWalk;
        _idle = idle;
    }


    public void Update(Entity.Direction direction, bool isRunning)
    {
        _direction = direction;
        switch (_direction)
        {
            case Entity.Direction.Up:
                _upWalk.Update();
                if(isRunning)_upWalk.Update();
                break;
            case Entity.Direction.Down:
                _downWalk.Update();
                if(isRunning)_downWalk.Update();
                break;
            case Entity.Direction.Left:
                _leftWalk.Update();
                if(isRunning)_leftWalk.Update();
                break;
            case Entity.Direction.Right:
                _rightWalk.Update();
                if(isRunning)_rightWalk.Update();
                break;
        }
    }
    
    
    public void Draw(Rectangle rectangle)
    {
        switch (_direction)
        {
            case Entity.Direction.Up:
                _upWalk.Draw(rectangle);
                break;
            case Entity.Direction.Down:
                _downWalk.Draw(rectangle);
                break;
            case Entity.Direction.Left:
                _leftWalk.Draw(rectangle);
                break;
            case Entity.Direction.Right:
                _rightWalk.Draw(rectangle);
                break;
            case Entity.Direction.Idle:
                Globals.spriteBatch.Draw(_idle,rectangle,Color.White);
                break;
        }
    }
    
    
    public void Draw(RectangleF rectangle)
    {
        switch (_direction)
        {
            case Entity.Direction.Up:
                _upWalk.Draw(rectangle.ToRectangle());
                break;
            case Entity.Direction.Down:
                _downWalk.Draw(rectangle.ToRectangle());
                break;
            case Entity.Direction.Left:
                _leftWalk.Draw(rectangle.ToRectangle());
                break;
            case Entity.Direction.Right:
                _rightWalk.Draw(rectangle.ToRectangle());
                break;
            case Entity.Direction.Idle:
                Globals.spriteBatch.Draw(_idle,rectangle.ToRectangle(),Color.White);
                break;
        }
    }
}