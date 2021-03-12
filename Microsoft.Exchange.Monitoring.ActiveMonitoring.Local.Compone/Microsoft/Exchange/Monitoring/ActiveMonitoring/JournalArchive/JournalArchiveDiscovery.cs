using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.JournalArchive
{
	// Token: 0x020001DC RID: 476
	public sealed class JournalArchiveDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000D47 RID: 3399 RVA: 0x0005838C File Offset: 0x0005658C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				this.WriteTrace("JournalArchiveDiscovery.DoWork: Skipping workitem generation since not on a mailbox server.");
				return;
			}
			this.CreateJournalArchiveWorkItems();
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000583C6 File Offset: 0x000565C6
		private void CreateJournalArchiveWorkItems()
		{
			this.AddWorkDefinition<MonitorDefinition>(JournalArchiveWorkItem.CreateMonitorDefinition(base.Definition));
			this.AddWorkDefinition<ResponderDefinition>(JournalArchiveWorkItem.CreateEscalateResponderDefinition(base.Definition));
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000583EA File Offset: 0x000565EA
		private void AddWorkDefinition<T>(T definition) where T : WorkDefinition
		{
			base.Broker.AddWorkDefinition<T>(definition, base.TraceContext);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x000583FF File Offset: 0x000565FF
		private void WriteTrace(string message)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.JournalingTracer, base.TraceContext, message, null, "WriteTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\JournalArchive\\JournalArchiveDiscovery.cs", 64);
		}
	}
}
