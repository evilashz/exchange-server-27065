using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000143 RID: 323
	[DataContract]
	public enum UserAccountControlStatusEntry
	{
		// Token: 0x040004CA RID: 1226
		[EnumMember]
		AlwaysNotify = 1,
		// Token: 0x040004CB RID: 1227
		[EnumMember]
		NotifyAppChanges,
		// Token: 0x040004CC RID: 1228
		[EnumMember]
		NotifyAppChangesDoNotDimdesktop,
		// Token: 0x040004CD RID: 1229
		[EnumMember]
		NeverNotify
	}
}
