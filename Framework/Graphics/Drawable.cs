using System.Diagnostics.CodeAnalysis;
using engine.Framework.Lists;
using engine.Framework.OpenGL;
using engine.Framework.OpenGL.Batches;
using OpenTK.Graphics.OpenGL4;

namespace engine.Framework.Graphics
{
    public partial class Drawable : IDisposable, IHasLifetime
    {
        protected Engine Engine;

        private LifetimeList<Drawable> internalChildren;
        public ReadOnlyList<Drawable> Children
        {
            get
            {
                return internalChildren!;
            }
        }

        private Queue<Drawable> depthChangeQueue = new Queue<Drawable>();

        private float depth;
        public float Depth
        {
            get { return depth; }
            set
            {
                if (depth == value)
                    return;
                depth = value;

                Parent?.depthChangeQueue.Enqueue(this);
            }
        }

        public Drawable? Parent { get; private set; }

        protected virtual IComparer<Drawable> DepthComparerV => new DepthComparer();

        private BlendingFactorSrc blendingSrc = BlendingFactorSrc.SrcAlpha;
        private BlendingFactorDest blendingDst = BlendingFactorDest.OneMinusSrcAlpha;

        protected virtual IVertexBatch ActiveBatch => Parent?.ActiveBatch!;

        public double LifetimeStart => throw new NotImplementedException();

        public double LifetimeEnd => throw new NotImplementedException();

        public bool IsAlive => true;

        private bool loaded;
        public virtual bool LoadRequired => !loaded;

        public bool RemoveWhenNotAlive => throw new NotImplementedException();
        
        public Drawable() {
            internalChildren = new LifetimeList<Drawable>(DepthComparerV);
            loaded = true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        protected virtual Drawable? Add(Drawable drawable)
        {
            if (drawable == null)
                return null;

            drawable.changeParent(this);

            internalChildren.Add(drawable);

            return drawable;
        }

        private void changeParent(Drawable parent)
        {
            if (Parent == parent)
                return;

            Parent?.Remove(this, false);
            Parent = parent;

            changeRoot(Parent?.Engine);
        }

        private void changeRoot(Engine? root)
        {
            if (root == null) return;

            Engine = root;

            Children.ForEach(c => c.changeRoot(root));
        }

        protected bool Remove(Drawable p, bool dispose = true)
        {
            if (p == null)
                return false;

            bool result = internalChildren.Remove(p);

            p.Parent = null;

            return result;
        }

        internal virtual void UpdateSubTree()
        {

            Update();

            UpdateDepthChanges();

            internalChildren.Update(0);

            foreach (Drawable child in internalChildren.Current)
                child.UpdateSubTree();

            UpdateLayout();
        }

        protected virtual void UpdateLayout()
        {

        }

        protected void DrawSubTree()
        {
            UpdateDepthChanges();

            PreDraw();

            GLWrapper.SetBlend(blendingSrc, blendingDst);

            Draw();

            UpdateDepthChanges();

            foreach (Drawable child in internalChildren.Current)
                child.DrawSubTree();

            PostDraw();

            ActiveBatch?.Draw();
        }

        private void UpdateDepthChanges()
        {
            while (depthChangeQueue.Count > 0)
            {
                Drawable childToResort = depthChangeQueue.Dequeue();

                internalChildren.Remove(childToResort);
                internalChildren.Add(childToResort);
            }
        }

        protected virtual void PreDraw()
        {
        }

        protected virtual void Draw()
        {
        }

        protected virtual void PostDraw()
        {
        }

        protected virtual void Update()
        {
        }

        public class DepthComparer : IComparer<Drawable>
        {
            public int Compare([AllowNull] Drawable x, [AllowNull] Drawable y)
            {
                if(x == null && y == null)
                    return 0;

                if (x!.Depth == y!.Depth) return -1;
                return x.Depth.CompareTo(y.Depth);
            }
        }
    }
}