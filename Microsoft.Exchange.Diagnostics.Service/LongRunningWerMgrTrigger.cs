using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x0200000E RID: 14
	public class LongRunningWerMgrTrigger : PerInstanceTrigger
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000050A4 File Offset: 0x000032A4
		public LongRunningWerMgrTrigger(IJob job) : this(job, "Process\\(wermgr.*\\)\\\\Elapsed Time", new PerfLogCounterTrigger.TriggerConfiguration("LongRunningWerMgrTrigger", 3600.0, double.MaxValue, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromHours(1.0), 0), new HashSet<DiagnosticMeasurement>(), new HashSet<string>())
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00005110 File Offset: 0x00003310
		protected LongRunningWerMgrTrigger(IJob job, string counterNamePattern, PerfLogCounterTrigger.TriggerConfiguration triggerConfiguration, HashSet<DiagnosticMeasurement> additionalCounters, HashSet<string> excludedInstances) : base(job, counterNamePattern, additionalCounters, new PerInstanceTrigger.PerInstanceConfiguration(triggerConfiguration, excludedInstances))
		{
			string configString = Configuration.GetConfigString("WatsonKillProcesses", string.Empty);
			string[] array = configString.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array != null && array.Length > 0)
			{
				this.ProcessKillSet.UnionWith(array);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00005179 File Offset: 0x00003379
		internal HashSet<string> ProcessKillSet
		{
			get
			{
				return this.processKillSet;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00005181 File Offset: 0x00003381
		internal static string ProcessNameNoInstance(string processName)
		{
			return LongRunningWerMgrTrigger.counterInstanceRegex.Replace(processName, string.Empty);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00005194 File Offset: 0x00003394
		protected override void OnThresholdEvent(PerfLogLine line, PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			base.OnThresholdEvent(line, context);
			HashSet<float> processIds = context.ProcessIds;
			if (processIds.Count == 0)
			{
				return;
			}
			float watsonPid = processIds.First<float>();
			float num = this.CrashReportingTargetPid(watsonPid, line, context);
			if (num > 0f)
			{
				processIds.Add(num);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000051DC File Offset: 0x000033DC
		protected virtual float CrashReportingTargetPid(float watsonPid, PerfLogLine line, PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			string instanceName = context.Counter.InstanceName;
			float? num = null;
			string text = string.Empty;
			foreach (DiagnosticMeasurement diagnosticMeasurement in line.PerformanceCounterNames)
			{
				if (DiagnosticMeasurement.CounterFilterComparer.Comparer.Equals(diagnosticMeasurement, LongRunningWerMgrTrigger.ParentProcessIdCounter) && instanceName.Equals(diagnosticMeasurement.InstanceName, StringComparison.OrdinalIgnoreCase))
				{
					num = line[diagnosticMeasurement];
					break;
				}
			}
			float result = -1f;
			if (num != null && num.Value > 0f)
			{
				float value = num.Value;
				foreach (DiagnosticMeasurement diagnosticMeasurement2 in line.PerformanceCounterNames)
				{
					if (DiagnosticMeasurement.CounterFilterComparer.Comparer.Equals(diagnosticMeasurement2, LongRunningWerMgrTrigger.ProcessIdCounter))
					{
						float? num2 = line[diagnosticMeasurement2];
						if (num2 != null && num2.Value == value)
						{
							text = diagnosticMeasurement2.InstanceName;
							break;
						}
					}
				}
				if (value > 0f && !string.IsNullOrEmpty(text) && (this.ProcessKillSet.Contains(text) || this.ProcessKillSet.Contains(LongRunningWerMgrTrigger.ProcessNameNoInstance(text))))
				{
					result = value;
				}
			}
			return result;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005348 File Offset: 0x00003548
		protected override bool ShouldTrigger(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			HashSet<float> processIds = context.ProcessIds;
			return base.ShouldTrigger(context) && processIds.Count == 2;
		}

		// Token: 0x04000034 RID: 52
		private static readonly DiagnosticMeasurement ParentProcessIdCounter = DiagnosticMeasurement.GetMeasure("Process", "Creating Process ID");

		// Token: 0x04000035 RID: 53
		private static readonly DiagnosticMeasurement ProcessIdCounter = DiagnosticMeasurement.GetMeasure("Process", "ID Process");

		// Token: 0x04000036 RID: 54
		private static Regex counterInstanceRegex = new Regex("\\#(\\d+)\\Z", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000037 RID: 55
		private HashSet<string> processKillSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
	}
}
