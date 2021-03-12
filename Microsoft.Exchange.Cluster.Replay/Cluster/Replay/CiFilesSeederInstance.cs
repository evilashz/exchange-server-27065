using System;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.RpcEndpoint;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000290 RID: 656
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CiFilesSeederInstance : SeederInstanceBase, IReplicaSeederCallback
	{
		// Token: 0x060019AA RID: 6570 RVA: 0x0006B4E8 File Offset: 0x000696E8
		public CiFilesSeederInstance(RpcSeederArgs rpcArgs, ConfigurationArgs configArgs) : base(rpcArgs, configArgs)
		{
			ITopologyConfigurationSession adSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 73, ".ctor", "f:\\15.00.1497\\sources\\dev\\cluster\\src\\Replay\\seeder\\cifileseederinstance.cs");
			this.targetServer = CiFilesSeederInstance.GetLocalServer(adSession);
			if (!string.IsNullOrEmpty(rpcArgs.SourceMachineName) && !SharedHelper.StringIEquals(rpcArgs.SourceMachineName, configArgs.SourceMachine))
			{
				this.m_fPassiveSeeding = true;
			}
			Server server = this.m_fPassiveSeeding ? CiFilesSeederInstance.GetServerByName(adSession, rpcArgs.SourceMachineName) : CiFilesSeederInstance.GetServerByName(adSession, configArgs.SourceMachine);
			string indexSystemName = FastIndexVersion.GetIndexSystemName(this.ConfigArgs.IdentityGuid);
			this.targetIndexSeeder = new IndexSeeder(indexSystemName);
			this.sourceSeederProvider = new CiFileSeederProvider(server.Fqdn, this.targetServer.Fqdn, this.ConfigArgs.IdentityGuid);
			this.sourceSeederProvider.NetworkName = this.SeederArgs.NetworkId;
			this.sourceSeederProvider.CompressOverride = this.SeederArgs.CompressOverride;
			this.sourceSeederProvider.EncryptOverride = this.SeederArgs.EncryptOverride;
			base.ReadSeedTestHook();
			ExTraceGlobals.SeederServerTracer.TraceDebug<string, string>((long)this.GetHashCode(), "CiFilesSeederInstance constructed with the following arguments: {0}; {1}", this.SeederArgs.ToString(), this.ConfigArgs.ToString());
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0006B633 File Offset: 0x00069833
		public override string Identity
		{
			get
			{
				return SafeInstanceTable<CiFilesSeederInstance>.GetIdentityFromGuid(this.SeederArgs.InstanceGuid);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (set) Token: 0x060019AC RID: 6572 RVA: 0x0006B645 File Offset: 0x00069845
		private int PerfmonSeedingPercent
		{
			set
			{
				if (value < 0 || value > 100)
				{
					throw new ArgumentOutOfRangeException("percentage must between 0 and 100");
				}
				if (base.SeederPerfmonInstance != null)
				{
					base.SeederPerfmonInstance.CiSeedingPercent.RawValue = (long)value;
				}
			}
		}

		// Token: 0x1700070D RID: 1805
		// (set) Token: 0x060019AD RID: 6573 RVA: 0x0006B675 File Offset: 0x00069875
		private bool PerfmonSeedingInProgress
		{
			set
			{
				if (base.SeederPerfmonInstance != null)
				{
					base.SeederPerfmonInstance.CiSeedingInProgress.RawValue = (value ? 1L : 0L);
				}
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0006B698 File Offset: 0x00069898
		public static Server GetServerByName(string serverName)
		{
			ITopologyConfigurationSession adSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 168, "GetServerByName", "f:\\15.00.1497\\sources\\dev\\cluster\\src\\Replay\\seeder\\cifileseederinstance.cs");
			return CiFilesSeederInstance.GetServerByName(adSession, serverName);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0006B6CC File Offset: 0x000698CC
		public void PrepareCiFileSeeding()
		{
			try
			{
				SeederState seederState;
				SeederState seederState2;
				if (!this.UpdateState(SeederState.SeedPrepared, out seederState, out seederState2))
				{
					base.LogError(new SeederOperationAbortedException());
				}
			}
			catch (TaskServerTransientException ex)
			{
				base.LogError(ex);
			}
			catch (TaskServerException ex2)
			{
				base.LogError(ex2);
			}
			base.CheckOperationCancelled();
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0006B734 File Offset: 0x00069934
		public void CancelCiFileSeed()
		{
			lock (this.locker)
			{
				if (!this.m_fcancelled)
				{
					SeederState seederState;
					SeederState seederState2;
					if (this.UpdateState(SeederState.SeedCancelled, out seederState, out seederState2) && seederState2 == SeederState.SeedInProgress)
					{
						this.PerformSeedingAction(delegate(CiFileSeederProvider provider)
						{
							provider.CancelSeeding();
						});
					}
					this.m_fcancelled = true;
				}
			}
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0006B7B4 File Offset: 0x000699B4
		public void ReportProgress(string edbName, long edbSize, long bytesRead, long bytesWritten)
		{
			lock (this.locker)
			{
				this.m_seederStatus.FileFullPath = edbName;
				this.m_seederStatus.BytesTotal = edbSize;
				this.m_seederStatus.BytesRead = bytesRead;
				this.m_seederStatus.BytesWritten = bytesWritten;
				ExTraceGlobals.SeederServerTracer.TraceDebug<int>((long)this.GetHashCode(), "CiFilesSeederInstance.UpdateProgress: Progress percentage = {0}%", this.m_seederStatus.PercentComplete);
				this.PerfmonSeedingPercent = this.m_seederStatus.PercentComplete;
			}
			this.TestHook();
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0006B858 File Offset: 0x00069A58
		public bool IsBackupCancelled()
		{
			return this.m_fcancelled;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0006B860 File Offset: 0x00069A60
		protected override void ResetPerfmonSeedingProgress()
		{
			this.PerfmonSeedingPercent = 0;
			this.PerfmonSeedingInProgress = false;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0006B870 File Offset: 0x00069A70
		protected override void CloseSeeding(bool wasSeedSuccessful)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "CiFilesSeederInstance.CloseSeeding( {0} ) called for {1} ({2})", wasSeedSuccessful.ToString(), this.ConfigArgs.Name, this.Identity);
			if (wasSeedSuccessful)
			{
				lock (this.locker)
				{
					SeederState seederState;
					SeederState seederState2;
					this.UpdateState(SeederState.SeedSuccessful, out seederState, out seederState2);
					this.m_seederStatus.ErrorInfo = new RpcErrorExceptionInfo();
					this.m_lastErrorMessage = null;
					ReplayEventLogConstants.Tuple_CiSeedInstanceSuccess.LogEvent(null, new object[]
					{
						this.ConfigArgs.Name
					});
				}
				this.IncreasePerfmonSeedingSuccesses();
			}
			else
			{
				this.IncreasePerfmonSeedingFailures();
			}
			this.PerfmonSeedingInProgress = false;
			this.Cleanup();
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0006B940 File Offset: 0x00069B40
		protected override void Cleanup()
		{
			if (this.targetIndexSeeder != null)
			{
				this.targetIndexSeeder.Dispose();
				this.targetIndexSeeder = null;
			}
			if (this.sourceSeederProvider != null)
			{
				this.sourceSeederProvider.Close();
				this.sourceSeederProvider = null;
			}
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0006B9AC File Offset: 0x00069BAC
		protected override void SeedThreadProcInternal()
		{
			base.CheckOperationCancelled();
			this.PerfmonSeedingInProgress = true;
			ReplayCrimsonEvents.CISeedingBegins.Log<Guid, string, string>(base.DatabaseGuid, base.DatabaseName, this.SeederArgs.Flags.ToString());
			Exception ex = null;
			try
			{
				string targetEndPoint = this.GetSeedingEndPoint();
				int length = targetEndPoint.IndexOf('/');
				string oldValue = targetEndPoint.Substring(0, length);
				targetEndPoint = targetEndPoint.Replace(oldValue, this.targetServer.Fqdn);
				ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "CiFilesSeederInstance: GetSeedingEndPoint returned {0}", targetEndPoint);
				base.CheckOperationCancelled();
				this.PerformSeedingAction(delegate(CiFileSeederProvider provider)
				{
					provider.SeedCatalog(targetEndPoint, this, this.SeederArgs.Flags.ToString());
				});
				ReplayCrimsonEvents.CISeedingTryResumeIndexing.Log<Guid, string, string>(base.DatabaseGuid, base.DatabaseName, string.Empty);
				this.TryResumeIndexing();
				this.CloseSeeding(true);
			}
			catch (SeederServerException ex2)
			{
				ex = ex2;
				throw;
			}
			finally
			{
				string text = "Success";
				if (ex is SeederOperationAbortedException)
				{
					text = "Aborted";
				}
				else if (ex != null)
				{
					text = "Failed";
				}
				ReplayCrimsonEvents.CISeedingCompleted.Log<Guid, string, string, string, string>(base.DatabaseGuid, base.DatabaseName, this.SeederArgs.Flags.ToString(), text, (ex == null) ? string.Empty : ex.ToString());
			}
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0006BB28 File Offset: 0x00069D28
		protected override void CallFailedDbSeed(ExEventLog.EventTuple tuple, Exception ex)
		{
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0006BB2C File Offset: 0x00069D2C
		private static Server GetLocalServer(ITopologyConfigurationSession adSession)
		{
			return adSession.FindServerByName(Environment.MachineName);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0006BB48 File Offset: 0x00069D48
		private static Server GetServerByName(ITopologyConfigurationSession adSession, string serverName)
		{
			serverName = SharedHelper.GetNodeNameFromFqdn(serverName);
			return adSession.FindServerByName(serverName);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0006BB66 File Offset: 0x00069D66
		private void IncreasePerfmonSeedingSuccesses()
		{
			if (base.SeederPerfmonInstance != null)
			{
				base.SeederPerfmonInstance.CiSeedingSuccesses.Increment();
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0006BB81 File Offset: 0x00069D81
		private void IncreasePerfmonSeedingFailures()
		{
			if (base.SeederPerfmonInstance != null)
			{
				base.SeederPerfmonInstance.CiSeedingFailures.Increment();
			}
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0006BB9C File Offset: 0x00069D9C
		private string GetSeedingEndPoint()
		{
			PerformingFastOperationException ex = null;
			ExDateTime utcNow = ExDateTime.UtcNow;
			TimeSpan t = TimeSpan.FromSeconds((double)RegistryParameters.WaitForCatalogReadyTimeoutInSec);
			TimeSpan timeout = TimeSpan.FromSeconds((double)RegistryParameters.CheckCatalogReadyIntervalInSec);
			string seedingEndPoint;
			for (;;)
			{
				try
				{
					seedingEndPoint = this.targetIndexSeeder.GetSeedingEndPoint();
					break;
				}
				catch (PerformingFastOperationException ex2)
				{
					ExTraceGlobals.SeederServerTracer.TraceError<string, PerformingFastOperationException>((long)this.GetHashCode(), "CiFilesSeederInstance: Failed to get seeding endpoint for database ({0}): {1}", this.ConfigArgs.Name, ex2);
					ex = ex2;
				}
				ExDateTime utcNow2 = ExDateTime.UtcNow;
				if (utcNow2 - utcNow >= t)
				{
					base.LogError(ex);
				}
				base.CheckOperationCancelled();
				Thread.Sleep(timeout);
			}
			return seedingEndPoint;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0006BC40 File Offset: 0x00069E40
		private void PerformSeedingAction(Action<CiFileSeederProvider> action)
		{
			try
			{
				action(this.sourceSeederProvider);
			}
			catch (PerformingFastOperationException ex)
			{
				base.LogError(ex);
			}
			catch (NetworkRemoteException innerException)
			{
				base.LogError(new PerformingFastOperationException(innerException));
			}
			catch (NetworkTransportException innerException2)
			{
				base.LogError(new PerformingFastOperationException(innerException2));
			}
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0006BCAC File Offset: 0x00069EAC
		private void TryResumeIndexing()
		{
			if (RegistryParameters.IgnoreCatalogHealthSetByCI)
			{
				return;
			}
			ExTraceGlobals.SeederServerTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "CiFilesSeederInstance: Notify search service to resume indexing for database {0} ({1})", this.ConfigArgs.Name, this.ConfigArgs.IdentityGuid);
			SearchServiceRpcClient searchServiceRpcClient = null;
			bool discard = false;
			try
			{
				searchServiceRpcClient = RpcConnectionPool.GetSearchRpcClient();
				searchServiceRpcClient.ResumeIndexing(this.ConfigArgs.IdentityGuid);
			}
			catch (RpcException arg)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<RpcException>((long)this.GetHashCode(), "CiFilesSeederInstance: ResumeIndexing threw: {0}", arg);
				discard = true;
			}
			finally
			{
				if (searchServiceRpcClient != null)
				{
					RpcConnectionPool.ReturnSearchRpcClientToCache(ref searchServiceRpcClient, discard);
				}
			}
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0006BD50 File Offset: 0x00069F50
		private void TestHook()
		{
			if (this.m_testHookSeedDelayPerCallback > 0)
			{
				Thread.Sleep(this.m_testHookSeedDelayPerCallback);
			}
		}

		// Token: 0x04000A4F RID: 2639
		private readonly Server targetServer;

		// Token: 0x04000A50 RID: 2640
		private CiFileSeederProvider sourceSeederProvider;

		// Token: 0x04000A51 RID: 2641
		private IIndexSeederTarget targetIndexSeeder;

		// Token: 0x04000A52 RID: 2642
		private object locker = new object();
	}
}
