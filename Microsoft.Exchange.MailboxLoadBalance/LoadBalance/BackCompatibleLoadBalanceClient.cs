using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.LoadBalance
{
	// Token: 0x02000095 RID: 149
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BackCompatibleLoadBalanceClient : MissingCapabilityLoadBalanceClientDecorator
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		public BackCompatibleLoadBalanceClient(MailboxLoadBalanceService serviceImpl, DirectoryServer targetServer) : base(null, targetServer)
		{
			this.serviceImpl = serviceImpl;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000E201 File Offset: 0x0000C401
		public override void BeginMailboxMove(BandMailboxRebalanceData rebalanceData, LoadMetric metric)
		{
			this.serviceImpl.MoveMailboxes(rebalanceData);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000E20F File Offset: 0x0000C40F
		public override void ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion)
		{
			serverVersion = LoadBalancerVersionInformation.LoadBalancerVersion;
		}

		// Token: 0x040001BA RID: 442
		private readonly MailboxLoadBalanceService serviceImpl;
	}
}
