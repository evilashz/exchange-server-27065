using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001E4 RID: 484
	internal interface ICopyStatusClientLookup
	{
		// Token: 0x06001338 RID: 4920
		CopyStatusClientCachedEntry GetCopyStatus(Guid dbGuid, AmServerName server, CopyStatusClientLookupFlags flags);

		// Token: 0x06001339 RID: 4921
		IEnumerable<CopyStatusClientCachedEntry> GetCopyStatusesByDatabase(Guid dbGuid, IEnumerable<AmServerName> servers, CopyStatusClientLookupFlags flags);

		// Token: 0x0600133A RID: 4922
		IEnumerable<CopyStatusClientCachedEntry> GetCopyStatusesByServer(AmServerName server, IEnumerable<IADDatabase> expectedDatabases, CopyStatusClientLookupFlags flags);
	}
}
