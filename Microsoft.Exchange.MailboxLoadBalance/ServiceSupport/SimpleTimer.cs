using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.ServiceSupport
{
	// Token: 0x020000F8 RID: 248
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SimpleTimer : ITimer
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x0001501A File Offset: 0x0001321A
		private SimpleTimer(TimeSpan interval)
		{
			this.interval = interval;
			this.executionSignal = new ManualResetEventSlim();
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00015034 File Offset: 0x00013234
		public static ITimer CreateTimer(TimeSpan interval)
		{
			return SimpleTimer.Factory.Value(interval);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00015048 File Offset: 0x00013248
		public void SetAction(Action newAction, bool startExecution)
		{
			if (this.timer != null)
			{
				this.timer.Dispose();
			}
			this.action = newAction;
			this.timer = new Timer(new TimerCallback(this.RunAction), null, startExecution ? TimeSpan.Zero : this.interval, this.interval);
			if (startExecution)
			{
				this.WaitExecution();
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000150A6 File Offset: 0x000132A6
		public void WaitExecution()
		{
			this.executionSignal.Wait();
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000150B3 File Offset: 0x000132B3
		public void WaitExecution(TimeSpan timeout)
		{
			this.executionSignal.Wait(timeout);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000150C2 File Offset: 0x000132C2
		private static ITimer CreateSimpleTimer(TimeSpan interval)
		{
			return new SimpleTimer(interval);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000150CC File Offset: 0x000132CC
		private void RunAction(object state)
		{
			this.executionSignal.Reset();
			try
			{
				this.action();
			}
			finally
			{
				this.executionSignal.Set();
			}
		}

		// Token: 0x040002DF RID: 735
		internal static readonly Hookable<Func<TimeSpan, ITimer>> Factory = Hookable<Func<TimeSpan, ITimer>>.Create(true, new Func<TimeSpan, ITimer>(SimpleTimer.CreateSimpleTimer));

		// Token: 0x040002E0 RID: 736
		private readonly ManualResetEventSlim executionSignal;

		// Token: 0x040002E1 RID: 737
		private readonly TimeSpan interval;

		// Token: 0x040002E2 RID: 738
		private Action action;

		// Token: 0x040002E3 RID: 739
		private Timer timer;
	}
}
