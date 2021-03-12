using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200050D RID: 1293
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetPhoneCallInformationResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetPhoneCallInformationResponseMessage : ResponseMessage
	{
		// Token: 0x0600253B RID: 9531 RVA: 0x000A5820 File Offset: 0x000A3A20
		public GetPhoneCallInformationResponseMessage()
		{
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000A5828 File Offset: 0x000A3A28
		internal GetPhoneCallInformationResponseMessage(ServiceResultCode code, ServiceError error, GetPhoneCallInformationResponseMessage response) : base(code, error)
		{
			this.callInfo = null;
			if (response != null)
			{
				this.callInfo = response.callInfo;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x000A5848 File Offset: 0x000A3A48
		// (set) Token: 0x0600253E RID: 9534 RVA: 0x000A5850 File Offset: 0x000A3A50
		[XmlAnyElement]
		public XmlNode PhoneCallInformation
		{
			get
			{
				return this.callInfo;
			}
			set
			{
				this.callInfo = value;
			}
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000A5859 File Offset: 0x000A3A59
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetPhoneCallInformationResponseMessage;
		}

		// Token: 0x040015B3 RID: 5555
		private XmlNode callInfo;
	}
}
