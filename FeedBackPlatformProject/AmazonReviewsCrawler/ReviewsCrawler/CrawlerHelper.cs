using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ReviewsCrawler
{
    public class CrawlerHelper
    {
        public static void GetReviews(string asin, int? timeout)
        {
            var page = "https://www.amazon.com/product-reviews/{0}?pageNumber={1}&sortBy=recent";
            var stopPaging = false;

            for (int pageNumber = 1; pageNumber < 1000; pageNumber++)
            {
                Console.WriteLine(pageNumber);

                //if (stopPaging)
                //    break;
                //else
                //    stopPaging = true;


                if (stopPaging)
                {
                    WriteReviewText("No data on page " + (pageNumber - 1), true);
                }


                if (timeout.HasValue)
                    System.Threading.Thread.Sleep(timeout.Value * 1000);

                var responseString = string.Empty;

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");

                    try
                    {
                        responseString = client.GetStringAsync(string.Format(page, asin, pageNumber)).Result;
                    }
                    catch (Exception ex)
                    {
                        WriteReviewText(ex.ToString(), true);
                        WriteReviewText("Error on page " + pageNumber, true);
                    }
                }


                if (!string.IsNullOrEmpty(responseString))
                {
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(responseString); //string
                    //htmlDoc.Load("C:/Users/amd10/Desktop/Amazon Reviews.html"); //file

                    HtmlNode divContainer = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'review-views')]");
                    if (divContainer != null)
                    {
                        var reviewContainerList = divContainer.SelectNodes("//div[@class='a-section review']");
                        if (reviewContainerList != null && reviewContainerList.Any())
                        {
                            var list = reviewContainerList.ToList();
                            var reviewText = list[0].SelectNodes("//span[@data-hook='review-body']");
                            if (reviewText != null && reviewText.Any())
                            {
                                foreach (var item in reviewText)
                                {
                                    var text = item.InnerText;
                                    stopPaging = false;
                                    Console.WriteLine(pageNumber + " Done");

                                    WriteReviewText(text, false);
                                }                                
                            }
                        }
                    }
                }
            }
        }

        private static void WriteReviewText(string text, bool isError)
        {
            string path = "";

            if (isError)
                path = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + @"\ReviewsErrors.txt";
            else
                path = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + @"\Reviews.txt";



            if (!File.Exists(path))
            {
                using (var str = File.CreateText(path))
                {
                    str.WriteLine(text);
                    str.WriteLine();
                }
            }
            else
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
                {
                    file.WriteLine(text);
                    file.WriteLine();
                }
            }
        }
    }
}
