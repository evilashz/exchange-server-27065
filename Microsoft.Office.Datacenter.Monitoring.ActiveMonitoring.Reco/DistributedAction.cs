using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000032 RID: 50
	public class DistributedAction : DisposeTrackableBase
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00005A3C File Offset: 0x00003C3C
		public DistributedAction(string[] resources, int votesRequired = -1, bool isSyncMode = false)
		{
			this.resources = resources;
			this.isSyncMode = isSyncMode;
			this.ExceptionsByServer = new ConcurrentDictionary<string, Exception>();
			if (votesRequired == -1)
			{
				this.votesRequired = this.resources.Length / 2 + 1;
				return;
			}
			this.votesRequired = votesRequired;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005AA7 File Offset: 0x00003CA7
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00005AAF File Offset: 0x00003CAF
		internal int TotalRequests { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00005AB8 File Offset: 0x00003CB8
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00005AC0 File Offset: 0x00003CC0
		internal int SuccessCount { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005AC9 File Offset: 0x00003CC9
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00005AD1 File Offset: 0x00003CD1
		internal int FailedCount { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005ADA File Offset: 0x00003CDA
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00005AE2 File Offset: 0x00003CE2
		internal ConcurrentDictionary<string, Exception> ExceptionsByServer { get; private set; }

		// Token: 0x0600018D RID: 397 RVA: 0x00005D48 File Offset: 0x00003F48
		public bool Run(Action<string> actionToPerform, TimeSpan timeout)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.TotalRequests = this.resources.Length;
			Task[] array = new Task[this.TotalRequests];
			for (int i = 0; i < this.TotalRequests; i++)
			{
				string resource = this.resources[i];
				Task task = Task.Factory.StartNew(delegate()
				{
					Exception ex3 = null;
					if (this.isCompleted)
					{
						WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.RecoveryActionTracer, this.traceContext, "DistributedAction: Action({0}) skipped since the calculations are already concluded.", resource, null, "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\DistributedAction.cs", 140);
						return;
					}
					try
					{
						actionToPerform(resource);
						WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.RecoveryActionTracer, this.traceContext, "DistributedAction: Action({0}) succeeded.", resource, null, "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\DistributedAction.cs", 153);
					}
					catch (Exception ex4)
					{
						this.ExceptionsByServer[resource] = ex4;
						ex3 = ex4;
						WTFDiagnostics.TraceError<string, string>(ExTraceGlobals.RecoveryActionTracer, this.traceContext, "DistributedAction: Action({0}) failed with error {1}.", resource, ex4.ToString(), null, "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\DistributedAction.cs", 165);
					}
					lock (this.locker)
					{
						if (!this.isCompleted)
						{
							if (ex3 == null)
							{
								this.SuccessCount++;
							}
							else
							{
								this.FailedCount++;
							}
							if (this.SuccessCount + this.FailedCount >= this.votesRequired)
							{
								this.isCompleted = true;
								if (!this.cancellationTokenSource.IsCancellationRequested)
								{
									this.cancellationTokenSource.Cancel();
								}
							}
						}
						else
						{
							WTFDiagnostics.TraceWarning<string>(ExTraceGlobals.RecoveryActionTracer, this.traceContext, "DistributedAction: Action({0}) success/failure count update skipped since it is already marked completed.", resource, null, "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\DistributedAction.cs", 200);
						}
					}
				});
				array[i] = task;
				if (this.isSyncMode)
				{
					task.Wait();
				}
			}
			Exception ex = null;
			bool flag = false;
			try
			{
				flag = !Task.WaitAll(array, (int)timeout.TotalMilliseconds, this.cancellationTokenSource.Token);
			}
			catch (Exception ex2)
			{
				if (!(ex2 is OperationCanceledException))
				{
					ex = ex2;
				}
			}
			WTFDiagnostics.TraceError(ExTraceGlobals.RecoveryActionTracer, this.traceContext, string.Format("DistributedAction: Run() Completed. Duration:{0}, IsTimedout:{1}, Total: {2}, Success: {3}, Failed: {4} (Error: {5})", new object[]
			{
				stopwatch.Elapsed,
				flag,
				this.TotalRequests,
				this.SuccessCount,
				this.FailedCount,
				(ex != null) ? ex.ToString() : "<none>"
			}), null, "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\DistributedAction.cs", 233);
			return this.isCompleted;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005EC4 File Offset: 0x000040C4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DistributedAction>(this);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005ECC File Offset: 0x000040CC
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				lock (this.locker)
				{
					this.cancellationTokenSource.Dispose();
				}
			}
		}

		// Token: 0x040000D6 RID: 214
		private readonly int votesRequired;

		// Token: 0x040000D7 RID: 215
		private readonly bool isSyncMode;

		// Token: 0x040000D8 RID: 216
		private object locker = new object();

		// Token: 0x040000D9 RID: 217
		private string[] resources;

		// Token: 0x040000DA RID: 218
		private bool isCompleted;

		// Token: 0x040000DB RID: 219
		private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

		// Token: 0x040000DC RID: 220
		private TracingContext traceContext = TracingContext.Default;
	}
}
