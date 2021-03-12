using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004B2 RID: 1202
	internal sealed class UMDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001E02 RID: 7682 RVA: 0x000B5334 File Offset: 0x000B3534
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			try
			{
				ExAssert.RetailAssert(instance.ExchangeServerRoleEndpoint != null, "Error: ExchangeServerRoleEndpoint should not be null");
				if (instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
				{
					UMSipOptionsDiscoveryUtils.InstantiateSipOptionsMonitoringForUMCallRouterService(base.Broker, base.TraceContext);
					this.InitializeUMMonitorsAndResponders(UMDiscovery.umCallRouterMonitorsAndResponders);
				}
				if (instance.ExchangeServerRoleEndpoint.IsUnifiedMessagingRoleInstalled)
				{
					UMSipOptionsDiscoveryUtils.InstantiateSipOptionsMonitoringForUMService(base.Broker, base.TraceContext);
					this.InitializeUMMonitorsAndResponders(UMDiscovery.umServiceMonitorsAndResponders);
				}
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DirectoryTracer, base.TraceContext, "UMDiscovery.DoWork: EndpointManagerEndpointUninitializedException is caught.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\UM\\Discovery\\UMDiscovery.cs", 92);
			}
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000B53E8 File Offset: 0x000B35E8
		private void InitializeUMMonitorsAndResponders(IUMLocalMonitoringMonitorAndResponder[] umAlertTypes)
		{
			foreach (IUMLocalMonitoringMonitorAndResponder iumlocalMonitoringMonitorAndResponder in umAlertTypes)
			{
				iumlocalMonitoringMonitorAndResponder.InitializeMonitorAndResponder(base.Broker, base.TraceContext);
			}
		}

		// Token: 0x0400151E RID: 5406
		private static IUMLocalMonitoringMonitorAndResponder[] umCallRouterMonitorsAndResponders = new IUMLocalMonitoringMonitorAndResponder[]
		{
			new PerfCounterRecentMissedCallNotificationProxyFailedMonitorAndResponder(),
			new UMCallRouterCertificateNearExpiryMonitorAndResponder()
		};

		// Token: 0x0400151F RID: 5407
		private static IUMLocalMonitoringMonitorAndResponder[] umServiceMonitorsAndResponders = new IUMLocalMonitoringMonitorAndResponder[]
		{
			new UMPipelineFullMonitorAndResponder(),
			new MediaEstablishedStatusFailedMonitorAndResponder(),
			new MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitorAndResponder(),
			new MediaEdgeResourceAllocationFailedMonitorAndResponder(),
			new PerfCounterUMPipelineSLAMonitorAndResponder(),
			new UMCertificateNearExpiryMonitorAndResponder(),
			new UMProtectedVoiceMessageEncryptDecryptFailedMonitorAndResponder(),
			new PerfCounterRecentPartnerTranscriptionFailedMonitorAndResponder(),
			new UMGrammarUsageMonitorAndResponder(),
			new UMTranscriptionThrottledMonitorAndResponder()
		};
	}
}
