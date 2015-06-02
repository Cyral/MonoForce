using System;

namespace MonoForce.Demo
{
    /// <summary>
    /// The main class for the XNA MonoForce Demo.
    /// </summary>
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            //Create an instance of the game and run it.
            using (var game = new Central())
                game.Run();
        }
    }
}