using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004F1 RID: 1265
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetConversationItemsDiagnosticsResponse : BaseInfoResponse
	{
		// Token: 0x060024C9 RID: 9417 RVA: 0x000A52C6 File Offset: 0x000A34C6
		public GetConversationItemsDiagnosticsResponse() : base(ResponseType.GetConversationItemsDiagnosticsResponseMessage)
		{
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000A52D0 File Offset: 0x000A34D0
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue item)
		{
			return new GetConversationItemsDiagnosticsResponseMessage(code, error, item as GetConversationItemsDiagnosticsResponseType);
		}
	}
}
