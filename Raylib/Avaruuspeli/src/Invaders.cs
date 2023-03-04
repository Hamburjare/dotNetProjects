using System;
using System.Numerics;
using Raylib_CsLo;
using GameEngine6000;

namespace Avaruuspeli;

class Invaders
{
    public void Run()
    {
        Random random = new Random();
        List<Enemy> enemies = new List<Enemy>();
        List<Bullet> bullets = new List<Bullet>();
        List<Bullet> enemyBullets = new List<Bullet>();
        float playerShootCooldown = 0.0f;

        

        Raylib.InitWindow(800, 900, "Space Invaders");
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();

        Player player = new Player(new Vector2(400, 830), 5.0f);
        GameManager gameManager = new GameManager();


        Sound shootSound;
        Sound explosionSound;
        Sound hitSound;

        SpawnEnemies();
        LoadSounds();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && playerShootCooldown <= 0)
            {
                if (gameManager.IsGameOver())
                {
                    return;
                }

                Bullet bullet = new Bullet();
                Vector2 bulletPosition = new Vector2(
                    player.transform.position.X + player.sprite.size.X / 2,
                    player.transform.position.Y
                );
                bullet.SetActive(bulletPosition, 10.0f);
                bullets.Add(bullet);

                Raylib.PlaySound(shootSound);

                playerShootCooldown = .75f;
            }

            playerShootCooldown -= Raylib.GetFrameTime();

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
            {
                RestartGame();
            }

            foreach (Bullet bullet in bullets)
            {
                bullet.transform.position.Y -= bullet.transform.velocity;
                bullet.Update();
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Update();
            }

            CheckForCollisions();
            EnemyShoot();
            CheckGameProgress();

            gameManager.Update();

            player.Update();

            Raylib.EndDrawing();
        }

        void CheckGameProgress()
        {
            if (gameManager.GetHealth() <= 0)
            {
                if (!gameManager.IsGameOver())
                {
                    Raylib.PlaySound(explosionSound);
                    gameManager.SetTime(Raylib.GetTime());
                }

                GameOver();
            }

            // Check if all enemies are dead
            bool allEnemiesDead = true;
            foreach (Enemy enemy in enemies)
            {
                if (enemy.IsActive())
                {
                    allEnemiesDead = false;
                }
            }

            if (allEnemiesDead)
            {
                if (!gameManager.IsGameOver())
                {
                    SpawnEnemies();
                }
            }
        }

        void GameOver()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.SetCanMove(false);
            }
            gameManager.SetGameOver(true);
            player.SetCanMove(false);

            Raylib.DrawText("Game Over!", 300, 400, 50, Raylib.RED);
            Raylib.DrawText("Press Enter to restart", 250, 500, 30, Raylib.RED);
            Raylib.DrawText("Your score was: " + gameManager.GetScore(), 250, 550, 30, Raylib.RED);
            Raylib.DrawText("Your time was: " + gameManager.GetTime(), 250, 600, 30, Raylib.RED);
            Raylib.DrawText(
                "You killed " + gameManager.GetEnemyCount() + " enemies",
                250,
                650,
                30,
                Raylib.RED
            );

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                RestartGame();
            }
        }

        void RestartGame()
        {
            bullets.Clear();
            enemyBullets.Clear();

            foreach (Enemy enemy in enemies)
            {
                enemy.SetActivityTrue();
            }
            
            player.SetCanMove(true);
            gameManager.Reset();
            SpawnEnemies();
        }

        void EnemyShoot()
        {
            if (gameManager.IsGameOver())
            {
                return;
            }

            foreach (Enemy enemy in enemies)
            {
                if (!enemy.IsActive())
                {
                    continue;
                }

                // Shoot every 2-4 seconds
                if (Raylib.GetRandomValue(0, 1001) < 1)
                {
                    Bullet bullet = new Bullet();
                    Vector2 bulletPosition = new Vector2(
                        enemy.transform.position.X + enemy.sprite.size.X / 2,
                        enemy.transform.position.Y
                    );
                    bullet.SetActive(bulletPosition, 5.0f);
                    enemyBullets.Add(bullet);
                }
            }
            foreach (Bullet bullet in enemyBullets)
            {
                bullet.transform.position.Y += bullet.transform.velocity;
                bullet.Update();
            }
        }

        void CheckForCollisions()
        {
            // Player shooted bullets
            Collision collision = new Collision();
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.IsActive())
                {
                    continue;
                }
                foreach (Bullet bullet in bullets)
                {
                    if (!bullet.IsActive())
                    {
                        continue;
                    }
                    if (
                        collision.CheckCollision(
                            enemy.sprite.GetRectangle(),
                            bullet.sprite.GetRectangle()
                        )
                    )
                    {
                        enemy.SetActivityFalse();
                        bullet.SetActivityFalse();
                        gameManager.AddScore(10);
                        gameManager.AddEnemyCount(1);
                        gameManager.AddScoreMultiplier((float)random.NextDouble());
                        Raylib.PlaySound(hitSound);
                    }
                }
            }

            // Enemy shooted bullets
            foreach (Bullet bullet in enemyBullets)
            {
                if (!bullet.IsActive())
                {
                    continue;
                }
                if (
                    collision.CheckCollision(
                        player.sprite.GetRectangle(),
                        bullet.sprite.GetRectangle()
                    )
                )
                {
                    gameManager.RemoveHealth(1);
                    bullet.SetActivityFalse();
                    Raylib.PlaySound(hitSound);
                }
            }

            // enemies colliding with each other
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.IsActive())
                {
                    continue;
                }
                foreach (Enemy enemy2 in enemies)
                {
                    if (!enemy2.IsActive())
                    {
                        continue;
                    }
                    if (enemy == enemy2)
                    {
                        continue;
                    }
                    if (
                        collision.CheckCollision(
                            enemy.sprite.GetRectangle(),
                            enemy2.sprite.GetRectangle()
                        )
                    )
                    {
                        // enemies cant go trough each other

                        // if enemy is on the left side of enemy2   
                        if (enemy.transform.position.X < enemy2.transform.position.X)
                        {
                            enemy.transform.position.X -= 1;
                            enemy2.transform.position.X += 1;
                        }
                        else
                        {
                            enemy.transform.position.X += 1;
                            enemy2.transform.position.X -= 1;
                        }
                    }
                }
            }
        }

        void SpawnEnemies()
        {
            if (enemies.Count == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Enemy enemy = new Enemy();
                        enemy.SetActive(new Vector2(40 + j * 75, 100 + i * 75), 1.0f);
                        enemies.Add(enemy);
                    }
                }
            }
            else
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.SetActivityTrue();
                }
            }
        }

        void LoadSounds()
        {
            hitSound = Raylib.LoadSound("resources/sounds/hit.wav");
            shootSound = Raylib.LoadSound("resources/sounds/shoot.wav");
            explosionSound = Raylib.LoadSound("resources/sounds/explosion.wav");
        }

        Raylib.CloseWindow();
    }

    
}
