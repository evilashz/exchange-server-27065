using System;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x0200015E RID: 350
	internal enum VssRestoreScenario
	{
		// Token: 0x040002E9 RID: 745
		rstscenUnknown,
		// Token: 0x040002EA RID: 746
		rstscenOriginalDB,
		// Token: 0x040002EB RID: 747
		rstscenAlternateDB,
		// Token: 0x040002EC RID: 748
		rstscenAlternateLoc
	}
}
