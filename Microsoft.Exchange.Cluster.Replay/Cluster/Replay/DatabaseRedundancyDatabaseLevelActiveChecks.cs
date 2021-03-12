using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200021E RID: 542
	internal class DatabaseRedundancyDatabaseLevelActiveChecks : DatabaseValidationMultiChecks
	{
		// Token: 0x0600148A RID: 5258 RVA: 0x00052900 File Offset: 0x00050B00
		protected override void DefineChecks()
		{
			base.DefineChecks();
			base.AddCheck(new DatabaseCheckActiveMountState());
			base.AddCheck(new DatabaseCheckServerHasTooManyActives());
		}
	}
}
