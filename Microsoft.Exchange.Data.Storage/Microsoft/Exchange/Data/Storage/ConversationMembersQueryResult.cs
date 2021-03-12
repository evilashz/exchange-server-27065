using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200079F RID: 1951
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConversationMembersQueryResult : QueryResult
	{
		// Token: 0x0600499C RID: 18844 RVA: 0x00134453 File Offset: 0x00132653
		internal ConversationMembersQueryResult(MapiTable mapiTable, ICollection<PropertyDefinition> propertyDefinitions, IList<PropTag> alteredProperties, StoreSession session, bool isTableOwned, SortOrder sortOrder, AggregationExtension aggregationExtension) : base(mapiTable, PropertyDefinitionCollection.Merge<PropertyDefinition>(propertyDefinitions, ConversationMembersQueryResult.RequiredProperties), alteredProperties, session, isTableOwned, sortOrder)
		{
			this.originalProperties = propertyDefinitions;
			this.aggregationExtension = aggregationExtension;
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x0013447D File Offset: 0x0013267D
		public override object[][] ExpandRow(int rowCount, long categoryId, out int rowsInExpandedCategory)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x00134484 File Offset: 0x00132684
		public override int CollapseRow(long categoryId)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x0013448B File Offset: 0x0013268B
		public override byte[] GetCollapseState(byte[] instanceKey)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x00134492 File Offset: 0x00132692
		public override uint SetCollapseState(byte[] collapseState)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x001344A8 File Offset: 0x001326A8
		public override object[][] GetRows(int rowCount, QueryRowsFlags flags, out bool mightBeMoreRows)
		{
			base.CheckDisposed("GetRows");
			object[][] items = this.GetItems<object[]>(rowCount, flags, (PropertyBag item) => item.GetProperties<PropertyDefinition>(this.originalProperties));
			mightBeMoreRows = (items.Length > 0);
			return items;
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x001344E6 File Offset: 0x001326E6
		public override IStorePropertyBag[] GetPropertyBags(int rowCount)
		{
			base.CheckDisposed("GetPropertyBags");
			return this.GetItems<IStorePropertyBag>(rowCount, QueryRowsFlags.None, (PropertyBag item) => item.AsIStorePropertyBag());
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x00134518 File Offset: 0x00132718
		private T PrepareItem<T>(List<IStorePropertyBag> sources, Func<PropertyBag, T> converResultItem)
		{
			this.aggregationExtension.BeforeAggregation(sources);
			PropertyAggregationContext propertyAggregationContext = this.aggregationExtension.GetPropertyAggregationContext(sources);
			PropertyBag arg = ApplicationAggregatedProperty.AggregateAsPropertyBag(propertyAggregationContext, this.originalProperties);
			return converResultItem(arg);
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x00134554 File Offset: 0x00132754
		private T[] GetItems<T>(int rowCount, QueryRowsFlags flags, Func<PropertyBag, T> converResultItem)
		{
			if (rowCount < 0)
			{
				throw new ArgumentOutOfRangeException("rowCount", ServerStrings.ExInvalidRowCount);
			}
			List<T> list = new List<T>(rowCount);
			rowCount = Math.Min(200, rowCount);
			PropValue[][] array = base.Fetch(rowCount, flags);
			if (array != null)
			{
				byte[] array2 = null;
				List<IStorePropertyBag> list2 = null;
				foreach (PropValue[] queryResultRow in array)
				{
					QueryResultPropertyBag queryResultPropertyBag = new QueryResultPropertyBag(base.HeaderPropBag);
					queryResultPropertyBag.SetQueryResultRow(queryResultRow);
					IStorePropertyBag storePropertyBag = queryResultPropertyBag.AsIStorePropertyBag();
					byte[] valueOrDefault = storePropertyBag.GetValueOrDefault<byte[]>(InternalSchema.MapiConversationId, null);
					if (array2 != null && !ArrayComparer<byte>.Comparer.Equals(valueOrDefault, array2))
					{
						list.Add(this.PrepareItem<T>(list2, converResultItem));
						list2 = null;
						array2 = null;
					}
					if (array2 == null)
					{
						array2 = valueOrDefault;
					}
					if (list2 == null)
					{
						list2 = new List<IStorePropertyBag>(1);
					}
					list2.Add(storePropertyBag);
				}
				if (list2 != null)
				{
					list.Add(this.PrepareItem<T>(list2, converResultItem));
				}
			}
			return list.ToArray();
		}

		// Token: 0x040027A3 RID: 10147
		private const int MaximumNumberOfRowsForExpandedConversationView = 200;

		// Token: 0x040027A4 RID: 10148
		private static readonly PropertyDefinition[] RequiredProperties = new PropertyTagPropertyDefinition[]
		{
			InternalSchema.MapiConversationId
		};

		// Token: 0x040027A5 RID: 10149
		private readonly ICollection<PropertyDefinition> originalProperties;

		// Token: 0x040027A6 RID: 10150
		private readonly AggregationExtension aggregationExtension;
	}
}
