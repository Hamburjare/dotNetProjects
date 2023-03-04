using System;
using System.Numerics;
using Raylib_CsLo;
using GameEngine6000;

namespace Avaruuspeli;

/// <summary>
/// Class <c>Player</c> is used to create the player for the game.
/// </summary>

class Player
{
    /* Creating a new instance of the Transform class. */
    public GameEngine6000.Transform transform;

    /* Creating a new instance of the SpriteRenderer class. */
    public SpriteRenderer sprite;

    /* A boolean variable that is used to check if the player can move or not. */
    bool canMove = true;

    //If true player uses keyboard if false player uses mouse
    bool keyboardMovement = true;

    /// <summary>
    /// The constructor for the Player class.
    /// </summary>
    /// <param name="position">The position of the player.</param>
    /// <param name="velocity">The velocity of the player.</param>
    public Player(Vector2 position, float velocity)
    {
        transform = new GameEngine6000.Transform(position, velocity);
        sprite = new SpriteRenderer(
            position,
            new Vector2(50, 50),
            Raylib.WHITE,
            "./resources/textures/widepasi.png"
        );
    }

    /// <summary>
    /// Sets whether or not the player can move.
    /// </summary>
    /// <param name="canMove">Whether or not the player can move.</param>
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
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
        if(keyboardMovement)
        {
            // Read input
            KeyPressed();
        }
        else
        {
            // Mouse movement
            MouseMovement();
        }

        if(Raylib.IsKeyPressed(KeyboardKey.KEY_I))
        {
            keyboardMovement = !keyboardMovement;
        }

        // Draw player
        sprite.position = transform.position;
        sprite.Draw();

        // Prevert player from going off screen
        transform.position.X = Math.Clamp(
            transform.position.X,
            0,
            Raylib.GetScreenWidth() - sprite.size.X
        );
    }

    /// <summary>
    /// Method <c>MouseMovement</c> is used to move the player with the mouse.
    /// </summary>
    /// <remarks>
    /// The method gets the mouse position.
    /// The method sets the player's position to the mouse's position.
    /// </remarks>
    void MouseMovement() {

        if(!canMove)
        {
            return;
        }

        Vector2 mousePosition = Raylib.GetMousePosition();
        transform.position.X = mousePosition.X;
    }

    /// <summary>
    /// Gets whether player uses keyboard or mouse for movement
    /// </summary>
    public bool GetKeyboardMovement()
    {
        return keyboardMovement;
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
        if (!canMove)
        {
            return;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            transform.position.X += transform.velocity;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) || Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            transform.position.X -= transform.velocity;
        }
    }
}
