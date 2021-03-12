using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001AA RID: 426
	internal class ReminderNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000F39 RID: 3897 RVA: 0x0003B0A0 File Offset: 0x000392A0
		internal ReminderNotifier(string subscriptionId, IMailboxContext userContext) : base(subscriptionId, userContext)
		{
			this.refreshAll = false;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0003B0B1 File Offset: 0x000392B1
		public void Clear(bool clearRefreshPayload)
		{
			this.shouldGetRemindersPayload = null;
			if (clearRefreshPayload)
			{
				this.refreshAll = false;
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0003B0C4 File Offset: 0x000392C4
		internal void AddGetRemindersPayload(ReminderNotificationPayload payload)
		{
			lock (this)
			{
				if (!this.refreshAll)
				{
					this.shouldGetRemindersPayload = payload;
				}
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003B10C File Offset: 0x0003930C
		internal void AddRefreshPayload()
		{
			lock (this)
			{
				this.Clear(false);
				if (!this.refreshAll)
				{
					this.refreshAll = true;
				}
			}
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0003B158 File Offset: 0x00039358
		protected override IList<NotificationPayloadBase> ReadDataAndResetStateInternal()
		{
			List<NotificationPayloadBase> list = new List<NotificationPayloadBase>();
			if (this.refreshAll)
			{
				list.Add(new ReminderNotificationPayload
				{
					Reload = true,
					Source = MailboxLocation.FromMailboxContext(base.UserContext)
				});
			}
			else if (this.shouldGetRemindersPayload != null)
			{
				list.Add(this.shouldGetRemindersPayload);
			}
			this.Clear(true);
			return list;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0003B1B6 File Offset: 0x000393B6
		protected override bool IsDataAvailableForPickup()
		{
			return this.shouldGetRemindersPayload != null || this.refreshAll;
		}

		// Token: 0x04000933 RID: 2355
		private bool refreshAll;

		// Token: 0x04000934 RID: 2356
		private ReminderNotificationPayload shouldGetRemindersPayload;
	}
}
