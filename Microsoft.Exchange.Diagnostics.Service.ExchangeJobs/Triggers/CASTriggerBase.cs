using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000030 RID: 48
	public abstract class CASTriggerBase : PerInstanceTrigger
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00008F05 File Offset: 0x00007105
		protected CASTriggerBase(IJob job, string counterNamePattern, PerfLogCounterTrigger.TriggerConfiguration configuration, DiagnosticMeasurement requestsPerSecondMeasurement, double requestsPerSecondThreshold, HashSet<DiagnosticMeasurement> additionalCounters, HashSet<string> excludedInstances) : this(job, counterNamePattern, new CASTriggerBase.CasConfiguration(requestsPerSecondThreshold, configuration, excludedInstances), additionalCounters, requestsPerSecondMeasurement)
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00008F1D File Offset: 0x0000711D
		private CASTriggerBase(IJob job, string counterNamePattern, CASTriggerBase.CasConfiguration configuration, HashSet<DiagnosticMeasurement> additionalCounters, DiagnosticMeasurement requestsPerSecondMeasurement) : base(job, counterNamePattern, additionalCounters, configuration)
		{
			this.requestsPerSecondMeasurement = requestsPerSecondMeasurement;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00008F34 File Offset: 0x00007134
		protected override bool ShouldMonitorCounter(DiagnosticMeasurement counter)
		{
			if (!base.ShouldMonitorCounter(counter))
			{
				return false;
			}
			foreach (string value in base.ExcludedInstances)
			{
				if (counter.InstanceName.Contains(value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00008FA0 File Offset: 0x000071A0
		protected override bool ShouldTrigger(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			bool flag = false;
			bool flag2 = base.ShouldTrigger(context);
			if (flag2)
			{
				DiagnosticMeasurement measure = DiagnosticMeasurement.GetMeasure(context.Counter.MachineName, this.requestsPerSecondMeasurement.ObjectName, this.requestsPerSecondMeasurement.CounterName, context.Counter.InstanceName);
				ValueStatistics valueStatistics;
				if (context.AdditionalData.TryGetValue(measure, out valueStatistics))
				{
					double requestsPerSecondThreshold = ((CASTriggerBase.CasConfiguration)base.Configuration).RequestsPerSecondThreshold;
					float? last = valueStatistics.Last;
					double num = requestsPerSecondThreshold;
					if ((double)last.GetValueOrDefault() >= num && last != null)
					{
						flag = true;
					}
					Log.LogInformationMessage("[CASTriggerBase.ShouldTrigger] Decision is {0} trigger, {1}/s {2} {3}/s for {4}", new object[]
					{
						flag ? "will" : "won't",
						valueStatistics.Last,
						flag ? ">=" : "<",
						requestsPerSecondThreshold,
						context.Counter.ToString()
					});
				}
				else
				{
					Log.LogWarningMessage("[CASTriggerBase.ShouldTrigger] Couldn't find requests per second measurement for {0} (currentMeasurement = {1})", new object[]
					{
						context.Counter.ToString(),
						measure.ToString()
					});
				}
			}
			return flag2 && flag;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000090D4 File Offset: 0x000072D4
		protected override string CollectAdditionalInformation(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			string[] array = context.Counter.InstanceName.Split(new char[]
			{
				';'
			});
			return string.Format("${0}|{1}|{2}|{3}$", new object[]
			{
				context.Counter.CounterName,
				array[0],
				array[1],
				(long)context.Value
			});
		}

		// Token: 0x04000139 RID: 313
		private readonly DiagnosticMeasurement requestsPerSecondMeasurement;

		// Token: 0x02000031 RID: 49
		public class CasConfiguration : PerInstanceTrigger.PerInstanceConfiguration
		{
			// Token: 0x06000113 RID: 275 RVA: 0x0000913C File Offset: 0x0000733C
			public CasConfiguration(double requestsPerSecondThreshold, PerfLogCounterTrigger.TriggerConfiguration triggerConfiguration, HashSet<string> excludedInstances) : base(triggerConfiguration, excludedInstances)
			{
				string triggerPrefix = triggerConfiguration.TriggerPrefix;
				string text = triggerPrefix + "RequestsPerSecondThreshold";
				this.defaultRequestsPerSecondThreshold = requestsPerSecondThreshold;
				this.requestsPerSecondThreshold = Configuration.GetConfigDouble(text, double.MinValue, double.MaxValue, requestsPerSecondThreshold);
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x06000114 RID: 276 RVA: 0x0000918A File Offset: 0x0000738A
			public double RequestsPerSecondThreshold
			{
				get
				{
					return this.requestsPerSecondThreshold;
				}
			}

			// Token: 0x06000115 RID: 277 RVA: 0x00009192 File Offset: 0x00007392
			public override PerfLogCounterTrigger.PerfLogCounterConfiguration Reload()
			{
				return new CASTriggerBase.CasConfiguration(this.defaultRequestsPerSecondThreshold, base.DefaultTriggerConfiguration, base.DefaultExcludedInstances);
			}

			// Token: 0x06000116 RID: 278 RVA: 0x000091AC File Offset: 0x000073AC
			public override int GetHashCode()
			{
				return this.requestsPerSecondThreshold.GetHashCode() ^ base.GetHashCode();
			}

			// Token: 0x06000117 RID: 279 RVA: 0x000091CE File Offset: 0x000073CE
			public override bool Equals(object right)
			{
				return this.Equals(right as CASTriggerBase.CasConfiguration);
			}

			// Token: 0x06000118 RID: 280 RVA: 0x000091DC File Offset: 0x000073DC
			protected bool Equals(CASTriggerBase.CasConfiguration right)
			{
				return right != null && this.requestsPerSecondThreshold.Equals(right.requestsPerSecondThreshold) && base.Equals(right);
			}

			// Token: 0x0400013A RID: 314
			private readonly double defaultRequestsPerSecondThreshold;

			// Token: 0x0400013B RID: 315
			private readonly double requestsPerSecondThreshold;
		}
	}
}
