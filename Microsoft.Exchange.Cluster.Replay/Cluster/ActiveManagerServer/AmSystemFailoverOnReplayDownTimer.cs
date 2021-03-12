using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200008D RID: 141
	internal class AmSystemFailoverOnReplayDownTimer : TimerComponent
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x0001AF49 File Offset: 0x00019149
		internal AmSystemFailoverOnReplayDownTimer(AmServerName serverName, TimeSpan startDelay) : base(startDelay, TimeSpan.FromMilliseconds(-1.0), "SystemFailoverOnReplayDownTimer")
		{
			this.serverName = serverName;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001AF88 File Offset: 0x00019188
		protected override void TimerCallbackInternal()
		{
			bool flag = true;
			Exception ex = null;
			AmSystemFailoverOnReplayDownTracker failoverTracker = AmSystemManager.Instance.SystemFailoverOnReplayDownTracker;
			if (failoverTracker != null)
			{
				try
				{
					ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
					{
						this.Run(failoverTracker);
					});
					flag = false;
				}
				finally
				{
					failoverTracker.RemoveTimer(this.serverName);
					if (flag || ex != null)
					{
						ReplayCrimsonEvents.FailoverOnReplDownFailedToInitiate.Log<AmServerName, string>(this.serverName, (ex != null) ? ex.Message : "<Unhandled exception>");
					}
				}
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001B024 File Offset: 0x00019224
		private void Run(AmSystemFailoverOnReplayDownTracker failoverTracker)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (!config.IsPAM)
			{
				ReplayCrimsonEvents.FailoverOnReplDownSkipped.Log<AmServerName, string, string>(this.serverName, "RoleNotPAM", "TimerCallback");
				return;
			}
			if (AmHelper.IsReplayRunning(this.serverName))
			{
				ReplayCrimsonEvents.FailoverOnReplDownSkipped.Log<AmServerName, string, string>(this.serverName, "ReplRunning", "TimerCallback");
				return;
			}
			AmThrottledActionTracker<FailoverData>.ThrottlingShapshot throttlingSnapshot = failoverTracker.GetThrottlingSnapshot(this.serverName);
			if (throttlingSnapshot.IsActionCalledTooSoonPerNode || throttlingSnapshot.IsActionCalledTooSoonAcrossDag || throttlingSnapshot.IsMaxActionsPerNodeExceeded || throttlingSnapshot.IsMaxActionsAcrossDagExceeded)
			{
				throttlingSnapshot.LogResults(ReplayCrimsonEvents.FailoverOnReplDownThrottlingFailed, TimeSpan.FromMinutes(15.0));
				return;
			}
			failoverTracker.AddFailoverEntry(this.serverName);
			throttlingSnapshot.LogResults(ReplayCrimsonEvents.FailoverOnReplDownThrottlingSucceeded, TimeSpan.Zero);
			AmEvtSystemFailoverOnReplayDown amEvtSystemFailoverOnReplayDown = new AmEvtSystemFailoverOnReplayDown(this.serverName);
			amEvtSystemFailoverOnReplayDown.Notify();
		}

		// Token: 0x04000232 RID: 562
		private readonly AmServerName serverName;
	}
}
