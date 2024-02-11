using System;
using Game1;
using Microsoft.Xna.Framework.Input;

using MathL;
namespace Movement;

public class Input
{
    static KeyboardState currentKeyState;
    static KeyboardState previousKeyState;
    static MouseState previousMouseState;
    static MouseState currentMouseState;
    public static int TopLeftPositionX;
    public static int TopLeftPositionY;

    public static KeyboardState GetKeyboardState()
    {
        previousKeyState = currentKeyState;
        currentKeyState = Keyboard.GetState();
        return currentKeyState;
    }

    public static MouseState GetMouseState()
    {
        previousMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
        TopLeftPositionX = (int)-Globals._camera.position.M41 - (int)Game1.Game1._screenWidth / 2;
        TopLeftPositionY = (int)-Globals._camera.position.M42 - (int)Game1.Game1._screenHeight / 2;
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
        int mouseX = TopLeftPositionX + Input.GetMouseState().X;
        int mouseY = TopLeftPositionY + Input.GetMouseState().Y;
        return mouseX > rec.X && mouseX < rec.X + rec.Width && mouseY > rec.Y && mouseY < rec.Y + rec.Height;
    }
}