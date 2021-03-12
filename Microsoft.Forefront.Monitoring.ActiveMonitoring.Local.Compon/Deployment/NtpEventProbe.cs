using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Deployment
{
	// Token: 0x0200007D RID: 125
	public class NtpEventProbe : ProbeWorkItem
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0001314C File Offset: 0x0001134C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			DateTime executionStartTime = base.Result.ExecutionStartTime;
			DateTime monitorEndLocalTime = executionStartTime.ToLocalTime();
			ProbeResult lastProbeResult = this.GetLastProbeResult(cancellationToken);
			DateTime dateTime = (lastProbeResult != null) ? lastProbeResult.ExecutionStartTime : executionStartTime.AddSeconds((double)(-(double)base.Definition.RecurrenceIntervalSeconds));
			DateTime monitorStartLocalTime = dateTime.ToLocalTime();
			ProbeResult result = base.Result;
			result.ExecutionContext += string.Format("Event Check in UTC time range [{0}, {1}]. ", dateTime, executionStartTime);
			using (EventLog eventLog = new EventLog("System"))
			{
				IEnumerable<EventLogEntry> enumerable = from EventLogEntry e in eventLog.Entries
				where e.Source.Equals("Microsoft-Windows-Time-Service", StringComparison.OrdinalIgnoreCase) && e.TimeGenerated >= monitorStartLocalTime && e.TimeGenerated < monitorEndLocalTime
				orderby e.TimeGenerated
				select e;
				foreach (EventLogEntry eventLogEntry in enumerable)
				{
					long instanceId = eventLogEntry.InstanceId;
					if (instanceId == 29L)
					{
						throw new Exception(string.Format("Event ID {0} found in localtime {1} Message: {2}", eventLogEntry.InstanceId, eventLogEntry.TimeGenerated, eventLogEntry.Message));
					}
					if (instanceId == 34L)
					{
						throw new Exception(string.Format("Event ID {0} found in localtime {1} Message: {2}", eventLogEntry.InstanceId, eventLogEntry.TimeGenerated, eventLogEntry.Message));
					}
				}
			}
			ProbeResult result2 = base.Result;
			result2.ExecutionContext += " No NTP Synchronization issue detected.";
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00013354 File Offset: 0x00011554
		private ProbeResult GetLastProbeResult(CancellationToken cancellationToken)
		{
			ProbeResult lastProbeResult = null;
			IEnumerable<ProbeResult> query = (from result in base.Broker.GetProbeResults(base.Definition, base.Result.ExecutionStartTime.AddSeconds((double)(-2 * base.Definition.RecurrenceIntervalSeconds)))
			where !string.IsNullOrEmpty(result.ExecutionContext)
			orderby result.ExecutionStartTime descending
			select result).Take(1);
			Task<int> task = base.Broker.AsDataAccessQuery<ProbeResult>(query).ExecuteAsync(delegate(ProbeResult probeResult)
			{
				if (lastProbeResult == null)
				{
					lastProbeResult = probeResult;
				}
			}, cancellationToken, base.TraceContext);
			task.Wait(cancellationToken);
			return lastProbeResult;
		}

		// Token: 0x040001D2 RID: 466
		private const long NtpNoNtpPeersButPending = 29L;

		// Token: 0x040001D3 RID: 467
		private const long NtpTimeChangeTooBig = 34L;
	}
}
