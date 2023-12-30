using Azure.TextAnalytics;

namespace Azure.Test
{
    public class ScanForKeyWordsShould
    {
        public string AzureKeyCredential = "[AzureKeyCredential]";

        public string Uri = "https://textanalytics[UserName].cognitiveservices.azure.com/";

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
en el 1 de septiembre de 1939, con la invasi�n alemana de Polonia, cuando Hitler
se decidi� a la incorporaci�n de una de sus reivindicaciones expansionistas m�s
delicadas: El Corredor Polaco, que implicaba la invasi�n de la mitad occidental
de Polonia; la mitad oriental, junto con Estonia, Letonia y Lituania fue ocupada
por la Uni�n Sovi�tica, mientras que Finlandia logr� mantener su independencia
de los sovi�ticos (guerra de Invierno).";

            List<string> lstResults = Functions.ScanForKeyWords(TextToAnalyze);

            Assert.NotNull(lstResults);
            Assert.NotEmpty(lstResults);
        }
    }
}