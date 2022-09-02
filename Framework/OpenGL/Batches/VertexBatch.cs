using engine.Framework.OpenGL.Buffers;

namespace engine.Framework.OpenGL.Batches
{
    public abstract class VertexBatch<T> : IVertexBatch where T : struct, IEquatable<T>
    {
        public List<VertexBuffer<T>> VertexBuffers = new List<VertexBuffer<T>>();
        public int Size { get; private set; }

        private int currentVertexBuffer = 0;

        private int changeBeginIndex = -1;
        private int changeEndIndex = -1;

        private int fixedBufferAmount;
        private int currentVertex = 0;
        private int lastVertex = 0;

        private VertexBuffer<T> CurrentVertexBuffer => VertexBuffers[currentVertexBuffer];

        public VertexBatch(int size, int fixedBufferAmount) {
            Size = size;

            this.fixedBufferAmount = fixedBufferAmount;
        }

        protected abstract VertexBuffer<T> CreateVertexBuffer();

        public void Add(T vertex)
        {
            while (currentVertexBuffer >= VertexBuffers.Count)
                VertexBuffers.Add(CreateVertexBuffer());

            VertexBuffer<T> vertexBuffer = CurrentVertexBuffer;

            if(vertexBuffer.Vertices[currentVertex].Equals(vertex)) {
                if (changeBeginIndex == -1)
                    changeBeginIndex = currentVertex;

                changeEndIndex = currentVertex + 1;
            }

            vertexBuffer.Vertices[currentVertex] = vertex;
            ++currentVertex;

            if (currentVertex >= vertexBuffer.Vertices.Length)
            {
                Draw();
                lastVertex = currentVertex = 0;
            }
        }

        public void Draw()
        {
            if (currentVertex == lastVertex)
                return;

            GLWrapper.SetActiveBatch(this);

            VertexBuffer<T> vertexBuffer = CurrentVertexBuffer;
            if (changeBeginIndex >= 0)
                vertexBuffer.UpdateRange(changeBeginIndex, changeEndIndex);

            vertexBuffer.DrawRange(lastVertex, currentVertex);

            currentVertexBuffer = (currentVertexBuffer + 1) % fixedBufferAmount;
            currentVertex = 0;

            lastVertex = currentVertex;
            changeBeginIndex = -1;
        }
    }
} 