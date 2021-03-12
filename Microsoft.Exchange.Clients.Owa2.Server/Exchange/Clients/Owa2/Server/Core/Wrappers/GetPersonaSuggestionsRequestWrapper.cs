using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200028F RID: 655
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPersonaSuggestionsRequestWrapper
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x00053D0F File Offset: 0x00051F0F
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x00053D17 File Offset: 0x00051F17
		[DataMember(Name = "personaId")]
		public ItemId PersonaId { get; set; }
	}
}
