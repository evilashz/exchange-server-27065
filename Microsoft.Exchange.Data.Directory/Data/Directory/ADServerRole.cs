using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000F7 RID: 247
	[Flags]
	internal enum ADServerRole
	{
		// Token: 0x04000547 RID: 1351
		None = 0,
		// Token: 0x04000548 RID: 1352
		GlobalCatalog = 1,
		// Token: 0x04000549 RID: 1353
		DomainController = 2,
		// Token: 0x0400054A RID: 1354
		ConfigurationDomainController = 4
	}
}
