using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000229 RID: 553
	[Flags]
	internal enum EffectiveRights
	{
		// Token: 0x0400102B RID: 4139
		None = 0,
		// Token: 0x0400102C RID: 4140
		Modify = 1,
		// Token: 0x0400102D RID: 4141
		Read = 2,
		// Token: 0x0400102E RID: 4142
		Delete = 4,
		// Token: 0x0400102F RID: 4143
		CreateHierarchy = 8,
		// Token: 0x04001030 RID: 4144
		CreateContents = 16,
		// Token: 0x04001031 RID: 4145
		CreateAssociated = 32
	}
}
