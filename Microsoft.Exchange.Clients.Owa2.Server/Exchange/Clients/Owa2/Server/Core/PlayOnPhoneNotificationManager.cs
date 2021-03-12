using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001A3 RID: 419
	internal sealed class PlayOnPhoneNotificationManager : DisposeTrackableBase
	{
		// Token: 0x06000F1B RID: 3867 RVA: 0x0003AC04 File Offset: 0x00038E04
		internal PlayOnPhoneNotificationManager(UserContext userContext)
		{
			this.userContext = userContext;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0003AC14 File Offset: 0x00038E14
		public void SubscribeToPlayOnPhoneNotification(string subscriptionId, SubscriptionParameters parameters)
		{
			if (this.popHandler == null)
			{
				this.popHandler = new PlayOnPhoneNotificationHandler(this.userContext);
			}
			string callId = parameters.CallId;
			this.popHandler.Subscribe(subscriptionId, callId);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0003AC4E File Offset: 0x00038E4E
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "PlayOnPhoneNotificationManager.Dispose. IsDisposing: {0}", isDisposing);
			if (isDisposing && this.popHandler != null)
			{
				this.popHandler.Dispose();
				this.popHandler = null;
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003AC84 File Offset: 0x00038E84
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PlayOnPhoneNotificationManager>(this);
		}

		// Token: 0x04000926 RID: 2342
		private const string PopCallId = "callid";

		// Token: 0x04000927 RID: 2343
		private UserContext userContext;

		// Token: 0x04000928 RID: 2344
		private PlayOnPhoneNotificationHandler popHandler;
	}
}
