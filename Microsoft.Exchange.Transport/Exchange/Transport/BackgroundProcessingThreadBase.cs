using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000008 RID: 8
	internal abstract class BackgroundProcessingThreadBase
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002494 File Offset: 0x00000694
		protected BackgroundProcessingThreadBase() : this(TimeSpan.FromMilliseconds(100.0))
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024AC File Offset: 0x000006AC
		protected BackgroundProcessingThreadBase(TimeSpan interval)
		{
			if (interval < TimeSpan.FromMilliseconds(100.0))
			{
				throw new ArgumentException("Background thread interval must be greater than or equal to 100ms.");
			}
			this.interval = interval;
			this.hangDetectionInterval = Components.TransportAppConfig.WorkerProcess.BackgroundProcessingThreadHangDetectionToleranceInterval + this.interval;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000252C File Offset: 0x0000072C
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
			this.startupServiceState = new ServiceState?(targetRunningState);
			this.backgroundThread = new Thread(new ThreadStart(this.Run));
			this.backgroundThread.Start();
			this.lastProcessingTime = DateTime.UtcNow;
			Thread thread = new Thread(new ThreadStart(this.DetectBackgroundProcessingThreadHang));
			thread.Start();
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Background processing thread started");
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000259C File Offset: 0x0000079C
		public virtual void Stop()
		{
			if (this.backgroundThread != null)
			{
				this.hangDetectionThreadShutdownEvent.Set();
				this.backgroundShutdownEvent.Set();
				this.backgroundThread.Join();
				this.backgroundThread = null;
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Background processing thread stopped");
				this.backgroundShutdownEvent.Reset();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000025F8 File Offset: 0x000007F8
		protected virtual void Run()
		{
			WaitHandle[] waitHandles = new WaitHandle[]
			{
				this.backgroundShutdownEvent
			};
			int num;
			for (;;)
			{
				num = WaitHandle.WaitAny(waitHandles, this.interval, false);
				if (num == 0)
				{
					break;
				}
				if (num != 258)
				{
					goto IL_44;
				}
				this.lastProcessingTime = DateTime.UtcNow;
				this.RunMain(DateTime.UtcNow);
			}
			return;
			IL_44:
			throw new InvalidOperationException("Unexpected WaitHandle index: " + num.ToString());
		}

		// Token: 0x06000032 RID: 50
		protected abstract void RunMain(DateTime utcNow);

		// Token: 0x06000033 RID: 51 RVA: 0x00002660 File Offset: 0x00000860
		private void DetectBackgroundProcessingThreadHang()
		{
			while (!this.hangDetectionThreadShutdownEvent.WaitOne(this.hangDetectionInterval))
			{
				TimeSpan timeSpan = DateTime.UtcNow - this.lastProcessingTime;
				if (timeSpan > this.hangDetectionInterval)
				{
					throw new BackgroundProcessingThreadBase.BackgroundProcessingThreadBlockedException("Background processing is Hung for " + timeSpan);
				}
			}
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Exiting DetectBackgroundProcessingThread Hang thread");
		}

		// Token: 0x04000008 RID: 8
		private readonly TimeSpan interval;

		// Token: 0x04000009 RID: 9
		private readonly TimeSpan hangDetectionInterval;

		// Token: 0x0400000A RID: 10
		private Thread backgroundThread;

		// Token: 0x0400000B RID: 11
		private AutoResetEvent backgroundShutdownEvent = new AutoResetEvent(false);

		// Token: 0x0400000C RID: 12
		private AutoResetEvent hangDetectionThreadShutdownEvent = new AutoResetEvent(false);

		// Token: 0x0400000D RID: 13
		private DateTime lastProcessingTime;

		// Token: 0x0400000E RID: 14
		protected ServiceState? startupServiceState = null;

		// Token: 0x02000009 RID: 9
		private class BackgroundProcessingThreadBlockedException : Exception
		{
			// Token: 0x06000034 RID: 52 RVA: 0x000026C6 File Offset: 0x000008C6
			public BackgroundProcessingThreadBlockedException(string message) : base(message)
			{
			}
		}
	}
}
