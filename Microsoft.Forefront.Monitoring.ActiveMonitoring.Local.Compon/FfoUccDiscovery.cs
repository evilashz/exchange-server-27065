using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020002A2 RID: 674
	internal sealed class FfoUccDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600167F RID: 5759 RVA: 0x00048898 File Offset: 0x00046A98
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				if (!FfoLocalEndpointManager.IsWebServiceInstalled)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.HTTPTracer, FfoUccDiscovery.traceContext, "[FFOUccDiscovery.DoWork]: WebService role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebUI\\FFOUccDiscovery.cs", 42);
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.HTTPTracer, FfoUccDiscovery.traceContext, "[FFOUccDiscovery.DoWork]: Discovery Started.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebUI\\FFOUccDiscovery.cs", 50);
					GenericWorkItemHelper.CreateAllDefinitions(new List<string>
					{
						"FFOUcc.xml"
					}, base.Broker, FfoUccDiscovery.traceContext, base.Result);
					GenericWorkItemHelper.CompleteDiscovery(FfoUccDiscovery.traceContext);
					WTFDiagnostics.TraceInformation(ExTraceGlobals.HTTPTracer, FfoUccDiscovery.traceContext, "[FFOUccDiscovery.DoWork]: Discovery Completed.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebUI\\FFOUccDiscovery.cs", 67);
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.HTTPTracer, FfoUccDiscovery.traceContext, "[FFOUccDiscovery.DoWork]: Exception occurred during discovery. {0}", ex.ToString(), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebUI\\FFOUccDiscovery.cs", 74);
				throw;
			}
		}

		// Token: 0x04000AFC RID: 2812
		private static TracingContext traceContext = new TracingContext();
	}
}
