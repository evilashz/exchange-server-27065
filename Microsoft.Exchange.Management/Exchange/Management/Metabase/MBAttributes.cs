using System;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004C9 RID: 1225
	[Flags]
	internal enum MBAttributes
	{
		// Token: 0x04001FC5 RID: 8133
		None = 0,
		// Token: 0x04001FC6 RID: 8134
		Inherit = 1,
		// Token: 0x04001FC7 RID: 8135
		InsertPath = 64,
		// Token: 0x04001FC8 RID: 8136
		IsInherited = 32,
		// Token: 0x04001FC9 RID: 8137
		PartialPath = 2,
		// Token: 0x04001FCA RID: 8138
		Reference = 8,
		// Token: 0x04001FCB RID: 8139
		Secure = 4,
		// Token: 0x04001FCC RID: 8140
		Volatile = 16,
		// Token: 0x04001FCD RID: 8141
		StoreAsExpandSz = 4096,
		// Token: 0x04001FCE RID: 8142
		ReadOnly = 8192
	}
}
