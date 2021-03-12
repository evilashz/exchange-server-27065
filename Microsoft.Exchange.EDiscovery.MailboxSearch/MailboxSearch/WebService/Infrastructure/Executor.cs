using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure
{
	// Token: 0x02000030 RID: 48
	internal class Executor : IExecutor
	{
		// Token: 0x0600021D RID: 541 RVA: 0x00012234 File Offset: 0x00010434
		public Executor(ISearchPolicy policy, Type taskType)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "Executor.ctor Task:", taskType);
			this.defaultTimeout = policy.ExecutionSettings.SearchTimeout;
			this.Policy = policy;
			this.TaskType = taskType;
			this.ExecutesInParallel = this.Policy.ExecutionSettings.DiscoveryExecutesInParallel;
			this.useRealThreads = this.useRealThreads;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000122CD File Offset: 0x000104CD
		// (set) Token: 0x0600021F RID: 543 RVA: 0x000122D5 File Offset: 0x000104D5
		public uint Concurrency
		{
			get
			{
				return this.threads;
			}
			set
			{
				this.threads = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000122DE File Offset: 0x000104DE
		// (set) Token: 0x06000221 RID: 545 RVA: 0x000122E6 File Offset: 0x000104E6
		public object TaskContext { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000122EF File Offset: 0x000104EF
		// (set) Token: 0x06000223 RID: 547 RVA: 0x000122F7 File Offset: 0x000104F7
		public ISearchPolicy Policy { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00012300 File Offset: 0x00010500
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00012308 File Offset: 0x00010508
		public ExecutorContext Context { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00012311 File Offset: 0x00010511
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00012319 File Offset: 0x00010519
		protected bool ExecutesInParallel { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00012322 File Offset: 0x00010522
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0001232A File Offset: 0x0001052A
		private protected Executor ChainedExecutor { protected get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00012333 File Offset: 0x00010533
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0001233B File Offset: 0x0001053B
		private protected Executor ParentExecutor { protected get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00012344 File Offset: 0x00010544
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0001234C File Offset: 0x0001054C
		private protected Type TaskType { protected get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00012355 File Offset: 0x00010555
		protected bool IsSynchronous
		{
			get
			{
				return this.threads == this.Policy.ExecutionSettings.DiscoverySynchronousConcurrency;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0001236F File Offset: 0x0001056F
		protected bool IsEnqueable
		{
			get
			{
				return !this.queue.IsAddingCompleted && !this.Context.CancellationTokenSource.IsCancellationRequested;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00012393 File Offset: 0x00010593
		protected bool IsCancelled
		{
			get
			{
				return this.Context.CancellationTokenSource.IsCancellationRequested;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000123A8 File Offset: 0x000105A8
		public Executor Chain(Executor executor)
		{
			this.EnsureContext();
			this.ChainedExecutor = executor;
			this.ChainedExecutor.Context = this.Context;
			this.ChainedExecutor.ParentExecutor = this;
			return executor;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000123E4 File Offset: 0x000105E4
		public virtual object Process(object item)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "Executor.Process Item:", item);
			this.EnsureContext();
			object output;
			using (this.Context)
			{
				if (this.Context.Input != null || !this.IsEnqueable || this.Context.Output != null)
				{
					Recorder.Trace(2L, TraceType.ErrorTrace, new object[]
					{
						"Executor.Process Invalid State  Input:",
						this.Context.Input,
						"Enqueuable:",
						this.IsEnqueable,
						"Output:",
						this.Context.Output
					});
					throw new InvalidOperationException();
				}
				this.Context.Input = item;
				this.Enqueue(item);
				this.SignalComplete();
				bool flag = this.Context.WaitHandle.WaitOne(this.Policy.ExecutionSettings.SearchTimeout);
				if (this.Context.FatalException != null)
				{
					Recorder.Trace(2L, TraceType.ErrorTrace, "Executor.Process Failed Error:", this.Context.FatalException);
					throw new SearchException(this.Context.FatalException);
				}
				if (!flag)
				{
					Recorder.Trace(2L, TraceType.ErrorTrace, "Executor.Process TimedOut");
					Exception ex = new SearchException(KnownError.ErrorSearchTimedOut);
					this.Cancel(ex);
					throw ex;
				}
				Recorder.Trace(2L, TraceType.InfoTrace, "Executor.Process Completed Output:", this.Context.Output);
				output = this.Context.Output;
			}
			return output;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0001256C File Offset: 0x0001076C
		public virtual void EnqueueNext(object item)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "Executor.Chain Item:", item);
			if (this.ChainedExecutor != null)
			{
				this.ChainedExecutor.Enqueue(item);
				return;
			}
			this.Context.Output = item;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000125A0 File Offset: 0x000107A0
		public virtual void Cancel(Exception ex)
		{
			Recorder.Trace(2L, TraceType.ErrorTrace, "Executor.Cancel Called Error:", ex);
			if (!this.IsCancelled)
			{
				Recorder.Trace(2L, TraceType.ErrorTrace, "Executor.Cancel First Cancel Error:", ex);
				this.Context.FatalException = ex;
				this.Context.CancellationTokenSource.Cancel(false);
				this.AttemptComplete();
			}
			Recorder.Record record = this.Policy.Recorder.Start(this.TaskType.Name, TraceType.FatalTrace, true);
			SearchException ex2 = ex as SearchException;
			if (ex2 != null)
			{
				record.Attributes["CancelError"] = ex2.Error.ToString();
				record.Attributes["CancelSource"] = ex2.Source;
			}
			record.Attributes["EX"] = ex.ToString();
			this.Policy.Recorder.End(record);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0001267C File Offset: 0x0001087C
		public virtual void Fail(Exception ex)
		{
			Recorder.Trace(2L, TraceType.WarningTrace, "Executor.Fail Error:", ex);
			this.Context.Failures.Add(ex);
			Recorder.Record record = this.Policy.Recorder.Start(this.TaskType.Name, TraceType.ErrorTrace, true);
			SearchException ex2 = ex as SearchException;
			if (ex2 != null)
			{
				record.Attributes["FailError"] = ex2.Error.ToString();
				record.Attributes["FailSource"] = ex2.Source;
			}
			record.Attributes["EX"] = ex.ToString();
			this.Policy.Recorder.End(record);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0001272D File Offset: 0x0001092D
		public override string ToString()
		{
			return string.Format("Executor: {0}", this.TaskType);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0001273F File Offset: 0x0001093F
		protected virtual void EnsureContext()
		{
			if (this.Context == null)
			{
				this.Context = new ExecutorContext();
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00012754 File Offset: 0x00010954
		protected virtual void EnsureRecorder()
		{
			if (this.currentRecord == null)
			{
				this.currentRecord = this.Policy.Recorder.Start(this.TaskType.Name, TraceType.InfoTrace, true);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00012784 File Offset: 0x00010984
		protected virtual void Enqueue(object item)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, new object[]
			{
				"Executor.Enqueue Item:",
				item,
				"Enqueable:",
				this.IsEnqueable,
				"Sync:",
				this.IsSynchronous
			});
			this.EnsureRecorder();
			if (this.IsEnqueable)
			{
				if (this.IsSynchronous)
				{
					this.RunThread(item);
					return;
				}
				this.queue.Add(item);
				this.EnsureThreads();
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0001280C File Offset: 0x00010A0C
		protected virtual void EnsureThreads()
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "Executor.EnsureThreads");
			if ((!this.ExecutesInParallel && !this.queue.IsAddingCompleted) || this.IsSynchronous)
			{
				return;
			}
			uint num = Math.Min((uint)this.queue.Count, (uint)((ulong)this.threads - (ulong)((long)this.current)));
			uint num2 = 0U;
			while ((long)this.current < (long)((ulong)this.threads) && num2 < num)
			{
				int num3 = Interlocked.Increment(ref this.current);
				if ((long)num3 < (long)((ulong)this.threads))
				{
					this.CreateThread();
				}
				else
				{
					Interlocked.Decrement(ref this.current);
				}
				num2 += 1U;
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000128AC File Offset: 0x00010AAC
		protected virtual void CreateThread()
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "Executor.CreateThreads Real:", this.useRealThreads);
			if (this.useRealThreads)
			{
				new Thread(new ParameterizedThreadStart(this.RunThread))
				{
					Name = string.Format("DiscoveryExecutionThread: {0}", this.TaskType)
				}.Start();
				return;
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.RunThread));
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000129F8 File Offset: 0x00010BF8
		protected virtual void RunThread(object state)
		{
			try
			{
				Recorder.Trace(2L, TraceType.InfoTrace, new object[]
				{
					"Executor.RunThread State:",
					state,
					"Synchronous:",
					this.IsSynchronous,
					"Current:",
					this.current
				});
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						if (this.IsSynchronous)
						{
							Interlocked.Increment(ref this.current);
							this.RunTask(state);
						}
						else
						{
							foreach (object item in this.queue.GetConsumingEnumerable())
							{
								this.RunTask(item);
							}
						}
					}
					catch (OperationCanceledException)
					{
						Recorder.Trace(2L, TraceType.WarningTrace, "Executor.RunThread OperationCancelled");
					}
					catch (SearchException ex2)
					{
						Recorder.Trace(2L, TraceType.ErrorTrace, "Executor.RunThread Search Error:", ex2);
						this.Cancel(ex2);
					}
				});
			}
			catch (GrayException ex)
			{
				Recorder.Trace(2L, TraceType.ErrorTrace, "Executor.RunThread Gray Error:", ex);
				this.Cancel(ex);
			}
			finally
			{
				Interlocked.Decrement(ref this.current);
				Recorder.Trace(2L, TraceType.InfoTrace, new object[]
				{
					"Executor.RunThread Completed Current:",
					this.current,
					"Cancelled:",
					this.IsCancelled
				});
				if (!this.IsCancelled)
				{
					this.AttemptComplete();
				}
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00012B14 File Offset: 0x00010D14
		protected virtual void RunTask(object item)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, new object[]
			{
				"Executor.RunTask Item:",
				item,
				"IsCancelled:",
				this.IsCancelled
			});
			if (!this.IsCancelled)
			{
				ITask task = null;
				try
				{
					long timestamp = this.Policy.Recorder.Timestamp;
					task = (Activator.CreateInstance(this.TaskType) as ITask);
					task.State = new SearchTaskContext
					{
						TaskContext = this.TaskContext,
						Executor = this,
						Item = item
					};
					task.Execute(this.defaultQueueDelay, this.defaultTimeout);
					task.Complete(this.defaultQueueDelay, this.defaultTimeout);
					long num = this.Policy.Recorder.Timestamp - timestamp;
					Interlocked.Increment(ref this.itemCount);
					Interlocked.Add(ref this.totalDuration, num);
					IList list = item as IList;
					int num2 = 1;
					if (list != null)
					{
						num2 = list.Count;
					}
					this.batchDurations.Add(new Tuple<long, long, long>(timestamp, num, (long)num2));
				}
				catch (SearchException ex)
				{
					if (task != null)
					{
						task.Cancel();
					}
					this.Cancel(ex);
					Recorder.Trace(2L, TraceType.ErrorTrace, "Executor.RunTask Failed Error:", ex);
				}
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00012C68 File Offset: 0x00010E68
		protected virtual void SignalComplete()
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "Executor.SignalComplete");
			this.queue.CompleteAdding();
			if (!this.IsSynchronous)
			{
				this.EnsureThreads();
			}
			this.AttemptComplete();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00012C98 File Offset: 0x00010E98
		protected virtual void AttemptComplete()
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "Executor.AttemptComplete");
			if ((this.queue.IsCompleted && this.current < 0) || this.IsCancelled)
			{
				Recorder.Trace(2L, TraceType.InfoTrace, "Executor.AttemptComplete Started");
				if (this.currentRecord != null)
				{
					this.currentRecord.Attributes["COUNT"] = this.itemCount;
					this.currentRecord.Attributes["WORKDURATION"] = this.totalDuration;
					this.Policy.Recorder.End(this.currentRecord);
					if (this.batchDurations.Count > 0)
					{
						string description = string.Format("{0}Batches", this.currentRecord.Description);
						Recorder.Record record = this.Policy.Recorder.Start(description, TraceType.InfoTrace, false);
						int num = 0;
						foreach (Tuple<long, long, long> tuple in this.batchDurations)
						{
							record.Attributes[string.Format("BATCH{0}START", num)] = tuple.Item1;
							record.Attributes[string.Format("BATCH{0}DURATION", num)] = tuple.Item2;
							record.Attributes[string.Format("BATCH{0}COUNT", num)] = tuple.Item3;
							num++;
						}
						this.Policy.Recorder.End(record);
					}
				}
				if (this.ChainedExecutor != null)
				{
					Recorder.Trace(2L, TraceType.InfoTrace, "Executor.AttemptComplete SignalNext");
					this.ChainedExecutor.SignalComplete();
				}
				else
				{
					Recorder.Trace(2L, TraceType.InfoTrace, "Executor.AttemptComplete SignalRoot");
					if (this.Context != null && !this.Context.IsDisposed && this.Context.WaitHandle != null && !this.Context.WaitHandle.SafeWaitHandle.IsClosed)
					{
						this.Context.WaitHandle.Set();
					}
				}
				Recorder.Trace(2L, TraceType.InfoTrace, "Executor.AttemptComplete Completed");
			}
		}

		// Token: 0x0400010C RID: 268
		protected BlockingCollection<object> queue = new BlockingCollection<object>();

		// Token: 0x0400010D RID: 269
		private readonly TimeSpan defaultQueueDelay = new TimeSpan(0L);

		// Token: 0x0400010E RID: 270
		private readonly TimeSpan defaultTimeout;

		// Token: 0x0400010F RID: 271
		private readonly bool useRealThreads;

		// Token: 0x04000110 RID: 272
		private readonly ConcurrentBag<Tuple<long, long, long>> batchDurations = new ConcurrentBag<Tuple<long, long, long>>();

		// Token: 0x04000111 RID: 273
		private Recorder.Record currentRecord;

		// Token: 0x04000112 RID: 274
		private uint threads = 1U;

		// Token: 0x04000113 RID: 275
		private int current = -1;

		// Token: 0x04000114 RID: 276
		private int itemCount;

		// Token: 0x04000115 RID: 277
		private long totalDuration;
	}
}
