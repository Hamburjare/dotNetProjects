using System;
using System.Numerics;
using Raylib_CsLo;
using GameEngine6000;

namespace Avaruuspeli;

/// <summary>
/// Class <c>Enemy</c> is used to create enemies for the game.
/// </summary>
class Enemy
{
    /* A variable that is used to check if the enemy is active or not. */
    public bool isActive = false;

    /* Used to check if the enemy can move or not. */
    bool canMove = true;

    /* Creating a new instance of the Transform class. */
    public GameEngine6000.Transform transform;

    /* Creating a new instance of the SpriteRenderer class. */
    public SpriteRenderer sprite;

    /// <summary>
    /// The constructor for the Enemy class.
    /// </summary>
    public Enemy(Texture texture)
    {
        transform = new GameEngine6000.Transform(new Vector2(0, 0), 5.0f);

        sprite = new SpriteRenderer(
            new Vector2(815, 290),
            new Vector2(50, 50),
            Raylib.WHITE,
            texture
        );
    }

    /// <summary>
    /// Method <c>Update</c> is used to update the enemy.
    /// </summary>
    /// <remarks>
    /// If the enemy is not active, the method returns.
    /// The method draws the enemy.
    /// The method moves the enemy.
    /// </remarks>
    public void Update()
    {
        // If enemy is not active, return
        if (!isActive)
        {
            return;
        }

        // Draw enemy
        sprite.position = transform.position;
        sprite.Draw();

        // Move enemy
        MoveEnemy();
    }

    /// <summary>
    /// It sets the activity to false.
    /// </summary>
    public void SetActivityFalse()
    {
        isActive = false;
    }

    /// <summary>
    /// Sets the activity to true.
    /// </summary>
    public void SetActivityTrue()
    {
        isActive = true;
    }

    /// <summary>
    /// Returns the isActive variable.
    /// </summary>
    public bool IsActive()
    {
        return isActive;
    }

    /// <summary>
    /// Sets the canMove variable.
    /// </summary>
    /// <param name="canMove">Whether or not the player can move.</param>
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    /// <summary>
    /// Sets the enemy active.
    /// </summary>
    /// <param name="start_position">The starting position of the enemy.</param>
    /// <param name="start_velocity">The starting velocity of the enemy.</param>
    public void SetActive(Vector2 start_position, float start_velocity)
    {
        isActive = true;
        transform.position = start_position;
        transform.velocity = start_velocity;
    }

    /// <summary>
    /// Method <c>MoveEnemy</c> is used to move the enemy.
    /// </summary>
    /// <remarks>
    /// If the enemy can't move, the method returns.
    /// The method moves the enemy randomly left or right.
    /// The method preverts the enemy from going outside of the screen.
    /// </remarks>
    void MoveEnemy()
    {
        if (!canMove)
        {
            return;
        }

        // Move enemy randomly left or right
        if (Raylib.GetRandomValue(0, 100) < 50)
        {
            transform.position.X += transform.velocity;
        }
        else
        {
            transform.position.X -= transform.velocity;
        }

        /* Preverts enemy for going outside of the screen*/
        transform.position.X = Math.Clamp(
            transform.position.X,
            0,
            Raylib.GetScreenWidth() - sprite.size.X
        );
    }
}
