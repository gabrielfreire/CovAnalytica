using CovAnalytica.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Services
{
    public class GithubService : IGithubService
    {
        private HttpClient _httpClient;
        private string _githubFileUrlPrefix = "https://raw.githubusercontent.com/";
        public GithubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content", "application/json");
        }

        public async Task<string> GetFileAsStringAsync(string githubFileUrl)
        {
            if (string.IsNullOrWhiteSpace(githubFileUrl))
                throw new InvalidOperationException("Github URL cannot be empty");

            if (!githubFileUrl.Contains(_githubFileUrlPrefix))
            {
                githubFileUrl = _githubFileUrlPrefix + githubFileUrl;
            }

            try
            {
                var _result = await _httpClient.GetAsync(githubFileUrl);
                var _resultContent = await _result.Content.ReadAsStringAsync();
                
                if (_result.IsSuccessStatusCode)
                {
                    return _resultContent;
                }

                throw new InvalidOperationException(_resultContent);
            }
            catch
            {
                throw;
            }
        }
    }
}
