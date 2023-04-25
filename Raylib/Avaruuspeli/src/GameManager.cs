namespace GameEngine6000;
using System;
using System.Numerics;
using Raylib_CsLo;

using Avaruuspeli;

/// <summary>
/// <c>GameManager</c> is a class that manages the game.
/// </summary>
/// <remarks>
/// <para>It contains the variables that are used in the game.</para>
/// <para>It also contains the methods that are used in the game.</para>
/// </remarks>


class GameManager : IBehaviour
{
    bool isGameOver = false;

    public bool IsGameOver
    {
        get => isGameOver;
        set => isGameOver = value;
    }

    static int maxHealth = 3;

    /* Setting the health to 3. */
    int health = maxHealth;

    public int Health
    {
        get => health;
        set => health = value;
    }

    /* Setting the score to 0. */
    int score = 0;

    public int Score
    {
        get => score;
        set => score = value;
    }

    int maxEnemies = 35;

    public int MaxEnemies
    {
        get => maxEnemies;
    }

    /* Setting the enemy count to 0. */
    int enemiesDestroyed = 0;

    public int EnemiesDestroyed
    {
        get => enemiesDestroyed;
        set => enemiesDestroyed = value;
    }

    /* Setting the time to 0. */
    double time = 0;

    public double Time
    {
        get => time;
        set => time = value;
    }

    /* Setting the score multiplier to 1. */
    float scoreMultiplier = 1.0f;

    public float ScoreMultiplier
    {
        get => scoreMultiplier;
        set => scoreMultiplier = value;
    }

    /// <summary>
    /// <c>Update</c> draws the score, health and score multiplier on the screen.
    /// </summary>
    public void Update()
    {
        Raylib.DrawText("Score: " + score, 10, 10, 20, Raylib.BLUE);
        Raylib.DrawText("Health: " + health, 10, 40, 20, Raylib.BLUE);
        Raylib.DrawText(
            "Score multiplier: " + scoreMultiplier.ToString("n2"),
            10,
            70,
            20,
            Raylib.BLUE
        );
        Raylib.DrawText("Switch input mode: I", 10, 100, 20, Raylib.BLUE);
    }

    /// <summary>
    /// It resets the game.
    /// </summary>
    public void Reset()
    {
        isGameOver = false;
        health = maxHealth;
        score = 0;
        scoreMultiplier = 1.0f;
        time = 0;
    }

    public void GameOver(Color color, string message)
    {
        isGameOver = true;
        Raylib.DrawText("Game Over!", 300, 400, 50, color);
        Raylib.DrawText(message, 250, 500, 30, color);
        Raylib.DrawText("Your score was: " + score, 250, 550, 30, color);
        Raylib.DrawText("Your time was: " + time, 250, 600, 30, color);
        Raylib.DrawText("You killed " + enemiesDestroyed + " enemies", 250, 650, 30, color);
    }
}
