using engine.Framework.OpenGL.Batches;
using OpenTK.Mathematics;

namespace engine.Framework.OpenGL.Drawables
{
    public class Box
    {
        private QuadBatch<Vertex2d> quadBatch = new QuadBatch<Vertex2d>(1, 3);
        protected IVertexBatch ActiveBatch => quadBatch;

        public Box()
        {
            quadBatch.Add(new Vertex2d(){ Position = new Vector2(0, 0) });
            quadBatch.Add(new Vertex2d(){ Position = new Vector2(0, 1) });
            quadBatch.Add(new Vertex2d(){ Position = new Vector2(1, 1) });
            quadBatch.Add(new Vertex2d(){ Position = new Vector2(1, 0) });

            Draw();
        }

        public void Draw()
        {
            quadBatch.Draw();
        }
    }
}