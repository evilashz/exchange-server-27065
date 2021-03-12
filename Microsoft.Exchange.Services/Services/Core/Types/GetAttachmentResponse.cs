using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004E9 RID: 1257
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetAttachmentResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetAttachmentResponse : AttachmentInfoResponse
	{
		// Token: 0x060024A4 RID: 9380 RVA: 0x000A508E File Offset: 0x000A328E
		public GetAttachmentResponse() : base(ResponseType.GetAttachmentResponseMessage)
		{
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000A50C7 File Offset: 0x000A32C7
		internal void BuildForGetAttachmentResults(ServiceResult<AttachmentInfoResponseMessage>[] serviceResults)
		{
			ServiceResult<AttachmentInfoResponseMessage>.ProcessServiceResults(serviceResults, delegate(ServiceResult<AttachmentInfoResponseMessage> serviceResult)
			{
				if (serviceResult.Value == null)
				{
					base.AddResponse(new AttachmentInfoResponseMessage(serviceResult.Code, serviceResult.Error, null));
					return;
				}
				base.AddResponse(serviceResult.Value);
			});
		}
	}
}
