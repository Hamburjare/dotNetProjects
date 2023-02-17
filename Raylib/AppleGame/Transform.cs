using System;
using System.Numerics;


namespace AppleGame;

class Transform {
    public Vector2 position;
    public float rotation;
    public Vector2 scale;
    public float velocity;

    public Transform(Vector2 position, float rotation, float velocity, Vector2 scale) {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.velocity = velocity;
    }

    public Transform() {
        this.position = Vector2.Zero;
        this.rotation = 0.0f;
        this.scale = Vector2.One;
        this.velocity = 0.0f;
    }
    
}