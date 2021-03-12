using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000145 RID: 325
	[DataContract]
	public enum AutoUpdateStatusEntry
	{
		// Token: 0x040004D1 RID: 1233
		[EnumMember]
		AutomaticUpdatesRequired,
		// Token: 0x040004D2 RID: 1234
		[EnumMember]
		AutomaticCheckForUpdates,
		// Token: 0x040004D3 RID: 1235
		[EnumMember]
		AutomaticDownloadUpdates,
		// Token: 0x040004D4 RID: 1236
		[EnumMember]
		NeverCheckUpdates,
		// Token: 0x040004D5 RID: 1237
		[EnumMember]
		DeviceDefault
	}
}
