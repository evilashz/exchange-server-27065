using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Cluster.ReplayService
{
	// Token: 0x02000003 RID: 3
	internal class RepairDatabaseAmState
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000021A7 File Offset: 0x000003A7
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000021AF File Offset: 0x000003AF
		internal IADDatabase Database { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000021B8 File Offset: 0x000003B8
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000021C0 File Offset: 0x000003C0
		internal IADServer Server { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000021C9 File Offset: 0x000003C9
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000021D1 File Offset: 0x000003D1
		internal IADDatabaseAvailabilityGroup Dag { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021DA File Offset: 0x000003DA
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000021E2 File Offset: 0x000003E2
		internal ReplayConfiguration ReplayConfiguration { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021EB File Offset: 0x000003EB
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021F3 File Offset: 0x000003F3
		internal FileChecker FileChecker { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021FC File Offset: 0x000003FC
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002204 File Offset: 0x00000404
		internal AmDbState DbState { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000220D File Offset: 0x0000040D
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002215 File Offset: 0x00000415
		internal bool IsVerbose { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000221E File Offset: 0x0000041E
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002226 File Offset: 0x00000426
		internal bool IsReplayRunning { get; private set; }

		// Token: 0x06000012 RID: 18 RVA: 0x00002230 File Offset: 0x00000430
		internal RepairDatabaseAmState(string dbName, bool isVerbose)
		{
			IADDatabase database = AmHelper.FindDatabaseByName(dbName, true);
			this.Initialize(database, isVerbose);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002254 File Offset: 0x00000454
		internal RepairDatabaseAmState(Guid dbGuid, bool isVerbose)
		{
			IADDatabase database = AmHelper.FindDatabaseByGuid(dbGuid, true);
			this.Initialize(database, isVerbose);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002278 File Offset: 0x00000478
		private void Initialize(IADDatabase database, bool isVerbose)
		{
			this.IsVerbose = isVerbose;
			this.Database = database;
			IADToplogyConfigurationSession iadtoplogyConfigurationSession = ADSessionFactory.CreateIgnoreInvalidRootOrgSession(true);
			this.Server = iadtoplogyConfigurationSession.FindServerByName(Environment.MachineName);
			if (this.Server == null)
			{
				throw new ServerNotFoundException(Environment.MachineName);
			}
			this.Dag = iadtoplogyConfigurationSession.FindDagByServer(this.Server);
			if (this.Database.ReplicationType == ReplicationType.Remote)
			{
				this.LogVerbose("Database is replicated", new object[0]);
			}
			else
			{
				this.LogVerbose("Database is not replicated", new object[0]);
			}
			this.ReplayConfiguration = RemoteReplayConfiguration.TaskGetReplayConfig(this.Dag, this.Database, this.Server);
			this.LogVerbose("ReplayConfiguration is initialized", new object[0]);
			this.FileChecker = ReplicaInstance.ConstructFileChecker(this.ReplayConfiguration);
			this.LogVerbose("FileChecker initialized", new object[0]);
			this.DbState = this.ConstructAmDbState(this.Server);
			this.LogVerbose("Persistent database state is initialized", new object[0]);
			this.IsReplayRunning = AmHelper.IsReplayRunning(AmServerName.LocalComputerName);
			this.LogVerbose("Replay service is{0} running", new object[]
			{
				this.IsReplayRunning ? string.Empty : " not"
			});
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023AF File Offset: 0x000005AF
		internal virtual void LogVerbose(string formatString, params object[] args)
		{
			if (this.IsVerbose)
			{
				Console.WriteLine(formatString, args);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023C0 File Offset: 0x000005C0
		internal virtual void LogMessage(string formatString, params object[] args)
		{
			Console.WriteLine(formatString, args);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023CC File Offset: 0x000005CC
		internal AmDbState ConstructAmDbState(IADServer localServer)
		{
			if (localServer.DatabaseAvailabilityGroup == null)
			{
				throw new RepairStateLocalServerIsNotInDagException(localServer.Name);
			}
			this.LogVerbose("This machine is part of a DAG", new object[0]);
			if (!AmCluster.IsRunning())
			{
				throw new RepairStateClusterIsNotRunningException();
			}
			this.LogVerbose("Cluster is running on the local machine", new object[0]);
			AmCluster cluster = AmCluster.Open();
			return new AmClusterDbState(cluster);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000242C File Offset: 0x0000062C
		internal static Exception HandleKnownExceptions(EventHandler ev)
		{
			Exception result = null;
			try
			{
				result = AmHelper.HandleKnownExceptions(ev);
			}
			catch (FileCheckException ex)
			{
				result = ex;
			}
			catch (RepairStateException ex2)
			{
				result = ex2;
			}
			catch (LastLogReplacementException ex3)
			{
				result = ex3;
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000247C File Offset: 0x0000067C
		internal void MakeLocalServerTheActiveServer(AmServerName activeServerName)
		{
			this.LogVerbose("Updating active server", new object[0]);
			AmDbStateInfo amDbStateInfo = this.DbState.Read(this.Database.Guid);
			amDbStateInfo.ActiveServer = activeServerName;
			amDbStateInfo.LastMountedServer = activeServerName;
			amDbStateInfo.MountStatus = MountStatus.Dismounted;
			amDbStateInfo.IsAdminDismounted = true;
			amDbStateInfo.IsAutomaticActionsAllowed = false;
			this.DbState.Write(amDbStateInfo);
			this.LogVerbose("Successfully updated active server", new object[0]);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024F4 File Offset: 0x000006F4
		internal void CreateTempLogFileIfRequired()
		{
			this.LogVerbose("Creating temp log file", new object[0]);
			Exception ex;
			if (!ReplicaInstance.CreateTempLogFile(this.ReplayConfiguration, out ex))
			{
				throw new RepairStateFailedToCreateTempLogFileException(this.Database.Name, ex.Message, ex);
			}
			this.LogVerbose("Successfully created temp log file", new object[0]);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000254C File Offset: 0x0000074C
		internal void RunPrereqChecks()
		{
			this.LogMessage("*************** Running prereq checks ***************", new object[0]);
			try
			{
				this.VerifyDatabaseIsDismounted();
				this.LogMessage("..... Verified that database is dismounted", new object[0]);
				if (this.IsReplayRunning && this.Database.ReplicationType == ReplicationType.Remote)
				{
					this.VerifyDatabaseIsSuspended();
					this.LogMessage("..... Verified that database is suspended", new object[0]);
				}
				this.VerifyNoPendingPagePatch();
				this.VerifyFileChecker();
				this.LogMessage("..... Verified that all database files are in valid state", new object[0]);
			}
			finally
			{
				this.LogMessage("************** Finished prereq checks ***************", new object[0]);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025F0 File Offset: 0x000007F0
		internal void VerifyDatabaseIsDismounted()
		{
			AmDbStateInfo amDbStateInfo = this.DbState.Read(this.Database.Guid);
			if (!AmServerName.IsNullOrEmpty(amDbStateInfo.ActiveServer) && AmStoreHelper.IsMounted(amDbStateInfo.ActiveServer, this.Database.Guid))
			{
				throw new RepairStateDatabaseShouldBeDismounted(this.Database.Name, amDbStateInfo.ActiveServer.NetbiosName);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002655 File Offset: 0x00000855
		internal void VerifyDatabaseIsSuspended()
		{
			if (!this.ReplayConfiguration.ReplayState.SuspendLockRemote.SuspendWanted)
			{
				throw new RepairStateDatabaseCopyShouldBeSuspendedException(this.Database.Name);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002680 File Offset: 0x00000880
		private void VerifyNoPendingPagePatch()
		{
			try
			{
				if (IncrementalReseeder.CheckForInterruptedPatch(this.ReplayConfiguration, null))
				{
					throw new RepairStateFailedPendingPagePatchException(this.Database.Name, string.Empty);
				}
			}
			catch (IncrementalReseedFailedException ex)
			{
				throw new RepairStateFailedPendingPagePatchException(this.Database.Name, ex.Message, ex);
			}
			catch (IncrementalReseedRetryableException ex2)
			{
				throw new RepairStateFailedPendingPagePatchException(this.Database.Name, ex2.Message, ex2);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002704 File Offset: 0x00000904
		internal void VerifyFileChecker()
		{
			LastLogReplacer.RollbackLastLogIfNecessary(this.ReplayConfiguration);
			this.FileChecker.RunChecks();
		}
	}
}
