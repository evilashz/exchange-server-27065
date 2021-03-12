using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200028D RID: 653
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPersonaNotesRequestWrapper
	{
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x00053CCC File Offset: 0x00051ECC
		// (set) Token: 0x0600178B RID: 6027 RVA: 0x00053CD4 File Offset: 0x00051ED4
		[DataMember(Name = "personaId")]
		public string PersonaId { get; set; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x00053CDD File Offset: 0x00051EDD
		// (set) Token: 0x0600178D RID: 6029 RVA: 0x00053CE5 File Offset: 0x00051EE5
		[DataMember(Name = "maxBytesToFetch")]
		public int MaxBytesToFetch { get; set; }
	}
}
