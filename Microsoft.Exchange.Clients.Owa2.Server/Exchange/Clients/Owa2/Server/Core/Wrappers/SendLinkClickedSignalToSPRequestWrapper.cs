using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002AE RID: 686
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SendLinkClickedSignalToSPRequestWrapper
	{
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x000540E2 File Offset: 0x000522E2
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x000540EA File Offset: 0x000522EA
		[DataMember(Name = "sendLinkClickedRequest")]
		public SendLinkClickedSignalToSPRequest SendLinkClickedRequest { get; set; }
	}
}
