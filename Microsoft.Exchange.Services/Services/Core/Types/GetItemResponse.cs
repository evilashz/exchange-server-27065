using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004FF RID: 1279
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetItemResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetItemResponse : ItemInfoResponse
	{
		// Token: 0x06002508 RID: 9480 RVA: 0x000A55DC File Offset: 0x000A37DC
		public GetItemResponse() : base(ResponseType.GetItemResponseMessage)
		{
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000A55E6 File Offset: 0x000A37E6
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new ItemInfoResponseMessage(code, error, value as ItemType[]);
		}
	}
}
