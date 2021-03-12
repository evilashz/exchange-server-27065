using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001A2 RID: 418
	internal class PlayOnPhoneNotificationHandler : DisposeTrackableBase
	{
		// Token: 0x06000F14 RID: 3860 RVA: 0x0003A9D6 File Offset: 0x00038BD6
		public PlayOnPhoneNotificationHandler(UserContext userContext)
		{
			this.notifier = this.CreateNotifier(userContext);
			this.stateProvider = this.CreateStateProvider(userContext);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0003A9F8 File Offset: 0x00038BF8
		public void Subscribe(string subscriptionId, string callId)
		{
			lock (this)
			{
				this.subscriptionId = subscriptionId;
				this.callId = callId;
				if (this.timer == null)
				{
					this.timer = new Timer(new TimerCallback(this.TimerCallback), null, 0, 2000);
				}
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0003AA64 File Offset: 0x00038C64
		protected override void InternalDispose(bool disposing)
		{
			lock (this)
			{
				if (this.stateProvider != null)
				{
					this.stateProvider.Dispose();
					this.stateProvider = null;
				}
				if (this.timer != null)
				{
					this.timer.Dispose();
					this.timer = null;
				}
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0003AAD0 File Offset: 0x00038CD0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PlayOnPhoneNotificationHandler>(this);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0003AAD8 File Offset: 0x00038CD8
		protected virtual PlayOnPhoneNotifier CreateNotifier(UserContext userContext)
		{
			PlayOnPhoneNotifier playOnPhoneNotifier = new PlayOnPhoneNotifier(userContext);
			playOnPhoneNotifier.RegisterWithPendingRequestNotifier();
			return playOnPhoneNotifier;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0003AAF3 File Offset: 0x00038CF3
		protected virtual IPlayOnPhoneStateProvider CreateStateProvider(UserContext userContext)
		{
			return new PlayOnPhoneStateProvider(userContext);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0003AAFC File Offset: 0x00038CFC
		private void TimerCallback(object state)
		{
			if (this.timer == null || this.processingTimer)
			{
				return;
			}
			lock (this)
			{
				this.processingTimer = true;
				try
				{
					UMCallState callState = this.stateProvider.GetCallState(this.callId);
					if (this.previousState == null || this.previousState.Value != callState)
					{
						PlayOnPhoneNotificationPayload playOnPhoneNotificationPayload = new PlayOnPhoneNotificationPayload(callState.ToString());
						playOnPhoneNotificationPayload.SubscriptionId = this.subscriptionId;
						playOnPhoneNotificationPayload.Source = new TypeLocation(base.GetType());
						this.notifier.NotifyStateChange(playOnPhoneNotificationPayload);
						this.previousState = new UMCallState?(callState);
					}
					if (callState == UMCallState.Disconnected)
					{
						this.timer.Dispose();
						this.timer = null;
						this.subscriptionId = null;
						this.callId = null;
						this.previousState = null;
					}
				}
				finally
				{
					this.processingTimer = false;
				}
			}
		}

		// Token: 0x0400091F RID: 2335
		private PlayOnPhoneNotifier notifier;

		// Token: 0x04000920 RID: 2336
		private IPlayOnPhoneStateProvider stateProvider;

		// Token: 0x04000921 RID: 2337
		private UMCallState? previousState;

		// Token: 0x04000922 RID: 2338
		private bool processingTimer;

		// Token: 0x04000923 RID: 2339
		private Timer timer;

		// Token: 0x04000924 RID: 2340
		private string subscriptionId;

		// Token: 0x04000925 RID: 2341
		private string callId;
	}
}
