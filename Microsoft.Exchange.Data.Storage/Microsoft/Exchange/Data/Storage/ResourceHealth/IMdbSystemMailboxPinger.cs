using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B24 RID: 2852
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMdbSystemMailboxPinger
	{
		// Token: 0x17001C6D RID: 7277
		// (get) Token: 0x0600675B RID: 26459
		DateTime LastSuccessfulPingUtc { get; }

		// Token: 0x17001C6E RID: 7278
		// (get) Token: 0x0600675C RID: 26460
		DateTime LastPingAttemptUtc { get; }

		// Token: 0x17001C6F RID: 7279
		// (get) Token: 0x0600675D RID: 26461
		bool Pinging { get; }

		// Token: 0x0600675E RID: 26462
		bool Ping();
	}
}
