using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;

namespace engine.Framework.Window
{
    public class Window : BasicGameWindow
    {
        public override GameWindow window { get; }

        public Window(BasicGameHost host) : base(host)
        {
            window = new GameWindow(windowSettings, nativeWindowSettings);

            Initialize();
        }

        public override void OnLoad()
        {
            base.OnLoad();
        }

        public override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        public override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
        }
    }
}