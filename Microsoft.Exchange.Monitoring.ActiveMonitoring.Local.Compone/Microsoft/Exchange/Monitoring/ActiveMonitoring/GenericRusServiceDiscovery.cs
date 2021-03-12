using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000181 RID: 385
	public sealed class GenericRusServiceDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000B30 RID: 2864 RVA: 0x000476B8 File Offset: 0x000458B8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				if (!FfoLocalEndpointManager.IsWebServiceInstalled)
				{
					string text = "GenericRusServiceDiscovery: FFO Web service role is not installed on this server.";
					WTFDiagnostics.TraceInformation(ExTraceGlobals.GenericRusTracer, GenericRusServiceDiscovery.traceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\GenericRus\\GenericRusServiceDiscovery.cs", 45);
					base.Result.StateAttribute1 = text;
				}
				else
				{
					GenericWorkItemHelper.CreateAllDefinitions(new List<string>
					{
						"GenericRus_Server.xml"
					}, base.Broker, base.TraceContext, base.Result);
				}
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericRusTracer, base.TraceContext, "[GenericRusServiceDiscovery.DoWork]: EndpointException occurred, ignoring exception and treating as transient.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\GenericRus\\GenericRusServiceDiscovery.cs", 64);
			}
		}

		// Token: 0x04000874 RID: 2164
		private static TracingContext traceContext = new TracingContext();
	}
}
