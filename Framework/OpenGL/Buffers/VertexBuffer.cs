using System.Reflection;
using OpenTK.Graphics.OpenGL4;

namespace engine.Framework.OpenGL.Buffers 
{
    public abstract class VertexBuffer<T> : IDisposable where T : struct, IEquatable<T> {
        public T[] Vertices = new T[0];

        private BufferUsageHint usage;
        private int vboId;

        private static readonly int stride = (int)typeof(T).GetField("Stride", BindingFlags.Public | BindingFlags.Static)!.GetValue(typeof(T))!;
        private static readonly Action bindAttributes =
                (Action)Delegate.CreateDelegate(
                    typeof(Action),
                    null,
                    typeof(T).GetMethod("Bind", BindingFlags.Public | BindingFlags.Static)!
                );

        protected abstract BeginMode Type
        {
            get;
        }

        protected virtual int ToElements(int vertices)
        {
            return vertices;
        }

        protected virtual int ToElementIndex(int vertexIndex)
        {
            return vertexIndex;
        }

        public VertexBuffer(int amountVertices, BufferUsageHint usage) {
            this.usage = usage;
            GL.GenBuffers(1, out vboId);

            Resize(amountVertices);
        }

        public void Resize(int amountVertices)
        {
            T[] oldVertices = Vertices;
            Vertices = new T[amountVertices];

            if (oldVertices != null)
                for (int i = 0; i < oldVertices.Length && i < Vertices.Length; ++i)
                    Vertices[i] = oldVertices[i];

            if (GLWrapper.BindBuffer(BufferTarget.ArrayBuffer, vboId))
                bindAttributes();

            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertices.Length * stride), IntPtr.Zero, usage);
        }

        public void Draw()
        {
            DrawRange(0, Vertices.Length);
        }

        public void DrawRange(int startIndex, int endIndex)
        {
            Bind(true);

            int amountVertices = endIndex - startIndex;
            GL.DrawElements(Type, ToElements(amountVertices), DrawElementsType.UnsignedShort, ToElementIndex(startIndex) * sizeof(ushort));

            Unbind();
        }

        public virtual void Bind(bool forRendering)
        {
            if (GLWrapper.BindBuffer(BufferTarget.ArrayBuffer, vboId))
                bindAttributes();
        }

        public virtual void Unbind()
        {
            GLWrapper.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(vboId);
        }

        public void Update()
        {
            UpdateRange(0, Vertices.Length);
        }

        public void UpdateRange(int startIndex, int endIndex)
        {
            Bind(false);

            int amountVertices = endIndex - startIndex;
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(startIndex * stride), (IntPtr)(amountVertices * stride), ref Vertices[startIndex]);

            Unbind();
        }
    }
}