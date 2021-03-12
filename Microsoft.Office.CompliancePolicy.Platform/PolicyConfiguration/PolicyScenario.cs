using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000084 RID: 132
	[DataContract]
	public enum PolicyScenario
	{
		// Token: 0x04000234 RID: 564
		[EnumMember]
		Retention,
		// Token: 0x04000235 RID: 565
		[EnumMember]
		Hold,
		// Token: 0x04000236 RID: 566
		[EnumMember]
		Dlp,
		// Token: 0x04000237 RID: 567
		[EnumMember]
		DeviceSettings,
		// Token: 0x04000238 RID: 568
		[EnumMember]
		AuditSettings,
		// Token: 0x04000239 RID: 569
		[EnumMember]
		DeviceConditionalAccess,
		// Token: 0x0400023A RID: 570
		[EnumMember]
		DeviceTenantConditionalAccess
	}
}
