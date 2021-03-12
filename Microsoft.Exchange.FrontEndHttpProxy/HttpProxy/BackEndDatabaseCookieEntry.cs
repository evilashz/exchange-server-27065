using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200003B RID: 59
	internal class BackEndDatabaseCookieEntry : BackEndCookieEntryBase
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000A397 File Offset: 0x00008597
		public BackEndDatabaseCookieEntry(Guid database, string domain, ExDateTime expiryTime) : base(BackEndCookieEntryType.Database, expiryTime)
		{
			this.Database = database;
			this.Domain = domain;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000A3AF File Offset: 0x000085AF
		public BackEndDatabaseCookieEntry(Guid database, string domain) : this(database, domain, ExDateTime.UtcNow + BackEndCookieEntryBase.LongLivedBackEndServerCookieLifeTime)
		{
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000A3C8 File Offset: 0x000085C8
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000A3D0 File Offset: 0x000085D0
		public Guid Database { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000A3D9 File Offset: 0x000085D9
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000A3E1 File Offset: 0x000085E1
		public string Domain { get; private set; }

		// Token: 0x060001CC RID: 460 RVA: 0x0000A3EC File Offset: 0x000085EC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				BackEndCookieEntryBase.ConvertBackEndCookieEntryTypeToString(base.EntryType),
				'~',
				this.Database.ToString(),
				'~',
				this.Domain,
				'~',
				base.ExpiryTime.ToString("s")
			});
		}
	}
}
