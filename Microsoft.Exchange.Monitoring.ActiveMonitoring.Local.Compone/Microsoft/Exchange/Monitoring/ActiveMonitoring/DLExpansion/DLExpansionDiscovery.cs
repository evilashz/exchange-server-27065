using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.RecipientDLExpansion;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.DLExpansion
{
	// Token: 0x02000161 RID: 353
	public sealed class DLExpansionDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0003F348 File Offset: 0x0003D548
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				this.WriteTrace("DLExpansionDiscovery.DoWork: Skipping workitem generation since not on a mailbox server.");
				return;
			}
			this.CreateDLExpansionSuccessWorkItems();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0003F382 File Offset: 0x0003D582
		private void CreateDLExpansionSuccessWorkItems()
		{
			this.AddWorkDefinition<MonitorDefinition>(DLExpansionSuccessWorkItem.CreateMonitorDefinition(base.Definition));
			this.AddWorkDefinition<ResponderDefinition>(DLExpansionSuccessWorkItem.CreateEscalateResponderDefinition(base.Definition));
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0003F3A6 File Offset: 0x0003D5A6
		private void AddWorkDefinition<T>(T definition) where T : WorkDefinition
		{
			base.Broker.AddWorkDefinition<T>(definition, base.TraceContext);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0003F3BB File Offset: 0x0003D5BB
		private void WriteTrace(string message)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RecipientDLExpansionEventBasedAssistantTracer, base.TraceContext, message, null, "WriteTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\DLExpansion\\DLExpansionDiscovery.cs", 64);
		}
	}
}
