using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.LoadBalance
{
	// Token: 0x02000098 RID: 152
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConsumerMetricsLoadBalanceCapabilityDecorator : MissingCapabilityLoadBalanceClientDecorator
	{
		// Token: 0x06000571 RID: 1393 RVA: 0x0000E331 File Offset: 0x0000C531
		public ConsumerMetricsLoadBalanceCapabilityDecorator(ILoadBalanceService service, DirectoryServer targetServer) : base(service, targetServer)
		{
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000E33B File Offset: 0x0000C53B
		public override void BeginMailboxMove(BandMailboxRebalanceData rebalanceData, LoadMetric metric)
		{
			base.BeginMailboxMove(rebalanceData.ToSerializationFormat(true), new LoadMetric(metric.Name, metric.IsSize));
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000E35C File Offset: 0x0000C55C
		public override DatabaseSizeInfo GetDatabaseSizeInformation(DirectoryIdentity database)
		{
			DirectoryDatabase database2 = (DirectoryDatabase)base.TargetServer.Directory.GetDirectoryObject(database);
			return base.GetDatabaseSizeInformation(database2);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000E388 File Offset: 0x0000C588
		public override LoadContainer GetLocalServerData(Band[] bands)
		{
			LoadContainer localServerData = base.GetLocalServerData(bands);
			localServerData.ConvertFromSerializationFormat();
			return localServerData;
		}
	}
}
