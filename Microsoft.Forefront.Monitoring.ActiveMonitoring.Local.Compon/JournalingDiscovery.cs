using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000055 RID: 85
	public sealed class JournalingDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000EB24 File Offset: 0x0000CD24
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportTracer, base.TraceContext, "JournalDiscovery.DoWork", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Compliance\\Journaling\\JournalingDiscovery.cs", 32);
			if (!DiscoveryUtils.IsHubTransportRoleInstalled() || !DiscoveryUtils.IsMailboxRoleInstalled())
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportTracer, base.TraceContext, "JournalDiscovery.DoWork(): Hub or MBX role not installed. Skip.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Compliance\\Journaling\\JournalingDiscovery.cs", 36);
				base.Result.StateAttribute1 = "JournalDiscovery: Hub or MBX role not installed. Skip.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new string[]
			{
				"Journaling.xml",
				"JournalAgent.xml"
			}, base.Broker, base.TraceContext, base.Result);
		}
	}
}
