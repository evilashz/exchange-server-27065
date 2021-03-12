using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020001F7 RID: 503
	public sealed class QueueDigestDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000F66 RID: 3942 RVA: 0x0002794C File Offset: 0x00025B4C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"QueueDigest.xml"
			}, base.Broker, base.TraceContext, base.Result);
		}

		// Token: 0x04000758 RID: 1880
		private const int E15MajorVersion = 15;
	}
}
