using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000C7 RID: 199
	public enum RequiredMaintenanceResourceType
	{
		// Token: 0x040004B5 RID: 1205
		Store,
		// Token: 0x040004B6 RID: 1206
		DirectoryServiceAndStore,
		// Token: 0x040004B7 RID: 1207
		StoreUrgent,
		// Token: 0x040004B8 RID: 1208
		StoreOnlineIntegrityCheck,
		// Token: 0x040004B9 RID: 1209
		StoreScheduledIntegrityCheck,
		// Token: 0x040004BA RID: 1210
		Size
	}
}
