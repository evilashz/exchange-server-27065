using System;

namespace Microsoft.Exchange.Provisioning.LoadBalancing
{
	// Token: 0x02000204 RID: 516
	internal sealed class LoadBalancingReport
	{
		// Token: 0x060011FE RID: 4606 RVA: 0x00037A34 File Offset: 0x00035C34
		public override string ToString()
		{
			return string.Format("Load balancing report: enabled databases with local copy - {0}, local databases count - {1}, remote databases count - {2}, databases selected for load balancing - {3}, databases excluded from initial provisioning - {4}.", new object[]
			{
				this.enabledDatabasesWithLocalCopyCount,
				this.firstPreferenceDatabasesCount,
				this.secondPreferenceDatabasesCount,
				this.databasesAndLocationCount,
				this.databasesExcludedFromInitialProvisioning
			});
		}

		// Token: 0x04000444 RID: 1092
		public int enabledDatabasesWithLocalCopyCount;

		// Token: 0x04000445 RID: 1093
		public int firstPreferenceDatabasesCount;

		// Token: 0x04000446 RID: 1094
		public int secondPreferenceDatabasesCount;

		// Token: 0x04000447 RID: 1095
		public int databasesAndLocationCount;

		// Token: 0x04000448 RID: 1096
		public int databasesExcludedFromInitialProvisioning;
	}
}
