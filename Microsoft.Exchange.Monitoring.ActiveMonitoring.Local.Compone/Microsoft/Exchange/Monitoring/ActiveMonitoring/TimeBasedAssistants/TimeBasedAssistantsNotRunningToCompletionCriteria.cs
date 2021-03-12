using System;
using System.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x0200052F RID: 1327
	internal class TimeBasedAssistantsNotRunningToCompletionCriteria : TimeBasedAssistantsLastNCriteria
	{
		// Token: 0x060020AE RID: 8366 RVA: 0x000C73DC File Offset: 0x000C55DC
		public TimeBasedAssistantsNotRunningToCompletionCriteria() : base(TimeBasedAssistantsNotRunningToCompletionCriteria.workcycleAlertClassifications)
		{
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000C746C File Offset: 0x000C566C
		protected override bool IsSatisfied(AssistantInfo assistantInfo, MailboxDatabase database, WindowJob[] history)
		{
			ArgumentValidator.ThrowIfNull("assistantInfo", assistantInfo);
			ArgumentValidator.ThrowIfNull("database", database);
			ArgumentValidator.ThrowIfNull("history", history);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("checkpoint", assistantInfo.WorkcycleCheckpointLength, TimeSpan.Zero, TimeSpan.MaxValue);
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			if (!snapshot.MailboxAssistants.GetObject<IMailboxAssistantSettings>(assistantInfo.AssistantName).CompletionMonitoringEnabled)
			{
				return true;
			}
			DateTime startTime = database.StartTime;
			TimeSpan workcycleCheckpointLength = assistantInfo.WorkcycleCheckpointLength;
			TimeSpan t = workcycleCheckpointLength.Add(workcycleCheckpointLength);
			bool flag = history.Any<WindowJob>();
			if (!flag && this.CurrentSampleTime.Subtract(startTime) < t)
			{
				return true;
			}
			if (!flag && this.CurrentSampleTime.Subtract(startTime) >= t)
			{
				return false;
			}
			Array.Sort<WindowJob>(history, (WindowJob x1, WindowJob x2) => x1.StartTime.CompareTo(x2.StartTime));
			if (this.CurrentSampleTime.Subtract(history.Last<WindowJob>().EndTime).Duration() > t)
			{
				return false;
			}
			Func<WindowJob, bool> alertCondition = delegate(WindowJob wc)
			{
				DateTime startTime2 = wc.StartTime;
				DateTime endTime = wc.EndTime;
				if (startTime2 > endTime)
				{
					return true;
				}
				int totalOnDatabaseMailboxCount = wc.TotalOnDatabaseMailboxCount;
				int interestingMailboxCount = wc.InterestingMailboxCount;
				int notInterestingMailboxCount = wc.NotInterestingMailboxCount;
				int filteredMailboxCount = wc.FilteredMailboxCount;
				int failedFilteringMailboxCount = wc.FailedFilteringMailboxCount;
				return totalOnDatabaseMailboxCount != interestingMailboxCount + notInterestingMailboxCount + filteredMailboxCount + failedFilteringMailboxCount;
			};
			return base.IsSatisfiedForLastN(assistantInfo, database, history, alertCondition);
		}

		// Token: 0x040017F5 RID: 6133
		private static readonly TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification[] workcycleAlertClassifications = new TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification[]
		{
			new TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification
			{
				WorkcycleMinutesMax = (int)TimeSpan.FromHours(24.0).TotalMinutes,
				AlertTimeThresholdMinutes = (int)TimeSpan.FromHours(4.0).TotalMinutes
			}
		};
	}
}
