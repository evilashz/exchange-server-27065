using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200027E RID: 638
	[XmlType(TypeName = "FieldOrderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SortResults
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x00050A51 File Offset: 0x0004EC51
		// (set) Token: 0x060010A7 RID: 4263 RVA: 0x00050A59 File Offset: 0x0004EC59
		[XmlElement("IndexedFieldURI", typeof(DictionaryPropertyUri))]
		[DataMember(Name = "Path")]
		[XmlElement("FieldURI", typeof(PropertyUri))]
		[XmlElement("ExtendedFieldURI", typeof(ExtendedPropertyUri))]
		[XmlElement("Path")]
		public PropertyPath SortByProperty { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00050A62 File Offset: 0x0004EC62
		// (set) Token: 0x060010A9 RID: 4265 RVA: 0x00050A6A File Offset: 0x0004EC6A
		[IgnoreDataMember]
		[XmlAttribute]
		public SortDirection Order { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x00050A73 File Offset: 0x0004EC73
		// (set) Token: 0x060010AB RID: 4267 RVA: 0x00050A80 File Offset: 0x0004EC80
		[XmlIgnore]
		[DataMember(Name = "Order", IsRequired = true)]
		public string OrderString
		{
			get
			{
				return EnumUtilities.ToString<SortDirection>(this.Order);
			}
			set
			{
				this.Order = EnumUtilities.Parse<SortDirection>(value);
			}
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00050A90 File Offset: 0x0004EC90
		internal static SortBy[] ToXsoSortBy(params SortResults[] sortResults)
		{
			if (sortResults == null)
			{
				return null;
			}
			SortBy[] array = new SortBy[sortResults.Length];
			for (int i = 0; i < sortResults.Length; i++)
			{
				SortResults sortResults2 = sortResults[i];
				PropertyDefinition propertyDefinition;
				if (!SearchSchemaMap.TryGetPropertyDefinition(sortResults2.SortByProperty, out propertyDefinition))
				{
					throw new UnsupportedPathForSortGroupException(sortResults2.SortByProperty);
				}
				StorePropertyDefinition storePropertyDefinition = (StorePropertyDefinition)propertyDefinition;
				if ((storePropertyDefinition.Capabilities & StorePropertyCapabilities.CanSortBy) != StorePropertyCapabilities.CanSortBy)
				{
					throw new UnsupportedPathForSortGroupException(sortResults2.SortByProperty);
				}
				SortOrder order = (SortOrder)sortResults2.Order;
				array[i] = new SortBy(storePropertyDefinition, order);
			}
			return array;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00050B10 File Offset: 0x0004ED10
		internal static SortResults[] FromXsoSortBy(params SortBy[] sortBys)
		{
			if (sortBys == null)
			{
				return null;
			}
			SortResults[] array = new SortResults[sortBys.Length];
			for (int i = 0; i < sortBys.Length; i++)
			{
				SortBy sortBy = sortBys[i];
				PropertyPath sortByProperty;
				if (!SearchSchemaMap.TryGetPropertyPath(sortBy.ColumnDefinition, out sortByProperty))
				{
					throw new UnsupportedPropertyDefinitionException(sortBy.ColumnDefinition.Name);
				}
				SortResults sortResults = new SortResults
				{
					SortByProperty = sortByProperty,
					Order = (SortDirection)sortBy.SortOrder
				};
				array[i] = sortResults;
			}
			return array;
		}
	}
}
