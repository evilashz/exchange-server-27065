using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EA6 RID: 3750
	internal abstract class EwsQueryAdapter : QueryAdapter
	{
		// Token: 0x060061AB RID: 25003 RVA: 0x00130D88 File Offset: 0x0012EF88
		public EwsQueryAdapter(EntitySchema entitySchema, ODataQueryOptions odataQueryOptions) : base(entitySchema, odataQueryOptions)
		{
			this.filterConverter = new EwsFilterConverter(base.EntitySchema);
		}

		// Token: 0x060061AC RID: 25004 RVA: 0x00130DA4 File Offset: 0x0012EFA4
		public BasePagingType GetPaging()
		{
			IndexedPageView indexedPageView = new IndexedPageView
			{
				MaxRows = base.GetPageSize(),
				Offset = 0,
				Origin = BasePagingType.PagingOrigin.Beginning
			};
			if (base.ODataQueryOptions.Skip != null)
			{
				indexedPageView.Offset = base.ODataQueryOptions.Skip.Value;
			}
			return indexedPageView;
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x00130E04 File Offset: 0x0012F004
		public RestrictionType GetRestriction()
		{
			if (base.ODataQueryOptions.Filter != null)
			{
				SearchExpressionType item = this.filterConverter.ConvertFilterClause(base.ODataQueryOptions.Filter);
				return new RestrictionType
				{
					Item = item
				};
			}
			return null;
		}

		// Token: 0x060061AE RID: 25006 RVA: 0x00130E48 File Offset: 0x0012F048
		public SortResults[] GetSortOrder()
		{
			if (base.ODataQueryOptions.OrderBy != null)
			{
				return this.filterConverter.ConvertOrderByClause(base.ODataQueryOptions.OrderBy);
			}
			return null;
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x00130E84 File Offset: 0x0012F084
		protected PropertyPath[] GetRequestedPropertyPaths()
		{
			List<PropertyInformation> list = new List<PropertyInformation>();
			foreach (PropertyDefinition propertyDefinition in base.RequestedProperties)
			{
				EwsPropertyProvider ewsPropertyProvider = propertyDefinition.EwsPropertyProvider.GetEwsPropertyProvider(base.EntitySchema);
				if (ewsPropertyProvider.IsMultiValueProperty)
				{
					list.AddRange(ewsPropertyProvider.PropertyInformationList);
				}
				else
				{
					list.Add(ewsPropertyProvider.PropertyInformation);
				}
			}
			IEnumerable<PropertyPath> source = from x in list
			select x.PropertyPath;
			return source.ToArray<PropertyPath>();
		}

		// Token: 0x040034D2 RID: 13522
		protected EwsFilterConverter filterConverter;
	}
}
