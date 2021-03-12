using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002A3 RID: 675
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveFavoriteRequestWrapper
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x00053F9C File Offset: 0x0005219C
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x00053FA4 File Offset: 0x000521A4
		[DataMember(Name = "personaId")]
		public ItemId PersonaId { get; set; }
	}
}
