using System;
using Microsoft.Xna.Framework;

namespace MonoForce.Demo
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            //Create an instance of the game and run it.
            using (var game = new Game1())
                game.Run();
        }
    }
}