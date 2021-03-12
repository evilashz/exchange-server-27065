using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000263 RID: 611
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "ContactsView", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ContactsPageView : BasePagingType
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x0004D99C File Offset: 0x0004BB9C
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x0004D9A4 File Offset: 0x0004BBA4
		[XmlAttribute]
		[DataMember(Name = "InitialName", IsRequired = false)]
		public string InitialName { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0004D9AD File Offset: 0x0004BBAD
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x0004D9B5 File Offset: 0x0004BBB5
		[XmlAttribute]
		[DataMember(Name = "FinalName", IsRequired = false)]
		public string FinalName { get; set; }

		// Token: 0x0600100A RID: 4106 RVA: 0x0004D9C0 File Offset: 0x0004BBC0
		internal override QueryFilter ApplyQueryAppend(QueryFilter filter)
		{
			if (!string.IsNullOrEmpty(this.InitialName))
			{
				ComparisonFilter comparisonFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, StoreObjectSchema.DisplayName, this.InitialName);
				if (filter == null)
				{
					filter = comparisonFilter;
				}
				else
				{
					filter = new AndFilter(new QueryFilter[]
					{
						filter,
						comparisonFilter
					});
				}
			}
			if (!string.IsNullOrEmpty(this.FinalName))
			{
				ComparisonFilter comparisonFilter2 = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, StoreObjectSchema.DisplayName, this.FinalName);
				if (filter == null)
				{
					filter = comparisonFilter2;
				}
				else
				{
					filter = new AndFilter(new QueryFilter[]
					{
						filter,
						comparisonFilter2
					});
				}
			}
			return filter;
		}
	}
}
