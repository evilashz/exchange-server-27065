using System;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007F0 RID: 2032
	internal interface ICustomObjectHandler
	{
		// Token: 0x0600648E RID: 25742
		bool HandleObject(TenantRelocationSyncObject obj, ModifyRequest modRequest, UpdateData mData, TenantRelocationSyncData syncData, ITopologyConfigurationSession targetPartitionSession);
	}
}
