using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004C6 RID: 1222
	[XmlType("DeleteAttachmentResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteAttachmentResponse : BaseInfoResponse
	{
		// Token: 0x0600240D RID: 9229 RVA: 0x000A470E File Offset: 0x000A290E
		public DeleteAttachmentResponse() : base(ResponseType.DeleteAttachmentResponseMessage)
		{
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000A4718 File Offset: 0x000A2918
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new DeleteAttachmentResponseMessage(code, error, value as RootItemIdType);
		}
	}
}
