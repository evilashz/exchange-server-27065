using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Ehf;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000046 RID: 70
	internal static class Utils
	{
		// Token: 0x0600023B RID: 571 RVA: 0x00009E3C File Offset: 0x0000803C
		public static bool IsLeaseDirectoryValidPath(string directoryPath)
		{
			return !string.IsNullOrEmpty(directoryPath) && directoryPath.IndexOfAny(Path.GetInvalidPathChars()) < 0;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009E57 File Offset: 0x00008057
		public static EhfTargetServerConfig CreateEhfTargetConfig(ITopologyConfigurationSession session, EdgeSyncEhfConnectorIdParameter con, Task task)
		{
			return new EhfTargetServerConfig(Utils.GetConnector(session, con, task), Utils.GetInternetWebProxy(session));
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009E88 File Offset: 0x00008088
		public static Uri GetInternetWebProxy(ITopologyConfigurationSession session)
		{
			Server localServer = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				localServer = session.ReadLocalServer();
			});
			if (localServer != null)
			{
				return localServer.InternetWebProxy;
			}
			return null;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009F2C File Offset: 0x0000812C
		public static EdgeSyncEhfConnector GetConnector(IConfigurationSession session, EdgeSyncEhfConnectorIdParameter connectorId, Task task)
		{
			EdgeSyncEhfConnector connector = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				if (connectorId != null)
				{
					if (connectorId.InternalADObjectId != null)
					{
						connector = session.Read<EdgeSyncEhfConnector>(connectorId.InternalADObjectId);
						return;
					}
				}
				else
				{
					connector = Utils.FindEnabledEhfSyncConnector(session, null);
				}
			});
			if (connector == null)
			{
				task.WriteError(new InvalidOperationException("Unable to find EHF connector object"), ErrorCategory.InvalidOperation, null);
			}
			return connector;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009F88 File Offset: 0x00008188
		public static EdgeSyncEhfConnector FindEnabledEhfSyncConnector(IConfigurationSession session, ADObjectId connectorIdToIgnore)
		{
			ADPagedReader<EdgeSyncEhfConnector> adpagedReader = session.FindAllPaged<EdgeSyncEhfConnector>();
			if (adpagedReader != null)
			{
				foreach (EdgeSyncEhfConnector edgeSyncEhfConnector in adpagedReader)
				{
					if (edgeSyncEhfConnector.Enabled && (connectorIdToIgnore == null || !connectorIdToIgnore.Equals(edgeSyncEhfConnector.Id)))
					{
						return edgeSyncEhfConnector;
					}
				}
			}
			return null;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009FF4 File Offset: 0x000081F4
		public static List<IPAddress> ParseValidAddresses(string[] ipList)
		{
			List<IPAddress> list = new List<IPAddress>();
			foreach (string ipString in ipList)
			{
				IPAddress item;
				if (IPAddress.TryParse(ipString, out item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A034 File Offset: 0x00008234
		public static IList<string> ConvertIPAddresssesToStrings(IEnumerable<IPAddress> ipList)
		{
			List<string> list = new List<string>();
			foreach (IPAddress ipaddress in ipList)
			{
				list.Add(ipaddress.ToString());
			}
			return list;
		}
	}
}
