using Google.Cloud.Language.V1;

namespace Google.NLP
{
    public class Functions
    {
        /// <summary>
        /// Estas credenciales estan fuera del repositorio de Github,
        /// para mayor seguridad
        /// </summary>
        public string GoogleAppCredentialsFilePath {  get; set; }

        public Functions(string googleAppCredentialsFilePath)
        {
            GoogleAppCredentialsFilePath = googleAppCredentialsFilePath;
        }

        /// <summary>
        /// Esta función que provee Google Cloud NLP permite extraer textos y
        /// analizar mediante una puntuación el sentimiento encontrado
        /// </summary>
        /// <param name="textToAnalyze">Texto extraido del documento a 
        /// analizar</param>
        /// <returns>Retorna una lista de textos con dos componentes,
        /// el texto analizado y la puntuación de sentimiento</returns>
        public List<string> ScanForSentiments(string textToAnalyze)
        {
            try
            {
                if (string.IsNullOrEmpty(textToAnalyze))
                {
                    throw new Exception("El parametro de entrada es nulo o vacío");
                }

                List<string> lstResults = [];

                //Set credentials
                Environment
                    .SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
                    GoogleAppCredentialsFilePath);

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

