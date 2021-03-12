using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x0200051E RID: 1310
	public sealed class ComplianceDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06002046 RID: 8262 RVA: 0x000C5728 File Offset: 0x000C3928
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				if (ExEnvironment.IsTest)
				{
					GenericWorkItemHelper.CreateAllDefinitions(new List<string>
					{
						"ComplianceDefinitions.xml",
						"eDiscoveryDefinitions.xml",
						"HoldDefinitionsForTest.xml"
					}, base.Broker, base.TraceContext, base.Result);
					return;
				}
				GenericWorkItemHelper.CreateAllDefinitions(new List<string>
				{
					"ComplianceDefinitions.xml",
					"eDiscoveryDefinitions.xml",
					"HoldDefinitions.xml"
				}, base.Broker, base.TraceContext, base.Result);
			}
		}

		// Token: 0x040017B8 RID: 6072
		private static TracingContext traceContext = new TracingContext();
	}
}
