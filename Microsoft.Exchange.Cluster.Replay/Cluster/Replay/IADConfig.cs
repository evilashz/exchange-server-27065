using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000CC RID: 204
	internal interface IADConfig
	{
		// Token: 0x06000843 RID: 2115
		IADDatabaseAvailabilityGroup GetLocalDag();

		// Token: 0x06000844 RID: 2116
		IADServer GetLocalServer();

		// Token: 0x06000845 RID: 2117
		IADServer GetServer(string nodeOrFqdn);

		// Token: 0x06000846 RID: 2118
		IADServer GetServer(AmServerName serverName);

		// Token: 0x06000847 RID: 2119
		IADDatabase GetDatabase(Guid dbGuid);

		// Token: 0x06000848 RID: 2120
		IEnumerable<IADDatabase> GetDatabasesOnServer(AmServerName serverName);

		// Token: 0x06000849 RID: 2121
		IEnumerable<IADDatabase> GetDatabasesOnServer(IADServer server);

		// Token: 0x0600084A RID: 2122
		IEnumerable<IADDatabase> GetDatabasesOnLocalServer();

		// Token: 0x0600084B RID: 2123
		IEnumerable<IADDatabase> GetDatabasesInLocalDag();

		// Token: 0x0600084C RID: 2124
		void Refresh(string reason);
	}
}
