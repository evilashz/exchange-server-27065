using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200054A RID: 1354
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("SaveUMPinResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SaveUMPinResponseMessage : ResponseMessage
	{
		// Token: 0x0600263D RID: 9789 RVA: 0x000A6447 File Offset: 0x000A4647
		public SaveUMPinResponseMessage()
		{
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x000A644F File Offset: 0x000A464F
		internal SaveUMPinResponseMessage(ServiceResultCode code, ServiceError error, SaveUMPinResponseMessage response) : base(code, error)
		{
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000A6459 File Offset: 0x000A4659
		public override ResponseType GetResponseType()
		{
			return ResponseType.SaveUMPinResponseMessage;
		}
	}
}
