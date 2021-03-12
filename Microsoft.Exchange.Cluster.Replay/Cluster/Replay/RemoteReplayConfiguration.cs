using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.Cluster;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200013F RID: 319
	internal class RemoteReplayConfiguration : ReplayConfiguration
	{
		// Token: 0x06000C40 RID: 3136 RVA: 0x0003643C File Offset: 0x0003463C
		public static RemoteReplayConfiguration TaskGetReplayConfig(IADDatabaseAvailabilityGroup dag, IADDatabase db, IADServer server)
		{
			DatabaseLocationInfo databaseLocationInfo;
			bool flag = RemoteReplayConfiguration.IsServerRcrSource(db, server, out databaseLocationInfo);
			if (flag)
			{
				return new RemoteReplayConfiguration(dag, db, server, server.Fqdn, LockType.Remote, ReplayConfigType.RemoteCopySource);
			}
			return new RemoteReplayConfiguration(dag, db, server, databaseLocationInfo.ServerFqdn, LockType.Remote, ReplayConfigType.RemoteCopyTarget);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00036478 File Offset: 0x00034678
		public static bool IsServerRcrSource(IADDatabase db, IADServer server, out DatabaseLocationInfo activeLocation)
		{
			ActiveManager noncachingActiveManagerInstance = ActiveManager.GetNoncachingActiveManagerInstance();
			activeLocation = noncachingActiveManagerInstance.GetServerForDatabase(db.Guid, GetServerForDatabaseFlags.BasicQuery);
			return Cluster.StringIEquals(activeLocation.ServerFqdn, server.Fqdn);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000364AC File Offset: 0x000346AC
		public static bool IsServerRcrSource(IADDatabase db, string serverName, ITopologyConfigurationSession adSession, out DatabaseLocationInfo activeLocation)
		{
			bool result;
			using (ActiveManager activeManager = ActiveManager.CreateCustomActiveManager(false, null, null, null, null, null, null, adSession, true))
			{
				activeLocation = activeManager.GetServerForDatabase(db.Guid, GetServerForDatabaseFlags.BasicQuery);
				result = Cluster.StringIEquals(new AmServerName(activeLocation.ServerFqdn).NetbiosName, serverName);
			}
			return result;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0003650C File Offset: 0x0003470C
		public static RemoteReplayConfiguration ServiceGetReplayConfig(IADDatabaseAvailabilityGroup dag, IADDatabase db, IADServer server, string activeFqdn, ReplayConfigType type)
		{
			return new RemoteReplayConfiguration(dag, db, server, activeFqdn, LockType.ReplayService, type);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0003651C File Offset: 0x0003471C
		public static bool IsServerValidRcrTarget(IADDatabase database, IADServer server, out int maxDB, string sourceDomain, bool fThrow)
		{
			maxDB = 0;
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			if (server.IsMailboxServer)
			{
				if (server.Edition == ServerEditionType.Enterprise || server.Edition == ServerEditionType.EnterpriseEvaluation)
				{
					maxDB = 100;
				}
				else
				{
					maxDB = 5;
				}
				IADDatabaseCopy[] databaseCopies = database.DatabaseCopies;
				int i = 0;
				while (i < databaseCopies.Length)
				{
					IADDatabaseCopy iaddatabaseCopy = databaseCopies[i];
					if (string.Equals(iaddatabaseCopy.HostServerName, server.Name))
					{
						if (fThrow)
						{
							throw new InvalidRcrConfigAlreadyHostsDb(server.Name, database.Name);
						}
						return false;
					}
					else
					{
						i++;
					}
				}
				return true;
			}
			if (fThrow)
			{
				throw new InvalidRcrConfigOnNonMailboxException(server.Name);
			}
			return false;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000365B8 File Offset: 0x000347B8
		private RemoteReplayConfiguration(IADDatabaseAvailabilityGroup dag, IADDatabase database, IADServer server, string activeFqdn, LockType lockType, ReplayConfigType type)
		{
			try
			{
				if (database == null)
				{
					throw new NullDatabaseException();
				}
				if (server == null)
				{
					throw new ErrorNullServerFromDb(database.Name);
				}
				if (activeFqdn == null)
				{
					throw new ArgumentException("Caller must provide the active node");
				}
				IADDatabaseCopy databaseCopy = database.GetDatabaseCopy(server.Name);
				if (databaseCopy == null)
				{
					throw new NullDbCopyException();
				}
				this.m_server = server;
				this.m_database = database;
				this.m_targetNodeFqdn = server.Fqdn;
				this.m_sourceNodeFqdn = activeFqdn;
				this.m_type = type;
				this.m_autoDatabaseMountDial = this.m_server.AutoDatabaseMountDial;
				if (type == ReplayConfigType.RemoteCopyTarget)
				{
					this.m_replayState = ReplayState.GetReplayState(this.m_targetNodeFqdn, this.m_sourceNodeFqdn, lockType, this.Identity, this.Database.Name);
				}
				else
				{
					this.m_replayState = ReplayState.GetReplayState(this.m_sourceNodeFqdn, this.m_sourceNodeFqdn, lockType, this.Identity, this.Database.Name);
				}
				this.m_replayLagTime = databaseCopy.ReplayLagTime;
				this.m_truncationLagTime = databaseCopy.TruncationLagTime;
				this.m_activationPreference = databaseCopy.ActivationPreference;
				base.PopulatePropertiesFromDag(dag);
			}
			finally
			{
				this.BuildDebugString();
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x000366E4 File Offset: 0x000348E4
		public virtual AutoDatabaseMountDial AutoDatabaseMountDial
		{
			get
			{
				return this.m_autoDatabaseMountDial;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x000366EC File Offset: 0x000348EC
		public override EnhancedTimeSpan ReplayLagTime
		{
			get
			{
				return this.m_replayLagTime;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x000366F4 File Offset: 0x000348F4
		public override EnhancedTimeSpan TruncationLagTime
		{
			get
			{
				return this.m_truncationLagTime;
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000366FC File Offset: 0x000348FC
		public ReplayConfiguration ConfigurationPathConflict(Dictionary<string, ReplayConfiguration> currentConfigurations, out string field)
		{
			field = string.Empty;
			foreach (ReplayConfiguration replayConfiguration in currentConfigurations.Values)
			{
				if (replayConfiguration.Type == ReplayConfigType.RemoteCopyTarget)
				{
					if (Cluster.StringIEquals(this.DestinationLogPath, replayConfiguration.DestinationLogPath))
					{
						field = "DestinationLogPath:" + replayConfiguration.DestinationLogPath;
						return replayConfiguration;
					}
					if (Cluster.StringIEquals(this.DestinationEdbPath, replayConfiguration.DestinationEdbPath))
					{
						field = "DestinationEdbPath:" + replayConfiguration.DestinationEdbPath;
						return replayConfiguration;
					}
					if (Cluster.StringIEquals(this.DestinationSystemPath, replayConfiguration.DestinationSystemPath))
					{
						field = "DestinationSystemPath:" + replayConfiguration.DestinationSystemPath;
						return replayConfiguration;
					}
				}
			}
			return null;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000367DC File Offset: 0x000349DC
		public bool ConfigurationPathConflict(IADDatabase[] databases, out string field)
		{
			field = string.Empty;
			if (databases != null)
			{
				int i = 0;
				while (i < databases.Length)
				{
					IADDatabase iaddatabase = databases[i];
					bool result;
					if (Cluster.StringIEquals(this.DestinationLogPath, iaddatabase.LogFolderPath.PathName))
					{
						field = "DestinationLogPath:" + this.DestinationLogPath;
						result = true;
					}
					else if (Cluster.StringIEquals(this.DestinationEdbPath, iaddatabase.EdbFilePath.PathName))
					{
						field = "DestinationEdbPath:" + this.DestinationEdbPath;
						result = true;
					}
					else
					{
						if (!Cluster.StringIEquals(this.DestinationSystemPath, iaddatabase.SystemFolderPath.PathName))
						{
							i++;
							continue;
						}
						field = "DestinationSystemPath:" + this.DestinationSystemPath;
						result = true;
					}
					return result;
				}
			}
			return false;
		}

		// Token: 0x04000539 RID: 1337
		private AutoDatabaseMountDial m_autoDatabaseMountDial;
	}
}
