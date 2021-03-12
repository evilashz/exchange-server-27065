using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors
{
	// Token: 0x020000A8 RID: 168
	public abstract class OverallConsecutiveAggregationMonitor : MonitorWorkItem
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00022EB0 File Offset: 0x000210B0
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x00022EB8 File Offset: 0x000210B8
		protected int LastProbeValue { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00022EC1 File Offset: 0x000210C1
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x00022EC9 File Offset: 0x000210C9
		private protected int[] ProbeValueHistory { protected get; private set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00022ED2 File Offset: 0x000210D2
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x00022EDA File Offset: 0x000210DA
		private protected int NumberOfValuesToStore { protected get; private set; }

		// Token: 0x060005EB RID: 1515 RVA: 0x00022EFC File Offset: 0x000210FC
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "OverallConsecutiveAggregationMonitor: Starting monitor action.", null, "DoMonitorWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Monitors\\OverallConsecutiveAggregationMonitor.cs", 66);
			if (base.Definition.Attributes.ContainsKey(OverallConsecutiveAggregationMonitor.ValueCountsToStoreKey))
			{
				this.NumberOfValuesToStore = int.Parse(base.Definition.Attributes[OverallConsecutiveAggregationMonitor.ValueCountsToStoreKey]);
			}
			else
			{
				this.NumberOfValuesToStore = Math.Max(1, base.Definition.MonitoringIntervalSeconds / base.Definition.RecurrenceIntervalSeconds);
			}
			DateTime executionStartTime = base.Result.ExecutionStartTime;
			Task<ProbeResult> task = base.Broker.GetProbeResults(base.Definition.SampleMask, base.MonitoringWindowStartTime, executionStartTime).ExecuteAsync(cancellationToken, base.TraceContext);
			Task task2 = task.Continue(new Action<ProbeResult>(this.GetLastProbeMetric), cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			Task<MonitorResult> task3 = base.Broker.GetLastMonitorResult(base.Definition, TimeSpan.FromSeconds((double)(base.Definition.RecurrenceIntervalSeconds * 2))).ExecuteAsync(cancellationToken, base.TraceContext);
			string probeHistory = null;
			task3.Continue(delegate(MonitorResult lastMonitorResult)
			{
				if (lastMonitorResult != null)
				{
					probeHistory = lastMonitorResult.StateAttribute1;
				}
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled).Wait();
			this.ProbeValueHistory = OverallConsecutiveAggregationMonitor.ParseProbeHistory(probeHistory);
			task2.Wait();
			this.ProbeValueHistory[this.ProbeValueHistory.Length - 1] = this.LastProbeValue;
			base.Result.StateAttribute1 = OverallConsecutiveAggregationMonitor.ProbeHistoryToString(this.ProbeValueHistory, this.NumberOfValuesToStore);
			base.Result.IsAlert = this.ShouldAlert();
			WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "OverallConsecutiveAggregationMonitor: Completed monitor action.", null, "DoMonitorWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Monitors\\OverallConsecutiveAggregationMonitor.cs", 107);
		}

		// Token: 0x060005EC RID: 1516
		protected abstract bool ShouldAlert();

		// Token: 0x060005ED RID: 1517 RVA: 0x000230B3 File Offset: 0x000212B3
		protected virtual void GetLastProbeMetric(ProbeResult probeResult)
		{
			this.LastProbeValue = ((probeResult == null) ? 0 : ((int)probeResult.StateAttribute6));
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000230C8 File Offset: 0x000212C8
		private static int[] ParseProbeHistory(string probeHistory)
		{
			if (string.IsNullOrEmpty(probeHistory))
			{
				return new int[1];
			}
			string[] array = probeHistory.TrimEnd(new char[]
			{
				OverallConsecutiveAggregationMonitor.ErrorListDelimiter
			}).Split(new char[]
			{
				OverallConsecutiveAggregationMonitor.ErrorListDelimiter
			});
			int[] array2 = new int[array.Length + 1];
			int num = 0;
			foreach (string s in array)
			{
				array2[num++] = int.Parse(s);
			}
			return array2;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00023150 File Offset: 0x00021350
		private static string ProbeHistoryToString(int[] errorCounts, int maxArrayLength)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = Math.Max(0, errorCounts.Length - maxArrayLength); i < errorCounts.Length; i++)
			{
				stringBuilder.AppendFormat("{0}{1}", errorCounts[i].ToString(), OverallConsecutiveAggregationMonitor.ErrorListDelimiter);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040003BB RID: 955
		public static readonly string ValueCountsToStoreKey = "ValueCountsToStore";

		// Token: 0x040003BC RID: 956
		private static readonly char ErrorListDelimiter = ',';
	}
}
