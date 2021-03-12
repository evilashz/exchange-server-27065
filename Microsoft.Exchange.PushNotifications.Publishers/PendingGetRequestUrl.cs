using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000BA RID: 186
	public class PendingGetRequestUrl
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x00013D93 File Offset: 0x00011F93
		public PendingGetRequestUrl(string baseUrl, int timeout, string subscriptionId, string unseenNotificationId)
		{
			this.Timeout = timeout;
			this.SubscriptionId = subscriptionId;
			this.UnseenNotificationId = unseenNotificationId;
			this.BaseUrl = baseUrl;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00013DB8 File Offset: 0x00011FB8
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00013DC0 File Offset: 0x00011FC0
		public string BaseUrl { get; private set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00013DC9 File Offset: 0x00011FC9
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00013DD1 File Offset: 0x00011FD1
		public int Timeout { get; private set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00013DDA File Offset: 0x00011FDA
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00013DE2 File Offset: 0x00011FE2
		public string SubscriptionId { get; private set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00013DEB File Offset: 0x00011FEB
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x00013DF3 File Offset: 0x00011FF3
		public string UnseenNotificationId { get; private set; }

		// Token: 0x06000633 RID: 1587 RVA: 0x00013DFC File Offset: 0x00011FFC
		public string Serialize()
		{
			Uri uri = new Uri(new Uri(this.BaseUrl), new Uri(string.Format("{0}/{1}?{2}", "PushNotifications", "mowapendingget.ashx", this.SerializedParameters()), UriKind.Relative));
			return uri.AbsoluteUri;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00013E40 File Offset: 0x00012040
		public override string ToString()
		{
			return this.Serialize();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00013E48 File Offset: 0x00012048
		private string SerializedParameters()
		{
			return string.Format("T={0}&S={1}&US={2}", this.Timeout, this.SubscriptionId, this.UnseenNotificationId);
		}

		// Token: 0x04000315 RID: 789
		public const string VirtualDirectory = "PushNotifications";

		// Token: 0x04000316 RID: 790
		public const string Page = "mowapendingget.ashx";
	}
}
