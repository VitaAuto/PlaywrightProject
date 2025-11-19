using Microsoft.Playwright;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaywrightProject.Components
{
    public class SearchResultsComponent : BaseComponent
    {
        public SearchResultsComponent(IPage page) : base(page, page.Locator("div.search-results__items")) { }

        /// <summary>
        /// Ждёт, пока контейнер с результатами станет видимым (использует глобальный таймаут Playwright)
        /// </summary>
        public async Task WaitForResultsAsync()
        {
            await Locator.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        }

        /// <summary>
        /// Ждёт появления сообщения "нет результатов" и возвращает его текст, либо null, если не дождался (использует глобальный таймаут)
        /// </summary>
        public async Task<string?> WaitForNoResultsMessageAsync()
        {
            var locator = Page.Locator("div.search-results__exception-message.search-results--empty-result[role='alert']");
            try
            {
                await locator.WaitForAsync(new() { State = WaitForSelectorState.Visible });
                return await locator.InnerTextAsync();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Возвращает все реально загруженные элементы-результаты поиска
        /// </summary>
        public async Task<IReadOnlyList<ILocator>> GetResultItemsAsync()
        {
            var items = Locator.Locator("article.search-results__item");
            return await items.AllAsync();
        }

        /// <summary>
        /// Возвращает общее количество результатов (из заголовка)
        /// </summary>
        public async Task<int> GetTotalResultsCountAsync()
        {
            var counterLocator = Page.Locator("h2.search-results__counter");
            if (await counterLocator.IsVisibleAsync())
            {
                var counterText = await counterLocator.InnerTextAsync();
                var match = System.Text.RegularExpressions.Regex.Match(counterText, @"(\d+)");
                if (match.Success)
                    return int.Parse(match.Value);
            }
            return 0;
        }

        /// <summary>
        /// Возвращает количество реально загруженных элементов
        /// </summary>
        public async Task<int> GetLoadedResultsCountAsync()
        {
            var items = Locator.Locator("article.search-results__item");
            return await items.CountAsync();
        }

        /// <summary>
        /// Проверяет, есть ли хотя бы один загруженный результат
        /// </summary>
        public async Task<bool> HasResultsAsync()
        {
            return await GetLoadedResultsCountAsync() > 0;
        }
    }
}