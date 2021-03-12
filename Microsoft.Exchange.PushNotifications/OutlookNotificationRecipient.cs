using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200002E RID: 46
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class OutlookNotificationRecipient : BasicNotificationRecipient
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00004B0D File Offset: 0x00002D0D
		public OutlookNotificationRecipient(string appId, string deviceId) : base(appId, deviceId)
		{
		}
	}
}
