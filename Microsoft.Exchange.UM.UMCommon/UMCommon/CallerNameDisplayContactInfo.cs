using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200007E RID: 126
	[Serializable]
	internal class CallerNameDisplayContactInfo : SimpleContactInfoBase
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		internal CallerNameDisplayContactInfo(string displayName)
		{
			this.displayName = displayName;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000F2BF File Offset: 0x0000D4BF
		internal override string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x040002F0 RID: 752
		private string displayName;
	}
}
