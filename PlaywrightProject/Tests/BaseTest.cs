using NUnit.Framework;

namespace PlaywrightProject.Tests
{
    public abstract class BaseTest
    {
        protected PlaywrightDriver? Driver;

        [SetUp]
        public async Task SetUp()
        {
            Driver = new PlaywrightDriver();
            await Driver.InitAsync(1920, 1080);
            //await Driver.InitAsync(1920, 1080);// Явно задаём размер
        }

        [TearDown]
        public async Task TearDown()
        {
            await Driver.CleanupAsync();
        }
    }
}