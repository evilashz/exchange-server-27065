using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000265 RID: 613
	[XmlType(TypeName = "GroupByType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GroupByType : BaseGroupByType
	{
		// Token: 0x0600100C RID: 4108 RVA: 0x0004DA52 File Offset: 0x0004BC52
		public GroupByType()
		{
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0004DA5A File Offset: 0x0004BC5A
		internal GroupByType(PropertyPath aggregateOnProperty, AggregateType aggregateType, PropertyPath groupByProperty, SortDirection sortDirection) : base(sortDirection)
		{
			this.groupByProperty = groupByProperty;
			this.AggregateOn = new AggregateOnType(aggregateOnProperty, aggregateType);
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0004DA78 File Offset: 0x0004BC78
		// (set) Token: 0x0600100F RID: 4111 RVA: 0x0004DA80 File Offset: 0x0004BC80
		[XmlElement("ExtendedFieldURI", typeof(ExtendedPropertyUri), IsNullable = false)]
		[DataMember(Name = "GroupByProperty", Order = 1)]
		[XmlElement("IndexedFieldURI", typeof(DictionaryPropertyUri), IsNullable = false)]
		[XmlElement("FieldURI", typeof(PropertyUri), IsNullable = false)]
		public PropertyPath GroupByProperty
		{
			get
			{
				return this.groupByProperty;
			}
			set
			{
				this.groupByProperty = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x0004DA89 File Offset: 0x0004BC89
		// (set) Token: 0x06001011 RID: 4113 RVA: 0x0004DA91 File Offset: 0x0004BC91
		[XmlElement(ElementName = "AggregateOn", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(Name = "AggregateOn", Order = 2)]
		public AggregateOnType AggregateOn
		{
			get
			{
				return this.aggregateOn;
			}
			set
			{
				this.aggregateOn = value;
			}
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0004DA9A File Offset: 0x0004BC9A
		internal GroupSort ToGroupSort()
		{
			return new GroupSort(BaseGroupByType.PropertyPathToPropertyDefinition(this.aggregateOn.AggregationProperty), (SortOrder)base.SortDirection, (Aggregate)this.AggregateOn.Aggregate);
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x0004DAC4 File Offset: 0x0004BCC4
		internal PropertyDefinition GroupByPropertyDefinition
		{
			get
			{
				if (this.groupByPropertyDefinition == null)
				{
					if (!SearchSchemaMap.TryGetPropertyDefinition(this.groupByProperty, out this.groupByPropertyDefinition))
					{
						throw new UnsupportedPathForSortGroupException(this.groupByProperty);
					}
					StorePropertyDefinition storePropertyDefinition = (StorePropertyDefinition)this.groupByPropertyDefinition;
					if ((storePropertyDefinition.Capabilities & StorePropertyCapabilities.CanGroupBy) != StorePropertyCapabilities.CanGroupBy)
					{
						throw new UnsupportedPathForSortGroupException(this.groupByProperty);
					}
				}
				return this.groupByPropertyDefinition;
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0004DB24 File Offset: 0x0004BD24
		internal override BasePageResult IssueQuery(QueryFilter query, Folder folder, SortBy[] sortBy, BasePagingType paging, ItemQueryTraversal traversal, PropertyDefinition[] propsToFetch, RequestDetailsLogger logger)
		{
			BasePageResult result;
			using (GroupedQueryResult groupedQueryResult = folder.GroupedItemQuery(query, this.GroupByPropertyDefinition, this.ToGroupSort(), sortBy, propsToFetch))
			{
				int num = 0;
				while (num < propsToFetch.Length && propsToFetch[num] != this.GroupByPropertyDefinition)
				{
					num++;
				}
				result = BasePagingType.ApplyPostQueryGroupedPaging(groupedQueryResult, paging, num);
			}
			return result;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0004DB8C File Offset: 0x0004BD8C
		internal override PropertyDefinition[] GetAdditionalFetchProperties()
		{
			return new PropertyDefinition[]
			{
				this.GroupByPropertyDefinition
			};
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0004DBAA File Offset: 0x0004BDAA
		internal override QueryType QueryType
		{
			get
			{
				return QueryType.Groups;
			}
		}

		// Token: 0x04000BF8 RID: 3064
		private PropertyPath groupByProperty;

		// Token: 0x04000BF9 RID: 3065
		private AggregateOnType aggregateOn;

		// Token: 0x04000BFA RID: 3066
		private PropertyDefinition groupByPropertyDefinition;
	}
}
