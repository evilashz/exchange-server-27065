using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Exchange.ServiceHost.Common
{
	// Token: 0x02000007 RID: 7
	internal sealed class QueuedTaskScheduler : TaskScheduler, IDisposable
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002F43 File Offset: 0x00001143
		public QueuedTaskScheduler() : this(TaskScheduler.Default, 0)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002F51 File Offset: 0x00001151
		public QueuedTaskScheduler(int maxConcurrencyLevel) : this(TaskScheduler.Default, maxConcurrencyLevel)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002F5F File Offset: 0x0000115F
		public QueuedTaskScheduler(TaskScheduler targetScheduler) : this(targetScheduler, 0)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002F6C File Offset: 0x0000116C
		public QueuedTaskScheduler(TaskScheduler targetScheduler, int maxConcurrencyLevel)
		{
			if (targetScheduler == null)
			{
				throw new ArgumentNullException("targetScheduler");
			}
			if (maxConcurrencyLevel < 0)
			{
				throw new ArgumentOutOfRangeException("maxConcurrencyLevel");
			}
			this.targetScheduler = targetScheduler;
			this.concurrencyLevel = ((maxConcurrencyLevel != 0) ? maxConcurrencyLevel : Environment.ProcessorCount);
			if (targetScheduler.MaximumConcurrencyLevel > 0 && targetScheduler.MaximumConcurrencyLevel < this.concurrencyLevel)
			{
				this.concurrencyLevel = targetScheduler.MaximumConcurrencyLevel;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002FF8 File Offset: 0x000011F8
		public override int MaximumConcurrencyLevel
		{
			get
			{
				return this.concurrencyLevel;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000300E File Offset: 0x0000120E
		public int QueueCount
		{
			get
			{
				return this.queueGroups.Sum((KeyValuePair<int, QueuedTaskScheduler.QueueGroup> group) => group.Value.Count);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00003044 File Offset: 0x00001244
		public int QueuedTaskCount
		{
			get
			{
				int result;
				lock (this.taskQueue)
				{
					result = this.taskQueue.Count((Task t) => t != null);
				}
				return result;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000030A8 File Offset: 0x000012A8
		public void Dispose()
		{
			this.disposeCancellation.Cancel();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000030B5 File Offset: 0x000012B5
		public TaskScheduler CreateQueue()
		{
			return this.CreateQueue(0);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000030C0 File Offset: 0x000012C0
		public TaskScheduler CreateQueue(int priority)
		{
			QueuedTaskScheduler.QueuedTaskSchedulerQueue queuedTaskSchedulerQueue = new QueuedTaskScheduler.QueuedTaskSchedulerQueue(priority, this);
			lock (this.queueGroups)
			{
				QueuedTaskScheduler.QueueGroup queueGroup;
				if (!this.queueGroups.TryGetValue(priority, out queueGroup))
				{
					queueGroup = new QueuedTaskScheduler.QueueGroup();
					this.queueGroups.Add(priority, queueGroup);
				}
				queueGroup.Add(queuedTaskSchedulerQueue);
			}
			return queuedTaskSchedulerQueue;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003130 File Offset: 0x00001330
		protected override void QueueTask(Task task)
		{
			if (this.disposeCancellation.IsCancellationRequested)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			bool flag = false;
			lock (this.taskQueue)
			{
				this.taskQueue.Enqueue(task);
				if (this.tasksQueuedOrRunning < this.concurrencyLevel)
				{
					this.tasksQueuedOrRunning++;
					flag = true;
				}
			}
			if (flag)
			{
				Task.Factory.StartNew(new Action(this.ProcessPrioritizedAndBatchedTasks), CancellationToken.None, TaskCreationOptions.None, this.targetScheduler);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000031DC File Offset: 0x000013DC
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return QueuedTaskScheduler.TaskProcessingThread.Value && base.TryExecuteTask(task);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000031FC File Offset: 0x000013FC
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return (from t in this.taskQueue
			where t != null
			select t).ToList<Task>();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000322C File Offset: 0x0000142C
		private void FindNextTask(out Task targetTask, out QueuedTaskScheduler.QueuedTaskSchedulerQueue targetTaskQueue)
		{
			targetTask = null;
			targetTaskQueue = null;
			lock (this.queueGroups)
			{
				foreach (KeyValuePair<int, QueuedTaskScheduler.QueueGroup> keyValuePair in this.queueGroups)
				{
					QueuedTaskScheduler.QueueGroup value = keyValuePair.Value;
					foreach (int index in value.CreateSearchOrder())
					{
						targetTaskQueue = value[index];
						Queue<Task> workItems = targetTaskQueue.WorkItems;
						if (workItems.Count > 0)
						{
							targetTask = workItems.Dequeue();
							if (targetTaskQueue.Disposed && workItems.Count == 0)
							{
								this.RemoveQueue(targetTaskQueue);
							}
							value.NextQueueIndex = (value.NextQueueIndex + 1) % keyValuePair.Value.Count;
							return;
						}
					}
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000334C File Offset: 0x0000154C
		private void ProcessPrioritizedAndBatchedTasks()
		{
			bool flag = true;
			while (!this.disposeCancellation.IsCancellationRequested && flag)
			{
				try
				{
					QueuedTaskScheduler.TaskProcessingThread.Value = true;
					while (!this.disposeCancellation.IsCancellationRequested)
					{
						Task task;
						lock (this.taskQueue)
						{
							if (this.taskQueue.Count == 0)
							{
								break;
							}
							task = this.taskQueue.Dequeue();
						}
						QueuedTaskScheduler.QueuedTaskSchedulerQueue queuedTaskSchedulerQueue = null;
						if (task == null)
						{
							this.FindNextTask(out task, out queuedTaskSchedulerQueue);
						}
						if (task != null)
						{
							if (queuedTaskSchedulerQueue != null)
							{
								queuedTaskSchedulerQueue.ExecuteTask(task);
							}
							else
							{
								base.TryExecuteTask(task);
							}
						}
					}
				}
				finally
				{
					lock (this.taskQueue)
					{
						if (this.taskQueue.Count == 0)
						{
							this.tasksQueuedOrRunning--;
							flag = false;
							QueuedTaskScheduler.TaskProcessingThread.Value = false;
						}
					}
				}
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003464 File Offset: 0x00001664
		private void NotifyNewWorkItem()
		{
			this.QueueTask(null);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003470 File Offset: 0x00001670
		private void RemoveQueue(QueuedTaskScheduler.QueuedTaskSchedulerQueue queue)
		{
			QueuedTaskScheduler.QueueGroup queueGroup = this.queueGroups[queue.Priority];
			int num = queueGroup.IndexOf(queue);
			if (queueGroup.NextQueueIndex >= num)
			{
				queueGroup.NextQueueIndex--;
			}
			queueGroup.RemoveAt(num);
		}

		// Token: 0x04000016 RID: 22
		private static readonly ThreadLocal<bool> TaskProcessingThread = new ThreadLocal<bool>();

		// Token: 0x04000017 RID: 23
		private readonly Queue<Task> taskQueue = new Queue<Task>();

		// Token: 0x04000018 RID: 24
		private readonly SortedList<int, QueuedTaskScheduler.QueueGroup> queueGroups = new SortedList<int, QueuedTaskScheduler.QueueGroup>();

		// Token: 0x04000019 RID: 25
		private readonly CancellationTokenSource disposeCancellation = new CancellationTokenSource();

		// Token: 0x0400001A RID: 26
		private readonly int concurrencyLevel;

		// Token: 0x0400001B RID: 27
		private readonly TaskScheduler targetScheduler;

		// Token: 0x0400001C RID: 28
		private int tasksQueuedOrRunning;

		// Token: 0x02000008 RID: 8
		private class QueueGroup : List<QueuedTaskScheduler.QueuedTaskSchedulerQueue>
		{
			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000039 RID: 57 RVA: 0x000034C1 File Offset: 0x000016C1
			// (set) Token: 0x0600003A RID: 58 RVA: 0x000034C9 File Offset: 0x000016C9
			public int NextQueueIndex { get; set; }

			// Token: 0x0600003B RID: 59 RVA: 0x00003624 File Offset: 0x00001824
			public IEnumerable<int> CreateSearchOrder()
			{
				for (int i = this.NextQueueIndex; i < base.Count; i++)
				{
					yield return i;
				}
				for (int j = 0; j < this.NextQueueIndex; j++)
				{
					yield return j;
				}
				yield break;
			}
		}

		// Token: 0x02000009 RID: 9
		private sealed class QueuedTaskSchedulerQueue : TaskScheduler, IDisposable
		{
			// Token: 0x0600003D RID: 61 RVA: 0x00003649 File Offset: 0x00001849
			internal QueuedTaskSchedulerQueue(int priority, QueuedTaskScheduler pool)
			{
				this.pool = pool;
				this.Priority = priority;
				this.WorkItems = new Queue<Task>();
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600003E RID: 62 RVA: 0x0000366A File Offset: 0x0000186A
			public override int MaximumConcurrencyLevel
			{
				get
				{
					return this.pool.MaximumConcurrencyLevel;
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600003F RID: 63 RVA: 0x00003677 File Offset: 0x00001877
			// (set) Token: 0x06000040 RID: 64 RVA: 0x0000367F File Offset: 0x0000187F
			internal bool Disposed { get; private set; }

			// Token: 0x06000041 RID: 65 RVA: 0x00003688 File Offset: 0x00001888
			public void Dispose()
			{
				if (!this.Disposed)
				{
					lock (this.pool.queueGroups)
					{
						if (this.WorkItems.Count == 0)
						{
							this.pool.RemoveQueue(this);
						}
					}
					this.Disposed = true;
				}
			}

			// Token: 0x06000042 RID: 66 RVA: 0x000036F0 File Offset: 0x000018F0
			internal void ExecuteTask(Task task)
			{
				base.TryExecuteTask(task);
			}

			// Token: 0x06000043 RID: 67 RVA: 0x000036FA File Offset: 0x000018FA
			protected override IEnumerable<Task> GetScheduledTasks()
			{
				return this.WorkItems.ToList<Task>();
			}

			// Token: 0x06000044 RID: 68 RVA: 0x00003708 File Offset: 0x00001908
			protected override void QueueTask(Task task)
			{
				if (this.Disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				lock (this.pool.queueGroups)
				{
					this.WorkItems.Enqueue(task);
				}
				this.pool.NotifyNewWorkItem();
			}

			// Token: 0x06000045 RID: 69 RVA: 0x00003778 File Offset: 0x00001978
			protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
			{
				return QueuedTaskScheduler.TaskProcessingThread.Value && base.TryExecuteTask(task);
			}

			// Token: 0x04000021 RID: 33
			internal readonly Queue<Task> WorkItems;

			// Token: 0x04000022 RID: 34
			internal readonly int Priority;

			// Token: 0x04000023 RID: 35
			private readonly QueuedTaskScheduler pool;
		}
	}
}
