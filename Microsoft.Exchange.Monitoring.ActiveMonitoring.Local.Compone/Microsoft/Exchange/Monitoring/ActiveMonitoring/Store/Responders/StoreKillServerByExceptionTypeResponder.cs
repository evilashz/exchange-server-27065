using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Store.Responders
{
	// Token: 0x020004E5 RID: 1253
	public class StoreKillServerByExceptionTypeResponder : DagForceRebootServerResponder
	{
		// Token: 0x06001F1B RID: 7963 RVA: 0x000BE3A8 File Offset: 0x000BC5A8
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			ProbeResult lastFailedProbeResult = WorkItemResultHelper.GetLastFailedProbeResult(this, base.Broker, cancellationToken);
			if (lastFailedProbeResult != null)
			{
				string text;
				if (base.Definition.Attributes.TryGetValue(StoreKillServerByExceptionTypeResponder.ExceptionTypePropertyKeyString, out text) && !string.IsNullOrWhiteSpace(text))
				{
					Type type = lastFailedProbeResult.GetType();
					PropertyInfo property = type.GetProperty(text);
					if (property != null)
					{
						string text2 = (string)property.GetValue(lastFailedProbeResult, null);
						string value;
						bool flag;
						if (!string.IsNullOrWhiteSpace(text2) && base.Definition.Attributes.TryGetValue(text2, out value) && !string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out flag) && flag)
						{
							WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.StoreTracer, base.TraceContext, "StoreKillServerResponder.DoResponderWork: Bugchecking server {0}", lastFailedProbeResult.MachineName, null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\StoreKillServerByExceptionTypeResponder.cs", 72);
							base.Result.StateAttribute1 = string.Format("Bugchecking for exception type {0}", text2);
							base.DoResponderWork(cancellationToken);
							return;
						}
					}
				}
			}
			else
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.StoreTracer, base.TraceContext, "StoreKillServerResponder.DoResponderWork: Failed to get last probe result, not taking any bugcheck action.", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\StoreKillServerByExceptionTypeResponder.cs", 94);
			}
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.StoreTracer, base.TraceContext, "StoreKillServerResponder.DoResponderWork: No bugcheck action is being taken on server {0}", Environment.MachineName, null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\StoreKillServerByExceptionTypeResponder.cs", 102);
			base.Result.RecoveryResult = ServiceRecoveryResult.Skipped;
			base.Result.StateAttribute2 = "Taking no bugcheck action";
		}

		// Token: 0x0400167D RID: 5757
		internal static readonly string ExceptionTypePropertyKeyString = "ExceptionTypeProperty";
	}
}
