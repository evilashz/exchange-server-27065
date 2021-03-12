using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000180 RID: 384
	public sealed class GenericRusClientDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000B2C RID: 2860 RVA: 0x000475A8 File Offset: 0x000457A8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				if (!GenericRusClientDiscovery.IsGenericRusClientInstalled())
				{
					string text = "[GenericRusClientDiscovery.DoWork]: None of the roles -  FFO HubTransport, FFO Background, FFO FrontEndTransport, Exchange Backend Role are installed on this server.";
					WTFDiagnostics.TraceInformation(ExTraceGlobals.GenericRusTracer, GenericRusClientDiscovery.traceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\GenericRus\\GenericRusClientDiscovery.cs", 45);
					base.Result.StateAttribute1 = text;
				}
				else
				{
					GenericWorkItemHelper.CreateAllDefinitions(new List<string>
					{
						"GenericRus_Client.xml"
					}, base.Broker, base.TraceContext, base.Result);
				}
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericRusTracer, base.TraceContext, "[GenericRusClientDiscovery.DoWork]: EndpointException occurred, ignoring exception and treating as transient.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\GenericRus\\GenericRusClientDiscovery.cs", 64);
			}
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00047650 File Offset: 0x00045850
		private static bool IsGenericRusClientInstalled()
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			return FfoLocalEndpointManager.IsHubTransportRoleInstalled || FfoLocalEndpointManager.IsFrontendTransportRoleInstalled || FfoLocalEndpointManager.IsBackgroundRoleInstalled || (instance.ExchangeServerRoleEndpoint != null && instance.ExchangeServerRoleEndpoint.IsBridgeheadRoleInstalled) || (instance.ExchangeServerRoleEndpoint != null && instance.ExchangeServerRoleEndpoint.IsFrontendTransportRoleInstalled);
		}

		// Token: 0x04000873 RID: 2163
		private static TracingContext traceContext = new TracingContext();
	}
}
