using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000D0 RID: 208
	internal abstract class ScheduledActionBase : DisposeTrackableBase
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x0003C399 File Offset: 0x0003A599
		public ScheduledActionBase(ExDateTime expectedTime)
		{
			this.ExpectedTime = expectedTime;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0003C3A8 File Offset: 0x0003A5A8
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x0003C3B0 File Offset: 0x0003A5B0
		public ExDateTime ExpectedTime { get; protected set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0003C3B9 File Offset: 0x0003A5B9
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x0003C3C1 File Offset: 0x0003A5C1
		public bool Performed { get; private set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0003C3CA File Offset: 0x0003A5CA
		protected bool DisposalRequested
		{
			get
			{
				return 1 == this.disposalRequested;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0003C3D5 File Offset: 0x0003A5D5
		protected virtual string SourceDescription
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0003C3DC File Offset: 0x0003A5DC
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x0003C3E4 File Offset: 0x0003A5E4
		private Timer Timer { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0003C3ED File Offset: 0x0003A5ED
		private long TimerCookie
		{
			get
			{
				return this.timerCookie;
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0003C3F5 File Offset: 0x0003A5F5
		public bool Schedule()
		{
			return !this.DisposalRequested && this.Reschedule(this.ExpectedTime);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0003C410 File Offset: 0x0003A610
		public bool Dismiss()
		{
			if (this.DisposalRequested)
			{
				return false;
			}
			ManualResetEvent disposed = null;
			lock (this)
			{
				this.GenerateNewTimerCookie();
				disposed = this.DisposeTimer();
			}
			this.WaitForTimerDisposal(disposed);
			return !this.Performed;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0003C470 File Offset: 0x0003A670
		public bool Reschedule(ExDateTime expectedTime)
		{
			if (this.DisposalRequested)
			{
				return false;
			}
			if (this.Dismiss())
			{
				lock (this)
				{
					this.ExpectedTime = expectedTime;
					TimeSpan dueTime = ScheduledActionBase.GetDueTime(this.ExpectedTime);
					bool flag2 = dueTime <= ScheduledActionBase.MaxTimerDueTime;
					this.Timer = new Timer(flag2 ? new TimerCallback(this.OnTimer) : new TimerCallback(this.OnSpool), this.TimerCookie, flag2 ? dueTime : ScheduledActionBase.MaxTimerDueTime, TimeSpan.FromMilliseconds(-1.0));
				}
				return true;
			}
			return false;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0003C528 File Offset: 0x0003A728
		protected virtual void OnPerforming(long cookie)
		{
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0003C52A File Offset: 0x0003A72A
		protected virtual void OnPerformed(long cookie)
		{
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0003C52C File Offset: 0x0003A72C
		protected bool ShouldContinue(long cookie)
		{
			return cookie == this.TimerCookie && !this.DisposalRequested;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0003C544 File Offset: 0x0003A744
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && Interlocked.CompareExchange(ref this.disposalRequested, 1, 0) == 0)
			{
				ManualResetEvent disposed = this.DisposeTimer();
				this.WaitForTimerDisposal(disposed);
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0003C571 File Offset: 0x0003A771
		private long GenerateNewTimerCookie()
		{
			return Interlocked.Increment(ref this.timerCookie);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0003C580 File Offset: 0x0003A780
		private ManualResetEvent DisposeTimer()
		{
			ManualResetEvent manualResetEvent = new ManualResetEvent(false);
			if (this.Timer != null)
			{
				this.Timer.Change(-1, -1);
				Timer timer = this.Timer;
				this.Timer = null;
				timer.Dispose(manualResetEvent);
			}
			else
			{
				manualResetEvent.Set();
			}
			return manualResetEvent;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0003C5CC File Offset: 0x0003A7CC
		private void WaitForTimerDisposal(ManualResetEvent disposed)
		{
			bool flag = false;
			try
			{
				if (258 == WaitHandle.WaitAny(new WaitHandle[]
				{
					disposed
				}, 30000, false))
				{
					flag = true;
					string message = string.Format("timeout to dispose action in type of {0}. this is probably caused by a deadlock. it need investigation", base.GetType().Name);
					ExTraceGlobals.AssistantTracer.TraceDebug((long)this.GetHashCode(), message);
				}
			}
			finally
			{
				if (!flag)
				{
					ExTraceGlobals.AssistantTracer.TraceDebug((long)this.GetHashCode(), "Disposing event, didn't time out waiting for timer disposal");
					disposed.Dispose();
					disposed = null;
				}
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0003C658 File Offset: 0x0003A858
		private static TimeSpan GetDueTime(ExDateTime expectedTime)
		{
			TimeSpan timeSpan = expectedTime - ExDateTime.Now;
			if (TimeSpan.Zero > timeSpan)
			{
				timeSpan = TimeSpan.Zero;
			}
			return timeSpan;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0003C688 File Offset: 0x0003A888
		private void OnSpool(object state)
		{
			bool flag = false;
			long cookie = (long)state;
			lock (this)
			{
				if (!this.ShouldContinue(cookie))
				{
					return;
				}
				TimeSpan dueTime = ScheduledActionBase.GetDueTime(this.ExpectedTime);
				if (!(flag = (TimeSpan.Zero == dueTime)))
				{
					this.Timer.Change((dueTime <= ScheduledActionBase.MaxTimerDueTime) ? dueTime : ScheduledActionBase.MaxTimerDueTime, TimeSpan.FromMilliseconds(-1.0));
				}
			}
			if (flag)
			{
				if (!this.ShouldContinue(cookie))
				{
					return;
				}
				this.OnTimer(state);
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0003C734 File Offset: 0x0003A934
		private void OnTimer(object state)
		{
			long cookie = (long)state;
			if (!this.ShouldContinue(cookie))
			{
				return;
			}
			try
			{
				try
				{
					this.OnPerforming(cookie);
				}
				finally
				{
					this.Performed = true;
					this.OnPerformed(cookie);
				}
			}
			catch (Exception ex)
			{
				if (!CalendarNotificationAssistant.TryHandleException((long)this.GetHashCode(), "performing timer call back", this.SourceDescription ?? "unknown", ex))
				{
					throw;
				}
			}
		}

		// Token: 0x04000617 RID: 1559
		private const int DisposeIsRunningOrHasRun = 1;

		// Token: 0x04000618 RID: 1560
		private const int DisposeIsNotRunningOrHasNeverRun = 0;

		// Token: 0x04000619 RID: 1561
		private const int TimerDisposingTimeout = 30000;

		// Token: 0x0400061A RID: 1562
		private static readonly TimeSpan MaxTimerDueTime = TimeSpan.FromMilliseconds(4294967293.0);

		// Token: 0x0400061B RID: 1563
		private int disposalRequested;

		// Token: 0x0400061C RID: 1564
		private long timerCookie;
	}
}
