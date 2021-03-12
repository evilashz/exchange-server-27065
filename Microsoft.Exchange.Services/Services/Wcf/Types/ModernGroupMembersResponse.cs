using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009EB RID: 2539
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ModernGroupMembersResponse
	{
		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x060047BE RID: 18366 RVA: 0x00100796 File Offset: 0x000FE996
		// (set) Token: 0x060047BF RID: 18367 RVA: 0x0010079E File Offset: 0x000FE99E
		[DataMember]
		public ModernGroupMemberType[] Members { get; set; }

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x060047C0 RID: 18368 RVA: 0x001007A7 File Offset: 0x000FE9A7
		// (set) Token: 0x060047C1 RID: 18369 RVA: 0x001007AF File Offset: 0x000FE9AF
		[DataMember]
		public int Count { get; set; }

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x060047C2 RID: 18370 RVA: 0x001007B8 File Offset: 0x000FE9B8
		// (set) Token: 0x060047C3 RID: 18371 RVA: 0x001007C0 File Offset: 0x000FE9C0
		[DataMember]
		public bool HasMoreMembers { get; set; }
	}
}
