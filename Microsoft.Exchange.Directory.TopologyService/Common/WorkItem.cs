using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Directory.TopologyService.Common
{
	// Token: 0x02000003 RID: 3
	internal abstract class WorkItem<T> : IWorkItem where T : class
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020D0 File Offset: 0x000002D0
		protected WorkItem()
		{
			this.whenCompleted = DateTime.MaxValue;
			this.whenStarted = DateTime.MaxValue;
			this.cancellationTokenSource = new CancellationTokenSource();
			this.ResultType = ResultType.None;
			this.completed = 0;
			this.data = default(T);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12
		public abstract string Id { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000211E File Offset: 0x0000031E
		public bool IsCompleted
		{
			get
			{
				return this.whenCompleted != DateTime.MaxValue;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002130 File Offset: 0x00000330
		public bool IsPending
		{
			get
			{
				return this.whenStarted == DateTime.MaxValue;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002142 File Offset: 0x00000342
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000214A File Offset: 0x0000034A
		public ResultType ResultType { get; protected set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002153 File Offset: 0x00000353
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000215B File Offset: 0x0000035B
		public T Data
		{
			get
			{
				return this.data;
			}
			protected set
			{
				this.data = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002164 File Offset: 0x00000364
		public DateTime WhenStarted
		{
			get
			{
				return this.whenStarted;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000216C File Offset: 0x0000036C
		public DateTime WhenCompleted
		{
			get
			{
				return this.whenCompleted;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000015 RID: 21
		public abstract TimeSpan TimeoutInterval { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002174 File Offset: 0x00000374
		public bool IsOverdue
		{
			get
			{
				return !(this.whenStarted == DateTime.MaxValue) && !this.IsCompleted && this.whenStarted < DateTime.UtcNow - this.TimeoutInterval;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002220 File Offset: 0x00000420
		public void StartCancel(int waitAmount, Action<IWorkItemResult> continuation)
		{
			Task task = Task.Factory.StartNew(delegate()
			{
				this.Cancel(waitAmount);
			}, TaskCreationOptions.LongRunning);
			task.ContinueWith(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					this.CompleteExecution(continuation, new WorkItemResult<T>(this, t.Exception));
					return;
				}
				this.CompleteExecution(continuation, new WorkItemResult<T>(this));
			}, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002310 File Offset: 0x00000510
		public void StartExecuting(Action<IWorkItemResult> continuation)
		{
			CancellationToken token = this.cancellationTokenSource.Token;
			this.workitemExecution = Task.Factory.StartNew(delegate()
			{
				this.Execute(token);
			}, token);
			this.workitemExecution.ContinueWith(delegate(Task t)
			{
				this.whenCompleted = DateTime.UtcNow;
				if (t.Status == TaskStatus.RanToCompletion)
				{
					this.ResultType = ResultType.Succeeded;
					this.CompleteExecution(continuation, new WorkItemResult<T>(this));
					return;
				}
				this.ResultType = ResultType.Failed;
				this.CompleteExecution(continuation, new WorkItemResult<T>(this, t.Exception));
			}, token, TaskContinuationOptions.AttachedToParent, TaskScheduler.Current);
		}

		// Token: 0x06000019 RID: 25
		protected abstract void DoWork(CancellationToken cancellationToken);

		// Token: 0x0600001A RID: 26 RVA: 0x00002388 File Offset: 0x00000588
		private void Execute(CancellationToken joinedToken)
		{
			this.whenStarted = DateTime.UtcNow;
			if (!joinedToken.IsCancellationRequested)
			{
				this.DoWork(joinedToken);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023A8 File Offset: 0x000005A8
		private void Cancel(int waitAmount)
		{
			this.cancellationTokenSource.Cancel();
			try
			{
				this.ResultType = ResultType.Abandoned;
				this.workitemExecution.Wait(waitAmount);
				this.ResultType = ResultType.TimedOut;
			}
			catch (AggregateException ex)
			{
				if (ex.InnerExceptions.Count != 1 || !(ex.InnerException is OperationCanceledException))
				{
					throw;
				}
				if (this.whenStarted == DateTime.MaxValue)
				{
					this.ResultType = ResultType.Abandoned;
				}
				else
				{
					this.ResultType = ResultType.TimedOut;
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002434 File Offset: 0x00000634
		private void CompleteExecution(Action<IWorkItemResult> continuation, IWorkItemResult result)
		{
			int num = Interlocked.Increment(ref this.completed);
			if (num == 1)
			{
				this.cancellationTokenSource.Dispose();
				continuation(result);
			}
		}

		// Token: 0x04000001 RID: 1
		private CancellationTokenSource cancellationTokenSource;

		// Token: 0x04000002 RID: 2
		private Task workitemExecution;

		// Token: 0x04000003 RID: 3
		private DateTime whenStarted;

		// Token: 0x04000004 RID: 4
		private DateTime whenCompleted;

		// Token: 0x04000005 RID: 5
		private int completed;

		// Token: 0x04000006 RID: 6
		private T data;
	}
}
