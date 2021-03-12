using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000086 RID: 134
	[Flags]
	[DataContract]
	public enum Workload
	{
		// Token: 0x04000241 RID: 577
		None = 0,
		// Token: 0x04000242 RID: 578
		[EnumMember]
		Exchange = 1,
		// Token: 0x04000243 RID: 579
		[EnumMember]
		SharePoint = 2,
		// Token: 0x04000244 RID: 580
		[EnumMember]
		Intune = 4,
		// Token: 0x04000245 RID: 581
		[EnumMember]
		OneDriveForBusiness = 8
	}
}
