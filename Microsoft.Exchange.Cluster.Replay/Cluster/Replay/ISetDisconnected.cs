using System;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200010A RID: 266
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISetDisconnected
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A60 RID: 2656
		bool IsDisconnected { get; }

		// Token: 0x06000A61 RID: 2657
		void SetDisconnected(FailureTag failureTag, ExEventLog.EventTuple setDisconnectedEventTuple, params string[] setDisconnectedArgs);

		// Token: 0x06000A62 RID: 2658
		void ClearDisconnected();

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A63 RID: 2659
		LocalizedString ErrorMessage { get; }
	}
}
