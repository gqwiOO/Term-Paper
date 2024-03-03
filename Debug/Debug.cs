using Microsoft.Xna.Framework;
using Font = TermPaper.Class.Font.Font;
namespace Game1.Debug;

public class Debug
{
    public static string Text = "";
    
    
    public static void DrawInfo()
    {
        Globals.spriteBatch.DrawString(Font.fonts["MainFont-24"],Text,new Vector2(10, 400),Color.Black);
    }
}