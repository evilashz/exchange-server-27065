using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADServerWrapper : ADObjectWrapperBase, IADServer, IADObjectCommon
	{
		// Token: 0x06000106 RID: 262 RVA: 0x00003BF0 File Offset: 0x00001DF0
		private void FinishConstruction(ADObject sourceObj)
		{
			this.Fqdn = (string)sourceObj[ServerSchema.Fqdn];
			this.Edition = (ServerEditionType)sourceObj[ServerSchema.Edition];
			this.VersionNumber = (int)sourceObj[ServerSchema.VersionNumber];
			this.MajorVersion = (int)sourceObj[ServerSchema.MajorVersion];
			this.AdminDisplayVersion = (ServerVersion)sourceObj[ServerSchema.AdminDisplayVersion];
			this.IsExchange2007OrLater = (bool)sourceObj[ServerSchema.IsExchange2007OrLater];
			this.IsE14OrLater = (bool)sourceObj[ServerSchema.IsE14OrLater];
			string value = (string)sourceObj[ActiveDirectoryServerSchema.MailboxRelease];
			MailboxRelease mailboxRelease;
			this.MailboxRelease = (Enum.TryParse<MailboxRelease>(value, true, out mailboxRelease) ? mailboxRelease : MailboxRelease.None);
			this.CurrentServerRole = (ServerRole)sourceObj[ServerSchema.CurrentServerRole];
			this.IsMailboxServer = (bool)sourceObj[ServerSchema.IsMailboxServer];
			this.ServerSite = (ADObjectId)sourceObj[ServerSchema.ServerSite];
			this.ExchangeLegacyDN = (string)sourceObj[ServerSchema.ExchangeLegacyDN];
			this.DatabaseAvailabilityGroup = (ADObjectId)sourceObj[ServerSchema.DatabaseAvailabilityGroup];
			this.AutoDatabaseMountDial = (AutoDatabaseMountDial)sourceObj[ActiveDirectoryServerSchema.AutoDatabaseMountDialType];
			this.DatabaseCopyAutoActivationPolicy = (DatabaseCopyAutoActivationPolicyType)sourceObj[ActiveDirectoryServerSchema.DatabaseCopyAutoActivationPolicy];
			this.DatabaseCopyActivationDisabledAndMoveNow = (bool)sourceObj[ActiveDirectoryServerSchema.DatabaseCopyActivationDisabledAndMoveNow];
			this.AutoDagServerConfigured = (bool)sourceObj[ActiveDirectoryServerSchema.AutoDagServerConfigured];
			this.MaximumActiveDatabases = (int?)sourceObj[ServerSchema.MaxActiveMailboxDatabases];
			this.MaximumPreferredActiveDatabases = (int?)sourceObj[ServerSchema.MaxPreferredActiveDatabases];
			this.ContinuousReplicationMaxMemoryPerDatabase = (long?)sourceObj[ActiveDirectoryServerSchema.ContinuousReplicationMaxMemoryPerDatabase];
			this.ComponentStates = (MultiValuedProperty<string>)sourceObj[ServerSchema.ComponentStates];
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003DDC File Offset: 0x00001FDC
		internal ADServerWrapper(IADServer source) : base(source)
		{
			this.Fqdn = source.Fqdn;
			this.Edition = source.Edition;
			this.VersionNumber = source.VersionNumber;
			this.MajorVersion = source.MajorVersion;
			this.AdminDisplayVersion = source.AdminDisplayVersion;
			this.IsExchange2007OrLater = source.IsExchange2007OrLater;
			this.IsE14OrLater = source.IsE14OrLater;
			this.MailboxRelease = source.MailboxRelease;
			this.CurrentServerRole = source.CurrentServerRole;
			this.IsMailboxServer = source.IsMailboxServer;
			this.ServerSite = source.ServerSite;
			this.ExchangeLegacyDN = source.ExchangeLegacyDN;
			this.DatabaseAvailabilityGroup = source.DatabaseAvailabilityGroup;
			this.AutoDatabaseMountDial = source.AutoDatabaseMountDial;
			this.DatabaseCopyAutoActivationPolicy = source.DatabaseCopyAutoActivationPolicy;
			this.DatabaseCopyActivationDisabledAndMoveNow = source.DatabaseCopyActivationDisabledAndMoveNow;
			this.AutoDagServerConfigured = source.AutoDagServerConfigured;
			this.MaximumActiveDatabases = source.MaximumActiveDatabases;
			this.MaximumPreferredActiveDatabases = source.MaximumPreferredActiveDatabases;
			this.ContinuousReplicationMaxMemoryPerDatabase = source.ContinuousReplicationMaxMemoryPerDatabase;
			this.ComponentStates = source.ComponentStates;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003EEC File Offset: 0x000020EC
		private ADServerWrapper(Server server) : base(server)
		{
			this.FinishConstruction(server);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003EFC File Offset: 0x000020FC
		public static ADServerWrapper CreateWrapper(Server server)
		{
			if (server == null)
			{
				return null;
			}
			return new ADServerWrapper(server);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003F09 File Offset: 0x00002109
		private ADServerWrapper(MiniServer server) : base(server)
		{
			this.FinishConstruction(server);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00003F19 File Offset: 0x00002119
		public static ADServerWrapper CreateWrapper(MiniServer server)
		{
			if (server == null)
			{
				return null;
			}
			return new ADServerWrapper(server);
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00003F26 File Offset: 0x00002126
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00003F2E File Offset: 0x0000212E
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
			private set
			{
				if (value != null)
				{
					this.fqdn = string.Intern(value);
					return;
				}
				this.fqdn = null;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003F47 File Offset: 0x00002147
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00003F4F File Offset: 0x0000214F
		public bool IsE14OrLater { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00003F58 File Offset: 0x00002158
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00003F60 File Offset: 0x00002160
		public ServerVersion AdminDisplayVersion { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003F69 File Offset: 0x00002169
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00003F71 File Offset: 0x00002171
		public ServerRole CurrentServerRole { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00003F7A File Offset: 0x0000217A
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00003F82 File Offset: 0x00002182
		public ADObjectId ServerSite { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003F8B File Offset: 0x0000218B
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00003F93 File Offset: 0x00002193
		public ADObjectId DatabaseAvailabilityGroup { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00003F9C File Offset: 0x0000219C
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00003FA4 File Offset: 0x000021A4
		public DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00003FAD File Offset: 0x000021AD
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00003FB5 File Offset: 0x000021B5
		public bool DatabaseCopyActivationDisabledAndMoveNow { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003FBE File Offset: 0x000021BE
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00003FC6 File Offset: 0x000021C6
		public bool AutoDagServerConfigured { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00003FCF File Offset: 0x000021CF
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00003FD7 File Offset: 0x000021D7
		public bool IsMailboxServer { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00003FE0 File Offset: 0x000021E0
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00003FE8 File Offset: 0x000021E8
		public ServerEditionType Edition { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00003FF1 File Offset: 0x000021F1
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00003FF9 File Offset: 0x000021F9
		public int VersionNumber { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004002 File Offset: 0x00002202
		// (set) Token: 0x06000125 RID: 293 RVA: 0x0000400A File Offset: 0x0000220A
		public int? MaximumActiveDatabases { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004013 File Offset: 0x00002213
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000401B File Offset: 0x0000221B
		public int? MaximumPreferredActiveDatabases { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00004024 File Offset: 0x00002224
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000402C File Offset: 0x0000222C
		public AutoDatabaseMountDial AutoDatabaseMountDial { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004035 File Offset: 0x00002235
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000403D File Offset: 0x0000223D
		public long? ContinuousReplicationMaxMemoryPerDatabase { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004046 File Offset: 0x00002246
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000404E File Offset: 0x0000224E
		public int MajorVersion { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004057 File Offset: 0x00002257
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000405F File Offset: 0x0000225F
		public MailboxRelease MailboxRelease { get; private set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00004068 File Offset: 0x00002268
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00004070 File Offset: 0x00002270
		public bool IsExchange2007OrLater { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004079 File Offset: 0x00002279
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00004081 File Offset: 0x00002281
		public string ExchangeLegacyDN { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00004092 File Offset: 0x00002292
		public MultiValuedProperty<string> ComponentStates
		{
			get
			{
				return this.componentStates;
			}
			private set
			{
				this.componentStates = value;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000409B File Offset: 0x0000229B
		public override void Minimize()
		{
			base.Minimize();
		}

		// Token: 0x0400005A RID: 90
		public static readonly ADPropertyDefinition[] PropertiesNeededForServer = new ADPropertyDefinition[]
		{
			ServerSchema.Fqdn,
			ServerSchema.Edition,
			ServerSchema.VersionNumber,
			ServerSchema.MajorVersion,
			ServerSchema.AdminDisplayVersion,
			ServerSchema.IsExchange2007OrLater,
			ServerSchema.IsE14OrLater,
			ActiveDirectoryServerSchema.MailboxRelease,
			ServerSchema.CurrentServerRole,
			ServerSchema.IsMailboxServer,
			ServerSchema.ServerSite,
			ServerSchema.ExchangeLegacyDN,
			ServerSchema.DatabaseAvailabilityGroup,
			ActiveDirectoryServerSchema.AutoDatabaseMountDialType,
			ActiveDirectoryServerSchema.DatabaseCopyAutoActivationPolicy,
			ActiveDirectoryServerSchema.DatabaseCopyActivationDisabledAndMoveNow,
			ActiveDirectoryServerSchema.AutoDagServerConfigured,
			ServerSchema.MaxActiveMailboxDatabases,
			ServerSchema.MaxPreferredActiveDatabases,
			ActiveDirectoryServerSchema.ContinuousReplicationMaxMemoryPerDatabase,
			ServerSchema.ComponentStates
		};

		// Token: 0x0400005B RID: 91
		private string fqdn;

		// Token: 0x0400005C RID: 92
		private MultiValuedProperty<string> componentStates;
	}
}
