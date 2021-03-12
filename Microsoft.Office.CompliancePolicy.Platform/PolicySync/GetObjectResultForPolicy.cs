using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000EF RID: 239
	[KnownType(typeof(PolicyConfiguration))]
	[DataContract]
	public class GetObjectResultForPolicy
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001474A File Offset: 0x0001294A
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x00014752 File Offset: 0x00012952
		[DataMember]
		public PolicyConfiguration GetObjectResult { get; set; }
	}
}
