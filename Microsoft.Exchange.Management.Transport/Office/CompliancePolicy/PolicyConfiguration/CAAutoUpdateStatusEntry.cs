using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200014C RID: 332
	[DataContract]
	public enum CAAutoUpdateStatusEntry
	{
		// Token: 0x0400054D RID: 1357
		[EnumMember]
		AutomaticUpdatesRequired,
		// Token: 0x0400054E RID: 1358
		[EnumMember]
		AutomaticCheckForUpdates,
		// Token: 0x0400054F RID: 1359
		[EnumMember]
		AutomaticDownloadUpdates,
		// Token: 0x04000550 RID: 1360
		[EnumMember]
		NeverCheckUpdates,
		// Token: 0x04000551 RID: 1361
		[EnumMember]
		DeviceDefault
	}
}
