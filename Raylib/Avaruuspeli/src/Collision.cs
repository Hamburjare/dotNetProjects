using System;
using System.Numerics;
using Raylib_CsLo;

namespace GameEngine6000;

/// <summary>
/// Class <c>Collision</c> is used to check if two rectangles collide.
/// </summary>
class Collision
{

    /// <summary>
    /// Checks if two rectangles are colliding.
    /// </summary>
    /// <param name="Rectangle">The rectangle to check for collision.</param>
    /// <param name="Rectangle">The rectangle to check for collision.</param>
    public bool CheckCollision(Rectangle rect1, Rectangle rect2)
    {
        if (Raylib.CheckCollisionRecs(rect1, rect2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
