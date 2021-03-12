using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000032 RID: 50
	public class ScheduledCheckTask : IntegrityCheckTaskBase
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00008234 File Offset: 0x00006434
		public ScheduledCheckTask(IJobExecutionTracker tracker) : base(new ScheduledCheckTask.AggregateJobExecutionTracker(tracker))
		{
			ScheduledCheckTaskConfiguration configuration = ScheduledCheckTaskConfiguration.GetConfiguration();
			this.isEnabled = configuration.IsEnabled;
			this.aggregateTracker = (ScheduledCheckTask.AggregateJobExecutionTracker)base.JobExecutionTracker;
			this.tasksDetectOnly = new List<IIntegrityCheckTask>(configuration.TaskIdsDetectOnly.Count);
			foreach (TaskId taskId in configuration.TaskIdsDetectOnly)
			{
				this.tasksDetectOnly.Add(TaskBuilder.Create(taskId).TrackedBy(this.aggregateTracker).Build());
			}
			this.tasksDetectAndFix = new List<IIntegrityCheckTask>(configuration.TaskIdsDetectAndFix.Count);
			foreach (TaskId taskId2 in configuration.TaskIdsDetectAndFix)
			{
				this.tasksDetectAndFix.Add(TaskBuilder.Create(taskId2).TrackedBy(this.aggregateTracker).Build());
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000835C File Offset: 0x0000655C
		public override string TaskName
		{
			get
			{
				return "ScheduledCheckTask";
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000083F8 File Offset: 0x000065F8
		public override ErrorCode Execute(Context context, MailboxEntry mailboxEntry, bool detectOnly, Func<bool> shouldContinue)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			Stopwatch stopwatch = Stopwatch.StartNew();
			if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug(0L, "Integrity check task \"{0}\" invoked on mailbox {1}, detect only={2}, enabled={3}", new object[]
				{
					this.TaskName,
					mailboxEntry.MailboxGuid,
					detectOnly,
					this.isEnabled
				});
			}
			foreach (IIntegrityCheckTask integrityCheckTask in this.tasksDetectOnly)
			{
				ErrorCode errorCode2 = integrityCheckTask.Execute(context, mailboxEntry.MailboxGuid, true, base.IsScheduled, shouldContinue);
				if (errorCode2 != ErrorCode.NoError && errorCode == ErrorCode.NoError)
				{
					errorCode = errorCode2;
				}
			}
			foreach (IIntegrityCheckTask integrityCheckTask2 in this.tasksDetectAndFix)
			{
				ErrorCode first = integrityCheckTask2.Execute(context, mailboxEntry.MailboxGuid, !base.IsScheduled && detectOnly, base.IsScheduled, shouldContinue);
				if (first != ErrorCode.NoError && errorCode == ErrorCode.NoError)
				{
					errorCode = first.Propagate((LID)37660U);
				}
			}
			stopwatch.Stop();
			StorePerDatabasePerformanceCountersInstance databaseInstance = PerformanceCounterFactory.GetDatabaseInstance(context.Database);
			if (base.IsScheduled && databaseInstance != null)
			{
				databaseInstance.ScheduledISIntegMailboxRate.Increment();
			}
			if (errorCode == ErrorCode.NoError && base.IsScheduled && !detectOnly)
			{
				errorCode = IntegrityCheckTaskBase.LockMailboxForOperation(context, mailboxEntry.MailboxNumber, delegate(MailboxState mailboxState)
				{
					using (Mailbox mailbox = Mailbox.OpenMailbox(context, mailboxState))
					{
						mailbox.SetISIntegScheduledLast(context, mailboxState.UtcNow, new int?((int)stopwatch.ElapsedMilliseconds), new int?(this.aggregateTracker.CorruptionCount));
						mailbox.Save(context);
					}
					mailboxState.CleanupAsNonActive(context);
					return ErrorCode.NoError;
				});
				if (errorCode != ErrorCode.NoError)
				{
					return errorCode.Propagate((LID)54044U);
				}
			}
			return errorCode;
		}

		// Token: 0x040000B4 RID: 180
		private readonly bool isEnabled;

		// Token: 0x040000B5 RID: 181
		private readonly List<IIntegrityCheckTask> tasksDetectOnly;

		// Token: 0x040000B6 RID: 182
		private readonly List<IIntegrityCheckTask> tasksDetectAndFix;

		// Token: 0x040000B7 RID: 183
		private ScheduledCheckTask.AggregateJobExecutionTracker aggregateTracker;

		// Token: 0x02000033 RID: 51
		internal class AggregateJobExecutionTracker : IJobExecutionTracker, IProgress<short>
		{
			// Token: 0x06000102 RID: 258 RVA: 0x00008630 File Offset: 0x00006830
			public AggregateJobExecutionTracker(IJobExecutionTracker tracker)
			{
				this.baseTracker = tracker;
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000103 RID: 259 RVA: 0x0000863F File Offset: 0x0000683F
			public int CorruptionCount
			{
				get
				{
					return this.corruptionCount;
				}
			}

			// Token: 0x06000104 RID: 260 RVA: 0x00008647 File Offset: 0x00006847
			void IJobExecutionTracker.OnCorruptionDetected(Corruption corruption)
			{
				this.corruptionCount++;
				this.baseTracker.OnCorruptionDetected(corruption);
			}

			// Token: 0x06000105 RID: 261 RVA: 0x00008663 File Offset: 0x00006863
			public void Report(short progress)
			{
				this.baseTracker.Report(progress);
			}

			// Token: 0x040000B8 RID: 184
			private int corruptionCount;

			// Token: 0x040000B9 RID: 185
			private IJobExecutionTracker baseTracker;
		}
	}
}
