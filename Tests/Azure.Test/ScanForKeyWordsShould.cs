using Azure.TextAnalytics;

namespace Azure.Test
{
    public class ScanForKeyWordsShould
    {
        public string AzureKeyCredential = "35d52c1dc04e4250a0a411a2eb0b5bb8";

        public string Uri = "https://textanalyticsmatiasnovillo.cognitiveservices.azure.com/";

        [Fact]
        public void ThrowWhenTextToAnalyzeIsNull()
        {
            Functions Functions = new(AzureKeyCredential, Uri);


            Assert.ThrowsAny<Exception>(() => { Functions.ScanForKeyWords(null); });
        }

        [Fact]
        public void ThrowWhenTextToAnalyzeIsEmpty()
        {
            Functions Functions = new(AzureKeyCredential, Uri);


            Assert.ThrowsAny<Exception>(() => { Functions.ScanForKeyWords(""); });
        }

        [Fact]
        public void AddTextToAnalyze()
        {
            Functions Functions = new(AzureKeyCredential, Uri);

            string TextToAnalyze = $@"El comienzo del conflicto se suele situar
en el 1 de septiembre de 1939, con la invasión alemana de Polonia, cuando Hitler
se decidió a la incorporación de una de sus reivindicaciones expansionistas más
delicadas: El Corredor Polaco, que implicaba la invasión de la mitad occidental
de Polonia; la mitad oriental, junto con Estonia, Letonia y Lituania fue ocupada
por la Unión Soviética, mientras que Finlandia logró mantener su independencia
de los soviéticos (guerra de Invierno).";

            List<string> lstResults = Functions.ScanForKeyWords(TextToAnalyze);

            Assert.NotNull(lstResults);
            Assert.NotEmpty(lstResults);
        }
    }
}