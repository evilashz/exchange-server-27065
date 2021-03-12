using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x02000464 RID: 1124
	public class HostContollerNodeRestartProbe : SearchProbeBase
	{
		// Token: 0x06001C85 RID: 7301 RVA: 0x000A7718 File Offset: 0x000A5918
		protected override void InternalDoWork(CancellationToken cancellationToken)
		{
			int @int = base.AttributeHelper.GetInt("RestartThreshold", true, 0, null, null);
			double num = base.AttributeHelper.GetDouble("RestartHistoryCheckWindowMinutes", true, 0.0, null, null);
			num *= 1.1;
			string targetResource = base.Definition.TargetResource;
			long performanceCounterValue = SearchMonitoringHelper.GetPerformanceCounterValue("Search Host Controller", "Component Restarts", targetResource + " Fsis");
			base.Result.StateAttribute1 = performanceCounterValue.ToString();
			long num2 = 0L;
			ProbeResult lastProbeResult = SearchMonitoringHelper.GetLastProbeResult(this, base.Broker, cancellationToken);
			if (lastProbeResult == null || string.IsNullOrEmpty(lastProbeResult.StateAttribute1))
			{
				return;
			}
			long.TryParse(lastProbeResult.StateAttribute1, out num2);
			base.Result.StateAttribute2 = num2.ToString();
			DateTime cutOffTime = base.Result.ExecutionStartTime.ToUniversalTime().AddMinutes(-num);
			List<HostContollerNodeRestartProbe.RestartHistoryRecord> list = this.ReadRestartHistory(lastProbeResult, cutOffTime);
			long num3 = (performanceCounterValue >= num2) ? (performanceCounterValue - num2) : performanceCounterValue;
			list.Add(new HostContollerNodeRestartProbe.RestartHistoryRecord
			{
				StartTime = lastProbeResult.ExecutionEndTime.ToUniversalTime(),
				EndTime = base.Result.ExecutionStartTime.ToUniversalTime(),
				RestartCount = (int)num3
			});
			this.PersistRestartHistory(list);
			int num4 = 0;
			foreach (HostContollerNodeRestartProbe.RestartHistoryRecord restartHistoryRecord in list)
			{
				num4 += restartHistoryRecord.RestartCount;
			}
			if (num4 > @int)
			{
				string minutes = ((int)(base.Result.ExecutionStartTime.ToUniversalTime() - list[0].StartTime).TotalMinutes).ToString();
				string details = this.FormatRestartHistory(list);
				throw new SearchProbeFailureException(Strings.HostControllerExcessiveNodeRestarts(targetResource, num4.ToString(), minutes, details));
			}
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000A796C File Offset: 0x000A5B6C
		private void PersistRestartHistory(List<HostContollerNodeRestartProbe.RestartHistoryRecord> records)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<HostContollerNodeRestartProbe.RestartHistoryRecord> list = new List<HostContollerNodeRestartProbe.RestartHistoryRecord>(records.ToArray());
			list.Sort((HostContollerNodeRestartProbe.RestartHistoryRecord x, HostContollerNodeRestartProbe.RestartHistoryRecord y) => y.StartTime.CompareTo(x.StartTime));
			foreach (HostContollerNodeRestartProbe.RestartHistoryRecord restartHistoryRecord in list)
			{
				stringBuilder.AppendFormat("{0}|{1}|{2}`", restartHistoryRecord.StartTime.ToString("s"), restartHistoryRecord.EndTime.ToString("s"), restartHistoryRecord.RestartCount);
				if (stringBuilder.Length > 768)
				{
					break;
				}
			}
			base.Result.StateAttribute3 = stringBuilder.ToString();
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x000A7A6C File Offset: 0x000A5C6C
		private List<HostContollerNodeRestartProbe.RestartHistoryRecord> ReadRestartHistory(ProbeResult probeResult, DateTime cutOffTime)
		{
			List<HostContollerNodeRestartProbe.RestartHistoryRecord> list = new List<HostContollerNodeRestartProbe.RestartHistoryRecord>();
			if (!string.IsNullOrEmpty(probeResult.StateAttribute3))
			{
				string[] array = probeResult.StateAttribute3.Split(new char[]
				{
					'`'
				});
				foreach (string text in array)
				{
					string[] array3 = text.Split(new char[]
					{
						'|'
					});
					if (array3.Length == 3)
					{
						HostContollerNodeRestartProbe.RestartHistoryRecord item = default(HostContollerNodeRestartProbe.RestartHistoryRecord);
						item.StartTime = DateTime.SpecifyKind(DateTime.Parse(array3[0]), DateTimeKind.Utc);
						item.EndTime = DateTime.SpecifyKind(DateTime.Parse(array3[1]), DateTimeKind.Utc);
						item.RestartCount = int.Parse(array3[2]);
						if (item.EndTime >= cutOffTime)
						{
							list.Add(item);
						}
					}
				}
			}
			list.Sort((HostContollerNodeRestartProbe.RestartHistoryRecord x, HostContollerNodeRestartProbe.RestartHistoryRecord y) => x.StartTime.CompareTo(y.StartTime));
			return list;
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000A7B68 File Offset: 0x000A5D68
		private string FormatRestartHistory(List<HostContollerNodeRestartProbe.RestartHistoryRecord> records)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (HostContollerNodeRestartProbe.RestartHistoryRecord restartHistoryRecord in records)
			{
				stringBuilder.AppendFormat(Strings.HostControllerNodeRestartDetails(restartHistoryRecord.StartTime.ToString(), restartHistoryRecord.EndTime.ToString(), restartHistoryRecord.RestartCount.ToString()), new object[0]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040013AF RID: 5039
		private const string RestartHistoryRecordTemplate = "{0}|{1}|{2}`";

		// Token: 0x040013B0 RID: 5040
		private const int StateAttributeLenghCap = 768;

		// Token: 0x02000465 RID: 1125
		private struct RestartHistoryRecord
		{
			// Token: 0x17000618 RID: 1560
			// (get) Token: 0x06001C8C RID: 7308 RVA: 0x000A7C18 File Offset: 0x000A5E18
			// (set) Token: 0x06001C8D RID: 7309 RVA: 0x000A7C20 File Offset: 0x000A5E20
			public DateTime StartTime { get; set; }

			// Token: 0x17000619 RID: 1561
			// (get) Token: 0x06001C8E RID: 7310 RVA: 0x000A7C29 File Offset: 0x000A5E29
			// (set) Token: 0x06001C8F RID: 7311 RVA: 0x000A7C31 File Offset: 0x000A5E31
			public DateTime EndTime { get; set; }

			// Token: 0x1700061A RID: 1562
			// (get) Token: 0x06001C90 RID: 7312 RVA: 0x000A7C3A File Offset: 0x000A5E3A
			// (set) Token: 0x06001C91 RID: 7313 RVA: 0x000A7C42 File Offset: 0x000A5E42
			public int RestartCount { get; set; }
		}
	}
}
