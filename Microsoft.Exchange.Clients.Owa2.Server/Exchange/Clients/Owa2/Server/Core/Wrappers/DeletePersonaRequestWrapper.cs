using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000275 RID: 629
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeletePersonaRequestWrapper
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x000539FD File Offset: 0x00051BFD
		// (set) Token: 0x06001735 RID: 5941 RVA: 0x00053A05 File Offset: 0x00051C05
		[DataMember(Name = "personaId")]
		public ItemId PersonaId { get; set; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x00053A0E File Offset: 0x00051C0E
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x00053A16 File Offset: 0x00051C16
		[DataMember(Name = "folderId")]
		public BaseFolderId FolderId { get; set; }
	}
}
