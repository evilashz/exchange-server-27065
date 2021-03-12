using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200027E RID: 638
	public class SLALatencyOverallConsecutiveBelowThresholdMonitor : OverallConsecutiveSampleValueBelowThresholdMonitor
	{
		// Token: 0x06001501 RID: 5377 RVA: 0x0004024C File Offset: 0x0003E44C
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			base.Result.StateAttribute6 = 0.0;
			base.Result.StateAttribute7 = 0.0;
			base.Result.StateAttribute8 = 0.0;
			string serverComponent = base.Definition.Attributes["ServerComponentName"];
			string expectedState = base.Definition.Attributes["ExpectedComponentState"];
			string text;
			this.shouldGatherSlaDataAndAlert = ComponentState.VerifyExpectedState(serverComponent, expectedState, out text);
			base.Result.StateAttribute10 = (double)(this.shouldGatherSlaDataAndAlert ? 1 : 0);
			base.DoMonitorWork(cancellationToken);
			if (!this.shouldGatherSlaDataAndAlert)
			{
				return;
			}
			base.Result.StateAttribute6 = this.GatherSlaDatainStateAttribute("stateAttribute6Category", "stateAttribute6Counter", "stateAttribute6Instance");
			base.Result.StateAttribute7 = this.GatherSlaDatainStateAttribute("stateAttribute7Category", "stateAttribute7Counter", "stateAttribute7Instance");
			base.Result.StateAttribute8 = this.GatherSlaDatainStateAttribute("stateAttribute8Category", "stateAttribute8Counter", "stateAttribute8Instance");
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00040358 File Offset: 0x0003E558
		protected override bool ShouldAlert()
		{
			double num = 0.0;
			if (!this.shouldGatherSlaDataAndAlert)
			{
				return false;
			}
			string s;
			if (!base.Definition.Attributes.TryGetValue("GatingThreshold", out s) || !double.TryParse(s, out num))
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.TransportTracer, base.TraceContext, "SLALatency: no GatingThreshold", null, "ShouldAlert", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\SLAOverallConsecutiveBelowThresholdMonitor.cs", 121);
				base.Result.StateAttribute1 = "No GatingThreshold or invalid value syntax";
				return true;
			}
			string text = base.Definition.Attributes["GatingCategory"];
			string text2 = base.Definition.Attributes["GatingCounter"];
			string text3 = base.Definition.Attributes["GatingInstance"];
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text3))
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.TransportTracer, base.TraceContext, "SLALatency: Gating parameters are incorrect", null, "ShouldAlert", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\SLAOverallConsecutiveBelowThresholdMonitor.cs", 132);
				throw new ArgumentNullException("Gating parameters are incorrect");
			}
			bool flag = base.ShouldAlert();
			if (flag)
			{
				double num2;
				if (SLALatencyOverallConsecutiveBelowThresholdMonitor.GetPerfmonCounter(text, text2, text3, out num2) && num > num2)
				{
					base.Result.StateAttribute1 = string.Format("Monitor alert suppressed by gated performance counter: threshold='{0}', current='{1}'", num, num2);
					return false;
				}
				base.Result.StateAttribute1 = string.Format("Both SLA and alert gating performance counter are exceeding threshold='{0}', current='{1}'", num, num2);
			}
			return flag;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x000404C5 File Offset: 0x0003E6C5
		protected override void SetStateAttribute6ForScopeMonitoring(double counter)
		{
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000404C8 File Offset: 0x0003E6C8
		private static bool GetPerfmonCounter(string category, string counterName, string instance, out double counterValue)
		{
			counterValue = 0.0;
			try
			{
				using (PerformanceCounter performanceCounter = new PerformanceCounter(category, counterName, instance))
				{
					performanceCounter.NextValue();
					counterValue = (double)performanceCounter.NextValue();
					return true;
				}
			}
			catch (InvalidOperationException)
			{
				counterValue = 0.0;
			}
			return false;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00040534 File Offset: 0x0003E734
		private double GatherSlaDatainStateAttribute(string categoryName, string counterName, string instanceName)
		{
			string text = base.Definition.Attributes[categoryName];
			string text2 = base.Definition.Attributes[counterName];
			string text3 = base.Definition.Attributes[instanceName];
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text3))
			{
				string text4 = string.Format("SLALatency: One of CateoryName: {0}, Counter: {1}, Instance: {2} parameters is incorrect", categoryName, counterName, instanceName);
				WTFDiagnostics.TraceError(ExTraceGlobals.TransportTracer, base.TraceContext, text4, null, "GatherSlaDatainStateAttribute", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\SLAOverallConsecutiveBelowThresholdMonitor.cs", 223);
				throw new ArgumentNullException(text4);
			}
			double result;
			if (!SLALatencyOverallConsecutiveBelowThresholdMonitor.GetPerfmonCounter(text, text2, text3, out result))
			{
				return 0.0;
			}
			return result;
		}

		// Token: 0x04000A29 RID: 2601
		internal const string ServerComponentName = "ServerComponentName";

		// Token: 0x04000A2A RID: 2602
		internal const string ExpectedComponentState = "ExpectedComponentState";

		// Token: 0x04000A2B RID: 2603
		internal const string GatingThresholdLabel = "GatingThreshold";

		// Token: 0x04000A2C RID: 2604
		internal const string GatingCategoryLabel = "GatingCategory";

		// Token: 0x04000A2D RID: 2605
		internal const string GatingCounterLabel = "GatingCounter";

		// Token: 0x04000A2E RID: 2606
		internal const string GatingInstanceLabel = "GatingInstance";

		// Token: 0x04000A2F RID: 2607
		private bool shouldGatherSlaDataAndAlert;
	}
}
