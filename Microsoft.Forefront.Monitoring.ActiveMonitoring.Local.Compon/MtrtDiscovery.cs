using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000097 RID: 151
	public sealed class MtrtDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x00019944 File Offset: 0x00017B44
		internal static bool DoesMTRTServiceExist()
		{
			ServiceController[] services = ServiceController.GetServices();
			ServiceController serviceController = services.FirstOrDefault((ServiceController s) => s.ServiceName.Equals("MSMessageTracingClient", StringComparison.InvariantCultureIgnoreCase));
			return serviceController != null;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00019984 File Offset: 0x00017B84
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!MtrtDiscovery.DoesMTRTServiceExist())
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.MessageTracingTracer, MtrtDiscovery.traceContext, "[MtrtDiscovery.DoWork]: MessageTracing role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\MessageTracing\\MTRTDiscovery.cs", 56);
				base.Result.StateAttribute1 = "MtrtDiscovery: MessageTracing role is not installed on this server.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"MTRT.xml",
				"MessageTracing.xml"
			}, base.Broker, base.TraceContext, base.Result);
		}

		// Token: 0x04000265 RID: 613
		public const string MessageTracingServiceName = "MSMessageTracingClient";

		// Token: 0x04000266 RID: 614
		private static TracingContext traceContext = new TracingContext();
	}
}
