using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E84 RID: 3716
	[Flags]
	internal enum PropertyDefinitionFlags
	{
		// Token: 0x04003470 RID: 13424
		None = 0,
		// Token: 0x04003471 RID: 13425
		Navigation = 1,
		// Token: 0x04003472 RID: 13426
		CanFilter = 2,
		// Token: 0x04003473 RID: 13427
		CanCreate = 8,
		// Token: 0x04003474 RID: 13428
		CanUpdate = 16,
		// Token: 0x04003475 RID: 13429
		ChildOnlyEntity = 32
	}
}
