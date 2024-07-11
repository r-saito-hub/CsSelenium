using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CsSelenium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (IWebDriver driver = new EdgeDriver())
            {
                // 要素の定義
                List<string> allElementTexts = new List<string>();

                // URLのリスト
                var urls = new List<string>
                {
                    @"https://www.toyoshingo.com/wanhai/index.php?port=13&week=1",
                    @"https://www.toyoshingo.com/wanhai/index.php?port=13&week=2",
                    @"https://www.toyoshingo.com/wanhai/index.php?port=13&week=3",
                    @"https://www.toyoshingo.com/wanhai/index.php?port=13&week=4",
                    @"https://www.toyoshingo.com/wanhai/index.php?port=13&week=5"
                };

                foreach (var url in urls)
                {
                    // URLを取得
                    driver.Url = url;
                    Thread.Sleep(1000); // ページが完全に読み込まれるまで待機

                    for (int i = 2; i <= 8; i++)
                    {
                        for (int j = 1; j <= 14; j++)
                        {
                            try
                            {
                                var elements = driver.FindElements(By.XPath($"/html/body/div[1]/div/div[2]/div[2]/div[3]/div[{i}]/div[{j}]/a/span[1]"));
                                foreach (var element in elements)
                                {
                                    allElementTexts.Add(element.Text);
                                }
                            }
                            catch (NoSuchElementException)
                            {
                                // 要素が見つからなかった場合の処理
                                Console.WriteLine($"No element found for {url}, div {i}, div {j}");
                            }
                            catch (StaleElementReferenceException)
                            {
                                // 要素が無効になった場合の処理
                                Console.WriteLine($"Encountered a stale element reference for {url}, div {i}, div {j}");
                            }
                        }
                    }
                }

                // 記事のタイトルをコンソールに表示
                foreach (var text in allElementTexts)
                {
                    Console.WriteLine(text);
                }

                // ブラウザを閉じる
                driver.Quit();
            }
            Console.ReadKey();
        }
    }
}