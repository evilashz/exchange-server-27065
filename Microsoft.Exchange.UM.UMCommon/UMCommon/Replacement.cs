using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000A5 RID: 165
	internal class Replacement
	{
		// Token: 0x060005A6 RID: 1446 RVA: 0x00016CF9 File Offset: 0x00014EF9
		public Replacement(string replacementString, bool shouldNormalize)
		{
			this.replacementString = replacementString;
			this.shouldNormalize = shouldNormalize;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00016D16 File Offset: 0x00014F16
		public string ReplacementString
		{
			get
			{
				return this.replacementString;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00016D1E File Offset: 0x00014F1E
		public bool ShouldNormalize
		{
			get
			{
				return this.shouldNormalize;
			}
		}

		// Token: 0x0400037A RID: 890
		private readonly string replacementString;

		// Token: 0x0400037B RID: 891
		private readonly bool shouldNormalize = true;
	}
}
