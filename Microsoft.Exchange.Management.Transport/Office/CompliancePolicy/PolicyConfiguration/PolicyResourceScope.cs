using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000151 RID: 337
	[DataContract]
	public enum PolicyResourceScope
	{
		// Token: 0x040005C2 RID: 1474
		[EnumMember]
		ExchangeOnline,
		// Token: 0x040005C3 RID: 1475
		[EnumMember]
		SharepointOnline,
		// Token: 0x040005C4 RID: 1476
		[EnumMember]
		ExchangeAndSharepoint
	}
}
