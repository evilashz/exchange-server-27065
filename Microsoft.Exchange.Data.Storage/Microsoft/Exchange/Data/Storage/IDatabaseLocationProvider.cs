using System;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001CC RID: 460
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDatabaseLocationProvider
	{
		// Token: 0x06001896 RID: 6294
		DatabaseLocationInfo GetLocationInfo(Guid mdbGuid, bool bypassCache, bool ignoreSiteBoundary);
	}
}
