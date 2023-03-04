using System;
using System.Numerics;
using Raylib_CsLo;

namespace GameEngine6000;

/// <summary>
/// Class <c>Transform</c> is used to store the position and velocity of an object.
/// </summary>
class Transform
{
    /* A variable that is used to store the position of the object. */
    public Vector2 position;

    /* A variable that is used to store the velocity of the object. */    
    public float velocity;

    /// <summary>
    /// The constructor for the Transform class.
    /// </summary>
    /// <param name="position">The position of the object.</param>
    /// <param name="velocity">The velocity of the object.</param>
    public Transform(Vector2 position, float velocity)
    {
        this.position = position;
        this.velocity = velocity;
    }

    /// <summary>
    /// The constructor for the Transform class.
    /// </summary>
    public Transform()
    {
        this.position = Vector2.Zero;
        this.velocity = 0.0f;
    }
}
