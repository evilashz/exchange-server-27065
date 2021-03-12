using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors
{
	// Token: 0x020000A7 RID: 167
	public class OverallPercentSuccessByStateAttribute1Monitor : OverallPercentSuccessNoTimeoutMonitor
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x000226D8 File Offset: 0x000208D8
		public static MonitorDefinition CreateDefinition(string name, string sampleMask, string serviceName, Component component, double availabilityPercentage, TimeSpan monitoringInterval, TimeSpan recurrenceInterval, TimeSpan secondaryMonitoringInterval, string stateAttribute1Mask = "", bool enabled = true)
		{
			return new MonitorDefinition
			{
				AssemblyPath = OverallPercentSuccessByStateAttribute1Monitor.AssemblyPath,
				TypeName = OverallPercentSuccessByStateAttribute1Monitor.TypeName,
				Name = name,
				SampleMask = sampleMask,
				ServiceName = serviceName,
				Component = component,
				MaxRetryAttempts = 0,
				Enabled = enabled,
				TimeoutSeconds = 200,
				MonitoringThreshold = availabilityPercentage,
				MonitoringIntervalSeconds = (int)monitoringInterval.TotalSeconds,
				RecurrenceIntervalSeconds = (int)recurrenceInterval.TotalSeconds,
				SecondaryMonitoringThreshold = (double)((int)secondaryMonitoringInterval.TotalSeconds),
				StateAttribute1Mask = stateAttribute1Mask
			};
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00022850 File Offset: 0x00020A50
		internal Task SetStateAttribute1Numbers(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "OverallPercentSuccessByStateAttribute1Monitor: Getting StateAttribute1 values of: {0}.", base.Definition.SampleMask, null, "SetStateAttribute1Numbers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Monitors\\OverallPercentSuccessByStateAttribute1Monitor.cs", 102);
			Task<Dictionary<string, int>> stateAttribute1CountsForNewFailedProbeResults = base.GetStateAttribute1CountsForNewFailedProbeResults(base.Definition.SampleMask, cancellationToken);
			stateAttribute1CountsForNewFailedProbeResults.Continue(delegate(Dictionary<string, int> stateAttribute1Counts)
			{
				string newStateAttribute1Value;
				int newStateAttribute1Count;
				double newStateAttribute1Percent;
				this.GetStateAttribute1Statistics(stateAttribute1Counts, out newStateAttribute1Value, out newStateAttribute1Count, out newStateAttribute1Percent);
				base.Result.NewStateAttribute1Value = newStateAttribute1Value;
				base.Result.NewStateAttribute1Count = newStateAttribute1Count;
				base.Result.NewStateAttribute1Percent = newStateAttribute1Percent;
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "OverallPercentSuccessByStateAttribute1Monitor: Processed new result(s), max calculated attribute is {0}.", base.Result.NewStateAttribute1Value, null, "SetStateAttribute1Numbers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Monitors\\OverallPercentSuccessByStateAttribute1Monitor.cs", 123);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			Task<Dictionary<string, int>> stateAttribute1CountsForAllFailedProbeResults = base.GetStateAttribute1CountsForAllFailedProbeResults(base.Definition.SampleMask, cancellationToken);
			return stateAttribute1CountsForAllFailedProbeResults.Continue(delegate(Dictionary<string, int> stateAttribute1Counts)
			{
				string totalStateAttribute1Value;
				int totalStateAttribute1Count;
				double totalStateAttribute1Percent;
				this.GetStateAttribute1Statistics(stateAttribute1Counts, out totalStateAttribute1Value, out totalStateAttribute1Count, out totalStateAttribute1Percent);
				base.Result.TotalStateAttribute1Value = totalStateAttribute1Value;
				base.Result.TotalStateAttribute1Count = totalStateAttribute1Count;
				base.Result.TotalStateAttribute1Percent = totalStateAttribute1Percent;
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "OverallPercentSuccessByStateAttribute1Monitor: Processed total result(s), total max caluculated attribute is {0}.", base.Result.TotalStateAttribute1Value, null, "SetStateAttribute1Numbers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Monitors\\OverallPercentSuccessByStateAttribute1Monitor.cs", 147);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00022900 File Offset: 0x00020B00
		internal void GetStateAttribute1Statistics(Dictionary<string, int> stateAttribute1Counts, out string maxAttribute1Value, out int maxAttribute1Count, out double maxAttribute1Percent)
		{
			if (stateAttribute1Counts.Count == 0)
			{
				maxAttribute1Value = null;
				maxAttribute1Count = 0;
				maxAttribute1Percent = 0.0;
				return;
			}
			maxAttribute1Value = (from k in stateAttribute1Counts
			orderby k.Value descending
			select k.Key).FirstOrDefault<string>();
			stateAttribute1Counts.TryGetValue(maxAttribute1Value, out maxAttribute1Count);
			int num = stateAttribute1Counts.Sum((KeyValuePair<string, int> attribute) => attribute.Value);
			maxAttribute1Percent = 100.0;
			if (num != 0)
			{
				maxAttribute1Percent = (double)(maxAttribute1Count * 100 / num);
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00022B78 File Offset: 0x00020D78
		internal Task<bool> AlertBasedOnResultHistory(CancellationToken cancellationToken)
		{
			bool isAlert = true;
			int monitorCounter = 0;
			DateTime startTime = base.Result.ExecutionStartTime.AddSeconds(-base.Definition.SecondaryMonitoringThreshold);
			base.Result.StateAttribute5 = startTime.ToString();
			IOrderedEnumerable<MonitorResult> query = from r in base.Broker.GetSuccessfulMonitorResults(base.Definition, startTime)
			where r.ExecutionStartTime < base.Result.ExecutionStartTime
			orderby r.ExecutionEndTime descending
			select r;
			DateTime firstMonitorTime = DateTime.MaxValue;
			Task<int> task = base.Broker.AsDataAccessQuery<MonitorResult>(query).ExecuteAsync(delegate(MonitorResult result)
			{
				monitorCounter++;
				if (result.TotalSampleCount > 0)
				{
					if (result.TotalValue >= this.Definition.MonitoringThreshold)
					{
						this.Result.StateAttribute6 = 1.0;
						isAlert = false;
					}
					else if (!string.IsNullOrEmpty(this.Definition.StateAttribute1Mask) && !string.Equals(result.TotalStateAttribute1Value, this.Definition.StateAttribute1Mask, StringComparison.CurrentCultureIgnoreCase))
					{
						this.Result.StateAttribute6 = 4.0;
						isAlert = false;
					}
					if (result.ExecutionStartTime < firstMonitorTime)
					{
						firstMonitorTime = result.ExecutionStartTime;
					}
				}
			}, cancellationToken, base.TraceContext);
			return task.Continue(delegate(Task<int> t)
			{
				if (isAlert && ((double)monitorCounter < this.Definition.SecondaryMonitoringThreshold / (double)this.Definition.RecurrenceIntervalSeconds - 1.0 || (this.Result.ExecutionStartTime - firstMonitorTime).TotalSeconds < this.Definition.SecondaryMonitoringThreshold - (double)this.Definition.RecurrenceIntervalSeconds))
				{
					this.Result.StateAttribute6 = 2.0;
					isAlert = false;
				}
				return isAlert;
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00022E1C File Offset: 0x0002101C
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			Task task = Task.Factory.StartNew(delegate()
			{
				this.SetPercentSuccessNumbers(cancellationToken);
				if (!string.IsNullOrEmpty(this.Definition.StateAttribute1Mask))
				{
					this.SetStateAttribute1Numbers(cancellationToken);
				}
				WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, this.TraceContext, "OverallPercentSuccessByStateAttribute1Monitor: Finished collecting result data.", null, "DoMonitorWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Monitors\\OverallPercentSuccessByStateAttribute1Monitor.cs", 293);
			}, cancellationToken, TaskCreationOptions.AttachedToParent, TaskScheduler.Current);
			task.ContinueWith(delegate(Task t)
			{
				if (this.Result.TotalValue < this.Definition.MonitoringThreshold)
				{
					Task<bool> task2 = this.AlertBasedOnResultHistory(cancellationToken);
					task2.Continue(delegate(bool alertTask)
					{
						if (alertTask)
						{
							if (!string.IsNullOrEmpty(this.Definition.StateAttribute1Mask))
							{
								this.Result.IsAlert = string.Equals(this.Result.TotalStateAttribute1Value, this.Definition.StateAttribute1Mask, StringComparison.CurrentCultureIgnoreCase);
								if (!this.Result.IsAlert)
								{
									this.Result.StateAttribute6 = 3.0;
									return;
								}
							}
							else
							{
								this.Result.IsAlert = true;
							}
						}
					}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
				}
				WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, this.TraceContext, "OverallPercentSuccessByStateAttribute1Monitor: Finished analyzing probe results.", null, "DoMonitorWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Monitors\\OverallPercentSuccessByStateAttribute1Monitor.cs", 335);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
		}

		// Token: 0x040003B5 RID: 949
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040003B6 RID: 950
		private static readonly string TypeName = typeof(OverallPercentSuccessByStateAttribute1Monitor).FullName;
	}
}
