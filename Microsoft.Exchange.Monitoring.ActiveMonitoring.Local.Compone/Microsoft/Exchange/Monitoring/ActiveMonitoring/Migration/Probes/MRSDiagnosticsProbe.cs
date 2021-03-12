using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Migration.Probes
{
	// Token: 0x02000213 RID: 531
	public class MRSDiagnosticsProbe : ProbeWorkItem
	{
		// Token: 0x06000EFC RID: 3836 RVA: 0x00063FBC File Offset: 0x000621BC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			bool logEnabledStatus;
			if (base.Definition.Attributes.ContainsKey("MRSAvailabilityLogEnabled") && bool.TryParse(base.Definition.Attributes["MRSAvailabilityLogEnabled"], out logEnabledStatus))
			{
				MRSAvailabilityLog.SetLogEnabledStatus(logEnabledStatus);
			}
			string targetResource = base.Definition.TargetResource;
			IDictionary<RequestWorkloadType, bool> workloadsIsEnabled = MRSHealth.GetWorkloadsIsEnabled(targetResource);
			this.LogResults(targetResource, workloadsIsEnabled);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00064020 File Offset: 0x00062220
		private void LogResults(string server, IDictionary<RequestWorkloadType, bool> workloadStatuses)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<RequestWorkloadType, bool> keyValuePair in workloadStatuses)
			{
				stringBuilder.AppendFormat("{0}={1};", keyValuePair.Key.ToString(), keyValuePair.Value ? 1 : 0);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			string text = stringBuilder.ToString();
			base.Result.StateAttribute1 = text;
			if (MRSDiagnosticsProbe.LoggingCount == 0)
			{
				MRSAvailabilityLog.Write("WorkloadAvailability", text);
			}
			base.Result.StateAttribute6 = (double)MRSDiagnosticsProbe.LoggingCount;
			MRSDiagnosticsProbe.LoggingCount = (MRSDiagnosticsProbe.LoggingCount + 1) % 3;
		}

		// Token: 0x04000B33 RID: 2867
		private const string MRSAvailabilityLogEnabled = "MRSAvailabilityLogEnabled";

		// Token: 0x04000B34 RID: 2868
		private const int LoggingFrequency = 3;

		// Token: 0x04000B35 RID: 2869
		private static int LoggingCount;
	}
}
