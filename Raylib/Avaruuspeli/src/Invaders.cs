using System;
using System.Numerics;
using Raylib_CsLo;
using GameEngine6000;

namespace Avaruuspeli;

///<summary>
/// Class <c>Invaders</c> is the main class of the game.
///</summary>

class Invaders
{
    ///<summary>
    /// Method <c>Run</c> is the main method of the game.
    ///</summary>
    public void Run()
    {
        int screenHeight = 900;
        int screenWidth = 800;

        /* Creating a new instance of the Random class. */
        Random random = new Random();

        /* Creating a list of enemies, bullets and enemy bullets. */
        List<Enemy> enemies = new List<Enemy>();
        List<Bullet> bullets = new List<Bullet>();
        List<Bullet> enemyBullets = new List<Bullet>();

        /* Initializing the window and setting the FPS to 60. */
        Raylib.InitWindow(screenWidth, screenHeight, "Space Invaders");
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();

        /* Creating a new instance of the Player class and the GameManager class. */
        Player player = new Player(new Vector2(400, 830), 7.0f);
        GameManager gameManager = new GameManager();

        /* Variables for SFX */
        Sound shootSound;
        Sound explosionSound;
        Sound hitSound;

        Camera2D camera;
        camera.target = new(screenWidth / 2, player.transform.position.Y - 125f);
        camera.offset = new(screenWidth / 2, screenHeight / 2);
        camera.rotation = 0.0f;
        camera.zoom = 1.0f;

        /* Variables for textures */
        Texture enemyTexture = Raylib.LoadTexture("./resources/textures/hjallis.png");

        /* A variable that is used to make the player shoot only once every 0.75 seconds. */
        float playerShootCooldown = 0.0f;

        int enemyShootMultiplier = 1;

        /* Calling the methods `SpawnEnemies()` and `LoadSounds()` */
        SpawnEnemies();
        LoadSounds();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);
            Raylib.BeginMode2D(camera);
            camera.target = new(screenWidth / 2, player.transform.position.Y - 125f);

            /* Calling the method `PlayerShoot()` */
            PlayerShoot();

            /* Checking if the key R is pressed. If it is, it calls the method `RestartGame()`. */
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
            {
                RestartGame();
            }

            /* Moving the bullets up. Bullets are shooted by player*/
            foreach (Bullet bullet in bullets)
            {
                if (!bullet.IsActive)
                {
                    continue;
                }
                Vector2 bulletPositionInScreen = Raylib.GetWorldToScreen2D(
                    bullet.transform.position,
                    camera
                );

                if (bulletPositionInScreen.Y < -1000 || bulletPositionInScreen.Y > screenHeight)
                {
                    bullet.SetActivityFalse();
                    continue;
                }

                bullet.camera = camera;
                bullet.transform.position.Y -= bullet.transform.velocity;
                bullet.Update();
            }

            /* Updating the enemies. */
            foreach (Enemy enemy in enemies)
            {
                Vector2 enemyPositionInScreen = Raylib.GetWorldToScreen2D(
                    enemy.transform.position,
                    camera
                );

                if (enemyPositionInScreen.Y < 0 || enemyPositionInScreen.Y > screenHeight)
                {
                    continue;
                }
                enemy.Update();
            }

            /* Checking for collisions between the player and the enemies. */
            CheckForCollisions();

            /* Calling the method `EnemyShoot()`. */
            EnemyShoot();

            /* Updating the player. */
            player.Update();

            Raylib.EndMode2D();

            /* It checks if the game is over. */
            CheckGameProgress();

            /* Updating the game manager. */
            gameManager.Update();

