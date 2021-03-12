using System;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000A8 RID: 168
	internal class SharedTimer
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x0000F00E File Offset: 0x0000D20E
		private SharedTimer()
		{
			this.internalTimer = new Timer(new TimerCallback(this.InternalCallback), null, 1000, -1);
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000F03F File Offset: 0x0000D23F
		internal static SharedTimer Instance
		{
			get
			{
				return SharedTimer.instance;
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000F048 File Offset: 0x0000D248
		internal void RegisterCallback(TimerCallback callback)
		{
			lock (this.sync)
			{
				this.callback = (TimerCallback)Delegate.Combine(this.callback, callback);
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000F09C File Offset: 0x0000D29C
		internal void UnRegisterCallback(TimerCallback callback)
		{
			lock (this.sync)
			{
				this.callback = (TimerCallback)Delegate.Remove(this.callback, callback);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
		private void InternalCallback(object arg)
		{
			TimerCallback timerCallback = null;
			lock (this.sync)
			{
				timerCallback = this.callback;
			}
			if (timerCallback != null)
			{
				timerCallback(null);
			}
			this.internalTimer.Change(1000, -1);
		}

		// Token: 0x0400033E RID: 830
		private const int TimerIntervalMilliseconds = 1000;

		// Token: 0x0400033F RID: 831
		private static SharedTimer instance = new SharedTimer();

		// Token: 0x04000340 RID: 832
		private Timer internalTimer;

		// Token: 0x04000341 RID: 833
		private TimerCallback callback;

		// Token: 0x04000342 RID: 834
		private object sync = new object();
	}
}
