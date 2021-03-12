using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003BC RID: 956
	[DataContract(Name = "FindMembersInUnifiedGroupResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindMembersInUnifiedGroupResponse : BaseJsonResponse
	{
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x00076ACB File Offset: 0x00074CCB
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x00076AD3 File Offset: 0x00074CD3
		[DataMember(Name = "Members", IsRequired = true)]
		public ModernGroupMemberType[] Members { get; set; }

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x00076ADC File Offset: 0x00074CDC
		// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x00076AE4 File Offset: 0x00074CE4
		[DataMember(Name = "HasMoreMembers", IsRequired = true)]
		public bool HasMoreMembers { get; set; }
	}
}
