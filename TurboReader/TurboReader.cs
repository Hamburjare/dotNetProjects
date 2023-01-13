using System.Text.RegularExpressions; // Regex

/// <summary>
/// TurboReader class contains methods for checking the type of input string.
/// </summary>
namespace TurboReader
{
    public class TurboReader
    {
        /// <summary>
        /// CheckIfOnlyLetters method checks if the input string contains only letters.
        /// </summary>
        /// <param name="input">The input string to be checked.</param>
        /// <returns>True if the input string contains only letters, False otherwise.</returns>
        public bool CheckIfOnlyLetters(string input)
        {
            if (Regex.IsMatch(input, @"\d"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// CheckIfNumber method checks if the input string can be parsed as an integer.
        /// </summary>
        /// <param name="input">The input string to be checked.</param>
        /// <returns>True if the input string can be parsed as an integer, False otherwise.</returns>
        public bool CheckIfNumber(string input)
        {
            int number;
            if (!int.TryParse(input, out number))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// CheckIfFloat method checks if the input string can be parsed as a float.
        /// </summary>
        /// <param name="input">The input string to be checked.</param>
        /// <returns>True if the input string can be parsed as a float, False otherwise.</returns>
        public bool CheckIfFloat(string input)
        {
            float number;
            if (!float.TryParse(input, out number))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// CheckIfDouble method checks if the input string can be parsed as a double.
        /// </summary>
        /// <param name="input">The input string to be checked.</param>
        /// <returns>True if the input string can be parsed as a double, False otherwise.</returns>
        public bool CheckIfDouble(string input)
        {
            double number;
            if (!double.TryParse(input, out number))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// CheckIfDecimal method checks if the input string can be parsed as a decimal.
        /// </summary>
        /// <param name="input">The input string to be checked.</param>
        /// <returns>True if the input string can be parsed as a decimal, False otherwise.</returns>
        public bool CheckIfDecimal(string input)
        {
            decimal number;
            if (!decimal.TryParse(input, out number))
            {
                return false;
            }
            return true;
        }
    }
}
