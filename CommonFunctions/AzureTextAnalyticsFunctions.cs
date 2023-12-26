using Azure;
using System;
using Azure.AI.TextAnalytics;

namespace OneCore.CommonFunctions
{
    public class AzureTextAnalyticsFunctions
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("35d52c1dc04e4250a0a411a2eb0b5bb8");
        private static readonly Uri endpoint = new Uri("https://textanalyticsmatiasnovillo.cognitiveservices.azure.com/");

        public List<string> ScanForKeyWords(string textToAnalyze)
        {
			try
			{
                List<string> lstResults = [];
                var client = new TextAnalyticsClient(endpoint, credentials);

                var response = client.ExtractKeyPhrases(textToAnalyze);

                foreach (string keyphrase in response.Value)
                {
                    lstResults.Add(keyphrase);
                }

                return lstResults;
            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}
