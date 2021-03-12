using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.WebServiceDataProvider
{
	// Token: 0x02000293 RID: 659
	public sealed class WebServiceDataProviderDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001623 RID: 5667 RVA: 0x00046D3C File Offset: 0x00044F3C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (DiscoveryUtils.IsForefrontForOfficeDatacenter() || (!DiscoveryUtils.IsHubTransportRoleInstalled() && !DiscoveryUtils.IsMailboxRoleInstalled()))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DALTracer, WebServiceDataProviderDiscovery.traceContext, "[WebServiceDataProviderDiscovery.DoWork]: The role for Ffo Web service data provider is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebService\\WebserviceDataProviderDiscovery.cs", 37);
				base.Result.StateAttribute1 = "WebServiceDataProviderDiscovery: The role for Ffo Web service data provider is not installed on this server.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new string[]
			{
				"WebserviceDataProvider.xml"
			}, base.Broker, base.TraceContext, base.Result);
		}

		// Token: 0x04000ABE RID: 2750
		private static TracingContext traceContext = new TracingContext();
	}
}
