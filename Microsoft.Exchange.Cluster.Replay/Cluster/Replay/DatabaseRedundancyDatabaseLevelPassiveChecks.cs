using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200021F RID: 543
	internal class DatabaseRedundancyDatabaseLevelPassiveChecks : DatabaseValidationMultiChecks
	{
		// Token: 0x0600148C RID: 5260 RVA: 0x00052926 File Offset: 0x00050B26
		protected override void DefineChecks()
		{
			base.DefineChecks();
			base.AddCheck(new DatabaseCheckPassiveCopyStatusIsOkForRedundancy());
			base.AddCheck(new DatabaseCheckPassiveCopyRealCopyQueueLength());
			base.AddCheck(new DatabaseCheckPassiveCopyInspectorQueueLength());
			base.AddCheck(new DatabaseCheckReplayServiceUpOnActiveCopy());
			base.AddCheck(new DatabaseCheckServerHasTooManyActives());
		}
	}
}
