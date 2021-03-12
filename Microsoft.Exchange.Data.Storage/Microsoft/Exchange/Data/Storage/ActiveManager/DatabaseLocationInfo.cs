using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x02000300 RID: 768
	[Serializable]
	public class DatabaseLocationInfo
	{
		// Token: 0x060022C3 RID: 8899 RVA: 0x0008CA70 File Offset: 0x0008AC70
		internal static DatabaseLocationInfo CloneDatabaseLocationInfo(DatabaseLocationInfo right, DatabaseLocationInfoResult requestResult)
		{
			return new DatabaseLocationInfo(right.ServerFqdn, right.ServerLegacyDN, right.LastMountedServerFqdn, right.LastMountedServerLegacyDN, right.DatabaseLegacyDN, right.RpcClientAccessServerLegacyDN, right.DatabaseName, right.DatabaseIsPublic, right.DatabaseIsRestored, right.HomePublicFolderDatabaseGuid, right.MountedTime, right.serverGuid, right.ServerSite, right.AdminDisplayVersion, right.MailboxRelease, requestResult, right.IsDatabaseHighlyAvailable);
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x0008CAE5 File Offset: 0x0008ACE5
		// (set) Token: 0x060022C5 RID: 8901 RVA: 0x0008CAED File Offset: 0x0008ACED
		public string ServerFqdn
		{
			get
			{
				return this.serverFqdn;
			}
			private set
			{
				if (value != null)
				{
					this.serverFqdn = string.Intern(value);
					return;
				}
				this.serverFqdn = null;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060022C6 RID: 8902 RVA: 0x0008CB06 File Offset: 0x0008AD06
		// (set) Token: 0x060022C7 RID: 8903 RVA: 0x0008CB0E File Offset: 0x0008AD0E
		public string ServerLegacyDN
		{
			get
			{
				return this.serverLegacyDN;
			}
			private set
			{
				if (value != null)
				{
					this.serverLegacyDN = string.Intern(value);
					return;
				}
				this.serverLegacyDN = null;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x0008CB27 File Offset: 0x0008AD27
		// (set) Token: 0x060022C9 RID: 8905 RVA: 0x0008CB2F File Offset: 0x0008AD2F
		public string DatabaseLegacyDN { get; private set; }

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x0008CB38 File Offset: 0x0008AD38
		// (set) Token: 0x060022CB RID: 8907 RVA: 0x0008CB40 File Offset: 0x0008AD40
		public string RpcClientAccessServerLegacyDN { get; private set; }

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x0008CB49 File Offset: 0x0008AD49
		// (set) Token: 0x060022CD RID: 8909 RVA: 0x0008CB51 File Offset: 0x0008AD51
		public ADObjectId ServerSite { get; private set; }

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x0008CB5A File Offset: 0x0008AD5A
		// (set) Token: 0x060022CF RID: 8911 RVA: 0x0008CB62 File Offset: 0x0008AD62
		public ServerVersion AdminDisplayVersion { get; private set; }

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x0008CB6B File Offset: 0x0008AD6B
		public int ServerVersion
		{
			get
			{
				if (this.AdminDisplayVersion == null)
				{
					return 0;
				}
				return this.AdminDisplayVersion.ToInt();
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x0008CB88 File Offset: 0x0008AD88
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x0008CB90 File Offset: 0x0008AD90
		public MailboxRelease MailboxRelease { get; private set; }

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x0008CB99 File Offset: 0x0008AD99
		// (set) Token: 0x060022D4 RID: 8916 RVA: 0x0008CBA1 File Offset: 0x0008ADA1
		public bool IsDatabaseHighlyAvailable { get; private set; }

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x0008CBAA File Offset: 0x0008ADAA
		// (set) Token: 0x060022D6 RID: 8918 RVA: 0x0008CBB2 File Offset: 0x0008ADB2
		public string LastMountedServerFqdn
		{
			get
			{
				return this.lastMountedServerFqdn;
			}
			private set
			{
				if (value != null)
				{
					this.lastMountedServerFqdn = string.Intern(value);
					return;
				}
				this.lastMountedServerFqdn = null;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x0008CBCB File Offset: 0x0008ADCB
		// (set) Token: 0x060022D8 RID: 8920 RVA: 0x0008CBD3 File Offset: 0x0008ADD3
		public string LastMountedServerLegacyDN
		{
			get
			{
				return this.lastMountedServerLegacyDN;
			}
			private set
			{
				if (value != null)
				{
					this.lastMountedServerLegacyDN = string.Intern(value);
					return;
				}
				this.lastMountedServerLegacyDN = null;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x0008CBEC File Offset: 0x0008ADEC
		// (set) Token: 0x060022DA RID: 8922 RVA: 0x0008CBF4 File Offset: 0x0008ADF4
		public Guid HomePublicFolderDatabaseGuid { get; private set; }

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x0008CBFD File Offset: 0x0008ADFD
		// (set) Token: 0x060022DC RID: 8924 RVA: 0x0008CC05 File Offset: 0x0008AE05
		public DateTime MountedTime { get; private set; }

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x0008CC0E File Offset: 0x0008AE0E
		// (set) Token: 0x060022DE RID: 8926 RVA: 0x0008CC16 File Offset: 0x0008AE16
		public string DatabaseName { get; private set; }

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x0008CC1F File Offset: 0x0008AE1F
		// (set) Token: 0x060022E0 RID: 8928 RVA: 0x0008CC27 File Offset: 0x0008AE27
		public bool DatabaseIsPublic { get; private set; }

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x0008CC30 File Offset: 0x0008AE30
		// (set) Token: 0x060022E2 RID: 8930 RVA: 0x0008CC38 File Offset: 0x0008AE38
		public bool DatabaseIsRestored { get; private set; }

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x0008CC41 File Offset: 0x0008AE41
		// (set) Token: 0x060022E4 RID: 8932 RVA: 0x0008CC49 File Offset: 0x0008AE49
		public DatabaseLocationInfoResult RequestResult { get; private set; }

		// Token: 0x060022E5 RID: 8933 RVA: 0x0008CC52 File Offset: 0x0008AE52
		internal DatabaseLocationInfo(Server server, bool isDatabaseHighlyAvailable) : this(server.Fqdn, server.ExchangeLegacyDN, ActiveManagerUtil.GetServerSiteFromServer(server), server.AdminDisplayVersion, isDatabaseHighlyAvailable)
		{
			this.serverGuid = new Guid?(server.Guid);
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0008CC84 File Offset: 0x0008AE84
		internal DatabaseLocationInfo(string serverFqdn, string serverLegacyDN, ADObjectId serverSite, ServerVersion serverVersion, bool isDatabaseHighlyAvailable) : this(serverFqdn, serverLegacyDN, serverFqdn, serverLegacyDN, string.Empty, string.Empty, string.Empty, false, false, Guid.Empty, DateTime.MinValue, null, serverSite, serverVersion, MailboxRelease.None, DatabaseLocationInfoResult.Success, isDatabaseHighlyAvailable)
		{
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0008CCC8 File Offset: 0x0008AEC8
		internal DatabaseLocationInfo(string serverFqdn, string serverLegacyDN, string lastMountedServerFqdn, string lastMountedServerLegacyDN, string databaseLegacyDN, string rpcClientAcessServerLegacyDN, string databaseName, bool databaseIsPublic, bool databaseIsRestored, Guid homePublicFolderDatabaseGuid, DateTime mountedTime, Guid? serverGuid, ADObjectId serverSite, ServerVersion serverVersion, MailboxRelease mailboxRelease, DatabaseLocationInfoResult requestResult, bool isDatabaseHighlyAvailable)
		{
			this.UpdateInPlace(serverFqdn, serverLegacyDN, lastMountedServerFqdn, lastMountedServerLegacyDN, databaseLegacyDN, rpcClientAcessServerLegacyDN, databaseName, databaseIsPublic, databaseIsRestored, homePublicFolderDatabaseGuid, mountedTime, serverGuid, serverSite, serverVersion, mailboxRelease, requestResult, isDatabaseHighlyAvailable);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0008CD00 File Offset: 0x0008AF00
		internal void UpdateInPlace(string serverFqdn, string serverLegacyDN, string lastMountedServerFqdn, string lastMountedServerLegacyDN, string databaseLegacyDN, string rpcClientAcessServerLegacyDN, string databaseName, bool databaseIsPublic, bool databaseIsRestored, Guid homePublicFolderDatabaseGuid, DateTime mountedTime, Guid? serverGuid, ADObjectId serverSite, ServerVersion serverVersion, MailboxRelease mailboxRelease, DatabaseLocationInfoResult requestResult, bool isDatabaseHighlyAvailable)
		{
			if (serverFqdn != null && serverFqdn.Length == 0)
			{
				throw new ArgumentException("serverFqdn is empty", "serverFqdn");
			}
			if (serverLegacyDN != null && serverLegacyDN.Length == 0)
			{
				throw new ArgumentException("serverLegacyDN is empty", "serverLegacyDN");
			}
			if (lastMountedServerFqdn != null && lastMountedServerFqdn.Length == 0)
			{
				throw new ArgumentException("lastMountedServerFqdn is empty", "lastMountedServerFqdn");
			}
			if (lastMountedServerLegacyDN != null && lastMountedServerLegacyDN.Length == 0)
			{
				throw new ArgumentException("lastMountedServerLegacyDN is empty", "lastMountedServerLegacyDN");
			}
			this.ServerFqdn = serverFqdn;
			this.ServerLegacyDN = serverLegacyDN;
			this.DatabaseLegacyDN = databaseLegacyDN;
			this.RpcClientAccessServerLegacyDN = rpcClientAcessServerLegacyDN;
			this.DatabaseName = databaseName;
			this.DatabaseIsPublic = databaseIsPublic;
			this.DatabaseIsRestored = databaseIsRestored;
			this.HomePublicFolderDatabaseGuid = homePublicFolderDatabaseGuid;
			this.serverGuid = serverGuid;
			this.ServerSite = serverSite;
			this.AdminDisplayVersion = serverVersion;
			this.MailboxRelease = mailboxRelease;
			this.IsDatabaseHighlyAvailable = isDatabaseHighlyAvailable;
			this.LastMountedServerFqdn = (lastMountedServerFqdn ?? serverFqdn);
			this.LastMountedServerLegacyDN = (lastMountedServerLegacyDN ?? serverLegacyDN);
			this.MountedTime = mountedTime;
			this.RequestResult = requestResult;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0008CE0A File Offset: 0x0008B00A
		internal bool DetectFailover(DatabaseLocationInfo oldDatabaseLocationInfo)
		{
			return this.ServerSite != oldDatabaseLocationInfo.ServerSite || this.ServerLegacyDN != oldDatabaseLocationInfo.ServerLegacyDN;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0008CE30 File Offset: 0x0008B030
		internal virtual bool Equals(DatabaseLocationInfo cmpObj)
		{
			return cmpObj != null && (this.DatabaseIsPublic == cmpObj.DatabaseIsPublic && this.DatabaseIsRestored == cmpObj.DatabaseIsRestored && this.IsDatabaseHighlyAvailable == cmpObj.IsDatabaseHighlyAvailable && this.HomePublicFolderDatabaseGuid == cmpObj.HomePublicFolderDatabaseGuid && this.MailboxRelease == cmpObj.MailboxRelease && this.RequestResult == cmpObj.RequestResult && string.Equals(this.DatabaseLegacyDN, cmpObj.DatabaseLegacyDN) && string.Equals(this.DatabaseName, cmpObj.DatabaseName) && string.Equals(this.LastMountedServerFqdn, cmpObj.LastMountedServerFqdn) && string.Equals(this.LastMountedServerLegacyDN, cmpObj.LastMountedServerLegacyDN) && string.Equals(this.RpcClientAccessServerLegacyDN, cmpObj.RpcClientAccessServerLegacyDN) && string.Equals(this.ServerFqdn, cmpObj.ServerFqdn) && string.Equals(this.ServerLegacyDN, cmpObj.ServerLegacyDN) && this.MountedTime.Equals(cmpObj.MountedTime) && (object.ReferenceEquals(this.ServerSite, cmpObj.ServerSite) || (this.ServerSite != null && this.ServerSite.Equals(cmpObj.ServerSite))));
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0008CF84 File Offset: 0x0008B184
		public override string ToString()
		{
			return string.Format("({0};{1};{2};{3};{4};{5};{6};{7})", new object[]
			{
				this.ServerFqdn,
				this.ServerLegacyDN,
				this.ServerVersion,
				this.ServerSite,
				this.IsDatabaseHighlyAvailable,
				this.LastMountedServerFqdn ?? string.Empty,
				this.LastMountedServerLegacyDN ?? string.Empty,
				this.MountedTime
			});
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x0008D00C File Offset: 0x0008B20C
		public Guid ServerGuid
		{
			get
			{
				if (this.serverGuid == null)
				{
					IADToplogyConfigurationSession adSession = ADSessionFactory.CreateFullyConsistentRootOrgSession(true);
					ADObjectId adobjectId = ActiveManagerImplementation.TryGetServerIdByFqdn(new SimpleMiniServerLookup(adSession), this.ServerFqdn);
					this.serverGuid = new Guid?((adobjectId != null) ? adobjectId.ObjectGuid : Guid.Empty);
				}
				return this.serverGuid.Value;
			}
		}

		// Token: 0x04001446 RID: 5190
		private Guid? serverGuid;

		// Token: 0x04001447 RID: 5191
		private string serverFqdn;

		// Token: 0x04001448 RID: 5192
		private string serverLegacyDN;

		// Token: 0x04001449 RID: 5193
		private string lastMountedServerFqdn;

		// Token: 0x0400144A RID: 5194
		private string lastMountedServerLegacyDN;
	}
}
