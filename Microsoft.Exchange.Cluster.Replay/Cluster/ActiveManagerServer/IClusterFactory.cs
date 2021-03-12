using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000B2 RID: 178
	internal interface IClusterFactory
	{
		// Token: 0x06000757 RID: 1879
		IAmCluster Open();

		// Token: 0x06000758 RID: 1880
		IAmDbState CreateClusterDbState(IAmCluster cluster);

		// Token: 0x06000759 RID: 1881
		IAmCluster OpenByName(AmServerName serverName);

		// Token: 0x0600075A RID: 1882
		bool IsRunning();

		// Token: 0x0600075B RID: 1883
		bool IsEvicted(AmServerName machineName);

		// Token: 0x0600075C RID: 1884
		IAmCluster CreateExchangeCluster(string clusterName, AmServerName firstNodeName, string[] ipAddress, uint[] ipPrefixLength, IClusterSetupProgress setupProgress, IntPtr context, out Exception failureException, bool throwExceptionOnFailure);
	}
}
