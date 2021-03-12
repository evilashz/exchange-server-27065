using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200014A RID: 330
	[DataContract]
	public enum CAUserAccountControlStatusEntry
	{
		// Token: 0x04000546 RID: 1350
		[EnumMember]
		AlwaysNotify = 1,
		// Token: 0x04000547 RID: 1351
		[EnumMember]
		NotifyAppChanges,
		// Token: 0x04000548 RID: 1352
		[EnumMember]
		NotifyAppChangesDoNotDimdesktop,
		// Token: 0x04000549 RID: 1353
		[EnumMember]
		NeverNotify
	}
}
