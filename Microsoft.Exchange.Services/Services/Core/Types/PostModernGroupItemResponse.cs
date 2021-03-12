using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000B25 RID: 2853
	[XmlType(TypeName = "PostModernGroupItemResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PostModernGroupItemResponse : BaseInfoResponse
	{
		// Token: 0x060050D9 RID: 20697 RVA: 0x00109FB2 File Offset: 0x001081B2
		public PostModernGroupItemResponse() : base(ResponseType.PostModernGroupItemResponseMessage)
		{
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x00109FBF File Offset: 0x001081BF
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue item)
		{
			return new ItemInfoResponseMessage(code, error, item as ItemType[]);
		}
	}
}
