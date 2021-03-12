using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000100 RID: 256
	[DataContract]
	[KnownType(typeof(PolicySyncPermanentFault))]
	public class PolicySyncPermanentFaultResult
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00014C72 File Offset: 0x00012E72
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00014C7A File Offset: 0x00012E7A
		[DataMember]
		public PolicySyncPermanentFault Detail { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00014C83 File Offset: 0x00012E83
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x00014C8B File Offset: 0x00012E8B
		[DataMember]
		public string FaultType { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00014C94 File Offset: 0x00012E94
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x00014C9C File Offset: 0x00012E9C
		[DataMember]
		public string Message { get; set; }
	}
}
