namespace MedarbejderIDLibrary
{
    public class MedarbejderIDGenerator
    {
        public static string GenerateMedarbejderID(string fornavn, string efternavn)
        {
            Random random = new Random();
            int randomTal = random.Next(0, 10000); 

            string forkortetFornavn = fornavn.Length >= 4 ? fornavn.Substring(0, 4) : fornavn;
            string forkortetEfternavn = efternavn.Length >= 4 ? efternavn.Substring(0, 4) : efternavn;

            return $"{forkortetFornavn.ToUpper()}{forkortetEfternavn.ToUpper()}{randomTal:D4}";
        }
    }
}
