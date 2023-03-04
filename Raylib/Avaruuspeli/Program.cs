namespace Avaruuspeli;

/// <summary>
/// Class <c>Invaders</c> is used to create the game.
/// </summary>
class Program
{
    /// <summary>
    /// The main function of the program.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    static void Main(string[] args)
    {
        /* It creates a new instance of the class Invaders. */
        Invaders invaders = new Invaders();

        /* Calling the Run method of the invaders object. */
        invaders.Run();
    }
}
