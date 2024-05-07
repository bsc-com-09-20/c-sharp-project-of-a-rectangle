 using OpenTK.Graphics.OpenGL4;
 namespace Hello {
    public class Program{
        [Obsolete]
        static void Main(string[]args)
        {
            using (Game game = new Game(800, 600, "Rhis is crazy"))
            {
                game.Run();
            }
        }
    }
 }