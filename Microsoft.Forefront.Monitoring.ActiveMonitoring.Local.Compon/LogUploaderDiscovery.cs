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
	// Token: 0x02000094 RID: 148
	public sealed class LogUploaderDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x000192A7 File Offset: 0x000174A7
		internal static bool DoesLogUploaderServiceExist()
		{
			return ServiceController.GetServices().Any((ServiceController s) => s.ServiceName == "MSComplianceAudit");
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000192D0 File Offset: 0x000174D0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ProvisioningTracer, LogUploaderDiscovery.traceContext, "[LogUploaderDiscovery.DoWork]: Started Execution.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\LogUploader\\LogUploaderDiscovery.cs", 48);
			if (!LogUploaderDiscovery.DoesLogUploaderServiceExist())
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.MessageTracingTracer, LogUploaderDiscovery.traceContext, "[LogUploaderDiscovery.DoWork]: ComplianceAuditService is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\LogUploader\\LogUploaderDiscovery.cs", 52);
				base.Result.StateAttribute1 = "LogUploaderDiscovery: ComplianceAuditService is not installed on this server.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"LogUploader.xml",
				"LogUploaderDefinitions.xml"
			}, base.Broker, base.TraceContext, base.Result);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ProvisioningTracer, LogUploaderDiscovery.traceContext, "[LogUploaderDiscovery.DoWork]: Ended Execution.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\LogUploader\\LogUploaderDiscovery.cs", 72);
		}

		// Token: 0x0400025F RID: 607
		public const string UploaderServiceName = "MSComplianceAudit";

		// Token: 0x04000260 RID: 608
		private static TracingContext traceContext = new TracingContext();
	}
}
