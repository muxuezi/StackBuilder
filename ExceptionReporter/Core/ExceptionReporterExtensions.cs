using System.Text;

namespace ExceptionReporting.Extensions
{
	/// <summary>
	/// All extension methods for ExceptionReporter
	/// </summary>
	public static class ExceptionReporterExtensions
	{
		/// <summary>
		/// Append a dotted line to the given string
		/// </summary>
		public static StringBuilder AppendDottedLine(StringBuilder stringBuilder)
		{
			return stringBuilder.AppendLine("-----------------------------");
		}

		/// <summary>
		/// Return a string if not null, else the current value
		/// </summary>
		public static string GetString(string newString, string currentString)
		{
			return string.IsNullOrEmpty(newString) ? currentString : newString.Trim();
		}

		/// <summary>
		/// Returns the boolean value of configString; where configString is null or empty, the current value is returned
		/// <remarks>all of (case insensitive) 'y' 'n' 'true' or 'false' are accepted as boolean indicators</remarks>
		/// </summary>
		public static bool GetBool(string configString, bool currentValue)
		{
			if (string.IsNullOrEmpty(configString)) return currentValue;

			switch (configString.ToLower())
			{
				case "y" : 
				case "true": 
					return true;

				case "n" :
				case "false" : 
					return false;
			}

			return currentValue;
		}
        /// <summary>
        /// Returns the int value of configString
        /// </summary>
        public static int GetInt(string configString, int currentValue)
        {
            if (string.IsNullOrEmpty(configString)) return currentValue;
            return System.Convert.ToInt32(configString);
        }
        /// <summary>
        /// Returns true if string is empty
        /// </summary>
		public static bool IsEmpty(string input)
		{
			return string.IsNullOrEmpty(input);
		}
	}
}