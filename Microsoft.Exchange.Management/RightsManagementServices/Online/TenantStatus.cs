using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.RightsManagementServices.Online
{
	// Token: 0x0200073A RID: 1850
	[DataContract(Name = "TenantStatus", Namespace = "http://microsoft.com/RightsManagementServiceOnline/2011/04")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum TenantStatus
	{
		// Token: 0x04002960 RID: 10592
		[EnumMember]
		Disabled,
		// Token: 0x04002961 RID: 10593
		[EnumMember]
		Enabled,
		// Token: 0x04002962 RID: 10594
		[EnumMember]
		Deprovisioned
	}
}
