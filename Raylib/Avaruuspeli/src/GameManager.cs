using System;
using System.Numerics;
using Raylib_CsLo;

using Avaruuspeli;

class GameManager
{
    bool isGameOver = false;
    int health = 3;
    int score = 0;

    int enemyCount = 0;

    double time = 0;

    float scoreMultiplier = 1.0f;

    public void Update()
    {
        Raylib.DrawText("Score: " + score, 10, 10, 20, Raylib.WHITE);
        Raylib.DrawText("Health: " + health, 10, 40, 20, Raylib.WHITE);
        Raylib.DrawText("Score multiplier: " + scoreMultiplier.ToString("n2"), 10, 70, 20, Raylib.WHITE);
    }

    public void Reset()
    {
        isGameOver = false;
        ResetHealth();
        ResetScore();
        ResetTime();
    }

    public void RemoveHealth(int amount)
    {
        health -= amount;
    }

    public void ResetHealth()
    {
        health = 3;
    }

    public int GetHealth()
    {
        return health;
    }

    public void AddScore(int amount)
    {
        score += amount * (int)scoreMultiplier;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScoreMultiplier(float value)
    {
        scoreMultiplier += value;
    }

    public float GetScoreMultiplier()
    {
        return scoreMultiplier;
    }


    public void SetGameOver(bool value)
    {
        if(isGameOver) return;
        
        isGameOver = value;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void AddEnemyCount(int value)
    {
        enemyCount += value;
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }

    public void SetTime(double value)
    {
        time = value;
    }

    public double GetTime()
    {
        return time;
    }

    public void ResetTime()
    {
        time = 0.0f;
    }


}
