using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000022 RID: 34
	internal abstract class CustomSyncStateInfo : SyncStateInfo
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000EF98 File Offset: 0x0000D198
		internal CustomSyncStateInfo()
		{
			AirSyncSyncStateTypeFactory.EnsureSyncStateTypesRegistered();
		}
	}
}
