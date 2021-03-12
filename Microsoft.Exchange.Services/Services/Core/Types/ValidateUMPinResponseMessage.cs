using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.UM.Rpc;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000580 RID: 1408
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("ValidateUMPinResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ValidateUMPinResponseMessage : ResponseMessage
	{
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x000A6F14 File Offset: 0x000A5114
		// (set) Token: 0x0600271F RID: 10015 RVA: 0x000A6F1C File Offset: 0x000A511C
		[XmlElement("PinInfo")]
		[DataMember(Name = "PinInfo")]
		public PINInfo PinInfo { get; set; }

		// Token: 0x06002720 RID: 10016 RVA: 0x000A6F25 File Offset: 0x000A5125
		public ValidateUMPinResponseMessage()
		{
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x000A6F2D File Offset: 0x000A512D
		internal ValidateUMPinResponseMessage(ServiceResultCode code, ServiceError error, ValidateUMPinResponseMessage response) : base(code, error)
		{
			if (response != null)
			{
				this.PinInfo = response.PinInfo;
			}
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x000A6F46 File Offset: 0x000A5146
		public override ResponseType GetResponseType()
		{
			return ResponseType.ValidateUMPinResponseMessage;
		}
	}
}
