using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000523 RID: 1315
	internal class InfrastructureValidationCriteria : TimeBasedAssistantsCriteria
	{
		// Token: 0x06002058 RID: 8280 RVA: 0x000C5F10 File Offset: 0x000C4110
		protected override bool IsSatisfied(AssistantInfo assistantInfo, MailboxDatabase database, WindowJob[] history)
		{
			ArgumentValidator.ThrowIfNull("assistantInfo", assistantInfo);
			ArgumentValidator.ThrowIfNull("database", database);
			ArgumentValidator.ThrowIfNull("history", history);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("checkpoint", assistantInfo.WorkcycleCheckpointLength, TimeSpan.Zero, TimeSpan.MaxValue);
			DateTime startTime = database.StartTime;
			TimeSpan workcycleCheckpointLength = assistantInfo.WorkcycleCheckpointLength;
			TimeSpan ts = new TimeSpan(workcycleCheckpointLength.Duration().Ticks / 2L);
			if (!history.Any<WindowJob>() && this.CurrentSampleTime.Subtract(startTime) < workcycleCheckpointLength.Add(ts))
			{
				return true;
			}
			if (!history.Any<WindowJob>() && this.CurrentSampleTime.Subtract(startTime) >= workcycleCheckpointLength.Add(ts))
			{
				return false;
			}
			if (history.Length == 0)
			{
				return false;
			}
			Array.Sort<WindowJob>(history, (WindowJob x1, WindowJob x2) => x2.StartTime.CompareTo(x1.StartTime));
			if (this.CurrentSampleTime > history.First<WindowJob>().EndTime && this.CurrentSampleTime.Subtract(history.First<WindowJob>().EndTime).Duration() > workcycleCheckpointLength.Add(ts))
			{
				return false;
			}
			int num = Math.Min(history.Length, 2);
			DateTime dateTime = this.CurrentSampleTime;
			for (int i = 0; i < num; i++)
			{
				WindowJob windowJob = history[i];
				DateTime startTime2 = windowJob.StartTime;
				if (dateTime.Subtract(startTime2).Duration() > workcycleCheckpointLength.Add(ts).Duration())
				{
					return false;
				}
				if (!InfrastructureValidationCriteria.AllMailboxesWereProcessed(windowJob))
				{
					return false;
				}
				dateTime = startTime2;
			}
			return true;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000C60B5 File Offset: 0x000C42B5
		private static bool AllMailboxesWereProcessed(WindowJob workJob)
		{
			return workJob.InterestingMailboxCount == workJob.CompletedMailboxCount + workJob.FailedMailboxCount + workJob.MovedToOnDemandMailboxCount - workJob.RetriedMailboxCount;
		}
	}
}
