using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004B8 RID: 1208
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FolderInfoResponse : BaseInfoResponse
	{
		// Token: 0x060023EF RID: 9199 RVA: 0x000A45C8 File Offset: 0x000A27C8
		internal FolderInfoResponse(ResponseType responseType) : base(responseType)
		{
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000A45D1 File Offset: 0x000A27D1
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new FolderInfoResponseMessage(code, error, value as BaseFolderType);
		}
	}
}
