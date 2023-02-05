using Jint;
using Jint.Native;

namespace Jext.Runtime
{
    public class JextRuntime
    {
        private readonly string _presets; 
        private readonly IDictionary<Guid, JextEngine> _engines;
        
        public JextRuntime(string presets = "")
        {
            _presets = presets;
            _engines = new Dictionary<Guid, JextEngine>();
        }

        public Guid AddEngine(Options? options = null)
        {
            var jextEngine = new JextEngine(options);
            jextEngine.Engine.Execute(_presets);
            var identifier = Guid.NewGuid();

            _engines.Add(identifier, jextEngine);

            return identifier;
        }

        public JsValue Inject(string code, Guid identifier)
        {
            var jextEngine = _engines[identifier];
            return jextEngine.Engine.Evaluate(code);
        }

        public JsValue Execute(JsValue action, Guid identifier)
        {
            var jextEngine = _engines[identifier];
            
            while (jextEngine.Occupied) { }

            jextEngine.Occupied = true;
            var result = jextEngine.Engine.Call(action);
            jextEngine.Occupied = false;

            return result;
        }
    }
}
