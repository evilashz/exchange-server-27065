using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000550 RID: 1360
	[XmlType("SendNotificationResultType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SendNotificationResult
	{
		// Token: 0x0600264E RID: 9806 RVA: 0x000A651C File Offset: 0x000A471C
		public SendNotificationResult()
		{
			this.subscriptionStatusField = SubscriptionStatus.Invalid;
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x000A652B File Offset: 0x000A472B
		// (set) Token: 0x06002650 RID: 9808 RVA: 0x000A6533 File Offset: 0x000A4733
		[XmlElement("SubscriptionStatus", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SubscriptionStatus SubscriptionStatus
		{
			get
			{
				return this.subscriptionStatusField;
			}
			set
			{
				this.subscriptionStatusField = value;
			}
		}

		// Token: 0x040018A3 RID: 6307
		private SubscriptionStatus subscriptionStatusField;
	}
}
