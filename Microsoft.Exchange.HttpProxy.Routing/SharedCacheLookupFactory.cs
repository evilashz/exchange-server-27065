using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.HttpProxy.Routing.RoutingLookups;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000046 RID: 70
	internal class SharedCacheLookupFactory : IRoutingLookupFactory
	{
		// Token: 0x06000129 RID: 297 RVA: 0x0000510F File Offset: 0x0000330F
		public SharedCacheLookupFactory(ISharedCache mailboxCache, ISharedCache databaseCache)
		{
			this.databaseCache = databaseCache;
			this.mailboxCache = mailboxCache;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005128 File Offset: 0x00003328
		public IRoutingLookup GetLookupForType(RoutingItemType routingEntryType)
		{
			switch (routingEntryType)
			{
			case RoutingItemType.ArchiveSmtp:
				return new ArchiveSmtpSharedCacheLookup(this.mailboxCache);
			case RoutingItemType.DatabaseGuid:
				return new DatabaseGuidSharedCacheLookup(this.databaseCache);
			case RoutingItemType.MailboxGuid:
				return new MailboxGuidSharedCacheLookup(this.mailboxCache);
			case RoutingItemType.Server:
				return new ServerRoutingLookup();
			case RoutingItemType.Smtp:
				return new SmtpSharedCacheLookup(this.mailboxCache);
			case RoutingItemType.ExternalDirectoryObjectId:
				return new ExternalDirectoryObjectIdSharedCacheLookup(this.mailboxCache);
			case RoutingItemType.LiveIdMemberName:
				return new LiveIdMemberNameSharedCacheLookup(this.mailboxCache);
			}
			return null;
		}

		// Token: 0x04000083 RID: 131
		private readonly ISharedCache mailboxCache;

		// Token: 0x04000084 RID: 132
		private readonly ISharedCache databaseCache;
	}
}
