using System;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002B3 RID: 691
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SeedManager : IServiceComponent
	{
		// Token: 0x06001B09 RID: 6921 RVA: 0x0007424C File Offset: 0x0007244C
		public SeedManager(IReplicaInstanceManager replicaInstanceManager)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "SeedManager instance is now being constructed.");
			this.m_replicaInstanceManager = replicaInstanceManager;
			this.m_seederInstances = new SeederInstances();
			this.m_cleaner = new SeederInstanceCleaner(this.m_seederInstances);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00074298 File Offset: 0x00072498
		internal SeedManager(IReplicaInstanceManager replicaInstanceManager, SeederInstances seederInstances, int maxDurationMs)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "SeedManager instance is now being constructed using the test constructor.");
			this.m_replicaInstanceManager = replicaInstanceManager;
			this.m_seederInstances = seederInstances;
			this.m_cleaner = new SeederInstanceCleaner(this.m_seederInstances, maxDurationMs);
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x000742D6 File Offset: 0x000724D6
		public string Name
		{
			get
			{
				return "Seed Manager";
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000742DD File Offset: 0x000724DD
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.SeedManager;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000742E0 File Offset: 0x000724E0
		public bool IsCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x000742E3 File Offset: 0x000724E3
		public bool IsEnabled
		{
			get
			{
				return !ThirdPartyManager.IsInitialized || !ThirdPartyManager.IsThirdPartyReplicationEnabled;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000742F6 File Offset: 0x000724F6
		public bool IsRetriableOnError
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x000742F9 File Offset: 0x000724F9
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x00074301 File Offset: 0x00072501
		public bool Started
		{
			get
			{
				return this.m_fStarted;
			}
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0007430C File Offset: 0x0007250C
		public bool Start()
		{
			bool fStarted;
			lock (this)
			{
				if (!this.m_fStarted)
				{
					EseHelper.GlobalInit();
					this.m_cleaner.Start();
					this.m_fStarted = true;
				}
				else
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "SeedManager is already started.");
				}
				fStarted = this.m_fStarted;
			}
			return fStarted;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x00074380 File Offset: 0x00072580
		public void Stop()
		{
			lock (this)
			{
				if (this.m_fStarted)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "SeedManager is now being stopped.");
					this.m_cleaner.Stop();
					this.m_cleaner = null;
					this.StopInstances();
					if (this.m_serverSeeder != null)
					{
						this.m_serverSeeder.Stop();
						this.m_serverSeeder = null;
					}
					this.m_fStarted = false;
				}
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0007440C File Offset: 0x0007260C
		public void BeginServerLevelSeed(RpcSeederArgs seederArgs)
		{
			lock (this)
			{
				if (!this.m_fStarted)
				{
					ExTraceGlobals.SeederServerTracer.TraceError((long)this.GetHashCode(), "SeedManager.BeginServerLevelSeed(): Skipping seeds because SeedManager is being stopped.");
					ReplayCrimsonEvents.FullServerSeedSkippedShutdown.Log();
					throw new FullServerSeedSkippedShutdownException();
				}
				if (this.m_serverSeeder != null && !this.m_serverSeeder.StopCalled)
				{
					ExTraceGlobals.SeederServerTracer.TraceError((long)this.GetHashCode(), "SeedManager.BeginServerLevelSeed(): Another server-seed is in progress.");
					ReplayCrimsonEvents.FullServerSeedAlreadyInProgress.Log();
					throw new FullServerSeedInProgressException();
				}
				this.m_serverSeeder = new FullServerReseeder(seederArgs);
				this.m_serverSeeder.Start();
			}
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000744C4 File Offset: 0x000726C4
		public void PrepareDbSeedAndBegin(RpcSeederArgs seederArgs)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "SeedManager: PrepareDbSeedAndBegin() called.");
			SeederInstanceContainer seederInstanceContainer;
			bool flag = this.m_seederInstances.TryGetInstance(seederArgs.InstanceGuid, out seederInstanceContainer);
			if (flag)
			{
				this.ThrowExceptionForExistingInstance(seederArgs, seederInstanceContainer);
			}
			ExTraceGlobals.SeederServerTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "SeedManager: A SeederInstanceContainer does NOT already exist for DB '{0}' ({1}).", seederArgs.DatabaseName, seederArgs.InstanceGuid);
			Dependencies.ADConfig.Refresh("SeedManager.PrepareDbSeedAndBegin");
			ReplayConfiguration replayConfiguration;
			this.CheckDbValidReplicationTarget(seederArgs, out replayConfiguration);
			ConfigurationArgs configurationArgs = new ConfigurationArgs(replayConfiguration, this.m_replicaInstanceManager);
			seederInstanceContainer = new SeederInstanceContainer(seederArgs, configurationArgs);
			try
			{
				this.m_seederInstances.AddInstance(seederInstanceContainer);
				ReplayEventLogConstants.Tuple_SeedInstancePrepareAdded.LogEvent(null, new object[]
				{
					configurationArgs.Name,
					seederArgs.ToString()
				});
			}
			catch (ArgumentException arg)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<string, ArgumentException>((long)this.GetHashCode(), "SeedManager: SeederInstanceContainer for db '{0}' has already been added. This indicates another PrepareDbSeed() call got to add it just before this one. Ex: {1}", replayConfiguration.Name, arg);
				throw new SeederInstanceAlreadyAddedException(seederInstanceContainer.SeedingSource);
			}
			try
			{
				seederInstanceContainer.PrepareDbSeed();
				ReplayEventLogConstants.Tuple_SeedInstancePrepareSucceeded.LogEvent(null, new object[]
				{
					configurationArgs.Name
				});
			}
			finally
			{
				SeederState seedState = seederInstanceContainer.SeedState;
				if (seedState != SeederState.SeedPrepared)
				{
					this.m_seederInstances.RemoveInstance(seederInstanceContainer);
					ExTraceGlobals.SeederServerTracer.TraceDebug<string, SeederState>((long)this.GetHashCode(), "SeedManager: SeederInstanceContainer for db '{0}' is being removed from table because PrepareDbSeed() did not pass (state ={1}, expected was SeedPrepared).", replayConfiguration.Name, seedState);
					ReplayEventLogConstants.Tuple_SeedInstancePrepareUnknownError.LogEvent(null, new object[]
					{
						configurationArgs.Name
					});
				}
			}
			ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "SeedManager: SeederInstanceContainer for db '{0} is being queued for seeding since PrepareDbSeed() passed.", replayConfiguration.Name);
			try
			{
				seederInstanceContainer.BeginDbSeed();
				ReplayEventLogConstants.Tuple_SeedInstanceBeginSucceeded.LogEvent(null, new object[]
				{
					configurationArgs.Name
				});
			}
			finally
			{
				SeederState seedState2 = seederInstanceContainer.SeedState;
				if (seedState2 != SeederState.SeedInProgress && seedState2 != SeederState.SeedSuccessful)
				{
					this.m_seederInstances.RemoveInstance(seederInstanceContainer);
					ExTraceGlobals.SeederServerTracer.TraceDebug<string, SeederState>((long)this.GetHashCode(), "SeedManager: SeederInstanceContainer for db '{0}' is being removed from table because BeginDbSeed() did not pass (state = {1}).", replayConfiguration.Name, seedState2);
					ReplayEventLogConstants.Tuple_SeedInstanceBeginUnknownError.LogEvent(null, new object[]
					{
						configurationArgs.Name
					});
				}
			}
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00074700 File Offset: 0x00072900
		public RpcSeederStatus GetDbSeedStatus(Guid dbGuid)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedManager: GetDbSeedStatus() called for DB ({0}).", dbGuid);
			SeederInstanceContainer seederInstanceContainer;
			if (this.m_seederInstances.TryGetInstance(SafeInstanceTable<SeederInstanceContainer>.GetIdentityFromGuid(dbGuid), out seederInstanceContainer))
			{
				ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedManager: Found instance for DB ({0}).", dbGuid);
				return seederInstanceContainer.GetSeedStatus();
			}
			ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedManager: A SeederInstanceContainer does NOT already exist for DB ({0}).", dbGuid);
			throw new SeederInstanceNotFoundException(dbGuid.ToString());
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00074784 File Offset: 0x00072984
		public void CancelDbSeed(Guid dbGuid, bool fAdminRequested)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedManager: CancelDbSeed() called for DB ({0}).", dbGuid);
			SeederInstanceContainer seederInstanceContainer;
			if (this.m_seederInstances.TryGetInstance(SafeInstanceTable<SeederInstanceContainer>.GetIdentityFromGuid(dbGuid), out seederInstanceContainer))
			{
				ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedManager: Found instance for DB ({0}).", dbGuid);
				seederInstanceContainer.CancelDbSeed();
				if (fAdminRequested)
				{
					ReplayEventLogConstants.Tuple_SeedInstanceCancelRequestedByAdmin.LogEvent(null, new object[]
					{
						seederInstanceContainer.Name
					});
				}
				ReplayCrimsonEvents.SeedingCancelled.Log<Guid, string, bool>(dbGuid, seederInstanceContainer.Name, fAdminRequested);
				return;
			}
			ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedManager: A SeederInstanceContainer does NOT already exist for DB ({0}).", dbGuid);
			throw new SeederInstanceNotFoundException(dbGuid.ToString());
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00074838 File Offset: 0x00072A38
		public void EndDbSeed(Guid dbGuid)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedManager: EndDbSeed() called for DB ({0}).", dbGuid);
			SeederInstanceContainer seederInstanceContainer;
			if (!this.m_seederInstances.TryGetInstance(SafeInstanceTable<SeederInstanceContainer>.GetIdentityFromGuid(dbGuid), out seederInstanceContainer))
			{
				ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedManager: Did NOT find instance for DB ({0})!", dbGuid);
				throw new SeederInstanceNotFoundException(dbGuid.ToString());
			}
			SeederState seedState = seederInstanceContainer.SeedState;
			ExTraceGlobals.SeederServerTracer.TraceDebug<Guid, SeederState>((long)this.GetHashCode(), "SeedManager: Found instance for DB ({0}) in state '{1}'.", dbGuid, seedState);
			if (seedState == SeederState.SeedSuccessful || seedState == SeederState.SeedCancelled || seedState == SeederState.SeedFailed)
			{
				this.m_seederInstances.RemoveInstance(seederInstanceContainer);
				ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedManager: Removed seeder instance for DB ({0}) from table.", dbGuid);
				ReplayEventLogConstants.Tuple_SeedInstanceCleanupRequestedByAdmin.LogEvent(null, new object[]
				{
					seederInstanceContainer.Name
				});
				return;
			}
			throw new SeederInstanceInvalidStateForEndException(dbGuid.ToString());
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00074918 File Offset: 0x00072B18
		private void CheckDbValidReplicationTarget(RpcSeederArgs seederArgs, out ReplayConfiguration replayConfig)
		{
			replayConfig = null;
			ADReplicationRetryTimer adreplicationRetryTimer = new ADReplicationRetryTimer();
			bool flag = !seederArgs.SeedDatabase && seederArgs.SeedCiFiles;
			try
			{
				bool flag2;
				while (!this.IsDBCurrentReplicaInstance(seederArgs.InstanceGuid, out replayConfig, out flag2))
				{
					if (SeedHelper.IsDbPendingLcrRcrTarget(seederArgs.InstanceGuid, out replayConfig, out flag2))
					{
						if (flag2)
						{
							if (flag)
							{
								ExTraceGlobals.SeederServerTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "SeedManager: Database '{0}' ({1}) is not a valid RCR replica target but the requested seeding is CI only.", replayConfig.Name, seederArgs.InstanceGuid);
								return;
							}
							this.HandleDbCopyNotTarget(seederArgs, replayConfig);
						}
						ExTraceGlobals.SeederServerTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "SeedManager: Database '{0}' ({1}) is a valid RCR replica target.", replayConfig.Name, seederArgs.InstanceGuid);
						return;
					}
					ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedManager: Database '{0}' is NOT a valid RCR replica target!", seederArgs.InstanceGuid);
					if (adreplicationRetryTimer.IsExpired)
					{
						throw new InvalidDbForSeedSpecifiedException();
					}
					adreplicationRetryTimer.Sleep();
				}
				if (flag2)
				{
					if (flag)
					{
						ExTraceGlobals.SeederServerTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "SeedManager: Database '{0}' ({1}) is not running as a valid RCR replica target but the requested seeding is CI only.", replayConfig.Name, seederArgs.InstanceGuid);
						return;
					}
					this.HandleDbCopyNotTarget(seederArgs, replayConfig);
				}
				ExTraceGlobals.SeederServerTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "SeedManager: Database '{0}' ({1}) is currently running as a valid RCR replica target.", replayConfig.Name, seederArgs.InstanceGuid);
			}
			catch (DataSourceOperationException ex)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<DataSourceOperationException>((long)this.GetHashCode(), "SeedManager: CheckDBValidReplicationTarget: Exception encountered: {0}", ex);
				throw new SeedPrepareException(ex.ToString(), ex);
			}
			catch (DataValidationException ex2)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<DataValidationException>((long)this.GetHashCode(), "SeedManager: CheckDBValidReplicationTarget: Exception encountered: {0}", ex2);
				throw new SeedPrepareException(ex2.ToString(), ex2);
			}
			catch (ObjectNotFoundException ex3)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<ObjectNotFoundException>((long)this.GetHashCode(), "SeedManager: CheckDBValidReplicationTarget: Exception encountered: {0}", ex3);
				throw new SeedPrepareException(ex3.ToString(), ex3);
			}
			catch (StoragePermanentException ex4)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<StoragePermanentException>((long)this.GetHashCode(), "SeedManager: CheckDBValidReplicationTarget: Exception encountered: {0}", ex4);
				throw new SeedPrepareException(ex4.ToString(), ex4);
			}
			catch (TransientException ex5)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<TransientException>((long)this.GetHashCode(), "SeedManager: CheckDBValidReplicationTarget: Exception encountered: {0}", ex5);
				throw new SeedPrepareException(ex5.ToString(), ex5);
			}
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00074B9C File Offset: 0x00072D9C
		private void HandleDbCopyNotTarget(RpcSeederArgs seederArgs, ReplayConfiguration replayConfig)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "SeedManager: Database '{0}' ({1}) is NOT a replication target on the local machine. It is a source!", replayConfig.Name, seederArgs.InstanceGuid);
			DbCopyNotTargetException ex = new DbCopyNotTargetException(replayConfig.DatabaseName, Environment.MachineName);
			throw ex;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00074BE0 File Offset: 0x00072DE0
		private bool IsDBCurrentReplicaInstance(Guid guid, out ReplayConfiguration replayConfig, out bool fSource)
		{
			replayConfig = null;
			fSource = false;
			ReplicaInstance replicaInstance;
			if (this.m_replicaInstanceManager.TryGetReplicaInstance(guid, out replicaInstance))
			{
				replayConfig = replicaInstance.Configuration;
				fSource = (replayConfig.Type == ReplayConfigType.RemoteCopySource);
				return true;
			}
			return false;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00074C1C File Offset: 0x00072E1C
		private void ThrowExceptionForExistingInstance(RpcSeederArgs seederArgs, SeederInstanceContainer seederInstance)
		{
			SeederState seedState = seederInstance.SeedState;
			ExTraceGlobals.SeederServerTracer.TraceError<string, Guid, SeederState>((long)this.GetHashCode(), "SeedManager: A SeederInstanceContainer already exists for DB '{0}' ({1}) and is in SeederState '{2}'.", seederArgs.DatabaseName, seederArgs.InstanceGuid, seedState);
			if (seedState == SeederState.Unknown)
			{
				throw new SeederInstanceAlreadyAddedException(seederInstance.SeedingSource);
			}
			if (seedState == SeederState.SeedPrepared)
			{
				throw new SeederInstanceAlreadyAddedException(seederInstance.SeedingSource);
			}
			if (seedState == SeederState.SeedInProgress)
			{
				throw new SeederInstanceAlreadyInProgressException(seederInstance.SeedingSource);
			}
			if (seedState == SeederState.SeedSuccessful)
			{
				throw new SeederInstanceAlreadyCompletedException(seederInstance.SeedingSource);
			}
			if (seedState == SeederState.SeedCancelled)
			{
				throw new SeederInstanceAlreadyCancelledException(seederInstance.SeedingSource);
			}
			if (seedState == SeederState.SeedFailed)
			{
				throw new SeederInstanceAlreadyFailedException(seederInstance.GetSeedStatus(), seederInstance.SeedingSource);
			}
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00074CB8 File Offset: 0x00072EB8
		private void StopInstances()
		{
			SeederInstanceContainer[] allInstances = this.m_seederInstances.GetAllInstances();
			foreach (SeederInstanceContainer seederInstanceContainer in allInstances)
			{
				seederInstanceContainer.CancelDbSeed();
				ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "SeedManager: StopInstances: CancelDbSeed() issued for DB ({0}) as part of shutdown.", seederInstanceContainer.Identity);
			}
			foreach (SeederInstanceContainer seederInstanceContainer2 in allInstances)
			{
				this.m_seederInstances.RemoveInstance(seederInstanceContainer2);
				ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "SeedManager: StopInstances: Seeder instance for DB ({0}) removed as part of shutdown.", seederInstanceContainer2.Identity);
			}
			if (allInstances.Length > 0)
			{
				ReplayEventLogConstants.Tuple_SeedInstancesStoppedServiceShutdown.LogEvent(null, new object[0]);
			}
		}

		// Token: 0x04000AD6 RID: 2774
		private IReplicaInstanceManager m_replicaInstanceManager;

		// Token: 0x04000AD7 RID: 2775
		private SeederInstances m_seederInstances;

		// Token: 0x04000AD8 RID: 2776
		private SeederInstanceCleaner m_cleaner;

		// Token: 0x04000AD9 RID: 2777
		private FullServerReseeder m_serverSeeder;

		// Token: 0x04000ADA RID: 2778
		private bool m_fStarted;
	}
}
