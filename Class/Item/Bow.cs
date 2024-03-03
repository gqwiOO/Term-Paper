using Game1.Class.Entity;
using Microsoft.Xna.Framework.Graphics;
using Movement;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Game1.Class.Item;

public class Bow: Item
{
 // Json Fields
    public int damage{ get; set; }
    public float cooldown { get; set;}
    public int animationTime { get; set; }

    private int _arrowFlyingDuration = 2000;
    private int _arrowTimer;
    
    private int _bowDuration = 400;
    private int _bowTimer;
    
    private bool isActive;

    private Arrow arrow;
    public bool getACtiveStatus() => isActive;
    public override void Use()
    {
    }

    public string spritePath
    {
        set { _sprite = Globals.Content.Load<Texture2D>(value); }
    }


    public override void Update()
    {
        if (Globals.gameState == State.State.Playing)
        {
            if (Input.hasBeenLeftMouseButtonPressed() && arrow == null &&
                Globals.Player.inventory.getCurrentItemIndex() == Globals.Player.inventory.getBowIndex())
            {
                arrow = new Arrow(MathL.MathL.GetUnitVector2Mouse());
            }

            if (arrow != null)
            {
                _arrowTimer += (int)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
                _bowTimer += (int)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
                arrow.Update();
                if (arrow.IntersectsEnemy()) arrow = null;
                
                if (_arrowTimer > _arrowFlyingDuration)
                {
                    _arrowTimer = 0;
                    arrow = null;
                    _bowTimer = 0;
                }
            }
            else
            {
                _bowTimer = 0;
            }
        }
    }
    
    
    public override void Draw()
    {
        if (arrow != null && Globals.gameState == State.State.Playing)
        {
            if (_bowTimer < _bowDuration)
            {
                Globals.spriteBatch.Draw(_sprite,
                    new Rectangle((int)Globals.Player._hitBox.CenterRec.X + BowXPosiition(), (int)Globals.Player._hitBox.CenterRec.Y,
                        60, 60),
                    null, Color.White, arrow.Rotation(), new Vector2(_sprite.Width / 2, _sprite.Height / 2),
                    SpriteEffects.None, 0f);
            }
            arrow.Draw();
        }
    }

    public int BowXPosiition()
    {
        if (Globals.Player.lastStrafeDirection == Direction.Left) return -10;
        return 10;
    }
}