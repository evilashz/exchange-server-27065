using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004EC RID: 1260
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetClientAccessTokenResponseMessage : ResponseMessage
	{
		// Token: 0x060024AF RID: 9391 RVA: 0x000A5159 File Offset: 0x000A3359
		public GetClientAccessTokenResponseMessage()
		{
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000A5161 File Offset: 0x000A3361
		internal GetClientAccessTokenResponseMessage(ServiceResultCode code, ServiceError error, ClientAccessTokenResponseType token) : base(code, error)
		{
			this.Token = token;
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x000A5172 File Offset: 0x000A3372
		// (set) Token: 0x060024B2 RID: 9394 RVA: 0x000A517A File Offset: 0x000A337A
		[DataMember]
		[XmlElement(ElementName = "Token", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ClientAccessTokenResponseType Token { get; set; }
	}
}
