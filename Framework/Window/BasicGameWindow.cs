using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using engine.Framework.Graphics;
using engine.Framework.OpenGL.Drawables;

namespace engine.Framework.Window
{
    public abstract class BasicGameWindow : Drawable
    {
        public abstract GameWindow window { get; }
        public abstract GLControl GLControl { get; }
        public BasicGameHost host { get; }
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
        
        public BasicGameWindow(BasicGameHost host) {
            this.host = host;
            windowSettings = new GameWindowSettings();
            nativeWindowSettings = new NativeWindowSettings();

            nativeWindowSettings.Size = Size;
            nativeWindowSettings.Title = Title;
        }

        public void Initialize() {
            window.UpdateFrame += OnUpdateFrame;
            window.RenderFrame += OnRenderFrame;
            window.Resize += OnResize;
            window.Load += OnLoad;
            window.Unload += OnUnload;

            GLControl.Initialize();
        }

        public virtual void OnUpdateFrame(FrameEventArgs args)
        {
            
        }

        public virtual void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            UpdateSubTree();
            DrawSubTree();

            window.SwapBuffers();
        }

        public virtual void OnLoad() { 
            GL.ClearColor(1f, 1f, 1f, 1.0f);

            Add(new Box());
        }

        public virtual void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public virtual void OnResize(ResizeEventArgs e) {
            GL.Viewport(0, 0, window.Size.X, window.Size.Y);
        }
    }
}
