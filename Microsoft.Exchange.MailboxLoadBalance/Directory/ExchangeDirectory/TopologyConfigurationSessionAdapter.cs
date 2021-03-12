using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory.ExchangeDirectory
{
	// Token: 0x02000088 RID: 136
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class TopologyConfigurationSessionAdapter
	{
		// Token: 0x06000508 RID: 1288
		public abstract TResult[] Find<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults) where TResult : ADObject, new();

		// Token: 0x06000509 RID: 1289
		public abstract IEnumerable<T> FindAllPaged<T>() where T : ADConfigurationObject, new();

		// Token: 0x0600050A RID: 1290
		public abstract IEnumerable<TResult> FindPaged<TResult>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where TResult : ADObject, new();

		// Token: 0x0600050B RID: 1291
		public abstract Server FindServerByFqdn(Fqdn fqdn);

		// Token: 0x0600050C RID: 1292
		public abstract IEnumerable<MailboxDatabase> GetDatabasesOnServer(DirectoryIdentity serverIdentity);

		// Token: 0x0600050D RID: 1293
		public abstract T Read<T>(ADObjectId objectId) where T : ADConfigurationObject, new();

		// Token: 0x0600050E RID: 1294
		public abstract MiniServer ReadMiniServer(ADObjectId entryId, IEnumerable<PropertyDefinition> properties);

		// Token: 0x04000198 RID: 408
		public static readonly Hookable<TopologyConfigurationSessionAdapter> Instance = Hookable<TopologyConfigurationSessionAdapter>.Create(true, new TopologyConfigurationSessionAdapter.ADDriverTopologyConfigurationAdapter());

		// Token: 0x02000089 RID: 137
		private class ADDriverTopologyConfigurationAdapter : TopologyConfigurationSessionAdapter
		{
			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000CC1C File Offset: 0x0000AE1C
			private ITopologyConfigurationSession ConfigurationSession
			{
				get
				{
					return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 111, "ConfigurationSession", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\MailboxLoadBalance\\Directory\\ExchangeDirectory\\TopologyConfigurationSessionAdapter.cs");
				}
			}

			// Token: 0x06000512 RID: 1298 RVA: 0x0000CC3B File Offset: 0x0000AE3B
			public override TResult[] Find<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
			{
				return this.ConfigurationSession.Find<TResult>(null, scope, filter, sortBy, maxResults);
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x0000CC4F File Offset: 0x0000AE4F
			public override IEnumerable<T> FindAllPaged<T>()
			{
				return this.ConfigurationSession.FindAllPaged<T>();
			}

			// Token: 0x06000514 RID: 1300 RVA: 0x0000CC5C File Offset: 0x0000AE5C
			public override IEnumerable<TResult> FindPaged<TResult>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
			{
				return this.ConfigurationSession.FindPaged<TResult>(filter, rootId, deepSearch, sortBy, pageSize);
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x0000CC70 File Offset: 0x0000AE70
			public override Server FindServerByFqdn(Fqdn fqdn)
			{
				return this.ConfigurationSession.FindServerByFqdn(fqdn.ToString());
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x0000CF6C File Offset: 0x0000B16C
			public override IEnumerable<MailboxDatabase> GetDatabasesOnServer(DirectoryIdentity serverIdentity)
			{
				QueryFilter serverNameFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, serverIdentity.Name);
				IEnumerable<DatabaseCopy> copies = this.ConfigurationSession.Find<DatabaseCopy>(null, QueryScope.SubTree, serverNameFilter, null, 0);
				IEnumerable<DatabaseCopy> validCopies = from dbCopy in copies
				where dbCopy.IsValidDatabaseCopy(false)
				select dbCopy;
				IEnumerable<MailboxDatabase> databases = from dbCopy in validCopies
				select dbCopy.GetDatabase<MailboxDatabase>() into db
				where db != null
				select db;
				foreach (MailboxDatabase database in databases)
				{
					if (object.Equals((from ap in database.ActivationPreference
					orderby ap.Value
					select ap).FirstOrDefault<KeyValuePair<ADObjectId, int>>().Key.ObjectGuid, serverIdentity.Guid))
					{
						yield return database;
					}
				}
				yield break;
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x0000CF90 File Offset: 0x0000B190
			public override T Read<T>(ADObjectId objectId)
			{
				return this.ConfigurationSession.Read<T>(objectId);
			}

			// Token: 0x06000518 RID: 1304 RVA: 0x0000CF9E File Offset: 0x0000B19E
			public override MiniServer ReadMiniServer(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
			{
				return this.ConfigurationSession.ReadMiniServer(entryId, properties);
			}
		}
	}
}
