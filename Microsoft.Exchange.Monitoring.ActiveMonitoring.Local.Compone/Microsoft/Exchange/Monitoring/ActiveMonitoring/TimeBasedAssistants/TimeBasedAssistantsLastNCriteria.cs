using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x0200052C RID: 1324
	internal abstract class TimeBasedAssistantsLastNCriteria : TimeBasedAssistantsCriteria
	{
		// Token: 0x060020A0 RID: 8352 RVA: 0x000C7164 File Offset: 0x000C5364
		protected TimeBasedAssistantsLastNCriteria(IEnumerable<TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification> alertThresholds)
		{
			ArgumentValidator.ThrowIfNull("alertThresholds", alertThresholds);
			this.alertThresholds = alertThresholds.ToList<TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification>();
			this.FailedCriteriaRecordList = new List<TimeBasedAssistantsLastNCriteria.FailedCriteriaRecord>();
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x000C718E File Offset: 0x000C538E
		// (set) Token: 0x060020A2 RID: 8354 RVA: 0x000C7196 File Offset: 0x000C5396
		public List<TimeBasedAssistantsLastNCriteria.FailedCriteriaRecord> FailedCriteriaRecordList { get; private set; }

		// Token: 0x060020A3 RID: 8355 RVA: 0x000C71B8 File Offset: 0x000C53B8
		protected bool IsSatisfiedForLastN(AssistantInfo assistantInfo, MailboxDatabase database, WindowJob[] history, Func<WindowJob, bool> alertCondition)
		{
			ArgumentValidator.ThrowIfNull("assistantInfo", assistantInfo);
			ArgumentValidator.ThrowIfNull("database", database);
			ArgumentValidator.ThrowIfNull("history", history);
			ArgumentValidator.ThrowIfNull("alertCondition", alertCondition);
			int checkpointHistoryEvaluationThreshold = this.GetCheckpointHistoryEvaluationThreshold(assistantInfo);
			int num = history.Length;
			if (num == 0 || num < checkpointHistoryEvaluationThreshold)
			{
				return true;
			}
			int num2 = num - checkpointHistoryEvaluationThreshold;
			List<WindowJob> source = (num2 > 0) ? history.Skip(num2).ToList<WindowJob>() : history.ToList<WindowJob>();
			bool flag = source.Any((WindowJob windowJob) => !alertCondition(windowJob));
			if (!flag)
			{
				this.FailedCriteriaRecordList.Add(new TimeBasedAssistantsLastNCriteria.FailedCriteriaRecord
				{
					AssistantName = assistantInfo.AssistantName,
					DatabaseGuid = database.Guid
				});
			}
			return flag;
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000C72A0 File Offset: 0x000C54A0
		private int GetCheckpointHistoryEvaluationThreshold(AssistantInfo assistantInfo)
		{
			ArgumentValidator.ThrowIfNull("assistantInfo", assistantInfo);
			int workcycleMinutes = (int)assistantInfo.WorkcycleLength.TotalMinutes;
			int num = (int)assistantInfo.WorkcycleCheckpointLength.TotalMinutes;
			List<TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification> list = (from t in this.alertThresholds
			where t.WorkcycleMinutesMax >= workcycleMinutes
			select t).ToList<TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification>();
			if (!list.Any<TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification>() || num == 0)
			{
				return 1;
			}
			TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification workcycleAlertClassification = list.First<TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification>();
			int num2 = workcycleAlertClassification.WorkcycleMinutesMax - workcycleMinutes;
			foreach (TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification workcycleAlertClassification2 in list)
			{
				int num3 = workcycleAlertClassification2.WorkcycleMinutesMax - workcycleMinutes;
				if (num3 < num2)
				{
					num2 = num3;
					workcycleAlertClassification = workcycleAlertClassification2;
				}
			}
			return Math.Max(1, workcycleAlertClassification.AlertTimeThresholdMinutes / num);
		}

		// Token: 0x040017EF RID: 6127
		private readonly List<TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification> alertThresholds;

		// Token: 0x0200052D RID: 1325
		internal class FailedCriteriaRecord
		{
			// Token: 0x170006A8 RID: 1704
			// (get) Token: 0x060020A5 RID: 8357 RVA: 0x000C7390 File Offset: 0x000C5590
			// (set) Token: 0x060020A6 RID: 8358 RVA: 0x000C7398 File Offset: 0x000C5598
			public string AssistantName { get; set; }

			// Token: 0x170006A9 RID: 1705
			// (get) Token: 0x060020A7 RID: 8359 RVA: 0x000C73A1 File Offset: 0x000C55A1
			// (set) Token: 0x060020A8 RID: 8360 RVA: 0x000C73A9 File Offset: 0x000C55A9
			public Guid DatabaseGuid { get; set; }
		}

		// Token: 0x0200052E RID: 1326
		internal struct WorkcycleAlertClassification
		{
			// Token: 0x170006AA RID: 1706
			// (get) Token: 0x060020AA RID: 8362 RVA: 0x000C73BA File Offset: 0x000C55BA
			// (set) Token: 0x060020AB RID: 8363 RVA: 0x000C73C2 File Offset: 0x000C55C2
			public int WorkcycleMinutesMax { get; set; }

			// Token: 0x170006AB RID: 1707
			// (get) Token: 0x060020AC RID: 8364 RVA: 0x000C73CB File Offset: 0x000C55CB
			// (set) Token: 0x060020AD RID: 8365 RVA: 0x000C73D3 File Offset: 0x000C55D3
			public int AlertTimeThresholdMinutes { get; set; }
		}
	}
}
