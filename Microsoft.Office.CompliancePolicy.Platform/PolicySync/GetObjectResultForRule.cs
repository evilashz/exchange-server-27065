using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000F0 RID: 240
	[KnownType(typeof(RuleConfiguration))]
	[DataContract]
	public class GetObjectResultForRule
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00014763 File Offset: 0x00012963
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x0001476B File Offset: 0x0001296B
		[DataMember]
		public RuleConfiguration GetObjectResult { get; set; }
	}
}
