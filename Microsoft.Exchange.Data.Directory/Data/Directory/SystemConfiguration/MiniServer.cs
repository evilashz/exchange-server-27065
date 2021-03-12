using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004E7 RID: 1255
	[Serializable]
	public class MiniServer : MiniObject
	{
		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x000D9946 File Offset: 0x000D7B46
		internal override ADObjectSchema Schema
		{
			get
			{
				return MiniServer.schema;
			}
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06003815 RID: 14357 RVA: 0x000D994D File Offset: 0x000D7B4D
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MiniServer.mostDerivedClass;
			}
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x000D9954 File Offset: 0x000D7B54
		public string Fqdn
		{
			get
			{
				return (string)this[MiniServerSchema.Fqdn];
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06003817 RID: 14359 RVA: 0x000D9966 File Offset: 0x000D7B66
		public int VersionNumber
		{
			get
			{
				return (int)this[MiniServerSchema.VersionNumber];
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x000D9978 File Offset: 0x000D7B78
		public int MajorVersion
		{
			get
			{
				return (int)this[MiniServerSchema.MajorVersion];
			}
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06003819 RID: 14361 RVA: 0x000D998A File Offset: 0x000D7B8A
		public ServerVersion AdminDisplayVersion
		{
			get
			{
				return (ServerVersion)this[MiniServerSchema.AdminDisplayVersion];
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x0600381A RID: 14362 RVA: 0x000D999C File Offset: 0x000D7B9C
		public bool IsE14OrLater
		{
			get
			{
				return (bool)this[MiniServerSchema.IsE14OrLater];
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x0600381B RID: 14363 RVA: 0x000D99AE File Offset: 0x000D7BAE
		public ADObjectId ServerSite
		{
			get
			{
				return (ADObjectId)this[MiniServerSchema.ServerSite];
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x0600381C RID: 14364 RVA: 0x000D99C0 File Offset: 0x000D7BC0
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[ServerSchema.ExchangeLegacyDN];
			}
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x000D99D2 File Offset: 0x000D7BD2
		public bool IsClientAccessServer
		{
			get
			{
				return (bool)this[ServerSchema.IsClientAccessServer];
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x0600381E RID: 14366 RVA: 0x000D99E4 File Offset: 0x000D7BE4
		public bool IsExchange2007OrLater
		{
			get
			{
				return (bool)this[ServerSchema.IsExchange2007OrLater];
			}
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x000D99F6 File Offset: 0x000D7BF6
		public bool IsMailboxServer
		{
			get
			{
				return (bool)this[ServerSchema.IsMailboxServer];
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06003820 RID: 14368 RVA: 0x000D9A08 File Offset: 0x000D7C08
		public ADObjectId DatabaseAvailabilityGroup
		{
			get
			{
				return (ADObjectId)this[ServerSchema.DatabaseAvailabilityGroup];
			}
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06003821 RID: 14369 RVA: 0x000D9A1A File Offset: 0x000D7C1A
		public DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
		{
			get
			{
				return (DatabaseCopyAutoActivationPolicyType)this[ActiveDirectoryServerSchema.DatabaseCopyAutoActivationPolicy];
			}
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06003822 RID: 14370 RVA: 0x000D9A2C File Offset: 0x000D7C2C
		public bool DatabaseCopyActivationDisabledAndMoveNow
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.DatabaseCopyActivationDisabledAndMoveNow];
			}
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06003823 RID: 14371 RVA: 0x000D9A3E File Offset: 0x000D7C3E
		public bool AutoDagServerConfigured
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.AutoDagServerConfigured];
			}
		}

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x06003824 RID: 14372 RVA: 0x000D9A50 File Offset: 0x000D7C50
		public MultiValuedProperty<string> ComponentStates
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.ComponentStates];
			}
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06003825 RID: 14373 RVA: 0x000D9A62 File Offset: 0x000D7C62
		public AutoDatabaseMountDial AutoDatabaseMountDial
		{
			get
			{
				return (AutoDatabaseMountDial)this[ActiveDirectoryServerSchema.AutoDatabaseMountDialType];
			}
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06003826 RID: 14374 RVA: 0x000D9A74 File Offset: 0x000D7C74
		public ServerRole CurrentServerRole
		{
			get
			{
				return (ServerRole)this[ServerSchema.CurrentServerRole];
			}
		}

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06003827 RID: 14375 RVA: 0x000D9A86 File Offset: 0x000D7C86
		public ServerEditionType Edition
		{
			get
			{
				return (ServerEditionType)this[ServerSchema.Edition];
			}
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06003828 RID: 14376 RVA: 0x000D9A98 File Offset: 0x000D7C98
		public long? ContinuousReplicationMaxMemoryPerDatabase
		{
			get
			{
				return (long?)this[ActiveDirectoryServerSchema.ContinuousReplicationMaxMemoryPerDatabase];
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06003829 RID: 14377 RVA: 0x000D9AAA File Offset: 0x000D7CAA
		public int? MaximumActiveDatabases
		{
			get
			{
				return (int?)this[ServerSchema.MaxActiveMailboxDatabases];
			}
		}

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x0600382A RID: 14378 RVA: 0x000D9ABC File Offset: 0x000D7CBC
		public int? MaximumPreferredActiveDatabases
		{
			get
			{
				return (int?)this[ServerSchema.MaxPreferredActiveDatabases];
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x0600382B RID: 14379 RVA: 0x000D9ACE File Offset: 0x000D7CCE
		internal ITopologyConfigurationSession Session
		{
			get
			{
				return (ITopologyConfigurationSession)this.m_Session;
			}
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000D9ADB File Offset: 0x000D7CDB
		internal Database[] GetDatabases()
		{
			return this.GetDatabases<Database>(false);
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000D9AE4 File Offset: 0x000D7CE4
		internal TDatabase[] GetDatabases<TDatabase>(bool allowInvalidCopies) where TDatabase : IConfigurable, new()
		{
			if (this.Session == null)
			{
				throw new InvalidOperationException("Server object does not have a session reference, so cannot get databases.");
			}
			List<TDatabase> list = new List<TDatabase>();
			if (this.IsE14OrLater)
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, base.Name);
				DatabaseCopy[] array = this.Session.Find<DatabaseCopy>(null, QueryScope.SubTree, filter, null, 0);
				foreach (DatabaseCopy databaseCopy in array)
				{
					if (databaseCopy.IsValidDatabaseCopy(allowInvalidCopies))
					{
						TDatabase database = databaseCopy.GetDatabase<TDatabase>();
						if (database != null)
						{
							list.Add(database);
						}
					}
				}
			}
			else
			{
				list.AddRange(this.Session.FindPaged<TDatabase>(null, base.Id, true, null, 0));
			}
			return list.ToArray();
		}

		// Token: 0x040025E0 RID: 9696
		private static MiniServerSchema schema = ObjectSchema.GetInstance<MiniServerSchema>();

		// Token: 0x040025E1 RID: 9697
		private static string mostDerivedClass = "msExchExchangeServer";
	}
}
