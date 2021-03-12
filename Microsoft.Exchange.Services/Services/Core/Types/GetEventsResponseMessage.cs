using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004F9 RID: 1273
	[XmlType("GetEventsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetEventsResponseMessage : ResponseMessage
	{
		// Token: 0x060024EF RID: 9455 RVA: 0x000A54EE File Offset: 0x000A36EE
		public GetEventsResponseMessage()
		{
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000A54F6 File Offset: 0x000A36F6
		internal GetEventsResponseMessage(ServiceResultCode code, ServiceError error, EwsNotificationType notification) : base(code, error)
		{
			this.Notification = notification;
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x000A5507 File Offset: 0x000A3707
		// (set) Token: 0x060024F2 RID: 9458 RVA: 0x000A550F File Offset: 0x000A370F
		[DataMember(Name = "Notification", IsRequired = true)]
		[XmlElement("Notification")]
		public EwsNotificationType Notification { get; set; }
	}
}
