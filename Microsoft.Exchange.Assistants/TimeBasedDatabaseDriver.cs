using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Assistants.Diagnostics;
using Microsoft.Exchange.Assistants.EventLog;
using Microsoft.Exchange.Assistants.Logging;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000085 RID: 133
	internal abstract class TimeBasedDatabaseDriver : Base, IDisposable
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x00011DC8 File Offset: 0x0000FFC8
		internal TimeBasedDatabaseDriver(ThrottleGovernor parentGovernor, DatabaseInfo databaseInfo, ITimeBasedAssistantType timeBasedAssistantType, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters)
		{
			this.databaseInfo = databaseInfo;
			this.performanceCounters = databaseCounters;
			this.governor = new DatabaseGovernor("time based for '" + databaseInfo.DisplayName + "'", parentGovernor, new Throttle("TimeBasedDatabaseDriver", parentGovernor.Throttle.OpenThrottleValue, parentGovernor.Throttle));
			this.assistant = timeBasedAssistantType.CreateInstance(databaseInfo);
			if (this.assistant == null)
			{
				throw new ApplicationException(string.Format("Assistant failed to create instance, assistant type {0}", timeBasedAssistantType.NonLocalizedName));
			}
			this.poisonControl = poisonControl;
			this.assistantType = timeBasedAssistantType;
			this.assistantWorkloadState = TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.Enabled;
			this.windowJobHistory = new DiagnosticsHistoryQueue<DiagnosticsSummaryJobWindow>(100);
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00011E96 File Offset: 0x00010096
		internal bool EnabledAndRunning
		{
			get
			{
				return this.assistantWorkloadState == TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.EnabledAndRunning;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00011EA1 File Offset: 0x000100A1
		internal bool DisabledAndNotRunning
		{
			get
			{
				return this.assistantWorkloadState == TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.DisabledAndNotRunning;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00011EAC File Offset: 0x000100AC
		public ThrottleGovernor Governor
		{
			get
			{
				return this.governor;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00011EB4 File Offset: 0x000100B4
		public Throttle Throttle
		{
			get
			{
				return this.governor.Throttle;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00011EC1 File Offset: 0x000100C1
		public ITimeBasedAssistant Assistant
		{
			get
			{
				return this.assistant;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00011EC9 File Offset: 0x000100C9
		public ITimeBasedAssistantType AssistantType
		{
			get
			{
				return this.assistantType;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00011ED1 File Offset: 0x000100D1
		public DatabaseInfo DatabaseInfo
		{
			get
			{
				return this.databaseInfo;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00011ED9 File Offset: 0x000100D9
		public PoisonMailboxControl PoisonControl
		{
			get
			{
				return this.poisonControl;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00011EE1 File Offset: 0x000100E1
		public int NumberOfMailboxesInQueue
		{
			get
			{
				return this.numberOfMailboxesInQueue;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00011EE9 File Offset: 0x000100E9
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00011EF1 File Offset: 0x000100F1
		public TimeSpan TimePerTask { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00011EFC File Offset: 0x000100FC
		public uint TotalMailboxesQueued
		{
			get
			{
				uint result;
				lock (this.instanceLock)
				{
					uint num = 0U;
					if (this.windowJob != null)
					{
						num += (uint)this.windowJob.MailboxesQueued;
					}
					foreach (TimeBasedDatabaseDemandJob timeBasedDatabaseDemandJob in this.demandJobs)
					{
						num += (uint)timeBasedDatabaseDemandJob.MailboxesQueued;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00011F98 File Offset: 0x00010198
		public IEnumerable<ResourceKey> ResourceDependencies
		{
			get
			{
				lock (this.instanceLock)
				{
					if (this.started)
					{
						return this.Assistant.GetResourceDependencies();
					}
				}
				return null;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00011FEC File Offset: 0x000101EC
		private bool WindowJobRunning
		{
			get
			{
				return this.windowJob != null && !this.windowJob.Finished;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00012008 File Offset: 0x00010208
		public void Dispose()
		{
			this.governor.Dispose();
			IDisposable disposable = this.Assistant as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			if (this.workerThreadsClear != null)
			{
				this.workerThreadsClear.Dispose();
				this.workerThreadsClear = null;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001204F File Offset: 0x0001024F
		public override string ToString()
		{
			return "TimeBasedDatabaseDriver for database '" + (this.databaseInfo.DisplayName ?? "<null>") + "', Assistant " + this.Assistant.GetType().Name;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00012084 File Offset: 0x00010284
		public bool IsVariantConfigurationChanged()
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(new DatabaseSettingsContext(this.databaseInfo.Guid, null), null, null);
			bool enabled = snapshot.MailboxAssistants.GetObject<IMailboxAssistantSettings>(this.assistantType.Identifier, new object[0]).Enabled;
			ExTraceGlobals.TimeBasedDriverManagerTracer.TraceDebug((long)this.GetHashCode(), "{0}: Assistant {1} is enabled: {2}, on database {3}.", new object[]
			{
				this.ToString(),
				this.assistantType.Identifier,
				enabled,
				this.databaseInfo.Guid
			});
			bool flag = false;
			lock (this.instanceLock)
			{
				if (enabled)
				{
					if (this.assistantWorkloadState != TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.EnabledAndRunning)
					{
						flag = true;
					}
				}
				else if (this.assistantWorkloadState != TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.DisabledAndNotRunning)
				{
					flag = true;
				}
			}
			ExTraceGlobals.TimeBasedDriverManagerTracer.TraceDebug((long)this.GetHashCode(), "{0}: Assistant {1} needs Variant Configuration update: {2}, on database {3}.", new object[]
			{
				this.ToString(),
				this.assistantType.Identifier,
				flag,
				this.databaseInfo.Guid
			});
			return flag;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000121DC File Offset: 0x000103DC
		public bool IsAssistantEnabled()
		{
			bool flag;
			if (this.AssistantType.WorkCycle.Equals(TimeSpan.Zero) || this.AssistantType.WorkCycleCheckpoint.Equals(TimeSpan.Zero))
			{
				ExTraceGlobals.TimeBasedDriverManagerTracer.TraceDebug((long)this.GetHashCode(), "{0}: Assistant {1} disabled with work cycle period {2}, work cycle check point {3}", new object[]
				{
					this,
					this.AssistantType.Identifier,
					this.AssistantType.WorkCycle,
					this.AssistantType.WorkCycleCheckpoint
				});
				flag = false;
			}
			else
			{
				ExTraceGlobals.TimeBasedDriverManagerTracer.TraceDebug<TimeBasedDatabaseDriver, TimeBasedAssistantIdentifier>((long)this.GetHashCode(), "{0}: Assistant {1} enabled on server.", this, this.AssistantType.Identifier);
				flag = true;
			}
			if (flag)
			{
				VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(DatabaseSettingsContext.Get(this.databaseInfo.Guid), null, null);
				flag = snapshot.MailboxAssistants.GetObject<IMailboxAssistantSettings>(this.assistantType.Identifier, new object[0]).Enabled;
				ExTraceGlobals.TimeBasedDriverManagerTracer.TraceDebug((long)this.GetHashCode(), "{0}: Assistant {1} is enabled: {2}, on database {3}.", new object[]
				{
					this.ToString(),
					this.assistantType.Identifier,
					flag,
					this.databaseInfo.Guid
				});
			}
			lock (this.instanceLock)
			{
				if (flag)
				{
					if (this.assistantWorkloadState != TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.EnabledAndRunning)
					{
						this.assistantWorkloadState = TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.Enabled;
					}
				}
				else if (this.assistantWorkloadState != TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.DisabledAndNotRunning)
				{
					this.assistantWorkloadState = TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.Disabled;
				}
			}
			return flag;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000123A4 File Offset: 0x000105A4
		public void Start()
		{
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Starting...", this);
			lock (this.instanceLock)
			{
				this.started = true;
				AssistantsLog.LogDatabaseStartEvent(this.Assistant as AssistantBase);
			}
			base.TracePfd("PFD AIS {0} {1}: Started", new object[]
			{
				30295,
				this
			});
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00012430 File Offset: 0x00010630
		public void RequestStop()
		{
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Requesting stop", this);
			AIBreadcrumbs.ShutdownTrail.Drop("Stopping time assistant: " + this.Assistant.NonLocalizedName);
			this.Assistant.OnShutdown();
			AIBreadcrumbs.ShutdownTrail.Drop("Finished stopping " + this.Assistant.NonLocalizedName);
			lock (this.instanceLock)
			{
				ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Stopping all jobs", this);
				this.StopAllJobs();
				this.started = false;
				AssistantsLog.LogDatabaseStopEvent(this.Assistant as AssistantBase);
			}
			base.TracePfd("PFD AIS {0} {1}: Requested Stop.", new object[]
			{
				19031,
				this
			});
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00012524 File Offset: 0x00010724
		public void WaitUntilStopped(TimeBasedAssistantController assistantController)
		{
			AIBreadcrumbs.ShutdownTrail.Drop("Waiting for stop on time assistant: " + this.Assistant.NonLocalizedName);
			ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController, TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Waiting stop of {1}", assistantController, this);
			this.workerThreadsClear.WaitOne();
			AIBreadcrumbs.ShutdownTrail.Drop("Done waiting for: " + this.Assistant.NonLocalizedName);
			lock (this.instanceLock)
			{
				this.Deinitialize();
			}
		}

		// Token: 0x060003DE RID: 990
		public abstract void RunNow(Guid mailboxGuid, string parameters);

		// Token: 0x060003DF RID: 991 RVA: 0x000125C8 File Offset: 0x000107C8
		public void Halt()
		{
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Halt requested...", this);
			lock (this.instanceLock)
			{
				this.StopAllJobs();
			}
			base.LogEvent(AssistantsEventLogConstants.Tuple_TimeHalt, null, new object[]
			{
				this.Assistant.Name,
				this.databaseInfo.DisplayName
			});
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Halted.", this);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001266C File Offset: 0x0001086C
		public void IncrementNumberOfMailboxes()
		{
			int arg = Interlocked.Increment(ref this.numberOfMailboxesInQueue);
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, int>((long)this.GetHashCode(), "{0}: Number of mailboxes incremented to {1}", this, arg);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000126A0 File Offset: 0x000108A0
		public void IncrementNumberOfMailboxes(int numberOfMailboxes)
		{
			int arg = Interlocked.Add(ref this.numberOfMailboxesInQueue, numberOfMailboxes);
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, int, int>((long)this.GetHashCode(), "{0}: Number of mailboxes incremented by {1} to {2}", this, numberOfMailboxes, arg);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000126D4 File Offset: 0x000108D4
		public void DecrementNumberOfMailboxes()
		{
			int arg = Interlocked.Decrement(ref this.numberOfMailboxesInQueue);
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, int>((long)this.GetHashCode(), "{0}: Number of mailboxes decremented to {1}", this, arg);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00012708 File Offset: 0x00010908
		public void DecrementNumberOfMailboxes(int numberOfMailboxes)
		{
			int arg = Interlocked.Add(ref this.numberOfMailboxesInQueue, -numberOfMailboxes);
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, int, int>((long)this.GetHashCode(), "{0}: Number of mailboxes decremented by {1} to {2}", this, numberOfMailboxes, arg);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001273C File Offset: 0x0001093C
		public bool HasTask()
		{
			TimeBasedDatabaseJob pendingJob;
			lock (this.instanceLock)
			{
				pendingJob = this.GetPendingJob(false);
			}
			return pendingJob != null && pendingJob.HasTask();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001278C File Offset: 0x0001098C
		public DiagnosticsSummaryDatabase GetDatabaseDiagnosticsSummary()
		{
			return new DiagnosticsSummaryDatabase(this.EnabledAndRunning, this.startTime, this.GetWindowJobMailboxesSummary(), this.GetOnDemandJobMailboxesSummary());
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000127AC File Offset: 0x000109AC
		public DiagnosticsSummaryJobWindow[] GetWindowJobHistory()
		{
			DiagnosticsSummaryJobWindow[] result;
			lock (this.instanceLock)
			{
				result = this.windowJobHistory.ToArray();
			}
			return result;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000127F4 File Offset: 0x000109F4
		public List<Guid> GetMailboxGuidList(bool isActive)
		{
			List<Guid> list = new List<Guid>();
			lock (this.instanceLock)
			{
				if (this.windowJob != null)
				{
					list.AddRange(this.windowJob.GetMailboxGuidList(isActive));
				}
				foreach (TimeBasedDatabaseDemandJob timeBasedDatabaseDemandJob in this.demandJobs)
				{
					list.AddRange(timeBasedDatabaseDemandJob.GetMailboxGuidList(isActive));
				}
			}
			return list;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000128BC File Offset: 0x00010ABC
		internal AssistantTaskContext ProcessNextTask(AssistantTaskContext context)
		{
			this.performanceCounters.NumberOfThreadsUsed.Increment();
			AssistantTaskContext nextContext = null;
			try
			{
				base.CatchMeIfYouCan(delegate
				{
					nextContext = this.ProcessOneMailbox(ref context);
				}, this.Assistant.NonLocalizedName);
			}
			catch (AIException e)
			{
				if (context != null && context.Job != null)
				{
					context.Job.LogAIException(context.MailboxData, e);
				}
				else
				{
					AssistantsLog.LogErrorProcessingMailboxEvent(this.Assistant.NonLocalizedName, (context == null) ? null : context.MailboxData, e, this.DatabaseInfo.DatabaseName, "", MailboxSlaRequestType.Unknown);
				}
			}
			catch (Exception e2)
			{
				AssistantsLog.LogErrorProcessingMailboxEvent(this.Assistant.NonLocalizedName, (context == null) ? null : context.MailboxData, e2, this.DatabaseInfo.DatabaseName, "", MailboxSlaRequestType.Unknown);
				throw;
			}
			finally
			{
				this.performanceCounters.NumberOfThreadsUsed.Decrement();
			}
			return nextContext;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00012A0C File Offset: 0x00010C0C
		internal void UpdateWorkCycle(TimeSpan workCyclePeriod)
		{
			bool flag = false;
			bool flag2 = false;
			lock (this.instanceLock)
			{
				if (this.assistantWorkloadState == TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.Enabled || this.assistantWorkloadState == TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.EnabledAndRunning)
				{
					flag = true;
					if (this.assistantWorkloadState == TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.Enabled)
					{
						this.windowJobHistory.Clear();
						this.startTime = DateTime.UtcNow;
						this.assistantWorkloadState = TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.EnabledAndRunning;
					}
					if (!this.inWorkCycle)
					{
						ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, bool, bool>((long)this.GetHashCode(), "{0}: Start Work Cycle. Started: {1}, Job Running: {2}.", this, this.started, this.WindowJobRunning);
						if (!this.started)
						{
							AssistantsLog.LogDriverNotStartedEvent(this.Assistant.NonLocalizedName, this.Assistant as AssistantBase);
						}
						else if (this.WindowJobRunning)
						{
							AssistantsLog.LogJobAlreadyRunningEvent(this.Assistant.NonLocalizedName);
						}
						else
						{
							flag2 = true;
						}
					}
				}
				else if (this.assistantWorkloadState == TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.Disabled)
				{
					this.windowJobHistory.Clear();
					this.startTime = DateTime.UtcNow;
					this.assistantWorkloadState = TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase.DisabledAndNotRunning;
				}
			}
			if (flag && !this.inWorkCycle)
			{
				if (flag2)
				{
					this.assistantType.OnWorkCycleStart(this.DatabaseInfo);
					this.TryStartWorkCycle(workCyclePeriod);
				}
				this.inWorkCycle = true;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00012B4C File Offset: 0x00010D4C
		internal uint StopWorkCycle()
		{
			return this.StopWorkCycle(false);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00012B58 File Offset: 0x00010D58
		internal uint StopWorkCycle(bool enqueueHistoryNow)
		{
			uint result = 0U;
			if (this.inWorkCycle)
			{
				lock (this.instanceLock)
				{
					ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Work Cycle is stopping.", this);
					result = this.StopWorkCycleJob(enqueueHistoryNow);
					ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Deinitializing driver.", this);
					this.Deinitialize();
				}
				this.inWorkCycle = false;
			}
			return result;
		}

		// Token: 0x060003EC RID: 1004
		protected abstract List<MailboxData> GetMailboxesForCurrentWindow(out int totalMailboxOnDatabaseCount, out int notInterestingMailboxCount, out int filteredMailboxCount, out int failedFilteringCount);

		// Token: 0x060003ED RID: 1005 RVA: 0x00012C1C File Offset: 0x00010E1C
		protected void RunNow(MailboxData mailboxData)
		{
			lock (this.instanceLock)
			{
				if (this.windowJob != null && this.windowJob.Remove(mailboxData))
				{
					ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, string>((long)this.GetHashCode(), "{0}: Removed mailbox {1} from windowJob so it can be added to a DemandJob.", this, mailboxData.DisplayName);
				}
				if (!this.demandJobs.Any((TimeBasedDatabaseDemandJob demandJob) => demandJob.GetMailboxGuidList(true).Contains(mailboxData.MailboxGuid) || demandJob.GetMailboxGuidList(false).Contains(mailboxData.MailboxGuid)))
				{
					this.demandJobs.Add(new TimeBasedDatabaseDemandJob(this, mailboxData, this.poisonControl, this.performanceCounters));
					ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, string, uint>((long)this.GetHashCode(), "{0}: Adding demand job with the following mailbox {1}, total queued: {2}", this, mailboxData.DisplayName, this.TotalMailboxesQueued);
				}
				else
				{
					ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, string, uint>((long)this.GetHashCode(), "{0}: Mailbox {1} has already been requested for a demand job, not queueing it again, total queued: {2}", this, mailboxData.DisplayName, this.TotalMailboxesQueued);
				}
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00012D7C File Offset: 0x00010F7C
		protected void TryStartWorkCycle(TimeSpan workCyclePeriod)
		{
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Starting a Work Cycle", this);
			List<MailboxData> mailboxes = null;
			int notInterestingCount = 0;
			int filteredCount = 0;
			int totalOnDatabaseMailboxCount = 0;
			int failedFilteringCount = 0;
			try
			{
				base.CatchMeIfYouCan(delegate
				{
					mailboxes = this.GetMailboxesForCurrentWindow(out totalOnDatabaseMailboxCount, out notInterestingCount, out filteredCount, out failedFilteringCount);
				}, this.Assistant.NonLocalizedName);
				if (mailboxes == null || mailboxes.Count <= 0)
				{
					ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceError<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: No mailboxes to process", this);
					lock (this.instanceLock)
					{
						this.windowJobHistory.Enqueue(new DiagnosticsSummaryJobWindow(totalOnDatabaseMailboxCount, 0, notInterestingCount, filteredCount, failedFilteringCount, 0, DateTime.UtcNow, DateTime.UtcNow, new DiagnosticsSummaryJob()));
					}
					return;
				}
				base.CatchMeIfYouCan(delegate
				{
					this.Assistant.OnWorkCycleCheckpoint();
				}, this.Assistant.NonLocalizedName);
			}
			catch (AIException ex)
			{
				ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceError<TimeBasedDatabaseDriver, AIException>((long)this.GetHashCode(), "{0}: Currently unable to list mailboxes: {1}", this, ex);
				base.LogEvent(AssistantsEventLogConstants.Tuple_TimeWindowBeginError, null, new object[]
				{
					this.Assistant.Name,
					this.databaseInfo.DisplayName,
					ex
				});
				return;
			}
			TimeBasedDatabaseWindowJob timeBasedDatabaseWindowJob = new TimeBasedDatabaseWindowJob(this, mailboxes, notInterestingCount, filteredCount, failedFilteringCount, totalOnDatabaseMailboxCount, this.poisonControl, this.performanceCounters);
			lock (this.instanceLock)
			{
				if (this.started && !this.WindowJobRunning)
				{
					this.RemoveWindowJobWithHistoryEntry();
					this.windowJob = timeBasedDatabaseWindowJob;
					double num = workCyclePeriod.TotalSeconds / (double)mailboxes.Count;
					if (num > TimeBasedDatabaseDriver.maxTimerIntervalInSeconds)
					{
						num = TimeBasedDatabaseDriver.maxTimerIntervalInSeconds;
					}
					this.TimePerTask = TimeSpan.FromSeconds(num);
					ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug((long)this.GetHashCode(), "{0}: Starting job with Work Cycle {1}, {2} mailboxes, default timer period of {3}.", new object[]
					{
						this,
						workCyclePeriod,
						mailboxes.Count,
						num
					});
				}
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00013038 File Offset: 0x00011238
		protected uint StopWorkCycleJob()
		{
			return this.StopWorkCycleJob(false);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00013044 File Offset: 0x00011244
		protected uint StopWorkCycleJob(bool enqueueHistoryNow)
		{
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Stoppping the window job", this);
			uint result = 0U;
			if (this.WindowJobRunning)
			{
				result = this.windowJob.RequestStop();
				if (enqueueHistoryNow)
				{
					this.windowJobHistory.Enqueue(this.windowJob.GetJobDiagnosticsSummary());
				}
				if (this.TotalMailboxesQueued == 0U && this.workersActive == 0)
				{
					ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Deinitializing the driver...", this);
					this.Deinitialize();
				}
			}
			return result;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000130C8 File Offset: 0x000112C8
		protected void StopAllJobs()
		{
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: StopAllJobs", this);
			this.StopWorkCycleJob();
			foreach (TimeBasedDatabaseDemandJob timeBasedDatabaseDemandJob in this.demandJobs)
			{
				AIBreadcrumbs.ShutdownTrail.Drop("Stopping job: " + timeBasedDatabaseDemandJob);
				timeBasedDatabaseDemandJob.RequestStop();
				AIBreadcrumbs.ShutdownTrail.Drop("Finished stopping " + timeBasedDatabaseDemandJob);
			}
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, uint>((long)this.GetHashCode(), "{0}: Stopped all jobs. Total mailboxes queued: {1}", this, this.TotalMailboxesQueued);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013180 File Offset: 0x00011380
		protected void Deinitialize()
		{
			if (this.poisonControl != null)
			{
				this.poisonControl.Clear();
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00013198 File Offset: 0x00011398
		protected AssistantTaskContext ProcessOneMailbox(ref AssistantTaskContext context)
		{
			lock (this.instanceLock)
			{
				if (context == null)
				{
					if (!this.started)
					{
						ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, string>((long)this.GetHashCode(), "{0}: Worker bailing (Not started) for assistant: {1}", this, this.Assistant.NonLocalizedName);
						AssistantsLog.LogNotStartedEvent(this.Assistant.NonLocalizedName, this.Assistant as AssistantBase);
						return null;
					}
					if (this.TotalMailboxesQueued == 0U)
					{
						ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, string>((long)this.GetHashCode(), "{0}: Worker bailing (Empty queue) for assistant: {1}", this, this.Assistant.NonLocalizedName);
						AssistantsLog.LogNoMailboxesPendingEvent(this.Assistant.NonLocalizedName);
						return null;
					}
				}
				if (this.workersActive++ == 0)
				{
					FastManualResetEvent fastManualResetEvent = this.workerThreadsClear;
					if (fastManualResetEvent != null)
					{
						fastManualResetEvent.Reset();
					}
				}
				ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, int, string>((long)this.GetHashCode(), "{0}: Worker started. Workers Active on this Driver: {1}, assistant: {2}", this, this.workersActive, this.Assistant.NonLocalizedName);
			}
			AssistantTaskContext assistantTaskContext = null;
			TimeBasedDatabaseJob timeBasedDatabaseJob = null;
			MailboxData mailboxData = null;
			try
			{
				if (context == null)
				{
					lock (this.instanceLock)
					{
						timeBasedDatabaseJob = this.GetPendingJob(true);
						ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, uint, string>((long)this.GetHashCode(), "{0}: Total Mailboxes Queued on this database: {1}, assistant: {2}", this, this.TotalMailboxesQueued, this.Assistant.NonLocalizedName);
					}
					if (timeBasedDatabaseJob != null)
					{
						mailboxData = timeBasedDatabaseJob.GetNextMailbox();
						if (mailboxData != null)
						{
							context = this.Assistant.InitializeContext(mailboxData, timeBasedDatabaseJob);
						}
					}
					else
					{
						AssistantsLog.LogNoJobsEvent(this.Assistant.NonLocalizedName);
					}
				}
				else
				{
					timeBasedDatabaseJob = context.Job;
					mailboxData = context.MailboxData;
				}
				if (context != null && context.Job != null)
				{
					assistantTaskContext = context.Job.ProcessNextMailbox(context);
				}
			}
			catch
			{
				lock (this.instanceLock)
				{
					if (--this.workersActive == 0)
					{
						ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, int, string>((long)this.GetHashCode(), "{0}: Worker exiting due to exception. Workers Active {1}, assistant: {2}", this, this.workersActive, this.Assistant.NonLocalizedName);
						this.workerThreadsClear.Set();
					}
				}
				throw;
			}
			finally
			{
				if (timeBasedDatabaseJob != null && mailboxData != null && (assistantTaskContext == null || context == null))
				{
					timeBasedDatabaseJob.RemoveFromActive(mailboxData);
					timeBasedDatabaseJob.FinishIfNecessary();
				}
			}
			lock (this.instanceLock)
			{
				try
				{
					ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, int, uint>((long)this.GetHashCode(), "{0}: Yielding thread. Workers Active: {1}, Remaining Mailboxes on this database: {2}", this, this.workersActive, this.TotalMailboxesQueued);
					if (context != null && context.Job != null && (context.Job.MailboxesQueued == 0 || context.Job.Finished))
					{
						TimeBasedDatabaseDemandJob timeBasedDatabaseDemandJob = context.Job as TimeBasedDatabaseDemandJob;
						if (timeBasedDatabaseDemandJob != null)
						{
							this.demandJobs.Remove(timeBasedDatabaseDemandJob);
							ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, int>((long)this.GetHashCode(), "{0}: Demand Job is done and has been removed. Remaining Demand Jobs: {1}", this, this.demandJobs.Count);
						}
						else
						{
							this.RemoveWindowJobWithHistoryEntry();
						}
					}
				}
				finally
				{
					if (--this.workersActive == 0)
					{
						ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver, int>((long)this.GetHashCode(), "{0}: Worker exiting. Workers Active {1}", this, this.workersActive);
						this.workerThreadsClear.Set();
					}
				}
			}
			return assistantTaskContext;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00013598 File Offset: 0x00011798
		protected TimeBasedDatabaseJob GetPendingJob(bool cycleDemandJob = true)
		{
			if (this.demandJobs.Count > 0)
			{
				TimeBasedDatabaseDemandJob timeBasedDatabaseDemandJob = this.demandJobs[0];
				if (cycleDemandJob)
				{
					this.demandJobs.RemoveAt(0);
					this.demandJobs.Add(timeBasedDatabaseDemandJob);
				}
				return timeBasedDatabaseDemandJob;
			}
			return this.windowJob;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000135E4 File Offset: 0x000117E4
		protected bool IsMailboxInDemandJob(MailboxData mailbox)
		{
			bool result;
			lock (this.instanceLock)
			{
				foreach (TimeBasedDatabaseDemandJob timeBasedDatabaseDemandJob in this.demandJobs)
				{
					if (timeBasedDatabaseDemandJob.IsMailboxActiveOrPending(mailbox))
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001366C File Offset: 0x0001186C
		private void RemoveWindowJobWithHistoryEntry()
		{
			if (this.windowJob == null)
			{
				ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Called RemoveWindowJob, but it's already gone.", this);
				return;
			}
			if (!this.windowJob.Finished)
			{
				ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Called RemoveWindowJob, but it's not yet finished.", this);
				return;
			}
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Enqueue history and remove Window Job.", this);
			DiagnosticsSummaryJobWindow jobDiagnosticsSummary = this.windowJob.GetJobDiagnosticsSummary();
			DiagnosticsSummaryJobWindow lastQueuedElement = this.windowJobHistory.GetLastQueuedElement();
			if (lastQueuedElement == null || !lastQueuedElement.StartTime.Equals(jobDiagnosticsSummary.StartTime))
			{
				this.windowJobHistory.Enqueue(jobDiagnosticsSummary);
			}
			this.windowJob = null;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001371C File Offset: 0x0001191C
		private DiagnosticsSummaryJobWindow GetWindowJobMailboxesSummary()
		{
			DiagnosticsSummaryJobWindow result;
			lock (this.instanceLock)
			{
				result = ((this.windowJob != null) ? this.windowJob.GetJobDiagnosticsSummary() : new DiagnosticsSummaryJobWindow());
			}
			return result;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00013774 File Offset: 0x00011974
		private DiagnosticsSummaryJob GetOnDemandJobMailboxesSummary()
		{
			DiagnosticsSummaryJob diagnosticsSummaryJob = new DiagnosticsSummaryJob();
			lock (this.instanceLock)
			{
				foreach (TimeBasedDatabaseDemandJob timeBasedDatabaseDemandJob in this.demandJobs)
				{
					diagnosticsSummaryJob.AddMoreSummary(timeBasedDatabaseDemandJob.GetJobDiagnosticsSummary());
				}
			}
			return diagnosticsSummaryJob;
		}

		// Token: 0x0400021C RID: 540
		private const int WindowJobHistoryLimit = 100;

		// Token: 0x0400021D RID: 541
		private static readonly double maxTimerIntervalInSeconds = TimeSpan.FromDays(45.0).TotalSeconds;

		// Token: 0x0400021E RID: 542
		private readonly DatabaseInfo databaseInfo;

		// Token: 0x0400021F RID: 543
		private readonly ThrottleGovernor governor;

		// Token: 0x04000220 RID: 544
		private readonly ITimeBasedAssistant assistant;

		// Token: 0x04000221 RID: 545
		private bool started;

		// Token: 0x04000222 RID: 546
		private int workersActive;

		// Token: 0x04000223 RID: 547
		private int numberOfMailboxesInQueue;

		// Token: 0x04000224 RID: 548
		private TimeBasedDatabaseWindowJob windowJob;

		// Token: 0x04000225 RID: 549
		private List<TimeBasedDatabaseDemandJob> demandJobs = new List<TimeBasedDatabaseDemandJob>();

		// Token: 0x04000226 RID: 550
		private PoisonMailboxControl poisonControl;

		// Token: 0x04000227 RID: 551
		private FastManualResetEvent workerThreadsClear = new FastManualResetEvent(true);

		// Token: 0x04000228 RID: 552
		private bool inWorkCycle;

		// Token: 0x04000229 RID: 553
		private ITimeBasedAssistantType assistantType;

		// Token: 0x0400022A RID: 554
		private PerformanceCountersPerDatabaseInstance performanceCounters;

		// Token: 0x0400022B RID: 555
		private readonly object instanceLock = new object();

		// Token: 0x0400022C RID: 556
		private DateTime startTime;

		// Token: 0x0400022D RID: 557
		private readonly DiagnosticsHistoryQueue<DiagnosticsSummaryJobWindow> windowJobHistory;

		// Token: 0x0400022E RID: 558
		private TimeBasedDatabaseDriver.AssistantWorkloadStateOnDatabase assistantWorkloadState;

		// Token: 0x02000086 RID: 134
		private enum AssistantWorkloadStateOnDatabase
		{
			// Token: 0x04000231 RID: 561
			Disabled,
			// Token: 0x04000232 RID: 562
			DisabledAndNotRunning,
			// Token: 0x04000233 RID: 563
			Enabled,
			// Token: 0x04000234 RID: 564
			EnabledAndRunning
		}
	}
}
