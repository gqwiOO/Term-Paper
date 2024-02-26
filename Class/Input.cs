using System;
using Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX;
using RectangleF = MathL.RectangleF;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Movement;

public class Input
{
    static KeyboardState currentKeyState;
    static KeyboardState previousKeyState;
    static MouseState previousMouseState;
    static MouseState currentMouseState;
    public static float TopLeftPositionX;
    public static float TopLeftPositionY;

    public static KeyboardState GetKeyboardState()
    {
        previousKeyState = currentKeyState;
        currentKeyState = Keyboard.GetState();
        return currentKeyState;
    }

    
    public static MouseState GetMouseState()
    {
        TopLeftPositionX = -Globals._camera.position.M41 - Game1.Game1._screenWidth / 2;
        TopLeftPositionY = -Globals._camera.position.M42 - Game1.Game1._screenHeight / 2;
        previousMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();

        return currentMouseState;
    }

    
    public static bool hasBeenLeftMouseButtonPressed()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed &&
               previousMouseState.LeftButton != ButtonState.Pressed;
    }

    
    public static bool isLeftButtonPressed()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed;
    }

    
    public static bool hasBeenRightMouseButtonPressed()
    {
        return currentMouseState.RightButton == ButtonState.Pressed &&
               previousMouseState.RightButton != ButtonState.Pressed;
    }

    
    public static bool isPressed(Keys key)
    {
        return currentKeyState.IsKeyDown(key);
    }

    
    public static bool hasBeenPressed(Keys key)
    {
        return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
    }

    
    public static bool isMouseInRectangle(RectangleF rec)
    {
        float mouseX = TopLeftPositionX + Globals.mouseState.X;
        float mouseY = TopLeftPositionY + Globals.mouseState.Y;
        return mouseX > rec.X && mouseX < rec.X + rec.Width && mouseY > rec.Y && mouseY < rec.Y + rec.Height;
    }
}