using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000046 RID: 70
	public abstract class TransportGatedTrigger : PerInstanceTrigger
	{
		// Token: 0x0600014E RID: 334 RVA: 0x0000A626 File Offset: 0x00008826
		protected TransportGatedTrigger(IJob job, string counterNamePattern, double gatingCounterThreshold, PerfLogCounterTrigger.TriggerConfiguration configuration, HashSet<DiagnosticMeasurement> additionalCounters, HashSet<string> excludedInstances) : this(job, counterNamePattern, additionalCounters, new TransportGatedTrigger.TransportGatedConfiguration(gatingCounterThreshold, configuration, excludedInstances))
		{
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A63C File Offset: 0x0000883C
		protected TransportGatedTrigger(IJob job, string counterNamePattern, HashSet<DiagnosticMeasurement> additionalCounters, TransportGatedTrigger.TransportGatedConfiguration configuration) : base(job, counterNamePattern, additionalCounters, configuration)
		{
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000A649 File Offset: 0x00008849
		protected double GatingCounterThreshold
		{
			get
			{
				return ((TransportGatedTrigger.TransportGatedConfiguration)base.Configuration).GatingCounterThreshold;
			}
		}

		// Token: 0x06000151 RID: 337
		protected abstract DiagnosticMeasurement AdditionalDiagnosticMeasurement(PerfLogCounterTrigger.SurpassedThresholdContext context);

		// Token: 0x06000152 RID: 338 RVA: 0x0000A65C File Offset: 0x0000885C
		protected override string CollectAdditionalInformation(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DiagnosticMeasurement diagnosticMeasurement = this.AdditionalDiagnosticMeasurement(context);
			ValueStatistics valueStatistics;
			if (diagnosticMeasurement != null && context.AdditionalData.TryGetValue(diagnosticMeasurement, out valueStatistics))
			{
				string arg = DiagnosticMeasurement.FormatMeasureName(diagnosticMeasurement.MachineName, diagnosticMeasurement.ObjectName, diagnosticMeasurement.CounterName, diagnosticMeasurement.InstanceName);
				string format = "Secondary performance counter '{0}' value is '{1}'";
				stringBuilder.AppendFormat(format, arg, valueStatistics.Last);
			}
			if (stringBuilder.Length != 0)
			{
				return stringBuilder.ToString();
			}
			return base.CollectAdditionalInformation(context);
		}

		// Token: 0x02000047 RID: 71
		public class TransportGatedConfiguration : PerInstanceTrigger.PerInstanceConfiguration
		{
			// Token: 0x06000153 RID: 339 RVA: 0x0000A6DC File Offset: 0x000088DC
			public TransportGatedConfiguration(double gatingCounterThreshold, PerfLogCounterTrigger.TriggerConfiguration triggerConfiguration, HashSet<string> excludedInstances) : base(triggerConfiguration, excludedInstances)
			{
				string triggerPrefix = triggerConfiguration.TriggerPrefix;
				string text = triggerPrefix + "GatingCounterThreshold";
				this.defaultGatingCounterThreshold = gatingCounterThreshold;
				this.gatingCounterThreshold = Configuration.GetConfigDouble(text, double.MinValue, double.MaxValue, gatingCounterThreshold);
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000154 RID: 340 RVA: 0x0000A72A File Offset: 0x0000892A
			public double GatingCounterThreshold
			{
				get
				{
					return this.gatingCounterThreshold;
				}
			}

			// Token: 0x06000155 RID: 341 RVA: 0x0000A732 File Offset: 0x00008932
			public override PerfLogCounterTrigger.PerfLogCounterConfiguration Reload()
			{
				return new TransportGatedTrigger.TransportGatedConfiguration(this.defaultGatingCounterThreshold, base.DefaultTriggerConfiguration, base.DefaultExcludedInstances);
			}

			// Token: 0x06000156 RID: 342 RVA: 0x0000A74C File Offset: 0x0000894C
			public override int GetHashCode()
			{
				return this.gatingCounterThreshold.GetHashCode() ^ base.GetHashCode();
			}

			// Token: 0x06000157 RID: 343 RVA: 0x0000A76E File Offset: 0x0000896E
			public override bool Equals(object right)
			{
				return this.Equals(right as TransportGatedTrigger.TransportGatedConfiguration);
			}

			// Token: 0x06000158 RID: 344 RVA: 0x0000A77C File Offset: 0x0000897C
			protected bool Equals(TransportGatedTrigger.TransportGatedConfiguration right)
			{
				return right != null && this.gatingCounterThreshold.Equals(right.gatingCounterThreshold) && base.Equals(right);
			}

			// Token: 0x04000175 RID: 373
			private readonly double defaultGatingCounterThreshold;

			// Token: 0x04000176 RID: 374
			private readonly double gatingCounterThreshold;
		}
	}
}
