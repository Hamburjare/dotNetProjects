using System;
using System.Numerics;
using Raylib_CsLo;

namespace GameEngine6000;

class Collision
{

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
