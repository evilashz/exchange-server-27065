using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002A0 RID: 672
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SeederClient : IDisposeTrackable, IDisposable
	{
		// Token: 0x06001A3D RID: 6717 RVA: 0x0006E555 File Offset: 0x0006C755
		private SeederClient(string serverName, ServerVersion serverVersion, string databaseName, string sourceName, ReplayRpcClient client)
		{
			this.m_disposeTracker = this.GetDisposeTracker();
			this.m_serverName = serverName;
			this.m_serverVersion = serverVersion;
			this.m_databaseName = databaseName;
			this.m_sourceName = sourceName;
			this.m_client = client;
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0006E590 File Offset: 0x0006C790
		~SeederClient()
		{
			this.Dispose(false);
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0006E5C0 File Offset: 0x0006C7C0
		public string ServerName
		{
			get
			{
				return this.m_serverName;
			}
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0006E5C8 File Offset: 0x0006C7C8
		public static SeederClient Create(Server server, string databaseName, Server sourceServer)
		{
			if (server == null)
			{
				throw new ArgumentException("server cannot be null.", "server");
			}
			return SeederClient.Create(server.Fqdn, databaseName, (sourceServer == null) ? null : sourceServer.Fqdn, server.AdminDisplayVersion);
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0006E618 File Offset: 0x0006C818
		public static SeederClient Create(string serverName, string databaseName, string sourceName, ServerVersion serverVersion)
		{
			if (!ReplayRpcVersionControl.IsSeedRpcSupported(serverVersion))
			{
				throw new SeederRpcUnsupportedException(serverName, serverVersion.ToString(), ReplayRpcVersionControl.SeedRpcSupportVersion.ToString());
			}
			ExTraceGlobals.SeederClientTracer.TraceDebug<string, ServerVersion>(0L, "SeederClient is now being created for server '{0}' with version '0x{1:x}'.", serverName, serverVersion);
			ReplayRpcClient rpcClient = null;
			SeederRpcExceptionWrapper.Instance.ClientRetryableOperation(serverName, delegate
			{
				rpcClient = new ReplayRpcClient(serverName);
			});
			return new SeederClient(serverName, serverVersion, databaseName, sourceName, rpcClient);
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0006E6A7 File Offset: 0x0006C8A7
		public void Dispose()
		{
			if (!this.m_fDisposed)
			{
				if (this.m_disposeTracker != null)
				{
					this.m_disposeTracker.Dispose();
				}
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0006E6D4 File Offset: 0x0006C8D4
		public void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.m_fDisposed)
				{
					if (disposing)
					{
						this.m_client.Dispose();
						this.m_client = null;
					}
					this.m_fDisposed = true;
				}
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0006E730 File Offset: 0x0006C930
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SeederClient>(this);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0006E738 File Offset: 0x0006C938
		public void SuppressDisposeTracker()
		{
			if (this.m_disposeTracker != null)
			{
				this.m_disposeTracker.Suppress();
			}
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0006E83C File Offset: 0x0006CA3C
		public void PrepareDbSeedAndBegin(Guid dbGuid, bool fDeleteExistingLogs, bool fSafeDeleteExistingFiles, bool fAutoSuspend, bool fManualResume, bool fSeedDatabase, bool fSeedCiFiles, string networkId, string seedingPath, string sourceName, bool? compressOverride, bool? encryptOverride, SeederRpcFlags flags = SeederRpcFlags.None)
		{
			if (dbGuid == Guid.Empty)
			{
				throw new ArgumentException("An invalid Database Guid was specified.", "dbGuid");
			}
			RpcSeederArgs args = new RpcSeederArgs(dbGuid, fDeleteExistingLogs, fAutoSuspend, seedingPath, null, networkId, false, sourceName, null, fManualResume, fSeedDatabase, fSeedCiFiles, compressOverride, encryptOverride, 0, fSafeDeleteExistingFiles, flags);
			this.ValidateArgs(args);
			ExTraceGlobals.SeederClientTracer.TraceDebug<RpcSeederArgs>((long)this.GetHashCode(), "PrepareDbSeedAndBegin(): Constructed RpcSeederArgs: {0}", args);
			RpcErrorExceptionInfo errorInfo = null;
			RpcSeederStatus seedStatus = null;
			ServerVersion version = this.GetTestHookServerVersion();
			bool isSafeDeleteSupported = ReplayRpcVersionControl.IsSeedRpcSafeDeleteSupported(version);
			bool isSeedV5Supported = ReplayRpcVersionControl.IsSeedRpcV5Supported(version);
			SeederRpcExceptionWrapper.Instance.ClientRetryableOperation(this.m_serverName, delegate
			{
				if (isSeedV5Supported)
				{
					errorInfo = this.m_client.RpccPrepareDatabaseSeedAndBegin5(args, ref seedStatus);
					return;
				}
				if (isSafeDeleteSupported)
				{
					errorInfo = this.m_client.RpccPrepareDatabaseSeedAndBegin4(args, ref seedStatus);
					return;
				}
				if (!fSafeDeleteExistingFiles)
				{
					errorInfo = this.m_client.RpccPrepareDatabaseSeedAndBegin(args, ref seedStatus);
					return;
				}
				ExTraceGlobals.SeederClientTracer.TraceError<string, ServerVersion, ServerVersion>((long)this.GetHashCode(), "PrepareDbSeedAndBegin(): Server '{0}' does not support SafeDeleteExistingFiles RPC. Server version: {1}. Minimum supported version: {2}", this.m_serverName, version, ReplayRpcVersionControl.SeedRpcSafeDeleteSupportVersion);
				throw new SeederRpcSafeDeleteUnsupportedException(this.m_serverName, version.ToString(), ReplayRpcVersionControl.SeedRpcSafeDeleteSupportVersion.ToString());
			});
			SeederRpcExceptionWrapper.Instance.ClientRethrowIfFailed(this.m_databaseName, this.m_serverName, errorInfo);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0006EA10 File Offset: 0x0006CC10
		public void BeginServerLevelSeed(bool fDeleteExistingLogs, bool fSafeDeleteExistingFiles, int maxSeedsInParallel, bool fAutoSuspend, bool fManualResume, bool fSeedDatabase, bool fSeedCiFiles, bool? compressOverride, bool? encryptOverride, SeederRpcFlags flags = SeederRpcFlags.None)
		{
			RpcSeederArgs args = new RpcSeederArgs(Guid.Empty, fDeleteExistingLogs, fAutoSuspend, null, null, string.Empty, false, string.Empty, null, fManualResume, fSeedDatabase, fSeedCiFiles, compressOverride, encryptOverride, maxSeedsInParallel, fSafeDeleteExistingFiles, flags);
			this.ValidateArgs(args);
			ExTraceGlobals.SeederClientTracer.TraceDebug<RpcSeederArgs>((long)this.GetHashCode(), "BeginServerLevelSeed(): Constructed RpcSeederArgs: {0}", args);
			RpcErrorExceptionInfo errorInfo = null;
			RpcSeederStatus seedStatus = null;
			ServerVersion version = this.GetTestHookServerVersion();
			SeederRpcExceptionWrapper.Instance.ClientRetryableOperation(this.m_serverName, delegate
			{
				if (ReplayRpcVersionControl.IsSeedRpcV5Supported(version))
				{
					errorInfo = this.m_client.RpccPrepareDatabaseSeedAndBegin5(args, ref seedStatus);
					return;
				}
				if (ReplayRpcVersionControl.IsSeedRpcSafeDeleteSupported(version))
				{
					errorInfo = this.m_client.RpccPrepareDatabaseSeedAndBegin4(args, ref seedStatus);
					return;
				}
				ExTraceGlobals.SeederClientTracer.TraceError<string, ServerVersion, ServerVersion>((long)this.GetHashCode(), "BeginServerLevelSeed(): Server '{0}' does not support server-level reseed RPC. Server version: {1}. Minimum supported version: {2}", this.m_serverName, version, ReplayRpcVersionControl.SeedRpcSafeDeleteSupportVersion);
				throw new SeederRpcServerLevelUnsupportedException(this.m_serverName, version.ToString(), ReplayRpcVersionControl.SeedRpcSafeDeleteSupportVersion.ToString());
			});
			SeederRpcExceptionWrapper.Instance.ClientRethrowIfFailed(this.m_databaseName, this.m_serverName, errorInfo);
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0006EB00 File Offset: 0x0006CD00
		public RpcSeederStatus GetDatabaseSeedStatus(Guid dbGuid)
		{
			if (dbGuid == Guid.Empty)
			{
				throw new ArgumentException("An invalid Database Guid was specified.", "dbGuid");
			}
			ExTraceGlobals.SeederClientTracer.TraceDebug<Guid>((long)this.GetHashCode(), "GetDatabaseSeedStatus(): calling server RPC for guid ({0}).", dbGuid);
			RpcErrorExceptionInfo errorExceptionInfo = null;
			RpcSeederStatus seedStatus = null;
			SeederRpcExceptionWrapper.Instance.ClientRetryableOperation(this.m_serverName, delegate
			{
				errorExceptionInfo = this.m_client.RpccGetDatabaseSeedStatus(dbGuid, ref seedStatus);
			});
			SeederRpcExceptionWrapper.Instance.ClientRethrowIfFailed(this.m_databaseName, this.m_serverName, errorExceptionInfo);
			return seedStatus;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0006EBD4 File Offset: 0x0006CDD4
		public void CancelDbSeed(Guid dbGuid)
		{
			if (dbGuid == Guid.Empty)
			{
				throw new ArgumentException("An invalid Database Guid was specified.", "dbGuid");
			}
			RpcErrorExceptionInfo errorInfo = null;
			ExTraceGlobals.SeederClientTracer.TraceDebug<Guid>((long)this.GetHashCode(), "CancelDbSeed(): calling server RPC for guid ({0}).", dbGuid);
			SeederRpcExceptionWrapper.Instance.ClientRetryableOperation(this.m_serverName, delegate
			{
				errorInfo = this.m_client.CancelDbSeed(dbGuid);
			});
			SeederRpcExceptionWrapper.Instance.ClientRethrowIfFailed(this.m_databaseName, this.m_serverName, errorInfo);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0006EC98 File Offset: 0x0006CE98
		public void EndDbSeed(Guid dbGuid)
		{
			if (dbGuid == Guid.Empty)
			{
				throw new ArgumentException("An invalid Database Guid was specified.", "dbGuid");
			}
			RpcErrorExceptionInfo errorInfo = null;
			ExTraceGlobals.SeederClientTracer.TraceDebug<Guid>((long)this.GetHashCode(), "EndDbSeed(): calling server RPC for guid ({0}).", dbGuid);
			SeederRpcExceptionWrapper.Instance.ClientRetryableOperation(this.m_serverName, delegate
			{
				errorInfo = this.m_client.EndDbSeed(dbGuid);
			});
			SeederRpcExceptionWrapper.Instance.ClientRethrowIfFailed(this.m_databaseName, this.m_serverName, errorInfo);
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0006ED36 File Offset: 0x0006CF36
		private void ValidateArgs(RpcSeederArgs args)
		{
			if (!args.SeedDatabase && !args.SeedCiFiles)
			{
				throw new ArgumentException("One of SeedDatabase and SeedCiFiles must be specified.");
			}
			if (args.SafeDeleteExistingFiles && args.DeleteExistingFiles)
			{
				throw new ArgumentException("Only one of SafeDeleteExistingFiles and DeleteExistingFiles can be specified at once.");
			}
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0006ED70 File Offset: 0x0006CF70
		private ServerVersion GetTestHookServerVersion()
		{
			ServerVersion serverVersion = this.m_serverVersion;
			if (RegistryTestHook.TargetServerVersionOverride > 0)
			{
				serverVersion = new ServerVersion(RegistryTestHook.TargetServerVersionOverride);
				ExTraceGlobals.SeederClientTracer.TraceDebug<string, ServerVersion>(0L, "GetTestHookServerVersion( {0} ) is returning TargetServerVersionOverride registry override of '{1}'.", this.m_serverName, serverVersion);
			}
			return serverVersion;
		}

		// Token: 0x04000A85 RID: 2693
		private ReplayRpcClient m_client;

		// Token: 0x04000A86 RID: 2694
		private string m_serverName;

		// Token: 0x04000A87 RID: 2695
		private string m_databaseName;

		// Token: 0x04000A88 RID: 2696
		private string m_sourceName;

		// Token: 0x04000A89 RID: 2697
		private ServerVersion m_serverVersion;

		// Token: 0x04000A8A RID: 2698
		private bool m_fDisposed;

		// Token: 0x04000A8B RID: 2699
		private DisposeTracker m_disposeTracker;

		// Token: 0x020002A1 RID: 673
		// (Invoke) Token: 0x06001A4E RID: 6734
		private delegate void SeederRpcOperation();
	}
}
