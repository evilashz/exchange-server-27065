using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000259 RID: 601
	[XmlInclude(typeof(DistinguishedGroupByType))]
	[KnownType(typeof(GroupByType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlInclude(typeof(GroupByType))]
	[XmlType(TypeName = "BaseGroupByType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[KnownType(typeof(DistinguishedGroupByType))]
	[KnownType(typeof(NoGrouping))]
	[Serializable]
	public abstract class BaseGroupByType
	{
		// Token: 0x06000FB2 RID: 4018 RVA: 0x0004CD20 File Offset: 0x0004AF20
		public BaseGroupByType()
		{
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0004CD28 File Offset: 0x0004AF28
		internal BaseGroupByType(SortDirection sortDirection)
		{
			this.sortDirection = sortDirection;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0004CD37 File Offset: 0x0004AF37
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x0004CD3F File Offset: 0x0004AF3F
		[XmlAttribute(AttributeName = "Order")]
		[IgnoreDataMember]
		public SortDirection SortDirection
		{
			get
			{
				return this.sortDirection;
			}
			set
			{
				this.sortDirection = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0004CD48 File Offset: 0x0004AF48
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x0004CD55 File Offset: 0x0004AF55
		[XmlIgnore]
		[DataMember(Name = "Order", EmitDefaultValue = false)]
		public string SortDirectionString
		{
			get
			{
				return EnumUtilities.ToString<SortDirection>(this.SortDirection);
			}
			set
			{
				this.SortDirection = EnumUtilities.Parse<SortDirection>(value);
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0004CD64 File Offset: 0x0004AF64
		internal static PropertyDefinition PropertyPathToPropertyDefinition(PropertyPath propertyPath)
		{
			if (propertyPath == null)
			{
				return null;
			}
			PropertyDefinition propertyDefinition;
			if (!SearchSchemaMap.TryGetPropertyDefinition(propertyPath, out propertyDefinition))
			{
				throw new UnsupportedPathForSortGroupException(propertyPath);
			}
			StorePropertyDefinition storePropertyDefinition = (StorePropertyDefinition)propertyDefinition;
			if ((storePropertyDefinition.Capabilities & StorePropertyCapabilities.CanGroupBy) != StorePropertyCapabilities.CanGroupBy)
			{
				throw new UnsupportedPathForSortGroupException(propertyPath);
			}
			return propertyDefinition;
		}

		// Token: 0x06000FB9 RID: 4025
		internal abstract BasePageResult IssueQuery(QueryFilter query, Folder folder, SortBy[] sortBy, BasePagingType paging, ItemQueryTraversal traversal, PropertyDefinition[] propsToFetch, RequestDetailsLogger logger);

		// Token: 0x06000FBA RID: 4026
		internal abstract PropertyDefinition[] GetAdditionalFetchProperties();

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000FBB RID: 4027
		internal abstract QueryType QueryType { get; }

		// Token: 0x04000BD8 RID: 3032
		private SortDirection sortDirection;
	}
}
