using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000101 RID: 257
	[KnownType(typeof(PolicySyncPermanentFault))]
	[DataContract]
	public class PolicySyncTransientFaultResult
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00014CAD File Offset: 0x00012EAD
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x00014CB5 File Offset: 0x00012EB5
		[DataMember]
		public PolicySyncTransientFault Detail { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00014CBE File Offset: 0x00012EBE
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x00014CC6 File Offset: 0x00012EC6
		[DataMember]
		public string FaultType { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00014CCF File Offset: 0x00012ECF
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x00014CD7 File Offset: 0x00012ED7
		[DataMember]
		public string Message { get; set; }
	}
}
