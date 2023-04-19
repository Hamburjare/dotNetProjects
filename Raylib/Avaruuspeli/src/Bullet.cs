using System;
using System.Numerics;
using Raylib_CsLo;
using GameEngine6000;

namespace Avaruuspeli;

/// <summary>
/// Class <c>Bullet</c> is used to create bullets for the game.
/// </summary>

class Bullet : IBehaviour
{
    /* A boolean variable that is used to check if the bullet is active or not. */
    public bool isActive = false;

    /* Creating a new instance of the Transform class. */
    public GameEngine6000.Transform transform;

    /* Creating a new instance of the SpriteRenderer class. */
    public SpriteRenderer sprite;

    /* Variable for camera */
    public Camera2D camera;

    /// <summary>
    /// The constructor for the Bullet class.
    /// </summary>
    public Bullet()
    {
        transform = new GameEngine6000.Transform(new Vector2(0, 0), 0);
        sprite = new SpriteRenderer(new Vector2(0, 0), new Vector2(10, 10), Raylib.WHITE);
    }

    /// <summary>
    /// Method <c>Update</c> is used to update the bullet.
    /// </summary>
    /// <remarks>
    /// If the bullet is not active, the method returns.
    /// If the bullet goes off screen, the method sets it to inactive.
    /// The method draws the bullet.
    /// </remarks>
    public void Update()
    {
        // If bullet is not active, return
        if (!isActive)
        {
            return;
        }

        Vector2 bulletPositionInScreen = Raylib.GetWorldToScreen2D(transform.position, camera);

        // If bullet goes off screen from below, set it to inactive
        if (bulletPositionInScreen.Y > Raylib.GetScreenHeight())
        {
            isActive = false;
        }

        // Draw bullet
        sprite.position = transform.position;
        sprite.Draw();
    }

    /// <summary>
    /// Method <c>SetActive</c> is used to set the bullet to active.
    /// </summary>
    /// <param name="Vector2">Bullet's starting position</param>
    /// <param name="start_velocity">The velocity of the projectile when it is fired.</param>
    public void SetActive(Vector2 start_position, float start_velocity)
    {
        isActive = true;
        transform.position = start_position;
        transform.velocity = start_velocity;
    }
}
