using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200057E RID: 1406
	[XmlType(TypeName = "UploadItemsResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class UploadItemsResponse : BaseInfoResponse
	{
		// Token: 0x06002716 RID: 10006 RVA: 0x000A6E4F File Offset: 0x000A504F
		public UploadItemsResponse() : base(ResponseType.UploadItemsResponseMessage)
		{
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000A6E59 File Offset: 0x000A5059
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new UploadItemsResponseMessage(code, error, value as XmlNode);
		}
	}
}
