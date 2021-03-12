using System;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000109 RID: 265
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISetBroken
	{
		// Token: 0x06000A58 RID: 2648
		void SetBroken(FailureTag failureTag, ExEventLog.EventTuple setBrokenEventTuple, params string[] setBrokenArgs);

		// Token: 0x06000A59 RID: 2649
		void SetBroken(FailureTag failureTag, ExEventLog.EventTuple setBrokenEventTuple, Exception exception, params string[] setBrokenArgs);

		// Token: 0x06000A5A RID: 2650
		void ClearBroken();

		// Token: 0x06000A5B RID: 2651
		void RestartInstanceSoon(bool fPrepareToStop);

		// Token: 0x06000A5C RID: 2652
		void RestartInstanceNow(ReplayConfigChangeHints restartReason);

		// Token: 0x06000A5D RID: 2653
		void RestartInstanceSoonAdminVisible();

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A5E RID: 2654
		bool IsBroken { get; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A5F RID: 2655
		LocalizedString ErrorMessage { get; }
	}
}
