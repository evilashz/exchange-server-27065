using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000210 RID: 528
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetFlowConversationResponse
	{
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00048F6C File Offset: 0x0004716C
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x00048F74 File Offset: 0x00047174
		[DataMember]
		public FlowConversationItem[] Conversations { get; set; }
	}
}
