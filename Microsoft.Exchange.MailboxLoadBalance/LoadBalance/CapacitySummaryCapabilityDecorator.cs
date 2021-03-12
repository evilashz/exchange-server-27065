using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.LoadBalance
{
	// Token: 0x02000097 RID: 151
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CapacitySummaryCapabilityDecorator : MissingCapabilityLoadBalanceClientDecorator
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x0000E2DE File Offset: 0x0000C4DE
		public CapacitySummaryCapabilityDecorator(ILoadBalanceService service, DirectoryServer targetServer, LoadBalanceAnchorContext context) : base(service, targetServer)
		{
			this.context = context;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000E2F0 File Offset: 0x0000C4F0
		public override HeatMapCapacityData GetCapacitySummary(DirectoryIdentity objectIdentity, bool refreshData)
		{
			if (objectIdentity.Equals(base.TargetServer.Identity))
			{
				LoadContainer localServerData = this.GetLocalServerData(this.context.GetActiveBands());
				return localServerData.ToCapacityData();
			}
			return base.GetCapacitySummary(objectIdentity, refreshData);
		}

		// Token: 0x040001BC RID: 444
		private readonly LoadBalanceAnchorContext context;
	}
}
