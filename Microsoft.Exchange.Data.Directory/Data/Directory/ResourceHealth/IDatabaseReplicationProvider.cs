using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009F9 RID: 2553
	internal interface IDatabaseReplicationProvider : IResourceLoadMonitor
	{
		// Token: 0x06007662 RID: 30306
		void Update(uint databaseReplicationHealth);
	}
}
