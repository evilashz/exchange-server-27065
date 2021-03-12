using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009C0 RID: 2496
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRmsLatencyTracker
	{
		// Token: 0x06003652 RID: 13906
		void BeginTrackRmsLatency(RmsOperationType operation);

		// Token: 0x06003653 RID: 13907
		void EndTrackRmsLatency(RmsOperationType operation);

		// Token: 0x06003654 RID: 13908
		void EndAndBeginTrackRmsLatency(RmsOperationType endOperation, RmsOperationType beginOperation);
	}
}
