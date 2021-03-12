using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004D0 RID: 1232
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("DisconnectPhoneCallResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class DisconnectPhoneCallResponseMessage : ResponseMessage
	{
		// Token: 0x06002425 RID: 9253 RVA: 0x000A4817 File Offset: 0x000A2A17
		public DisconnectPhoneCallResponseMessage()
		{
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000A481F File Offset: 0x000A2A1F
		internal DisconnectPhoneCallResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000A4829 File Offset: 0x000A2A29
		public override ResponseType GetResponseType()
		{
			return ResponseType.DisconnectPhoneCallResponseMessage;
		}
	}
}
