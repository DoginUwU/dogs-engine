using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace engine.Framework.OpenGL
{
    public static class Vertex
    {
        private static int amountEnabledAttributes = 0;
        public static void EnableAttributes(int amount)
        {
            if (amount == amountEnabledAttributes)
                return;
            else if (amount > amountEnabledAttributes)
            {
                for (int i = amountEnabledAttributes; i < amount; ++i)
                {
                    GL.EnableVertexAttribArray(i);
                }
            }
            else
            {
                for (int i = amountEnabledAttributes - 1; i >= amount; --i)
                {
                    GL.DisableVertexAttribArray(i);
                }
            }

            amountEnabledAttributes = amount;
        }
    }

    public struct Vertex2d : IEquatable<Vertex2d>
    {
        public Vector2 Position;

        private static readonly IntPtr positionOffset = Marshal.OffsetOf(typeof(Vertex2d), "Position");

        public bool Equals(Vertex2d other)
        {
            return Position.Equals(other.Position);
        }

        public static void Bind()
        {
            Vertex.EnableAttributes(1);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Stride, positionOffset);
        }

        public static readonly int Stride = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vertex2d));
    }
}