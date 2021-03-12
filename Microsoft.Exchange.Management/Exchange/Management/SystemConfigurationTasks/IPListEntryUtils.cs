using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A6B RID: 2667
	internal static class IPListEntryUtils
	{
		// Token: 0x06005F3F RID: 24383 RVA: 0x0018F5F2 File Offset: 0x0018D7F2
		public static bool IsSupportedRole(Server server)
		{
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			return server.IsEdgeServer || server.IsHubTransportServer;
		}
	}
}
