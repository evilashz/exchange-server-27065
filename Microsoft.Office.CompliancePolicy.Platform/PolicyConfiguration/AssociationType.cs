using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000087 RID: 135
	[DataContract]
	public enum AssociationType
	{
		// Token: 0x04000247 RID: 583
		[EnumMember]
		SPSiteCollection,
		// Token: 0x04000248 RID: 584
		[EnumMember]
		SPTemplate
	}
}
