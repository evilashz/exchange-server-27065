using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000007 RID: 7
	internal abstract class DatabaseBasedAnchorMailbox : AnchorMailbox
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003150 File Offset: 0x00001350
		public DatabaseBasedAnchorMailbox(AnchorSource anchorSource, object sourceObject, IRequestContext requestContext) : base(anchorSource, sourceObject, requestContext)
		{
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000315B File Offset: 0x0000135B
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00003163 File Offset: 0x00001363
		public bool UseServerCookie { get; set; }

		// Token: 0x06000039 RID: 57 RVA: 0x0000316C File Offset: 0x0000136C
		public virtual ADObjectId GetDatabase()
		{
			return base.GetCacheEntry().Database;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000317C File Offset: 0x0000137C
		public override BackEndCookieEntryBase BuildCookieEntryForTarget(BackEndServer routingTarget, bool proxyToDownLevel, bool useResourceForest)
		{
			if (routingTarget == null)
			{
				throw new ArgumentNullException("routingTarget");
			}
			if (!proxyToDownLevel && !this.UseServerCookie)
			{
				ADObjectId database = this.GetDatabase();
				if (database != null)
				{
					if (useResourceForest)
					{
						return new BackEndDatabaseResourceForestCookieEntry(database.ObjectGuid, string.Empty, database.PartitionFQDN);
					}
					return new BackEndDatabaseCookieEntry(database.ObjectGuid, string.Empty);
				}
			}
			return base.BuildCookieEntryForTarget(routingTarget, proxyToDownLevel, useResourceForest);
		}
	}
}
