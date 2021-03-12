using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EA7 RID: 3751
	internal class DataEntityQueryAdpater : QueryAdapter
	{
		// Token: 0x060061B1 RID: 25009 RVA: 0x00130F34 File Offset: 0x0012F134
		public DataEntityQueryAdpater(EntitySchema entitySchema, ODataQueryOptions odataQueryOptions) : base(entitySchema, odataQueryOptions)
		{
			this.filterConverter = new DataEntityFilterConverter(base.EntitySchema);
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x00130F50 File Offset: 0x0012F150
		public IEntityQueryOptions GetEntityQueryOptions()
		{
			DataEntityQueryAdpater.EntityQueryOptions entityQueryOptions = new DataEntityQueryAdpater.EntityQueryOptions();
			entityQueryOptions.Skip = base.ODataQueryOptions.Skip;
			entityQueryOptions.Take = new int?(base.GetPageSize());
			if (base.ODataQueryOptions.Filter != null)
			{
				entityQueryOptions.Filter = this.filterConverter.ConvertFilterClause(base.ODataQueryOptions.Filter);
			}
			if (base.ODataQueryOptions.OrderBy != null)
			{
				entityQueryOptions.OrderBy = this.filterConverter.ConvertOrderByClause(base.ODataQueryOptions.OrderBy);
			}
			return entityQueryOptions;
		}

		// Token: 0x040034D4 RID: 13524
		protected DataEntityFilterConverter filterConverter;

		// Token: 0x02000EA8 RID: 3752
		private class EntityQueryOptions : IEntityQueryOptions
		{
			// Token: 0x1700166C RID: 5740
			// (get) Token: 0x060061B3 RID: 25011 RVA: 0x00130FD8 File Offset: 0x0012F1D8
			// (set) Token: 0x060061B4 RID: 25012 RVA: 0x00130FE0 File Offset: 0x0012F1E0
			public int? Skip { get; set; }

			// Token: 0x1700166D RID: 5741
			// (get) Token: 0x060061B5 RID: 25013 RVA: 0x00130FE9 File Offset: 0x0012F1E9
			// (set) Token: 0x060061B6 RID: 25014 RVA: 0x00130FF1 File Offset: 0x0012F1F1
			public IReadOnlyList<OrderByClause> OrderBy { get; set; }

			// Token: 0x1700166E RID: 5742
			// (get) Token: 0x060061B7 RID: 25015 RVA: 0x00130FFA File Offset: 0x0012F1FA
			// (set) Token: 0x060061B8 RID: 25016 RVA: 0x00131002 File Offset: 0x0012F202
			public int? Take { get; set; }

			// Token: 0x1700166F RID: 5743
			// (get) Token: 0x060061B9 RID: 25017 RVA: 0x0013100B File Offset: 0x0012F20B
			// (set) Token: 0x060061BA RID: 25018 RVA: 0x00131013 File Offset: 0x0012F213
			public Expression Filter { get; set; }
		}
	}
}
