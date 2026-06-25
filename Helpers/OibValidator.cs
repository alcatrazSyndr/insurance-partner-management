namespace InsurancePartnerManagement.Helpers
{
    public static class OibValidator
    {
        public static bool IsValid(string oib)
        {
            if (string.IsNullOrEmpty(oib) || oib.Length != 11)
                return false;

            if (!oib.All(char.IsDigit))
                return false;

            int remainder = 10;
            for (int i = 0; i < 10; i++)
            {
                remainder = (remainder + int.Parse(oib[i].ToString())) % 10;
                if (remainder == 0) remainder = 10;
                remainder = (remainder * 2) % 11;
            }

            int checkDigit = 11 - remainder;
            if (checkDigit == 10) checkDigit = 0;

            return checkDigit == int.Parse(oib[10].ToString());
        }
    }
}