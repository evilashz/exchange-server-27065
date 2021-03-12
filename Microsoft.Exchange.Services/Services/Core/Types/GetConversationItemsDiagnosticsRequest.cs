using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000430 RID: 1072
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetConversationItemsDiagnosticsRequest : GetConversationItemsRequest
	{
		// Token: 0x06001F7C RID: 8060 RVA: 0x000A0DA2 File Offset: 0x0009EFA2
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetConversationItemsDiagnostics(callContext, this);
		}
	}
}
