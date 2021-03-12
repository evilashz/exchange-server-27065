using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200008D RID: 141
	[DataContract]
	public enum PolicyBindingTypes
	{
		// Token: 0x04000270 RID: 624
		[EnumMember]
		IndividualResource,
		// Token: 0x04000271 RID: 625
		[EnumMember]
		Tenant,
		// Token: 0x04000272 RID: 626
		[EnumMember]
		SiteTemplate
	}
}
