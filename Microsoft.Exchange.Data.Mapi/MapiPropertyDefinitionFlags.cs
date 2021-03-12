using System;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x0200000B RID: 11
	[Flags]
	internal enum MapiPropertyDefinitionFlags
	{
		// Token: 0x04000011 RID: 17
		None = 0,
		// Token: 0x04000012 RID: 18
		ReadOnly = 1,
		// Token: 0x04000013 RID: 19
		MultiValued = 2,
		// Token: 0x04000014 RID: 20
		Calculated = 4,
		// Token: 0x04000015 RID: 21
		FilterOnly = 8,
		// Token: 0x04000016 RID: 22
		Mandatory = 16,
		// Token: 0x04000017 RID: 23
		PersistDefaultValue = 32,
		// Token: 0x04000018 RID: 24
		WriteOnce = 64
	}
}
