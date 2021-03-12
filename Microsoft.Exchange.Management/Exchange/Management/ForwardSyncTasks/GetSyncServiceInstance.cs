using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000353 RID: 851
	[Cmdlet("Get", "SyncServiceInstance")]
	public sealed class GetSyncServiceInstance : GetObjectWithIdentityTaskBase<ServiceInstanceIdParameter, SyncServiceInstance>
	{
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00081BBE File Offset: 0x0007FDBE
		protected override ObjectId RootId
		{
			get
			{
				return SyncServiceInstance.GetMsoSyncRootContainer();
			}
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x00081BC5 File Offset: 0x0007FDC5
		protected override IConfigDataProvider CreateSession()
		{
			return ForwardSyncDataAccessHelper.CreateSession(false);
		}
	}
}
