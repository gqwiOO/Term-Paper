using Microsoft.Xna.Framework.Input;

namespace Movement;

public class Input
{
    static KeyboardState currentKeyState;
    static KeyboardState previousKeyState;
    static MouseState previousMouseState;
    static MouseState currentMouseState;

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
}