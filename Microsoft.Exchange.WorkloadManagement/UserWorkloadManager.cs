using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000040 RID: 64
	internal class UserWorkloadManager : DisposeTrackableBase, IUserWorkloadManager
	{
		// Token: 0x0600024F RID: 591 RVA: 0x0000A67C File Offset: 0x0000887C
		public UserWorkloadManager(int maxThreadCount, int maxTasksQueued, int maxDelayCacheTasks, TimeSpan maxDelayCacheTime)
		{
			UserWorkloadManager.ValidateParams(maxThreadCount, maxTasksQueued, maxDelayCacheTasks, maxDelayCacheTime);
			this.maxThreadCount = maxThreadCount;
			this.maxTasksQueued = maxTasksQueued;
			this.maxDelayCacheTime = maxDelayCacheTime;
			this.taskQueue = new Queue<WrappedTask>(maxTasksQueued);
			this.workerThreadCount = 0;
			this.delayCache = new ExactTimeoutCache<int, WrappedTask>(new RemoveItemDelegate<int, WrappedTask>(this.HandleDelayCacheRemove), null, new UnhandledExceptionDelegate(this.HandleDelayCacheThreadException), maxDelayCacheTasks, true, CacheFullBehavior.ExpireExisting);
			this.maxWrappedTaskPoolSize = 2 * maxTasksQueued;
			this.wrappedTaskPool = new Stack<WrappedTask>(this.maxWrappedTaskPoolSize);
			this.allActiveTasks = new ConcurrentDictionary<ITask, WrappedTask>();
			this.PermitSynchronousExecution = UserWorkloadManager.PermitSynchronousTaskExecutionEntry.Value;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A773 File Offset: 0x00008973
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000A77A File Offset: 0x0000897A
		public static TimeSpan MinimumEnforcedDelayTime { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A782 File Offset: 0x00008982
		public static UserWorkloadManager Singleton
		{
			get
			{
				if (UserWorkloadManager.singleton == null)
				{
					throw new InvalidOperationException("UserWorkloadManager must be initialized before accessing Singleton.");
				}
				return UserWorkloadManager.singleton;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A79B File Offset: 0x0000899B
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000A7A3 File Offset: 0x000089A3
		public bool PermitSynchronousExecution { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A7AC File Offset: 0x000089AC
		public bool IsQueueFull
		{
			get
			{
				return this.taskQueue.Count >= this.maxTasksQueued;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A7C4 File Offset: 0x000089C4
		public bool Canceled
		{
			get
			{
				return base.IsDisposed;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000A7CC File Offset: 0x000089CC
		public int DelayedTaskCount
		{
			get
			{
				if (this.delayCache != null)
				{
					return this.delayCache.Count;
				}
				return 0;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000A7E3 File Offset: 0x000089E3
		public int QueueTaskCount
		{
			get
			{
				return this.taskQueue.Count;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000A7F0 File Offset: 0x000089F0
		public int ExecutingTaskCount
		{
			get
			{
				return this.workerThreadCount;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000A7F8 File Offset: 0x000089F8
		public int TotalTasks
		{
			get
			{
				return this.DelayedTaskCount + this.QueueTaskCount + this.ExecutingTaskCount;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000A80E File Offset: 0x00008A0E
		public uint TaskSubmissionFailuresPerMinute
		{
			get
			{
				return this.taskSubmissionFailuresPerMinute.GetValue();
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A81B File Offset: 0x00008A1B
		public uint TasksCompletedPerMinute
		{
			get
			{
				return this.tasksCompletedPerMinute.GetValue();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000A828 File Offset: 0x00008A28
		public uint TaskTimeoutsPerMinute
		{
			get
			{
				return this.taskTimeoutsPerMinute.GetValue();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000A835 File Offset: 0x00008A35
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000A83D File Offset: 0x00008A3D
		public Action<ITask, TimeSpan> OnDelayTask { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000A846 File Offset: 0x00008A46
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000A84E File Offset: 0x00008A4E
		public Action<ITask, RemoveReason> OnTaskReleaseFromDelay { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000A857 File Offset: 0x00008A57
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000A85F File Offset: 0x00008A5F
		public Func<ITask, DelayInfo, DelayInfo> OnGetDelayForTest { get; set; }

		// Token: 0x06000264 RID: 612 RVA: 0x0000A868 File Offset: 0x00008A68
		public static int GetAvailableThreads()
		{
			int result;
			int num;
			ThreadPool.GetAvailableThreads(out result, out num);
			return result;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A880 File Offset: 0x00008A80
		public static void Initialize(int maxThreadCount, int maxTasksQueued, int maxDelayCacheTasks, TimeSpan maxDelayCacheTime, int? delayTimeThreshold)
		{
			if (UserWorkloadManager.singleton != null && !UserWorkloadManager.singleton.Canceled)
			{
				throw new InvalidOperationException("UserWorkloadManager may only be initialized once.");
			}
			UserWorkloadManager.ValidateParams(maxThreadCount, maxTasksQueued, maxDelayCacheTasks, maxDelayCacheTime);
			ExTraceGlobals.UserWorkloadManagerTracer.TraceError(0L, "UserWorkloadManager initialized with maxThreadCount: {0}, maxTasksQueued: {1}, maxDelayCacheTasks: {2}, and maxDelayCacheTime: {3}", new object[]
			{
				maxThreadCount,
				maxTasksQueued,
				maxDelayCacheTasks,
				maxDelayCacheTime
			});
			ResourceLoadDelayInfo.Initialize();
			UserWorkloadManagerPerfCounterWrapper.Initialize(maxTasksQueued, maxThreadCount, maxDelayCacheTasks, delayTimeThreshold);
			UserWorkloadManager.MinimumEnforcedDelayTime = ((UserWorkloadManager.E14WLMMinimumEnforcedDelayTimeInSecondsEntry.Value < 0) ? UserWorkloadManager.DefaultMinimumEnforcedDelay : TimeSpan.FromSeconds((double)UserWorkloadManager.E14WLMMinimumEnforcedDelayTimeInSecondsEntry.Value));
			UserWorkloadManager.singleton = new UserWorkloadManager(maxThreadCount, maxTasksQueued, maxDelayCacheTasks, maxDelayCacheTime);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A9C8 File Offset: 0x00008BC8
		public void SubmitDelayedTask(ITask task, TimeSpan delayTime)
		{
			UserWorkloadManager.SendWatsonReportOnUnhandledException(delegate
			{
				if (this.Canceled)
				{
					return;
				}
				this.ValidateTask(task);
				if (delayTime > task.MaxExecutionTime)
				{
					delayTime = task.MaxExecutionTime.Add(UserWorkloadManager.ClockCorrectionForMaxDelay);
				}
				this.SubmitDelayedTask(this.GetWrappedTask(task), delayTime, false);
			});
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000AA60 File Offset: 0x00008C60
		public bool TrySubmitNewTask(ITask task)
		{
			bool isSubmitted = false;
			UserWorkloadManager.SendWatsonReportOnUnhandledException(delegate
			{
				if (this.Canceled)
				{
					return;
				}
				this.ValidateTask(task);
				WrappedTask wrappedTask = this.GetWrappedTask(task);
				isSubmitted = this.TrySubmitTask(wrappedTask, true, true);
			});
			return isSubmitted;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		public bool TrySubmitNewTaskForTest(ITask task, bool processTask)
		{
			if (this.Canceled)
			{
				return false;
			}
			this.ValidateTask(task);
			WrappedTask wrappedTask = this.GetWrappedTask(task);
			return this.TrySubmitTask(wrappedTask, true, processTask);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000AB21 File Offset: 0x00008D21
		internal static void SendWatsonReportOnUnhandledException(ExWatson.MethodDelegate methodDelegate)
		{
			ExWatson.SendReportOnUnhandledException(methodDelegate, delegate(object exception)
			{
				bool flag = true;
				Exception ex = exception as Exception;
				if (ex != null)
				{
					ExTraceGlobals.UserWorkloadManagerTracer.TraceError<Exception>(0L, "Encountered unhandled exception: {0}", ex);
					flag = !ExWatson.IsWatsonReportAlreadySent(ex);
					if (flag)
					{
						ExWatson.SetWatsonReportAlreadySent(ex);
					}
				}
				ExTraceGlobals.UserWorkloadManagerTracer.TraceError<bool>(0L, "SendWatsonReportOnUnhandledException shouldSendReport: {0}", flag);
				return flag;
			});
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000AB48 File Offset: 0x00008D48
		internal static UserWorkloadManagerPerfCounterWrapper GetPerfCounterWrapper(BudgetType budgetType)
		{
			UserWorkloadManagerPerfCounterWrapper userWorkloadManagerPerfCounterWrapper;
			if (!UserWorkloadManager.perfCounterWrappers.TryGetValue(budgetType, out userWorkloadManagerPerfCounterWrapper))
			{
				lock (UserWorkloadManager.perfCounterWrappers)
				{
					if (!UserWorkloadManager.perfCounterWrappers.TryGetValue(budgetType, out userWorkloadManagerPerfCounterWrapper))
					{
						userWorkloadManagerPerfCounterWrapper = new UserWorkloadManagerPerfCounterWrapper(budgetType);
						UserWorkloadManager.perfCounterWrappers.Add(budgetType, userWorkloadManagerPerfCounterWrapper);
					}
				}
			}
			return userWorkloadManagerPerfCounterWrapper;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		internal UserWorkloadManagerResult GetDiagnosticSnapshot(bool dumpCaches)
		{
			UserWorkloadManagerResult userWorkloadManagerResult = new UserWorkloadManagerResult();
			userWorkloadManagerResult.MaxTasksQueued = this.maxTasksQueued;
			userWorkloadManagerResult.MaxThreadCount = this.maxThreadCount;
			userWorkloadManagerResult.MaxDelayCacheTime = this.maxDelayCacheTime.ToString();
			userWorkloadManagerResult.CurrentWorkerThreads = this.workerThreadCount;
			userWorkloadManagerResult.IsQueueFull = this.IsQueueFull;
			userWorkloadManagerResult.Canceled = this.Canceled;
			userWorkloadManagerResult.TotalTaskCount = this.TotalTasks;
			userWorkloadManagerResult.QueuedTaskCount = this.QueueTaskCount;
			userWorkloadManagerResult.DelayedTaskCount = this.DelayedTaskCount;
			long num = this.synchronousExecutions;
			long num2 = this.asyncExecutions;
			userWorkloadManagerResult.SyncToAsyncRatio = string.Format("1:{0}", (num == 0L) ? "?" : ((double)num2 / (double)num).ToString("######0.0###"));
			userWorkloadManagerResult.SynchronousExecutionAllowed = this.PermitSynchronousExecution;
			userWorkloadManagerResult.TaskSubmissionFailuresPerMinute = (int)this.TaskSubmissionFailuresPerMinute;
			userWorkloadManagerResult.TasksCompletedPerMinute = (int)this.TasksCompletedPerMinute;
			userWorkloadManagerResult.TaskTimeoutsPerMinute = (int)this.TaskTimeoutsPerMinute;
			if (dumpCaches)
			{
				List<WLMTaskDetails> list;
				List<WLMTaskDetails> list2;
				List<WLMTaskDetails> list3;
				lock (this.instanceLock)
				{
					list = new List<WLMTaskDetails>(this.taskQueue.Count);
					list2 = new List<WLMTaskDetails>(this.delayCache.Count);
					list3 = new List<WLMTaskDetails>(this.ExecutingTaskCount);
					foreach (KeyValuePair<ITask, WrappedTask> keyValuePair in this.allActiveTasks)
					{
						WrappedTask value = keyValuePair.Value;
						TaskLocation taskLocation = value.TaskLocation;
						WLMTaskDetails taskDetails = value.GetTaskDetails();
						switch (taskLocation)
						{
						case TaskLocation.Queue:
							list.Add(taskDetails);
							break;
						case TaskLocation.DelayCache:
							list2.Add(taskDetails);
							break;
						case TaskLocation.Thread:
							list3.Add(taskDetails);
							break;
						}
					}
				}
				userWorkloadManagerResult.QueuedTasks = list;
				userWorkloadManagerResult.DelayedTasks = list2;
				userWorkloadManagerResult.ExecutingTasks = list3;
			}
			return userWorkloadManagerResult;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000AE48 File Offset: 0x00009048
		protected override void InternalDispose(bool disposing)
		{
			UserWorkloadManager.SendWatsonReportOnUnhandledException(delegate
			{
				if (disposing)
				{
					lock (this.instanceLock)
					{
						this.disposeLock.EnterWriteLock();
						try
						{
							this.InternalDispose();
						}
						finally
						{
							this.disposeLock.ExitWriteLock();
						}
					}
				}
			});
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000AE7A File Offset: 0x0000907A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UserWorkloadManager>(this);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000AE84 File Offset: 0x00009084
		private static int GetMaxThreads()
		{
			int result;
			int num;
			ThreadPool.GetMaxThreads(out result, out num);
			return result;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000AE9C File Offset: 0x0000909C
		private static void ValidateParams(int maxThreadCount, int maxTasksQueued, int maxDelayCacheTasks, TimeSpan maxDelayCacheTime)
		{
			if (maxThreadCount < 1)
			{
				throw new ArgumentOutOfRangeException("maxThreadCount", "maxThreadCount must be greater than 0.");
			}
			if (maxThreadCount > UserWorkloadManager.GetMaxThreads())
			{
				throw new ArgumentOutOfRangeException("maxThreadCount", "maxThreadCount may not be greater than " + UserWorkloadManager.GetMaxThreads().ToString());
			}
			if (maxTasksQueued < 1)
			{
				throw new ArgumentOutOfRangeException("maxTasksQueued", "maxTasksQueued must be greater than 0.");
			}
			if (maxDelayCacheTasks < 1)
			{
				throw new ArgumentOutOfRangeException("maxDelayCacheTasks", "maxDelayCacheTasks must be greater than 0.");
			}
			if (maxDelayCacheTime <= TimeSpan.Zero || maxDelayCacheTime > TimeSpan.FromDays(1.0))
			{
				throw new ArgumentOutOfRangeException("maxDelayCacheTime", "maxDelayCacheTime must be positive and less than one day");
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000AF78 File Offset: 0x00009178
		private static void WorkerCallback(object workloadManagerAsObject)
		{
			UserWorkloadManager workloadManager = (UserWorkloadManager)workloadManagerAsObject;
			UserWorkloadManager.SendWatsonReportOnUnhandledException(delegate
			{
				for (;;)
				{
					WrappedTask task = workloadManager.GetTask();
					if (task == null)
					{
						break;
					}
					workloadManager.Execute(task, false);
				}
			});
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000AFA8 File Offset: 0x000091A8
		private void ValidateTask(ITask task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task", "Task cannot be null");
			}
			if (task.MaxExecutionTime <= TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("ITask.MaxExecutionTime must be positive");
			}
			if (task.Budget == null)
			{
				throw new ArgumentException("task", "Task cannot have a null budget");
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000B000 File Offset: 0x00009200
		private bool TrySubmitTask(WrappedTask wrapper, bool isNewTask, bool processTask)
		{
			if (wrapper == null)
			{
				throw new ArgumentNullException("task");
			}
			BudgetType budgetType = wrapper.Task.Budget.Owner.BudgetType;
			ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "Task {0} submitted to UserWorkloadManager", wrapper.Task.Description);
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			lock (this.instanceLock)
			{
				if (this.Canceled)
				{
					return false;
				}
				if (isNewTask && this.IsQueueFull)
				{
					ExTraceGlobals.UserWorkloadManagerTracer.TraceError<string>((long)this.GetHashCode(), "Too many jobs queued, task {0} rejected", wrapper.Task.Description);
					flag3 = true;
					flag = false;
				}
				if (!flag3)
				{
					if (isNewTask)
					{
						this.AddTaskToActiveList(wrapper);
					}
					if (this.ExecuteTaskSynchronously(wrapper))
					{
						return true;
					}
					wrapper.StartQueue();
					this.taskQueue.Enqueue(wrapper);
					if (processTask && this.workerThreadCount < this.maxThreadCount)
					{
						this.workerThreadCount++;
						flag2 = true;
					}
				}
			}
			this.UpdatePerfCountersForSubmit(budgetType, isNewTask, flag3);
			if (flag2)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(UserWorkloadManager.WorkerCallback), this);
				this.IncrementWorkerThreadCount(budgetType);
			}
			if (!flag)
			{
				this.taskSubmissionFailuresPerMinute.Add(1U);
			}
			return flag;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000B16C File Offset: 0x0000936C
		private bool ExecuteTaskSynchronously(WrappedTask wrapper)
		{
			OverBudgetException ex;
			if (this.PermitSynchronousExecution && !wrapper.Task.Budget.TryCheckOverBudget(out ex) && this.GetBudgetBalance(wrapper) >= 0f)
			{
				UserWorkloadManager.SendWatsonReportOnUnhandledException(delegate
				{
					this.Execute(wrapper, true);
				});
				return true;
			}
			return false;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000B1DC File Offset: 0x000093DC
		private float GetBudgetBalance(WrappedTask wrapper)
		{
			StandardBudgetWrapper standardBudgetWrapper = wrapper.Task.Budget as StandardBudgetWrapper;
			if (standardBudgetWrapper == null)
			{
				return 0f;
			}
			return standardBudgetWrapper.GetInnerBudget().CasTokenBucket.GetBalance();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000B214 File Offset: 0x00009414
		private void UpdatePerfCountersForSubmit(BudgetType budgetType, bool isNewTask, bool isNewTaskRejected)
		{
			UserWorkloadManagerPerfCounterWrapper perfCounterWrapper = UserWorkloadManager.GetPerfCounterWrapper(budgetType);
			if (perfCounterWrapper != null)
			{
				if (isNewTask)
				{
					perfCounterWrapper.UpdateTotalNewTasksCount();
				}
				if (isNewTaskRejected)
				{
					perfCounterWrapper.UpdateTotalNewTaskRejectionsCount();
					return;
				}
				perfCounterWrapper.UpdateTaskQueueLength((long)this.taskQueue.Count);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000B250 File Offset: 0x00009450
		private bool PreExecuteValidation(WrappedTask wrapper, out LocalizedException preExecuteException)
		{
			preExecuteException = null;
			OverBudgetException ex;
			if (wrapper.Task.Budget.TryCheckOverBudget(out ex))
			{
				ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug<BudgetKey, string, OverBudgetException>((long)this.GetHashCode(), "[UserWorkloadManager.PreExecuteValidation] User {0} was over budget in part {1} and therefore the step will be canceled.  Exception: {2}", wrapper.Task.Budget.Owner, ex.PolicyPart, ex);
				preExecuteException = ex;
				return false;
			}
			bool flag = false;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(56604U, ref flag);
			if (flag)
			{
				preExecuteException = new OverBudgetException(wrapper.Task.Budget, "faultInjection", "0", 1000);
				return false;
			}
			ResourceUnhealthyException ex2;
			if (ResourceLoadDelayInfo.TryCheckResourceHealth(wrapper.Task.Budget, wrapper.Task.WorkloadSettings, wrapper.Task.GetResources(), out ex2))
			{
				ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug<BudgetKey, WorkloadSettings, ResourceKey>((long)this.GetHashCode(), "[UserWorkloadManager.PreExecuteValidation] User {0} encountered a resource in critical health for workload settings {1} and therefore the step will be canceled.  Resource: {2}", wrapper.Task.Budget.Owner, wrapper.Task.WorkloadSettings, ex2.ResourceKey);
				preExecuteException = ex2;
				return false;
			}
			return true;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000B348 File Offset: 0x00009548
		private void TraceInfiniteDelay(WrappedTask wrapper, DelayInfo delayInfo)
		{
			UserQuotaDelayInfo userQuotaDelayInfo = delayInfo as UserQuotaDelayInfo;
			if (userQuotaDelayInfo != null)
			{
				ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug<BudgetKey, OverBudgetException>((long)this.GetHashCode(), "[UserWorkloadManager.TraceInfiniteDelay] Caller {0} was over budget in a non-latency based policy part.  Exception: {1}", wrapper.Task.Budget.Owner, userQuotaDelayInfo.OverBudgetException);
				return;
			}
			ResourceLoadDelayInfo resourceLoadDelayInfo = delayInfo as ResourceLoadDelayInfo;
			ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "[UserWorkloadManager.TraceInfiniteDelay] Task accessed critical resource {0} and was stymied.", resourceLoadDelayInfo.ResourceKey);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000B3B0 File Offset: 0x000095B0
		private bool ShouldInterruptTask(WrappedTask wrapper, TaskExecuteResult stepResult, out bool canReclaimTaskWrapper)
		{
			if (stepResult == TaskExecuteResult.Undefined)
			{
				throw new ArgumentException("[UserWorkloadManager.ShouldInterruptTask] stepResult cannot be TaskExecuteResult.Undefined", "stepResult");
			}
			canReclaimTaskWrapper = true;
			ResourceKey[] previousStepResources = wrapper.PreviousStepResources;
			DelayInfo delayInfo = ResourceLoadDelayInfo.GetDelay(wrapper.Task.Budget, wrapper.Task.WorkloadSettings, previousStepResources, false);
			bool flag = false;
			if (this.OnGetDelayForTest != null)
			{
				delayInfo = this.OnGetDelayForTest(wrapper.Task, delayInfo);
				flag = true;
			}
			if (delayInfo.Delay == Budget.IndefiniteDelay)
			{
				this.TraceInfiniteDelay(wrapper, delayInfo);
				delayInfo = DelayInfo.NoDelay;
			}
			TimeSpan timeSpan = delayInfo.Delay + (flag ? TimeSpan.Zero : wrapper.UnaccountedForDelay);
			if (timeSpan > TimeSpan.Zero)
			{
				if (stepResult == TaskExecuteResult.StepComplete && timeSpan < UserWorkloadManager.MinimumEnforcedDelayTime)
				{
					ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug<TimeSpan, TimeSpan>((long)this.GetHashCode(), "[UserWorkloadManager.ShouldInterruptTask] Task step suggests delay, but delay {0} is less than the minimum threshold  of {1}.  Deferring delay.", timeSpan, UserWorkloadManager.MinimumEnforcedDelayTime);
					wrapper.UnaccountedForDelay += delayInfo.Delay;
					wrapper.Task.Budget.ResetWorkAccomplished();
					return false;
				}
				TimeSpan totalTime = wrapper.TotalTime;
				if (totalTime + timeSpan > wrapper.Task.MaxExecutionTime)
				{
					ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug<TimeSpan, TimeSpan>((long)this.GetHashCode(), "[UserWorkloadManager.ShouldInterruptTask] Suggested delay {0} would push us pass MaxExecutionTime {1} for the task.  Capping to MaxExecutionTime.", timeSpan, wrapper.Task.MaxExecutionTime);
					timeSpan = wrapper.Task.MaxExecutionTime - totalTime;
				}
				if (timeSpan > TimeSpan.Zero)
				{
					wrapper.UnaccountedForDelay = TimeSpan.Zero;
					wrapper.Task.Budget.ResetWorkAccomplished();
					wrapper.PreviousStepResult = stepResult;
					string instance = ResourceLoadDelayInfo.GetInstance(delayInfo);
					WorkloadManagementLogger.SetThrottlingValues(timeSpan, !(delayInfo is ResourceLoadDelayInfo), instance, wrapper.Task.GetActivityScope());
					if (!(timeSpan <= UserWorkloadManager.SynchronousDelayThreshold))
					{
						this.SubmitDelayedTask(wrapper, timeSpan, true);
						canReclaimTaskWrapper = false;
						return true;
					}
					ThrottlingPerfCounterWrapper.IncrementBudgetsMicroDelayed(wrapper.Task.Budget.Owner);
					Thread.Sleep(timeSpan);
					if (stepResult == TaskExecuteResult.ProcessingComplete)
					{
						this.CompleteTask(wrapper);
						canReclaimTaskWrapper = true;
						return true;
					}
					return false;
				}
			}
			if (stepResult == TaskExecuteResult.ProcessingComplete)
			{
				this.CompleteTask(wrapper);
				return true;
			}
			return false;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000B5C0 File Offset: 0x000097C0
		private void SubmitDelayedTask(WrappedTask wrapper, TimeSpan delayTime, bool frameworkSuggested)
		{
			if (delayTime <= TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("delayTime", delayTime, "DelayTime must be greater than zero.");
			}
			if (delayTime > this.maxDelayCacheTime)
			{
				throw new ArgumentOutOfRangeException("delayTime", delayTime, "DelayTime must be less than " + this.maxDelayCacheTime);
			}
			BudgetKey owner = wrapper.Task.Budget.Owner;
			ITask task = null;
			try
			{
				this.disposeLock.EnterReadLock();
				if (base.IsDisposed)
				{
					return;
				}
				task = wrapper.Task;
				if (!frameworkSuggested)
				{
					this.AddTaskToActiveList(wrapper);
				}
				wrapper.StartDelay();
				this.delayCache.TryAddAbsolute(wrapper.GetHashCode(), wrapper, delayTime);
			}
			finally
			{
				try
				{
					this.disposeLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			if (frameworkSuggested)
			{
				UserWorkloadManagerPerfCounterWrapper perfCounterWrapper = UserWorkloadManager.GetPerfCounterWrapper(owner.BudgetType);
				if (perfCounterWrapper != null)
				{
					perfCounterWrapper.UpdateCurrentDelayedTasks((long)this.delayCache.Count);
					perfCounterWrapper.UpdateMaxDelay(owner, (int)delayTime.TotalMilliseconds);
				}
			}
			if (this.OnDelayTask != null && task != null)
			{
				this.OnDelayTask(task, delayTime);
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000B80C File Offset: 0x00009A0C
		private void HandleDelayCacheRemove(int key, WrappedTask wrapper, RemoveReason reason)
		{
			UserWorkloadManager.SendWatsonReportOnUnhandledException(delegate
			{
				UserWorkloadManagerPerfCounterWrapper perfCounterWrapper = UserWorkloadManager.GetPerfCounterWrapper(wrapper.Task.Budget.Owner.BudgetType);
				if (perfCounterWrapper != null)
				{
					if (reason != RemoveReason.Cleanup)
					{
						perfCounterWrapper.UpdateCurrentDelayedTasks((long)this.DelayedTaskCount);
					}
					else
					{
						perfCounterWrapper.UpdateCurrentDelayedTasks(0L);
					}
				}
				wrapper.EndDelay();
				switch (reason)
				{
				case RemoveReason.Expired:
				case RemoveReason.PreemptivelyExpired:
				{
					if (this.OnTaskReleaseFromDelay != null)
					{
						this.OnTaskReleaseFromDelay(wrapper.Task, reason);
					}
					if (wrapper.PreviousStepResult == TaskExecuteResult.ProcessingComplete)
					{
						this.CompleteTask(wrapper);
						this.ReturnWrappedTaskToPool(wrapper);
						return;
					}
					this.TrySubmitTask(wrapper, false, true);
					bool canceled = this.Canceled;
					break;
				}
				case RemoveReason.Removed:
					break;
				case RemoveReason.Cleanup:
					this.CancelTask(wrapper);
					return;
				default:
					return;
				}
			});
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000B848 File Offset: 0x00009A48
		private WrappedTask GetTask()
		{
			WrappedTask wrappedTask = null;
			lock (this.instanceLock)
			{
				if (this.taskQueue.Count == 0 || this.Canceled)
				{
					this.workerThreadCount--;
				}
				else
				{
					wrappedTask = this.taskQueue.Dequeue();
					if (wrappedTask != null)
					{
						wrappedTask.EndQueue();
					}
				}
			}
			if (wrappedTask != null)
			{
				this.UpdatePerfCountersForWorkerCallback(wrappedTask);
			}
			return wrappedTask;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000B8CC File Offset: 0x00009ACC
		private void UpdatePerfCountersForWorkerCallback(WrappedTask wrapper)
		{
			if (wrapper != null)
			{
				TimeSpan totalTime = wrapper.TotalTime;
				UserWorkloadManagerPerfCounterWrapper perfCounterWrapper = UserWorkloadManager.GetPerfCounterWrapper(wrapper.Task.Budget.Owner.BudgetType);
				if (perfCounterWrapper != null)
				{
					perfCounterWrapper.UpdateAverageTaskWaitTime((long)totalTime.Milliseconds);
					perfCounterWrapper.UpdateTaskQueueLength((long)this.taskQueue.Count);
				}
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000B924 File Offset: 0x00009B24
		private void Execute(WrappedTask wrapper, bool synchronously)
		{
			if (synchronously)
			{
				Interlocked.Increment(ref this.synchronousExecutions);
			}
			else
			{
				Interlocked.Increment(ref this.asyncExecutions);
			}
			string description = wrapper.Task.Description;
			BudgetType budgetType = wrapper.Task.Budget.Owner.BudgetType;
			ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "Starting task execution {0}.  Synchronous? {1}", description, synchronously);
			ConnectionPoolManager.BlockImpersonatedCallers();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			UserWorkloadManagerPerfCounterWrapper perfCounterWrapper = UserWorkloadManager.GetPerfCounterWrapper(budgetType);
			WorkloadPolicy workloadPolicy = new WorkloadPolicy(wrapper.Task.WorkloadSettings.WorkloadType);
			WorkloadManagementLogger.SetWorkloadMetadataValues(wrapper.Task.WorkloadSettings.WorkloadType.ToString(), (workloadPolicy != null) ? workloadPolicy.Classification.ToString() : null, wrapper.Task.Budget.ThrottlingPolicy.IsServiceAccount, !wrapper.Task.WorkloadSettings.IsBackgroundLoad, wrapper.Task.GetActivityScope());
			try
			{
				for (;;)
				{
					flag = false;
					flag2 = false;
					if (this.Canceled)
					{
						break;
					}
					wrapper.PreviousStepResources = wrapper.Task.GetResources();
					bool flag4 = false;
					ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(3014012221U, ref flag4);
					TimeSpan totalTime = wrapper.TotalTime;
					if (totalTime > wrapper.Task.MaxExecutionTime || flag4)
					{
						goto IL_14F;
					}
					LocalizedException exception = null;
					if (!this.PreExecuteValidation(wrapper, out exception))
					{
						TaskExecuteResult stepResult = wrapper.CancelStep(exception);
						if (this.ShouldInterruptTask(wrapper, stepResult, out flag3))
						{
							break;
						}
					}
					else
					{
						ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "About to execute task {0}", description);
						flag = true;
						TaskExecuteResult taskExecuteResult = wrapper.Execute();
						flag2 = true;
						ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug<string, TaskExecuteResult>((long)this.GetHashCode(), "Finished task execution {0}, ExecuteResult: {1}", description, taskExecuteResult);
						if (this.ShouldInterruptTask(wrapper, taskExecuteResult, out flag3))
						{
							break;
						}
					}
				}
				return;
				IL_14F:
				if (perfCounterWrapper != null)
				{
					perfCounterWrapper.IncrementTimeoutsSeen();
				}
				this.TimeoutTask(wrapper);
			}
			finally
			{
				if (flag3)
				{
					this.ReturnWrappedTaskToPool(wrapper);
					wrapper = null;
				}
				if (flag && !flag2)
				{
					ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "Task {0} failed", description);
					if (perfCounterWrapper != null)
					{
						perfCounterWrapper.UpdateTotalTaskExecutionFailuresCount();
					}
				}
				if (!synchronously)
				{
					this.DecrementWorkerThreadCount(budgetType);
				}
				ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "Ending task execution {0}", description);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000BB80 File Offset: 0x00009D80
		private void IncrementWorkerThreadCount(BudgetType budgetType)
		{
			int num = 0;
			lock (this.workerThreadCounts)
			{
				if (!this.workerThreadCounts.TryGetValue(budgetType, out num))
				{
					this.workerThreadCounts.Add(budgetType, 1);
				}
				else
				{
					num = (this.workerThreadCounts[budgetType] = num + 1);
				}
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000BBEC File Offset: 0x00009DEC
		private void DecrementWorkerThreadCount(BudgetType budgetType)
		{
			int num = 0;
			lock (this.workerThreadCounts)
			{
				if (this.workerThreadCounts.TryGetValue(budgetType, out num))
				{
					num = Math.Max(0, num - 1);
					this.workerThreadCounts[budgetType] = num;
				}
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000BC50 File Offset: 0x00009E50
		private void HandleDelayCacheThreadException(Exception exception)
		{
			if (!(exception is ThreadAbortException) && !(exception is AppDomainUnloadedException))
			{
				ExWatson.SendReport(exception, ReportOptions.None, null);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000BC6C File Offset: 0x00009E6C
		private WrappedTask GetWrappedTask(ITask task)
		{
			WrappedTask result;
			lock (this.wrappedTaskPool)
			{
				if (this.wrappedTaskPool.Count == 0)
				{
					result = new WrappedTask(task);
				}
				else
				{
					WrappedTask wrappedTask = this.wrappedTaskPool.Pop();
					wrappedTask.Initialize(task);
					result = wrappedTask;
				}
			}
			return result;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		private void ReturnWrappedTaskToPool(WrappedTask wrapper)
		{
			wrapper.Initialize(null);
			if (this.wrappedTaskPool.Count >= this.maxWrappedTaskPoolSize)
			{
				ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug((long)this.GetHashCode(), "Wrapped task pool count > maximum wrapped task pool size, so not returning object to pool.");
				return;
			}
			lock (this.wrappedTaskPool)
			{
				if (this.wrappedTaskPool.Count >= this.maxWrappedTaskPoolSize)
				{
					ExTraceGlobals.UserWorkloadManagerTracer.TraceDebug((long)this.GetHashCode(), "Wrapped task pool count > maximum wrapped task pool size, so not returning object to pool.");
				}
				else
				{
					this.wrappedTaskPool.Push(wrapper);
				}
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000BD78 File Offset: 0x00009F78
		private void AddTaskToActiveList(WrappedTask wrapper)
		{
			ITask task = wrapper.Task;
			if (!this.allActiveTasks.TryAdd(task, wrapper))
			{
				string message = string.Format("The specified instance of Task '{0}' has already been submitted for execution. Cannot submit it again.", task.Description);
				ExTraceGlobals.UserWorkloadManagerTracer.TraceError((long)this.GetHashCode(), message);
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000BDC5 File Offset: 0x00009FC5
		private void CompleteTask(WrappedTask wrapper)
		{
			this.RemoveTaskFromActiveList(wrapper.Task);
			this.tasksCompletedPerMinute.Add(1U);
			wrapper.Complete();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000BDE5 File Offset: 0x00009FE5
		private void TimeoutTask(WrappedTask wrapper)
		{
			this.RemoveTaskFromActiveList(wrapper.Task);
			this.taskTimeoutsPerMinute.Add(1U);
			wrapper.Timeout();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000BE05 File Offset: 0x0000A005
		private void CancelTask(WrappedTask wrapper)
		{
			this.RemoveTaskFromActiveList(wrapper.Task);
			wrapper.Cancel();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000BE1C File Offset: 0x0000A01C
		private void RemoveTaskFromActiveList(ITask task)
		{
			WrappedTask wrappedTask;
			this.allActiveTasks.TryRemove(task, out wrappedTask);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000BE38 File Offset: 0x0000A038
		private void InternalDispose()
		{
			if (this.taskQueue != null)
			{
				foreach (WrappedTask wrappedTask in this.taskQueue)
				{
					wrappedTask.Cancel();
				}
				this.taskQueue.Clear();
			}
			if (this.delayCache != null)
			{
				this.delayCache.Dispose();
				this.delayCache = null;
			}
			lock (UserWorkloadManager.perfCounterWrappers)
			{
				foreach (UserWorkloadManagerPerfCounterWrapper userWorkloadManagerPerfCounterWrapper in UserWorkloadManager.perfCounterWrappers.Values)
				{
					userWorkloadManagerPerfCounterWrapper.UpdateCurrentDelayedTasks(0L);
				}
			}
		}

		// Token: 0x0400012A RID: 298
		private const uint LidWLMTimeout = 3014012221U;

		// Token: 0x0400012B RID: 299
		private const uint LidPreExecuteValidationOverBudget = 56604U;

		// Token: 0x0400012C RID: 300
		private static readonly TimeSpan SynchronousDelayThreshold = TimeSpan.FromMilliseconds(10.0);

		// Token: 0x0400012D RID: 301
		private static readonly TimeSpan DefaultMinimumEnforcedDelay = TimeSpan.FromMilliseconds(100.0);

		// Token: 0x0400012E RID: 302
		private static readonly IntAppSettingsEntry E14WLMMinimumEnforcedDelayTimeInSecondsEntry = new IntAppSettingsEntry("E14WLMMinimumEnforcedDelayTimeInSeconds", (int)UserWorkloadManager.DefaultMinimumEnforcedDelay.TotalSeconds, ExTraceGlobals.UserWorkloadManagerTracer);

		// Token: 0x0400012F RID: 303
		private static readonly BoolAppSettingsEntry PermitSynchronousTaskExecutionEntry = new BoolAppSettingsEntry("UserWorkloadManager.PermitSynchronousTaskExecution", false, ExTraceGlobals.UserWorkloadManagerTracer);

		// Token: 0x04000130 RID: 304
		private static readonly TimeSpan ClockCorrectionForMaxDelay = TimeSpan.FromMilliseconds(50.0);

		// Token: 0x04000131 RID: 305
		private static Dictionary<BudgetType, UserWorkloadManagerPerfCounterWrapper> perfCounterWrappers = new Dictionary<BudgetType, UserWorkloadManagerPerfCounterWrapper>();

		// Token: 0x04000132 RID: 306
		private static UserWorkloadManager singleton;

		// Token: 0x04000133 RID: 307
		private readonly int maxTasksQueued;

		// Token: 0x04000134 RID: 308
		private readonly int maxThreadCount;

		// Token: 0x04000135 RID: 309
		private readonly TimeSpan maxDelayCacheTime;

		// Token: 0x04000136 RID: 310
		private readonly int maxWrappedTaskPoolSize;

		// Token: 0x04000137 RID: 311
		private long synchronousExecutions;

		// Token: 0x04000138 RID: 312
		private long asyncExecutions;

		// Token: 0x04000139 RID: 313
		private FixedTimeSum taskSubmissionFailuresPerMinute = new FixedTimeSum(10000, 6);

		// Token: 0x0400013A RID: 314
		private FixedTimeSum tasksCompletedPerMinute = new FixedTimeSum(10000, 6);

		// Token: 0x0400013B RID: 315
		private FixedTimeSum taskTimeoutsPerMinute = new FixedTimeSum(10000, 6);

		// Token: 0x0400013C RID: 316
		private Dictionary<BudgetType, int> workerThreadCounts = new Dictionary<BudgetType, int>();

		// Token: 0x0400013D RID: 317
		private Queue<WrappedTask> taskQueue;

		// Token: 0x0400013E RID: 318
		private int workerThreadCount;

		// Token: 0x0400013F RID: 319
		private ExactTimeoutCache<int, WrappedTask> delayCache;

		// Token: 0x04000140 RID: 320
		private object instanceLock = new object();

		// Token: 0x04000141 RID: 321
		private ReaderWriterLockSlim disposeLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		// Token: 0x04000142 RID: 322
		private Stack<WrappedTask> wrappedTaskPool;

		// Token: 0x04000143 RID: 323
		private ConcurrentDictionary<ITask, WrappedTask> allActiveTasks;

		// Token: 0x02000041 RID: 65
		private enum DelayResult
		{
			// Token: 0x0400014B RID: 331
			None,
			// Token: 0x0400014C RID: 332
			Delay,
			// Token: 0x0400014D RID: 333
			CancelStepProcessingDone,
			// Token: 0x0400014E RID: 334
			CancelStep,
			// Token: 0x0400014F RID: 335
			Timeout
		}
	}
}
