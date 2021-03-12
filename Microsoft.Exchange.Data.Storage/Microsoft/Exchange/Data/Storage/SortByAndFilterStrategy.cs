using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CCE RID: 3278
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal abstract class SortByAndFilterStrategy
	{
		// Token: 0x060071A9 RID: 29097 RVA: 0x001F7BD9 File Offset: 0x001F5DD9
		public static SortByAndFilterStrategy CreateSimpleSort(NativeStorePropertyDefinition property)
		{
			return new SortByAndFilterStrategy.SimpleSortStrategy(property);
		}

		// Token: 0x17001E63 RID: 7779
		// (get) Token: 0x060071AB RID: 29099 RVA: 0x001F7BE9 File Offset: 0x001F5DE9
		public virtual StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.None;
			}
		}

		// Token: 0x060071AC RID: 29100 RVA: 0x001F7BEC File Offset: 0x001F5DEC
		public virtual NativeStorePropertyDefinition GetSortProperty()
		{
			return null;
		}

		// Token: 0x060071AD RID: 29101 RVA: 0x001F7BEF File Offset: 0x001F5DEF
		public virtual SortBy[] GetNativeSortBy(SortOrder sortOrder)
		{
			return null;
		}

		// Token: 0x060071AE RID: 29102 RVA: 0x001F7BF2 File Offset: 0x001F5DF2
		public virtual QueryFilter NativeFilterToSmartFilter(QueryFilter filter, ApplicationAggregatedProperty aggregatedProperty)
		{
			return null;
		}

		// Token: 0x060071AF RID: 29103 RVA: 0x001F7BF5 File Offset: 0x001F5DF5
		public virtual QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter, ApplicationAggregatedProperty aggregatedProperty)
		{
			return null;
		}

		// Token: 0x04004F03 RID: 20227
		public static readonly SortByAndFilterStrategy None = new SortByAndFilterStrategy.NoneStrategy();

		// Token: 0x04004F04 RID: 20228
		public static readonly SortByAndFilterStrategy SimpleCanQuery = new SortByAndFilterStrategy.SimpleCanQueryStrategy();

		// Token: 0x04004F05 RID: 20229
		public static readonly SortByAndFilterStrategy PersonType = new SortByAndFilterStrategy.PersonTypeStrategy();

		// Token: 0x02000CCF RID: 3279
		private sealed class NoneStrategy : SortByAndFilterStrategy
		{
		}

		// Token: 0x02000CD0 RID: 3280
		private sealed class SimpleCanQueryStrategy : SortByAndFilterStrategy
		{
			// Token: 0x17001E64 RID: 7780
			// (get) Token: 0x060071B3 RID: 29107 RVA: 0x001F7C28 File Offset: 0x001F5E28
			public override StorePropertyCapabilities Capabilities
			{
				get
				{
					return StorePropertyCapabilities.CanQuery;
				}
			}
		}

		// Token: 0x02000CD1 RID: 3281
		private sealed class SimpleSortStrategy : SortByAndFilterStrategy
		{
			// Token: 0x060071B4 RID: 29108 RVA: 0x001F7C2B File Offset: 0x001F5E2B
			public SimpleSortStrategy(NativeStorePropertyDefinition property)
			{
				this.property = property;
			}

			// Token: 0x17001E65 RID: 7781
			// (get) Token: 0x060071B5 RID: 29109 RVA: 0x001F7C3A File Offset: 0x001F5E3A
			public override StorePropertyCapabilities Capabilities
			{
				get
				{
					return StorePropertyCapabilities.CanQuery | StorePropertyCapabilities.CanSortBy;
				}
			}

			// Token: 0x060071B6 RID: 29110 RVA: 0x001F7C3D File Offset: 0x001F5E3D
			public override NativeStorePropertyDefinition GetSortProperty()
			{
				return this.property;
			}

			// Token: 0x060071B7 RID: 29111 RVA: 0x001F7C48 File Offset: 0x001F5E48
			public override SortBy[] GetNativeSortBy(SortOrder sortOrder)
			{
				return new SortBy[]
				{
					new SortBy(this.property, sortOrder)
				};
			}

			// Token: 0x060071B8 RID: 29112 RVA: 0x001F7C6C File Offset: 0x001F5E6C
			public override QueryFilter NativeFilterToSmartFilter(QueryFilter filter, ApplicationAggregatedProperty aggregatedProperty)
			{
				SinglePropertyFilter singlePropertyFilter = filter as SinglePropertyFilter;
				if (singlePropertyFilter != null && singlePropertyFilter.Property.Equals(this.property))
				{
					return singlePropertyFilter.CloneWithAnotherProperty(aggregatedProperty);
				}
				return base.NativeFilterToSmartFilter(filter, aggregatedProperty);
			}

			// Token: 0x060071B9 RID: 29113 RVA: 0x001F7CA6 File Offset: 0x001F5EA6
			public override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter, ApplicationAggregatedProperty aggregatedProperty)
			{
				if (filter != null && filter.Property.Equals(aggregatedProperty))
				{
					return filter.CloneWithAnotherProperty(this.property);
				}
				return base.SmartFilterToNativeFilter(filter, aggregatedProperty);
			}

			// Token: 0x04004F06 RID: 20230
			private NativeStorePropertyDefinition property;
		}

		// Token: 0x02000CD2 RID: 3282
		private sealed class PersonTypeStrategy : SortByAndFilterStrategy
		{
			// Token: 0x17001E66 RID: 7782
			// (get) Token: 0x060071BB RID: 29115 RVA: 0x001F7CD6 File Offset: 0x001F5ED6
			public override StorePropertyCapabilities Capabilities
			{
				get
				{
					return StorePropertyCapabilities.CanQuery;
				}
			}

			// Token: 0x060071BC RID: 29116 RVA: 0x001F7CDC File Offset: 0x001F5EDC
			public override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter, ApplicationAggregatedProperty aggregatedProperty)
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				if (comparisonFilter == null || !comparisonFilter.Property.Equals(PersonSchema.PersonType) || comparisonFilter.ComparisonOperator != ComparisonOperator.Equal)
				{
					return base.SmartFilterToNativeFilter(filter, aggregatedProperty);
				}
				string text;
				switch ((PersonType)comparisonFilter.PropertyValue)
				{
				case Microsoft.Exchange.Data.PersonType.Person:
					text = "IPM.Contact";
					break;
				case Microsoft.Exchange.Data.PersonType.DistributionList:
					text = "IPM.DistList";
					break;
				default:
					return base.SmartFilterToNativeFilter(filter, aggregatedProperty);
				}
				return new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, AggregatedContactSchema.MessageClass, text),
					new TextFilter(AggregatedContactSchema.MessageClass, text + ".", MatchOptions.Prefix, MatchFlags.IgnoreCase)
				});
			}
		}
	}
}
