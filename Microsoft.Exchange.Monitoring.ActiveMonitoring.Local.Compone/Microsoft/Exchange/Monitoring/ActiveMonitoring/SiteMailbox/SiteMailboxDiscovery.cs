using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.SiteMailbox
{
	// Token: 0x020004D5 RID: 1237
	public sealed class SiteMailboxDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001EC0 RID: 7872 RVA: 0x000B8E5C File Offset: 0x000B705C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
				{
					this.WriteTrace("SiteMailboxDiscovery.DoWork: Skipping workitem generation since not on a mailbox server.");
					return;
				}
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				this.WriteTrace("SiteMailboxDiscovery.DoWork(): Skipping due to EndpointManagerEndpointUninitializedException. LAM will retry.");
				return;
			}
			this.CreateServiceHostServiceAvailabilityWorkItems();
			this.CreateServiceHostProcessCrashDetectionContext();
			this.CreateDocumentSyncSuccessWorkItems();
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x000B8EC4 File Offset: 0x000B70C4
		private void CreateServiceHostServiceAvailabilityWorkItems()
		{
			this.AddWorkDefinition<ProbeDefinition>(ServiceHostAvailabilityWorkItem.CreateProbeDefinition(base.Definition));
			this.AddWorkDefinition<MonitorDefinition>(ServiceHostAvailabilityWorkItem.CreateMonitorDefinition(base.Definition));
			this.AddWorkDefinition<ResponderDefinition>(ServiceHostAvailabilityWorkItem.CreateRecoveryResponderDefinition());
			this.AddWorkDefinition<ResponderDefinition>(ServiceHostAvailabilityWorkItem.CreateRecovery2ResponderDefinition());
			this.AddWorkDefinition<ResponderDefinition>(ServiceHostAvailabilityWorkItem.CreateEscalateResponderDefinition());
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000B8F14 File Offset: 0x000B7114
		private void CreateServiceHostProcessCrashDetectionContext()
		{
			this.AddWorkDefinition<ProbeDefinition>(ServiceHostProcessCrashDetectionWorkItem.CreateProbeDefinition(base.Definition));
			this.AddWorkDefinition<MonitorDefinition>(ServiceHostProcessCrashDetectionWorkItem.CreateMonitorDefinition(base.Definition));
			this.AddWorkDefinition<ResponderDefinition>(ServiceHostProcessCrashDetectionWorkItem.CreateEscalateResponderDefinition(base.Definition));
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000B8F49 File Offset: 0x000B7149
		private void CreateDocumentSyncSuccessWorkItems()
		{
			this.AddWorkDefinition<MonitorDefinition>(SiteMailboxSyncSuccessWorkItem.CreateMonitorDefinition(base.Definition));
			this.AddWorkDefinition<ResponderDefinition>(SiteMailboxSyncSuccessWorkItem.CreateEscalateResponderDefinition(base.Definition));
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000B8F6D File Offset: 0x000B716D
		private void AddWorkDefinition<T>(T definition) where T : WorkDefinition
		{
			base.Broker.AddWorkDefinition<T>(definition, base.TraceContext);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000B8F82 File Offset: 0x000B7182
		private void WriteTrace(string message)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.SiteMailboxTracer, base.TraceContext, message, null, "WriteTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\SiteMailbox\\SiteMailboxDiscovery.cs", 96);
		}
	}
}
