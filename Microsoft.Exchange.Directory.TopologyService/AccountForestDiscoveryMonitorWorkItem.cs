using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Directory.TopologyService.Common;
using Microsoft.Exchange.Directory.TopologyService.Configuration;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000004 RID: 4
	internal class AccountForestDiscoveryMonitorWorkItem : WorkItem<List<PartitionId>>
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002464 File Offset: 0x00000664
		public AccountForestDiscoveryMonitorWorkItem()
		{
			this.id = string.Format("AccountForestDiscoveryMonitorWorkItem-{0}", DateTime.UtcNow.ToString());
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000249A File Offset: 0x0000069A
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024A2 File Offset: 0x000006A2
		public override TimeSpan TimeoutInterval
		{
			get
			{
				return ConfigurationData.Instance.ForestScanTimeout;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024B0 File Offset: 0x000006B0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				base.ResultType = ResultType.Abandoned;
				return;
			}
			PartitionId[] allAccountPartitionIds = ADAccountPartitionLocator.GetAllAccountPartitionIds(true);
			base.Data = allAccountPartitionIds.ToList<PartitionId>();
			base.ResultType = ResultType.Succeeded;
		}

		// Token: 0x04000008 RID: 8
		private const string WorkIdFormat = "AccountForestDiscoveryMonitorWorkItem-{0}";

		// Token: 0x04000009 RID: 9
		private readonly string id;
	}
}
