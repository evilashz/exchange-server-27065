using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Rpc.QueueViewer;

namespace Microsoft.Exchange.Transport.QueueViewer
{
	// Token: 0x02000360 RID: 864
	internal class VersionedQueueViewerClient : QueueViewerRpcClient
	{
		// Token: 0x0600255C RID: 9564 RVA: 0x00090FD5 File Offset: 0x0008F1D5
		public VersionedQueueViewerClient(string serverName) : base(serverName)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00090FE8 File Offset: 0x0008F1E8
		public byte[] GetQueueViewerObjectPage(QVObjectType objectType, byte[] queryFilterBytes, byte[] sortOrderBytes, bool searchForward, int pageSize, byte[] bookmarkObjectBytes, int bookmarkIndex, bool includeBookmark, bool includeDetails, byte[] inArgsBytes, ref int totalCount, ref int pageOffset)
		{
			bool usePreE14R4API = !VersionedQueueViewerClient.IsLocalHost(this.serverName) && !VersionedQueueViewerClient.ServerLaterThanVersion(VersionedQueueViewerClient.latencyComponentMinVersion, this.serverName);
			return base.GetQueueViewerObjectPage(objectType, queryFilterBytes, sortOrderBytes, searchForward, pageSize, bookmarkObjectBytes, bookmarkIndex, includeBookmark, includeDetails, usePreE14R4API, inArgsBytes, ref totalCount, ref pageOffset);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00091038 File Offset: 0x0008F238
		internal static bool UsePropertyBagBasedAPI(string serverName)
		{
			Server server = VersionedQueueViewerClient.GetServer(serverName);
			return VersionedQueueViewerClient.UsePropertyBagBasedAPI(server);
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00091052 File Offset: 0x0008F252
		internal static bool UsePropertyBagBasedAPI(Server targetServer)
		{
			return VersionedQueueViewerClient.IsLocalHost(targetServer) || VersionedQueueViewerClient.ServerLaterThanVersion(VersionedQueueViewerClient.newAPIVersion, targetServer);
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x0009106C File Offset: 0x0008F26C
		internal static bool UseCustomSerialization(string serverName)
		{
			Server server = VersionedQueueViewerClient.GetServer(serverName);
			return VersionedQueueViewerClient.UseCustomSerialization(server);
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x00091086 File Offset: 0x0008F286
		internal static bool UseCustomSerialization(Server targetServer)
		{
			return VersionedQueueViewerClient.IsLocalHost(targetServer) || VersionedQueueViewerClient.ServerLaterThan14Or15Version(VersionedQueueViewerClient.customSerialzationApiE14Version, VersionedQueueViewerClient.customSerialzationApiE15Version, targetServer);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000910A2 File Offset: 0x0008F2A2
		internal static bool IsLocalHost(string serverName)
		{
			return serverName.Equals("localhost", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000910B0 File Offset: 0x0008F2B0
		internal static bool IsLocalHost(Server targetServer)
		{
			return targetServer.Name.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000910C4 File Offset: 0x0008F2C4
		internal static bool ServerLaterThanVersion(int version, string serverName)
		{
			Server server = VersionedQueueViewerClient.GetServer(serverName);
			return VersionedQueueViewerClient.ServerLaterThanVersion(version, server);
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000910E0 File Offset: 0x0008F2E0
		internal static bool ServerLaterThanVersion(int version, Server targetServer)
		{
			int versionNumber = targetServer.VersionNumber;
			bool result;
			if (versionNumber >= Server.E2007SP2MinVersion && versionNumber < version)
			{
				result = false;
			}
			else
			{
				if (versionNumber < version)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_SERVER_DATA);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000911A0 File Offset: 0x0008F3A0
		internal static Server GetServer(string serverName)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				throw new ArgumentException("serverName is invalid", "servername");
			}
			Server targetServer = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 258, "GetServer", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Queuing\\QueueViewer\\VersionedQueueViewerClient.cs");
				if (VersionedQueueViewerClient.IsLocalHost(serverName))
				{
					targetServer = topologyConfigurationSession.FindLocalServer();
					return;
				}
				if (serverName.Contains("."))
				{
					targetServer = topologyConfigurationSession.FindServerByFqdn(serverName);
					return;
				}
				targetServer = topologyConfigurationSession.FindServerByName(serverName);
			}, 0);
			if (!adoperationResult.Succeeded || targetServer == null)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_RPC_SERVER_UNAVAILABLE);
			}
			return targetServer;
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00091214 File Offset: 0x0008F414
		private static bool ServerLaterThan14Or15Version(int versionIf14, int versionIf15, string serverName)
		{
			Server server = VersionedQueueViewerClient.GetServer(serverName);
			return VersionedQueueViewerClient.ServerLaterThan14Or15Version(versionIf14, versionIf15, server);
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x00091230 File Offset: 0x0008F430
		private static bool ServerLaterThan14Or15Version(int versionIf14, int versionIf15, Server targetServer)
		{
			int versionNumber = targetServer.VersionNumber;
			return (versionNumber >= Server.E15MinVersion && versionNumber > versionIf15) || (versionNumber >= Server.E14MinVersion && versionNumber < Server.E15MinVersion && versionNumber >= versionIf14);
		}

		// Token: 0x04001352 RID: 4946
		private const int PropertyBagBasedAPIBuildCutOff = 562;

		// Token: 0x04001353 RID: 4947
		private static readonly int latencyComponentMinVersion = new ServerVersion(14, 0, 562, 0).ToInt();

		// Token: 0x04001354 RID: 4948
		private static readonly int newAPIVersion = new ServerVersion(14, 1, 218, 0).ToInt();

		// Token: 0x04001355 RID: 4949
		private static readonly int customSerialzationApiE15Version = new ServerVersion(15, 0, 419, 0).ToInt();

		// Token: 0x04001356 RID: 4950
		private static readonly int customSerialzationApiE14Version = new ServerVersion(14, 0, 562, 0).ToInt();

		// Token: 0x04001357 RID: 4951
		protected string serverName;
	}
}
