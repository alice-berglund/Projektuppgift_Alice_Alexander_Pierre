using System.Text.RegularExpressions;

namespace Logic.Services
{
    public static class StringExtension
    {
        public static bool ValidEmail(this string input)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(input);

            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
