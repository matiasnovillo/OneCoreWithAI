using Azure.AI.TextAnalytics;

namespace Azure.TextAnalytics
{
    public class Functions
    {
        /// <summary>
        /// Este objeto es utilizado para proveer las credenciales correctas
        /// a la plataforma Azure
        /// </summary>
        private AzureKeyCredential AzureKeyCredential { get; set; }

        /// <summary>
        /// Esta es la URL a la cual se conecta la aplicación para enviar
        /// el texto a analizar
        /// </summary>
        private Uri Uri { get; set; }

        public Functions(string azureKeyCredential, string uri)
        {
            AzureKeyCredential = new(azureKeyCredential);
            Uri = new(uri);
        }

        /// <summary>
        /// Esta función que provee Azure permite extraer las palabras claves
        /// de un texto particular
        /// </summary>
        /// <param name="textToAnalyze">Texto extraido del documento a 
        /// analizar</param>
        /// <returns>Lo que retorna es una lista de palabras claves</returns>
        public List<string> ScanForKeyWords(string textToAnalyze)
        {
            try
            {
                if (string.IsNullOrEmpty(textToAnalyze))
                {
                    throw new Exception("El texto a analizar es nulo o vacío");
                }

                List<string> lstResults = [];
                var client = new TextAnalyticsClient(Uri, AzureKeyCredential);

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
