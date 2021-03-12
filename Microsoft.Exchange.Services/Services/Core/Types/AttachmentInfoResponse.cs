using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004B2 RID: 1202
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AttachmentInfoResponse : BaseInfoResponse
	{
		// Token: 0x060023D0 RID: 9168 RVA: 0x000A43B6 File Offset: 0x000A25B6
		internal AttachmentInfoResponse(ResponseType responseType) : base(responseType)
		{
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000A43BF File Offset: 0x000A25BF
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new AttachmentInfoResponseMessage(code, error, value as AttachmentType);
		}
	}
}
