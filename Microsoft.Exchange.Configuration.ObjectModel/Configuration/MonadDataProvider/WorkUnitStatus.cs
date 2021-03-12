using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001E5 RID: 485
	public enum WorkUnitStatus
	{
		// Token: 0x040003E9 RID: 1001
		[LocDescription(Strings.IDs.WorkUnitStatusNotStarted)]
		NotStarted,
		// Token: 0x040003EA RID: 1002
		[LocDescription(Strings.IDs.WorkUnitStatusInProgress)]
		InProgress,
		// Token: 0x040003EB RID: 1003
		[LocDescription(Strings.IDs.WorkUnitStatusCompleted)]
		Completed,
		// Token: 0x040003EC RID: 1004
		[LocDescription(Strings.IDs.WorkUnitStatusFailed)]
		Failed
	}
}
