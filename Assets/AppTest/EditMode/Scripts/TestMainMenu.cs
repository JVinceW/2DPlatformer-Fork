using NUnit.Framework;

namespace AppTest.EditMode.Scripts {
    public class TestMainMenu {
        
        private int _sampleTest = 0;

        [SetUp]
        public void InitTest() {
            _sampleTest = 10;
        }

        [TearDown]
        public void FinishedTest() {
            _sampleTest = 0;
        }

        [Test]
        public void TestMainMenuSimplePasses() {
            int actual = 5;
            Assert.That(actual == _sampleTest);
        }
        
        [Test]
        public void TestMainMenuSimplePasses1() {
            int actual = 10;
            Assert.That(actual == _sampleTest);
        }
    }
}