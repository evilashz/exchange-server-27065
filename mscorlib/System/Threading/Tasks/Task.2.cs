using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	// Token: 0x02000531 RID: 1329
	[DebuggerTypeProxy(typeof(SystemThreadingTasks_TaskDebugView))]
	[DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class Task : IThreadPoolWorkItem, IAsyncResult, IDisposable
	{
		// Token: 0x06003F52 RID: 16210 RVA: 0x000EBBFC File Offset: 0x000E9DFC
		[FriendAccessAllowed]
		internal static bool AddToActiveTasks(Task task)
		{
			object obj = Task.s_activeTasksLock;
			lock (obj)
			{
				Task.s_currentActiveTasks[task.Id] = task;
			}
			return true;
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x000EBC48 File Offset: 0x000E9E48
		[FriendAccessAllowed]
		internal static void RemoveFromActiveTasks(int taskId)
		{
			object obj = Task.s_activeTasksLock;
			lock (obj)
			{
				Task.s_currentActiveTasks.Remove(taskId);
			}
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x000EBC90 File Offset: 0x000E9E90
		internal Task(bool canceled, TaskCreationOptions creationOptions, CancellationToken ct)
		{
			if (canceled)
			{
				this.m_stateFlags = (int)((TaskCreationOptions)5242880 | creationOptions);
				Task.ContingentProperties contingentProperties = this.m_contingentProperties = new Task.ContingentProperties();
				contingentProperties.m_cancellationToken = ct;
				contingentProperties.m_internalCancellationRequested = 1;
				return;
			}
			this.m_stateFlags = (int)((TaskCreationOptions)16777216 | creationOptions);
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x000EBCE6 File Offset: 0x000E9EE6
		internal Task()
		{
			this.m_stateFlags = 33555456;
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x000EBCFC File Offset: 0x000E9EFC
		internal Task(object state, TaskCreationOptions creationOptions, bool promiseStyle)
		{
			if ((creationOptions & ~(TaskCreationOptions.AttachedToParent | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
			if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None)
			{
				this.m_parent = Task.InternalCurrent;
			}
			this.TaskConstructorCore(null, state, default(CancellationToken), creationOptions, InternalTaskOptions.PromiseTask, null);
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x000EBD48 File Offset: 0x000E9F48
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action action) : this(action, null, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x000EBD74 File Offset: 0x000E9F74
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action action, CancellationToken cancellationToken) : this(action, null, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x000EBD98 File Offset: 0x000E9F98
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action action, TaskCreationOptions creationOptions) : this(action, null, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x000EBDCC File Offset: 0x000E9FCC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : this(action, null, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x000EBDF8 File Offset: 0x000E9FF8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action<object> action, object state) : this(action, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x000EBE24 File Offset: 0x000EA024
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action<object> action, object state, CancellationToken cancellationToken) : this(action, state, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x000EBE48 File Offset: 0x000EA048
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action<object> action, object state, TaskCreationOptions creationOptions) : this(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x000EBE7C File Offset: 0x000EA07C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : this(action, state, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x000EBEA7 File Offset: 0x000EA0A7
		internal Task(Action<object> action, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark) : this(action, state, parent, cancellationToken, creationOptions, internalOptions, scheduler)
		{
			this.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x000EBEC2 File Offset: 0x000EA0C2
		internal Task(Delegate action, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None || (internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
			{
				this.m_parent = parent;
			}
			this.TaskConstructorCore(action, state, cancellationToken, creationOptions, internalOptions, scheduler);
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x000EBF00 File Offset: 0x000EA100
		internal void TaskConstructorCore(object action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			this.m_action = action;
			this.m_stateObject = state;
			this.m_taskScheduler = scheduler;
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
			if ((creationOptions & TaskCreationOptions.LongRunning) != TaskCreationOptions.None && (internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Task_ctor_LRandSR"));
			}
			int num = (int)(creationOptions | (TaskCreationOptions)internalOptions);
			if (this.m_action == null || (internalOptions & InternalTaskOptions.ContinuationTask) != InternalTaskOptions.None)
			{
				num |= 33554432;
			}
			this.m_stateFlags = num;
			if (this.m_parent != null && (creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
			{
				this.m_parent.AddNewChild();
			}
			if (cancellationToken.CanBeCanceled)
			{
				this.AssignCancellationToken(cancellationToken, null, null);
			}
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x000EBFB8 File Offset: 0x000EA1B8
		private void AssignCancellationToken(CancellationToken cancellationToken, Task antecedent, TaskContinuation continuation)
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(false);
			contingentProperties.m_cancellationToken = cancellationToken;
			try
			{
				if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
				{
					cancellationToken.ThrowIfSourceDisposed();
				}
				if ((this.Options & (TaskCreationOptions)13312) == TaskCreationOptions.None)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						this.InternalCancel(false);
					}
					else
					{
						CancellationTokenRegistration value;
						if (antecedent == null)
						{
							value = cancellationToken.InternalRegisterWithoutEC(Task.s_taskCancelCallback, this);
						}
						else
						{
							value = cancellationToken.InternalRegisterWithoutEC(Task.s_taskCancelCallback, new Tuple<Task, Task, TaskContinuation>(this, antecedent, continuation));
						}
						contingentProperties.m_cancellationRegistration = new Shared<CancellationTokenRegistration>(value);
					}
				}
			}
			catch
			{
				if (this.m_parent != null && (this.Options & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.Options & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
				{
					this.m_parent.DisregardChild();
				}
				throw;
			}
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x000EC078 File Offset: 0x000EA278
		private static void TaskCancelCallback(object o)
		{
			Task task = o as Task;
			if (task == null)
			{
				Tuple<Task, Task, TaskContinuation> tuple = o as Tuple<Task, Task, TaskContinuation>;
				if (tuple != null)
				{
					task = tuple.Item1;
					Task item = tuple.Item2;
					TaskContinuation item2 = tuple.Item3;
					item.RemoveContinuation(item2);
				}
			}
			task.InternalCancel(false);
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x000EC0C0 File Offset: 0x000EA2C0
		private string DebuggerDisplayMethodDescription
		{
			get
			{
				Delegate @delegate = (Delegate)this.m_action;
				if (@delegate == null)
				{
					return "{null}";
				}
				return @delegate.Method.ToString();
			}
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x000EC0ED File Offset: 0x000EA2ED
		[SecuritySafeCritical]
		internal void PossiblyCaptureContext(ref StackCrawlMark stackMark)
		{
			this.CapturedContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x000EC0FC File Offset: 0x000EA2FC
		internal TaskCreationOptions Options
		{
			get
			{
				int stateFlags = this.m_stateFlags;
				return Task.OptionsMethod(stateFlags);
			}
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x000EC118 File Offset: 0x000EA318
		internal static TaskCreationOptions OptionsMethod(int flags)
		{
			return (TaskCreationOptions)(flags & 65535);
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x000EC124 File Offset: 0x000EA324
		internal bool AtomicStateUpdate(int newBits, int illegalBits)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int stateFlags = this.m_stateFlags;
				if ((stateFlags & illegalBits) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_stateFlags, stateFlags | newBits, stateFlags) == stateFlags)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x000EC168 File Offset: 0x000EA368
		internal bool AtomicStateUpdate(int newBits, int illegalBits, ref int oldFlags)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				oldFlags = this.m_stateFlags;
				if ((oldFlags & illegalBits) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_stateFlags, oldFlags | newBits, oldFlags) == oldFlags)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x000EC1B0 File Offset: 0x000EA3B0
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			if (enabled)
			{
				bool flag = this.AtomicStateUpdate(268435456, 90177536);
				return;
			}
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int stateFlags = this.m_stateFlags;
				int value = stateFlags & -268435457;
				if (Interlocked.CompareExchange(ref this.m_stateFlags, value, stateFlags) == stateFlags)
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x000EC204 File Offset: 0x000EA404
		internal bool NotifyDebuggerOfWaitCompletionIfNecessary()
		{
			if (this.IsWaitNotificationEnabled && this.ShouldNotifyDebuggerOfWaitCompletion)
			{
				this.NotifyDebuggerOfWaitCompletion();
				return true;
			}
			return false;
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x000EC220 File Offset: 0x000EA420
		internal static bool AnyTaskRequiresNotifyDebuggerOfWaitCompletion(Task[] tasks)
		{
			foreach (Task task in tasks)
			{
				if (task != null && task.IsWaitNotificationEnabled && task.ShouldNotifyDebuggerOfWaitCompletion)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06003F6D RID: 16237 RVA: 0x000EC257 File Offset: 0x000EA457
		internal bool IsWaitNotificationEnabledOrNotRanToCompletion
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.m_stateFlags & 285212672) != 16777216;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x000EC274 File Offset: 0x000EA474
		internal virtual bool ShouldNotifyDebuggerOfWaitCompletion
		{
			get
			{
				return this.IsWaitNotificationEnabled;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x000EC289 File Offset: 0x000EA489
		internal bool IsWaitNotificationEnabled
		{
			get
			{
				return (this.m_stateFlags & 268435456) != 0;
			}
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x000EC29C File Offset: 0x000EA49C
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private void NotifyDebuggerOfWaitCompletion()
		{
			this.SetNotificationForWaitCompletion(false);
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x000EC2A5 File Offset: 0x000EA4A5
		internal bool MarkStarted()
		{
			return this.AtomicStateUpdate(65536, 4259840);
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x000EC2B8 File Offset: 0x000EA4B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool FireTaskScheduledIfNeeded(TaskScheduler ts)
		{
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.IsEnabled() && (this.m_stateFlags & 1073741824) == 0)
			{
				this.m_stateFlags |= 1073741824;
				Task internalCurrent = Task.InternalCurrent;
				Task parent = this.m_parent;
				log.TaskScheduled(ts.Id, (internalCurrent == null) ? 0 : internalCurrent.Id, this.Id, (parent == null) ? 0 : parent.Id, (int)this.Options, Thread.GetDomainID());
				return true;
			}
			return false;
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x000EC340 File Offset: 0x000EA540
		internal void AddNewChild()
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
			if (contingentProperties.m_completionCountdown == 1 && !this.IsSelfReplicatingRoot)
			{
				contingentProperties.m_completionCountdown++;
				return;
			}
			Interlocked.Increment(ref contingentProperties.m_completionCountdown);
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x000EC388 File Offset: 0x000EA588
		internal void DisregardChild()
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
			Interlocked.Decrement(ref contingentProperties.m_completionCountdown);
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x000EC3A9 File Offset: 0x000EA5A9
		[__DynamicallyInvokable]
		public void Start()
		{
			this.Start(TaskScheduler.Current);
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x000EC3B8 File Offset: 0x000EA5B8
		[__DynamicallyInvokable]
		public void Start(TaskScheduler scheduler)
		{
			int stateFlags = this.m_stateFlags;
			if (Task.IsCompletedMethod(stateFlags))
			{
				throw new InvalidOperationException(Environment.GetResourceString("Task_Start_TaskCompleted"));
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions = Task.OptionsMethod(stateFlags);
			if ((taskCreationOptions & (TaskCreationOptions)1024) != TaskCreationOptions.None)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Task_Start_Promise"));
			}
			if ((taskCreationOptions & (TaskCreationOptions)512) != TaskCreationOptions.None)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Task_Start_ContinuationTask"));
			}
			if (Interlocked.CompareExchange<TaskScheduler>(ref this.m_taskScheduler, scheduler, null) != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Task_Start_AlreadyStarted"));
			}
			this.ScheduleAndStart(true);
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x000EC453 File Offset: 0x000EA653
		[__DynamicallyInvokable]
		public void RunSynchronously()
		{
			this.InternalRunSynchronously(TaskScheduler.Current, true);
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x000EC461 File Offset: 0x000EA661
		[__DynamicallyInvokable]
		public void RunSynchronously(TaskScheduler scheduler)
		{
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			this.InternalRunSynchronously(scheduler, true);
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x000EC47C File Offset: 0x000EA67C
		[SecuritySafeCritical]
		internal void InternalRunSynchronously(TaskScheduler scheduler, bool waitForCompletion)
		{
			int stateFlags = this.m_stateFlags;
			TaskCreationOptions taskCreationOptions = Task.OptionsMethod(stateFlags);
			if ((taskCreationOptions & (TaskCreationOptions)512) != TaskCreationOptions.None)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_Continuation"));
			}
			if ((taskCreationOptions & (TaskCreationOptions)1024) != TaskCreationOptions.None)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_Promise"));
			}
			if (Task.IsCompletedMethod(stateFlags))
			{
				throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_TaskCompleted"));
			}
			if (Interlocked.CompareExchange<TaskScheduler>(ref this.m_taskScheduler, scheduler, null) != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_AlreadyStarted"));
			}
			if (this.MarkStarted())
			{
				bool flag = false;
				try
				{
					if (!scheduler.TryRunInline(this, false))
					{
						scheduler.InternalQueueTask(this);
						flag = true;
					}
					if (waitForCompletion && !this.IsCompleted)
					{
						this.SpinThenBlockingWait(-1, default(CancellationToken));
					}
					return;
				}
				catch (Exception ex)
				{
					if (!flag && !(ex is ThreadAbortException))
					{
						TaskSchedulerException ex2 = new TaskSchedulerException(ex);
						this.AddException(ex2);
						this.Finish(false);
						this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
						throw ex2;
					}
					throw;
				}
			}
			throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_TaskCompleted"));
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x000EC5A0 File Offset: 0x000EA7A0
		internal static Task InternalStartNew(Task creatingTask, Delegate action, object state, CancellationToken cancellationToken, TaskScheduler scheduler, TaskCreationOptions options, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
		{
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task task = new Task(action, state, creatingTask, cancellationToken, options, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler);
			task.PossiblyCaptureContext(ref stackMark);
			task.ScheduleAndStart(false);
			return task;
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x000EC5E4 File Offset: 0x000EA7E4
		internal static int NewId()
		{
			int num;
			do
			{
				num = Interlocked.Increment(ref Task.s_taskIdCounter);
			}
			while (num == 0);
			TplEtwProvider.Log.NewID(num);
			return num;
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x000EC610 File Offset: 0x000EA810
		[__DynamicallyInvokable]
		public int Id
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_taskId == 0)
				{
					int value = Task.NewId();
					Interlocked.CompareExchange(ref this.m_taskId, value, 0);
				}
				return this.m_taskId;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06003F7D RID: 16253 RVA: 0x000EC644 File Offset: 0x000EA844
		[__DynamicallyInvokable]
		public static int? CurrentId
		{
			[__DynamicallyInvokable]
			get
			{
				Task internalCurrent = Task.InternalCurrent;
				if (internalCurrent != null)
				{
					return new int?(internalCurrent.Id);
				}
				return null;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06003F7E RID: 16254 RVA: 0x000EC66F File Offset: 0x000EA86F
		internal static Task InternalCurrent
		{
			get
			{
				return Task.t_currentTask;
			}
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x000EC676 File Offset: 0x000EA876
		internal static Task InternalCurrentIfAttached(TaskCreationOptions creationOptions)
		{
			if ((creationOptions & TaskCreationOptions.AttachedToParent) == TaskCreationOptions.None)
			{
				return null;
			}
			return Task.InternalCurrent;
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06003F80 RID: 16256 RVA: 0x000EC684 File Offset: 0x000EA884
		internal static StackGuard CurrentStackGuard
		{
			get
			{
				StackGuard stackGuard = Task.t_stackGuard;
				if (stackGuard == null)
				{
					stackGuard = (Task.t_stackGuard = new StackGuard());
				}
				return stackGuard;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06003F81 RID: 16257 RVA: 0x000EC6A8 File Offset: 0x000EA8A8
		[__DynamicallyInvokable]
		public AggregateException Exception
		{
			[__DynamicallyInvokable]
			get
			{
				AggregateException result = null;
				if (this.IsFaulted)
				{
					result = this.GetExceptions(false);
				}
				return result;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x000EC6C8 File Offset: 0x000EA8C8
		[__DynamicallyInvokable]
		public TaskStatus Status
		{
			[__DynamicallyInvokable]
			get
			{
				int stateFlags = this.m_stateFlags;
				TaskStatus result;
				if ((stateFlags & 2097152) != 0)
				{
					result = TaskStatus.Faulted;
				}
				else if ((stateFlags & 4194304) != 0)
				{
					result = TaskStatus.Canceled;
				}
				else if ((stateFlags & 16777216) != 0)
				{
					result = TaskStatus.RanToCompletion;
				}
				else if ((stateFlags & 8388608) != 0)
				{
					result = TaskStatus.WaitingForChildrenToComplete;
				}
				else if ((stateFlags & 131072) != 0)
				{
					result = TaskStatus.Running;
				}
				else if ((stateFlags & 65536) != 0)
				{
					result = TaskStatus.WaitingToRun;
				}
				else if ((stateFlags & 33554432) != 0)
				{
					result = TaskStatus.WaitingForActivation;
				}
				else
				{
					result = TaskStatus.Created;
				}
				return result;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06003F83 RID: 16259 RVA: 0x000EC73C File Offset: 0x000EA93C
		[__DynamicallyInvokable]
		public bool IsCanceled
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.m_stateFlags & 6291456) == 4194304;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x000EC754 File Offset: 0x000EA954
		internal bool IsCancellationRequested
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				return contingentProperties != null && (contingentProperties.m_internalCancellationRequested == 1 || contingentProperties.m_cancellationToken.IsCancellationRequested);
			}
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x000EC788 File Offset: 0x000EA988
		internal Task.ContingentProperties EnsureContingentPropertiesInitialized(bool needsProtection)
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties == null)
			{
				return this.EnsureContingentPropertiesInitializedCore(needsProtection);
			}
			return contingentProperties;
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x000EC7AC File Offset: 0x000EA9AC
		private Task.ContingentProperties EnsureContingentPropertiesInitializedCore(bool needsProtection)
		{
			if (needsProtection)
			{
				return LazyInitializer.EnsureInitialized<Task.ContingentProperties>(ref this.m_contingentProperties, Task.s_createContingentProperties);
			}
			return this.m_contingentProperties = new Task.ContingentProperties();
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06003F87 RID: 16263 RVA: 0x000EC7E0 File Offset: 0x000EA9E0
		internal CancellationToken CancellationToken
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null)
				{
					return contingentProperties.m_cancellationToken;
				}
				return default(CancellationToken);
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x000EC809 File Offset: 0x000EAA09
		internal bool IsCancellationAcknowledged
		{
			get
			{
				return (this.m_stateFlags & 1048576) != 0;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x000EC81C File Offset: 0x000EAA1C
		[__DynamicallyInvokable]
		public bool IsCompleted
		{
			[__DynamicallyInvokable]
			get
			{
				int stateFlags = this.m_stateFlags;
				return Task.IsCompletedMethod(stateFlags);
			}
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x000EC838 File Offset: 0x000EAA38
		private static bool IsCompletedMethod(int flags)
		{
			return (flags & 23068672) != 0;
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x000EC844 File Offset: 0x000EAA44
		internal bool IsRanToCompletion
		{
			get
			{
				return (this.m_stateFlags & 23068672) == 16777216;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x000EC85B File Offset: 0x000EAA5B
		[__DynamicallyInvokable]
		public TaskCreationOptions CreationOptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Options & (TaskCreationOptions)(-65281);
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000EC86C File Offset: 0x000EAA6C
		[__DynamicallyInvokable]
		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			[__DynamicallyInvokable]
			get
			{
				bool flag = (this.m_stateFlags & 262144) != 0;
				if (flag)
				{
					throw new ObjectDisposedException(null, Environment.GetResourceString("Task_ThrowIfDisposed"));
				}
				return this.CompletedEvent.WaitHandle;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06003F8E RID: 16270 RVA: 0x000EC8AA File Offset: 0x000EAAAA
		[__DynamicallyInvokable]
		public object AsyncState
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_stateObject;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06003F8F RID: 16271 RVA: 0x000EC8B2 File Offset: 0x000EAAB2
		[__DynamicallyInvokable]
		bool IAsyncResult.CompletedSynchronously
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06003F90 RID: 16272 RVA: 0x000EC8B5 File Offset: 0x000EAAB5
		internal TaskScheduler ExecutingTaskScheduler
		{
			get
			{
				return this.m_taskScheduler;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x000EC8BD File Offset: 0x000EAABD
		[__DynamicallyInvokable]
		public static TaskFactory Factory
		{
			[__DynamicallyInvokable]
			get
			{
				return Task.s_factory;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06003F92 RID: 16274 RVA: 0x000EC8C4 File Offset: 0x000EAAC4
		[__DynamicallyInvokable]
		public static Task CompletedTask
		{
			[__DynamicallyInvokable]
			get
			{
				Task task = Task.s_completedTask;
				if (task == null)
				{
					task = (Task.s_completedTask = new Task(false, (TaskCreationOptions)16384, default(CancellationToken)));
				}
				return task;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x000EC8F8 File Offset: 0x000EAAF8
		internal ManualResetEventSlim CompletedEvent
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
				if (contingentProperties.m_completionEvent == null)
				{
					bool isCompleted = this.IsCompleted;
					ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim(isCompleted);
					if (Interlocked.CompareExchange<ManualResetEventSlim>(ref contingentProperties.m_completionEvent, manualResetEventSlim, null) != null)
					{
						manualResetEventSlim.Dispose();
					}
					else if (!isCompleted && this.IsCompleted)
					{
						manualResetEventSlim.Set();
					}
				}
				return contingentProperties.m_completionEvent;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06003F94 RID: 16276 RVA: 0x000EC955 File Offset: 0x000EAB55
		internal bool IsSelfReplicatingRoot
		{
			get
			{
				return (this.Options & (TaskCreationOptions)2304) == (TaskCreationOptions)2048;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003F95 RID: 16277 RVA: 0x000EC96A File Offset: 0x000EAB6A
		internal bool IsChildReplica
		{
			get
			{
				return (this.Options & (TaskCreationOptions)256) > TaskCreationOptions.None;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003F96 RID: 16278 RVA: 0x000EC97C File Offset: 0x000EAB7C
		internal int ActiveChildCount
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties == null)
				{
					return 0;
				}
				return contingentProperties.m_completionCountdown - 1;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06003F97 RID: 16279 RVA: 0x000EC9A4 File Offset: 0x000EABA4
		internal bool ExceptionRecorded
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				return contingentProperties != null && contingentProperties.m_exceptionsHolder != null && contingentProperties.m_exceptionsHolder.ContainsFaultList;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06003F98 RID: 16280 RVA: 0x000EC9D6 File Offset: 0x000EABD6
		[__DynamicallyInvokable]
		public bool IsFaulted
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.m_stateFlags & 2097152) != 0;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06003F99 RID: 16281 RVA: 0x000EC9EC File Offset: 0x000EABEC
		// (set) Token: 0x06003F9A RID: 16282 RVA: 0x000ECA30 File Offset: 0x000EAC30
		internal ExecutionContext CapturedContext
		{
			get
			{
				if ((this.m_stateFlags & 536870912) == 536870912)
				{
					return null;
				}
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null && contingentProperties.m_capturedContext != null)
				{
					return contingentProperties.m_capturedContext;
				}
				return ExecutionContext.PreAllocatedDefault;
			}
			set
			{
				if (value == null)
				{
					this.m_stateFlags |= 536870912;
					return;
				}
				if (!value.IsPreAllocatedDefault)
				{
					this.EnsureContingentPropertiesInitialized(false).m_capturedContext = value;
				}
			}
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x000ECA61 File Offset: 0x000EAC61
		private static ExecutionContext CopyExecutionContext(ExecutionContext capturedContext)
		{
			if (capturedContext == null)
			{
				return null;
			}
			if (capturedContext.IsPreAllocatedDefault)
			{
				return ExecutionContext.PreAllocatedDefault;
			}
			return capturedContext.CreateCopy();
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x000ECA7C File Offset: 0x000EAC7C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x000ECA8C File Offset: 0x000EAC8C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if ((this.Options & (TaskCreationOptions)16384) != TaskCreationOptions.None)
				{
					return;
				}
				if (!this.IsCompleted)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Task_Dispose_NotCompleted"));
				}
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null)
				{
					ManualResetEventSlim completionEvent = contingentProperties.m_completionEvent;
					if (completionEvent != null)
					{
						contingentProperties.m_completionEvent = null;
						if (!completionEvent.IsSet)
						{
							completionEvent.Set();
						}
						completionEvent.Dispose();
					}
				}
			}
			this.m_stateFlags |= 262144;
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x000ECB10 File Offset: 0x000EAD10
		[SecuritySafeCritical]
		internal void ScheduleAndStart(bool needsProtection)
		{
			if (needsProtection)
			{
				if (!this.MarkStarted())
				{
					return;
				}
			}
			else
			{
				this.m_stateFlags |= 65536;
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(this);
			}
			if (AsyncCausalityTracer.LoggingOn && (this.Options & (TaskCreationOptions)512) == TaskCreationOptions.None)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "Task: " + ((Delegate)this.m_action).Method.Name, 0UL);
			}
			try
			{
				this.m_taskScheduler.InternalQueueTask(this);
			}
			catch (ThreadAbortException exceptionObject)
			{
				this.AddException(exceptionObject);
				this.FinishThreadAbortedTask(true, false);
			}
			catch (Exception innerException)
			{
				TaskSchedulerException ex = new TaskSchedulerException(innerException);
				this.AddException(ex);
				this.Finish(false);
				if ((this.Options & (TaskCreationOptions)512) == TaskCreationOptions.None)
				{
					this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
				}
				throw ex;
			}
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x000ECC08 File Offset: 0x000EAE08
		internal void AddException(object exceptionObject)
		{
			this.AddException(exceptionObject, false);
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x000ECC14 File Offset: 0x000EAE14
		internal void AddException(object exceptionObject, bool representsCancellation)
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
			if (contingentProperties.m_exceptionsHolder == null)
			{
				TaskExceptionHolder taskExceptionHolder = new TaskExceptionHolder(this);
				if (Interlocked.CompareExchange<TaskExceptionHolder>(ref contingentProperties.m_exceptionsHolder, taskExceptionHolder, null) != null)
				{
					taskExceptionHolder.MarkAsHandled(false);
				}
			}
			Task.ContingentProperties obj = contingentProperties;
			lock (obj)
			{
				contingentProperties.m_exceptionsHolder.Add(exceptionObject, representsCancellation);
			}
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x000ECC88 File Offset: 0x000EAE88
		private AggregateException GetExceptions(bool includeTaskCanceledExceptions)
		{
			Exception ex = null;
			if (includeTaskCanceledExceptions && this.IsCanceled)
			{
				ex = new TaskCanceledException(this);
			}
			if (this.ExceptionRecorded)
			{
				return this.m_contingentProperties.m_exceptionsHolder.CreateExceptionObject(false, ex);
			}
			if (ex != null)
			{
				return new AggregateException(new Exception[]
				{
					ex
				});
			}
			return null;
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x000ECCDC File Offset: 0x000EAEDC
		internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
		{
			if (!this.IsFaulted || !this.ExceptionRecorded)
			{
				return new ReadOnlyCollection<ExceptionDispatchInfo>(new ExceptionDispatchInfo[0]);
			}
			return this.m_contingentProperties.m_exceptionsHolder.GetExceptionDispatchInfos();
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x000ECD20 File Offset: 0x000EAF20
		internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties == null)
			{
				return null;
			}
			TaskExceptionHolder exceptionsHolder = contingentProperties.m_exceptionsHolder;
			if (exceptionsHolder == null)
			{
				return null;
			}
			return exceptionsHolder.GetCancellationExceptionDispatchInfo();
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x000ECD50 File Offset: 0x000EAF50
		internal void ThrowIfExceptional(bool includeTaskCanceledExceptions)
		{
			Exception exceptions = this.GetExceptions(includeTaskCanceledExceptions);
			if (exceptions != null)
			{
				this.UpdateExceptionObservedStatus();
				throw exceptions;
			}
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000ECD70 File Offset: 0x000EAF70
		internal void UpdateExceptionObservedStatus()
		{
			if (this.m_parent != null && (this.Options & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None && Task.InternalCurrent == this.m_parent)
			{
				this.m_stateFlags |= 524288;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06003FA6 RID: 16294 RVA: 0x000ECDC1 File Offset: 0x000EAFC1
		internal bool IsExceptionObservedByParent
		{
			get
			{
				return (this.m_stateFlags & 524288) != 0;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06003FA7 RID: 16295 RVA: 0x000ECDD4 File Offset: 0x000EAFD4
		internal bool IsDelegateInvoked
		{
			get
			{
				return (this.m_stateFlags & 131072) != 0;
			}
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x000ECDE8 File Offset: 0x000EAFE8
		internal void Finish(bool bUserDelegateExecuted)
		{
			if (!bUserDelegateExecuted)
			{
				this.FinishStageTwo();
				return;
			}
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties == null || (contingentProperties.m_completionCountdown == 1 && !this.IsSelfReplicatingRoot) || Interlocked.Decrement(ref contingentProperties.m_completionCountdown) == 0)
			{
				this.FinishStageTwo();
			}
			else
			{
				this.AtomicStateUpdate(8388608, 23068672);
			}
			List<Task> list = (contingentProperties != null) ? contingentProperties.m_exceptionalChildren : null;
			if (list != null)
			{
				List<Task> obj = list;
				lock (obj)
				{
					list.RemoveAll(Task.s_IsExceptionObservedByParentPredicate);
				}
			}
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x000ECE8C File Offset: 0x000EB08C
		internal void FinishStageTwo()
		{
			this.AddExceptionsFromChildren();
			int num;
			if (this.ExceptionRecorded)
			{
				num = 2097152;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(this.Id);
				}
			}
			else if (this.IsCancellationRequested && this.IsCancellationAcknowledged)
			{
				num = 4194304;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Canceled);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(this.Id);
				}
			}
			else
			{
				num = 16777216;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(this.Id);
				}
			}
			Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | num);
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties != null)
			{
				contingentProperties.SetCompleted();
				contingentProperties.DeregisterCancellationCallback();
			}
			this.FinishStageThree();
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x000ECF74 File Offset: 0x000EB174
		internal void FinishStageThree()
		{
			this.m_action = null;
			if (this.m_parent != null && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None && (this.m_stateFlags & 65535 & 4) != 0)
			{
				this.m_parent.ProcessChildCompletion(this);
			}
			this.FinishContinuations();
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x000ECFC4 File Offset: 0x000EB1C4
		internal void ProcessChildCompletion(Task childTask)
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (childTask.IsFaulted && !childTask.IsExceptionObservedByParent)
			{
				if (contingentProperties.m_exceptionalChildren == null)
				{
					Interlocked.CompareExchange<List<Task>>(ref contingentProperties.m_exceptionalChildren, new List<Task>(), null);
				}
				List<Task> exceptionalChildren = contingentProperties.m_exceptionalChildren;
				if (exceptionalChildren != null)
				{
					List<Task> obj = exceptionalChildren;
					lock (obj)
					{
						exceptionalChildren.Add(childTask);
					}
				}
			}
			if (Interlocked.Decrement(ref contingentProperties.m_completionCountdown) == 0)
			{
				this.FinishStageTwo();
			}
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x000ED054 File Offset: 0x000EB254
		internal void AddExceptionsFromChildren()
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			List<Task> list = (contingentProperties != null) ? contingentProperties.m_exceptionalChildren : null;
			if (list != null)
			{
				List<Task> obj = list;
				lock (obj)
				{
					foreach (Task task in list)
					{
						if (task.IsFaulted && !task.IsExceptionObservedByParent)
						{
							TaskExceptionHolder exceptionsHolder = task.m_contingentProperties.m_exceptionsHolder;
							this.AddException(exceptionsHolder.CreateExceptionObject(false, null));
						}
					}
				}
				contingentProperties.m_exceptionalChildren = null;
			}
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x000ED118 File Offset: 0x000EB318
		internal void FinishThreadAbortedTask(bool bTAEAddedToExceptionHolder, bool delegateRan)
		{
			if (bTAEAddedToExceptionHolder)
			{
				this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
			}
			if (!this.AtomicStateUpdate(134217728, 157286400))
			{
				return;
			}
			this.Finish(delegateRan);
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x000ED14C File Offset: 0x000EB34C
		private void Execute()
		{
			if (this.IsSelfReplicatingRoot)
			{
				Task.ExecuteSelfReplicating(this);
				return;
			}
			try
			{
				this.InnerInvoke();
			}
			catch (ThreadAbortException unhandledException)
			{
				if (!this.IsChildReplica)
				{
					this.HandleException(unhandledException);
					this.FinishThreadAbortedTask(true, true);
				}
			}
			catch (Exception unhandledException2)
			{
				this.HandleException(unhandledException2);
			}
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x000ED1B4 File Offset: 0x000EB3B4
		internal virtual bool ShouldReplicate()
		{
			return true;
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x000ED1B8 File Offset: 0x000EB3B8
		internal virtual Task CreateReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
		{
			return new Task(taskReplicaDelegate, stateObject, parentTask, default(CancellationToken), creationOptionsForReplica, internalOptionsForReplica, parentTask.ExecutingTaskScheduler);
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06003FB1 RID: 16305 RVA: 0x000ED1E0 File Offset: 0x000EB3E0
		// (set) Token: 0x06003FB2 RID: 16306 RVA: 0x000ED1E3 File Offset: 0x000EB3E3
		internal virtual object SavedStateForNextReplica
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06003FB3 RID: 16307 RVA: 0x000ED1E5 File Offset: 0x000EB3E5
		// (set) Token: 0x06003FB4 RID: 16308 RVA: 0x000ED1E8 File Offset: 0x000EB3E8
		internal virtual object SavedStateFromPreviousReplica
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06003FB5 RID: 16309 RVA: 0x000ED1EA File Offset: 0x000EB3EA
		// (set) Token: 0x06003FB6 RID: 16310 RVA: 0x000ED1ED File Offset: 0x000EB3ED
		internal virtual Task HandedOverChildReplica
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x000ED1F0 File Offset: 0x000EB3F0
		private static void ExecuteSelfReplicating(Task root)
		{
			TaskCreationOptions creationOptionsForReplicas = root.CreationOptions | TaskCreationOptions.AttachedToParent;
			InternalTaskOptions internalOptionsForReplicas = InternalTaskOptions.ChildReplica | InternalTaskOptions.SelfReplicating | InternalTaskOptions.QueuedByRuntime;
			bool replicasAreQuitting = false;
			Action<object> taskReplicaDelegate = null;
			taskReplicaDelegate = delegate(object <p0>)
			{
				Task internalCurrent = Task.InternalCurrent;
				Task task = internalCurrent.HandedOverChildReplica;
				if (task == null)
				{
					if (!root.ShouldReplicate())
					{
						return;
					}
					if (Volatile.Read(ref replicasAreQuitting))
					{
						return;
					}
					ExecutionContext capturedContext = root.CapturedContext;
					task = root.CreateReplicaTask(taskReplicaDelegate, root.m_stateObject, root, root.ExecutingTaskScheduler, creationOptionsForReplicas, internalOptionsForReplicas);
					task.CapturedContext = Task.CopyExecutionContext(capturedContext);
					task.ScheduleAndStart(false);
				}
				try
				{
					root.InnerInvokeWithArg(internalCurrent);
				}
				catch (Exception ex)
				{
					root.HandleException(ex);
					if (ex is ThreadAbortException)
					{
						internalCurrent.FinishThreadAbortedTask(false, true);
					}
				}
				object savedStateForNextReplica = internalCurrent.SavedStateForNextReplica;
				if (savedStateForNextReplica != null)
				{
					Task task2 = root.CreateReplicaTask(taskReplicaDelegate, root.m_stateObject, root, root.ExecutingTaskScheduler, creationOptionsForReplicas, internalOptionsForReplicas);
					ExecutionContext capturedContext2 = root.CapturedContext;
					task2.CapturedContext = Task.CopyExecutionContext(capturedContext2);
					task2.HandedOverChildReplica = task;
					task2.SavedStateFromPreviousReplica = savedStateForNextReplica;
					task2.ScheduleAndStart(false);
					return;
				}
				replicasAreQuitting = true;
				try
				{
					task.InternalCancel(true);
				}
				catch (Exception unhandledException)
				{
					root.HandleException(unhandledException);
				}
			};
			taskReplicaDelegate(null);
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x000ED254 File Offset: 0x000EB454
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.ExecuteEntry(false);
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x000ED25E File Offset: 0x000EB45E
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
			if (!this.IsCompleted)
			{
				this.HandleException(tae);
				this.FinishThreadAbortedTask(true, false);
			}
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x000ED278 File Offset: 0x000EB478
		[SecuritySafeCritical]
		internal bool ExecuteEntry(bool bPreventDoubleExecution)
		{
			if (bPreventDoubleExecution || (this.Options & (TaskCreationOptions)2048) != TaskCreationOptions.None)
			{
				int num = 0;
				if (!this.AtomicStateUpdate(131072, 23199744, ref num) && (num & 4194304) == 0)
				{
					return false;
				}
			}
			else
			{
				this.m_stateFlags |= 131072;
			}
			if (!this.IsCancellationRequested && !this.IsCanceled)
			{
				this.ExecuteWithThreadLocal(ref Task.t_currentTask);
			}
			else if (!this.IsCanceled)
			{
				int num2 = Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304);
				if ((num2 & 4194304) == 0)
				{
					this.CancellationCleanupLogic();
				}
			}
			return true;
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x000ED31C File Offset: 0x000EB51C
		[SecurityCritical]
		private void ExecuteWithThreadLocal(ref Task currentTaskSlot)
		{
			Task task = currentTaskSlot;
			TplEtwProvider log = TplEtwProvider.Log;
			Guid currentThreadActivityId = default(Guid);
			bool flag = log.IsEnabled();
			if (flag)
			{
				if (log.TasksSetActivityIds)
				{
					EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(this.Id), out currentThreadActivityId);
				}
				if (task != null)
				{
					log.TaskStarted(task.m_taskScheduler.Id, task.Id, this.Id);
				}
				else
				{
					log.TaskStarted(TaskScheduler.Current.Id, 0, this.Id);
				}
			}
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, this.Id, CausalitySynchronousWork.Execution);
			}
			try
			{
				currentTaskSlot = this;
				ExecutionContext capturedContext = this.CapturedContext;
				if (capturedContext == null)
				{
					this.Execute();
				}
				else
				{
					if (this.IsSelfReplicatingRoot || this.IsChildReplica)
					{
						this.CapturedContext = Task.CopyExecutionContext(capturedContext);
					}
					ContextCallback contextCallback = Task.s_ecCallback;
					if (contextCallback == null)
					{
						contextCallback = (Task.s_ecCallback = new ContextCallback(Task.ExecutionContextCallback));
					}
					ExecutionContext.Run(capturedContext, contextCallback, this, true);
				}
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
				}
				this.Finish(true);
			}
			finally
			{
				currentTaskSlot = task;
				if (flag)
				{
					if (task != null)
					{
						log.TaskCompleted(task.m_taskScheduler.Id, task.Id, this.Id, this.IsFaulted);
					}
					else
					{
						log.TaskCompleted(TaskScheduler.Current.Id, 0, this.Id, this.IsFaulted);
					}
					if (log.TasksSetActivityIds)
					{
						EventSource.SetCurrentThreadActivityId(currentThreadActivityId);
					}
				}
			}
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x000ED48C File Offset: 0x000EB68C
		[SecurityCritical]
		private static void ExecutionContextCallback(object obj)
		{
			Task task = obj as Task;
			task.Execute();
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x000ED4A8 File Offset: 0x000EB6A8
		internal virtual void InnerInvoke()
		{
			Action action = this.m_action as Action;
			if (action != null)
			{
				action();
				return;
			}
			Action<object> action2 = this.m_action as Action<object>;
			if (action2 != null)
			{
				action2(this.m_stateObject);
				return;
			}
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x000ED4E7 File Offset: 0x000EB6E7
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal void InnerInvokeWithArg(Task childTask)
		{
			this.InnerInvoke();
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x000ED4F0 File Offset: 0x000EB6F0
		private void HandleException(Exception unhandledException)
		{
			OperationCanceledException ex = unhandledException as OperationCanceledException;
			if (ex != null && this.IsCancellationRequested && this.m_contingentProperties.m_cancellationToken == ex.CancellationToken)
			{
				this.SetCancellationAcknowledged();
				this.AddException(ex, true);
				return;
			}
			this.AddException(unhandledException);
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x000ED53F File Offset: 0x000EB73F
		[__DynamicallyInvokable]
		public TaskAwaiter GetAwaiter()
		{
			return new TaskAwaiter(this);
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x000ED547 File Offset: 0x000EB747
		[__DynamicallyInvokable]
		public ConfiguredTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredTaskAwaitable(this, continueOnCapturedContext);
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x000ED550 File Offset: 0x000EB750
		[SecurityCritical]
		internal void SetContinuationForAwait(Action continuationAction, bool continueOnCapturedContext, bool flowExecutionContext, ref StackCrawlMark stackMark)
		{
			TaskContinuation taskContinuation = null;
			if (continueOnCapturedContext)
			{
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					taskContinuation = new SynchronizationContextAwaitTaskContinuation(currentNoFlow, continuationAction, flowExecutionContext, ref stackMark);
				}
				else
				{
					TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
					if (internalCurrent != null && internalCurrent != TaskScheduler.Default)
					{
						taskContinuation = new TaskSchedulerAwaitTaskContinuation(internalCurrent, continuationAction, flowExecutionContext, ref stackMark);
					}
				}
			}
			if (taskContinuation == null && flowExecutionContext)
			{
				taskContinuation = new AwaitTaskContinuation(continuationAction, true, ref stackMark);
			}
			if (taskContinuation != null)
			{
				if (!this.AddTaskContinuation(taskContinuation, false))
				{
					taskContinuation.Run(this, false);
					return;
				}
			}
			else if (!this.AddTaskContinuation(continuationAction, false))
			{
				AwaitTaskContinuation.UnsafeScheduleAction(continuationAction, this);
			}
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x000ED5E4 File Offset: 0x000EB7E4
		[__DynamicallyInvokable]
		public static YieldAwaitable Yield()
		{
			return default(YieldAwaitable);
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x000ED5FC File Offset: 0x000EB7FC
		[__DynamicallyInvokable]
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x000ED61C File Offset: 0x000EB81C
		[__DynamicallyInvokable]
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x000ED65C File Offset: 0x000EB85C
		[__DynamicallyInvokable]
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x000ED668 File Offset: 0x000EB868
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x000ED688 File Offset: 0x000EB888
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!this.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				return true;
			}
			if (!this.InternalWait(millisecondsTimeout, cancellationToken))
			{
				return false;
			}
			if (this.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				this.NotifyDebuggerOfWaitCompletionIfNecessary();
				if (this.IsCanceled)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				this.ThrowIfExceptional(true);
			}
			return true;
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x000ED6E0 File Offset: 0x000EB8E0
		private bool WrappedTryRunInline()
		{
			if (this.m_taskScheduler == null)
			{
				return false;
			}
			bool result;
			try
			{
				result = this.m_taskScheduler.TryRunInline(this, true);
			}
			catch (Exception ex)
			{
				if (!(ex is ThreadAbortException))
				{
					TaskSchedulerException ex2 = new TaskSchedulerException(ex);
					throw ex2;
				}
				throw;
			}
			return result;
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x000ED730 File Offset: 0x000EB930
		[MethodImpl(MethodImplOptions.NoOptimization)]
		internal bool InternalWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			TplEtwProvider log = TplEtwProvider.Log;
			bool flag = log.IsEnabled();
			if (flag)
			{
				Task internalCurrent = Task.InternalCurrent;
				log.TaskWaitBegin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.Id, TplEtwProvider.TaskWaitBehavior.Synchronous, 0, Thread.GetDomainID());
			}
			bool flag2 = this.IsCompleted;
			if (!flag2)
			{
				Debugger.NotifyOfCrossThreadDependency();
				flag2 = ((millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled && this.WrappedTryRunInline() && this.IsCompleted) || this.SpinThenBlockingWait(millisecondsTimeout, cancellationToken));
			}
			if (flag)
			{
				Task internalCurrent2 = Task.InternalCurrent;
				if (internalCurrent2 != null)
				{
					log.TaskWaitEnd(internalCurrent2.m_taskScheduler.Id, internalCurrent2.Id, this.Id);
				}
				else
				{
					log.TaskWaitEnd(TaskScheduler.Default.Id, 0, this.Id);
				}
				log.TaskWaitContinuationComplete(this.Id);
			}
			return flag2;
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x000ED818 File Offset: 0x000EBA18
		private bool SpinThenBlockingWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			bool flag = millisecondsTimeout == -1;
			uint num = (uint)(flag ? 0 : Environment.TickCount);
			bool flag2 = this.SpinWait(millisecondsTimeout);
			if (!flag2)
			{
				Task.SetOnInvokeMres setOnInvokeMres = new Task.SetOnInvokeMres();
				try
				{
					this.AddCompletionAction(setOnInvokeMres, true);
					if (flag)
					{
						flag2 = setOnInvokeMres.Wait(-1, cancellationToken);
					}
					else
					{
						uint num2 = (uint)(Environment.TickCount - (int)num);
						if ((ulong)num2 < (ulong)((long)millisecondsTimeout))
						{
							flag2 = setOnInvokeMres.Wait((int)((long)millisecondsTimeout - (long)((ulong)num2)), cancellationToken);
						}
					}
				}
				finally
				{
					if (!this.IsCompleted)
					{
						this.RemoveContinuation(setOnInvokeMres);
					}
				}
			}
			return flag2;
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x000ED8A0 File Offset: 0x000EBAA0
		private bool SpinWait(int millisecondsTimeout)
		{
			if (this.IsCompleted)
			{
				return true;
			}
			if (millisecondsTimeout == 0)
			{
				return false;
			}
			int num = PlatformHelper.IsSingleProcessor ? 1 : 10;
			for (int i = 0; i < num; i++)
			{
				if (this.IsCompleted)
				{
					return true;
				}
				if (i == num / 2)
				{
					Thread.Yield();
				}
				else
				{
					Thread.SpinWait(4 << i);
				}
			}
			return this.IsCompleted;
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x000ED900 File Offset: 0x000EBB00
		[SecuritySafeCritical]
		internal bool InternalCancel(bool bCancelNonExecutingOnly)
		{
			bool flag = false;
			bool flag2 = false;
			TaskSchedulerException ex = null;
			if ((this.m_stateFlags & 65536) != 0)
			{
				TaskScheduler taskScheduler = this.m_taskScheduler;
				try
				{
					flag = (taskScheduler != null && taskScheduler.TryDequeue(this));
				}
				catch (Exception ex2)
				{
					if (!(ex2 is ThreadAbortException))
					{
						ex = new TaskSchedulerException(ex2);
					}
				}
				bool flag3 = (taskScheduler != null && taskScheduler.RequiresAtomicStartTransition) || (this.Options & (TaskCreationOptions)2048) > TaskCreationOptions.None;
				if (!flag && bCancelNonExecutingOnly && flag3)
				{
					flag2 = this.AtomicStateUpdate(4194304, 4325376);
				}
			}
			if (!bCancelNonExecutingOnly || flag || flag2)
			{
				this.RecordInternalCancellationRequest();
				if (flag)
				{
					flag2 = this.AtomicStateUpdate(4194304, 4325376);
				}
				else if (!flag2 && (this.m_stateFlags & 65536) == 0)
				{
					flag2 = this.AtomicStateUpdate(4194304, 23265280);
				}
				if (flag2)
				{
					this.CancellationCleanupLogic();
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			return flag2;
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x000ED9F4 File Offset: 0x000EBBF4
		internal void RecordInternalCancellationRequest()
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
			contingentProperties.m_internalCancellationRequested = 1;
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x000EDA14 File Offset: 0x000EBC14
		internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord)
		{
			this.RecordInternalCancellationRequest();
			if (tokenToRecord != default(CancellationToken))
			{
				this.m_contingentProperties.m_cancellationToken = tokenToRecord;
			}
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x000EDA46 File Offset: 0x000EBC46
		internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord, object cancellationException)
		{
			this.RecordInternalCancellationRequest(tokenToRecord);
			if (cancellationException != null)
			{
				this.AddException(cancellationException, true);
			}
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x000EDA5C File Offset: 0x000EBC5C
		internal void CancellationCleanupLogic()
		{
			Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304);
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties != null)
			{
				contingentProperties.SetCompleted();
				contingentProperties.DeregisterCancellationCallback();
			}
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Canceled);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.RemoveFromActiveTasks(this.Id);
			}
			this.FinishStageThree();
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x000EDAC7 File Offset: 0x000EBCC7
		private void SetCancellationAcknowledged()
		{
			this.m_stateFlags |= 1048576;
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x000EDAE0 File Offset: 0x000EBCE0
		[SecuritySafeCritical]
		internal void FinishContinuations()
		{
			object obj = Interlocked.Exchange(ref this.m_continuationObject, Task.s_taskCompletionSentinel);
			TplEtwProvider.Log.RunningContinuation(this.Id, obj);
			if (obj != null)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, this.Id, CausalitySynchronousWork.CompletionNotification);
				}
				bool flag = (this.m_stateFlags & 134217728) == 0 && Thread.CurrentThread.ThreadState != ThreadState.AbortRequested && (this.m_stateFlags & 64) == 0;
				Action action = obj as Action;
				if (action != null)
				{
					AwaitTaskContinuation.RunOrScheduleAction(action, flag, ref Task.t_currentTask);
					this.LogFinishCompletionNotification();
					return;
				}
				ITaskCompletionAction taskCompletionAction = obj as ITaskCompletionAction;
				if (taskCompletionAction != null)
				{
					if (flag)
					{
						taskCompletionAction.Invoke(this);
					}
					else
					{
						ThreadPool.UnsafeQueueCustomWorkItem(new CompletionActionInvoker(taskCompletionAction, this), false);
					}
					this.LogFinishCompletionNotification();
					return;
				}
				TaskContinuation taskContinuation = obj as TaskContinuation;
				if (taskContinuation != null)
				{
					taskContinuation.Run(this, flag);
					this.LogFinishCompletionNotification();
					return;
				}
				List<object> list = obj as List<object>;
				if (list == null)
				{
					this.LogFinishCompletionNotification();
					return;
				}
				List<object> obj2 = list;
				lock (obj2)
				{
				}
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					StandardTaskContinuation standardTaskContinuation = list[i] as StandardTaskContinuation;
					if (standardTaskContinuation != null && (standardTaskContinuation.m_options & TaskContinuationOptions.ExecuteSynchronously) == TaskContinuationOptions.None)
					{
						TplEtwProvider.Log.RunningContinuationList(this.Id, i, standardTaskContinuation);
						list[i] = null;
						standardTaskContinuation.Run(this, flag);
					}
				}
				for (int j = 0; j < count; j++)
				{
					object obj3 = list[j];
					if (obj3 != null)
					{
						list[j] = null;
						TplEtwProvider.Log.RunningContinuationList(this.Id, j, obj3);
						Action action2 = obj3 as Action;
						if (action2 != null)
						{
							AwaitTaskContinuation.RunOrScheduleAction(action2, flag, ref Task.t_currentTask);
						}
						else
						{
							TaskContinuation taskContinuation2 = obj3 as TaskContinuation;
							if (taskContinuation2 != null)
							{
								taskContinuation2.Run(this, flag);
							}
							else
							{
								ITaskCompletionAction taskCompletionAction2 = (ITaskCompletionAction)obj3;
								if (flag)
								{
									taskCompletionAction2.Invoke(this);
								}
								else
								{
									ThreadPool.UnsafeQueueCustomWorkItem(new CompletionActionInvoker(taskCompletionAction2, this), false);
								}
							}
						}
					}
				}
				this.LogFinishCompletionNotification();
			}
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x000EDD08 File Offset: 0x000EBF08
		private void LogFinishCompletionNotification()
		{
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.CompletionNotification);
			}
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x000EDD18 File Offset: 0x000EBF18
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task> continuationAction)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000EDD40 File Offset: 0x000EBF40
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x000EDD60 File Offset: 0x000EBF60
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, scheduler, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x000EDD84 File Offset: 0x000EBF84
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x000EDDAC File Offset: 0x000EBFAC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, scheduler, cancellationToken, continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x000EDDC8 File Offset: 0x000EBFC8
		private Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task task = new ContinuationTaskFromTask(this, continuationAction, null, creationOptions, internalOptions, ref stackMark);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x000EDE18 File Offset: 0x000EC018
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task, object> continuationAction, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x000EDE40 File Offset: 0x000EC040
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x000EDE60 File Offset: 0x000EC060
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x000EDE84 File Offset: 0x000EC084
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskContinuationOptions continuationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x000EDEAC File Offset: 0x000EC0AC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, scheduler, cancellationToken, continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000EDECC File Offset: 0x000EC0CC
		private Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task task = new ContinuationTaskFromTask(this, continuationAction, state, creationOptions, internalOptions, ref stackMark);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000EDF1C File Offset: 0x000EC11C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x000EDF44 File Offset: 0x000EC144
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x000EDF64 File Offset: 0x000EC164
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, scheduler, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x000EDF88 File Offset: 0x000EC188
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x000EDFB0 File Offset: 0x000EC1B0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x000EDFCC File Offset: 0x000EC1CC
		private Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task<TResult> task = new ContinuationResultTaskFromTask<TResult>(this, continuationFunction, null, creationOptions, internalOptions, ref stackMark);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x000EE01C File Offset: 0x000EC21C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000EE044 File Offset: 0x000EC244
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000EE064 File Offset: 0x000EC264
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x000EE088 File Offset: 0x000EC288
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskContinuationOptions continuationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x000EE0B0 File Offset: 0x000EC2B0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, state, scheduler, cancellationToken, continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000EE0D0 File Offset: 0x000EC2D0
		private Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task<TResult> task = new ContinuationResultTaskFromTask<TResult>(this, continuationFunction, state, creationOptions, internalOptions, ref stackMark);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000EE120 File Offset: 0x000EC320
		internal static void CreationOptionsFromContinuationOptions(TaskContinuationOptions continuationOptions, out TaskCreationOptions creationOptions, out InternalTaskOptions internalOptions)
		{
			TaskContinuationOptions taskContinuationOptions = TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled;
			TaskContinuationOptions taskContinuationOptions2 = TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.RunContinuationsAsynchronously;
			TaskContinuationOptions taskContinuationOptions3 = TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously;
			if ((continuationOptions & taskContinuationOptions3) == taskContinuationOptions3)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("Task_ContinueWith_ESandLR"));
			}
			if ((continuationOptions & ~((taskContinuationOptions2 | taskContinuationOptions | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.ExecuteSynchronously) != TaskContinuationOptions.None)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions");
			}
			if ((continuationOptions & taskContinuationOptions) == taskContinuationOptions)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("Task_ContinueWith_NotOnAnything"));
			}
			creationOptions = (TaskCreationOptions)(continuationOptions & taskContinuationOptions2);
			internalOptions = InternalTaskOptions.ContinuationTask;
			if ((continuationOptions & TaskContinuationOptions.LazyCancellation) != TaskContinuationOptions.None)
			{
				internalOptions |= InternalTaskOptions.LazyCancellation;
			}
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000EE1AC File Offset: 0x000EC3AC
		internal void ContinueWithCore(Task continuationTask, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions options)
		{
			TaskContinuation taskContinuation = new StandardTaskContinuation(continuationTask, options, scheduler);
			if (cancellationToken.CanBeCanceled)
			{
				if (this.IsCompleted || cancellationToken.IsCancellationRequested)
				{
					continuationTask.AssignCancellationToken(cancellationToken, null, null);
				}
				else
				{
					continuationTask.AssignCancellationToken(cancellationToken, this, taskContinuation);
				}
			}
			if (!continuationTask.IsCompleted)
			{
				if ((this.Options & (TaskCreationOptions)1024) != TaskCreationOptions.None && !(this is ITaskCompletionAction))
				{
					TplEtwProvider log = TplEtwProvider.Log;
					if (log.IsEnabled())
					{
						log.AwaitTaskContinuationScheduled(TaskScheduler.Current.Id, Task.CurrentId ?? 0, continuationTask.Id);
					}
				}
				if (!this.AddTaskContinuation(taskContinuation, false))
				{
					taskContinuation.Run(this, true);
				}
			}
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000EE260 File Offset: 0x000EC460
		internal void AddCompletionAction(ITaskCompletionAction action)
		{
			this.AddCompletionAction(action, false);
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000EE26A File Offset: 0x000EC46A
		private void AddCompletionAction(ITaskCompletionAction action, bool addBeforeOthers)
		{
			if (!this.AddTaskContinuation(action, addBeforeOthers))
			{
				action.Invoke(this);
			}
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x000EE280 File Offset: 0x000EC480
		private bool AddTaskContinuationComplex(object tc, bool addBeforeOthers)
		{
			object continuationObject = this.m_continuationObject;
			if (continuationObject != Task.s_taskCompletionSentinel && !(continuationObject is List<object>))
			{
				Interlocked.CompareExchange(ref this.m_continuationObject, new List<object>
				{
					continuationObject
				}, continuationObject);
			}
			List<object> list = this.m_continuationObject as List<object>;
			if (list != null)
			{
				List<object> obj = list;
				lock (obj)
				{
					if (this.m_continuationObject != Task.s_taskCompletionSentinel)
					{
						if (list.Count == list.Capacity)
						{
							list.RemoveAll(Task.s_IsTaskContinuationNullPredicate);
						}
						if (addBeforeOthers)
						{
							list.Insert(0, tc);
						}
						else
						{
							list.Add(tc);
						}
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000EE344 File Offset: 0x000EC544
		private bool AddTaskContinuation(object tc, bool addBeforeOthers)
		{
			return !this.IsCompleted && ((this.m_continuationObject == null && Interlocked.CompareExchange(ref this.m_continuationObject, tc, null) == null) || this.AddTaskContinuationComplex(tc, addBeforeOthers));
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000EE374 File Offset: 0x000EC574
		internal void RemoveContinuation(object continuationObject)
		{
			object continuationObject2 = this.m_continuationObject;
			if (continuationObject2 == Task.s_taskCompletionSentinel)
			{
				return;
			}
			List<object> list = continuationObject2 as List<object>;
			if (list == null)
			{
				if (Interlocked.CompareExchange(ref this.m_continuationObject, new List<object>(), continuationObject) == continuationObject)
				{
					return;
				}
				list = (this.m_continuationObject as List<object>);
			}
			if (list != null)
			{
				List<object> obj = list;
				lock (obj)
				{
					if (this.m_continuationObject != Task.s_taskCompletionSentinel)
					{
						int num = list.IndexOf(continuationObject);
						if (num != -1)
						{
							list[num] = null;
						}
					}
				}
			}
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000EE418 File Offset: 0x000EC618
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static void WaitAll(params Task[] tasks)
		{
			Task.WaitAll(tasks, -1);
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x000EE424 File Offset: 0x000EC624
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static bool WaitAll(Task[] tasks, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return Task.WaitAll(tasks, (int)num);
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x000EE45C File Offset: 0x000EC65C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static bool WaitAll(Task[] tasks, int millisecondsTimeout)
		{
			return Task.WaitAll(tasks, millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x000EE479 File Offset: 0x000EC679
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static void WaitAll(Task[] tasks, CancellationToken cancellationToken)
		{
			Task.WaitAll(tasks, -1, cancellationToken);
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x000EE484 File Offset: 0x000EC684
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static bool WaitAll(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			cancellationToken.ThrowIfCancellationRequested();
			List<Exception> innerExceptions = null;
			List<Task> list = null;
			List<Task> list2 = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			for (int i = tasks.Length - 1; i >= 0; i--)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_WaitMulti_NullTask"), "tasks");
				}
				bool flag4 = task.IsCompleted;
				if (!flag4)
				{
					if (millisecondsTimeout != -1 || cancellationToken.CanBeCanceled)
					{
						Task.AddToList<Task>(task, ref list, tasks.Length);
					}
					else
					{
						flag4 = (task.WrappedTryRunInline() && task.IsCompleted);
						if (!flag4)
						{
							Task.AddToList<Task>(task, ref list, tasks.Length);
						}
					}
				}
				if (flag4)
				{
					if (task.IsFaulted)
					{
						flag = true;
					}
					else if (task.IsCanceled)
					{
						flag2 = true;
					}
					if (task.IsWaitNotificationEnabled)
					{
						Task.AddToList<Task>(task, ref list2, 1);
					}
				}
			}
			if (list != null)
			{
				flag3 = Task.WaitAllBlockingCore(list, millisecondsTimeout, cancellationToken);
				if (flag3)
				{
					foreach (Task task2 in list)
					{
						if (task2.IsFaulted)
						{
							flag = true;
						}
						else if (task2.IsCanceled)
						{
							flag2 = true;
						}
						if (task2.IsWaitNotificationEnabled)
						{
							Task.AddToList<Task>(task2, ref list2, 1);
						}
					}
				}
				GC.KeepAlive(tasks);
			}
			if (flag3 && list2 != null)
			{
				foreach (Task task3 in list2)
				{
					if (task3.NotifyDebuggerOfWaitCompletionIfNecessary())
					{
						break;
					}
				}
			}
			if (flag3 && (flag || flag2))
			{
				if (!flag)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				foreach (Task t in tasks)
				{
					Task.AddExceptionsForCompletedTask(ref innerExceptions, t);
				}
				throw new AggregateException(innerExceptions);
			}
			return flag3;
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x000EE688 File Offset: 0x000EC888
		private static void AddToList<T>(T item, ref List<T> list, int initSize)
		{
			if (list == null)
			{
				list = new List<T>(initSize);
			}
			list.Add(item);
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x000EE6A0 File Offset: 0x000EC8A0
		private static bool WaitAllBlockingCore(List<Task> tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			bool flag = false;
			Task.SetOnCountdownMres setOnCountdownMres = new Task.SetOnCountdownMres(tasks.Count);
			try
			{
				foreach (Task task in tasks)
				{
					task.AddCompletionAction(setOnCountdownMres, true);
				}
				flag = setOnCountdownMres.Wait(millisecondsTimeout, cancellationToken);
			}
			finally
			{
				if (!flag)
				{
					foreach (Task task2 in tasks)
					{
						if (!task2.IsCompleted)
						{
							task2.RemoveContinuation(setOnCountdownMres);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x000EE764 File Offset: 0x000EC964
		internal static void FastWaitAll(Task[] tasks)
		{
			List<Exception> list = null;
			for (int i = tasks.Length - 1; i >= 0; i--)
			{
				if (!tasks[i].IsCompleted)
				{
					tasks[i].WrappedTryRunInline();
				}
			}
			for (int j = tasks.Length - 1; j >= 0; j--)
			{
				Task task = tasks[j];
				task.SpinThenBlockingWait(-1, default(CancellationToken));
				Task.AddExceptionsForCompletedTask(ref list, task);
			}
			if (list != null)
			{
				throw new AggregateException(list);
			}
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x000EE7D0 File Offset: 0x000EC9D0
		internal static void AddExceptionsForCompletedTask(ref List<Exception> exceptions, Task t)
		{
			AggregateException exceptions2 = t.GetExceptions(true);
			if (exceptions2 != null)
			{
				t.UpdateExceptionObservedStatus();
				if (exceptions == null)
				{
					exceptions = new List<Exception>(exceptions2.InnerExceptions.Count);
				}
				exceptions.AddRange(exceptions2.InnerExceptions);
			}
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x000EE814 File Offset: 0x000ECA14
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(params Task[] tasks)
		{
			return Task.WaitAny(tasks, -1);
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x000EE82C File Offset: 0x000ECA2C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return Task.WaitAny(tasks, (int)num);
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x000EE863 File Offset: 0x000ECA63
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, CancellationToken cancellationToken)
		{
			return Task.WaitAny(tasks, -1, cancellationToken);
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x000EE870 File Offset: 0x000ECA70
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, int millisecondsTimeout)
		{
			return Task.WaitAny(tasks, millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x000EE890 File Offset: 0x000ECA90
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			cancellationToken.ThrowIfCancellationRequested();
			int num = -1;
			for (int i = 0; i < tasks.Length; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_WaitMulti_NullTask"), "tasks");
				}
				if (num == -1 && task.IsCompleted)
				{
					num = i;
				}
			}
			if (num == -1 && tasks.Length != 0)
			{
				Task<Task> task2 = TaskFactory.CommonCWAnyLogic(tasks);
				bool flag = task2.Wait(millisecondsTimeout, cancellationToken);
				if (flag)
				{
					num = Array.IndexOf<Task>(tasks, task2.Result);
				}
			}
			GC.KeepAlive(tasks);
			return num;
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x000EE92C File Offset: 0x000ECB2C
		[__DynamicallyInvokable]
		public static Task<TResult> FromResult<TResult>(TResult result)
		{
			return new Task<TResult>(result);
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x000EE934 File Offset: 0x000ECB34
		[__DynamicallyInvokable]
		public static Task FromException(Exception exception)
		{
			return Task.FromException<VoidTaskResult>(exception);
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x000EE93C File Offset: 0x000ECB3C
		[__DynamicallyInvokable]
		public static Task<TResult> FromException<TResult>(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Task<TResult> task = new Task<TResult>();
			bool flag = task.TrySetException(exception);
			return task;
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x000EE966 File Offset: 0x000ECB66
		[FriendAccessAllowed]
		internal static Task FromCancellation(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				throw new ArgumentOutOfRangeException("cancellationToken");
			}
			return new Task(true, TaskCreationOptions.None, cancellationToken);
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x000EE984 File Offset: 0x000ECB84
		[__DynamicallyInvokable]
		public static Task FromCanceled(CancellationToken cancellationToken)
		{
			return Task.FromCancellation(cancellationToken);
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x000EE98C File Offset: 0x000ECB8C
		[FriendAccessAllowed]
		internal static Task<TResult> FromCancellation<TResult>(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				throw new ArgumentOutOfRangeException("cancellationToken");
			}
			return new Task<TResult>(true, default(TResult), TaskCreationOptions.None, cancellationToken);
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x000EE9BE File Offset: 0x000ECBBE
		[__DynamicallyInvokable]
		public static Task<TResult> FromCanceled<TResult>(CancellationToken cancellationToken)
		{
			return Task.FromCancellation<TResult>(cancellationToken);
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x000EE9C8 File Offset: 0x000ECBC8
		internal static Task<TResult> FromCancellation<TResult>(OperationCanceledException exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Task<TResult> task = new Task<TResult>();
			bool flag = task.TrySetCanceled(exception.CancellationToken, exception);
			return task;
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x000EE9F8 File Offset: 0x000ECBF8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Task Run(Action action)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(null, action, null, default(CancellationToken), TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, ref stackCrawlMark);
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x000EEA24 File Offset: 0x000ECC24
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Task Run(Action action, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(null, action, null, cancellationToken, TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, ref stackCrawlMark);
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x000EEA48 File Offset: 0x000ECC48
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Task<TResult> Run<TResult>(Func<TResult> function)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(null, function, default(CancellationToken), TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default, ref stackCrawlMark);
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x000EEA70 File Offset: 0x000ECC70
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Task<TResult> Run<TResult>(Func<TResult> function, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(null, function, cancellationToken, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default, ref stackCrawlMark);
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x000EEA90 File Offset: 0x000ECC90
		[__DynamicallyInvokable]
		public static Task Run(Func<Task> function)
		{
			return Task.Run(function, default(CancellationToken));
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x000EEAAC File Offset: 0x000ECCAC
		[__DynamicallyInvokable]
		public static Task Run(Func<Task> function, CancellationToken cancellationToken)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
			{
				cancellationToken.ThrowIfSourceDisposed();
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			Task<Task> outerTask = Task<Task>.Factory.StartNew(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			return new UnwrapPromise<VoidTaskResult>(outerTask, true);
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x000EEB04 File Offset: 0x000ECD04
		[__DynamicallyInvokable]
		public static Task<TResult> Run<TResult>(Func<Task<TResult>> function)
		{
			return Task.Run<TResult>(function, default(CancellationToken));
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x000EEB20 File Offset: 0x000ECD20
		[__DynamicallyInvokable]
		public static Task<TResult> Run<TResult>(Func<Task<TResult>> function, CancellationToken cancellationToken)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
			{
				cancellationToken.ThrowIfSourceDisposed();
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<TResult>(cancellationToken);
			}
			Task<Task<TResult>> outerTask = Task<Task<TResult>>.Factory.StartNew(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			return new UnwrapPromise<TResult>(outerTask, true);
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x000EEB78 File Offset: 0x000ECD78
		[__DynamicallyInvokable]
		public static Task Delay(TimeSpan delay)
		{
			return Task.Delay(delay, default(CancellationToken));
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x000EEB94 File Offset: 0x000ECD94
		[__DynamicallyInvokable]
		public static Task Delay(TimeSpan delay, CancellationToken cancellationToken)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay", Environment.GetResourceString("Task_Delay_InvalidDelay"));
			}
			return Task.Delay((int)num, cancellationToken);
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x000EEBD8 File Offset: 0x000ECDD8
		[__DynamicallyInvokable]
		public static Task Delay(int millisecondsDelay)
		{
			return Task.Delay(millisecondsDelay, default(CancellationToken));
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x000EEBF4 File Offset: 0x000ECDF4
		[__DynamicallyInvokable]
		public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken)
		{
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay", Environment.GetResourceString("Task_Delay_InvalidMillisecondsDelay"));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			if (millisecondsDelay == 0)
			{
				return Task.CompletedTask;
			}
			Task.DelayPromise delayPromise = new Task.DelayPromise(cancellationToken);
			if (cancellationToken.CanBeCanceled)
			{
				delayPromise.Registration = cancellationToken.InternalRegisterWithoutEC(delegate(object state)
				{
					((Task.DelayPromise)state).Complete();
				}, delayPromise);
			}
			if (millisecondsDelay != -1)
			{
				delayPromise.Timer = new Timer(delegate(object state)
				{
					((Task.DelayPromise)state).Complete();
				}, delayPromise, millisecondsDelay, -1);
				delayPromise.Timer.KeepRootedWhileScheduled();
			}
			return delayPromise;
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x000EECB0 File Offset: 0x000ECEB0
		[__DynamicallyInvokable]
		public static Task WhenAll(IEnumerable<Task> tasks)
		{
			Task[] array = tasks as Task[];
			if (array != null)
			{
				return Task.WhenAll(array);
			}
			ICollection<Task> collection = tasks as ICollection<Task>;
			if (collection != null)
			{
				int num = 0;
				array = new Task[collection.Count];
				foreach (Task task in tasks)
				{
					if (task == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
					}
					array[num++] = task;
				}
				return Task.InternalWhenAll(array);
			}
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			List<Task> list = new List<Task>();
			foreach (Task task2 in tasks)
			{
				if (task2 == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
				}
				list.Add(task2);
			}
			return Task.InternalWhenAll(list.ToArray());
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x000EEDC0 File Offset: 0x000ECFC0
		[__DynamicallyInvokable]
		public static Task WhenAll(params Task[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			int num = tasks.Length;
			if (num == 0)
			{
				return Task.InternalWhenAll(tasks);
			}
			Task[] array = new Task[num];
			for (int i = 0; i < num; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
				}
				array[i] = task;
			}
			return Task.InternalWhenAll(array);
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x000EEE22 File Offset: 0x000ED022
		private static Task InternalWhenAll(Task[] tasks)
		{
			if (tasks.Length != 0)
			{
				return new Task.WhenAllPromise(tasks);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x000EEE34 File Offset: 0x000ED034
		[__DynamicallyInvokable]
		public static Task<TResult[]> WhenAll<TResult>(IEnumerable<Task<TResult>> tasks)
		{
			Task<TResult>[] array = tasks as Task<TResult>[];
			if (array != null)
			{
				return Task.WhenAll<TResult>(array);
			}
			ICollection<Task<TResult>> collection = tasks as ICollection<Task<TResult>>;
			if (collection != null)
			{
				int num = 0;
				array = new Task<TResult>[collection.Count];
				foreach (Task<TResult> task in tasks)
				{
					if (task == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
					}
					array[num++] = task;
				}
				return Task.InternalWhenAll<TResult>(array);
			}
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			List<Task<TResult>> list = new List<Task<TResult>>();
			foreach (Task<TResult> task2 in tasks)
			{
				if (task2 == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
				}
				list.Add(task2);
			}
			return Task.InternalWhenAll<TResult>(list.ToArray());
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x000EEF44 File Offset: 0x000ED144
		[__DynamicallyInvokable]
		public static Task<TResult[]> WhenAll<TResult>(params Task<TResult>[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			int num = tasks.Length;
			if (num == 0)
			{
				return Task.InternalWhenAll<TResult>(tasks);
			}
			Task<TResult>[] array = new Task<TResult>[num];
			for (int i = 0; i < num; i++)
			{
				Task<TResult> task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
				}
				array[i] = task;
			}
			return Task.InternalWhenAll<TResult>(array);
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x000EEFA8 File Offset: 0x000ED1A8
		private static Task<TResult[]> InternalWhenAll<TResult>(Task<TResult>[] tasks)
		{
			if (tasks.Length != 0)
			{
				return new Task.WhenAllPromise<TResult>(tasks);
			}
			return new Task<TResult[]>(false, new TResult[0], TaskCreationOptions.None, default(CancellationToken));
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x000EEFD8 File Offset: 0x000ED1D8
		[__DynamicallyInvokable]
		public static Task<Task> WhenAny(params Task[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
			}
			int num = tasks.Length;
			Task[] array = new Task[num];
			for (int i = 0; i < num; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
				}
				array[i] = task;
			}
			return TaskFactory.CommonCWAnyLogic(array);
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x000EF04C File Offset: 0x000ED24C
		[__DynamicallyInvokable]
		public static Task<Task> WhenAny(IEnumerable<Task> tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			List<Task> list = new List<Task>();
			foreach (Task task in tasks)
			{
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
				}
				list.Add(task);
			}
			if (list.Count == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
			}
			return TaskFactory.CommonCWAnyLogic(list);
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x000EF0E4 File Offset: 0x000ED2E4
		[__DynamicallyInvokable]
		public static Task<Task<TResult>> WhenAny<TResult>(params Task<TResult>[] tasks)
		{
			Task<Task> task = Task.WhenAny(tasks);
			return task.ContinueWith<Task<TResult>>(Task<TResult>.TaskWhenAnyCast, default(CancellationToken), TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x000EF118 File Offset: 0x000ED318
		[__DynamicallyInvokable]
		public static Task<Task<TResult>> WhenAny<TResult>(IEnumerable<Task<TResult>> tasks)
		{
			Task<Task> task = Task.WhenAny(tasks);
			return task.ContinueWith<Task<TResult>>(Task<TResult>.TaskWhenAnyCast, default(CancellationToken), TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x000EF14A File Offset: 0x000ED34A
		[FriendAccessAllowed]
		internal static Task<TResult> CreateUnwrapPromise<TResult>(Task outerTask, bool lookForOce)
		{
			return new UnwrapPromise<TResult>(outerTask, lookForOce);
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x000EF153 File Offset: 0x000ED353
		internal virtual Delegate[] GetDelegateContinuationsForDebugger()
		{
			if (this.m_continuationObject != this)
			{
				return Task.GetDelegatesFromContinuationObject(this.m_continuationObject);
			}
			return null;
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x000EF170 File Offset: 0x000ED370
		internal static Delegate[] GetDelegatesFromContinuationObject(object continuationObject)
		{
			if (continuationObject != null)
			{
				Action action = continuationObject as Action;
				if (action != null)
				{
					return new Delegate[]
					{
						AsyncMethodBuilderCore.TryGetStateMachineForDebugger(action)
					};
				}
				TaskContinuation taskContinuation = continuationObject as TaskContinuation;
				if (taskContinuation != null)
				{
					return taskContinuation.GetDelegateContinuationsForDebugger();
				}
				Task task = continuationObject as Task;
				if (task != null)
				{
					Delegate[] delegateContinuationsForDebugger = task.GetDelegateContinuationsForDebugger();
					if (delegateContinuationsForDebugger != null)
					{
						return delegateContinuationsForDebugger;
					}
				}
				ITaskCompletionAction taskCompletionAction = continuationObject as ITaskCompletionAction;
				if (taskCompletionAction != null)
				{
					return new Delegate[]
					{
						new Action<Task>(taskCompletionAction.Invoke)
					};
				}
				List<object> list = continuationObject as List<object>;
				if (list != null)
				{
					List<Delegate> list2 = new List<Delegate>();
					foreach (object continuationObject2 in list)
					{
						Delegate[] delegatesFromContinuationObject = Task.GetDelegatesFromContinuationObject(continuationObject2);
						if (delegatesFromContinuationObject != null)
						{
							foreach (Delegate @delegate in delegatesFromContinuationObject)
							{
								if (@delegate != null)
								{
									list2.Add(@delegate);
								}
							}
						}
					}
					return list2.ToArray();
				}
			}
			return null;
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x000EF27C File Offset: 0x000ED47C
		private static Task GetActiveTaskFromId(int taskId)
		{
			Task result = null;
			Task.s_currentActiveTasks.TryGetValue(taskId, out result);
			return result;
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x000EF29A File Offset: 0x000ED49A
		private static Task[] GetActiveTasks()
		{
			return new List<Task>(Task.s_currentActiveTasks.Values).ToArray();
		}

		// Token: 0x04001A3C RID: 6716
		[ThreadStatic]
		internal static Task t_currentTask;

		// Token: 0x04001A3D RID: 6717
		[ThreadStatic]
		private static StackGuard t_stackGuard;

		// Token: 0x04001A3E RID: 6718
		internal static int s_taskIdCounter;

		// Token: 0x04001A3F RID: 6719
		private static readonly TaskFactory s_factory = new TaskFactory();

		// Token: 0x04001A40 RID: 6720
		private volatile int m_taskId;

		// Token: 0x04001A41 RID: 6721
		internal object m_action;

		// Token: 0x04001A42 RID: 6722
		internal object m_stateObject;

		// Token: 0x04001A43 RID: 6723
		internal TaskScheduler m_taskScheduler;

		// Token: 0x04001A44 RID: 6724
		internal readonly Task m_parent;

		// Token: 0x04001A45 RID: 6725
		internal volatile int m_stateFlags;

		// Token: 0x04001A46 RID: 6726
		private const int OptionsMask = 65535;

		// Token: 0x04001A47 RID: 6727
		internal const int TASK_STATE_STARTED = 65536;

		// Token: 0x04001A48 RID: 6728
		internal const int TASK_STATE_DELEGATE_INVOKED = 131072;

		// Token: 0x04001A49 RID: 6729
		internal const int TASK_STATE_DISPOSED = 262144;

		// Token: 0x04001A4A RID: 6730
		internal const int TASK_STATE_EXCEPTIONOBSERVEDBYPARENT = 524288;

		// Token: 0x04001A4B RID: 6731
		internal const int TASK_STATE_CANCELLATIONACKNOWLEDGED = 1048576;

		// Token: 0x04001A4C RID: 6732
		internal const int TASK_STATE_FAULTED = 2097152;

		// Token: 0x04001A4D RID: 6733
		internal const int TASK_STATE_CANCELED = 4194304;

		// Token: 0x04001A4E RID: 6734
		internal const int TASK_STATE_WAITING_ON_CHILDREN = 8388608;

		// Token: 0x04001A4F RID: 6735
		internal const int TASK_STATE_RAN_TO_COMPLETION = 16777216;

		// Token: 0x04001A50 RID: 6736
		internal const int TASK_STATE_WAITINGFORACTIVATION = 33554432;

		// Token: 0x04001A51 RID: 6737
		internal const int TASK_STATE_COMPLETION_RESERVED = 67108864;

		// Token: 0x04001A52 RID: 6738
		internal const int TASK_STATE_THREAD_WAS_ABORTED = 134217728;

		// Token: 0x04001A53 RID: 6739
		internal const int TASK_STATE_WAIT_COMPLETION_NOTIFICATION = 268435456;

		// Token: 0x04001A54 RID: 6740
		internal const int TASK_STATE_EXECUTIONCONTEXT_IS_NULL = 536870912;

		// Token: 0x04001A55 RID: 6741
		internal const int TASK_STATE_TASKSCHEDULED_WAS_FIRED = 1073741824;

		// Token: 0x04001A56 RID: 6742
		private const int TASK_STATE_COMPLETED_MASK = 23068672;

		// Token: 0x04001A57 RID: 6743
		private const int CANCELLATION_REQUESTED = 1;

		// Token: 0x04001A58 RID: 6744
		private volatile object m_continuationObject;

		// Token: 0x04001A59 RID: 6745
		private static readonly object s_taskCompletionSentinel = new object();

		// Token: 0x04001A5A RID: 6746
		[FriendAccessAllowed]
		internal static bool s_asyncDebuggingEnabled;

		// Token: 0x04001A5B RID: 6747
		private static readonly Dictionary<int, Task> s_currentActiveTasks = new Dictionary<int, Task>();

		// Token: 0x04001A5C RID: 6748
		private static readonly object s_activeTasksLock = new object();

		// Token: 0x04001A5D RID: 6749
		internal volatile Task.ContingentProperties m_contingentProperties;

		// Token: 0x04001A5E RID: 6750
		private static readonly Action<object> s_taskCancelCallback = new Action<object>(Task.TaskCancelCallback);

		// Token: 0x04001A5F RID: 6751
		private static readonly Func<Task.ContingentProperties> s_createContingentProperties = () => new Task.ContingentProperties();

		// Token: 0x04001A60 RID: 6752
		private static Task s_completedTask;

		// Token: 0x04001A61 RID: 6753
		private static readonly Predicate<Task> s_IsExceptionObservedByParentPredicate = (Task t) => t.IsExceptionObservedByParent;

		// Token: 0x04001A62 RID: 6754
		[SecurityCritical]
		private static ContextCallback s_ecCallback;

		// Token: 0x04001A63 RID: 6755
		private static readonly Predicate<object> s_IsTaskContinuationNullPredicate = (object tc) => tc == null;

		// Token: 0x02000BDE RID: 3038
		internal class ContingentProperties
		{
			// Token: 0x06006EA8 RID: 28328 RVA: 0x0017D034 File Offset: 0x0017B234
			internal void SetCompleted()
			{
				ManualResetEventSlim completionEvent = this.m_completionEvent;
				if (completionEvent != null)
				{
					completionEvent.Set();
				}
			}

			// Token: 0x06006EA9 RID: 28329 RVA: 0x0017D054 File Offset: 0x0017B254
			internal void DeregisterCancellationCallback()
			{
				if (this.m_cancellationRegistration != null)
				{
					try
					{
						this.m_cancellationRegistration.Value.Dispose();
					}
					catch (ObjectDisposedException)
					{
					}
					this.m_cancellationRegistration = null;
				}
			}

			// Token: 0x040035CA RID: 13770
			internal ExecutionContext m_capturedContext;

			// Token: 0x040035CB RID: 13771
			internal volatile ManualResetEventSlim m_completionEvent;

			// Token: 0x040035CC RID: 13772
			internal volatile TaskExceptionHolder m_exceptionsHolder;

			// Token: 0x040035CD RID: 13773
			internal CancellationToken m_cancellationToken;

			// Token: 0x040035CE RID: 13774
			internal Shared<CancellationTokenRegistration> m_cancellationRegistration;

			// Token: 0x040035CF RID: 13775
			internal volatile int m_internalCancellationRequested;

			// Token: 0x040035D0 RID: 13776
			internal volatile int m_completionCountdown = 1;

			// Token: 0x040035D1 RID: 13777
			internal volatile List<Task> m_exceptionalChildren;
		}

		// Token: 0x02000BDF RID: 3039
		private sealed class SetOnInvokeMres : ManualResetEventSlim, ITaskCompletionAction
		{
			// Token: 0x06006EAB RID: 28331 RVA: 0x0017D0A9 File Offset: 0x0017B2A9
			internal SetOnInvokeMres() : base(false, 0)
			{
			}

			// Token: 0x06006EAC RID: 28332 RVA: 0x0017D0B3 File Offset: 0x0017B2B3
			public void Invoke(Task completingTask)
			{
				base.Set();
			}
		}

		// Token: 0x02000BE0 RID: 3040
		private sealed class SetOnCountdownMres : ManualResetEventSlim, ITaskCompletionAction
		{
			// Token: 0x06006EAD RID: 28333 RVA: 0x0017D0BB File Offset: 0x0017B2BB
			internal SetOnCountdownMres(int count)
			{
				this._count = count;
			}

			// Token: 0x06006EAE RID: 28334 RVA: 0x0017D0CA File Offset: 0x0017B2CA
			public void Invoke(Task completingTask)
			{
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					base.Set();
				}
			}

			// Token: 0x040035D2 RID: 13778
			private int _count;
		}

		// Token: 0x02000BE1 RID: 3041
		private sealed class DelayPromise : Task<VoidTaskResult>
		{
			// Token: 0x06006EAF RID: 28335 RVA: 0x0017D0DF File Offset: 0x0017B2DF
			internal DelayPromise(CancellationToken token)
			{
				this.Token = token;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "Task.Delay", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06006EB0 RID: 28336 RVA: 0x0017D118 File Offset: 0x0017B318
			internal void Complete()
			{
				bool flag;
				if (this.Token.IsCancellationRequested)
				{
					flag = base.TrySetCanceled(this.Token);
				}
				else
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					flag = base.TrySetResult(default(VoidTaskResult));
				}
				if (flag)
				{
					if (this.Timer != null)
					{
						this.Timer.Dispose();
					}
					this.Registration.Dispose();
				}
			}

			// Token: 0x040035D3 RID: 13779
			internal readonly CancellationToken Token;

			// Token: 0x040035D4 RID: 13780
			internal CancellationTokenRegistration Registration;

			// Token: 0x040035D5 RID: 13781
			internal Timer Timer;
		}

		// Token: 0x02000BE2 RID: 3042
		private sealed class WhenAllPromise : Task<VoidTaskResult>, ITaskCompletionAction
		{
			// Token: 0x06006EB1 RID: 28337 RVA: 0x0017D19C File Offset: 0x0017B39C
			internal WhenAllPromise(Task[] tasks)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "Task.WhenAll", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
				this.m_tasks = tasks;
				this.m_count = tasks.Length;
				foreach (Task task in tasks)
				{
					if (task.IsCompleted)
					{
						this.Invoke(task);
					}
					else
					{
						task.AddCompletionAction(this);
					}
				}
			}

			// Token: 0x06006EB2 RID: 28338 RVA: 0x0017D214 File Offset: 0x0017B414
			public void Invoke(Task completedTask)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
				}
				if (Interlocked.Decrement(ref this.m_count) == 0)
				{
					List<ExceptionDispatchInfo> list = null;
					Task task = null;
					for (int i = 0; i < this.m_tasks.Length; i++)
					{
						Task task2 = this.m_tasks[i];
						if (task2.IsFaulted)
						{
							if (list == null)
							{
								list = new List<ExceptionDispatchInfo>();
							}
							list.AddRange(task2.GetExceptionDispatchInfos());
						}
						else if (task2.IsCanceled && task == null)
						{
							task = task2;
						}
						if (task2.IsWaitNotificationEnabled)
						{
							base.SetNotificationForWaitCompletion(true);
						}
						else
						{
							this.m_tasks[i] = null;
						}
					}
					if (list != null)
					{
						base.TrySetException(list);
						return;
					}
					if (task != null)
					{
						base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
						return;
					}
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					base.TrySetResult(default(VoidTaskResult));
				}
			}

			// Token: 0x1700130D RID: 4877
			// (get) Token: 0x06006EB3 RID: 28339 RVA: 0x0017D305 File Offset: 0x0017B505
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					return base.ShouldNotifyDebuggerOfWaitCompletion && Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this.m_tasks);
				}
			}

			// Token: 0x040035D6 RID: 13782
			private readonly Task[] m_tasks;

			// Token: 0x040035D7 RID: 13783
			private int m_count;
		}

		// Token: 0x02000BE3 RID: 3043
		private sealed class WhenAllPromise<T> : Task<T[]>, ITaskCompletionAction
		{
			// Token: 0x06006EB4 RID: 28340 RVA: 0x0017D31C File Offset: 0x0017B51C
			internal WhenAllPromise(Task<T>[] tasks)
			{
				this.m_tasks = tasks;
				this.m_count = tasks.Length;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "Task.WhenAll", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
				foreach (Task<T> task in tasks)
				{
					if (task.IsCompleted)
					{
						this.Invoke(task);
					}
					else
					{
						task.AddCompletionAction(this);
					}
				}
			}

			// Token: 0x06006EB5 RID: 28341 RVA: 0x0017D394 File Offset: 0x0017B594
			public void Invoke(Task ignored)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
				}
				if (Interlocked.Decrement(ref this.m_count) == 0)
				{
					T[] array = new T[this.m_tasks.Length];
					List<ExceptionDispatchInfo> list = null;
					Task task = null;
					for (int i = 0; i < this.m_tasks.Length; i++)
					{
						Task<T> task2 = this.m_tasks[i];
						if (task2.IsFaulted)
						{
							if (list == null)
							{
								list = new List<ExceptionDispatchInfo>();
							}
							list.AddRange(task2.GetExceptionDispatchInfos());
						}
						else if (task2.IsCanceled)
						{
							if (task == null)
							{
								task = task2;
							}
						}
						else
						{
							array[i] = task2.GetResultCore(false);
						}
						if (task2.IsWaitNotificationEnabled)
						{
							base.SetNotificationForWaitCompletion(true);
						}
						else
						{
							this.m_tasks[i] = null;
						}
					}
					if (list != null)
					{
						base.TrySetException(list);
						return;
					}
					if (task != null)
					{
						base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
						return;
					}
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					base.TrySetResult(array);
				}
			}

			// Token: 0x1700130E RID: 4878
			// (get) Token: 0x06006EB6 RID: 28342 RVA: 0x0017D4A1 File Offset: 0x0017B6A1
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					return base.ShouldNotifyDebuggerOfWaitCompletion && Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this.m_tasks);
				}
			}

			// Token: 0x040035D8 RID: 13784
			private readonly Task<T>[] m_tasks;

			// Token: 0x040035D9 RID: 13785
			private int m_count;
		}
	}
}
