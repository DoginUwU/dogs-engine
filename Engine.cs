using engine.Framework;

namespace engine
{
    public class Engine
    {
        public static void Main(string[] args)
        {
            Window window = new Window();
            window.Title = "Engine - OpenTK";

            window.window.Run();
        }
    }
}