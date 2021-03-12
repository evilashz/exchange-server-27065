using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004DF RID: 1247
	[Serializable]
	public class MiniClientAccessServerOrArray : MiniObject
	{
		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x060037DF RID: 14303 RVA: 0x000D94D1 File Offset: 0x000D76D1
		internal override ADObjectSchema Schema
		{
			get
			{
				return MiniClientAccessServerOrArray.schema;
			}
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x060037E0 RID: 14304 RVA: 0x000D94D8 File Offset: 0x000D76D8
		internal override string MostDerivedObjectClass
		{
			get
			{
				throw new InvalidADObjectOperationException(DirectoryStrings.ExceptionMostDerivedOnBase("MiniClientAccessServerOrArray"));
			}
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x060037E1 RID: 14305 RVA: 0x000D94E9 File Offset: 0x000D76E9
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return MiniClientAccessServerOrArray.implicitFilter;
			}
		}

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x060037E2 RID: 14306 RVA: 0x000D94F0 File Offset: 0x000D76F0
		public string Fqdn
		{
			get
			{
				return (string)this[MiniClientAccessServerOrArraySchema.Fqdn];
			}
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x060037E3 RID: 14307 RVA: 0x000D9502 File Offset: 0x000D7702
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[MiniClientAccessServerOrArraySchema.ExchangeLegacyDN];
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x060037E4 RID: 14308 RVA: 0x000D9514 File Offset: 0x000D7714
		public string Site
		{
			get
			{
				return (string)this[MiniClientAccessServerOrArraySchema.Site];
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x060037E5 RID: 14309 RVA: 0x000D9526 File Offset: 0x000D7726
		// (set) Token: 0x060037E6 RID: 14310 RVA: 0x000D9538 File Offset: 0x000D7738
		public ADObjectId ServerSite
		{
			get
			{
				return (ADObjectId)this[MiniClientAccessServerOrArraySchema.Site];
			}
			internal set
			{
				this[MiniClientAccessServerOrArraySchema.Site] = value;
			}
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x060037E7 RID: 14311 RVA: 0x000D9546 File Offset: 0x000D7746
		public bool IsClientAccessArray
		{
			get
			{
				return (bool)this[MiniClientAccessServerOrArraySchema.IsClientAccessArray];
			}
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x060037E8 RID: 14312 RVA: 0x000D9558 File Offset: 0x000D7758
		public bool IsClientAccessServer
		{
			get
			{
				return (bool)this[ServerSchema.IsClientAccessServer];
			}
		}

		// Token: 0x040025C1 RID: 9665
		private static readonly QueryFilter implicitFilter = QueryFilter.OrTogether(new QueryFilter[]
		{
			QueryFilter.AndTogether(new QueryFilter[]
			{
				new ClientAccessArray().ImplicitFilter,
				ClientAccessArray.PriorTo15ExchangeObjectVersionFilter
			}),
			QueryFilter.AndTogether(new QueryFilter[]
			{
				new Server().ImplicitFilter,
				new BitMaskOrFilter(ServerSchema.CurrentServerRole, 6UL)
			})
		});

		// Token: 0x040025C2 RID: 9666
		private static MiniClientAccessServerOrArraySchema schema = ObjectSchema.GetInstance<MiniClientAccessServerOrArraySchema>();
	}
}
