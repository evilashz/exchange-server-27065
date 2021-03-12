using System;
using System.Linq;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200000A RID: 10
	internal sealed class DatabaseInfoCache : LazyLookupTimeoutCache<Guid, DatabaseInfo>
	{
		// Token: 0x0600005D RID: 93 RVA: 0x0000386D File Offset: 0x00001A6D
		public DatabaseInfoCache(IConfigurationSession adConfigurationSession, TimeSpan cacheTimeout) : base(2, 1000, false, cacheTimeout)
		{
			this.adConfigurationSession = adConfigurationSession;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003900 File Offset: 0x00001B00
		protected override DatabaseInfo CreateOnCacheMiss(Guid key, ref bool shouldAdd)
		{
			ADRawEntry database = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				database = this.adConfigurationSession.Find(this.adConfigurationSession.ConfigurationNamingContext, QueryScope.SubTree, new AndFilter(new QueryFilter[]
				{
					new MailboxDatabase().ImplicitFilter,
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, new ADObjectId(key))
				}), null, 1, DatabaseInfoCache.RequiredProperties).FirstOrDefault<ADRawEntry>();
			});
			if (!adoperationResult.Succeeded || database == null)
			{
				throw new CallFailedException(string.Format("Failed to look up database information for Database Guid {0}", key), adoperationResult.Exception);
			}
			SecurityDescriptor securityDescriptor = (SecurityDescriptor)database[ADObjectSchema.NTSecurityDescriptor];
			if (securityDescriptor != null)
			{
				return new DatabaseInfo(securityDescriptor);
			}
			throw new CallFailedException(string.Format("Security descriptor not available for database {0}", key));
		}

		// Token: 0x0400003B RID: 59
		private static readonly PropertyDefinition[] RequiredProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Guid,
			MailboxDatabaseSchema.Name,
			ADObjectSchema.NTSecurityDescriptor
		};

		// Token: 0x0400003C RID: 60
		private readonly IConfigurationSession adConfigurationSession;
	}
}
