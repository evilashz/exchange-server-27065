using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x020004FD RID: 1277
	public sealed class TransportSyncManagerDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001FA1 RID: 8097 RVA: 0x000C07C0 File Offset: 0x000BE9C0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				if (!LocalEndpointManager.IsDataCenter)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportSyncTracer, base.TraceContext, "TransportSyncManagerDiscovery.DoWork: Transport Sync is a datacenter only feature, no need to create transport sync related work items in case of on-premises", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TransportSync\\Discovery\\TransportSyncManagerDiscovery.cs", 43);
					base.Result.StateAttribute1 = "TransportSyncManagerDiscovery: Transport Sync is a datacenter only feature, no need to create transport sync related work items in case of on-premises";
				}
				else
				{
					LocalEndpointManager instance = LocalEndpointManager.Instance;
					ExchangeServerRoleEndpoint exchangeServerRoleEndpoint = instance.ExchangeServerRoleEndpoint;
					MailboxDatabaseEndpoint mailboxDatabaseEndpoint = instance.MailboxDatabaseEndpoint;
					if (exchangeServerRoleEndpoint == null || mailboxDatabaseEndpoint == null || !exchangeServerRoleEndpoint.IsMailboxRoleInstalled || mailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportSyncTracer, base.TraceContext, "TransportSyncManagerDiscovery.DoWork: Mailbox role is not installed on this server, no need to create transport sync related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TransportSync\\Discovery\\TransportSyncManagerDiscovery.cs", 60);
						base.Result.StateAttribute1 = "TransportSyncManagerDiscovery: Mailbox role is not installed on this server, no need to create transport sync related work items";
					}
					else
					{
						this.DoWorkHelper(cancellationToken, mailboxDatabaseEndpoint);
					}
				}
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportSyncTracer, base.TraceContext, "TransportSyncManagerDiscovery.DoWork: Endpoint initialization failed. Treating as transient error.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TransportSync\\Discovery\\TransportSyncManagerDiscovery.cs", 75);
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000C08AC File Offset: 0x000BEAAC
		private void DoWorkHelper(CancellationToken cancellationToken, MailboxDatabaseEndpoint mdbe)
		{
			TransportSyncManagerDiscovery.workitems = new IWorkItem[]
			{
				new ServiceAvailability(),
				new SubscriptionSlaMissed()
			};
			TransportSyncManagerDiscovery.workitemsPerMdb = new IWorkItem[]
			{
				new DatabaseConsistency()
			};
			this.InitializeWorkItem(TransportSyncManagerDiscovery.workitems);
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in mdbe.MailboxDatabaseInfoCollectionForBackend)
			{
				base.Definition.Attributes["DatabaseName"] = mailboxDatabaseInfo.MailboxDatabaseName;
				this.InitializeWorkItem(TransportSyncManagerDiscovery.workitemsPerMdb);
			}
			ResponderDefinition definition = RestartServiceResponder.CreateDefinition("TransportSyncManager.DatabaseConsistency.Restart", "TransportSyncManager.DatabaseConsistency.Monitor", Configurations.TransportSyncManagerServiceName, ServiceHealthStatus.Degraded, 15, 120, 0, false, DumpMode.None, null, 15.0, 0, ExchangeComponent.MailboxMigration.Name, null, true, true, null, false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000C09A4 File Offset: 0x000BEBA4
		private void InitializeWorkItem(IWorkItem[] workitems)
		{
			foreach (IWorkItem workItem in workitems)
			{
				workItem.Initialize(base.Definition, base.Broker, base.TraceContext);
			}
		}

		// Token: 0x0400172E RID: 5934
		private static IWorkItem[] workitems;

		// Token: 0x0400172F RID: 5935
		private static IWorkItem[] workitemsPerMdb;
	}
}
