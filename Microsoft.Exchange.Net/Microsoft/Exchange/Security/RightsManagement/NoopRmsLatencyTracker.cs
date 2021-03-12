using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009C1 RID: 2497
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NoopRmsLatencyTracker : IRmsLatencyTracker
	{
		// Token: 0x06003655 RID: 13909 RVA: 0x0008AC95 File Offset: 0x00088E95
		private NoopRmsLatencyTracker()
		{
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x0008AC9D File Offset: 0x00088E9D
		public void BeginTrackRmsLatency(RmsOperationType operation)
		{
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x0008AC9F File Offset: 0x00088E9F
		public void EndTrackRmsLatency(RmsOperationType operation)
		{
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x0008ACA1 File Offset: 0x00088EA1
		public void EndAndBeginTrackRmsLatency(RmsOperationType endOperation, RmsOperationType beginOperation)
		{
		}

		// Token: 0x04002EAD RID: 11949
		public static readonly NoopRmsLatencyTracker Instance = new NoopRmsLatencyTracker();
	}
}
