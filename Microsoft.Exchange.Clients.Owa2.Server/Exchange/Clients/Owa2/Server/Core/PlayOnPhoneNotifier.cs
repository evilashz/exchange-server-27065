using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001A5 RID: 421
	internal class PlayOnPhoneNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000F23 RID: 3875 RVA: 0x0003ACB4 File Offset: 0x00038EB4
		public PlayOnPhoneNotifier(UserContext callContext) : base(callContext)
		{
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003ACC8 File Offset: 0x00038EC8
		public void ConnectionAliveTimer()
		{
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003ACCC File Offset: 0x00038ECC
		public override IList<NotificationPayloadBase> ReadDataAndResetState()
		{
			List<NotificationPayloadBase> list = new List<NotificationPayloadBase>();
			lock (this)
			{
				foreach (NotificationPayloadBase item in this.payloadList)
				{
					list.Add(item);
				}
				this.payloadList.Clear();
			}
			return list;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0003AD58 File Offset: 0x00038F58
		internal virtual void NotifyStateChange(PlayOnPhoneNotificationPayload payload)
		{
			lock (this)
			{
				this.payloadList.Add(payload);
				base.FireDataAvailableEvent();
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0003ADA0 File Offset: 0x00038FA0
		protected override IList<NotificationPayloadBase> ReadDataAndResetStateInternal()
		{
			return null;
		}

		// Token: 0x0400092A RID: 2346
		private List<NotificationPayloadBase> payloadList = new List<NotificationPayloadBase>();
	}
}
