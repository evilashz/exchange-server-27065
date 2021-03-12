using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003FD RID: 1021
	[DataContract(Name = "LicenseErrorType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum LicenseErrorType
	{
		// Token: 0x040011B4 RID: 4532
		[EnumMember]
		MutuallyExclusiveViolation,
		// Token: 0x040011B5 RID: 4533
		[EnumMember]
		DependencyViolation,
		// Token: 0x040011B6 RID: 4534
		[EnumMember]
		ProhibitedInUsageLocationViolation,
		// Token: 0x040011B7 RID: 4535
		[EnumMember]
		RestrictedInUsageLocationViolation,
		// Token: 0x040011B8 RID: 4536
		[EnumMember]
		OnPremiseMailboxViolation,
		// Token: 0x040011B9 RID: 4537
		[EnumMember]
		Other
	}
}
