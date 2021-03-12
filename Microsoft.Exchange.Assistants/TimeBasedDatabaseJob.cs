using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Assistants.Diagnostics;
using Microsoft.Exchange.Assistants.EventLog;
using Microsoft.Exchange.Assistants.Logging;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200008C RID: 140
	internal abstract class TimeBasedDatabaseJob : Base
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x00014F3C File Offset: 0x0001313C
		protected TimeBasedDatabaseJob(TimeBasedDatabaseDriver driver, List<MailboxData> queue, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters)
		{
			this.Finished = false;
			if (driver == null)
			{
				throw new ArgumentNullException("driver", "Time based Database Job can not be started with a null driver");
			}
			this.Driver = driver;
			this.SetPendingQueue(queue);
			this.poisonControl = poisonControl;
			this.performanceCounters = databaseCounters;
			this.initialPendingQueueCount = this.pendingQueue.Count;
			this.OnDemandMailboxCount = 0;
			this.mailboxesProcessedFailureCount = 0;
			this.mailboxesProcessedSuccessfullyCount = 0;
			this.mailboxesFailedToOpenStoreSessionCount = 0;
			this.mailboxesRetriedCount = 0;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00014FE6 File Offset: 0x000131E6
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00014FEE File Offset: 0x000131EE
		private protected TimeBasedDatabaseDriver Driver { protected get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00014FF7 File Offset: 0x000131F7
		protected int InterestingMailboxCount
		{
			get
			{
				return this.initialPendingQueueCount;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00014FFF File Offset: 0x000131FF
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x00015007 File Offset: 0x00013207
		private protected int OnDemandMailboxCount { protected get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00015010 File Offset: 0x00013210
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x00015018 File Offset: 0x00013218
		public DateTime StartTime { get; protected set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00015021 File Offset: 0x00013221
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x00015029 File Offset: 0x00013229
		public DateTime EndTime { get; protected set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00015032 File Offset: 0x00013232
		public TimeSpan Duration
		{
			get
			{
				if (this.Finished)
				{
					return this.EndTime - this.StartTime;
				}
				return DateTime.UtcNow - this.StartTime;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0001505E File Offset: 0x0001325E
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x00015066 File Offset: 0x00013266
		public bool Finished { get; private set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00015070 File Offset: 0x00013270
		public int MailboxesQueued
		{
			get
			{
				int result;
				lock (this.instanceLock)
				{
					result = this.activeQueue.Count + this.pendingQueue.Count;
				}
				return result;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x000150C4 File Offset: 0x000132C4
		internal ITimeBasedAssistant Assistant
		{
			get
			{
				return this.Driver.Assistant;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x000150D1 File Offset: 0x000132D1
		protected DatabaseInfo DatabaseInfo
		{
			get
			{
				return this.Driver.DatabaseInfo;
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000150E0 File Offset: 0x000132E0
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Concat(new string[]
				{
					base.GetType().Name,
					" for database '",
					this.Driver.DatabaseInfo.DisplayName,
					"', assistant ",
					this.Driver.Assistant.GetType().Name
				});
			}
			return this.toString;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00015158 File Offset: 0x00013358
		public AssistantTaskContext ProcessNextMailbox(AssistantTaskContext context)
		{
			ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceDebug<TimeBasedDatabaseJob>((long)this.GetHashCode(), "{0}: ProcessNextMailbox", this);
			MailboxData mailboxData = context.MailboxData;
			lock (this.instanceLock)
			{
				if (mailboxData == null)
				{
					ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceDebug<TimeBasedDatabaseJob>((long)this.GetHashCode(), "{0}: No more mailboxes to process", this);
					return null;
				}
				if (!this.loggedBegin)
				{
					ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceDebug<TimeBasedDatabaseJob, int>((long)this.GetHashCode(), "{0}: Processing first mailbox out of {1}", this, this.initialPendingQueueCount);
					this.LogJobBegin(this.initialPendingQueueCount);
					this.loggedBegin = true;
				}
			}
			AssistantTaskContext assistantTaskContext = this.ProcessMailbox(context);
			AssistantTaskContext result;
			lock (this.instanceLock)
			{
				if (assistantTaskContext == null)
				{
					ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceDebug((long)this.GetHashCode(), "{0}: Finished processing of mailbox.  mailboxesProcessedSuccessfully: {1}, mailboxesProcessedError: {2}, mailboxesFailedToOpenStoreSession: {3}, mailboxesRetriedCount: {4},remaining: {5}", new object[]
					{
						this,
						this.mailboxesProcessedSuccessfullyCount,
						this.mailboxesProcessedFailureCount,
						this.mailboxesFailedToOpenStoreSessionCount,
						this.mailboxesRetriedCount,
						this.pendingQueue.Count
					});
				}
				result = assistantTaskContext;
			}
			return result;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000152C0 File Offset: 0x000134C0
		public uint RequestStop()
		{
			uint num = 0U;
			ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceDebug<TimeBasedDatabaseJob>((long)this.GetHashCode(), "{0}: RequestingStop...", this);
			lock (this.instanceLock)
			{
				num = (uint)this.pendingQueue.Count;
				this.Driver.DecrementNumberOfMailboxes(this.pendingQueue.Count);
				this.pendingQueue.Clear();
				this.FinishIfNecessary();
			}
			base.TracePfd("PFD AIS {0} {1}: RequestedStop.  Skipping {2} mailboxes", new object[]
			{
				27223,
				this,
				num
			});
			return num;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00015374 File Offset: 0x00013574
		public bool Remove(MailboxData mailbox)
		{
			lock (this.instanceLock)
			{
				if (this.pendingQueue.Remove(mailbox))
				{
					this.OnDemandMailboxCount++;
					this.Driver.DecrementNumberOfMailboxes();
					ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceDebug((long)this.GetHashCode(), "{0}: Removed mailbox.  total removed: {1}, mailboxesProcessedSuccessfully: {2}, mailboxesProcessedError: {3}, mailboxesFailedToOpenStoreSession: {4}, mailboxesRetriedCount: {5},remaining: {6}", new object[]
					{
						this,
						this.OnDemandMailboxCount,
						this.mailboxesProcessedSuccessfullyCount,
						this.mailboxesProcessedFailureCount,
						this.mailboxesFailedToOpenStoreSessionCount,
						this.mailboxesRetriedCount,
						this.pendingQueue.Count
					});
					this.FinishIfNecessary();
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00015460 File Offset: 0x00013660
		public bool IsMailboxActiveOrPending(MailboxData mailbox)
		{
			bool result;
			lock (this.instanceLock)
			{
				result = (this.activeQueue.Contains(mailbox) || this.pendingQueue.Contains(mailbox));
			}
			return result;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000154BC File Offset: 0x000136BC
		public bool HasTask()
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			bool spreadLoad = snapshot.MailboxAssistants.GetObject<IMailboxAssistantSettings>(this.Driver.AssistantType.Identifier, new object[0]).SpreadLoad;
			lock (this.instanceLock)
			{
				switch (this.Driver.Governor.GetHierarchyStatus())
				{
				case GovernorStatus.Retry:
					if (!this.retry)
					{
						return false;
					}
					this.retry = false;
					break;
				case GovernorStatus.Failure:
					return false;
				}
				if (this.pendingQueue == null || this.pendingQueue.Count == 0)
				{
					return false;
				}
				if (spreadLoad)
				{
					float num = (float)(this.InterestingMailboxCount - this.pendingQueue.Count - 1) / (float)this.InterestingMailboxCount;
					float num2 = (float)this.Duration.Ticks / (float)this.Driver.AssistantType.WorkCycleCheckpoint.Ticks;
					if (num > num2)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000155F4 File Offset: 0x000137F4
		public DiagnosticsSummaryJob GetJobDiagnosticsSummary()
		{
			int processing;
			int queued;
			lock (this.instanceLock)
			{
				processing = ((this.activeQueue == null) ? 0 : this.activeQueue.Count);
				queued = ((this.pendingQueue == null) ? 0 : this.pendingQueue.Count);
			}
			return new DiagnosticsSummaryJob(processing, this.mailboxesProcessedSuccessfullyCount, this.mailboxesProcessedFailureCount, this.mailboxesFailedToOpenStoreSessionCount, this.mailboxesRetriedCount, queued);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00015684 File Offset: 0x00013884
		public List<Guid> GetMailboxGuidList(bool isActive)
		{
			List<Guid> list = new List<Guid>();
			lock (this.instanceLock)
			{
				list.AddRange(from mbx in isActive ? this.activeQueue : this.pendingQueue
				select mbx.MailboxGuid);
			}
			return list;
		}

		// Token: 0x06000442 RID: 1090
		protected abstract void LogJobBegin(int initialPendingQueueCount);

		// Token: 0x06000443 RID: 1091
		protected abstract void LogJobEnd(int initialPendingQueueCount, int mailboxesProcessedSuccessfullyCount, int mailboxesProcessedFailureCount, int mailboxesFailedToOpenStoreSessionCount, int mailboxesProcessedSeparatelyCount, int mailboxesRetriedCount);

		// Token: 0x06000444 RID: 1092 RVA: 0x00015700 File Offset: 0x00013900
		private void Finish()
		{
			if (this.loggedBegin)
			{
				ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceDebug((long)this.GetHashCode(), "{0}: finishing.  initialPendingQueueCount: {1}, processed successfully: {2}, failure count: {3}, failed to open store session: {4}, separately: {5},retried: {6}", new object[]
				{
					this,
					this.initialPendingQueueCount,
					this.mailboxesProcessedSuccessfullyCount,
					this.mailboxesProcessedFailureCount,
					this.mailboxesFailedToOpenStoreSessionCount,
					this.OnDemandMailboxCount,
					this.mailboxesRetriedCount
				});
				this.LogJobEnd(this.initialPendingQueueCount, this.mailboxesProcessedSuccessfullyCount, this.mailboxesProcessedFailureCount, this.mailboxesFailedToOpenStoreSessionCount, this.OnDemandMailboxCount, this.mailboxesRetriedCount);
				this.LogSkippedMailboxes();
			}
			else
			{
				this.StartTime = DateTime.UtcNow;
				this.EndTime = DateTime.UtcNow;
			}
			this.Finished = true;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000157E0 File Offset: 0x000139E0
		public MailboxData GetNextMailbox()
		{
			MailboxData result;
			lock (this.instanceLock)
			{
				if (this.pendingQueue.Count <= 0)
				{
					result = null;
				}
				else
				{
					MailboxData mailboxData = this.pendingQueue[0];
					this.pendingQueue.RemoveAt(0);
					this.activeQueue.Add(mailboxData);
					result = mailboxData;
				}
			}
			return result;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00015854 File Offset: 0x00013A54
		internal void LogAIException(MailboxData mailbox, AIException e)
		{
			ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceError<TimeBasedDatabaseJob, string, AIException>((long)this.GetHashCode(), "{0}: Exception on mailbox {1}: {2}", this, (mailbox == null) ? "No Mailbox Present" : mailbox.DisplayName, e);
			base.LogEvent(AssistantsEventLogConstants.Tuple_TimeBasedAssistantFailed, null, new object[]
			{
				this.Assistant.Name,
				(mailbox == null) ? "No Mailbox Present" : mailbox.MailboxGuid.ToString(),
				e
			});
			AssistantsLog.LogErrorProcessingMailboxEvent(this.Assistant.NonLocalizedName, mailbox, e, this.DatabaseInfo.DatabaseName, this.StartTime.ToString("O"), (this is TimeBasedDatabaseWindowJob) ? MailboxSlaRequestType.Scheduled : MailboxSlaRequestType.OnDemand);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00015911 File Offset: 0x00013B11
		private void SetPendingQueue(List<MailboxData> queue)
		{
			this.pendingQueue = queue;
			this.Driver.IncrementNumberOfMailboxes(queue.Count);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00015954 File Offset: 0x00013B54
		private AssistantTaskContext ProcessMailbox(AssistantTaskContext context)
		{
			TimeBasedDatabaseJob.<>c__DisplayClassd CS$<>8__locals1 = new TimeBasedDatabaseJob.<>c__DisplayClassd();
			CS$<>8__locals1.context = context;
			CS$<>8__locals1.<>4__this = this;
			Guid mailboxGuid = CS$<>8__locals1.context.MailboxData.MailboxGuid;
			CS$<>8__locals1.nextContext = null;
			if (this.poisonControl.IsPoisonMailbox(mailboxGuid))
			{
				ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceDebug<TimeBasedDatabaseJob, string, int>((long)this.GetHashCode(), "{0}: Poison mailbox detected and skipped. Mailbox: {1}, crashCount: {2}", this, (CS$<>8__locals1.context.MailboxData is StoreMailboxData) ? ((StoreMailboxData)CS$<>8__locals1.context.MailboxData).DisplayName : string.Empty, this.poisonControl.GetCrashCount(mailboxGuid));
				if (Test.NotifyPoisonMailboxSkipped != null)
				{
					Test.NotifyPoisonMailboxSkipped(this.DatabaseInfo, mailboxGuid);
				}
				this.mailboxesProcessedFailureCount++;
				return null;
			}
			CS$<>8__locals1.kit = new EmergencyKit(mailboxGuid);
			this.poisonControl.PoisonCall(CS$<>8__locals1.kit, new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ProcessMailbox>b__c)));
			return CS$<>8__locals1.nextContext;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00015ABC File Offset: 0x00013CBC
		private AssistantTaskContext ProcessMailboxUnderPoisonControl(AssistantTaskContext context, EmergencyKit kit)
		{
			AIException exception = null;
			AssistantTaskContext nextContext = null;
			try
			{
				base.CatchMeIfYouCan(delegate
				{
					AdminRpcMailboxData adminRpcMailboxData = context.MailboxData as AdminRpcMailboxData;
					if (adminRpcMailboxData != null)
					{
						nextContext = this.ProcessAdminRpcMailboxUnderPoisonControl(context, kit);
						return;
					}
					StoreMailboxData storeMailboxData = context.MailboxData as StoreMailboxData;
					if (storeMailboxData != null)
					{
						nextContext = this.ProcessStoreMailboxUnderPoisonControl(context, kit);
					}
				}, this.Assistant.NonLocalizedName);
			}
			catch (AIException ex)
			{
				this.LogAIException(context.MailboxData, ex);
				exception = ex;
			}
			this.PostProcessMailbox(exception, nextContext, context.MailboxData);
			return nextContext;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00015B58 File Offset: 0x00013D58
		private AssistantTaskContext ProcessAdminRpcMailboxUnderPoisonControl(AssistantTaskContext context, EmergencyKit kit)
		{
			TimeBasedDatabaseJob.processMailboxTestHook.Value();
			AssistantTaskContext result = null;
			Guid activityId = (ActivityContext.ActivityId != null) ? ActivityContext.ActivityId.Value : Guid.Empty;
			if (context.Args == null)
			{
				context.Args = InvokeArgs.Create(null, this.Driver.TimePerTask, context.MailboxData);
			}
			AssistantsLog.LogStartProcessingMailboxEvent(activityId, this.Assistant as AssistantBase, context.MailboxData.MailboxGuid, context.MailboxData.DisplayName, this);
			try
			{
				kit.SetContext(this.Assistant, context.MailboxData);
				result = context.Step(context);
			}
			finally
			{
				kit.UnsetContext();
			}
			AssistantsLog.LogEndProcessingMailboxEvent(activityId, this.Assistant as AssistantBase, context.CustomDataToLog, context.MailboxData.MailboxGuid, context.MailboxData.DisplayName, this);
			return result;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00015C50 File Offset: 0x00013E50
		private AssistantTaskContext ProcessStoreMailboxUnderPoisonControl(AssistantTaskContext context, EmergencyKit kit)
		{
			StoreMailboxData storeMailboxData = context.MailboxData as StoreMailboxData;
			AssistantTaskContext result = null;
			Guid activityId = (ActivityContext.ActivityId != null) ? ActivityContext.ActivityId.Value : Guid.Empty;
			base.TracePfd("PFD AIS {0} {1}: ProcessMailbox: {2}", new object[]
			{
				23127,
				this,
				storeMailboxData.DisplayName
			});
			AssistantsLog.LogStartProcessingMailboxEvent(activityId, this.Assistant as AssistantBase, storeMailboxData.MailboxGuid, storeMailboxData.DisplayName, this);
			bool flag = false;
			IMailboxFilter mailboxFilter = this.Driver.AssistantType as IMailboxFilter;
			if (mailboxFilter != null && mailboxFilter.MailboxType.Contains(MailboxType.InactiveMailbox))
			{
				flag = true;
				ADSessionSettingsFactory.InactiveMailboxVisibilityEnabler.Enable();
			}
			try
			{
				result = this.ProcessStoreMailbox(context, kit);
			}
			finally
			{
				if (flag)
				{
					ADSessionSettingsFactory.InactiveMailboxVisibilityEnabler.Disable();
				}
			}
			AssistantsLog.LogEndProcessingMailboxEvent(activityId, this.Assistant as AssistantBase, context.CustomDataToLog, storeMailboxData.MailboxGuid, storeMailboxData.DisplayName, this);
			return result;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00015D5C File Offset: 0x00013F5C
		private AssistantTaskContext ProcessStoreMailbox(AssistantTaskContext context, EmergencyKit kit)
		{
			TimeBasedDatabaseJob.processMailboxTestHook.Value();
			StoreMailboxData storeMailboxData = context.MailboxData as StoreMailboxData;
			AssistantTaskContext assistantTaskContext;
			using (StoreSession storeSession = this.OpenMailboxSession(storeMailboxData))
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				try
				{
					if (context.Args == null)
					{
						context.Args = InvokeArgs.Create(storeSession, this.Driver.TimePerTask, storeMailboxData);
					}
					kit.SetContext(this.Assistant, storeMailboxData);
					assistantTaskContext = context.Step(context);
				}
				finally
				{
					kit.UnsetContext();
					if (this.Driver.AssistantType.ControlDataPropertyDefinition != null && context.Args != null)
					{
						context.Args.StoreSession.Mailbox[this.Driver.AssistantType.ControlDataPropertyDefinition] = ControlData.Create(DateTime.UtcNow).ToByteArray();
						context.Args.StoreSession.Mailbox.Save();
					}
				}
				stopwatch.Stop();
				this.performanceCounters.AverageMailboxProcessingTime.IncrementBy(stopwatch.ElapsedTicks);
				this.performanceCounters.AverageMailboxProcessingTimeBase.Increment();
				if (assistantTaskContext == null)
				{
					this.performanceCounters.MailboxesProcessed.Increment();
				}
			}
			return assistantTaskContext;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00015EA4 File Offset: 0x000140A4
		private void PostProcessMailbox(AIException exception, AssistantTaskContext nextContext, MailboxData mailbox)
		{
			lock (this.instanceLock)
			{
				bool flag2 = this.Driver.Governor.ReportResult(exception);
				if (flag2)
				{
					if (nextContext == null)
					{
						this.FinalizeMailboxProcessing(mailbox, exception);
					}
				}
				else
				{
					this.AddForRetry(mailbox);
				}
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00015F08 File Offset: 0x00014108
		private void FinalizeMailboxProcessing(MailboxData mailbox, AIException e)
		{
			if (e != null)
			{
				lock (this.skippedMailboxesLock)
				{
					this.skippedMailboxes.Add(mailbox);
				}
				Interlocked.Increment(ref this.mailboxesProcessedFailureCount);
				return;
			}
			Interlocked.Increment(ref this.mailboxesProcessedSuccessfullyCount);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00015F6C File Offset: 0x0001416C
		private void AddForRetry(MailboxData mailbox)
		{
			if (this.activeQueue.Contains(mailbox))
			{
				this.pendingQueue.Insert(0, mailbox);
				this.activeQueue.Remove(mailbox);
			}
			this.retry = true;
			Interlocked.Increment(ref this.mailboxesRetriedCount);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00015FAC File Offset: 0x000141AC
		private StoreSession OpenMailboxSession(StoreMailboxData mailbox)
		{
			Guid activityId = (ActivityContext.ActivityId != null) ? ActivityContext.ActivityId.Value : Guid.Empty;
			AssistantBase assistant = this.Assistant as AssistantBase;
			string nonLocalizedName = this.Assistant.NonLocalizedName;
			Guid mailboxGuid = mailbox.MailboxGuid;
			StoreSession result;
			try
			{
				ExchangePrincipal exchangePrincipal;
				if (mailbox.TenantPartitionHint != null)
				{
					ADSessionSettings adSettings = ADSessionSettings.FromTenantPartitionHint(mailbox.TenantPartitionHint);
					exchangePrincipal = ExchangePrincipal.FromLocalServerMailboxGuid(adSettings, this.DatabaseInfo.Guid, mailbox.Guid);
				}
				else
				{
					exchangePrincipal = ExchangePrincipal.FromMailboxData(mailbox.Guid, this.DatabaseInfo.Guid, mailbox.OrganizationId ?? OrganizationId.ForestWideOrgId, Array<CultureInfo>.Empty);
				}
				if (mailbox.IsPublicFolderMailbox)
				{
					StoreSession storeSession = PublicFolderSession.OpenAsAdmin(null, exchangePrincipal, null, CultureInfo.InstalledUICulture, string.Format("{0};Action={1}", "Client=TBA", this.Assistant.GetType().Name), null);
					AssistantsLog.LogMailboxSucceedToOpenStoreSessionEvent(activityId, nonLocalizedName, assistant, mailboxGuid, mailbox.DisplayName, this);
					result = storeSession;
				}
				else
				{
					bool flag = false;
					MailboxSession mailbox2 = this.DatabaseInfo.GetMailbox(exchangePrincipal, ClientType.TimeBased, this.Assistant.GetType().Name);
					try
					{
						mailbox2.ReconstructExchangePrincipal();
						mailbox2.ExTimeZone = ExTimeZone.CurrentTimeZone;
						flag = true;
						AssistantsLog.LogMailboxSucceedToOpenStoreSessionEvent(activityId, nonLocalizedName, assistant, mailboxGuid, mailbox.DisplayName, this);
						result = mailbox2;
					}
					finally
					{
						if (!flag)
						{
							mailbox2.Dispose();
						}
					}
				}
			}
			catch (ObjectNotFoundException ex)
			{
				string text = "MailboxNotFound";
				string message = string.Format("{0}: {1}", this, text);
				string value = string.Format("{0}:{1}", text, mailbox.MailboxGuid);
				ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceError((long)this.GetHashCode(), message);
				AssistantsLog.LogMailboxFailedToOpenStoreSessionEvent(activityId, nonLocalizedName, assistant, ex, mailboxGuid, mailbox.DisplayName, this);
				throw new SkipException(new LocalizedString(value), ex);
			}
			catch (StorageTransientException ex2)
			{
				string message2 = string.Format("{0}: Could not open mailbox store session due to storage transient error: {1}", this, ex2.Message);
				ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceError((long)this.GetHashCode(), message2);
				AssistantsLog.LogMailboxFailedToOpenStoreSessionEvent(activityId, nonLocalizedName, assistant, ex2, mailboxGuid, mailbox.DisplayName, this);
				Interlocked.Increment(ref this.mailboxesFailedToOpenStoreSessionCount);
				throw;
			}
			catch (Exception ex3)
			{
				string message3 = string.Format("{0}: Could not open mailbox store session due to error: {1}", this, ex3.Message);
				ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceError((long)this.GetHashCode(), message3);
				AssistantsLog.LogMailboxFailedToOpenStoreSessionEvent(activityId, nonLocalizedName, assistant, ex3, mailboxGuid, mailbox.DisplayName, this);
				Interlocked.Increment(ref this.mailboxesFailedToOpenStoreSessionCount);
				throw;
			}
			return result;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00016268 File Offset: 0x00014468
		private void LogSkippedMailboxes()
		{
			if (this.skippedMailboxes.Count <= 0)
			{
				return;
			}
			string newLine = Environment.NewLine;
			StringBuilder stringBuilder = new StringBuilder(Math.Min(20 * this.skippedMailboxes.Count, 16384));
			foreach (MailboxData mailboxData in this.skippedMailboxes)
			{
				if (stringBuilder.Length > 0 && stringBuilder.Length + newLine.Length + mailboxData.DisplayName.Length >= 16384)
				{
					ExTraceGlobals.TimeBasedDatabaseJobTracer.TraceDebug<TimeBasedDatabaseJob, int, int>((long)this.GetHashCode(), "{0}: Only logging the first {1} mailboxes out of {2} skipped mailboxes.", this, stringBuilder.Length, this.skippedMailboxes.Count);
					break;
				}
				stringBuilder.Append(newLine);
				stringBuilder.Append(mailboxData.DisplayName);
			}
			base.LogEvent(AssistantsEventLogConstants.Tuple_SkippedMailboxes, null, new object[]
			{
				this.Assistant.Name,
				this.skippedMailboxes.Count,
				this.DatabaseInfo.DisplayName,
				this.Chop(stringBuilder.ToString(), 16384)
			});
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000163B4 File Offset: 0x000145B4
		private string Chop(string str, int len)
		{
			if (str.Length > len)
			{
				return str.Substring(0, len);
			}
			return str;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000163CC File Offset: 0x000145CC
		internal void FinishIfNecessary()
		{
			lock (this.instanceLock)
			{
				if (!this.Finished && this.pendingQueue.Count == 0 && this.activeQueue.Count == 0)
				{
					this.Finish();
				}
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00016430 File Offset: 0x00014630
		internal void RemoveFromActive(MailboxData mailbox)
		{
			lock (this.instanceLock)
			{
				if (this.activeQueue.Remove(mailbox))
				{
					this.Driver.DecrementNumberOfMailboxes();
				}
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00016484 File Offset: 0x00014684
		internal static IDisposable SetProcessMailboxTestHook(Action delegateFunction)
		{
			return TimeBasedDatabaseJob.processMailboxTestHook.SetTestHook(delegateFunction);
		}

		// Token: 0x04000263 RID: 611
		private const int MaxCharsPerEventLog = 16384;

		// Token: 0x04000264 RID: 612
		private static Hookable<Action> processMailboxTestHook = Hookable<Action>.Create(true, delegate()
		{
		});

		// Token: 0x04000265 RID: 613
		protected List<MailboxData> pendingQueue;

		// Token: 0x04000266 RID: 614
		protected List<MailboxData> activeQueue = new List<MailboxData>();

		// Token: 0x04000267 RID: 615
		private string toString;

		// Token: 0x04000268 RID: 616
		private PerformanceCountersPerDatabaseInstance performanceCounters;

		// Token: 0x04000269 RID: 617
		private PoisonMailboxControl poisonControl;

		// Token: 0x0400026A RID: 618
		private bool loggedBegin;

		// Token: 0x0400026B RID: 619
		private readonly int initialPendingQueueCount;

		// Token: 0x0400026C RID: 620
		private int mailboxesProcessedSuccessfullyCount;

		// Token: 0x0400026D RID: 621
		private int mailboxesProcessedFailureCount;

		// Token: 0x0400026E RID: 622
		private int mailboxesFailedToOpenStoreSessionCount;

		// Token: 0x0400026F RID: 623
		private int mailboxesRetriedCount;

		// Token: 0x04000270 RID: 624
		private bool retry;

		// Token: 0x04000271 RID: 625
		private readonly List<MailboxData> skippedMailboxes = new List<MailboxData>();

		// Token: 0x04000272 RID: 626
		private readonly object skippedMailboxesLock = new object();

		// Token: 0x04000273 RID: 627
		private readonly object instanceLock = new object();
	}
}
