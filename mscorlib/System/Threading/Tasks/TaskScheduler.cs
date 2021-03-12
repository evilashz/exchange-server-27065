using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	// Token: 0x0200054A RID: 1354
	[DebuggerDisplay("Id={Id}")]
	[DebuggerTypeProxy(typeof(TaskScheduler.SystemThreadingTasks_TaskSchedulerDebugView))]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract class TaskScheduler
	{
		// Token: 0x060040E2 RID: 16610
		[SecurityCritical]
		[__DynamicallyInvokable]
		protected internal abstract void QueueTask(Task task);

		// Token: 0x060040E3 RID: 16611
		[SecurityCritical]
		[__DynamicallyInvokable]
		protected abstract bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued);

		// Token: 0x060040E4 RID: 16612
		[SecurityCritical]
		[__DynamicallyInvokable]
		protected abstract IEnumerable<Task> GetScheduledTasks();

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060040E5 RID: 16613 RVA: 0x000F170E File Offset: 0x000EF90E
		[__DynamicallyInvokable]
		public virtual int MaximumConcurrencyLevel
		{
			[__DynamicallyInvokable]
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x000F1718 File Offset: 0x000EF918
		[SecuritySafeCritical]
		internal bool TryRunInline(Task task, bool taskWasPreviouslyQueued)
		{
			TaskScheduler executingTaskScheduler = task.ExecutingTaskScheduler;
			if (executingTaskScheduler != this && executingTaskScheduler != null)
			{
				return executingTaskScheduler.TryRunInline(task, taskWasPreviouslyQueued);
			}
			StackGuard currentStackGuard;
			if (executingTaskScheduler == null || task.m_action == null || task.IsDelegateInvoked || task.IsCanceled || !(currentStackGuard = Task.CurrentStackGuard).TryBeginInliningScope())
			{
				return false;
			}
			bool flag = false;
			try
			{
				task.FireTaskScheduledIfNeeded(this);
				flag = this.TryExecuteTaskInline(task, taskWasPreviouslyQueued);
			}
			finally
			{
				currentStackGuard.EndInliningScope();
			}
			if (flag && !task.IsDelegateInvoked && !task.IsCanceled)
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskScheduler_InconsistentStateAfterTryExecuteTaskInline"));
			}
			return flag;
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x000F17B8 File Offset: 0x000EF9B8
		[SecurityCritical]
		[__DynamicallyInvokable]
		protected internal virtual bool TryDequeue(Task task)
		{
			return false;
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x000F17BB File Offset: 0x000EF9BB
		internal virtual void NotifyWorkItemProgress()
		{
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060040E9 RID: 16617 RVA: 0x000F17BD File Offset: 0x000EF9BD
		internal virtual bool RequiresAtomicStartTransition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x000F17C0 File Offset: 0x000EF9C0
		[SecurityCritical]
		internal void InternalQueueTask(Task task)
		{
			task.FireTaskScheduledIfNeeded(this);
			this.QueueTask(task);
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x000F17D1 File Offset: 0x000EF9D1
		[__DynamicallyInvokable]
		protected TaskScheduler()
		{
			if (Debugger.IsAttached)
			{
				this.AddToActiveTaskSchedulers();
			}
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x000F17E8 File Offset: 0x000EF9E8
		private void AddToActiveTaskSchedulers()
		{
			ConditionalWeakTable<TaskScheduler, object> conditionalWeakTable = TaskScheduler.s_activeTaskSchedulers;
			if (conditionalWeakTable == null)
			{
				Interlocked.CompareExchange<ConditionalWeakTable<TaskScheduler, object>>(ref TaskScheduler.s_activeTaskSchedulers, new ConditionalWeakTable<TaskScheduler, object>(), null);
				conditionalWeakTable = TaskScheduler.s_activeTaskSchedulers;
			}
			conditionalWeakTable.Add(this, null);
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060040ED RID: 16621 RVA: 0x000F181D File Offset: 0x000EFA1D
		[__DynamicallyInvokable]
		public static TaskScheduler Default
		{
			[__DynamicallyInvokable]
			get
			{
				return TaskScheduler.s_defaultTaskScheduler;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060040EE RID: 16622 RVA: 0x000F1824 File Offset: 0x000EFA24
		[__DynamicallyInvokable]
		public static TaskScheduler Current
		{
			[__DynamicallyInvokable]
			get
			{
				TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
				return internalCurrent ?? TaskScheduler.Default;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060040EF RID: 16623 RVA: 0x000F1844 File Offset: 0x000EFA44
		internal static TaskScheduler InternalCurrent
		{
			get
			{
				Task internalCurrent = Task.InternalCurrent;
				if (internalCurrent == null || (internalCurrent.CreationOptions & TaskCreationOptions.HideScheduler) != TaskCreationOptions.None)
				{
					return null;
				}
				return internalCurrent.ExecutingTaskScheduler;
			}
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x000F186D File Offset: 0x000EFA6D
		[__DynamicallyInvokable]
		public static TaskScheduler FromCurrentSynchronizationContext()
		{
			return new SynchronizationContextTaskScheduler();
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060040F1 RID: 16625 RVA: 0x000F1874 File Offset: 0x000EFA74
		[__DynamicallyInvokable]
		public int Id
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_taskSchedulerId == 0)
				{
					int num;
					do
					{
						num = Interlocked.Increment(ref TaskScheduler.s_taskSchedulerIdCounter);
					}
					while (num == 0);
					Interlocked.CompareExchange(ref this.m_taskSchedulerId, num, 0);
				}
				return this.m_taskSchedulerId;
			}
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x000F18B1 File Offset: 0x000EFAB1
		[SecurityCritical]
		[__DynamicallyInvokable]
		protected bool TryExecuteTask(Task task)
		{
			if (task.ExecutingTaskScheduler != this)
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskScheduler_ExecuteTask_WrongTaskScheduler"));
			}
			return task.ExecuteEntry(true);
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060040F3 RID: 16627 RVA: 0x000F18D4 File Offset: 0x000EFAD4
		// (remove) Token: 0x060040F4 RID: 16628 RVA: 0x000F192C File Offset: 0x000EFB2C
		[__DynamicallyInvokable]
		public static event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException
		{
			[SecurityCritical]
			[__DynamicallyInvokable]
			add
			{
				if (value != null)
				{
					RuntimeHelpers.PrepareContractedDelegate(value);
					object unobservedTaskExceptionLockObject = TaskScheduler._unobservedTaskExceptionLockObject;
					lock (unobservedTaskExceptionLockObject)
					{
						TaskScheduler._unobservedTaskException = (EventHandler<UnobservedTaskExceptionEventArgs>)Delegate.Combine(TaskScheduler._unobservedTaskException, value);
					}
				}
			}
			[SecurityCritical]
			[__DynamicallyInvokable]
			remove
			{
				object unobservedTaskExceptionLockObject = TaskScheduler._unobservedTaskExceptionLockObject;
				lock (unobservedTaskExceptionLockObject)
				{
					TaskScheduler._unobservedTaskException = (EventHandler<UnobservedTaskExceptionEventArgs>)Delegate.Remove(TaskScheduler._unobservedTaskException, value);
				}
			}
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x000F197C File Offset: 0x000EFB7C
		internal static void PublishUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs ueea)
		{
			object unobservedTaskExceptionLockObject = TaskScheduler._unobservedTaskExceptionLockObject;
			lock (unobservedTaskExceptionLockObject)
			{
				EventHandler<UnobservedTaskExceptionEventArgs> unobservedTaskException = TaskScheduler._unobservedTaskException;
				if (unobservedTaskException != null)
				{
					unobservedTaskException(sender, ueea);
				}
			}
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x000F19C8 File Offset: 0x000EFBC8
		[SecurityCritical]
		internal Task[] GetScheduledTasksForDebugger()
		{
			IEnumerable<Task> scheduledTasks = this.GetScheduledTasks();
			if (scheduledTasks == null)
			{
				return null;
			}
			Task[] array = scheduledTasks as Task[];
			if (array == null)
			{
				array = new List<Task>(scheduledTasks).ToArray();
			}
			foreach (Task task in array)
			{
				int id = task.Id;
			}
			return array;
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x000F1A18 File Offset: 0x000EFC18
		[SecurityCritical]
		internal static TaskScheduler[] GetTaskSchedulersForDebugger()
		{
			if (TaskScheduler.s_activeTaskSchedulers == null)
			{
				return new TaskScheduler[]
				{
					TaskScheduler.s_defaultTaskScheduler
				};
			}
			ICollection<TaskScheduler> keys = TaskScheduler.s_activeTaskSchedulers.Keys;
			if (!keys.Contains(TaskScheduler.s_defaultTaskScheduler))
			{
				keys.Add(TaskScheduler.s_defaultTaskScheduler);
			}
			TaskScheduler[] array = new TaskScheduler[keys.Count];
			keys.CopyTo(array, 0);
			foreach (TaskScheduler taskScheduler in array)
			{
				int id = taskScheduler.Id;
			}
			return array;
		}

		// Token: 0x04001AB2 RID: 6834
		private static ConditionalWeakTable<TaskScheduler, object> s_activeTaskSchedulers;

		// Token: 0x04001AB3 RID: 6835
		private static readonly TaskScheduler s_defaultTaskScheduler = new ThreadPoolTaskScheduler();

		// Token: 0x04001AB4 RID: 6836
		internal static int s_taskSchedulerIdCounter;

		// Token: 0x04001AB5 RID: 6837
		private volatile int m_taskSchedulerId;

		// Token: 0x04001AB6 RID: 6838
		private static EventHandler<UnobservedTaskExceptionEventArgs> _unobservedTaskException;

		// Token: 0x04001AB7 RID: 6839
		private static readonly object _unobservedTaskExceptionLockObject = new object();

		// Token: 0x02000BEE RID: 3054
		internal sealed class SystemThreadingTasks_TaskSchedulerDebugView
		{
			// Token: 0x06006ED6 RID: 28374 RVA: 0x0017D9FA File Offset: 0x0017BBFA
			public SystemThreadingTasks_TaskSchedulerDebugView(TaskScheduler scheduler)
			{
				this.m_taskScheduler = scheduler;
			}

			// Token: 0x17001311 RID: 4881
			// (get) Token: 0x06006ED7 RID: 28375 RVA: 0x0017DA09 File Offset: 0x0017BC09
			public int Id
			{
				get
				{
					return this.m_taskScheduler.Id;
				}
			}

			// Token: 0x17001312 RID: 4882
			// (get) Token: 0x06006ED8 RID: 28376 RVA: 0x0017DA16 File Offset: 0x0017BC16
			public IEnumerable<Task> ScheduledTasks
			{
				[SecurityCritical]
				get
				{
					return this.m_taskScheduler.GetScheduledTasks();
				}
			}

			// Token: 0x040035F1 RID: 13809
			private readonly TaskScheduler m_taskScheduler;
		}
	}
}
