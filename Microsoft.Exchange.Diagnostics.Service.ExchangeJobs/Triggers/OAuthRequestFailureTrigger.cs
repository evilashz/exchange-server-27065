using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x0200003C RID: 60
	public class OAuthRequestFailureTrigger : PerInstanceTrigger
	{
		// Token: 0x0600012F RID: 303 RVA: 0x0000998C File Offset: 0x00007B8C
		static OAuthRequestFailureTrigger()
		{
			OAuthRequestFailureTrigger.excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
			{
				"_Total"
			};
			OAuthRequestFailureTrigger.additionalContext = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
			{
				OAuthRequestFailureTrigger.totalOAuthRequests,
				OAuthRequestFailureTrigger.totalOAuthFailedRequests
			};
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009A08 File Offset: 0x00007C08
		public OAuthRequestFailureTrigger(IJob job) : base(job, "MSExchange OAuth\\(.+?\\)\\\\Inbound: Failed Auth Requests Total", new PerfLogCounterTrigger.TriggerConfiguration("OAuthRequestFailureTrigger", double.NaN, 1.0, TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(10.0), 0), OAuthRequestFailureTrigger.additionalContext, OAuthRequestFailureTrigger.excludedInstances)
		{
			this.lastValueOftotalOAuthFailedRequests = new float?(0f);
			this.lastValueOfTotalRequests = new float?(0f);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009A94 File Offset: 0x00007C94
		protected override bool ShouldTrigger(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			bool flag = false;
			bool flag2 = base.ShouldTrigger(context);
			if (flag2)
			{
				DiagnosticMeasurement measure = DiagnosticMeasurement.GetMeasure(context.Counter.MachineName, OAuthRequestFailureTrigger.totalOAuthRequests.ObjectName, OAuthRequestFailureTrigger.totalOAuthRequests.CounterName, context.Counter.InstanceName);
				DiagnosticMeasurement measure2 = DiagnosticMeasurement.GetMeasure(context.Counter.MachineName, OAuthRequestFailureTrigger.totalOAuthFailedRequests.ObjectName, OAuthRequestFailureTrigger.totalOAuthFailedRequests.CounterName, context.Counter.InstanceName);
				ValueStatistics valueStatistics;
				ValueStatistics valueStatistics2;
				if (context.AdditionalData.TryGetValue(measure2, out valueStatistics) && context.AdditionalData.TryGetValue(measure, out valueStatistics2))
				{
					float? last = valueStatistics.Last;
					float? num = this.lastValueOftotalOAuthFailedRequests;
					float? num2 = (last != null & num != null) ? new float?(last.GetValueOrDefault() - num.GetValueOrDefault()) : null;
					float? last2 = valueStatistics2.Last;
					float? num3 = this.lastValueOfTotalRequests;
					float? num4 = (last2 != null & num3 != null) ? new float?(last2.GetValueOrDefault() - num3.GetValueOrDefault()) : null;
					float? num5 = num2;
					if (num5.GetValueOrDefault() > 100f && num5 != null)
					{
						float? num6 = num4;
						if (num6.GetValueOrDefault() > 100f && num6 != null)
						{
							float? num7 = num2;
							float? num8 = num4;
							float? num9 = (num7 != null & num8 != null) ? new float?(num7.GetValueOrDefault() / num8.GetValueOrDefault()) : null;
							float? num10 = (num9 != null) ? new float?(num9.GetValueOrDefault() * 100f) : null;
							if (num10.GetValueOrDefault() > 20f && num10 != null)
							{
								flag = true;
							}
						}
					}
					this.lastValueOfTotalRequests = valueStatistics2.Last;
					this.lastValueOftotalOAuthFailedRequests = valueStatistics.Last;
				}
			}
			return flag2 && flag;
		}

		// Token: 0x0400015C RID: 348
		private static readonly HashSet<string> excludedInstances;

		// Token: 0x0400015D RID: 349
		private static readonly HashSet<DiagnosticMeasurement> additionalContext;

		// Token: 0x0400015E RID: 350
		private static readonly DiagnosticMeasurement totalOAuthRequests = DiagnosticMeasurement.GetMeasure("MSExchange OAuth", "Inbound: Total Auth Requests");

		// Token: 0x0400015F RID: 351
		private static readonly DiagnosticMeasurement totalOAuthFailedRequests = DiagnosticMeasurement.GetMeasure("MSExchange OAuth", "Inbound: Failed Auth Requests Total");

		// Token: 0x04000160 RID: 352
		private float? lastValueOftotalOAuthFailedRequests;

		// Token: 0x04000161 RID: 353
		private float? lastValueOfTotalRequests;
	}
}
