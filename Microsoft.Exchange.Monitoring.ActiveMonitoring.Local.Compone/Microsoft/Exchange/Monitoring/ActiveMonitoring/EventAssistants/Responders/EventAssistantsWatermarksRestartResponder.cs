using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.EventAssistants.Responders
{
	// Token: 0x0200017E RID: 382
	public class EventAssistantsWatermarksRestartResponder : RestartServiceResponder
	{
		// Token: 0x06000B23 RID: 2851 RVA: 0x00046F34 File Offset: 0x00045134
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			base.Result.RecoveryResult = ServiceRecoveryResult.Skipped;
			ProbeResult lastFailedProbeResult = WorkItemResultHelper.GetLastFailedProbeResult(this, base.Broker, cancellationToken);
			if (lastFailedProbeResult != null)
			{
				if (!string.IsNullOrWhiteSpace(lastFailedProbeResult.StateAttribute11))
				{
					string value;
					if (base.Definition.Attributes.TryGetValue(lastFailedProbeResult.StateAttribute11, out value))
					{
						base.Definition.Attributes["WindowsServiceName"] = value;
					}
					if (base.Definition.Attributes.ContainsKey("WindowsServiceName") && !string.IsNullOrWhiteSpace(base.Definition.Attributes["WindowsServiceName"]))
					{
						WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "EventAssistantsWatermarksRestartResponder.DoResponderWork: Restarting service {0}", base.Definition.Attributes["WindowsServiceName"], null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsWatermarksRestartResponder.cs", 48);
						base.Result.StateAttribute1 = base.Definition.Attributes["WindowsServiceName"];
						base.DoResponderWork(cancellationToken);
					}
					base.Result.RecoveryResult = ServiceRecoveryResult.Succeeded;
					return;
				}
			}
			else
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "EventAssistantsWatermarksRestartResponder.DoResponderWork: Failed to get last probe result, not taking any service restart action.", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsWatermarksRestartResponder.cs", 65);
			}
		}
	}
}
