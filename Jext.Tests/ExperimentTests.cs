using Jext.Runtime;
using Jint;
using Xunit;

namespace Jext.Tests
{
    public class ExperimentTests
    {
        [Fact]
        public void CrossEngineValueAssignmentExperiment()
        {
            // Creating the experiments independently

            var jextRuntime = new JextRuntime();
            var engineBaseId = jextRuntime.AddEngine();
            var engineTargetId = jextRuntime.AddEngine();

            /* Injecting a global value in the target engine and creating
               an assignment function in the base engine */

            jextRuntime.Inject("globalThis.value = 1", engineTargetId);

            var baseFunction = jextRuntime.Inject(
                "function a() { globalThis.value = 2; return globalThis.value } a", 
                engineBaseId);

            /* Calling the assignment function in the target engine from 
               the base engine */

            var targetInjectionResult = jextRuntime
                .Execute(baseFunction, engineTargetId)
                .AsNumber();

            Assert.Equal(2, targetInjectionResult);
        }
    }
}