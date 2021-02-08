using NUnit.Framework;
using SimplePollingTest;

namespace SimplePollingTests
{
    public class SimplePollingTests
    {
        [Test]
        public void TestSimplePolling_GetSet_Success()
        {
            var polling = new SimplePolling();

            polling.Set("Test");

            var result = polling.TryGet();

            Assert.AreEqual("Test", result);
        }

        [Test]
        public void TestSimplePolling_GetNeverSet_ReturnsNull()
        {
            var polling = new SimplePolling();

            var result = polling.TryGet();

            Assert.IsNull(result);
        }

        [Test]
        public void TestSimplePolling_SetGet_And_Then_Get_ReturnsNull()
        {
            var polling = new SimplePolling();

            polling.Set("Test");

            var result = polling.TryGet();

            Assert.AreEqual("Test", result);

            var anotherAttempt = polling.TryGet();

            Assert.IsNull(anotherAttempt);
        }
    }
}
