//using NUnit.Framework;
//using FluentAssertions;
//using System.Threading.Tasks;
//using PlaywrightProject.Pages;

//namespace PlaywrightProject.Tests
//{
//    public class MainPageTests : BaseTest
//    {
//        private MainPage _mainPage;

//        [SetUp]
//        public async Task TestSetUp()
//        {
//            _mainPage = new MainPage(Driver.Page);
//        }

//        [Test]
//        public async Task HamburgerMenuButton_ShouldExpandAndShowServicesLink()
//        {
//            await _mainPage.OpenAsync();

//            var hamburgerButton = await _mainPage.GetHamburgerMenuButtonAsync();
//            hamburgerButton.Should().NotBeNull();

//            await hamburgerButton.ClickAsync();

//            var ariaExpanded = await hamburgerButton.GetAttributeAsync("aria-expanded");
//            ariaExpanded.Should().Be("true");

//            var servicesLink = await _mainPage.GetHamburgerMenuServicesLinkAsync();
//            servicesLink.Should().NotBeNull();

//            var linkText = await servicesLink.InnerTextAsync();
//            linkText.Should().Contain("Services");
//        }

//        [Test]
//        public async Task HamburgerMenuButton_ShouldExpandAndNavigateToServices()
//        {
//            await _mainPage.OpenAsync();

//            var hamburgerButton = await _mainPage.GetHamburgerMenuButtonAsync();
//            hamburgerButton.Should().NotBeNull();

//            await hamburgerButton.ClickAsync();

//            var ariaExpanded = await hamburgerButton.GetAttributeAsync("aria-expanded");
//            ariaExpanded.Should().Be("true");

//            var servicesLink = await _mainPage.GetHamburgerMenuServicesLinkAsync();
//            servicesLink.Should().NotBeNull();

//            // Клик по ссылке "Services"
//            await servicesLink.ClickAsync();

//            // Ожидание перехода на страницу сервисов через обёртку
//            await _mainPage.WaitForUrlAsync("**/services");

//            // Проверка, что URL действительно содержит "/services" через обёртку
//            _mainPage.GetCurrentUrl().Should().Contain("/services");
//        }
//    }
//}