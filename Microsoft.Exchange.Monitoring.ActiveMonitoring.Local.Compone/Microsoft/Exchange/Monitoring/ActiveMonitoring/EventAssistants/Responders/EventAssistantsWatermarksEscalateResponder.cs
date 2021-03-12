using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.EventAssistants.Responders
{
	// Token: 0x0200017D RID: 381
	public class EventAssistantsWatermarksEscalateResponder : EscalateResponder
	{
		// Token: 0x06000B21 RID: 2849 RVA: 0x00046D48 File Offset: 0x00044F48
		protected override void BeforeEscalate(CancellationToken cancellationToken)
		{
			ProbeResult probeResult = null;
			string text = null;
			ProbeResult lastFailedProbeResult = WorkItemResultHelper.GetLastFailedProbeResult(this, base.Broker, cancellationToken);
			if (lastFailedProbeResult != null)
			{
				if (!string.IsNullOrWhiteSpace(lastFailedProbeResult.StateAttribute3))
				{
					int probeId;
					int resultId;
					if (int.TryParse(lastFailedProbeResult.StateAttribute2, out probeId) && int.TryParse(lastFailedProbeResult.StateAttribute3, out resultId))
					{
						probeResult = WorkItemResultHelper.GetProbeResultById(probeId, resultId, base.Broker, cancellationToken);
					}
					else
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.StoreTracer, base.TraceContext, "EventAssistantsWatermarksEscalateResponder.BeforeEscalate: Unable to get probe id's from probe stateattributes.", null, "BeforeEscalate", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsWatermarksEscalateResponder.cs", 49);
					}
				}
				if (probeResult != null)
				{
					if (!string.IsNullOrWhiteSpace(probeResult.StateAttribute11))
					{
						if (base.Definition.Attributes.TryGetValue(probeResult.StateAttribute11, out text))
						{
							base.SetServiceAndTeam(ExchangeComponent.EventAssistants.Service, text);
						}
						string key = string.Format("{0}_{1}", probeResult.StateAttribute11, "EscalationType");
						string value;
						NotificationServiceClass value2;
						if (base.Definition.Attributes.TryGetValue(key, out value) && Enum.TryParse<NotificationServiceClass>(value, true, out value2))
						{
							base.EscalationNotificationType = new NotificationServiceClass?(value2);
						}
					}
					base.CustomEscalationMessage = StoreMonitoringHelpers.PopulateEscalationMessage(base.Definition.EscalationMessage, probeResult);
				}
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "EventAssistantsWatermarksEscalateResponder.BeforeEscalate: Escalating to '{0}' team based on failed probe result", (!string.IsNullOrWhiteSpace(text)) ? text : base.Definition.EscalationTeam, null, "BeforeEscalate", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsWatermarksEscalateResponder.cs", 89);
				base.Result.StateAttribute4 = ((!string.IsNullOrWhiteSpace(text)) ? text : base.Definition.EscalationTeam);
				base.Result.StateAttribute5 = ((base.EscalationNotificationType != null) ? base.EscalationNotificationType.ToString() : base.Definition.NotificationServiceClass.ToString());
				return;
			}
			WTFDiagnostics.TraceError(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "EventAssistantsWatermarksEscalateResponder.BeforeEscalate: Failed to get last probe result, escalating to the default team.", null, "BeforeEscalate", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsWatermarksEscalateResponder.cs", 100);
		}
	}
}
