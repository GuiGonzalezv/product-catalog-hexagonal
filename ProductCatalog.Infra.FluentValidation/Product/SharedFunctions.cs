namespace ProductCatalog.Infra.FluentValidation.Product
{
    public static class SharedFunctions
    {
        public static bool IsValidHexString(string input)
        {
            if (input.Length != 24)
                return false;

            foreach (char c in input)
            {
                if (!Uri.IsHexDigit(c))
                    return false;
            }

            return true;
        }
    }
}
