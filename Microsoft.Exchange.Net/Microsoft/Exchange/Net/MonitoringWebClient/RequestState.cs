using System;
using System.Threading;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007E7 RID: 2023
	internal class RequestState
	{
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002A54 RID: 10836 RVA: 0x0005BE08 File Offset: 0x0005A008
		public int State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06002A55 RID: 10837 RVA: 0x0005BE10 File Offset: 0x0005A010
		public bool IsCancelled
		{
			get
			{
				return this.state == 1;
			}
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x0005BE1B File Offset: 0x0005A01B
		public RequestState(TimerCallback timerCallback, object asyncState, int dueTime) : this(timerCallback, asyncState, dueTime, true)
		{
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x0005BE27 File Offset: 0x0005A027
		public RequestState(TimerCallback timerCallback, object asyncState, int dueTime, bool startTimer)
		{
			this.timerCallback = timerCallback;
			this.timerAsyncState = asyncState;
			this.dueTime = dueTime;
			if (startTimer)
			{
				this.StartTimer();
			}
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x0005BE4E File Offset: 0x0005A04E
		public void StartTimer()
		{
			this.timer = new Timer(this.timerCallback, this.timerAsyncState, this.dueTime, 0);
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x0005BE6E File Offset: 0x0005A06E
		public bool TryTransitionFromExecutingToTimedOut()
		{
			Interlocked.CompareExchange(ref this.state, 1, 0);
			this.DisposeTimer();
			return this.state == 1;
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x0005BE90 File Offset: 0x0005A090
		public bool TryTransitionFromExecutingToFinished()
		{
			Interlocked.CompareExchange(ref this.state, 2, 0);
			this.DisposeTimer();
			return this.state == 2;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x0005BEB2 File Offset: 0x0005A0B2
		public void Cancel()
		{
			if (Interlocked.CompareExchange(ref this.state, 1, 0) == 0)
			{
				this.DisposeTimer();
				this.timerCallback(this.timerAsyncState);
			}
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x0005BEDC File Offset: 0x0005A0DC
		private void DisposeTimer()
		{
			Timer timer = Interlocked.Exchange<Timer>(ref this.timer, null);
			if (timer != null)
			{
				timer.Dispose();
			}
		}

		// Token: 0x040024FE RID: 9470
		private int state;

		// Token: 0x040024FF RID: 9471
		private Timer timer;

		// Token: 0x04002500 RID: 9472
		private TimerCallback timerCallback;

		// Token: 0x04002501 RID: 9473
		private readonly int dueTime;

		// Token: 0x04002502 RID: 9474
		private object timerAsyncState;
	}
}
