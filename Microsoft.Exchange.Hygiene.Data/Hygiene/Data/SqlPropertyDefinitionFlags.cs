using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200007F RID: 127
	[Flags]
	public enum SqlPropertyDefinitionFlags
	{
		// Token: 0x0400030F RID: 783
		None = 0,
		// Token: 0x04000310 RID: 784
		Required = 1,
		// Token: 0x04000311 RID: 785
		MXRecord = 2,
		// Token: 0x04000312 RID: 786
		Extended = 1,
		// Token: 0x04000313 RID: 787
		MultiValued = 2,
		// Token: 0x04000314 RID: 788
		ExtendedMultiValued = 3
	}
}
