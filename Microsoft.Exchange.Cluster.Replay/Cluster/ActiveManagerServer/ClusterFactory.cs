using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000B3 RID: 179
	internal class ClusterFactory : IClusterFactory, IMockableCluster
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x000240B0 File Offset: 0x000222B0
		public static IClusterFactory Instance
		{
			get
			{
				return ClusterFactory.instance;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000240B7 File Offset: 0x000222B7
		public static void SetTestClusterFactory(IClusterFactory testClusterFactory, IMockableCluster amClusterOverride)
		{
			ClusterFactory.instance = testClusterFactory;
			MockableCluster.SetTestInstance(amClusterOverride);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000240C5 File Offset: 0x000222C5
		public IAmCluster Open()
		{
			return AmCluster.Open();
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000240CC File Offset: 0x000222CC
		IAmCluster IMockableCluster.OpenByName(AmServerName serverName)
		{
			return AmCluster.OpenByName(serverName);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000240D4 File Offset: 0x000222D4
		IAmCluster IClusterFactory.OpenByName(AmServerName serverName)
		{
			return AmCluster.OpenByName(serverName);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000240DC File Offset: 0x000222DC
		public IAmDbState CreateClusterDbState(IAmCluster cluster)
		{
			return new AmClusterDbState(cluster);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000240E4 File Offset: 0x000222E4
		public bool IsRunning()
		{
			return AmCluster.IsRunning();
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000240EB File Offset: 0x000222EB
		public bool IsEvicted(AmServerName machineName)
		{
			return AmCluster.IsEvicted(machineName);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000240F3 File Offset: 0x000222F3
		public IAmCluster CreateExchangeCluster(string clusterName, AmServerName firstNodeName, string[] ipAddress, uint[] ipPrefixLength, IClusterSetupProgress setupProgress, IntPtr context, out Exception failureException, bool throwExceptionOnFailure)
		{
			return AmCluster.CreateExchangeCluster(clusterName, firstNodeName, ipAddress, ipPrefixLength, setupProgress, context, out failureException, throwExceptionOnFailure);
		}

		// Token: 0x04000341 RID: 833
		private static IClusterFactory instance = new ClusterFactory();
	}
}
