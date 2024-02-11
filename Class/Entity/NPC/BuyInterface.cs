using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Movement;

namespace Game1.Class.Entity;

public class BuyInterface
{
    private Texture2D _npcShopBackground = Globals.Content.Load<Texture2D>("NPC/Interface/npcShopBackground");
    private int _widthNPCShopBackground = Game1._screenWidth;
    private int _heightNPCShopBackground = Game1._screenHeight;
    
    public void Update()
    {
        
    }
    
    public void Draw()
    {
        if (Globals.gameState == State.State.InShop)
        {
            Globals.spriteBatch.Draw(_npcShopBackground,
                new Rectangle(0,0,_widthNPCShopBackground,_heightNPCShopBackground), null, Color.Black,
                0,new Vector2(0, 0),SpriteEffects.None, 0f);
        }
        
    }
}