using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000299 RID: 665
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LinkPersonaRequestWrapper
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x00053E2B File Offset: 0x0005202B
		// (set) Token: 0x060017B5 RID: 6069 RVA: 0x00053E33 File Offset: 0x00052033
		[DataMember(Name = "linkToPersonaId")]
		public ItemId LinkToPersonaId { get; set; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x00053E3C File Offset: 0x0005203C
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x00053E44 File Offset: 0x00052044
		[DataMember(Name = "personaIdToBeLinked")]
		public ItemId PersonaIdToBeLinked { get; set; }
	}
}
