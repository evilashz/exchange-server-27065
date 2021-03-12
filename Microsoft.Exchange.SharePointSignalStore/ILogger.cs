using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharePointSignalStore
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ILogger
	{
		// Token: 0x06000021 RID: 33
		void LogWarning(string format, params object[] args);

		// Token: 0x06000022 RID: 34
		void LogInfo(string format, params object[] args);
	}
}
