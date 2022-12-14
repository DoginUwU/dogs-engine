using OpenTK.Graphics.OpenGL4;

namespace engine.Framework.OpenGL.Buffers
{
    static class QuadIndexData
    {
        static QuadIndexData()
        {
            GL.GenBuffers(1, out QuadIndexData.EboId);
        }

        public static readonly int EboId;
        public static int MaxAmountIndices;
    }

    public class QuadVertexBuffer<T> : VertexBuffer<T> where T : struct, IEquatable<T>
    {
        protected override BeginMode Type => BeginMode.Triangles;

        public QuadVertexBuffer(int amountQuads, BufferUsageHint usage)
            : base(amountQuads * 4, usage)
        {
            int amountIndices = amountQuads * 6;
            if (amountIndices > QuadIndexData.MaxAmountIndices)
            {
                ushort[] indices = new ushort[amountIndices];

                for (ushort i = 0, j = 0; j < amountIndices; i += 4, j += 6)
                {
                    indices[j] = i;
                    indices[j + 1] = (ushort)(i + 1);
                    indices[j + 2] = (ushort)(i + 3);
                    indices[j + 3] = (ushort)(i + 2);
                    indices[j + 4] = (ushort)(i + 3);
                    indices[j + 5] = (ushort)(i + 1);
                }

                GLWrapper.BindBuffer(BufferTarget.ElementArrayBuffer, QuadIndexData.EboId);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(amountIndices * sizeof(ushort)), indices, BufferUsageHint.StaticDraw);

                QuadIndexData.MaxAmountIndices = amountIndices;
            }
        }

        public override void Bind(bool forRendering)
        {
            base.Bind(forRendering);

            if (forRendering)
                GLWrapper.BindBuffer(BufferTarget.ElementArrayBuffer, QuadIndexData.EboId);
        }

        public override void Unbind()
        {
            base.Unbind();
        }

        protected override int ToElements(int vertices)
        {
            return 3 * vertices / 2;
        }

        protected override int ToElementIndex(int verticexIndex)
        {
            return 3 * verticexIndex / 2;
        }
    }
}