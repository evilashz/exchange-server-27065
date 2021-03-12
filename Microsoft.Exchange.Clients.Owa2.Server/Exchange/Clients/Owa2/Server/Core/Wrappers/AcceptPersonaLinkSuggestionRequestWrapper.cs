using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200025A RID: 602
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AcceptPersonaLinkSuggestionRequestWrapper
	{
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0005366C File Offset: 0x0005186C
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x00053674 File Offset: 0x00051874
		[DataMember(Name = "linkToPersonaId")]
		public ItemId LinkToPersonaId { get; set; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0005367D File Offset: 0x0005187D
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x00053685 File Offset: 0x00051885
		[DataMember(Name = "suggestedPersonaId")]
		public ItemId SuggestedPersonaId { get; set; }
	}
}
