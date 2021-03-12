using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200021A RID: 538
	internal static class DatabaseValidationChecks
	{
		// Token: 0x040007F3 RID: 2035
		public static readonly DatabaseValidationMultiChecks DatabaseAvailabilityActiveChecks = new DatabaseAvailabilityActiveChecks();

		// Token: 0x040007F4 RID: 2036
		public static readonly DatabaseValidationMultiChecks DatabaseAvailabilityPassiveChecks = new DatabaseAvailabilityPassiveChecks();

		// Token: 0x040007F5 RID: 2037
		public static readonly DatabaseValidationMultiChecks DatabaseRedundancyDatabaseLevelActiveChecks = new DatabaseRedundancyDatabaseLevelActiveChecks();

		// Token: 0x040007F6 RID: 2038
		public static readonly DatabaseValidationMultiChecks DatabaseRedundancyDatabaseLevelPassiveChecks = new DatabaseRedundancyDatabaseLevelPassiveChecks();

		// Token: 0x040007F7 RID: 2039
		public static readonly DatabaseValidationMultiChecks DatabaseConnectedActiveChecks = new DatabaseConnectedActiveChecks();

		// Token: 0x040007F8 RID: 2040
		public static readonly DatabaseValidationMultiChecks DatabaseConnectedPassiveChecks = new DatabaseConnectedPassiveChecks();
	}
}
