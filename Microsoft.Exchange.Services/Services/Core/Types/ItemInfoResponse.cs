using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004B0 RID: 1200
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ItemInfoResponse : BaseInfoResponse
	{
		// Token: 0x060023CD RID: 9165 RVA: 0x000A438C File Offset: 0x000A258C
		internal ItemInfoResponse(ResponseType responseType) : base(responseType)
		{
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000A4395 File Offset: 0x000A2595
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new ItemInfoResponseMessage(code, error, value as ItemType);
		}
	}
}
