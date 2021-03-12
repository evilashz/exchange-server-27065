using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000562 RID: 1378
	[XmlType("SubscribeToPushNotificationResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SubscribeToPushNotificationResponse : ResponseMessage
	{
		// Token: 0x0600268A RID: 9866 RVA: 0x000A6758 File Offset: 0x000A4958
		public SubscribeToPushNotificationResponse()
		{
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000A6760 File Offset: 0x000A4960
		internal SubscribeToPushNotificationResponse(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000A676A File Offset: 0x000A496A
		public override ResponseType GetResponseType()
		{
			return ResponseType.SubscribeToPushNotificationResponseMessage;
		}
	}
}
