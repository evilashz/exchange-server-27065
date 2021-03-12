using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000A RID: 10
	internal class DatabaseGuidAnchorMailbox : DatabaseBasedAnchorMailbox
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00003820 File Offset: 0x00001A20
		public DatabaseGuidAnchorMailbox(Guid databaseGuid, IRequestContext requestContext) : base(AnchorSource.DatabaseGuid, databaseGuid, requestContext)
		{
			base.NotFoundExceptionCreator = (() => new DatabaseNotFoundException(this.DatabaseGuid.ToString()));
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003854 File Offset: 0x00001A54
		public Guid DatabaseGuid
		{
			get
			{
				return (Guid)base.SourceObject;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003864 File Offset: 0x00001A64
		protected override AnchorMailboxCacheEntry RefreshCacheEntry()
		{
			return new AnchorMailboxCacheEntry
			{
				Database = new ADObjectId(Guid.Empty, (Guid)base.SourceObject)
			};
		}
	}
}
