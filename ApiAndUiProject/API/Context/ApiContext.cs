using Microsoft.Playwright;
using ApiAndUiProject.API.Models;
using RestSharp;
using Reqnroll;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace ApiAndUiProject.API.Context
{
    public class ApiContext(ScenarioContext scenarioContext)
    {
        
        public T Get<T>(string key)
        {
            return scenarioContext.ContainsKey(key) ? scenarioContext.Get<T>(key) : default!;
        }

        public void Set<T>(string key, T value)
        {
            scenarioContext.Set(value, key);
        }
    }
}