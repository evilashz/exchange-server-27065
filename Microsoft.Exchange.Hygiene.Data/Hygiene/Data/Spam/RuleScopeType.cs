using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001F1 RID: 497
	[Flags]
	public enum RuleScopeType : byte
	{
		// Token: 0x04000A67 RID: 2663
		None = 0,
		// Token: 0x04000A68 RID: 2664
		GlobalSpamRules = 1,
		// Token: 0x04000A69 RID: 2665
		OptionalSpamRules = 2,
		// Token: 0x04000A6A RID: 2666
		EnvelopeRules = 4,
		// Token: 0x04000A6B RID: 2667
		PartitionRules = 8,
		// Token: 0x04000A6C RID: 2668
		EnvelopeHeaderRules = 16,
		// Token: 0x04000A6D RID: 2669
		All = 255
	}
}
