using System;
using System.Numerics;
using Raylib_CsLo;
using GameEngine6000;

namespace Avaruuspeli;

enum GameState
{
    MainMenu,
    Game,
    GameOver
}

enum Levels
{
    Level1,
    Level2
}

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
        GameState gameState = GameState.MainMenu;
        Levels level = Levels.Level1;

        int screenHeight = 900;
        int screenWidth = 800;

        Vector2 mapTileSize = new(16, 16);
        Vector2 playerTileSize = new(32, 32);
        Vector2 enemyTileSize = new(32, 32);

        string level1Path = @"./Tiled/level1.csv";

        string level2Path = @"./Tiled/level2.csv";

        string tileTexturePath = @"./Tiled/tiles/";

        string playerTexturePath = @"./Tiled/ships/ship_0001.png";

        string enemyTexturePath = @"./Tiled/ships/ship_0014.png";

        /* Creating a new instance of the Random class. */
        Random random = new Random();

        /* Creating a list of enemies, bullets and enemy bullets. */
        List<Enemy> enemies = new List<Enemy>();
        List<Bullet> bullets = new List<Bullet>();
        List<Bullet> enemyBullets = new List<Bullet>();
        List<HealthPack> healthPacks = new List<HealthPack>();

        /* Initializing the window and setting the FPS to 60. */
        Raylib.InitWindow(screenWidth, screenHeight, "Space Invaders");
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();

        /* Variables for textures */
        Texture enemyTexture = Raylib.LoadTexture(enemyTexturePath);
        Texture playerTexture = Raylib.LoadTexture(playerTexturePath);
        List<Texture> tileTextures = new List<Texture>();

        /* Creating a new instance of the Player class and the GameManager class. */
        Vector2 playerStartingPosition = new Vector2(400, 4700);
        Player player = new Player(playerStartingPosition, 4.0f, playerTexture, playerTileSize);
        GameManager gameManager = new GameManager();

        /* Variables for SFX */
        Sound shootSound;
        Sound explosionSound;
        Sound hitSound;
        Sound pickUpSound;

        Camera2D camera;
        camera.target = new(screenWidth / 2, player.transform.position.Y - 125f);
        camera.offset = new(screenWidth / 2, screenHeight / 2);
        camera.rotation = 0.0f;
        camera.zoom = 1.0f;

        /* Loading the textures for the tiles. */
        var tileTextureFiles = Directory.GetFiles(tileTexturePath, "*.png");
        foreach (var tileTextureFile in tileTextureFiles)
        {
            tileTextures.Add(Raylib.LoadTexture(tileTextureFile));
        }

        Map map = new Map(level1Path, mapTileSize, tileTextures);
        /* Calling the methods `SpawnEnemies()` and `LoadSounds()` */
        SpawnEnemies();
        SpawnHealthPacks();
        LoadSounds();

        /* A variable that is used to make the player shoot only once every 0.75 seconds. */
        float playerShootCooldown = 0.0f;

        int enemyShootMultiplier = 1;

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);
            switch (gameState)
            {
                case GameState.MainMenu:
                    MainMenu();
                    break;
                case GameState.Game:
                    Game();
                    break;
                case GameState.GameOver:
                    GameFinished();
                    break;
            }
            Raylib.EndDrawing();
        }

        /// <summary>
        /// The MainMenu function displays the game's title, instructions, input instructions, and how
        /// to play, and waits for the player to press Enter to start the game.
        /// </summary>
        void MainMenu()
        {
            // Draw the text centered
            string Title = "SPACE INVADERS";
            string Instructions = "Press Enter to start the game";
            string inputInstructions =
                "You can change input system\nbetween Keyboard and Gamepad by pressing I";
            string howToPlay =
                "How to play:\nSpace to shoot when using keyboard,\nA to shoot when using gamepad,\nWASD or D-Pad to move";

            Raylib.DrawText(
                Title,
                screenWidth / 2 - Title.Length * 5,
                screenHeight / 2 - 100,
                20,
                Raylib.WHITE
            );

            Raylib.DrawText(
                Instructions,
                screenWidth / 2 - Instructions.Length * 5,
                screenHeight / 2 - 50,
                20,
                Raylib.WHITE
            );

            Raylib.DrawText(
                inputInstructions,
                screenWidth / 2 - inputInstructions.Length * 2,
                screenHeight / 2,
                20,
                Raylib.WHITE
            );

            Raylib.DrawText(
                howToPlay,
                screenWidth / 2 - howToPlay.Length * 2,
                screenHeight / 2 + 70,
                20,
                Raylib.WHITE
            );

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                gameState = GameState.Game;
            }
        }

        /// <summary>
        /// The function displays a message and allows the player to progress to the next level or
        /// return to the main menu depending on the current level and player input.
        /// </summary>
        void GameFinished()
        {
            if (level == Levels.Level1)
            {
                Raylib.DrawText(
                    "Level 1 completed!",
                    screenWidth / 2 - 80,
                    screenHeight / 2 - 100,
                    20,
                    Raylib.WHITE
                );

                Raylib.DrawText(
                    "Press Enter to go to Level 2",
                    screenWidth / 2 - 80,
                    screenHeight / 2 - 50,
                    20,
                    Raylib.WHITE
                );

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                {
                    gameState = GameState.Game;
                    map.StartReading(level2Path, mapTileSize, tileTextures);
                    level = Levels.Level2;
                    bullets.Clear();
                    enemyBullets.Clear();
                    enemies.Clear();
                    healthPacks.Clear();
                    SpawnEnemies();
                    SpawnHealthPacks();

                    player.transform.position = playerStartingPosition;
                    return;
                }
            }

            if (level == Levels.Level2)
            {
                gameManager.GameOver(Raylib.WHITE, "Press Enter to go to Main Menu");

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                {
                    gameState = GameState.MainMenu;
                    RestartGame();
                }
            }
        }

        /// <summary>
        /// This function updates and renders the game world, including the player, enemies, bullets,
        /// and map, and checks for collisions and game progress.
        /// </summary>
        void Game()
        {
            Raylib.BeginMode2D(camera);
            camera.target = new(screenWidth / 2, player.transform.position.Y - 125f);

            /* Calling the method `Draw()` from the class `Map` */
            map.Draw();

            // Making sure that camera sides is not too high or too low
            if (camera.target.Y < screenHeight / 2)
            {
                camera.target.Y = screenHeight / 2;
            }
            else if (camera.target.Y > map.MapSize.Y - screenHeight / 2)
            {
                camera.target.Y = map.MapSize.Y - screenHeight / 2;
            }

            /* Calling the method `PlayerShoot()` */
            PlayerShoot();

            /* Checking if the key R is pressed. If it is, it calls the method `RestartGame()`. */
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
            {
                RestartGame();
            }

            // Updating the healthpacks
            foreach (HealthPack healthPack in healthPacks)
            {
                healthPack.Update();
            }

            /* Moving the bullets up. Bullets are shooted by player*/
            foreach (Bullet bullet in bullets)
            {
                if (!bullet.isActive)
                {
                    continue;
                }
                Vector2 bulletPositionInScreen = Raylib.GetWorldToScreen2D(
                    bullet.transform.position,
                    camera
                );

                if (bulletPositionInScreen.Y < 0 || bulletPositionInScreen.Y > screenHeight)
                {
                    bullet.isActive = false;
                    continue;
                }

                bullet.camera = camera;
                bullet.Update();
            }

            /* Updating the enemies. */
            foreach (Enemy enemy in enemies)
            {
                Vector2 enemyPositionInScreen = Raylib.GetWorldToScreen2D(
                    enemy.transform.position,
                    camera
                );

                // If enemy touches the top or bottom of the screen, change direction
                if (enemy.transform.position.Y > map.MapSize.Y - enemyTileSize.Y)
                {
                    enemy.moveDirection.Y = -1;
                }
                else if (enemy.transform.position.Y < 1)
                {
                    enemy.moveDirection.Y = 1;
                }

                // If enemy is not on screen, skip it
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
            PrevertPlayerFromGoingOffScreen();

            Raylib.EndMode2D();

            /* It checks if the game is over. */
            CheckGameProgress();

            /* Updating the game manager. */
            gameManager.Update();
        }

        /// <summary>
        /// This function prevents the player from going off the screen by clamping their position
        /// within the screen boundaries.
        /// </summary>
        void PrevertPlayerFromGoingOffScreen()
        {
            // Prevert player from going off camera
            player.transform.position.Y = Math.Clamp(
                player.transform.position.Y,
                0,
                map.MapSize.Y - playerTileSize.Y
            );
            player.transform.position.X = Math.Clamp(
                player.transform.position.X,
                0,
                Raylib.GetScreenWidth() - player.sprite.size.X
            );
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
            if (gameManager.Health <= 0)
            {
                if (!gameManager.IsGameOver)
                {
                    Raylib.PlaySound(explosionSound);
                    gameManager.Time = Raylib.GetTime();
                }

                GameOver();
            }

            // Check if all enemies are dead
            bool allEnemiesDead = true;
            foreach (Enemy enemy in enemies)
            {
                if (enemy.isActive)
                {
                    allEnemiesDead = false;
                }
            }

            if (allEnemiesDead)
            {
                if (player.transform.position.Y > 10)
                    return;
                gameManager.Time = Raylib.GetTime();

                gameState = GameState.GameOver;
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
                enemy.canMove = false;
            }
            gameManager.GameOver(Raylib.RED, "Press Enter to restart");
            player.canMove = false;

            if (!player.KeyboardMovement)
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
                    "Want to change to gamepad control? Press 'I'",
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
            enemies.Clear();
            healthPacks.Clear();

            player.transform.position = playerStartingPosition;
            player.canMove = true;
            gameManager.Reset();
            SpawnEnemies();
            SpawnHealthPacks();
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
                    && player.KeyboardMovement
                || Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_DOWN)
                    && playerShootCooldown <= 0
                    && !player.KeyboardMovement
            )
            {
                if (gameManager.IsGameOver)
                {
                    return;
                }

                Bullet bullet = new Bullet(mapTileSize, tileTextures[12]);
                Vector2 bulletPosition = new Vector2(
                    player.transform.position.X + playerTileSize.X / 2 - bullet.sprite.size.X / 2,
                    player.transform.position.Y
                );
                bullet.moveDirection.Y = -1;
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
            if (gameManager.IsGameOver)
            {
                return;
            }

            foreach (Enemy enemy in enemies)
            {
                if (!enemy.isActive)
                {
                    continue;
                }

                if (Raylib.GetRandomValue(0, 100) < enemyShootMultiplier)
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
                    Bullet bullet = new Bullet(mapTileSize, tileTextures[15]);
                    Vector2 bulletPosition = new Vector2(
                        enemy.transform.position.X
                            + enemy.sprite.size.X / 2
                            - bullet.sprite.size.X / 2,
                        enemy.transform.position.Y
                    );
                    bullet.moveDirection.Y = enemy.moveDirection.Y;
                    bullet.SetActive(bulletPosition, 5.0f);
                    enemyBullets.Add(bullet);
                }
            }

            /* Looping through the enemyBullets array and updating each bullet. */
            foreach (Bullet bullet in enemyBullets)
            {
                bullet.camera = camera;

                bullet.Update();
            }
        }

        /// <summary>
        /// Check for collisions between player bullets and enemies, player and enemy bullets, and
        /// enemies and enemies
        /// </summary>
        void CheckForCollisions()
        {
            Collision collision = new Collision();

            // Healthpacks
            foreach (HealthPack healthPack in healthPacks)
            {
                if (!healthPack.isActive)
                {
                    continue;
                }
                if (
                    collision.CheckCollision(
                        player.sprite.GetRectangle(),
                        healthPack.sprite.GetRectangle()
                    )
                )
                {
                    healthPack.isActive = false;
                    gameManager.Health += 1;
                    Raylib.PlaySound(pickUpSound);
                }
            }

            // Player shooted bullets
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.isActive)
                {
                    continue;
                }
                foreach (Bullet bullet in bullets)
                {
                    if (!bullet.isActive)
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
                        enemy.isActive = false;
                        bullet.isActive = false;
                        gameManager.Score += (int)(gameManager.ScoreMultiplier * 10);
                        gameManager.EnemiesDestroyed += 1;
                        gameManager.ScoreMultiplier += (float)random.NextDouble();
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
                if (!bullet.isActive)
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
                    gameManager.Health -= 1;
                    bullet.isActive = false;
                    Raylib.PlaySound(hitSound);
                }
            }

            // enemies colliding with each other
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.isActive)
                {
                    continue;
                }
                foreach (Enemy enemy2 in enemies)
                {
                    if (!enemy2.isActive)
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
                            enemy.moveDirection.X = -1;
                            enemy2.moveDirection.X = 1;
                        }
                        else
                        {
                            enemy.moveDirection.X = 1;
                            enemy2.moveDirection.X = 1;
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
                for (int i = 0; i < gameManager.MaxEnemies; i++)
                {
                    Enemy enemy = new Enemy(enemyTexture, enemyTileSize);
                    enemy.SetActive(
                        new Vector2(
                            Raylib.GetRandomValue(0, (int)map.MapSize.X - (int)enemyTileSize.X),
                            Raylib.GetRandomValue(0, (int)map.MapSize.Y - 250)
                        ),
                        1.0f
                    );
                    enemies.Add(enemy);
                    enemy.Update();
                }
            }
            else
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.isActive = true;
                }
            }
        }

        void SpawnHealthPacks()
        {
            if (healthPacks.Count == 0)
            {
                for (int i = 0; i < gameManager.MaxHealthPacks; i++)
                {
                    HealthPack healthPack = new HealthPack(mapTileSize, tileTextures[24]);
                    healthPack.transform.position = new Vector2(
                        Raylib.GetRandomValue(0, (int)map.MapSize.X - (int)mapTileSize.X),
                        Raylib.GetRandomValue(0, (int)map.MapSize.Y - 250)
                    );
                    healthPack.isActive = true;
                    healthPacks.Add(healthPack);
                }
            } else
            {
                foreach (HealthPack healthPack in healthPacks)
                {
                    healthPack.isActive = true;
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
            pickUpSound = Raylib.LoadSound("resources/sounds/powerUp.wav");
        }

        Raylib.CloseWindow();
    }
}
