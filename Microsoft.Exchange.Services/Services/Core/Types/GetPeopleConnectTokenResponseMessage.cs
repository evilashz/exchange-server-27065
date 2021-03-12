using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000509 RID: 1289
	[XmlType("GetPeopleConnectTokenResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Name = "GetPeopleConnectTokenResponseMessage", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public sealed class GetPeopleConnectTokenResponseMessage : ResponseMessage
	{
		// Token: 0x0600252C RID: 9516 RVA: 0x000A5781 File Offset: 0x000A3981
		public GetPeopleConnectTokenResponseMessage()
		{
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000A5789 File Offset: 0x000A3989
		internal GetPeopleConnectTokenResponseMessage(ServiceResultCode code, ServiceError error, PeopleConnectionToken result) : base(code, error)
		{
			this.PeopleConnectionToken = result;
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600252E RID: 9518 RVA: 0x000A579A File Offset: 0x000A399A
		// (set) Token: 0x0600252F RID: 9519 RVA: 0x000A57A2 File Offset: 0x000A39A2
		[XmlElement]
		[DataMember]
		public PeopleConnectionToken PeopleConnectionToken { get; set; }
	}
}
