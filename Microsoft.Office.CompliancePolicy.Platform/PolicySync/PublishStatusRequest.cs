using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000104 RID: 260
	[KnownType(typeof(UnifiedPolicyStatus))]
	[DataContract]
	[KnownType(typeof(SyncCallerContext))]
	[KnownType(typeof(PolicyVersion))]
	public class PublishStatusRequest
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00014D65 File Offset: 0x00012F65
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x00014D6D File Offset: 0x00012F6D
		[DataMember]
		public SyncCallerContext CallerContext { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00014D76 File Offset: 0x00012F76
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x00014D7E File Offset: 0x00012F7E
		[DataMember]
		public IEnumerable<UnifiedPolicyStatus> ConfigurationStatuses { get; set; }
	}
}
