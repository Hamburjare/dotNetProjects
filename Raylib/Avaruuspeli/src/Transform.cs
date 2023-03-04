using System;
using System.Numerics;
using Raylib_CsLo;

namespace GameEngine6000;

class Transform
{
    public Vector2 position;
    public float velocity;

    public Transform(Vector2 position, float velocity)
    {
        this.position = position;
        this.velocity = velocity;
    }

    public Transform()
    {
        this.position = Vector2.Zero;
        this.velocity = 0.0f;
    }
}
