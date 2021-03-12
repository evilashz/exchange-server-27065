using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000052 RID: 82
	public sealed class ELCDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000D35C File Offset: 0x0000B55C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportTracer, base.TraceContext, "ELCDiscovery.DoWork", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Compliance\\ELC\\ELCDiscovery.cs", 32);
			if (!DiscoveryUtils.IsMailboxRoleInstalled())
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportTracer, base.TraceContext, "ELCDiscovery.DoWork(): MBX role not installed. Skip.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Compliance\\ELC\\ELCDiscovery.cs", 36);
				base.Result.StateAttribute1 = "ELCDiscovery: MBX role not installed. Skip.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"ELCMonitor.xml"
			}, base.Broker, base.TraceContext, base.Result);
		}
	}
}
