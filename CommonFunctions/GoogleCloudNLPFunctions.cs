using Google.Cloud.Language.V1;

namespace OneCore.CommonFunctions
{
    public class GoogleCloudNLPFunctions
    {
        public string GoogleAppCredentialsFile = "C:\\FiyiStack\\Test\\AWSTextract\\lustrous-router-409218-e98c8c9c38a8.json";
        public List<string> ScanForSentiments(string textToAnalyze)
        {
            try
            {
                List<string> lstResults = [];

                //Set credentials
                Environment
                    .SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", 
                    GoogleAppCredentialsFile);

                var client = LanguageServiceClient.Create();

                //Analyze sentiments for the given text
                var response = client.AnalyzeSentiment(new Document()
                {
                    Content = textToAnalyze,
                    Type = Document.Types.Type.PlainText
                });

                //Take the results
                var sentences = response.Sentences;

                foreach (var sentence in sentences)
                {
                    lstResults.Add($@"<p><b>Texto analizado:</b></p> 
                                    <p>{sentence.Text.Content}</p> 
                                    <p><b>Puntuación de sentimiento:</b></p> 
                                    <p>{sentence.Sentiment.Score}</p>");
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
