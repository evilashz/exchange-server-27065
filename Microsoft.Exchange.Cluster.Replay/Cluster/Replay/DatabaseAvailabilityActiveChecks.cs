using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200021C RID: 540
	internal class DatabaseAvailabilityActiveChecks : DatabaseValidationMultiChecks
	{
		// Token: 0x06001486 RID: 5254 RVA: 0x0005287D File Offset: 0x00050A7D
		protected override void DefineChecks()
		{
			base.DefineChecks();
			base.AddCheck(new DatabaseCheckClusterNodeUp());
			base.AddCheck(new DatabaseCheckServerAllowedForActivation());
			base.AddCheck(new DatabaseCheckActiveMountState());
			base.AddCheck(new DatabaseCheckActiveCopyNotActivationSuspended());
		}
	}
}
