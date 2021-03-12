using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x0200003B RID: 59
	public class OAuthAcsTimeoutTrigger : PerInstanceTrigger
	{
		// Token: 0x0600012C RID: 300 RVA: 0x000097B8 File Offset: 0x000079B8
		static OAuthAcsTimeoutTrigger()
		{
			OAuthAcsTimeoutTrigger.excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
			{
				"_Total"
			};
			OAuthAcsTimeoutTrigger.additionalContext = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
			{
				OAuthAcsTimeoutTrigger.totalOAuthAcsTimeoutRequests
			};
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00009814 File Offset: 0x00007A14
		public OAuthAcsTimeoutTrigger(IJob job) : base(job, "MSExchange OAuth\\(.+?\\)\\\\Outbound: Total Timeout Token Requests to AuthServer", new PerfLogCounterTrigger.TriggerConfiguration("OAuthAcsTimeoutTrigger", double.NaN, 1.0, TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(10.0), 0), OAuthAcsTimeoutTrigger.additionalContext, OAuthAcsTimeoutTrigger.excludedInstances)
		{
			this.lastValueOfTotalOAuthAcsTimeoutRequests = new float?(0f);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009890 File Offset: 0x00007A90
		protected override bool ShouldTrigger(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			bool result = false;
			bool flag = base.ShouldTrigger(context);
			if (flag)
			{
				DiagnosticMeasurement measure = DiagnosticMeasurement.GetMeasure(context.Counter.MachineName, OAuthAcsTimeoutTrigger.totalOAuthAcsTimeoutRequests.ObjectName, OAuthAcsTimeoutTrigger.totalOAuthAcsTimeoutRequests.CounterName, context.Counter.InstanceName);
				ValueStatistics valueStatistics;
				if (context.AdditionalData.TryGetValue(measure, out valueStatistics))
				{
					float? last = valueStatistics.Last;
					float? num = this.lastValueOfTotalOAuthAcsTimeoutRequests;
					float? num2 = (last != null & num != null) ? new float?(last.GetValueOrDefault() - num.GetValueOrDefault()) : null;
					float? num3 = num2;
					if (num3.GetValueOrDefault() > 5f && num3 != null)
					{
						float? num4 = this.lastValueOfTotalOAuthAcsTimeoutRequests;
						if (num4.GetValueOrDefault() != 0f || num4 == null)
						{
							result = true;
						}
					}
					this.lastValueOfTotalOAuthAcsTimeoutRequests = valueStatistics.Last;
				}
			}
			return result;
		}

		// Token: 0x04000158 RID: 344
		private static readonly HashSet<string> excludedInstances;

		// Token: 0x04000159 RID: 345
		private static readonly HashSet<DiagnosticMeasurement> additionalContext;

		// Token: 0x0400015A RID: 346
		private static readonly DiagnosticMeasurement totalOAuthAcsTimeoutRequests = DiagnosticMeasurement.GetMeasure("MSExchange OAuth", "Outbound: Total Timeout Token Requests to AuthServer");

		// Token: 0x0400015B RID: 347
		private float? lastValueOfTotalOAuthAcsTimeoutRequests;
	}
}
