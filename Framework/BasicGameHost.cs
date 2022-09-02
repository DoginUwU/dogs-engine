using engine.Framework.Window;

namespace engine.Framework
{
    public class BasicGameHost {
        private BasicGameWindow window;
        
        public BasicGameHost () {
            window = new engine.Framework.Window.Window(this);
            window.Title = "Engine - OpenTK";
            
            window.window.Run();
        }
    }
}