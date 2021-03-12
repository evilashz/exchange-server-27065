using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000018 RID: 24
	internal interface IMockableCluster
	{
		// Token: 0x0600009F RID: 159
		IAmCluster OpenByName(AmServerName serverName);
	}
}
