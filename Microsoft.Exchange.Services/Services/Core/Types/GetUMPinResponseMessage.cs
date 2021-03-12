using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.UM.Rpc;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200051D RID: 1309
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetUMPinResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUMPinResponseMessage : ResponseMessage
	{
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x000A5C16 File Offset: 0x000A3E16
		// (set) Token: 0x06002592 RID: 9618 RVA: 0x000A5C1E File Offset: 0x000A3E1E
		[DataMember(Name = "PinInfo")]
		[XmlElement("PinInfo")]
		public PINInfo PinInfo { get; set; }

		// Token: 0x06002593 RID: 9619 RVA: 0x000A5C27 File Offset: 0x000A3E27
		public GetUMPinResponseMessage()
		{
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000A5C2F File Offset: 0x000A3E2F
		internal GetUMPinResponseMessage(ServiceResultCode code, ServiceError error, GetUMPinResponseMessage response) : base(code, error)
		{
			if (response != null)
			{
				this.PinInfo = response.PinInfo;
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000A5C48 File Offset: 0x000A3E48
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetUMPinResponseMessage;
		}
	}
}
