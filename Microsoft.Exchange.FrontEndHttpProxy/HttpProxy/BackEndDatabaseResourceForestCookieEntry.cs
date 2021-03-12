using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200003C RID: 60
	internal class BackEndDatabaseResourceForestCookieEntry : BackEndDatabaseCookieEntry
	{
		// Token: 0x060001CD RID: 461 RVA: 0x0000A468 File Offset: 0x00008668
		public BackEndDatabaseResourceForestCookieEntry(Guid database, string domainName, string resourceForest) : this(database, domainName, resourceForest, ExDateTime.UtcNow + BackEndCookieEntryBase.LongLivedBackEndServerCookieLifeTime)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A482 File Offset: 0x00008682
		public BackEndDatabaseResourceForestCookieEntry(Guid database, string domainName, string resourceForest, ExDateTime expiryTime) : base(database, domainName, expiryTime)
		{
			this.ResourceForest = resourceForest;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000A495 File Offset: 0x00008695
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000A49D File Offset: 0x0000869D
		public string ResourceForest { get; private set; }

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A4A6 File Offset: 0x000086A6
		public override string ToString()
		{
			return base.ToString() + '~' + this.ResourceForest;
		}
	}
}
