using Jext.Runtime;
using Jint;
using Xunit;

namespace Jext.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CrossEngineValueExperiment()
        {
            var a = new JextRuntime();
            var id = a.AddEngine();
            var id2 = a.AddEngine();

            a.Inject("globalThis.value = 1", id2);
            var funcA = a.Inject("function a() { globalThis.value = 2; return globalThis.value } a", id);

            var res = a.Execute(funcA, id2).AsNumber();

            Assert.Equal(2, res);
        }
    }
}