using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200003D RID: 61
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class UserNotificationRecipient : BasicNotificationRecipient
	{
		// Token: 0x06000176 RID: 374 RVA: 0x0000509D File Offset: 0x0000329D
		public UserNotificationRecipient(string appId, string deviceId) : base(appId, deviceId)
		{
		}
	}
}
