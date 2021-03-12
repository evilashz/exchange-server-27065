using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001B4 RID: 436
	internal class SearchNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000F7D RID: 3965 RVA: 0x0003C5EA File Offset: 0x0003A7EA
		internal SearchNotifier(IMailboxContext userContext) : base("SearchNotification", userContext)
		{
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0003C604 File Offset: 0x0003A804
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x0003C648 File Offset: 0x0003A848
		internal SearchNotificationPayload Payload
		{
			get
			{
				SearchNotificationPayload result;
				lock (this.lockObject)
				{
					result = this.payload;
				}
				return result;
			}
			set
			{
				lock (this.lockObject)
				{
					this.payload = value;
				}
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0003C68C File Offset: 0x0003A88C
		protected override IList<NotificationPayloadBase> ReadDataAndResetStateInternal()
		{
			List<NotificationPayloadBase> list = new List<NotificationPayloadBase>();
			lock (this.lockObject)
			{
				if (this.Payload != null)
				{
					list.Add(this.Payload);
					this.Payload = null;
				}
			}
			return list;
		}

		// Token: 0x0400094D RID: 2381
		public const string SearchId = "SearchNotification";

		// Token: 0x0400094E RID: 2382
		private SearchNotificationPayload payload;

		// Token: 0x0400094F RID: 2383
		private object lockObject = new object();
	}
}
