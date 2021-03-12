using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000043 RID: 67
	public class StoreRpcAverageLatencyTrigger : PerInstanceTrigger
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0000A354 File Offset: 0x00008554
		static StoreRpcAverageLatencyTrigger()
		{
			StoreRpcAverageLatencyTrigger.excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
			{
				"_Total"
			};
			StoreRpcAverageLatencyTrigger.additionalContext = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
			{
				StoreRpcAverageLatencyTrigger.rpcOperationPerSecond
			};
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000A3B0 File Offset: 0x000085B0
		public StoreRpcAverageLatencyTrigger(IJob job) : this(job, new StoreRpcAverageLatencyTrigger.MbxConfiguration(new PerfLogCounterTrigger.TriggerConfiguration("StoreRpcAverageLatencyTrigger", 70.0, 150.0, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(5.0), 0), StoreRpcAverageLatencyTrigger.excludedInstances))
		{
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000A415 File Offset: 0x00008615
		public StoreRpcAverageLatencyTrigger(IJob job, StoreRpcAverageLatencyTrigger.MbxConfiguration configuration) : base(job, "MSExchangeIS Store\\(.+?\\)\\\\RPC Average Latency", StoreRpcAverageLatencyTrigger.additionalContext, configuration)
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000A42C File Offset: 0x0000862C
		protected override bool ShouldTrigger(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			bool flag = false;
			bool flag2 = base.ShouldTrigger(context);
			if (flag2)
			{
				DiagnosticMeasurement measure = DiagnosticMeasurement.GetMeasure(context.Counter.MachineName, StoreRpcAverageLatencyTrigger.rpcOperationPerSecond.ObjectName, StoreRpcAverageLatencyTrigger.rpcOperationPerSecond.CounterName, context.Counter.InstanceName);
				ValueStatistics valueStatistics;
				if (context.AdditionalData.TryGetValue(measure, out valueStatistics))
				{
					double rpcOperationPerSecondThreshold = ((StoreRpcAverageLatencyTrigger.MbxConfiguration)base.Configuration).RpcOperationPerSecondThreshold;
					float? last = valueStatistics.Last;
					double num = rpcOperationPerSecondThreshold;
					if ((double)last.GetValueOrDefault() >= num && last != null)
					{
						flag = true;
					}
				}
			}
			return flag2 && flag;
		}

		// Token: 0x0400016F RID: 367
		private static readonly HashSet<string> excludedInstances;

		// Token: 0x04000170 RID: 368
		private static readonly HashSet<DiagnosticMeasurement> additionalContext;

		// Token: 0x04000171 RID: 369
		private static readonly DiagnosticMeasurement rpcOperationPerSecond = DiagnosticMeasurement.GetMeasure("MSExchangeIS Store", "RPC Operations/sec");

		// Token: 0x02000044 RID: 68
		public class MbxConfiguration : PerInstanceTrigger.PerInstanceConfiguration
		{
			// Token: 0x06000146 RID: 326 RVA: 0x0000A4C4 File Offset: 0x000086C4
			public MbxConfiguration(PerfLogCounterTrigger.TriggerConfiguration triggerConfiguration, HashSet<string> excludedInstances) : base(triggerConfiguration, excludedInstances)
			{
				string triggerPrefix = triggerConfiguration.TriggerPrefix;
				string text = triggerPrefix + "RpcOperationsPerSecondThreshold";
				this.rpcOperationPerSecondThreshold = Configuration.GetConfigDouble(text, double.MinValue, double.MaxValue, 50.0);
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000147 RID: 327 RVA: 0x0000A513 File Offset: 0x00008713
			public double RpcOperationPerSecondThreshold
			{
				get
				{
					return this.rpcOperationPerSecondThreshold;
				}
			}

			// Token: 0x06000148 RID: 328 RVA: 0x0000A51B File Offset: 0x0000871B
			public override PerfLogCounterTrigger.PerfLogCounterConfiguration Reload()
			{
				return new StoreRpcAverageLatencyTrigger.MbxConfiguration(base.DefaultTriggerConfiguration, base.DefaultExcludedInstances);
			}

			// Token: 0x06000149 RID: 329 RVA: 0x0000A530 File Offset: 0x00008730
			public override int GetHashCode()
			{
				return this.rpcOperationPerSecondThreshold.GetHashCode() ^ base.GetHashCode();
			}

			// Token: 0x0600014A RID: 330 RVA: 0x0000A552 File Offset: 0x00008752
			public override bool Equals(object right)
			{
				return this.Equals(right as StoreRpcAverageLatencyTrigger.MbxConfiguration);
			}

			// Token: 0x0600014B RID: 331 RVA: 0x0000A560 File Offset: 0x00008760
			protected bool Equals(StoreRpcAverageLatencyTrigger.MbxConfiguration right)
			{
				return right != null && this.rpcOperationPerSecondThreshold.Equals(right.rpcOperationPerSecondThreshold) && base.Equals(right);
			}

			// Token: 0x04000172 RID: 370
			private readonly double rpcOperationPerSecondThreshold;
		}
	}
}
