using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000083 RID: 131
	[DataContract]
	public enum ConfigurationObjectType
	{
		// Token: 0x0400022E RID: 558
		[EnumMember]
		Policy,
		// Token: 0x0400022F RID: 559
		[EnumMember]
		Rule,
		// Token: 0x04000230 RID: 560
		[EnumMember]
		Association,
		// Token: 0x04000231 RID: 561
		[EnumMember]
		Binding,
		// Token: 0x04000232 RID: 562
		[EnumMember]
		Scope
	}
}
