using engine.Framework.OpenGL.Buffers;
using OpenTK.Graphics.OpenGL4;

namespace engine.Framework.OpenGL.Batches
{
    public class QuadBatch<T> : VertexBatch<T> where T : struct, IEquatable<T>
    {
        public QuadBatch(int size, int fixedBufferAmount)
            : base(size, fixedBufferAmount)
        {
        }

        protected override VertexBuffer<T> CreateVertexBuffer()
        {
            return new QuadVertexBuffer<T>(Size, BufferUsageHint.DynamicDraw);
        }
    }
}
