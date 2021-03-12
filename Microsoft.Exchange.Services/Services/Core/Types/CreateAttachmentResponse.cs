using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004BB RID: 1211
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("CreateAttachmentResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateAttachmentResponse : AttachmentInfoResponse
	{
		// Token: 0x060023F3 RID: 9203 RVA: 0x000A45F7 File Offset: 0x000A27F7
		public CreateAttachmentResponse() : base(ResponseType.CreateAttachmentResponseMessage)
		{
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000A4620 File Offset: 0x000A2820
		internal void AddResponses(ServiceResult<AttachmentType>[] serviceResults)
		{
			ServiceResult<AttachmentType>.ProcessServiceResults(serviceResults, delegate(ServiceResult<AttachmentType> result)
			{
				base.AddResponse(new AttachmentInfoResponseMessage(result.Code, result.Error, result.Value));
			});
		}
	}
}
