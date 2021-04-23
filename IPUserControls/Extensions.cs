using System.Text.RegularExpressions;

namespace IPUserControls
{
    /// <summary>
    /// string extensions for validating if a string is a number and to
    /// remove chars like whitespace, leading zeros etc.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Removes any space in any place of the string.
        /// Does not remove tabs, newlines etc. only space chars.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>String that guarantees to not have space characters.</returns>
        public static string RemoveWhitespace(this string input)
        {
            return input.Replace(" ", "");
        }

        /// <summary>
        /// Removes any leading zeros in any string that is longer than 1.
        /// Does not check whether the input is a valid number or a valid byte.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>String that guarantees to not start with "0".</returns>
        public static string RemoveLeadingZerosInByte(this string input)
        {
            while (input.StartsWith("0") && input.Length > 1)
                input = input.Remove(0, 1);
            return input;
        }

        /// <summary>
        /// Removes any "+" signs and "-" signs from the string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>String without the number signs.</returns>
        public static string RemoveNumberSigns(this string input)
        {
            return input.Replace("+", "").Replace("-", "");
        }

        /// <summary>
        /// Validates if a string is an integer.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True if string is an int.</returns>
        public static bool IsNumber(this string input)
        {
            return !string.IsNullOrWhiteSpace(input) && int.TryParse(input, out _);
        }

        //public bool IsNumber(this string input)
        //{
        //    return new Regex("^[0-9]+$").IsMatch(input);
        //}

        /// <summary>
        /// Validates if a string is a byte.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True if string is a byte.</returns>
        public static bool IsByte(this string input)
        {
            return !string.IsNullOrWhiteSpace(input) && byte.TryParse(input, out _);
        }

        /// <summary>
        /// Validates if a string is a ushort (UInt16).
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True if string is ushort. </returns>
        public static bool IsUShort(this string input)
        {
            if (input.Length > 5) return false;
            if (!input.IsNumber()) return false;
            return uint.Parse(input) <= 65535;
        }

        /// <summary>
        /// Converts a string to a byte. If the string is an invalid number
        /// or bigger than a byte, the result returned is 0.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Byte value of the string.</returns>
        public static byte StringToByte(this string input)
        {
            return byte.TryParse(input, out var result) ? result : (byte)0;
        }

        /// <summary>
        /// Does not validate leading 0's
        /// </summary>
        private static readonly Regex ValidIpRegex = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

        /// <summary>
        /// Validates a string against a valid IP format. Does not validate
        /// leading zeros.
        /// </summary>
        /// <param name="value"></param>
        /// <example>
        /// 192.000.001.011 is a valid IP the way 192.0.1.11 would be. The same way
        /// 0.0.0.0 and 00.000.0.000 is valid. To avoid this weirdness, RemoveLeadingZeros()
        /// before calling this validation.
        /// </example>
        /// <returns>True if the string is a valid IP</returns>
        public static bool IsValidIpAddress(this string value)
        {
            return ValidIpRegex.IsMatch(value);
        }
    }
}