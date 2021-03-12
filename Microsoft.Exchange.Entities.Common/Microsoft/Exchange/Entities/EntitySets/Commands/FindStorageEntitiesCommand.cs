using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x02000035 RID: 53
	internal abstract class FindStorageEntitiesCommand<TContext, TEntity> : FindEntitiesCommand<TContext, TEntity>, IPropertyDefinitionMap where TEntity : IEntity
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00004617 File Offset: 0x00002817
		protected FindStorageEntitiesCommand()
		{
			base.RegisterOnBeforeExecute(new Action(this.AddRequestedPropertyDependencies));
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004631 File Offset: 0x00002831
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00004639 File Offset: 0x00002839
		public QueryFilter QueryFilter { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004642 File Offset: 0x00002842
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000464A File Offset: 0x0000284A
		public SortBy[] SortColumns { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004653 File Offset: 0x00002853
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000465B File Offset: 0x0000285B
		public HashSet<PropertyDefinition> Properties { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000112 RID: 274
		public abstract IDictionary<string, PropertyDefinition> PropertyMap { get; }

		// Token: 0x06000113 RID: 275 RVA: 0x00004664 File Offset: 0x00002864
		bool IPropertyDefinitionMap.TryGetPropertyDefinition(PropertyInfo propertyInfo, out PropertyDefinition propertyDefinition)
		{
			return this.PropertyMap.TryGetValue(propertyInfo.Name, out propertyDefinition);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004678 File Offset: 0x00002878
		protected override void OnQueryOptionsChanged()
		{
			QueryFilter queryFilter = null;
			SortBy[] sortColumns = null;
			if (base.QueryOptions != null)
			{
				if (base.QueryOptions.Filter != null)
				{
					queryFilter = base.QueryOptions.Filter.ToQueryFilter(this);
				}
				if (base.QueryOptions.OrderBy != null)
				{
					sortColumns = base.QueryOptions.OrderBy.ToSortBy(this).ToArray<SortBy>();
				}
			}
			this.QueryFilter = queryFilter;
			this.SortColumns = sortColumns;
			this.Properties = new HashSet<PropertyDefinition>();
			if (queryFilter != null)
			{
				this.Properties.AddRange(queryFilter.FilterProperties());
			}
			base.OnQueryOptionsChanged();
		}

		// Token: 0x06000115 RID: 277
		protected abstract IEnumerable<PropertyDefinition> GetRequestedPropertyDependencies();

		// Token: 0x06000116 RID: 278 RVA: 0x00004708 File Offset: 0x00002908
		private void AddRequestedPropertyDependencies()
		{
			IEnumerable<PropertyDefinition> requestedPropertyDependencies = this.GetRequestedPropertyDependencies();
			this.Properties.AddRange(requestedPropertyDependencies);
		}
	}
}
