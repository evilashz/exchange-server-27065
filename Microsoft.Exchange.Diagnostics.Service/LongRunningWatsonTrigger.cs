using System;
using System.Collections.Generic;
using System.Management;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x0200000F RID: 15
	public class LongRunningWatsonTrigger : LongRunningWerMgrTrigger
	{
		// Token: 0x0600004F RID: 79 RVA: 0x000053B0 File Offset: 0x000035B0
		public LongRunningWatsonTrigger(IJob job) : base(job, "Process\\((dw20|werfault).*\\)\\\\Elapsed Time", new PerfLogCounterTrigger.TriggerConfiguration("LongRunningWatsonTrigger", 120.0, double.MaxValue, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromHours(1.0), 0), new HashSet<DiagnosticMeasurement>(), new HashSet<string>())
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000541C File Offset: 0x0000361C
		internal float WatsonTargetPid(float watsonProcessId)
		{
			if (watsonProcessId < 1f)
			{
				return 0f;
			}
			float num = 0f;
			string queryString = string.Format("SELECT CommandLine FROM Win32_Process WHERE ProcessID = '{0}'", watsonProcessId);
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(queryString))
			{
				try
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						using (managementObject)
						{
							PropertyData propertyData = managementObject.Properties["CommandLine"];
							string text = (propertyData == null || propertyData.Value == null) ? string.Empty : propertyData.Value.ToString();
							try
							{
								int num2 = text.LastIndexOf("-p", StringComparison.OrdinalIgnoreCase);
								if (num2 > 0)
								{
									string text2 = text.Substring(num2);
									string[] array = text2.Split(new char[]
									{
										' '
									}, StringSplitOptions.RemoveEmptyEntries);
									if (array.Length > 1 && float.TryParse(array[1], out num) && num > 0f)
									{
										break;
									}
								}
							}
							catch (Exception ex)
							{
								Log.LogErrorMessage("LongRunningWatsonTrigger: Exception thrown retrieving Watson target PID from command line '{0}'{1}{2}", new object[]
								{
									string.IsNullOrEmpty(text) ? string.Empty : text,
									Environment.NewLine,
									ex.ToString()
								});
							}
						}
					}
				}
				catch (Exception ex2)
				{
					Log.LogErrorMessage("LongRunningWatsonTrigger: Exception thrown querying for command line for process id'{0}'{1}{2}", new object[]
					{
						watsonProcessId,
						Environment.NewLine,
						ex2.ToString()
					});
				}
			}
			if (num == 0f)
			{
				Log.LogErrorMessage("LongRunningWatsonTrigger: No target process found for watson process '{0}'", new object[]
				{
					watsonProcessId
				});
			}
			return num;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00005658 File Offset: 0x00003858
		protected override float CrashReportingTargetPid(float watsonPid, PerfLogLine line, PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			float num = this.WatsonTargetPid(watsonPid);
			string text = string.Empty;
			if (num > 0f)
			{
				foreach (DiagnosticMeasurement diagnosticMeasurement in line.PerformanceCounterNames)
				{
					if (DiagnosticMeasurement.CounterFilterComparer.Comparer.Equals(diagnosticMeasurement, LongRunningWatsonTrigger.ProcessIdCounter))
					{
						float? num2 = line[diagnosticMeasurement];
						if (num2 != null && num2.Value == num)
						{
							text = diagnosticMeasurement.InstanceName;
							break;
						}
					}
				}
			}
			float result = -1f;
			if (!string.IsNullOrEmpty(text) && (base.ProcessKillSet.Contains(text) || base.ProcessKillSet.Contains(LongRunningWerMgrTrigger.ProcessNameNoInstance(text))))
			{
				result = num;
			}
			return result;
		}

		// Token: 0x04000038 RID: 56
		private static readonly DiagnosticMeasurement ProcessIdCounter = DiagnosticMeasurement.GetMeasure("Process", "ID Process");
	}
}
