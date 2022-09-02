using OpenTK.Graphics.ES30;

namespace engine.Framework.OpenGL
{
    public static class GLWrapper {
        private static int[] lastBoundBuffers = new int[2];
        private static int lastBoundVertexArray = 0;
        private static int lastBoundProgram = 0;
        private static int lastBoundTexture = 0;
        private static int lastBoundFramebuffer = 0;
        private static int lastBoundRenderbuffer = 0;
        private static int lastBoundSampler = 0;
        private static int lastBoundTransformFeedback = 0;

        public static bool BindBuffer(BufferTarget target, int buffer)
        {
            int bufferIndex = (int)(target - BufferTarget.ArrayBuffer);
            if (lastBoundBuffers[bufferIndex] == buffer)
                return false;

            lastBoundBuffers[bufferIndex] = buffer;

            GL.BindBuffer(target, buffer);
            
            return true;
        }

        public static bool BindVertexArray(int array)
        {
            if (lastBoundVertexArray == array)
                return false;

            lastBoundVertexArray = array;

            GL.BindVertexArray(array);

            return true;
        }

        public static bool UseProgram(int program)
        {
            if (lastBoundProgram == program)
                return false;

            lastBoundProgram = program;

            GL.UseProgram(program);

            return true;
        }

        public static bool BindTexture(TextureTarget target, int texture)
        {
            if (lastBoundTexture == texture)
                return false;

            lastBoundTexture = texture;

            GL.BindTexture(target, texture);

            return true;
        }

        public static bool BindFramebuffer(FramebufferTarget target, int framebuffer)
        {
            if (lastBoundFramebuffer == framebuffer)
                return false;

            lastBoundFramebuffer = framebuffer;

            GL.BindFramebuffer(target, framebuffer);

            return true;
        }

        public static bool BindRenderbuffer(RenderbufferTarget target, int renderbuffer)
        {
            if (lastBoundRenderbuffer == renderbuffer)
                return false;

            lastBoundRenderbuffer = renderbuffer;

            GL.BindRenderbuffer(target, renderbuffer);

            return true;
        }

        public static bool BindSampler(int unit, int sampler)
        {
            if (lastBoundSampler == sampler)
                return false;

            lastBoundSampler = sampler;

            GL.BindSampler(unit, sampler);

            return true;
        }

        public static bool BindTransformFeedback(TransformFeedbackTarget target, int transformFeedback)
        {
            if (lastBoundTransformFeedback == transformFeedback)
                return false;

            lastBoundTransformFeedback = transformFeedback;

            GL.BindTransformFeedback(target, transformFeedback);

            return true;
        }
    }
}