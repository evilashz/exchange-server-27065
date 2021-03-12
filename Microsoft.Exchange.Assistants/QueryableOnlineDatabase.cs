using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000B3 RID: 179
	internal class QueryableOnlineDatabase : QueryableObjectImplBase<QueryableOnlineDatabaseObjectSchema>
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0001B25A File Offset: 0x0001945A
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0001B26C File Offset: 0x0001946C
		public string DatabaseName
		{
			get
			{
				return (string)this[QueryableOnlineDatabaseObjectSchema.DatabaseName];
			}
			set
			{
				this[QueryableOnlineDatabaseObjectSchema.DatabaseName] = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0001B27A File Offset: 0x0001947A
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0001B28C File Offset: 0x0001948C
		public Guid DatabaseGuid
		{
			get
			{
				return (Guid)this[QueryableOnlineDatabaseObjectSchema.DatabaseGuid];
			}
			set
			{
				this[QueryableOnlineDatabaseObjectSchema.DatabaseGuid] = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001B29F File Offset: 0x0001949F
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x0001B2B1 File Offset: 0x000194B1
		public bool RestartRequired
		{
			get
			{
				return (bool)this[QueryableOnlineDatabaseObjectSchema.RestartRequired];
			}
			set
			{
				this[QueryableOnlineDatabaseObjectSchema.RestartRequired] = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001B2C4 File Offset: 0x000194C4
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x0001B2D6 File Offset: 0x000194D6
		public QueryableEventController EventController
		{
			get
			{
				return (QueryableEventController)this[QueryableOnlineDatabaseObjectSchema.EventController];
			}
			set
			{
				this[QueryableOnlineDatabaseObjectSchema.EventController] = value;
			}
		}
	}
}
