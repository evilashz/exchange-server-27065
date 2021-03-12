using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200054F RID: 1359
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("SendNotificationResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SendNotificationResponseMessage : ResponseMessage
	{
		// Token: 0x0600264A RID: 9802 RVA: 0x000A64F2 File Offset: 0x000A46F2
		public SendNotificationResponseMessage()
		{
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000A64FA File Offset: 0x000A46FA
		internal SendNotificationResponseMessage(ServiceResultCode code, ServiceError error, EwsNotificationType notification) : base(code, error)
		{
			this.Notification = notification;
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x000A650B File Offset: 0x000A470B
		// (set) Token: 0x0600264D RID: 9805 RVA: 0x000A6513 File Offset: 0x000A4713
		[DataMember(Name = "Notification", IsRequired = true)]
		[XmlElement("Notification")]
		public EwsNotificationType Notification { get; set; }
	}
}
