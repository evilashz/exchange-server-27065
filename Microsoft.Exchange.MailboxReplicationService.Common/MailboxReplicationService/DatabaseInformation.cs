using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000157 RID: 343
	internal struct DatabaseInformation
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0001AEB3 File Offset: 0x000190B3
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x0001AEBB File Offset: 0x000190BB
		public Guid MdbGuid { get; private set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0001AEC4 File Offset: 0x000190C4
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x0001AECC File Offset: 0x000190CC
		public string DatabaseName { get; private set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0001AED5 File Offset: 0x000190D5
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x0001AEDD File Offset: 0x000190DD
		public string ServerDN { get; private set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0001AEE6 File Offset: 0x000190E6
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x0001AEEE File Offset: 0x000190EE
		public string ServerFqdn { get; private set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0001AEF7 File Offset: 0x000190F7
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x0001AEFF File Offset: 0x000190FF
		public Guid ServerGuid { get; private set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0001AF08 File Offset: 0x00019108
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x0001AF10 File Offset: 0x00019110
		public ADObjectId ServerSite { get; private set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0001AF19 File Offset: 0x00019119
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x0001AF21 File Offset: 0x00019121
		public int ServerVersion { get; private set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0001AF2A File Offset: 0x0001912A
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x0001AF32 File Offset: 0x00019132
		public Guid SystemMailboxGuid { get; private set; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0001AF3B File Offset: 0x0001913B
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x0001AF43 File Offset: 0x00019143
		public MailboxRelease MailboxRelease { get; private set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0001AF4C File Offset: 0x0001914C
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x0001AF54 File Offset: 0x00019154
		public string ForestFqdn { get; private set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0001AF5D File Offset: 0x0001915D
		public bool IsMissing
		{
			get
			{
				return string.IsNullOrEmpty(this.DatabaseName);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0001AF6C File Offset: 0x0001916C
		public bool IsOnThisServer
		{
			get
			{
				if (Guid.Empty.Equals(this.ServerGuid))
				{
					return StringComparer.OrdinalIgnoreCase.Equals(this.ServerFqdn, CommonUtils.LocalComputerName);
				}
				return this.ServerGuid == CommonUtils.LocalServerGuid;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0001AFB4 File Offset: 0x000191B4
		public bool IsInLocalSite
		{
			get
			{
				return ADObjectId.Equals(this.ServerSite, CommonUtils.LocalSiteId);
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0001AFC8 File Offset: 0x000191C8
		public static DatabaseInformation FromDatabaseLocationInfo(Guid mdbGuid, DatabaseLocationInfo location, Guid systemMailboxGuid)
		{
			return new DatabaseInformation
			{
				MdbGuid = mdbGuid,
				DatabaseName = location.DatabaseName,
				ServerDN = location.ServerLegacyDN,
				ServerFqdn = location.ServerFqdn,
				ServerGuid = location.ServerGuid,
				ServerSite = location.ServerSite,
				ServerVersion = location.ServerVersion,
				MailboxRelease = location.MailboxRelease,
				SystemMailboxGuid = systemMailboxGuid
			};
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0001B04C File Offset: 0x0001924C
		public static DatabaseInformation FromBackEndServer(ADObjectId database, BackEndServer backend)
		{
			return new DatabaseInformation
			{
				MdbGuid = database.ObjectGuid,
				DatabaseName = database.Name,
				ServerFqdn = backend.Fqdn,
				ServerVersion = backend.Version
			};
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0001B098 File Offset: 0x00019298
		public static DatabaseInformation Missing(Guid mdbGuid, string forestFqdn)
		{
			return new DatabaseInformation
			{
				MdbGuid = mdbGuid,
				ForestFqdn = (forestFqdn ?? "Local Resource Forest")
			};
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0001B0C8 File Offset: 0x000192C8
		public static DatabaseInformation FromAD(Database db, MiniServer server, Guid systemMailboxGuid)
		{
			MailboxRelease mailboxRelease;
			return new DatabaseInformation
			{
				MdbGuid = db.Guid,
				DatabaseName = db.Name,
				ServerDN = server.ExchangeLegacyDN,
				ServerFqdn = server.Fqdn,
				ServerGuid = server.Guid,
				ServerSite = server.ServerSite,
				ServerVersion = server.VersionNumber,
				SystemMailboxGuid = systemMailboxGuid,
				MailboxRelease = (Enum.TryParse<MailboxRelease>((string)server[ActiveDirectoryServerSchema.MailboxRelease], true, out mailboxRelease) ? mailboxRelease : MailboxRelease.None)
			};
		}
	}
}
