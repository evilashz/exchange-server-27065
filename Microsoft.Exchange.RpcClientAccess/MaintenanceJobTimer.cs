using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200001E RID: 30
	internal sealed class MaintenanceJobTimer : BaseObject
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00003B38 File Offset: 0x00001D38
		internal MaintenanceJobTimer(Action worker, Func<bool> isJobEnabled, TimeSpan maintenanceJobTimerCheckPeriod, TimeSpan initialDelay)
		{
			this.worker = worker;
			this.isJobEnabled = isJobEnabled;
			this.maintenanceJobTimerCheckPeriod = maintenanceJobTimerCheckPeriod;
			this.timer = new Timer(new TimerCallback(this.TimerCallback), null, -1, -1);
			this.timer.Change((int)initialDelay.TotalMilliseconds, -1);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003B9C File Offset: 0x00001D9C
		protected override void InternalDispose()
		{
			lock (this.timerLock)
			{
				this.timer.Dispose();
				this.timer = null;
			}
			base.InternalDispose();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003BF0 File Offset: 0x00001DF0
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MaintenanceJobTimer>(this);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003C94 File Offset: 0x00001E94
		private void TimerCallback(object context)
		{
			ExWatson.SendReportOnUnhandledException(delegate()
			{
				if (base.IsDisposed)
				{
					return;
				}
				lock (this.timerLock)
				{
					if (this.timer != null)
					{
						bool flag2 = this.isJobEnabled();
						try
						{
							if (flag2)
							{
								this.worker();
							}
						}
						finally
						{
							TimeSpan timeSpan = flag2 ? this.maintenanceJobTimerCheckPeriod : ConfigurationSchema.RegistryNotificationPollPeriod;
							this.timer.Change((int)timeSpan.TotalMilliseconds, -1);
						}
					}
				}
			});
		}

		// Token: 0x0400004C RID: 76
		private readonly Action worker;

		// Token: 0x0400004D RID: 77
		private readonly Func<bool> isJobEnabled;

		// Token: 0x0400004E RID: 78
		private readonly TimeSpan maintenanceJobTimerCheckPeriod;

		// Token: 0x0400004F RID: 79
		private readonly object timerLock = new object();

		// Token: 0x04000050 RID: 80
		private Timer timer;
	}
}
