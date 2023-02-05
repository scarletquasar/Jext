using Jint;

namespace Jext.Runtime
{
    public class JextEngine
    {
        public bool Occupied;
        public Engine Engine;

        public JextEngine(Options? options = null)
        {
            Engine = options != null ? new Engine(options) : new();
        }
    }
}
