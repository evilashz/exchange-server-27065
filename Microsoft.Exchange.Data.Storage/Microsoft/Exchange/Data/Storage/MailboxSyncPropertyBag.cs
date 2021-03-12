using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E5E RID: 3678
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxSyncPropertyBag : IReadOnlyPropertyBag
	{
		// Token: 0x06007F4F RID: 32591 RVA: 0x0022E1EC File Offset: 0x0022C3EC
		public MailboxSyncPropertyBag(ICollection<PropertyDefinition> initialColumns)
		{
			this.columnDictionary = new Dictionary<PropertyDefinition, int>(initialColumns.Count);
			foreach (PropertyDefinition property in initialColumns)
			{
				this.AddColumn(property);
			}
			this.columnArray = initialColumns;
		}

		// Token: 0x170021F4 RID: 8692
		// (get) Token: 0x06007F50 RID: 32592 RVA: 0x0022E254 File Offset: 0x0022C454
		public ICollection<PropertyDefinition> Columns
		{
			get
			{
				if (this.columnArray == null)
				{
					PropertyDefinition[] array = new PropertyDefinition[this.columnDictionary.Count];
					foreach (KeyValuePair<PropertyDefinition, int> keyValuePair in this.columnDictionary)
					{
						array[keyValuePair.Value] = keyValuePair.Key;
					}
					this.columnArray = array;
				}
				return this.columnArray;
			}
		}

		// Token: 0x170021F5 RID: 8693
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				int num;
				if (this.columnDictionary.TryGetValue(propertyDefinition, out num))
				{
					return this.row[num];
				}
				return new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
			}
		}

		// Token: 0x170021F6 RID: 8694
		public object this[int idx]
		{
			get
			{
				return this.row[idx];
			}
		}

		// Token: 0x06007F53 RID: 32595 RVA: 0x0022E30F File Offset: 0x0022C50F
		public void Bind(object[] row)
		{
			this.row = row;
		}

		// Token: 0x06007F54 RID: 32596 RVA: 0x0022E318 File Offset: 0x0022C518
		public int AddColumn(PropertyDefinition property)
		{
			int count;
			if (!this.columnDictionary.TryGetValue(property, out count))
			{
				count = this.columnDictionary.Count;
				this.columnDictionary[property] = count;
				this.columnArray = null;
			}
			return count;
		}

		// Token: 0x06007F55 RID: 32597 RVA: 0x0022E358 File Offset: 0x0022C558
		public void AddColumnsFromFilter(QueryFilter filter)
		{
			SinglePropertyFilter singlePropertyFilter = filter as SinglePropertyFilter;
			if (singlePropertyFilter != null)
			{
				this.AddColumn(singlePropertyFilter.Property);
				return;
			}
			CompositeFilter compositeFilter = filter as CompositeFilter;
			if (compositeFilter != null)
			{
				foreach (QueryFilter filter2 in compositeFilter.Filters)
				{
					this.AddColumnsFromFilter(filter2);
				}
				return;
			}
			NotFilter notFilter = filter as NotFilter;
			if (notFilter != null)
			{
				this.AddColumnsFromFilter(notFilter.Filter);
				return;
			}
			PropertyComparisonFilter propertyComparisonFilter = filter as PropertyComparisonFilter;
			if (propertyComparisonFilter != null)
			{
				this.AddColumn(propertyComparisonFilter.Property1);
				this.AddColumn(propertyComparisonFilter.Property2);
				return;
			}
			CommentFilter commentFilter = filter as CommentFilter;
			if (commentFilter != null)
			{
				this.AddColumnsFromFilter(commentFilter.Filter);
				return;
			}
			if (filter is FalseFilter || filter is TrueFilter)
			{
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06007F56 RID: 32598 RVA: 0x0022E440 File Offset: 0x0022C640
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400563C RID: 22076
		private object[] row;

		// Token: 0x0400563D RID: 22077
		private Dictionary<PropertyDefinition, int> columnDictionary;

		// Token: 0x0400563E RID: 22078
		private ICollection<PropertyDefinition> columnArray;
	}
}
