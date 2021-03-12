using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Journaling
{
	// Token: 0x020001E8 RID: 488
	public sealed class JournalingDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000D86 RID: 3462 RVA: 0x0005BDF8 File Offset: 0x00059FF8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				this.WriteTrace("JournalingDiscovery.DoWork: Skipping workitem generation since not on a mailbox server.");
				return;
			}
			this.CreateJournalingWorkItems();
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0005BE34 File Offset: 0x0005A034
		private void CreateJournalingWorkItems()
		{
			this.AddWorkDefinition<MonitorDefinition>(JournalingWorkItem.CreateMonitorDefinition(base.Definition));
			this.AddWorkDefinition<ResponderDefinition>(JournalingWorkItem.CreateEscalateResponderDefinition(base.Definition));
			this.AddWorkDefinition<MonitorDefinition>(JournalFilterAgentWorkItem.CreateMonitorDefinition(base.Definition));
			this.AddWorkDefinition<ResponderDefinition>(JournalFilterAgentWorkItem.CreateEscalateResponderDefinition(base.Definition));
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0005BE85 File Offset: 0x0005A085
		private void AddWorkDefinition<T>(T definition) where T : WorkDefinition
		{
			base.Broker.AddWorkDefinition<T>(definition, base.TraceContext);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0005BE9A File Offset: 0x0005A09A
		private void WriteTrace(string message)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.JournalingTracer, base.TraceContext, message, null, "WriteTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Journaling\\JournalingDiscovery.cs", 69);
		}
	}
}
