using Google.NLP;

namespace Google.Test
{
    public class ScanForSentimentsShould
    {
        public string GoogleAppCredentialsFilePath = "[GoogleAppCredentialsFilePath].json";

        [Fact]
        public void ThrowWhenTextToAnalyzeIsNull()
        {
            Functions Functions = new(GoogleAppCredentialsFilePath);
            

            Assert.ThrowsAny<Exception>(() => { Functions.ScanForSentiments(null); });
        }

        [Fact]
        public void ThrowWhenTextToAnalyzeIsEmpty()
        {
            Functions Functions = new(GoogleAppCredentialsFilePath);

            string TextToAnalyze = $@"El comienzo del conflicto se suele situar
en el 1 de septiembre de 1939, con la invasión alemana de Polonia, cuando Hitler
se decidió a la incorporación de una de sus reivindicaciones expansionistas más
delicadas: El Corredor Polaco, que implicaba la invasión de la mitad occidental
de Polonia; la mitad oriental, junto con Estonia, Letonia y Lituania fue ocupada
por la Unión Soviética, mientras que Finlandia logró mantener su independencia
de los soviéticos (guerra de Invierno).";

            Assert.ThrowsAny<Exception>(() => { Functions.ScanForSentiments(""); });
        }

        [Fact]
        public void AddTextToAnalyze()
        {
            Functions Functions = new(GoogleAppCredentialsFilePath);

            string TextToAnalyze = $@"El comienzo del conflicto se suele situar
en el 1 de septiembre de 1939, con la invasión alemana de Polonia, cuando Hitler
se decidió a la incorporación de una de sus reivindicaciones expansionistas más
delicadas: El Corredor Polaco, que implicaba la invasión de la mitad occidental
de Polonia; la mitad oriental, junto con Estonia, Letonia y Lituania fue ocupada
por la Unión Soviética, mientras que Finlandia logró mantener su independencia
de los soviéticos (guerra de Invierno).";

            List<string> lstResults = Functions.ScanForSentiments(TextToAnalyze);

            Assert.NotNull(lstResults);
            Assert.NotEmpty(lstResults);
        }
    }
}