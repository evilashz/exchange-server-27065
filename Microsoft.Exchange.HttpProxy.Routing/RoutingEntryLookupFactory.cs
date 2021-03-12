using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.HttpProxy.Routing.RoutingLookups;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000026 RID: 38
	internal class RoutingEntryLookupFactory : IRoutingLookupFactory
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000033E7 File Offset: 0x000015E7
		public RoutingEntryLookupFactory(IDatabaseLocationProvider databaseLocationProvider, IUserProvider userProvider)
		{
			if (databaseLocationProvider == null)
			{
				throw new ArgumentNullException("databaseLocationProvider");
			}
			if (userProvider == null)
			{
				throw new ArgumentNullException("userProvider");
			}
			this.databaseLocationProvider = databaseLocationProvider;
			this.userProvider = userProvider;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000341C File Offset: 0x0000161C
		public IRoutingLookup GetLookupForType(RoutingItemType routingEntryType)
		{
			switch (routingEntryType)
			{
			case RoutingItemType.ArchiveSmtp:
				return new ArchiveSmtpRoutingLookup(this.userProvider);
			case RoutingItemType.DatabaseGuid:
				return new DatabaseGuidRoutingLookup(this.databaseLocationProvider);
			case RoutingItemType.MailboxGuid:
				return new MailboxGuidRoutingLookup(this.userProvider);
			case RoutingItemType.Server:
				return new ServerRoutingLookup();
			case RoutingItemType.Smtp:
				return new SmtpRoutingLookup(this.userProvider);
			case RoutingItemType.ExternalDirectoryObjectId:
				return new ExternalDirectoryObjectIdRoutingLookup(this.userProvider);
			case RoutingItemType.LiveIdMemberName:
				return new LiveIdMemberNameRoutingLookup(this.userProvider);
			}
			return null;
		}

		// Token: 0x0400003C RID: 60
		private readonly IUserProvider userProvider;

		// Token: 0x0400003D RID: 61
		private readonly IDatabaseLocationProvider databaseLocationProvider;
	}
}
