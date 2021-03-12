using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000B RID: 11
	internal class DatabaseNameAnchorMailbox : DatabaseBasedAnchorMailbox
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000038A0 File Offset: 0x00001AA0
		public DatabaseNameAnchorMailbox(string databaseName, IRequestContext requestContext) : base(AnchorSource.DatabaseName, databaseName, requestContext)
		{
			base.NotFoundExceptionCreator = (() => new DatabaseNotFoundException(this.DatabaseName));
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000038CF File Offset: 0x00001ACF
		public string DatabaseName
		{
			get
			{
				return (string)base.SourceObject;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000391C File Offset: 0x00001B1C
		protected override AnchorMailboxCacheEntry RefreshCacheEntry()
		{
			IConfigurationSession session = DirectoryHelper.GetConfigurationSession();
			MailboxDatabase[] array = DirectoryHelper.InvokeResourceForest(base.RequestContext.LatencyTracker, () => session.Find<MailboxDatabase>(session.GetExchangeConfigurationContainer().Id, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, DatabaseSchema.Name, this.DatabaseName), null, 1));
			if (array.Length == 0)
			{
				base.CheckForNullAndThrowIfApplicable<ADObjectId>(null);
				return new AnchorMailboxCacheEntry();
			}
			return new AnchorMailboxCacheEntry
			{
				Database = array[0].Id
			};
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003988 File Offset: 0x00001B88
		protected override AnchorMailboxCacheEntry LoadCacheEntryFromIncomingCookie()
		{
			BackEndDatabaseCookieEntry backEndDatabaseCookieEntry = base.IncomingCookieEntry as BackEndDatabaseCookieEntry;
			if (backEndDatabaseCookieEntry != null)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<DatabaseNameAnchorMailbox, BackEndDatabaseCookieEntry>((long)this.GetHashCode(), "[DatabaseNameAnchorMailbox::LoadCacheEntryFromCookie]: Anchor mailbox {0} using cookie entry {1} as cache entry.", this, backEndDatabaseCookieEntry);
				BackEndDatabaseResourceForestCookieEntry backEndDatabaseResourceForestCookieEntry = base.IncomingCookieEntry as BackEndDatabaseResourceForestCookieEntry;
				return new AnchorMailboxCacheEntry
				{
					Database = new ADObjectId(backEndDatabaseCookieEntry.Database, (backEndDatabaseResourceForestCookieEntry == null) ? null : backEndDatabaseResourceForestCookieEntry.ResourceForest)
				};
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<DatabaseNameAnchorMailbox>((long)this.GetHashCode(), "[DatabaseNameAnchorMailbox::LoadCacheEntryFromCookie]: Anchor mailbox {0} had no BackEndDatabaseCookie.", this);
			return null;
		}
	}
}
