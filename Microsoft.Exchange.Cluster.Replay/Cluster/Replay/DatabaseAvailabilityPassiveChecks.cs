using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200021D RID: 541
	internal class DatabaseAvailabilityPassiveChecks : DatabaseValidationMultiChecks
	{
		// Token: 0x06001488 RID: 5256 RVA: 0x000528B9 File Offset: 0x00050AB9
		protected override void DefineChecks()
		{
			base.DefineChecks();
			base.AddCheck(new DatabaseCheckClusterNodeUp());
			base.AddCheck(new DatabaseCheckServerAllowedForActivation());
			base.AddCheck(new DatabaseCheckPassiveCopyStatusIsOkForAvailability());
			base.AddCheck(new DatabaseCheckPassiveCopyTotalQueueLength());
			base.AddCheck(new DatabaseCheckPassiveCopyRealCopyQueueLength());
		}
	}
}
