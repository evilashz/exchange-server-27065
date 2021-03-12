using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000CB RID: 203
	internal class AmRpcClientWrapper : IAmRpcClientHelper
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x000281B8 File Offset: 0x000263B8
		public int RpcchGetAutomountConsensusState(string serverName)
		{
			return AmRpcClientHelper.RpcchGetAutomountConsensusState(serverName);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000281C0 File Offset: 0x000263C0
		public bool IsReplayRunning(AmServerName serverName)
		{
			bool result = false;
			try
			{
				result = AmRpcClientHelper.IsRunning(serverName.Fqdn);
			}
			catch (AmServerException ex)
			{
				AmTrace.Error("IsReplayRunning() failed with {0}", new object[]
				{
					ex
				});
			}
			catch (AmServerTransientException ex2)
			{
				AmTrace.Error("IsReplayRunning() failed with {0}", new object[]
				{
					ex2
				});
			}
			return result;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00028230 File Offset: 0x00026430
		public bool IsReplayRunning(string serverFqdn)
		{
			return this.IsReplayRunning(new AmServerName(serverFqdn));
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002823E File Offset: 0x0002643E
		public AmRole GetActiveManagerRole(string serverToRpc, out string errorMessage)
		{
			return AmRpcClientHelper.GetActiveManagerRole(serverToRpc, out errorMessage);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00028247 File Offset: 0x00026447
		public void MountDatabaseDirectEx(string serverToRpc, Guid dbGuid, AmMountArg mountArg)
		{
			AmRpcClientHelper.MountDatabaseDirectEx(serverToRpc, dbGuid, mountArg);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00028251 File Offset: 0x00026451
		public void DismountDatabase(IADDatabase database, int flags)
		{
			AmRpcClientHelper.DismountDatabase(database, flags);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0002825A File Offset: 0x0002645A
		public void DismountDatabaseDirect(string serverToRpc, Guid dbGuid, AmDismountArg dismountArg)
		{
			AmRpcClientHelper.DismountDatabaseDirect(serverToRpc, dbGuid, dismountArg);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00028264 File Offset: 0x00026464
		public void AttemptCopyLastLogsDirect(string serverToRpc, Guid dbGuid, DatabaseMountDialOverride mountDialOverride, int numRetries, int e00timeoutMs, int networkIOtimeoutMs, int networkConnecttimeoutMs, string sourceServer, int actionCode, int skipValidationChecks, bool mountPending, string uniqueOperationId, int subactionAttemptNumber, ref AmAcllReturnStatus acllStatus)
		{
			AmRpcClientHelper.AttemptCopyLastLogsDirect(serverToRpc, dbGuid, mountDialOverride, numRetries, e00timeoutMs, networkIOtimeoutMs, networkConnecttimeoutMs, sourceServer, actionCode, skipValidationChecks, mountPending, uniqueOperationId, subactionAttemptNumber, ref acllStatus);
		}
	}
}
