using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000019 RID: 25
	internal class MockableCluster : IMockableCluster
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003987 File Offset: 0x00001B87
		public static IMockableCluster Instance
		{
			get
			{
				return MockableCluster.instance;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000398E File Offset: 0x00001B8E
		public static void SetTestInstance(IMockableCluster testInstance)
		{
			MockableCluster.instance = testInstance;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003996 File Offset: 0x00001B96
		public IAmCluster OpenByName(AmServerName serverName)
		{
			return AmCluster.OpenByName(serverName);
		}

		// Token: 0x04000032 RID: 50
		private static IMockableCluster instance = new MockableCluster();
	}
}
