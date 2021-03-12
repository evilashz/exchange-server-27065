using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000BA RID: 186
	[Flags]
	internal enum ServiceControlManagerAccessFlags
	{
		// Token: 0x040001B9 RID: 441
		Connect = 1,
		// Token: 0x040001BA RID: 442
		CreateService = 2,
		// Token: 0x040001BB RID: 443
		EnumerateService = 4,
		// Token: 0x040001BC RID: 444
		Lock = 8,
		// Token: 0x040001BD RID: 445
		QueryLockStatus = 16,
		// Token: 0x040001BE RID: 446
		ModifyBootConfig = 32,
		// Token: 0x040001BF RID: 447
		AllAccess = 63
	}
}
