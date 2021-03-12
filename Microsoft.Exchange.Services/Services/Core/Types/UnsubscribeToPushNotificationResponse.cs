using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000571 RID: 1393
	[XmlType("UnsubscribeToPushNotificationResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UnsubscribeToPushNotificationResponse : ResponseMessage
	{
		// Token: 0x060026EA RID: 9962 RVA: 0x000A6BCA File Offset: 0x000A4DCA
		public UnsubscribeToPushNotificationResponse()
		{
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000A6BD2 File Offset: 0x000A4DD2
		internal UnsubscribeToPushNotificationResponse(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000A6BDC File Offset: 0x000A4DDC
		public override ResponseType GetResponseType()
		{
			return ResponseType.UnsubscribeToPushNotificationResponseMessage;
		}
	}
}
