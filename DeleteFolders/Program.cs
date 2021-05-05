using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace DeleteFolders
{
    class Program
    {
        private static IConfiguration _configuration;

        static void Main(string[] args)
        {
            // 設定情報取得
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();


            var path = GetBasePath();
            var searchPatterns = GetSearchPatterns();
            System.IO.SearchOption searchOption = SearchOption.AllDirectories;
            if (searchPatterns == null) return;
            foreach (var pattern in searchPatterns)
            {
                var directories =
                    Directory.EnumerateDirectories(path, pattern ?? string.Empty, searchOption);
                foreach (var item in directories)
                {
                    Directory.Delete(item, true);
                }
            }
        }
        /// <summary>
        /// 実行対象パス
        /// appsettings.jsonで指定。なければ現在の実行パス
        /// </summary>
        /// <returns></returns>
        private static string GetBasePath()
        {
            IConfigurationSection section = _configuration?.GetSection("BasePath");
            return string.IsNullOrEmpty(section?.Value) ? Directory.GetCurrentDirectory() : section.Value;
        }
        /// <summary>
        /// フォルダー取得
        /// </summary>
        /// <returns></returns>
        private static List<string> GetSearchPatterns()
        {
            IConfigurationSection section = _configuration?.GetSection("Folders");
            var searchPatterns = section?.GetChildren()?.ToList().Select(x => x?.Value).ToList();
            return searchPatterns;
        }
    }
}
