using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADDatabaseWrapper : ADObjectWrapperBase, IADDatabase, IADObjectCommon
	{
		// Token: 0x060000AE RID: 174 RVA: 0x000031F8 File Offset: 0x000013F8
		private void FinishConstructionCommon(ADObject sourceObj)
		{
			this.MasterServerOrAvailabilityGroup = (ADObjectId)sourceObj[DatabaseSchema.MasterServerOrAvailabilityGroup];
			this.EdbFilePath = (EdbFilePath)sourceObj[DatabaseSchema.EdbFilePath];
			this.LogFolderPath = (NonRootLocalLongFullPath)sourceObj[DatabaseSchema.LogFolderPath];
			this.SystemFolderPath = (NonRootLocalLongFullPath)sourceObj[DatabaseSchema.SystemFolderPath];
			this.LogFilePrefix = (string)sourceObj[DatabaseSchema.LogFilePrefix];
			this.Recovery = (bool)sourceObj[DatabaseSchema.Recovery];
			this.AutoDagExcludeFromMonitoring = (bool)sourceObj[DatabaseSchema.AutoDagExcludeFromMonitoring];
			this.Server = (ADObjectId)sourceObj[DatabaseSchema.Server];
			this.MountAtStartup = (bool)sourceObj[DatabaseSchema.MountAtStartup];
			this.DatabaseCreated = (bool)sourceObj[DatabaseSchema.DatabaseCreated];
			this.AllowFileRestore = (bool)sourceObj[DatabaseSchema.AllowFileRestore];
			this.CircularLoggingEnabled = (bool)sourceObj[DatabaseSchema.CircularLoggingEnabled];
			this.ExchangeLegacyDN = (string)sourceObj[DatabaseSchema.ExchangeLegacyDN];
			this.RpcClientAccessServerLegacyDN = (string)sourceObj[DatabaseSchema.RpcClientAccessServerExchangeLegacyDN];
			this.DistinguishedName = sourceObj.DistinguishedName;
			this.MailboxPublicFolderDatabase = (ADObjectId)sourceObj[DatabaseSchema.MailboxPublicFolderDatabase];
			this.IsExchange2009OrLater = (bool)sourceObj[DatabaseSchema.IsExchange2009OrLater];
			this.DatabaseGroup = (string)sourceObj[DatabaseSchema.DatabaseGroup];
			this.IsPublicFolderDatabase = sourceObj.ObjectClass.Contains(PublicFolderDatabase.MostDerivedClass);
			this.IsMailboxDatabase = sourceObj.ObjectClass.Contains(MailboxDatabase.MostDerivedClass);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000033B4 File Offset: 0x000015B4
		internal ADDatabaseWrapper(IADDatabase database) : base(database)
		{
			this.MasterServerOrAvailabilityGroup = database.MasterServerOrAvailabilityGroup;
			this.EdbFilePath = database.EdbFilePath;
			this.LogFolderPath = database.LogFolderPath;
			this.SystemFolderPath = database.SystemFolderPath;
			this.LogFilePrefix = database.LogFilePrefix;
			this.Recovery = database.Recovery;
			this.AutoDagExcludeFromMonitoring = database.AutoDagExcludeFromMonitoring;
			this.Server = database.Server;
			this.MountAtStartup = database.MountAtStartup;
			this.DatabaseCreated = database.DatabaseCreated;
			this.AllowFileRestore = database.AllowFileRestore;
			this.CircularLoggingEnabled = database.CircularLoggingEnabled;
			this.ExchangeLegacyDN = database.ExchangeLegacyDN;
			this.RpcClientAccessServerLegacyDN = database.RpcClientAccessServerLegacyDN;
			this.DistinguishedName = database.DistinguishedName;
			this.MailboxPublicFolderDatabase = database.MailboxPublicFolderDatabase;
			this.IsExchange2009OrLater = database.IsExchange2009OrLater;
			this.DatabaseGroup = database.DatabaseGroup;
			this.IsPublicFolderDatabase = database.IsPublicFolderDatabase;
			this.IsMailboxDatabase = database.IsMailboxDatabase;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000034B8 File Offset: 0x000016B8
		private ADDatabaseWrapper(Database database) : base(database)
		{
			this.FinishConstructionCommon(database);
			this.CompletePropertiesFromDbCopies(database.AllDatabaseCopies);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000034D4 File Offset: 0x000016D4
		private ADDatabaseWrapper(MiniDatabase database) : base(database)
		{
			this.FinishConstructionCommon(database);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000034E4 File Offset: 0x000016E4
		private IConfigurationSession CreateCustomConfigSessionIfNecessary(IConfigurationSession existingSession)
		{
			IConfigurationSession configurationSession = existingSession;
			if (configurationSession != null && configurationSession.ConsistencyMode != ConsistencyMode.PartiallyConsistent)
			{
				configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(configurationSession.DomainController, configurationSession.ReadOnly, ConsistencyMode.PartiallyConsistent, configurationSession.NetworkCredential, configurationSession.SessionSettings, 567, "CreateCustomConfigSessionIfNecessary", "f:\\15.00.1497\\sources\\dev\\data\\src\\HA\\DirectoryServices\\ADObjectWrappers.cs");
			}
			return configurationSession;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003534 File Offset: 0x00001734
		public void FinishConstructionFromMiniDatabase(IConfigurationSession session)
		{
			session = this.CreateCustomConfigSessionIfNecessary(session);
			DatabaseCopy[] allCopies = session.Find<DatabaseCopy>((ADObjectId)base.Identity, QueryScope.SubTree, null, null, 0);
			this.CompletePropertiesFromDbCopies(allCopies);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003568 File Offset: 0x00001768
		private void CompletePropertiesFromDbCopies(DatabaseCopy[] allCopies)
		{
			if (allCopies == null || allCopies.Length == 0)
			{
				this.AssignCopies(null);
				return;
			}
			ADDatabaseCopyWrapper[] array = new ADDatabaseCopyWrapper[allCopies.Length];
			for (int i = 0; i < allCopies.Length; i++)
			{
				array[i] = ADObjectWrapperFactory.CreateWrapper(allCopies[i]);
			}
			this.AssignCopies(array);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000035B0 File Offset: 0x000017B0
		private void AssignCopies(ADDatabaseCopyWrapper[] knownCopies)
		{
			if (knownCopies == null || knownCopies.Length == 0)
			{
				this.validDbCopies = new ADDatabaseCopyWrapper[0];
				this.allDbCopies = new ADDatabaseCopyWrapper[0];
				this.servers = new ADObjectId[0];
				this.ReplicationType = ReplicationType.None;
				return;
			}
			Array.Sort<ADDatabaseCopyWrapper>(knownCopies);
			int num = 1;
			List<ADDatabaseCopyWrapper> list = new List<ADDatabaseCopyWrapper>(knownCopies.Length);
			List<ADObjectId> list2 = new List<ADObjectId>(knownCopies.Length);
			foreach (ADDatabaseCopyWrapper addatabaseCopyWrapper in knownCopies)
			{
				addatabaseCopyWrapper.ActivationPreference = num++;
				if (addatabaseCopyWrapper.IsValidForRead && addatabaseCopyWrapper.IsHostServerPresent)
				{
					list.Add(addatabaseCopyWrapper);
					list2.Add(addatabaseCopyWrapper.HostServer);
				}
			}
			this.allDbCopies = knownCopies;
			this.validDbCopies = list.ToArray();
			this.servers = list2.ToArray();
			this.HostServerForPreference1 = knownCopies[0].HostServer;
			if (this.allDbCopies.Length > 1)
			{
				this.replicationType = new ReplicationType?(ReplicationType.Remote);
				return;
			}
			this.replicationType = new ReplicationType?(ReplicationType.None);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000036C8 File Offset: 0x000018C8
		public void ExcludeDatabaseCopyFromProperties(string hostServerToExclude)
		{
			if (this.allDbCopies == null || this.allDbCopies.Length <= 1)
			{
				return;
			}
			ADDatabaseCopyWrapper[] knownCopies = (from dbCopy in this.allDbCopies
			where !string.Equals(dbCopy.Name, hostServerToExclude, StringComparison.OrdinalIgnoreCase)
			select dbCopy).ToArray<ADDatabaseCopyWrapper>();
			this.AssignCopies(knownCopies);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000373C File Offset: 0x0000193C
		public static ADDatabaseWrapper CreateWrapper(Database database)
		{
			if (database == null)
			{
				return null;
			}
			return new ADDatabaseWrapper(database);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003749 File Offset: 0x00001949
		public static ADDatabaseWrapper CreateWrapper(MiniDatabase database)
		{
			if (database == null)
			{
				return null;
			}
			return new ADDatabaseWrapper(database);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003756 File Offset: 0x00001956
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003763 File Offset: 0x00001963
		public ReplicationType ReplicationType
		{
			get
			{
				return this.replicationType.Value;
			}
			private set
			{
				this.replicationType = new ReplicationType?(value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003771 File Offset: 0x00001971
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003784 File Offset: 0x00001984
		public EdbFilePath EdbFilePath
		{
			get
			{
				base.CheckMinimizedProperty("EdbFilePath");
				return this._edbFilePath;
			}
			private set
			{
				this._edbFilePath = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000378D File Offset: 0x0000198D
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000037A0 File Offset: 0x000019A0
		public NonRootLocalLongFullPath LogFolderPath
		{
			get
			{
				base.CheckMinimizedProperty("LogFolderPath");
				return this._logFolderPath;
			}
			private set
			{
				this._logFolderPath = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000037A9 File Offset: 0x000019A9
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000037BC File Offset: 0x000019BC
		public NonRootLocalLongFullPath SystemFolderPath
		{
			get
			{
				base.CheckMinimizedProperty("SystemFolderPath");
				return this._systemFolderPath;
			}
			private set
			{
				this._systemFolderPath = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000037C5 File Offset: 0x000019C5
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000037CD File Offset: 0x000019CD
		public ADObjectId HostServerForPreference1 { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000037D6 File Offset: 0x000019D6
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000037DE File Offset: 0x000019DE
		public bool Recovery { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000037E7 File Offset: 0x000019E7
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000037EF File Offset: 0x000019EF
		public bool AutoDagExcludeFromMonitoring { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000037F8 File Offset: 0x000019F8
		public IADDatabaseCopy[] DatabaseCopies
		{
			get
			{
				base.CheckMinimizedProperty("DatabaseCopies");
				return this.validDbCopies;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000380B File Offset: 0x00001A0B
		public IADDatabaseCopy[] AllDatabaseCopies
		{
			get
			{
				base.CheckMinimizedProperty("AllDatabaseCopies");
				return this.allDbCopies;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003820 File Offset: 0x00001A20
		private IADDatabaseCopy FindCopy(string serverShortName, IADDatabaseCopy[] copies)
		{
			if (copies != null)
			{
				foreach (IADDatabaseCopy iaddatabaseCopy in copies)
				{
					if (MachineName.Comparer.Equals(iaddatabaseCopy.HostServerName, serverShortName))
					{
						return iaddatabaseCopy;
					}
				}
			}
			return null;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000385E File Offset: 0x00001A5E
		public IADDatabaseCopy GetDatabaseCopy(string serverShortName)
		{
			return this.FindCopy(serverShortName, this.AllDatabaseCopies);
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000386D File Offset: 0x00001A6D
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00003875 File Offset: 0x00001A75
		public ADObjectId Server
		{
			get
			{
				return this._owningServer;
			}
			private set
			{
				this._owningServer = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000387E File Offset: 0x00001A7E
		public ADObjectId[] Servers
		{
			get
			{
				base.CheckMinimizedProperty("ADDatabaseWrapper.Servers");
				return this.servers;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003891 File Offset: 0x00001A91
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003899 File Offset: 0x00001A99
		public bool MountAtStartup { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000038A2 File Offset: 0x00001AA2
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000038AA File Offset: 0x00001AAA
		public bool DatabaseCreated { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000038B3 File Offset: 0x00001AB3
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000038BB File Offset: 0x00001ABB
		public bool AllowFileRestore { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000038C4 File Offset: 0x00001AC4
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000038D7 File Offset: 0x00001AD7
		public string DistinguishedName
		{
			get
			{
				base.CheckMinimizedProperty("ADDatabaseWrapper.DistinguishedName");
				return this._distinguishedName;
			}
			private set
			{
				this._distinguishedName = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000038E0 File Offset: 0x00001AE0
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000038E8 File Offset: 0x00001AE8
		public string LogFilePrefix { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000038F1 File Offset: 0x00001AF1
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000038F9 File Offset: 0x00001AF9
		public bool IsPublicFolderDatabase { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003902 File Offset: 0x00001B02
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000390A File Offset: 0x00001B0A
		public bool IsMailboxDatabase { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003913 File Offset: 0x00001B13
		// (set) Token: 0x060000DD RID: 221 RVA: 0x0000391B File Offset: 0x00001B1B
		public bool CircularLoggingEnabled { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003924 File Offset: 0x00001B24
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000392C File Offset: 0x00001B2C
		public string ExchangeLegacyDN { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003935 File Offset: 0x00001B35
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x0000393D File Offset: 0x00001B3D
		public string RpcClientAccessServerLegacyDN { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003946 File Offset: 0x00001B46
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000394E File Offset: 0x00001B4E
		public ADObjectId MailboxPublicFolderDatabase { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003957 File Offset: 0x00001B57
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000395F File Offset: 0x00001B5F
		public bool IsExchange2009OrLater { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003968 File Offset: 0x00001B68
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003970 File Offset: 0x00001B70
		public ADObjectId MasterServerOrAvailabilityGroup { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003979 File Offset: 0x00001B79
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00003981 File Offset: 0x00001B81
		public string DatabaseGroup { get; private set; }

		// Token: 0x060000EA RID: 234 RVA: 0x0000398A File Offset: 0x00001B8A
		public override void Minimize()
		{
			this.allDbCopies = null;
			this.validDbCopies = null;
			this.EdbFilePath = null;
			this.LogFolderPath = null;
			this.SystemFolderPath = null;
			this.servers = null;
			this._distinguishedName = null;
			base.Minimize();
		}

		// Token: 0x04000035 RID: 53
		public static readonly ADPropertyDefinition[] PropertiesNeededForDatabase = new ADPropertyDefinition[]
		{
			DatabaseSchema.MasterServerOrAvailabilityGroup,
			DatabaseSchema.EdbFilePath,
			DatabaseSchema.LogFolderPath,
			DatabaseSchema.SystemFolderPath,
			DatabaseSchema.LogFilePrefix,
			DatabaseSchema.Recovery,
			DatabaseSchema.AutoDagExcludeFromMonitoring,
			DatabaseSchema.Server,
			DatabaseSchema.MountAtStartup,
			DatabaseSchema.DatabaseCreated,
			DatabaseSchema.AllowFileRestore,
			DatabaseSchema.CircularLoggingEnabled,
			DatabaseSchema.ExchangeLegacyDN,
			DatabaseSchema.RpcClientAccessServerExchangeLegacyDN,
			DatabaseSchema.MailboxPublicFolderDatabase,
			DatabaseSchema.IsExchange2009OrLater,
			DatabaseSchema.DatabaseGroup,
			ADObjectSchema.ObjectClass
		};

		// Token: 0x04000036 RID: 54
		private ADDatabaseCopyWrapper[] validDbCopies;

		// Token: 0x04000037 RID: 55
		private ADDatabaseCopyWrapper[] allDbCopies;

		// Token: 0x04000038 RID: 56
		private ADObjectId[] servers;

		// Token: 0x04000039 RID: 57
		private ReplicationType? replicationType;

		// Token: 0x0400003A RID: 58
		private EdbFilePath _edbFilePath;

		// Token: 0x0400003B RID: 59
		private NonRootLocalLongFullPath _logFolderPath;

		// Token: 0x0400003C RID: 60
		private NonRootLocalLongFullPath _systemFolderPath;

		// Token: 0x0400003D RID: 61
		private ADObjectId _owningServer;

		// Token: 0x0400003E RID: 62
		private string _distinguishedName;
	}
}
