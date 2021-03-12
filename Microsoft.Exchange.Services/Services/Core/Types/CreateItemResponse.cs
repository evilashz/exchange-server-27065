using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004BE RID: 1214
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("CreateItemResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateItemResponse : ItemInfoResponse
	{
		// Token: 0x060023F8 RID: 9208 RVA: 0x000A4646 File Offset: 0x000A2846
		public CreateItemResponse() : base(ResponseType.CreateItemResponseMessage)
		{
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x000A464F File Offset: 0x000A284F
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new ItemInfoResponseMessage(code, error, value as ItemType[]);
		}
	}
}
