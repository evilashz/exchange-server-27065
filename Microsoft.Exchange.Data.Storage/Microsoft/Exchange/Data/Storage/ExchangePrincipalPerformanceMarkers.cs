using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200026F RID: 623
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ExchangePrincipalPerformanceMarkers
	{
		// Token: 0x0400126B RID: 4715
		public const string UpdateDatabaseLocationInfo = "UpdateDatabaseLocationInfo";

		// Token: 0x0400126C RID: 4716
		public const string UpdateCrossPremiseStatus = "UpdateCrossPremiseStatus";

		// Token: 0x0400126D RID: 4717
		public const string UpdateCrossPremiseStatusFindByExchangeGuidIncludingAlternate = "UpdateCrossPremiseStatusFindByExchangeGuidIncludingAlternate";

		// Token: 0x0400126E RID: 4718
		public const string UpdateCrossPremiseStatusFindByLegacyExchangeDN = "UpdateCrossPremiseStatusFindByLegacyExchangeDN";

		// Token: 0x0400126F RID: 4719
		public const string UpdateCrossPremiseStatusRemoteMailbox = "UpdateCrossPremiseStatusRemoteMailbox";

		// Token: 0x04001270 RID: 4720
		public const string UpdateDelegationTokenRequest = "UpdateDelegationTokenRequest";

		// Token: 0x04001271 RID: 4721
		public const string GetUserSKUCapability = "GetUserSKUCapability";

		// Token: 0x04001272 RID: 4722
		public const string GetIsLicensingEnforcedInOrg = "GetIsLicensingEnforcedInOrg";
	}
}
