using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001C6 RID: 454
	[Serializable]
	internal class DialGroupEntryDialedNumber : DialGroupEntryField
	{
		// Token: 0x06000FEC RID: 4076 RVA: 0x000308E7 File Offset: 0x0002EAE7
		public DialGroupEntryDialedNumber(string dialedNumber) : base(dialedNumber, "DialedNumber")
		{
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x000308F5 File Offset: 0x0002EAF5
		public static DialGroupEntryDialedNumber Parse(string dialedNumber)
		{
			return new DialGroupEntryDialedNumber(dialedNumber);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x000308FD File Offset: 0x0002EAFD
		protected override void Validate()
		{
			base.ValidateNullOrEmpty();
			base.ValidateMaxLength(64);
			base.ValidateRegex(DialGroupEntryDialedNumber.dialedNumberRegex);
		}

		// Token: 0x0400097E RID: 2430
		public const int MaxLength = 64;

		// Token: 0x0400097F RID: 2431
		public const string DialedNumberRegexString = "^\\+?(\\*$|\\d+\\*$|\\d+x+$|x+$|\\d+$)";

		// Token: 0x04000980 RID: 2432
		private static Regex dialedNumberRegex = new Regex("^\\+?(\\*$|\\d+\\*$|\\d+x+$|x+$|\\d+$)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
	}
}
