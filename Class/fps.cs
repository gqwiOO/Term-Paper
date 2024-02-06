using Microsoft.Xna.Framework;
using TermPaper.Class.Font;

namespace Game1.Class;

public class Fps
{
    private double frames = 0;
    private double updates = 0;
    private double elapsed = 0;
    private double last = 0;
    private double now = 0;
    public double msgFrequency = 1.0f; 
    public string msg = "";

    /// <summary>
    /// The msgFrequency here is the reporting time to update the message.
    /// </summary>
    public void Update()
    {
        now = Globals.gameTime.TotalGameTime.TotalSeconds;
        elapsed = (now - last);
        if (elapsed > msgFrequency)
        {
            msg = " Fps: " + frames.ToString();
            elapsed = 0;
            frames = 0;
            updates = 0;
            last = now;
        }
        updates++;
    }

    public void DrawFps( Vector2 fpsDisplayPosition, Color fpsTextColor)
    {
        Globals.spriteBatch.DrawString(Font.fonts["MainFont-24"], msg, fpsDisplayPosition, fpsTextColor);
        frames++;
    }
}