using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	// Token: 0x02000554 RID: 1364
	[DebuggerDisplay("Concurrent={ConcurrentTaskCountForDebugger}, Exclusive={ExclusiveTaskCountForDebugger}, Mode={ModeForDebugger}")]
	[DebuggerTypeProxy(typeof(ConcurrentExclusiveSchedulerPair.DebugView))]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ConcurrentExclusiveSchedulerPair
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06004129 RID: 16681 RVA: 0x000F2224 File Offset: 0x000F0424
		private static int DefaultMaxConcurrencyLevel
		{
			get
			{
				return Environment.ProcessorCount;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x000F222B File Offset: 0x000F042B
		private object ValueLock
		{
			get
			{
				return this.m_threadProcessingMapping;
			}
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x000F2233 File Offset: 0x000F0433
		[__DynamicallyInvokable]
		public ConcurrentExclusiveSchedulerPair() : this(TaskScheduler.Default, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
		{
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x000F2246 File Offset: 0x000F0446
		[__DynamicallyInvokable]
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler) : this(taskScheduler, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
		{
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x000F2255 File Offset: 0x000F0455
		[__DynamicallyInvokable]
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel) : this(taskScheduler, maxConcurrencyLevel, -1)
		{
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x000F2260 File Offset: 0x000F0460
		[__DynamicallyInvokable]
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel, int maxItemsPerTask)
		{
			if (taskScheduler == null)
			{
				throw new ArgumentNullException("taskScheduler");
			}
			if (maxConcurrencyLevel == 0 || maxConcurrencyLevel < -1)
			{
				throw new ArgumentOutOfRangeException("maxConcurrencyLevel");
			}
			if (maxItemsPerTask == 0 || maxItemsPerTask < -1)
			{
				throw new ArgumentOutOfRangeException("maxItemsPerTask");
			}
			this.m_underlyingTaskScheduler = taskScheduler;
			this.m_maxConcurrencyLevel = maxConcurrencyLevel;
			this.m_maxItemsPerTask = maxItemsPerTask;
			int maximumConcurrencyLevel = taskScheduler.MaximumConcurrencyLevel;
			if (maximumConcurrencyLevel > 0 && maximumConcurrencyLevel < this.m_maxConcurrencyLevel)
			{
				this.m_maxConcurrencyLevel = maximumConcurrencyLevel;
			}
			if (this.m_maxConcurrencyLevel == -1)
			{
				this.m_maxConcurrencyLevel = int.MaxValue;
			}
			if (this.m_maxItemsPerTask == -1)
			{
				this.m_maxItemsPerTask = int.MaxValue;
			}
			this.m_exclusiveTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, 1, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask);
			this.m_concurrentTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, this.m_maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks);
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x000F232C File Offset: 0x000F052C
		[__DynamicallyInvokable]
		public void Complete()
		{
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				if (!this.CompletionRequested)
				{
					this.RequestCompletion();
					this.CleanupStateIfCompletingAndQuiesced();
				}
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06004130 RID: 16688 RVA: 0x000F237C File Offset: 0x000F057C
		[__DynamicallyInvokable]
		public Task Completion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.EnsureCompletionStateInitialized().Task;
			}
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x000F2389 File Offset: 0x000F0589
		private ConcurrentExclusiveSchedulerPair.CompletionState EnsureCompletionStateInitialized()
		{
			return LazyInitializer.EnsureInitialized<ConcurrentExclusiveSchedulerPair.CompletionState>(ref this.m_completionState, () => new ConcurrentExclusiveSchedulerPair.CompletionState());
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06004132 RID: 16690 RVA: 0x000F23B5 File Offset: 0x000F05B5
		private bool CompletionRequested
		{
			get
			{
				return this.m_completionState != null && Volatile.Read(ref this.m_completionState.m_completionRequested);
			}
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x000F23D1 File Offset: 0x000F05D1
		private void RequestCompletion()
		{
			this.EnsureCompletionStateInitialized().m_completionRequested = true;
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x000F23DF File Offset: 0x000F05DF
		private void CleanupStateIfCompletingAndQuiesced()
		{
			if (this.ReadyToComplete)
			{
				this.CompleteTaskAsync();
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06004135 RID: 16693 RVA: 0x000F23F0 File Offset: 0x000F05F0
		private bool ReadyToComplete
		{
			get
			{
				if (!this.CompletionRequested || this.m_processingCount != 0)
				{
					return false;
				}
				ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
				return (completionState.m_exceptions != null && completionState.m_exceptions.Count > 0) || (this.m_concurrentTaskScheduler.m_tasks.IsEmpty && this.m_exclusiveTaskScheduler.m_tasks.IsEmpty);
			}
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x000F2454 File Offset: 0x000F0654
		private void CompleteTaskAsync()
		{
			ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
			if (!completionState.m_completionQueued)
			{
				completionState.m_completionQueued = true;
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					ConcurrentExclusiveSchedulerPair.CompletionState completionState2 = (ConcurrentExclusiveSchedulerPair.CompletionState)state;
					List<Exception> exceptions = completionState2.m_exceptions;
					if (exceptions == null || exceptions.Count <= 0)
					{
						completionState2.TrySetResult(default(VoidTaskResult));
					}
					else
					{
						completionState2.TrySetException(exceptions);
					}
				}, completionState);
			}
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x000F24A0 File Offset: 0x000F06A0
		private void FaultWithTask(Task faultedTask)
		{
			ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
			if (completionState.m_exceptions == null)
			{
				completionState.m_exceptions = new List<Exception>();
			}
			completionState.m_exceptions.AddRange(faultedTask.Exception.InnerExceptions);
			this.RequestCompletion();
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06004138 RID: 16696 RVA: 0x000F24E3 File Offset: 0x000F06E3
		[__DynamicallyInvokable]
		public TaskScheduler ConcurrentScheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_concurrentTaskScheduler;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06004139 RID: 16697 RVA: 0x000F24EB File Offset: 0x000F06EB
		[__DynamicallyInvokable]
		public TaskScheduler ExclusiveScheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_exclusiveTaskScheduler;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x0600413A RID: 16698 RVA: 0x000F24F3 File Offset: 0x000F06F3
		private int ConcurrentTaskCountForDebugger
		{
			get
			{
				return this.m_concurrentTaskScheduler.m_tasks.Count;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600413B RID: 16699 RVA: 0x000F2505 File Offset: 0x000F0705
		private int ExclusiveTaskCountForDebugger
		{
			get
			{
				return this.m_exclusiveTaskScheduler.m_tasks.Count;
			}
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x000F2518 File Offset: 0x000F0718
		private void ProcessAsyncIfNecessary(bool fairly = false)
		{
			if (this.m_processingCount >= 0)
			{
				bool flag = !this.m_exclusiveTaskScheduler.m_tasks.IsEmpty;
				Task task = null;
				if (this.m_processingCount == 0 && flag)
				{
					this.m_processingCount = -1;
					try
					{
						task = new Task(delegate(object thisPair)
						{
							((ConcurrentExclusiveSchedulerPair)thisPair).ProcessExclusiveTasks();
						}, this, default(CancellationToken), ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly));
						task.Start(this.m_underlyingTaskScheduler);
						goto IL_149;
					}
					catch
					{
						this.m_processingCount = 0;
						this.FaultWithTask(task);
						goto IL_149;
					}
				}
				int count = this.m_concurrentTaskScheduler.m_tasks.Count;
				if (count > 0 && !flag && this.m_processingCount < this.m_maxConcurrencyLevel)
				{
					int num = 0;
					while (num < count && this.m_processingCount < this.m_maxConcurrencyLevel)
					{
						this.m_processingCount++;
						try
						{
							task = new Task(delegate(object thisPair)
							{
								((ConcurrentExclusiveSchedulerPair)thisPair).ProcessConcurrentTasks();
							}, this, default(CancellationToken), ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly));
							task.Start(this.m_underlyingTaskScheduler);
						}
						catch
						{
							this.m_processingCount--;
							this.FaultWithTask(task);
						}
						num++;
					}
				}
				IL_149:
				this.CleanupStateIfCompletingAndQuiesced();
			}
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x000F2690 File Offset: 0x000F0890
		private void ProcessExclusiveTasks()
		{
			try
			{
				this.m_threadProcessingMapping[Thread.CurrentThread.ManagedThreadId] = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
				for (int i = 0; i < this.m_maxItemsPerTask; i++)
				{
					Task task;
					if (!this.m_exclusiveTaskScheduler.m_tasks.TryDequeue(out task))
					{
						break;
					}
					if (!task.IsFaulted)
					{
						this.m_exclusiveTaskScheduler.ExecuteTask(task);
					}
				}
			}
			finally
			{
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
				this.m_threadProcessingMapping.TryRemove(Thread.CurrentThread.ManagedThreadId, out processingMode);
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					this.m_processingCount = 0;
					this.ProcessAsyncIfNecessary(true);
				}
			}
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x000F2754 File Offset: 0x000F0954
		private void ProcessConcurrentTasks()
		{
			try
			{
				this.m_threadProcessingMapping[Thread.CurrentThread.ManagedThreadId] = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
				for (int i = 0; i < this.m_maxItemsPerTask; i++)
				{
					Task task;
					if (!this.m_concurrentTaskScheduler.m_tasks.TryDequeue(out task))
					{
						break;
					}
					if (!task.IsFaulted)
					{
						this.m_concurrentTaskScheduler.ExecuteTask(task);
					}
					if (!this.m_exclusiveTaskScheduler.m_tasks.IsEmpty)
					{
						break;
					}
				}
			}
			finally
			{
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
				this.m_threadProcessingMapping.TryRemove(Thread.CurrentThread.ManagedThreadId, out processingMode);
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					if (this.m_processingCount > 0)
					{
						this.m_processingCount--;
					}
					this.ProcessAsyncIfNecessary(true);
				}
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x0600413F RID: 16703 RVA: 0x000F2840 File Offset: 0x000F0A40
		private ConcurrentExclusiveSchedulerPair.ProcessingMode ModeForDebugger
		{
			get
			{
				if (this.m_completionState != null && this.m_completionState.Task.IsCompleted)
				{
					return ConcurrentExclusiveSchedulerPair.ProcessingMode.Completed;
				}
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode = ConcurrentExclusiveSchedulerPair.ProcessingMode.NotCurrentlyProcessing;
				if (this.m_processingCount == -1)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
				}
				if (this.m_processingCount >= 1)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
				}
				if (this.CompletionRequested)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.Completing;
				}
				return processingMode;
			}
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x000F2892 File Offset: 0x000F0A92
		[Conditional("DEBUG")]
		internal static void ContractAssertMonitorStatus(object syncObj, bool held)
		{
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x000F2894 File Offset: 0x000F0A94
		internal static TaskCreationOptions GetCreationOptionsForTask(bool isReplacementReplica = false)
		{
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.DenyChildAttach;
			if (isReplacementReplica)
			{
				taskCreationOptions |= TaskCreationOptions.PreferFairness;
			}
			return taskCreationOptions;
		}

		// Token: 0x04001AD5 RID: 6869
		private readonly ConcurrentDictionary<int, ConcurrentExclusiveSchedulerPair.ProcessingMode> m_threadProcessingMapping = new ConcurrentDictionary<int, ConcurrentExclusiveSchedulerPair.ProcessingMode>();

		// Token: 0x04001AD6 RID: 6870
		private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_concurrentTaskScheduler;

		// Token: 0x04001AD7 RID: 6871
		private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_exclusiveTaskScheduler;

		// Token: 0x04001AD8 RID: 6872
		private readonly TaskScheduler m_underlyingTaskScheduler;

		// Token: 0x04001AD9 RID: 6873
		private readonly int m_maxConcurrencyLevel;

		// Token: 0x04001ADA RID: 6874
		private readonly int m_maxItemsPerTask;

		// Token: 0x04001ADB RID: 6875
		private int m_processingCount;

		// Token: 0x04001ADC RID: 6876
		private ConcurrentExclusiveSchedulerPair.CompletionState m_completionState;

		// Token: 0x04001ADD RID: 6877
		private const int UNLIMITED_PROCESSING = -1;

		// Token: 0x04001ADE RID: 6878
		private const int EXCLUSIVE_PROCESSING_SENTINEL = -1;

		// Token: 0x04001ADF RID: 6879
		private const int DEFAULT_MAXITEMSPERTASK = -1;

		// Token: 0x02000BF1 RID: 3057
		private sealed class CompletionState : TaskCompletionSource<VoidTaskResult>
		{
			// Token: 0x040035FB RID: 13819
			internal bool m_completionRequested;

			// Token: 0x040035FC RID: 13820
			internal bool m_completionQueued;

			// Token: 0x040035FD RID: 13821
			internal List<Exception> m_exceptions;
		}

		// Token: 0x02000BF2 RID: 3058
		[DebuggerDisplay("Count={CountForDebugger}, MaxConcurrencyLevel={m_maxConcurrencyLevel}, Id={Id}")]
		[DebuggerTypeProxy(typeof(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.DebugView))]
		private sealed class ConcurrentExclusiveTaskScheduler : TaskScheduler
		{
			// Token: 0x06006EE3 RID: 28387 RVA: 0x0017DBAC File Offset: 0x0017BDAC
			internal ConcurrentExclusiveTaskScheduler(ConcurrentExclusiveSchedulerPair pair, int maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode)
			{
				this.m_pair = pair;
				this.m_maxConcurrencyLevel = maxConcurrencyLevel;
				this.m_processingMode = processingMode;
				IProducerConsumerQueue<Task> tasks;
				if (processingMode != ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask)
				{
					IProducerConsumerQueue<Task> producerConsumerQueue = new MultiProducerMultiConsumerQueue<Task>();
					tasks = producerConsumerQueue;
				}
				else
				{
					IProducerConsumerQueue<Task> producerConsumerQueue = new SingleProducerSingleConsumerQueue<Task>();
					tasks = producerConsumerQueue;
				}
				this.m_tasks = tasks;
			}

			// Token: 0x17001315 RID: 4885
			// (get) Token: 0x06006EE4 RID: 28388 RVA: 0x0017DBEE File Offset: 0x0017BDEE
			public override int MaximumConcurrencyLevel
			{
				get
				{
					return this.m_maxConcurrencyLevel;
				}
			}

			// Token: 0x06006EE5 RID: 28389 RVA: 0x0017DBF8 File Offset: 0x0017BDF8
			[SecurityCritical]
			protected internal override void QueueTask(Task task)
			{
				object valueLock = this.m_pair.ValueLock;
				lock (valueLock)
				{
					if (this.m_pair.CompletionRequested)
					{
						throw new InvalidOperationException(base.GetType().Name);
					}
					this.m_tasks.Enqueue(task);
					this.m_pair.ProcessAsyncIfNecessary(false);
				}
			}

			// Token: 0x06006EE6 RID: 28390 RVA: 0x0017DC70 File Offset: 0x0017BE70
			[SecuritySafeCritical]
			internal void ExecuteTask(Task task)
			{
				base.TryExecuteTask(task);
			}

			// Token: 0x06006EE7 RID: 28391 RVA: 0x0017DC7C File Offset: 0x0017BE7C
			[SecurityCritical]
			protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
			{
				if (!taskWasPreviouslyQueued && this.m_pair.CompletionRequested)
				{
					return false;
				}
				bool flag = this.m_pair.m_underlyingTaskScheduler == TaskScheduler.Default;
				if (flag && taskWasPreviouslyQueued && !Thread.CurrentThread.IsThreadPoolThread)
				{
					return false;
				}
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
				if (!this.m_pair.m_threadProcessingMapping.TryGetValue(Thread.CurrentThread.ManagedThreadId, out processingMode) || processingMode != this.m_processingMode)
				{
					return false;
				}
				if (!flag || taskWasPreviouslyQueued)
				{
					return this.TryExecuteTaskInlineOnTargetScheduler(task);
				}
				return base.TryExecuteTask(task);
			}

			// Token: 0x06006EE8 RID: 28392 RVA: 0x0017DD00 File Offset: 0x0017BF00
			private bool TryExecuteTaskInlineOnTargetScheduler(Task task)
			{
				Task<bool> task2 = new Task<bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.s_tryExecuteTaskShim, Tuple.Create<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>(this, task));
				bool result;
				try
				{
					task2.RunSynchronously(this.m_pair.m_underlyingTaskScheduler);
					result = task2.Result;
				}
				catch
				{
					AggregateException exception = task2.Exception;
					throw;
				}
				finally
				{
					task2.Dispose();
				}
				return result;
			}

			// Token: 0x06006EE9 RID: 28393 RVA: 0x0017DD68 File Offset: 0x0017BF68
			[SecuritySafeCritical]
			private static bool TryExecuteTaskShim(object state)
			{
				Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task> tuple = (Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>)state;
				return tuple.Item1.TryExecuteTask(tuple.Item2);
			}

			// Token: 0x06006EEA RID: 28394 RVA: 0x0017DD8D File Offset: 0x0017BF8D
			[SecurityCritical]
			protected override IEnumerable<Task> GetScheduledTasks()
			{
				return this.m_tasks;
			}

			// Token: 0x17001316 RID: 4886
			// (get) Token: 0x06006EEB RID: 28395 RVA: 0x0017DD95 File Offset: 0x0017BF95
			private int CountForDebugger
			{
				get
				{
					return this.m_tasks.Count;
				}
			}

			// Token: 0x040035FE RID: 13822
			private static readonly Func<object, bool> s_tryExecuteTaskShim = new Func<object, bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.TryExecuteTaskShim);

			// Token: 0x040035FF RID: 13823
			private readonly ConcurrentExclusiveSchedulerPair m_pair;

			// Token: 0x04003600 RID: 13824
			private readonly int m_maxConcurrencyLevel;

			// Token: 0x04003601 RID: 13825
			private readonly ConcurrentExclusiveSchedulerPair.ProcessingMode m_processingMode;

			// Token: 0x04003602 RID: 13826
			internal readonly IProducerConsumerQueue<Task> m_tasks;

			// Token: 0x02000CD6 RID: 3286
			private sealed class DebugView
			{
				// Token: 0x060070F3 RID: 28915 RVA: 0x00184991 File Offset: 0x00182B91
				public DebugView(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler scheduler)
				{
					this.m_taskScheduler = scheduler;
				}

				// Token: 0x17001375 RID: 4981
				// (get) Token: 0x060070F4 RID: 28916 RVA: 0x001849A0 File Offset: 0x00182BA0
				public int MaximumConcurrencyLevel
				{
					get
					{
						return this.m_taskScheduler.m_maxConcurrencyLevel;
					}
				}

				// Token: 0x17001376 RID: 4982
				// (get) Token: 0x060070F5 RID: 28917 RVA: 0x001849AD File Offset: 0x00182BAD
				public IEnumerable<Task> ScheduledTasks
				{
					get
					{
						return this.m_taskScheduler.m_tasks;
					}
				}

				// Token: 0x17001377 RID: 4983
				// (get) Token: 0x060070F6 RID: 28918 RVA: 0x001849BA File Offset: 0x00182BBA
				public ConcurrentExclusiveSchedulerPair SchedulerPair
				{
					get
					{
						return this.m_taskScheduler.m_pair;
					}
				}

				// Token: 0x04003878 RID: 14456
				private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_taskScheduler;
			}
		}

		// Token: 0x02000BF3 RID: 3059
		private sealed class DebugView
		{
			// Token: 0x06006EED RID: 28397 RVA: 0x0017DDB5 File Offset: 0x0017BFB5
			public DebugView(ConcurrentExclusiveSchedulerPair pair)
			{
				this.m_pair = pair;
			}

			// Token: 0x17001317 RID: 4887
			// (get) Token: 0x06006EEE RID: 28398 RVA: 0x0017DDC4 File Offset: 0x0017BFC4
			public ConcurrentExclusiveSchedulerPair.ProcessingMode Mode
			{
				get
				{
					return this.m_pair.ModeForDebugger;
				}
			}

			// Token: 0x17001318 RID: 4888
			// (get) Token: 0x06006EEF RID: 28399 RVA: 0x0017DDD1 File Offset: 0x0017BFD1
			public IEnumerable<Task> ScheduledExclusive
			{
				get
				{
					return this.m_pair.m_exclusiveTaskScheduler.m_tasks;
				}
			}

			// Token: 0x17001319 RID: 4889
			// (get) Token: 0x06006EF0 RID: 28400 RVA: 0x0017DDE3 File Offset: 0x0017BFE3
			public IEnumerable<Task> ScheduledConcurrent
			{
				get
				{
					return this.m_pair.m_concurrentTaskScheduler.m_tasks;
				}
			}

			// Token: 0x1700131A RID: 4890
			// (get) Token: 0x06006EF1 RID: 28401 RVA: 0x0017DDF5 File Offset: 0x0017BFF5
			public int CurrentlyExecutingTaskCount
			{
				get
				{
					if (this.m_pair.m_processingCount != -1)
					{
						return this.m_pair.m_processingCount;
					}
					return 1;
				}
			}

			// Token: 0x1700131B RID: 4891
			// (get) Token: 0x06006EF2 RID: 28402 RVA: 0x0017DE12 File Offset: 0x0017C012
			public TaskScheduler TargetScheduler
			{
				get
				{
					return this.m_pair.m_underlyingTaskScheduler;
				}
			}

			// Token: 0x04003603 RID: 13827
			private readonly ConcurrentExclusiveSchedulerPair m_pair;
		}

		// Token: 0x02000BF4 RID: 3060
		[Flags]
		private enum ProcessingMode : byte
		{
			// Token: 0x04003605 RID: 13829
			NotCurrentlyProcessing = 0,
			// Token: 0x04003606 RID: 13830
			ProcessingExclusiveTask = 1,
			// Token: 0x04003607 RID: 13831
			ProcessingConcurrentTasks = 2,
			// Token: 0x04003608 RID: 13832
			Completing = 4,
			// Token: 0x04003609 RID: 13833
			Completed = 8
		}
	}
}
