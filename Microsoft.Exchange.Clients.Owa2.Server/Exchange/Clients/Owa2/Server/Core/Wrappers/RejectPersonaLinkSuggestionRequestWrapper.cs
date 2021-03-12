using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002A1 RID: 673
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RejectPersonaLinkSuggestionRequestWrapper
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00053F48 File Offset: 0x00052148
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x00053F50 File Offset: 0x00052150
		[DataMember(Name = "personaId")]
		public ItemId PersonaId { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x00053F59 File Offset: 0x00052159
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x00053F61 File Offset: 0x00052161
		[DataMember(Name = "suggestedPersonaId")]
		public ItemId SuggestedPersonaId { get; set; }
	}
}
