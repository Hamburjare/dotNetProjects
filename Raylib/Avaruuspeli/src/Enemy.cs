using System;
using System.Numerics;
using Raylib_CsLo;
using GameEngine6000;

namespace Avaruuspeli;

enum MoveStyle
{
    Random,
    Stationary
}

/// <summary>
/// Class <c>Enemy</c> is used to create enemies for the game.
/// </summary>
class Enemy : IBehaviour
{
    /* A variable that is used to check if the enemy is active or not. */
    public bool isActive = false;

    /* Used to check if the enemy can move or not. */
    public bool canMove = true;

    /* Creating a new instance of the Transform class. */
    public GameEngine6000.Transform transform;

    /* Creating a new instance of the SpriteRenderer class. */
    public SpriteRenderer sprite;

    public MoveStyle moveStyle = MoveStyle.Random;

    public Vector2 moveDirection = Vector2.Zero;

    /// <summary>
    /// The constructor for the Enemy class.
    /// </summary>
    public Enemy(Texture texture, Vector2 size)
    {
        transform = new GameEngine6000.Transform(new Vector2(0, 0), 5.0f);

        sprite = new SpriteRenderer(new Vector2(0, 0), size, Raylib.WHITE, texture);

        Random random = new Random();
        moveStyle = (MoveStyle)random.Next(0, Enum.GetNames(typeof(MoveStyle)).Length);

        moveDirection.X = random.Next(0, 2) * 2 - 1;
        moveDirection.Y = 1;
    }

    /// <summary>
    /// The constructor for the Enemy class.
    /// </summary>
    public Enemy(string texture)
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

        if (transform.position.X < 0)
        {
            transform.position.X = 0;
        }
        else if (transform.position.X > Raylib.GetScreenWidth() - sprite.size.X)
        {
            transform.position.X = Raylib.GetScreenWidth() - sprite.size.X;
        }

        switch (moveStyle)
        {
            case MoveStyle.Random:
                MoveRandom();
                break;
            case MoveStyle.Stationary:
                MoveStationary();
                break;
            default:
                break;
        }
    }

    // TODO: Add more move styles
    
    void MoveRandom()
    {
        // Move enemy randomly left or right and dont change direction until enemy is outside of the screen use moveDirection
        transform.position.X += transform.velocity * moveDirection.X;
        
        // If enemy is outside of the screen, change direction
        if (transform.position.X < 0 || transform.position.X > Raylib.GetScreenWidth() - sprite.size.X)
        {
            moveDirection.X *= -1;
        }
        
    }

    // Move stationary
    void MoveStationary()
    {
        transform.position.Y += transform.velocity * moveDirection.Y;
    }

}
