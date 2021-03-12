using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon.FaultInjection
{
	// Token: 0x02000077 RID: 119
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DiagnosticFaultInjection
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		internal static bool TryCreateException(string exceptionType, ref Exception exception)
		{
			return false;
		}

		// Token: 0x040002DF RID: 735
		public const uint MaxCallReceivedLid = 3676712253U;

		// Token: 0x040002E0 RID: 736
		public const uint NoAvailableDiskspaceLid = 2871405885U;

		// Token: 0x040002E1 RID: 737
		public const uint ParseSipUri = 2602970429U;

		// Token: 0x040002E2 RID: 738
		public const uint TryFindHuntgroupWhenNoPilotIdentifier = 3945147709U;

		// Token: 0x040002E3 RID: 739
		public const uint TryMapCallToWorkerProcess = 2334534973U;

		// Token: 0x040002E4 RID: 740
		public const uint IsCallToDifferentForest = 3408276797U;

		// Token: 0x040002E5 RID: 741
		public const uint MonitoringDelayInCallRouter = 3062246717U;

		// Token: 0x040002E6 RID: 742
		public const uint MonitoringDelayInUMService = 4135988541U;

		// Token: 0x040002E7 RID: 743
		public const uint MonitoringDelayInUMWorkerProcess = 2525375805U;
	}
}
