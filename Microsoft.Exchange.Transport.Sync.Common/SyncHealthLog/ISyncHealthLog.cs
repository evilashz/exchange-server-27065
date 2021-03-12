using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.SyncHealthLog
{
	// Token: 0x020000F3 RID: 243
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISyncHealthLog
	{
		// Token: 0x0600073C RID: 1852
		void LogWorkTypeBudgets(KeyValuePair<string, object>[] eventData);
	}
}
