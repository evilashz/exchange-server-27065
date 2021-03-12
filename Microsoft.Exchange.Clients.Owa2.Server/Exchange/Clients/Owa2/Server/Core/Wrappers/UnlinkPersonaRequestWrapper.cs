using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002CA RID: 714
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UnlinkPersonaRequestWrapper
	{
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x00054415 File Offset: 0x00052615
		// (set) Token: 0x0600186A RID: 6250 RVA: 0x0005441D File Offset: 0x0005261D
		[DataMember(Name = "personaId")]
		public ItemId PersonaId { get; set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x00054426 File Offset: 0x00052626
		// (set) Token: 0x0600186C RID: 6252 RVA: 0x0005442E File Offset: 0x0005262E
		[DataMember(Name = "contactId")]
		public ItemId ContactId { get; set; }
	}
}
