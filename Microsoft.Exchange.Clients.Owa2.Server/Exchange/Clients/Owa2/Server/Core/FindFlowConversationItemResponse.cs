using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000211 RID: 529
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class FindFlowConversationItemResponse
	{
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x00048F85 File Offset: 0x00047185
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x00048F8D File Offset: 0x0004718D
		[DataMember]
		public FlowItem[] Items { get; set; }
	}
}
