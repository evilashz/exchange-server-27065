using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Threading
{
	// Token: 0x02000B07 RID: 2823
	public sealed class GuardedTimer : IGuardedTimer, IDisposeTrackable, IDisposable
	{
		// Token: 0x06003CB7 RID: 15543 RVA: 0x0009E271 File Offset: 0x0009C471
		public GuardedTimer(TimerCallback timerCallback, object state, TimeSpan dueTime, TimeSpan period) : this(timerCallback, state, (long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds)
		{
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x0009E28C File Offset: 0x0009C48C
		public GuardedTimer(TimerCallback timerCallback)
		{
			this.syncRoot = new object();
			base..ctor();
			if (timerCallback == null)
			{
				throw new ArgumentNullException("timerCallback");
			}
			this.disposeTracker = ((IDisposeTrackable)this).GetDisposeTracker();
			this.timerCallback = timerCallback;
			this.period = -1L;
			using (ActivityContext.SuppressThreadScope())
			{
				this.timer = new Timer(new TimerCallback(this.TimerGuardedCallback), this, -1, -1);
			}
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x0009E310 File Offset: 0x0009C510
		public GuardedTimer(TimerCallback timerCallback, object state, int dueTime, int period) : this(timerCallback, state, (long)dueTime, (long)period)
		{
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x0009E320 File Offset: 0x0009C520
		public GuardedTimer(TimerCallback timerCallback, object state, long dueTime, long period)
		{
			this.syncRoot = new object();
			base..ctor();
			if (timerCallback == null)
			{
				throw new ArgumentNullException("timerCallback");
			}
			this.disposeTracker = ((IDisposeTrackable)this).GetDisposeTracker();
			this.timerCallback = timerCallback;
			this.period = period;
			using (ActivityContext.SuppressThreadScope())
			{
				this.timer = new Timer(new TimerCallback(this.TimerGuardedCallback), state, dueTime, period);
			}
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x0009E3A4 File Offset: 0x0009C5A4
		public GuardedTimer(TimerCallback timerCallback, object state, TimeSpan period) : this(timerCallback, state, period, period)
		{
		}

		// Token: 0x06003CBC RID: 15548 RVA: 0x0009E3B0 File Offset: 0x0009C5B0
		public bool Change(int dueTime, int period)
		{
			return this.Change((long)dueTime, (long)period);
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x0009E3BC File Offset: 0x0009C5BC
		public bool Change(long dueTime, long period)
		{
			bool result;
			using (ActivityContext.SuppressThreadScope())
			{
				if (this.timer != null && !this.disposing)
				{
					this.period = period;
					result = this.timer.Change(dueTime, period);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x0009E418 File Offset: 0x0009C618
		public bool Change(TimeSpan dueTime, TimeSpan period)
		{
			return this.Change((long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds);
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x0009E430 File Offset: 0x0009C630
		public void Pause()
		{
			using (ActivityContext.SuppressThreadScope())
			{
				this.ThrowIfDisposed();
				this.timer.Change(-1, -1);
			}
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x0009E474 File Offset: 0x0009C674
		public void Continue()
		{
			using (ActivityContext.SuppressThreadScope())
			{
				this.ThrowIfDisposed();
				if (!this.disposing)
				{
					this.timer.Change(this.period, this.period);
				}
			}
		}

		// Token: 0x06003CC1 RID: 15553 RVA: 0x0009E4CC File Offset: 0x0009C6CC
		public void Continue(TimeSpan dueTime, TimeSpan period)
		{
			using (ActivityContext.SuppressThreadScope())
			{
				this.ThrowIfDisposed();
				this.period = (long)period.TotalMilliseconds;
				if (!this.disposing)
				{
					this.timer.Change(dueTime, period);
				}
			}
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x0009E528 File Offset: 0x0009C728
		public override bool Equals(object obj)
		{
			this.ThrowIfDisposed();
			return this.timer.Equals(obj);
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x0009E53C File Offset: 0x0009C73C
		public override int GetHashCode()
		{
			this.ThrowIfDisposed();
			return this.timer.GetHashCode();
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x0009E54F File Offset: 0x0009C74F
		public override string ToString()
		{
			this.ThrowIfDisposed();
			return this.timer.ToString();
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x0009E564 File Offset: 0x0009C764
		public void Dispose(bool wait)
		{
			this.ThrowIfDisposed();
			this.guard = true;
			if (wait)
			{
				this.disposing = true;
				this.Pause();
				lock (this.syncRoot)
				{
					ManualResetEvent manualResetEvent = new ManualResetEvent(false);
					try
					{
						if (this.timer.Dispose(manualResetEvent) && !Environment.HasShutdownStarted)
						{
							manualResetEvent.WaitOne();
						}
					}
					finally
					{
						manualResetEvent.Close();
					}
					goto IL_71;
				}
			}
			this.timer.Dispose();
			IL_71:
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.timer = null;
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x0009E618 File Offset: 0x0009C818
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x0009E621 File Offset: 0x0009C821
		DisposeTracker IDisposeTrackable.GetDisposeTracker()
		{
			return DisposeTracker.Get<GuardedTimer>(this);
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x0009E629 File Offset: 0x0009C829
		void IDisposeTrackable.SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x0009E640 File Offset: 0x0009C840
		private void TimerGuardedCallback(object state)
		{
			try
			{
				if (Monitor.TryEnter(this.syncRoot) && !this.guard)
				{
					using (ActivityContext.SuppressThreadScope())
					{
						this.timerCallback(state);
					}
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.syncRoot))
				{
					Monitor.Exit(this.syncRoot);
				}
			}
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x0009E6B8 File Offset: 0x0009C8B8
		private void ThrowIfDisposed()
		{
			if (this.timer == null)
			{
				throw new ObjectDisposedException("Timer already disposed.");
			}
		}

		// Token: 0x04003554 RID: 13652
		private Timer timer;

		// Token: 0x04003555 RID: 13653
		private DisposeTracker disposeTracker;

		// Token: 0x04003556 RID: 13654
		private TimerCallback timerCallback;

		// Token: 0x04003557 RID: 13655
		private long period;

		// Token: 0x04003558 RID: 13656
		private bool guard;

		// Token: 0x04003559 RID: 13657
		private object syncRoot;

		// Token: 0x0400355A RID: 13658
		private bool disposing;
	}
}
