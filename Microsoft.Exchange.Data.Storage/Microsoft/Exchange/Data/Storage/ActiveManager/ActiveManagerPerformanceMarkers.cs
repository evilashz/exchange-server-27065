using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x02000308 RID: 776
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ActiveManagerPerformanceMarkers
	{
		// Token: 0x04001470 RID: 5232
		public const string GetServerForDatabaseGetServerNameForDatabase = "GetServerForDatabaseGetServerNameForDatabase";

		// Token: 0x04001471 RID: 5233
		public const string GetServerForDatabaseGetServerInformationForDatabase = "GetServerForDatabaseGetServerInformationForDatabase";

		// Token: 0x04001472 RID: 5234
		public const string GetServerNameForDatabaseGetDatabaseByGuidEx = "GetServerNameForDatabaseGetDatabaseByGuidEx";

		// Token: 0x04001473 RID: 5235
		public const string GetServerNameForDatabaseLookupDatabaseAndPossiblyPopulateCache = "GetServerNameForDatabaseLookupDatabaseAndPossiblyPopulateCache";

		// Token: 0x04001474 RID: 5236
		public const string GetServerInformationForDatabaseGetDatabaseByGuidEx = "GetServerInformationForDatabaseGetDatabaseByGuidEx";

		// Token: 0x04001475 RID: 5237
		public const string ActiveDirectoryQueryLatency = "ADQuery";
	}
}
