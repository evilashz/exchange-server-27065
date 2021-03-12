using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000536 RID: 1334
	[XmlType("PlayOnPhoneResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PlayOnPhoneResponseMessage : ResponseMessage
	{
		// Token: 0x060025FB RID: 9723 RVA: 0x000A61C9 File Offset: 0x000A43C9
		public PlayOnPhoneResponseMessage()
		{
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x000A61D1 File Offset: 0x000A43D1
		internal PlayOnPhoneResponseMessage(ServiceResultCode code, ServiceError error, PhoneCallId phoneCallId) : base(code, error)
		{
			this.PhoneCallId = phoneCallId;
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x000A61E2 File Offset: 0x000A43E2
		// (set) Token: 0x060025FE RID: 9726 RVA: 0x000A61EA File Offset: 0x000A43EA
		[XmlElement(ElementName = "PhoneCallId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "PhoneCallId", IsRequired = false, EmitDefaultValue = false)]
		public PhoneCallId PhoneCallId { get; set; }

		// Token: 0x060025FF RID: 9727 RVA: 0x000A61F3 File Offset: 0x000A43F3
		public override ResponseType GetResponseType()
		{
			return ResponseType.PlayOnPhoneResponseMessage;
		}
	}
}
