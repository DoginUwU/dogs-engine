using engine.Framework.Graphics;
using engine.Framework.OpenGL.Batches;
using OpenTK.Mathematics;

namespace engine.Framework.OpenGL.Drawables
{
    public class Box : Drawable
    {
        private QuadBatch<Vertex2d> quadBatch = new QuadBatch<Vertex2d>(1, 3);
        protected override IVertexBatch ActiveBatch => quadBatch;

        protected override void Draw()
        {
            base.Draw();

            quadBatch.Add(new Vertex2d(){ Position = new Vector2(0, 0) });
            quadBatch.Add(new Vertex2d(){ Position = new Vector2(0, 1) });
            quadBatch.Add(new Vertex2d(){ Position = new Vector2(1, 1) });
            quadBatch.Add(new Vertex2d(){ Position = new Vector2(1, 0) });
            quadBatch.Draw();
        }
    }
}