using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000023 RID: 35
	public class JobRunner : IJobExecutionTracker, IProgress<short>
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00006234 File Offset: 0x00004434
		public JobRunner(IIntegrityCheckJob job, IJobStateTracker jobStateReporter, IJobProgressTracker jobProgressReporter)
		{
			this.job = job;
			this.jobStateReporter = jobStateReporter;
			this.jobProgressReporter = jobProgressReporter;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006251 File Offset: 0x00004451
		public JobRunner AssignJob(IIntegrityCheckJob job)
		{
			this.job = job;
			return this;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000625E File Offset: 0x0000445E
		public void Run(Context context)
		{
			this.timeWatcher = new Stopwatch();
			this.timeWatcher.Start();
			this.InternalExecute(context, () => true);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000629C File Offset: 0x0000449C
		public void Report(short progress)
		{
			this.progress = progress;
			this.jobProgressReporter.Report(new ProgressInfo
			{
				Progress = progress,
				LastExecutionTime = new DateTime?(DateTime.UtcNow),
				CorruptionsDetected = this.corruptionsDetected,
				CorruptionsFixed = this.corruptionsFixed,
				TimeInServer = this.timeWatcher.Elapsed
			});
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00006304 File Offset: 0x00004504
		void IJobExecutionTracker.OnCorruptionDetected(Corruption corruption)
		{
			if (this.corruptions == null)
			{
				this.corruptions = new List<Corruption>();
			}
			this.corruptions.Add(corruption);
			this.corruptionsDetected++;
			if (corruption.IsFixed)
			{
				this.corruptionsFixed++;
			}
			this.jobProgressReporter.Report(new ProgressInfo
			{
				Progress = this.progress,
				LastExecutionTime = new DateTime?(DateTime.UtcNow),
				CorruptionsDetected = this.corruptionsDetected,
				CorruptionsFixed = this.corruptionsFixed,
				TimeInServer = this.timeWatcher.Elapsed
			});
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000063AC File Offset: 0x000045AC
		private void InternalExecute(Context context, Func<bool> shouldTaskContinue)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			IIntegrityCheckTask integrityCheckTask = TaskBuilder.Create(this.job.TaskId).TrackedBy(this).Build();
			bool isScheduled = this.job.Source == JobSource.Maintenance;
			bool flag = true;
			try
			{
				errorCode = integrityCheckTask.Execute(context, this.job.MailboxGuid, this.job.DetectOnly, isScheduled, shouldTaskContinue).Propagate((LID)36956U);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					errorCode = ErrorCode.CreateErrorCanNotComplete((LID)60508U);
				}
				JobState state = (errorCode == ErrorCode.NoError) ? JobState.Completed : JobState.Failed;
				this.jobProgressReporter.Report(new ProgressInfo
				{
					Progress = 100,
					LastExecutionTime = new DateTime?(DateTime.UtcNow),
					CompletedTime = new DateTime?(DateTime.UtcNow),
					CorruptionsDetected = this.corruptionsDetected,
					CorruptionsFixed = this.corruptionsFixed,
					TimeInServer = this.timeWatcher.Elapsed,
					Corruptions = this.corruptions,
					Error = errorCode
				});
				this.jobStateReporter.MoveToState(state);
			}
		}

		// Token: 0x04000086 RID: 134
		private IIntegrityCheckJob job;

		// Token: 0x04000087 RID: 135
		private IJobStateTracker jobStateReporter;

		// Token: 0x04000088 RID: 136
		private IJobProgressTracker jobProgressReporter;

		// Token: 0x04000089 RID: 137
		private short progress;

		// Token: 0x0400008A RID: 138
		private int corruptionsDetected;

		// Token: 0x0400008B RID: 139
		private int corruptionsFixed;

		// Token: 0x0400008C RID: 140
		private List<Corruption> corruptions;

		// Token: 0x0400008D RID: 141
		private Stopwatch timeWatcher;
	}
}
