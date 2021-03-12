using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.EDS.Analyzers
{
	// Token: 0x0200016C RID: 364
	public class PerfLogActiveMonitoringAnalyzer : PerfLogAggregatorAnalyzer
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x00042A4C File Offset: 0x00040C4C
		public PerfLogActiveMonitoringAnalyzer(IJob job) : base(job)
		{
			this.maximumPerformanceCounterDelay = Configuration.GetConfigTimeSpan("PerfLogActiveMonitoringAnalyzerMaximumPerformanceCounterDelayPeriod", TimeSpan.FromMinutes(5.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(10.0));
			Log.LogInformationMessage("[PerfLogActiveMonitoringAnalyzer] Started.", new object[0]);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00042AB4 File Offset: 0x00040CB4
		internal bool ShouldWritePerformanceCounter(Dictionary<string, HashSet<string>> perfCounterFilterCollection, DiagnosticMeasurement counterMeasurement)
		{
			bool result = false;
			string key = this.GenerateObjectCounterName(counterMeasurement.ObjectName, counterMeasurement.CounterName);
			string text = counterMeasurement.InstanceName;
			HashSet<string> hashSet;
			if (perfCounterFilterCollection.TryGetValue(key, out hashSet))
			{
				if (hashSet.Contains(text) || hashSet.Contains("*"))
				{
					result = true;
				}
				else
				{
					int num = text.IndexOf('#');
					if (num != -1)
					{
						text = text.Substring(0, num);
						if (hashSet.Contains(text))
						{
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00042B28 File Offset: 0x00040D28
		internal void WritePerformanceCounterToActiveMonitoring(DiagnosticMeasurement counterMeasurement, double counterValue, DateTime timeStamp)
		{
			try
			{
				string counterName = this.GenerateFullCounterName(counterMeasurement);
				PerformanceCounterNotificationItem performanceCounterNotificationItem = new PerformanceCounterNotificationItem(counterName, counterValue, DateTime.SpecifyKind(timeStamp, DateTimeKind.Utc));
				performanceCounterNotificationItem.Publish(false);
			}
			catch (Exception ex)
			{
				Log.LogErrorMessage("[PerfLogActiveMonitoringAnalyzer] Caught exception writing perf counter: \n{0}.", new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00042B84 File Offset: 0x00040D84
		internal string GenerateFullCounterName(DiagnosticMeasurement counterMeasurement)
		{
			return string.Format(string.IsNullOrEmpty(counterMeasurement.InstanceName) ? "{0}\\{1}" : "{0}\\{1}\\{2}", counterMeasurement.ObjectName, counterMeasurement.CounterName, counterMeasurement.InstanceName);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00042BB6 File Offset: 0x00040DB6
		internal string GenerateObjectCounterName(string objectName, string counterName)
		{
			return string.Format("{0}\\{1}", objectName, counterName);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00042BC4 File Offset: 0x00040DC4
		internal Dictionary<string, HashSet<string>> CreatePerformanceCounterFilterCollection()
		{
			Dictionary<string, HashSet<string>> dictionary = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
			string configString = Configuration.GetConfigString("PerfLogActiveMonitoringAnalyzerPerformanceCounterFileDirectory", "Monitoring\\Config");
			WorkDefinitionDeploymentFileReader workDefinitionDeploymentFileReader = null;
			try
			{
				workDefinitionDeploymentFileReader = new WorkDefinitionDeploymentFileReader(configString, TracingContext.Default);
			}
			catch (ArgumentException exception)
			{
				this.ReportErrorToActiveMonitoring("[PerfLogActiveMonitoringAnalyzer] Error Raised from when instantiating WorkDefinitionDeploymentFileReader.", exception);
				return dictionary;
			}
			Log.LogInformationMessage("[PerfLogActiveMonitoringAnalyzer] Reading the list of performance counter filters.", new object[0]);
			try
			{
				string key = string.Empty;
				string text = string.Empty;
				foreach (WorkDefinitionDeploymentFileReader.PerformanceCounter performanceCounter in workDefinitionDeploymentFileReader.GetPerformanceCounterFilters())
				{
					key = this.GenerateObjectCounterName(performanceCounter.ObjectName, performanceCounter.CounterName);
					text = performanceCounter.InstanceName;
					HashSet<string> hashSet;
					if (!dictionary.TryGetValue(key, out hashSet))
					{
						hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
						dictionary.Add(key, hashSet);
					}
					if (text != "*")
					{
						text = text.TrimEnd("*".ToCharArray());
					}
					hashSet.Add(text);
				}
			}
			catch (XmlException ex)
			{
				this.ReportErrorToActiveMonitoring("[PerfLogActiveMonitoringAnalyzer] Error Raised from when reading performance counter filters.", ex);
				throw ex;
			}
			Log.LogInformationMessage("[PerfLogActiveMonitoringAnalyzer] Finished reading the list of performance counter filters.", new object[0]);
			return dictionary;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00042D1C File Offset: 0x00040F1C
		internal void ReportErrorToActiveMonitoring(string errorMessage, Exception exception = null)
		{
			if (exception != null)
			{
				errorMessage = string.Format("{0}. Exception: {1}.", errorMessage, exception.ToString());
			}
			Log.LogErrorMessage("{0}", new object[]
			{
				errorMessage
			});
			NotificationItem notificationItem = new EventNotificationItem(this.edsNotificationServiceName, PerformanceCounterNotificationItem.PerformanceCounterAnalyzerName, null, errorMessage, ResultSeverityLevel.Error);
			notificationItem.Publish(false);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00042D70 File Offset: 0x00040F70
		protected override void OutputDataInternal()
		{
			DateTime utcNow = DateTime.UtcNow;
			if (this.perfCounterFilterCollection == null)
			{
				this.perfCounterFilterCollection = this.CreatePerformanceCounterFilterCollection();
			}
			DateTime value = base.CurrentAggregateStamp.Value;
			bool flag = utcNow - value < this.maximumPerformanceCounterDelay;
			if (flag)
			{
				foreach (KeyValuePair<DiagnosticMeasurement, ValueStatistics> keyValuePair in base.CurrentValues)
				{
					DiagnosticMeasurement key = keyValuePair.Key;
					ValueStatistics value2 = keyValuePair.Value;
					if (value2.SampleCount > 0 && this.ShouldWritePerformanceCounter(this.perfCounterFilterCollection, key))
					{
						double counterValue = (double)value2.Mean.GetValueOrDefault();
						this.WritePerformanceCounterToActiveMonitoring(key, counterValue, value);
					}
				}
			}
			base.CurrentValues.Clear();
		}

		// Token: 0x04000784 RID: 1924
		private const string InstanceWildCardCharacter = "*";

		// Token: 0x04000785 RID: 1925
		private readonly TimeSpan maximumPerformanceCounterDelay;

		// Token: 0x04000786 RID: 1926
		private readonly string edsNotificationServiceName = ExchangeComponent.Eds.Name;

		// Token: 0x04000787 RID: 1927
		private Dictionary<string, HashSet<string>> perfCounterFilterCollection;
	}
}
