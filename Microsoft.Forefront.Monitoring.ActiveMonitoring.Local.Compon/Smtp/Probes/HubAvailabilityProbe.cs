using System;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200021F RID: 543
	public class HubAvailabilityProbe : SmtpConnectionProbe
	{
		// Token: 0x060011A3 RID: 4515 RVA: 0x00033D48 File Offset: 0x00031F48
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (TransportCommon.IsServiceDisabledAndInactive("MSExchangeTransport", ServerComponentEnum.HubTransport))
			{
				WTFDiagnostics.TraceDebug<string, ServerComponentEnum>(ExTraceGlobals.MonitoringTracer, base.TraceContext, "Skipping probe execution as service ({0}) is disabled and component state ({1}) is marked as inactive.", "MSExchangeTransport", ServerComponentEnum.HubTransport, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\HubAvailabilityProbe.cs", 55);
				base.Result.StateAttribute1 = string.Format("{0} Inactive and Disabled", "MSExchangeTransport");
				return;
			}
			base.DoWork(cancellationToken);
		}

		// Token: 0x04000836 RID: 2102
		private const string HubTransportServiceName = "MSExchangeTransport";

		// Token: 0x04000837 RID: 2103
		private const ServerComponentEnum HubTransportComponentName = ServerComponentEnum.HubTransport;
	}
}
