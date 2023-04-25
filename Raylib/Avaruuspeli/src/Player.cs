using System;
using System.Numerics;
using Raylib_CsLo;
using GameEngine6000;

namespace Avaruuspeli;

/// <summary>
/// Class <c>Player</c> is used to create the player for the game.
/// </summary>

class Player : IBehaviour
{
    /* Creating a new instance of the Transform class. */
    public GameEngine6000.Transform transform;

    /* Creating a new instance of the SpriteRenderer class. */
    public SpriteRenderer sprite;

    /* A boolean variable that is used to check if the player can move or not. */
    public bool canMove = true;

    //If true player uses keyboard if false player uses mouse
    bool keyboardMovement = true;

    public bool KeyboardMovement
    {
        get => keyboardMovement;
    }

    /// <summary>
    /// The constructor for the Player class.
    /// </summary>
    /// <param name="position">The position of the player.</param>
    /// <param name="velocity">The velocity of the player.</param>
    public Player(Vector2 position, float maxVelocity, Texture texture, Vector2 size)
    {
        transform = new GameEngine6000.Transform(position, 0.0f, maxVelocity, .3f, 0.3f);
        sprite = new SpriteRenderer(new Vector2(0, 0), size, Raylib.WHITE, texture);
    }

    /// <summary>
    /// This function reads the directional input from a gamepad and returns a Vector2 representing the
    /// direction.
    /// </summary>
    public Vector2 ReadDirectionInput()
    {
        Vector2 direction = Vector2.Zero;
        direction.X = 0;
        direction.Y = 0;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            direction.X = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) || Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            direction.X = -1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP) || Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            direction.Y = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) || Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            direction.Y = 1;
        }

        return direction;
    }

    /// <summary>
    /// This function reads the directional input from a gamepad and returns a Vector2 representing the
    /// direction.
    /// </summary>
    public Vector2 ReadDirectionInputGamePad()
    {
        Vector2 direction = Vector2.Zero;
        direction.X = 0;
        direction.Y = 0;

        if (Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_RIGHT))
        {
            direction.X = 1;
        }
        else if (Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_LEFT))
        {
            direction.X = -1;
        }

        if (Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_UP))
        {
            direction.Y = -1;
        }
        else if (Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_DOWN))
        {
            direction.Y = 1;
        }

        return direction;
    }

    /// <summary>
    /// Method <c>Update</c> is used to update the player.
    /// </summary>
    /// <remarks>
    /// The method reads input.
    /// The method draws the player.
    /// The method prevents the player from going off screen.
    /// </remarks>
    public void Update()
    {
        if (keyboardMovement)
        {
            // Read input
            KeyPressed();
        }
        else
        {
            // GamePad movement
            GamePadMovement();
        }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_I))
        {
            keyboardMovement = !keyboardMovement;
        }

        // Draw player
        sprite.position = transform.position;
        sprite.Draw();
    }

    /// <summary>
    /// Method <c>GamePadMovement</c> is used to move the player with a gamepad.
    /// </summary>
    void GamePadMovement()
    {
        if (!canMove)
        {
            return;
        }

        if (!Raylib.IsGamepadAvailable(0))
        {
            Console.WriteLine("Gamepad not available");
            return;
        }

        Vector2 newDirection = ReadDirectionInputGamePad();

        // If the player is not moving, slow down

        if (newDirection.X == 0 && newDirection.Y == 0)
        {
            transform.velocity -= transform.sluggishness;
            if (transform.velocity < 0)
            {
                transform.velocity = 0;
            }
        }
        else
        {
            transform.velocity += transform.acceleration;
            if (transform.velocity > transform.maxVelocity)
            {
                transform.velocity = transform.maxVelocity;
            }
            if (
                newDirection.X > 0 && transform.direction.Y > 0
                || newDirection.X < 0 && transform.direction.Y < 0
                || newDirection.Y > 0 && transform.direction.X < 0
                || newDirection.Y < 0 && transform.direction.X > 0
            )
            {
                newDirection /= 1.25f;
            }
            if (transform.direction != newDirection)
            {
                transform.velocity -= transform.sluggishness * 2;
            }

            transform.direction = newDirection;
        }

        transform.position += transform.direction * transform.velocity;
    }

    /// <summary>
    /// A function that is called when a key is pressed.
    /// </summary>
    /// <remarks>
    /// If the player can't move, the function returns.
    /// If the right or D key is pressed, the player moves right.
    /// If the left or A key is pressed, the player moves left.
    /// </remarks>
    public void KeyPressed()
    {
        Vector2 newDirection = ReadDirectionInput();

        if (!canMove)
        {
            return;
        }

        // If the player is not moving, slow down
        if (newDirection.X == 0 && newDirection.Y == 0)
        {
            transform.velocity -= transform.sluggishness;
            if (transform.velocity < 0)
            {
                transform.velocity = 0;
            }
        }
        else
        {
            transform.velocity += transform.acceleration;
            if (transform.velocity > transform.maxVelocity)
            {
                transform.velocity = transform.maxVelocity;
            }
            if (
                newDirection.X > 0 && transform.direction.Y > 0
                || newDirection.X < 0 && transform.direction.Y < 0
                || newDirection.Y > 0 && transform.direction.X < 0
                || newDirection.Y < 0 && transform.direction.X > 0
            )
            {
                newDirection /= 1.25f;
            }
            if (transform.direction != newDirection)
            {
                transform.velocity -= transform.sluggishness * 2;
            }

            transform.direction = newDirection;
        }
        // Move the player
        transform.position += transform.velocity * transform.direction;
    }
}
