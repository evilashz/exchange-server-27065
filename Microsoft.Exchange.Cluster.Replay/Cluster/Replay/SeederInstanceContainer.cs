using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002A9 RID: 681
	internal class SeederInstanceContainer : IIdentityGuid
	{
		// Token: 0x06001A9D RID: 6813 RVA: 0x00071EC8 File Offset: 0x000700C8
		public SeederInstanceContainer(RpcSeederArgs rpcArgs, ConfigurationArgs configArgs)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 66, ".ctor", "f:\\15.00.1497\\sources\\dev\\cluster\\src\\Replay\\seeder\\seederinstancecontainer.cs");
			Database database = null;
			Exception ex = null;
			try
			{
				database = topologyConfigurationSession.FindDatabaseByGuid<Database>(configArgs.IdentityGuid);
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
			catch (ADExternalException ex3)
			{
				ex = ex3;
			}
			catch (ADOperationException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				throw new SeedPrepareException(ReplayStrings.CouldNotFindDatabase(configArgs.IdentityGuid.ToString(), ex.ToString()), ex);
			}
			this.m_seederArgs = rpcArgs;
			this.m_configArgs = configArgs;
			this.m_seedDatabase = rpcArgs.SeedDatabase;
			this.m_seedCiFiles = (rpcArgs.SeedCiFiles && !database.IsPublicFolderDatabase);
			if (this.m_seedDatabase)
			{
				if (this.m_seedCiFiles)
				{
					this.m_databaseSeeder = new DatabaseSeederInstance(rpcArgs, configArgs, new SeedCompletionCallback(this.LaunchCiFileSeeder), null);
				}
				else
				{
					this.m_databaseSeeder = new DatabaseSeederInstance(rpcArgs, configArgs, null, null);
				}
			}
			if (this.m_seedCiFiles)
			{
				this.m_ciFilesSeeder = new CiFilesSeederInstance(rpcArgs, configArgs);
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x00071FFC File Offset: 0x000701FC
		public string Identity
		{
			get
			{
				return SafeInstanceTable<DatabaseSeederInstance>.GetIdentityFromGuid(this.m_seederArgs.InstanceGuid);
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x00072010 File Offset: 0x00070210
		public DateTime CompletedTimeUtc
		{
			get
			{
				DateTime result;
				lock (this)
				{
					DateTime dateTime = DateTime.MaxValue;
					SeederState seederState = SeederState.Unknown;
					if (this.m_databaseSeeder != null)
					{
						seederState = this.m_databaseSeeder.SeedState;
						dateTime = this.m_databaseSeeder.CompletedTimeUtc;
					}
					if (this.m_ciFilesSeeder != null && (seederState == SeederState.SeedSuccessful || seederState == SeederState.Unknown))
					{
						seederState = this.m_ciFilesSeeder.SeedState;
						dateTime = this.m_ciFilesSeeder.CompletedTimeUtc;
					}
					result = dateTime;
				}
				return result;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x0007209C File Offset: 0x0007029C
		// (set) Token: 0x06001AA1 RID: 6817 RVA: 0x00072108 File Offset: 0x00070308
		public SeederState SeedState
		{
			get
			{
				SeederState result;
				lock (this)
				{
					SeederState seederState = SeederState.Unknown;
					if (this.m_databaseSeeder != null)
					{
						seederState = this.m_databaseSeeder.SeedState;
					}
					if (this.m_ciFilesSeeder != null && (seederState == SeederState.SeedSuccessful || seederState == SeederState.Unknown))
					{
						seederState = this.m_ciFilesSeeder.SeedState;
					}
					result = seederState;
				}
				return result;
			}
			internal set
			{
				if (this.m_databaseSeeder != null)
				{
					this.m_databaseSeeder.SeedState = value;
				}
				if (this.m_ciFilesSeeder != null)
				{
					this.m_ciFilesSeeder.SeedState = value;
				}
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x00072132 File Offset: 0x00070332
		public string Name
		{
			get
			{
				return this.m_configArgs.Name;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x0007213F File Offset: 0x0007033F
		public string SeedingSource
		{
			get
			{
				if (!string.IsNullOrEmpty(this.m_seederArgs.SourceMachineName))
				{
					return this.m_seederArgs.SourceMachineName;
				}
				return this.m_configArgs.SourceMachine;
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0007216C File Offset: 0x0007036C
		public void PrepareDbSeed()
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "SeederInstanceContainer.PrepareDbSeed() entered.");
			if (this.m_databaseSeeder != null)
			{
				this.m_databaseSeeder.PrepareDbSeed();
			}
			if (this.m_databaseSeeder == null && this.m_ciFilesSeeder != null)
			{
				this.m_ciFilesSeeder.PrepareCiFileSeeding();
			}
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000721C0 File Offset: 0x000703C0
		public void BeginDbSeed()
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "SeederInstanceContainer.BeginDbSeed() entered.");
			if (this.m_databaseSeeder != null)
			{
				this.m_databaseSeeder.BeginDbSeed();
			}
			if (this.m_ciFilesSeeder != null && !this.m_seedDatabase)
			{
				this.m_ciFilesSeeder.BeginDbSeed();
			}
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00072211 File Offset: 0x00070411
		public void CancelDbSeed()
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "SeederInstanceContainer.CancelDbSeed() entered.");
			if (this.m_databaseSeeder != null)
			{
				this.m_databaseSeeder.CancelDbSeed();
			}
			if (this.m_ciFilesSeeder != null)
			{
				this.m_ciFilesSeeder.CancelCiFileSeed();
			}
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00072250 File Offset: 0x00070450
		public RpcSeederStatus GetSeedStatus()
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "SeederInstanceContainer.GetSeedStatus() entered.");
			RpcSeederStatus rpcSeederStatus = null;
			bool flag = true;
			bool flag2 = this.m_seedDatabase && this.m_seedCiFiles;
			if (this.m_databaseSeeder != null)
			{
				if (!flag2)
				{
					flag = false;
				}
				RpcSeederStatus seedStatus = this.m_databaseSeeder.GetSeedStatus();
				if (seedStatus.State != SeederState.SeedSuccessful)
				{
					flag = false;
				}
				rpcSeederStatus = SeederInstanceContainer.ScaleDatabaseSeedStatus(seedStatus, flag2);
			}
			if (flag)
			{
				rpcSeederStatus = this.m_ciFilesSeeder.GetSeedStatus();
				rpcSeederStatus = SeederInstanceContainer.ScaleCiSeedStatus(rpcSeederStatus, flag2);
			}
			return rpcSeederStatus;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000722CF File Offset: 0x000704CF
		public void WaitUntilStopped()
		{
			if (this.m_databaseSeeder != null)
			{
				this.m_databaseSeeder.WaitUntilStopped();
			}
			if (this.m_ciFilesSeeder != null)
			{
				this.m_ciFilesSeeder.WaitUntilStopped();
			}
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x000722F7 File Offset: 0x000704F7
		public void LaunchCiFileSeeder(bool successfulSeed)
		{
			if (this.m_seedCiFiles && successfulSeed)
			{
				this.m_ciFilesSeeder.PrepareCiFileSeeding();
				this.m_ciFilesSeeder.BeginDbSeed();
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0007231A File Offset: 0x0007051A
		internal void TestSetCompletedTimeUtc(DateTime completedTimeUtc)
		{
			if (this.m_databaseSeeder != null)
			{
				this.m_databaseSeeder.CompletedTimeUtc = completedTimeUtc;
			}
			if (this.m_ciFilesSeeder != null)
			{
				this.m_ciFilesSeeder.CompletedTimeUtc = completedTimeUtc;
			}
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00072344 File Offset: 0x00070544
		private static RpcSeederStatus ScaleDatabaseSeedStatus(RpcSeederStatus databaseStatus, bool performingTwoSeeds)
		{
			RpcSeederStatus rpcSeederStatus;
			if (!performingTwoSeeds)
			{
				rpcSeederStatus = databaseStatus;
			}
			else
			{
				rpcSeederStatus = new RpcSeederStatus(databaseStatus);
				rpcSeederStatus.BytesTotalDivisor = rpcSeederStatus.BytesTotal * 5L / 4L;
			}
			return rpcSeederStatus;
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00072374 File Offset: 0x00070574
		private static RpcSeederStatus ScaleCiSeedStatus(RpcSeederStatus ciSeedStatus, bool performingTwoSeeds)
		{
			RpcSeederStatus result = new RpcSeederStatus(ciSeedStatus);
			if (!performingTwoSeeds)
			{
				result = ciSeedStatus;
			}
			return result;
		}

		// Token: 0x04000AA7 RID: 2727
		private readonly RpcSeederArgs m_seederArgs;

		// Token: 0x04000AA8 RID: 2728
		private readonly ConfigurationArgs m_configArgs;

		// Token: 0x04000AA9 RID: 2729
		private readonly bool m_seedDatabase;

		// Token: 0x04000AAA RID: 2730
		private readonly bool m_seedCiFiles;

		// Token: 0x04000AAB RID: 2731
		private DatabaseSeederInstance m_databaseSeeder;

		// Token: 0x04000AAC RID: 2732
		private CiFilesSeederInstance m_ciFilesSeeder;
	}
}
