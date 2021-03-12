using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001CB RID: 459
	[Serializable]
	public class NumberFormat
	{
		// Token: 0x06001024 RID: 4132 RVA: 0x0003120E File Offset: 0x0002F40E
		private NumberFormat(string prefix, int suffixLength)
		{
			this.phoneNumberPrefix = prefix;
			this.phoneNumberSuffixLength = suffixLength;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x00031224 File Offset: 0x0002F424
		public string Prefix
		{
			get
			{
				return this.phoneNumberPrefix;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0003122C File Offset: 0x0002F42C
		public int PhoneNumberSuffixLength
		{
			get
			{
				return this.phoneNumberSuffixLength;
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00031234 File Offset: 0x0002F434
		public bool TryMapNumber(string number, out string mappedNumber)
		{
			mappedNumber = null;
			if (number.Length < this.phoneNumberSuffixLength)
			{
				return false;
			}
			string str = number.Substring(number.Length - this.phoneNumberSuffixLength);
			mappedNumber = this.phoneNumberPrefix + str;
			return true;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00031278 File Offset: 0x0002F478
		public static NumberFormat Parse(string numberFormat)
		{
			if (string.IsNullOrEmpty(numberFormat))
			{
				return null;
			}
			if (numberFormat.Length > 24)
			{
				throw new FormatException(DataStrings.NumberFormatStringTooLong("Number Format", 24, numberFormat.Length));
			}
			int num = numberFormat.IndexOf("x", StringComparison.OrdinalIgnoreCase);
			string text;
			int suffixLength;
			if (num != -1)
			{
				string input = numberFormat.Substring(num);
				if (!NumberFormat.wildcardDigitRegex.IsMatch(input))
				{
					throw new ArgumentException(DataStrings.InvalidNumberFormat(numberFormat));
				}
				text = numberFormat.Substring(0, num);
				suffixLength = numberFormat.Length - text.Length;
			}
			else
			{
				text = numberFormat;
				suffixLength = 0;
			}
			if (!NumberFormat.numberRegex.IsMatch(text))
			{
				throw new ArgumentException(DataStrings.InvalidNumberFormat(numberFormat));
			}
			return new NumberFormat(text, suffixLength);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00031334 File Offset: 0x0002F534
		public override string ToString()
		{
			string arg = new string('x', this.phoneNumberSuffixLength);
			return string.Format("{0}{1}", this.phoneNumberPrefix, arg);
		}

		// Token: 0x0400099B RID: 2459
		public const int MaxLength = 24;

		// Token: 0x0400099C RID: 2460
		public const string AllowedCharacters = "[0-9x]";

		// Token: 0x0400099D RID: 2461
		private string phoneNumberPrefix;

		// Token: 0x0400099E RID: 2462
		private int phoneNumberSuffixLength;

		// Token: 0x0400099F RID: 2463
		private static Regex numberRegex = new Regex("^\\d+$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040009A0 RID: 2464
		private static Regex wildcardDigitRegex = new Regex("x*$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
	}
}
