using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B21 RID: 2849
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernGroupsResponse
	{
		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x060050C8 RID: 20680 RVA: 0x00109F1C File Offset: 0x0010811C
		// (set) Token: 0x060050C9 RID: 20681 RVA: 0x00109F24 File Offset: 0x00108124
		[DataMember(Name = "JoinedGroups", IsRequired = true)]
		public ModernGroupType[] JoinedGroups { get; set; }

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x060050CA RID: 20682 RVA: 0x00109F2D File Offset: 0x0010812D
		// (set) Token: 0x060050CB RID: 20683 RVA: 0x00109F35 File Offset: 0x00108135
		[DataMember(Name = "PinnedGroups", IsRequired = true)]
		public ModernGroupType[] PinnedGroups { get; set; }

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x060050CC RID: 20684 RVA: 0x00109F3E File Offset: 0x0010813E
		// (set) Token: 0x060050CD RID: 20685 RVA: 0x00109F46 File Offset: 0x00108146
		[DataMember(Name = "RecommendedGroups", IsRequired = true)]
		public ModernGroupType[] RecommendedGroups { get; set; }

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x060050CE RID: 20686 RVA: 0x00109F4F File Offset: 0x0010814F
		// (set) Token: 0x060050CF RID: 20687 RVA: 0x00109F57 File Offset: 0x00108157
		[DataMember(Name = "IsModernGroupsAddressListPresent", IsRequired = true)]
		public bool IsModernGroupsAddressListPresent { get; set; }
	}
}
