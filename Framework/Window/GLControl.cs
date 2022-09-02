using OpenTK.Graphics.ES30;

namespace engine.Framework.Window
{
    public class GLControl {
        internal Version? GLVersion;
        internal Version? GLSLVersion;

        public virtual void Initialize() {
            string version = GL.GetString(StringName.Version);
            GLVersion = new Version(version.Split(' ')[0]);
            version = GL.GetString(StringName.ShadingLanguageVersion);

            if (!string.IsNullOrEmpty(version))
            {
                GLSLVersion = new Version(version.Split(' ')[0]);
            }

            if (GLSLVersion == null)
                GLSLVersion = new Version();

            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.StencilTest);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.ScissorTest);
            
            // TODO: Create a log system
            Console.WriteLine("GL Version: " + GLVersion);
            Console.WriteLine("GLSL Version: " + GLSLVersion);
        }
    }
}