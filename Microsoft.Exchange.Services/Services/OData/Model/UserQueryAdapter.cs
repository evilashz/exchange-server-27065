using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EB6 RID: 3766
	internal class UserQueryAdapter : QueryAdapter
	{
		// Token: 0x06006206 RID: 25094 RVA: 0x001333FC File Offset: 0x001315FC
		public UserQueryAdapter(UserSchema entitySchema, ODataQueryOptions odataQueryOptions) : base(entitySchema, odataQueryOptions)
		{
			this.filterConverter = new ADDriverFilterConverter(base.EntitySchema);
		}

		// Token: 0x06006207 RID: 25095 RVA: 0x00133417 File Offset: 0x00131617
		public QueryFilter GetQueryFilter()
		{
			if (base.ODataQueryOptions.Filter != null)
			{
				return this.filterConverter.ConvertFilterClause(base.ODataQueryOptions.Filter);
			}
			return null;
		}

		// Token: 0x06006208 RID: 25096 RVA: 0x0013343E File Offset: 0x0013163E
		public SortBy GetSortBy()
		{
			if (base.ODataQueryOptions.OrderBy != null)
			{
				return this.filterConverter.ConvertOrderByClause(base.ODataQueryOptions.OrderBy);
			}
			return new SortBy(ADObjectSchema.Name, SortOrder.Ascending);
		}

		// Token: 0x06006209 RID: 25097 RVA: 0x00133470 File Offset: 0x00131670
		public List<ADPropertyDefinition> GetRequestedADProperties()
		{
			List<ADPropertyDefinition> list = new List<ADPropertyDefinition>(base.RequestedProperties.Count);
			foreach (PropertyDefinition propertyDefinition in base.RequestedProperties)
			{
				DirectoryPropertyProvider directoryPropertyProvider = propertyDefinition.ADDriverPropertyProvider as DirectoryPropertyProvider;
				list.Add(directoryPropertyProvider.ADPropertyDefinition);
			}
			return list;
		}

		// Token: 0x0600620A RID: 25098 RVA: 0x001334E0 File Offset: 0x001316E0
		public int GetSkipCount()
		{
			int result = 0;
			if (base.ODataQueryOptions.Skip != null)
			{
				result = base.ODataQueryOptions.Skip.Value;
			}
			return result;
		}

		// Token: 0x040034EB RID: 13547
		private ADDriverFilterConverter filterConverter;
	}
}
