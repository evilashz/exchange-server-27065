using System;
using Microsoft.Exchange.Data.PushNotifications;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000346 RID: 838
	internal class PublishO365Notification : ServiceCommand<IAsyncResult>
	{
		// Token: 0x06001B82 RID: 7042 RVA: 0x0006965F File Offset: 0x0006785F
		public PublishO365Notification(CallContext callContext, O365Notification notification, AsyncCallback asyncCallback, object asyncState) : base(callContext)
		{
			this.notification = notification;
			this.asyncCallback = asyncCallback;
			this.asyncState = asyncState;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00069680 File Offset: 0x00067880
		protected override IAsyncResult InternalExecute()
		{
			this.asyncResult = new ServiceAsyncResult<bool>();
			this.asyncResult.AsyncState = this.asyncState;
			this.asyncResult.AsyncCallback = this.asyncCallback;
			if (PushNotificationsCrimsonEvents.MonitoringO365Notification.IsEnabled(PushNotificationsCrimsonEvent.Provider) && this.notification.ChannelId.StartsWith("::AE82E53440744F2798C276818CE8BD5C::"))
			{
				PushNotificationsCrimsonEvents.MonitoringO365Notification.Log<string>(this.notification.Data);
			}
			this.asyncResult.Data = true;
			this.asyncResult.Complete(this);
			return this.asyncResult;
		}

		// Token: 0x04000F82 RID: 3970
		private O365Notification notification;

		// Token: 0x04000F83 RID: 3971
		private AsyncCallback asyncCallback;

		// Token: 0x04000F84 RID: 3972
		private object asyncState;

		// Token: 0x04000F85 RID: 3973
		private ServiceAsyncResult<bool> asyncResult;
	}
}
