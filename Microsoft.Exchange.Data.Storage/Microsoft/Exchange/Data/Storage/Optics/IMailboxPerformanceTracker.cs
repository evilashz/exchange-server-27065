using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Optics
{
	// Token: 0x020007E9 RID: 2025
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxPerformanceTracker : IPerformanceTracker
	{
		// Token: 0x06004BA8 RID: 19368
		ILogEvent GetLogEvent(string operationName);
	}
}
