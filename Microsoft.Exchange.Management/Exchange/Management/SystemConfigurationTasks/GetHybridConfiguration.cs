using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008D4 RID: 2260
	[Cmdlet("Get", "HybridConfiguration")]
	public sealed class GetHybridConfiguration : GetSingletonSystemConfigurationObjectTask<HybridConfiguration>
	{
		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x06005021 RID: 20513 RVA: 0x0014F780 File Offset: 0x0014D980
		protected override ObjectId RootId
		{
			get
			{
				ADObjectId currentOrgContainerId = base.CurrentOrgContainerId;
				return HybridConfiguration.GetWellKnownParentLocation(currentOrgContainerId);
			}
		}
	}
}
