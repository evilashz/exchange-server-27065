using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x020004FE RID: 1278
	public sealed class TransportSyncWorkerDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001FA5 RID: 8101 RVA: 0x000C09E8 File Offset: 0x000BEBE8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				if (!LocalEndpointManager.IsDataCenter)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportSyncTracer, base.TraceContext, "TransportSyncWorkerDiscovery.DoWork: Transport Sync is a datacenter only feature, no need to create transport sync related work items in case of on-premises", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TransportSync\\Discovery\\TransportSyncWorkerDiscovery.cs", 36);
					base.Result.StateAttribute1 = "TransportSyncWorkerDiscovery: Transport Sync is a datacenter only feature, no need to create transport sync related work items in case of on-premises";
				}
				else
				{
					LocalEndpointManager instance = LocalEndpointManager.Instance;
					if (instance.ExchangeServerRoleEndpoint == null || (!instance.ExchangeServerRoleEndpoint.IsBridgeheadRoleInstalled && !instance.ExchangeServerRoleEndpoint.IsFrontendTransportRoleInstalled))
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportSyncTracer, base.TraceContext, "TransportSyncWorkerDiscovery.DoWork: Transport not installed. Skip.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TransportSync\\Discovery\\TransportSyncWorkerDiscovery.cs", 52);
					}
					else
					{
						this.DoWorkHelper(cancellationToken);
					}
				}
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportSyncTracer, base.TraceContext, "TransportSyncWorkerDiscovery.DoWork: Endpoint initialization failed. Treating as transient error.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TransportSync\\Discovery\\TransportSyncWorkerDiscovery.cs", 62);
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000C0ABC File Offset: 0x000BECBC
		private void DoWorkHelper(CancellationToken cancellationToken)
		{
			TransportSyncWorkerDiscovery.workitems = new IWorkItem[]
			{
				new DeltaSyncEndpointUnreachable(),
				new DeltaSyncPartnerAuthenticationFailed(),
				new DeltaSyncServiceEndpointsLoadFailed(),
				new RegistryAccessDenied()
			};
			this.InitializeWorkItem(TransportSyncWorkerDiscovery.workitems);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000C0B04 File Offset: 0x000BED04
		private void InitializeWorkItem(IWorkItem[] items)
		{
			foreach (IWorkItem workItem in items)
			{
				workItem.Initialize(base.Definition, base.Broker, base.TraceContext);
			}
		}

		// Token: 0x04001730 RID: 5936
		private static IWorkItem[] workitems;
	}
}
