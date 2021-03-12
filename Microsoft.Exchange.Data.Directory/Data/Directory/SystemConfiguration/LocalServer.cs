using System;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004A1 RID: 1185
	internal static class LocalServer
	{
		// Token: 0x06003632 RID: 13874 RVA: 0x000D5155 File Offset: 0x000D3355
		public static bool AllowsTokenSerializationBy(ClientSecurityContext clientContext)
		{
			return LocalServer.HasExtendedRightOnServer(clientContext, WellKnownGuid.TokenSerializationRightGuid);
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000D5162 File Offset: 0x000D3362
		public static bool HasExtendedRightOnServer(ClientSecurityContext clientContext, Guid extendedRight)
		{
			return LocalServer.GetServer().HasExtendedRight(clientContext, extendedRight);
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000D5170 File Offset: 0x000D3370
		public static Server GetServer()
		{
			if (LocalServer.server == null)
			{
				lock (LocalServer.lockObject)
				{
					if (LocalServer.server == null)
					{
						ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.NonCacheSessionFactory.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 71, "GetServer", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\LocalServer.cs");
						LocalServer.server = topologyConfigurationSession.FindLocalServer();
					}
				}
			}
			return LocalServer.server;
		}

		// Token: 0x04002496 RID: 9366
		private static object lockObject = new object();

		// Token: 0x04002497 RID: 9367
		private static Server server = null;
	}
}
