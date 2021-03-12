using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007AA RID: 1962
	[Flags]
	internal enum ModifyTableOptions
	{
		// Token: 0x040027E9 RID: 10217
		None = 0,
		// Token: 0x040027EA RID: 10218
		FreeBusyAware = 1,
		// Token: 0x040027EB RID: 10219
		ExtendedPermissionInformation = 2
	}
}
