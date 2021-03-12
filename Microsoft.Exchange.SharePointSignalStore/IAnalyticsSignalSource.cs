using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharePointSignalStore
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IAnalyticsSignalSource
	{
		// Token: 0x0600001F RID: 31
		IEnumerable<AnalyticsSignal> GetSignals();

		// Token: 0x06000020 RID: 32
		string GetSourceName();
	}
}
