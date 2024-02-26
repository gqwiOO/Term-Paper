using Microsoft.Xna.Framework;
using Font = TermPaper.Class.Font.Font;
namespace Game1.Debug;

public class Debug
{
    public static void DrawInfo(string text,int yPosition)
    {
        Globals.spriteBatch.DrawString(Font.fonts["MainFont-24"],text,new Vector2(10, yPosition),Color.Black);
    }
}