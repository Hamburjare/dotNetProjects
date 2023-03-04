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


class GameManager
{
    /* Setting the variable isGameOver to false. */
    bool isGameOver = false;

    /* Setting the health to 3. */
    int health = 3;

    /* Setting the score to 0. */
    int score = 0;

    /* Setting the enemy count to 0. */
    int enemyCount = 0;
    
    /* Setting the time to 0. */
    double time = 0;

    /* Setting the score multiplier to 1. */
    float scoreMultiplier = 1.0f;

    /// <summary>
    /// <c>Update</c> draws the score, health and score multiplier on the screen.
    /// </summary>
    public void Update()
    {
        Raylib.DrawText("Score: " + score, 10, 10, 20, Raylib.WHITE);
        Raylib.DrawText("Health: " + health, 10, 40, 20, Raylib.WHITE);
        Raylib.DrawText("Score multiplier: " + scoreMultiplier.ToString("n2"), 10, 70, 20, Raylib.WHITE);
    }

    /// <summary>
    /// It resets the game.
    /// </summary>
    public void Reset()
    {
        isGameOver = false;
        ResetHealth();
        ResetScore();
        ResetTime();
    }

    /// <summary>
    /// It removes health from the player.
    /// </summary>
    /// <param name="amount">The amount of health to remove.</param>
    public void RemoveHealth(int amount)
    {
        health -= amount;
    }

    /// <summary>
    /// It resets the health of the player.
    /// </summary>
    public void ResetHealth()
    {
        health = 3;
    }

    /// <summary>
    /// Returns the health of the player.
    /// </summary>
    public int GetHealth()
    {
        return health;
    }

    /// <summary>
    /// Adds the amount to the score.
    /// </summary>
    /// <param name="amount">The amount of points to add to the score.</param>
    public void AddScore(int amount)
    {
        score += amount * (int)scoreMultiplier;
    }

    /// <summary>
    /// It resets the score to 0.
    /// </summary>
    public void ResetScore()
    {
        score = 0;
    }

    /// <summary>
    /// Returns the score.
    /// </summary>
    public int GetScore()
    {
        return score;
    }

    /// <summary>
    /// Updates the scoreMultiplier variable.
    /// </summary>
    /// <param name="value">The value to add to the score multiplier.</param>
    public void AddScoreMultiplier(float value)
    {
        scoreMultiplier += value;
    }


    /// <summary>
    /// Sets the game over to true or false.
    /// </summary>
    /// <param name="value">This is a boolean value that determines whether the game is over or
    /// not.</param>
    public void SetGameOver(bool value)
    {
        isGameOver = value;
    }

    /// <summary>
    /// Returns the isGameOver variable value.
    /// </summary>
    public bool IsGameOver()
    {
        return isGameOver;
    }

    /// <summary>
    /// It adds the value to the enemyCount variable.
    /// </summary>
    /// <param name="value">The amount of enemies to add to the count.</param>
    public void AddEnemyCount(int value)
    {
        enemyCount += value;
    }

    /// <summary>
    /// It returns the number of enemies in the game.
    /// </summary>
    public int GetEnemyCount()
    {
        return enemyCount;
    }

    /// <summary>
    /// Sets the time of the clock to the value passed in.
    /// </summary>
    /// <param name="value">The time to set the timer to.</param>
    public void SetTime(double value)
    {
        time = value;
    }

    /// <summary>
    /// Returns the value of the time variable.
    /// </summary>
    public double GetTime()
    {
        return time;
    }

    /// <summary>
    /// It resets the time to 0.
    /// </summary>
    public void ResetTime()
    {
        time = 0.0f;
    }


}
