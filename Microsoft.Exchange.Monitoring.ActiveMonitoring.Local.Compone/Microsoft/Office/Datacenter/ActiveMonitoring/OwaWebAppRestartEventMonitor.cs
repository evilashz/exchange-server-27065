using System;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Threading;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200026A RID: 618
	public class OwaWebAppRestartEventMonitor : OverallXFailuresMonitor
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x00075958 File Offset: 0x00073B58
		public new static MonitorDefinition CreateDefinition(string name, string sampleMask, string serviceName, Component component, int monitoringInterval, int recurrenceInterval, int numberOfFailures, bool enabled = true)
		{
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition(name, sampleMask, serviceName, component, monitoringInterval, recurrenceInterval, numberOfFailures, enabled);
			monitorDefinition.AssemblyPath = OwaWebAppRestartEventMonitor.AssemblyPath;
			monitorDefinition.TypeName = OwaWebAppRestartEventMonitor.TypeName;
			return monitorDefinition;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00075990 File Offset: 0x00073B90
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			base.DoMonitorWork(cancellationToken);
			string queryString = string.Format("<QueryList><Query Id=\"0\" Path=\"System\"><Select Path=\"System\">*[System[Provider[@Name='Microsoft-Windows-WAS'] and (EventID=5079) and (TimeCreated[@SystemTime&gt;='{0}' and @SystemTime&lt;='{1}'])] and (EventData/Data[@Name='AppPoolID']='MSExchangeOWAAppPool')]</Select></Query></QueryList>", base.MonitoringWindowStartTime.ToString("o"), base.Result.ExecutionStartTime.ToString("o"));
			if (this.GetSystemEvent(queryString))
			{
				base.Result.IsAlert = false;
			}
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000759F0 File Offset: 0x00073BF0
		private bool GetSystemEvent(string queryString)
		{
			TimeSpan timeout = TimeSpan.FromSeconds(30.0);
			using (EventLogReader eventLogReader = new EventLogReader(new EventLogQuery("System", PathType.LogName, queryString)
			{
				ReverseDirection = true
			}))
			{
				EventRecord eventRecord = eventLogReader.ReadEvent(timeout);
				if (eventRecord != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000D36 RID: 3382
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000D37 RID: 3383
		private static readonly string TypeName = typeof(OwaWebAppRestartEventMonitor).FullName;
	}
}
