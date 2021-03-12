using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x02000098 RID: 152
	public enum TraceLevel
	{
		// Token: 0x040001CC RID: 460
		[TrackingStringsLocDescription(CoreStrings.IDs.TraceLevelLow)]
		Low,
		// Token: 0x040001CD RID: 461
		[TrackingStringsLocDescription(CoreStrings.IDs.TraceLevelMedium)]
		Medium,
		// Token: 0x040001CE RID: 462
		[TrackingStringsLocDescription(CoreStrings.IDs.TraceLevelHigh)]
		High
	}
}
