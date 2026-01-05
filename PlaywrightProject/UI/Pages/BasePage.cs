using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightProject.UI.Pages
{
    public abstract class BasePage
    {
        protected readonly IPage Page;

        protected BasePage(IPage page)
        {
            Page = page;
        }

        public abstract string Url { get; }

        public async Task OpenAsync()
        {
            await Page.GotoAsync(Url);
        }

        public async Task<string> GetTitleAsync()
        {
            return await Page.TitleAsync();
        }

        public async Task WaitForLoadAsync()
        {
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        // Новый метод: ожидание перехода по URL (шаблон поддерживает wildcard)
        public async Task WaitForUrlAsync(string urlPattern)
        {
            await Page.WaitForURLAsync(urlPattern);
        }

        // Новый метод: получить текущий URL страницы
        public string GetCurrentUrl()
        {
            return Page.Url;
        }
    }
}