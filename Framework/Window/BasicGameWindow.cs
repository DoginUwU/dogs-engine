using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;
using OpenTK.Graphics.ES30;

namespace engine.Framework.Window
{
    public abstract class BasicGameWindow : GLControl
    {
        public abstract GameWindow window { get; }
        public GameWindowSettings windowSettings { get; set; }
        public NativeWindowSettings nativeWindowSettings { get; set; }

        private string title = "Window";
        public string Title
        {
            get { return title; }
            set
            {
                if (value == null || title == value)
                    return;

                title = value;
                window.Title = title;
            }
        }

        public Vector2i size = new Vector2i(800, 600);
        public Vector2i Size
        {
            get { return size; }
            set {
                window.Size = value;
                size = value;
            }
        }
        
        public BasicGameWindow() {
            windowSettings = new GameWindowSettings();
            nativeWindowSettings = new NativeWindowSettings();

            nativeWindowSettings.Size = Size;
            nativeWindowSettings.Title = Title;
        }

        public override void Initialize() {
            window.UpdateFrame += OnUpdateFrame;
            window.Load += OnLoad;
            window.Resize += OnResize;

            base.Initialize();
        }

        public virtual void OnUpdateFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            window.SwapBuffers();
        }

        public virtual void OnLoad() { 
            GL.ClearColor(0f, 0f, 0f, 1.0f);
        }

        public virtual void OnResize(ResizeEventArgs e) {
            GL.Viewport(0, 0, window.Size.X, window.Size.Y);
        }
    }
}
