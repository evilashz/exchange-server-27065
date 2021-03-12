using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B26 RID: 2854
	internal static class ForeignConnectorTaskUtil
	{
		// Token: 0x060066CE RID: 26318 RVA: 0x001A9102 File Offset: 0x001A7302
		public static void ValidateObject(ForeignConnector connector, IConfigurationSession session, Task task)
		{
			ForeignConnectorTaskUtil.ValidateSourceServers(connector, session, task);
			ForeignConnectorTaskUtil.ValidateDropDirectory(connector);
		}

		// Token: 0x060066CF RID: 26319 RVA: 0x001A9112 File Offset: 0x001A7312
		public static void CheckTopology()
		{
			if (TopologyProvider.IsAdamTopology())
			{
				throw new CannotRunForeignConnectorTaskOnEdgeException();
			}
		}

		// Token: 0x060066D0 RID: 26320 RVA: 0x001A9121 File Offset: 0x001A7321
		public static bool IsHubServer(Server server)
		{
			return server != null && server.IsExchange2007OrLater && server.IsHubTransportServer;
		}

		// Token: 0x060066D1 RID: 26321 RVA: 0x001A9138 File Offset: 0x001A7338
		public static void ValidateSourceServers(ForeignConnector connector, IConfigurationSession session, Task task)
		{
			ADObjectId sourceRoutingGroup = connector.SourceRoutingGroup;
			bool flag;
			bool flag2;
			LocalizedException ex = ManageSendConnectors.ValidateTransportServers(session, connector, ref sourceRoutingGroup, false, true, task, out flag, out flag2);
			if (ex != null)
			{
				throw ex;
			}
			if (flag2)
			{
				throw new MultiSiteSourceServersException();
			}
		}

		// Token: 0x060066D2 RID: 26322 RVA: 0x001A916B File Offset: 0x001A736B
		public static void ValidateDropDirectory(ForeignConnector connector)
		{
			if (string.IsNullOrEmpty(connector.DropDirectory))
			{
				throw new ForeignConnectorNullOrEmptyDropDirectoryException();
			}
		}
	}
}