            Raylib.EndDrawing();
        }

        ///<summary>
        /// Method <c>CheckGameProgress</c> checks if the game is over or all the enemies are dead.
        ///</summary>
        ///<remarks>
        /// If the game is over, it calls the method `GameOver()`.
        /// If all the enemies are dead, it calls the method `SpawnEnemies()`.
        ///</remarks>

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

        ///<summary>
        /// Method <c>GameOver</c> is called when the game is over.
        ///</summary>
        ///<remarks>
        /// It sets the game over to true, stops the enemies from moving and stops the player from moving.
        /// It also draws the text "Game Over!", "Press Enter to restart", Players score, how long the player was alive and how many enemies player killed.
        /// If the key Enter is pressed, it calls the method `RestartGame()`.
        ///</remarks>
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
            if (player.GetKeyboardMovement())
            {
                Raylib.DrawText(
                    "Want to change to keyboard control? Press 'I'",
                    40,
                    700,
                    30,
                    Raylib.RED
                );
            }
            else
            {
                Raylib.DrawText(
                    "Want to change to mouse control? Press 'I'",
                    70,
                    700,
                    30,
                    Raylib.RED
                );
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                RestartGame();
            }
        }

        /// <summary>
        /// <c>RestartGame()</c> is a function that resets the game to its original state
        /// </summary>
        void RestartGame()
        {
            bullets.Clear();
            enemyBullets.Clear();

            foreach (Enemy enemy in enemies)
            {
                enemy.SetCanMove(true);
                enemy.SetActivityTrue();
            }

            player.SetCanMove(true);
            gameManager.Reset();
            SpawnEnemies();
        }

        /// <summary>
        /// If the space bar is pressed and the player's shoot cooldown is less than or equal to 0, then
        /// create a new bullet, set its position, and add it to the list of bullets
        /// </summary>
        void PlayerShoot()
        {
            if (
                Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)
                    && playerShootCooldown <= 0
                    && player.GetKeyboardMovement()
                || Raylib.IsMouseButtonPressed(0)
                    && playerShootCooldown <= 0
                    && !player.GetKeyboardMovement()
            )
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
        }

        /// <summary>
        /// This function checks if the game is over, if it isn't, it loops through the enemies and
        /// checks if they are active, if they are, it checks if the random value is less than the
        /// enemyShootMultiplier, if it is, it creates a new bullet, sets the bullet's position, and
        /// adds it to the enemyBullets list
        /// </summary>
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

                if (Raylib.GetRandomValue(0, 1001) < enemyShootMultiplier)
                {
                    Vector2 enemyPositionInScreen = Raylib.GetWorldToScreen2D(
                        enemy.transform.position,
                        camera
                    );
                    // If enemy is not in the screen, don't shoot
                    if (
                        enemyPositionInScreen.X < 0
                        || enemyPositionInScreen.X > Raylib.GetScreenWidth()
                        || enemyPositionInScreen.Y < 0
                        || enemyPositionInScreen.Y > Raylib.GetScreenHeight()
                    )
                    {
                        continue;
                    }
                    Bullet bullet = new Bullet();
                    Vector2 bulletPosition = new Vector2(
                        enemy.transform.position.X + enemy.sprite.size.X / 2,
                        enemy.transform.position.Y
                    );
                    bullet.SetActive(bulletPosition, 5.0f);
                    enemyBullets.Add(bullet);
                }
            }

            /* Looping through the enemyBullets array and updating each bullet. */
            foreach (Bullet bullet in enemyBullets)
            {
                bullet.camera = camera;

                bullet.transform.position.Y += bullet.transform.velocity;
                bullet.Update();
            }
        }

        /// <summary>
        /// Check for collisions between player bullets and enemies, player and enemy bullets, and
        /// enemies and enemies
        /// </summary>
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
                    if (!bullet.IsActive)
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

                        if (Raylib.GetRandomValue(0, 500) < 1)
                        {
                            enemyShootMultiplier += 1;
                        }
                    }
                }
            }

            // Enemy shooted bullets
            foreach (Bullet bullet in enemyBullets)
            {
                if (!bullet.IsActive)
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

        /// <summary>
        /// If there are no enemies, spawn them. If there are enemies, make them active
        /// </summary>
        void SpawnEnemies()
        {
            if (enemies.Count == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Enemy enemy = new Enemy(enemyTexture);
                        enemy.SetActive(new Vector2(40 + j * 75, 130 + i * 75), 1.0f);
                        enemies.Add(enemy);
                        enemy.Update();
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

        /// <summary>
        /// Loads the sounds from the resources folder
        /// </summary>
        void LoadSounds()
        {
            hitSound = Raylib.LoadSound("resources/sounds/hit.wav");
            shootSound = Raylib.LoadSound("resources/sounds/shoot.wav");
            explosionSound = Raylib.LoadSound("resources/sounds/explosion.wav");
        }

        Raylib.CloseWindow();
    }
}
