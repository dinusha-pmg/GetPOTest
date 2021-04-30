using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Get.Po.Tester
{
   public static class PoManager
    {
        public static async void MainAsyncFromApim(string url, int iteration)
        {
            Console.WriteLine($"Fetching iteration {iteration}");
            string authToken = GetAuthToken();
            HttpClient httpClient = new HttpClient();

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Add("Authorization", "Bearer " + authToken);
                    request.Headers.Add("Ocp-Apim-Subscription-Key", "dceaef0f1af741ca93eca8122034009c");
                    HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);
                    string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Console.WriteLine($"========Reequest No.:{iteration}.{i}============================================================");
                    Console.WriteLine(responseString);
                    Console.WriteLine();
                    Console.WriteLine($"===============================================================================================");
                }
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }

        public static async void MainAsync(string url, int iteration)
        {
            Console.WriteLine($"Fetching iteration {iteration}");
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage[] result = new HttpResponseMessage[5];
            HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false);
            string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Console.WriteLine($"========Reequest No.:{iteration}==============================================================");
            Console.WriteLine(responseString);
            Console.WriteLine();
            Console.WriteLine($"===============================================================================================");

            //return await Task.FromResult(result);
        }



        private static string GetAuthToken()
        {
            return "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Im5PbzNaRHJPRFhFSzFqS1doWHNsSFJfS1hFZyIsImtpZCI6Im5PbzNaRHJPRFhFSzFqS1doWHNsSFJfS1hFZyJ9.eyJhdWQiOiIwMDAwMDAwMi0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC80NmM1MTc4ZS1hMGY0LTRmNGQtOGM0MC05NTk4ZTNkMTE4NjAvIiwiaWF0IjoxNjE5NzYzNTU5LCJuYmYiOjE2MTk3NjM1NTksImV4cCI6MTYxOTc2NzQ1OSwiYWlvIjoiRTJaZ1lHajg1QlJ1M3ZrL2ZGZjNvNWtId3RwOUFRPT0iLCJhcHBpZCI6ImU4NjcyNmUxLWExMzEtNDFhMC05NTc2LWU5MzE1Y2E3MWUwMCIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzQ2YzUxNzhlLWEwZjQtNGY0ZC04YzQwLTk1OThlM2QxMTg2MC8iLCJvaWQiOiJiMmM5MDk2Yi0xMTRiLTRjNjQtOTU0Ny01MjBmYjEzODk1ZTYiLCJyaCI6IjAuQVFNQWpoZkZSdlNnVFUtTVFKV1k0OUVZWU9FbVotZ3hvYUJCbFhicE1WeW5IZ0FEQUFBLiIsInN1YiI6ImIyYzkwOTZiLTExNGItNGM2NC05NTQ3LTUyMGZiMTM4OTVlNiIsInRlbmFudF9yZWdpb25fc2NvcGUiOiJOQSIsInRpZCI6IjQ2YzUxNzhlLWEwZjQtNGY0ZC04YzQwLTk1OThlM2QxMTg2MCIsInV0aSI6IjBmaXdXY2c2RWt1X0JYb3A0OXhDQVEiLCJ2ZXIiOiIxLjAifQ.la48EigzZXT437i2XGG-ks5TtMDHedo7XpSnCLe8rfvZs_P4rhK5yCwZxZ884YRPs4kPVW2P9qbiW9rgxv5z6LrlqBZR98dspocZrEY32YYUuBlpo0rmae70zk4kqOjVAQzZLQZjWdhuDGzkn1kkM5BbODgRmYc_2Jw8FsaxUGy0QZXonNGpJmqMDlbw3irJy7uhNkiYT5E5G6pUuvWGHFtM8pRG7_RWfedcR9xX2mptfwKV8V4JUUbuJRh7O9skqy-O5tRwbco5HnNpjTkO6w24MR7XjN6CQloN1Oi_7S9EA3qpDFDGM7xVsQQLcmVfXxFbZGNxS8eKnLPjH-QSWA";

        }
    }
}
