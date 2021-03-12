using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001C5 RID: 453
	[Serializable]
	internal class DialGroupEntryNumberMask : DialGroupEntryField
	{
		// Token: 0x06000FE8 RID: 4072 RVA: 0x000308A0 File Offset: 0x0002EAA0
		public DialGroupEntryNumberMask(string numberMask) : base(numberMask, "NumberMask")
		{
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000308AE File Offset: 0x0002EAAE
		public static DialGroupEntryNumberMask Parse(string numberMask)
		{
			return new DialGroupEntryNumberMask(numberMask);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x000308B6 File Offset: 0x0002EAB6
		protected override void Validate()
		{
			base.ValidateNullOrEmpty();
			base.ValidateMaxLength(64);
			base.ValidateRegex(DialGroupEntryNumberMask.numberRegex);
		}

		// Token: 0x0400097B RID: 2427
		public const int MaxLength = 64;

		// Token: 0x0400097C RID: 2428
		public const string NumberRegexString = "^\\*$|^\\d+\\*$|^\\d+x+$|^x+$|^\\d+$";

		// Token: 0x0400097D RID: 2429
		private static Regex numberRegex = new Regex("^\\*$|^\\d+\\*$|^\\d+x+$|^x+$|^\\d+$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
	}
}
