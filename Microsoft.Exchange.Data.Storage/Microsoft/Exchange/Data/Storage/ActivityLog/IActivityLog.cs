using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IActivityLog
	{
		// Token: 0x0600030C RID: 780
		void Append(IEnumerable<Activity> activities);

		// Token: 0x0600030D RID: 781
		IEnumerable<Activity> Query();

		// Token: 0x0600030E RID: 782
		void Reset();

		// Token: 0x0600030F RID: 783
		bool IsGroup();
	}
}
