using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200054E RID: 1358
	[XmlRoot(ElementName = "SendNotification", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlType("SendNotificationResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SendNotificationResponse : BaseInfoResponse
	{
		// Token: 0x06002648 RID: 9800 RVA: 0x000A64D4 File Offset: 0x000A46D4
		public SendNotificationResponse() : base(ResponseType.SendNotificationResponseMessage)
		{
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000A64DE File Offset: 0x000A46DE
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new SendNotificationResponseMessage(code, error, value as EwsNotificationType);
		}
	}
}
