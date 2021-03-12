using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200027A RID: 634
	[XmlType(TypeName = "SeekToConditionPageViewType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SeekToConditionPageView : BasePagingType
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00050669 File Offset: 0x0004E869
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x00050671 File Offset: 0x0004E871
		[XmlAttribute(AttributeName = "BasePoint")]
		[IgnoreDataMember]
		public BasePagingType.PagingOrigin Origin { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x0005067A File Offset: 0x0004E87A
		// (set) Token: 0x0600108E RID: 4238 RVA: 0x00050687 File Offset: 0x0004E887
		[DataMember(Name = "BasePoint", IsRequired = true)]
		[XmlIgnore]
		public string OriginString
		{
			get
			{
				return EnumUtilities.ToString<BasePagingType.PagingOrigin>(this.Origin);
			}
			set
			{
				this.Origin = EnumUtilities.Parse<BasePagingType.PagingOrigin>(value);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00050695 File Offset: 0x0004E895
		// (set) Token: 0x06001090 RID: 4240 RVA: 0x0005069D File Offset: 0x0004E89D
		[DataMember(Name = "Condition", IsRequired = true)]
		[XmlElement("Condition", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public RestrictionType Condition { get; set; }

		// Token: 0x06001091 RID: 4241 RVA: 0x000506A6 File Offset: 0x0004E8A6
		internal override BasePageResult CreatePageResult(IQueryResult queryResult, BaseQueryView view)
		{
			if (this.Origin == BasePagingType.PagingOrigin.End)
			{
				view.RetrievedLastItem = this.isLastPage;
			}
			return new IndexedPageResult(view, (this.Origin == BasePagingType.PagingOrigin.Beginning) ? queryResult.CurrentRow : (queryResult.EstimatedRowCount - queryResult.CurrentRow));
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x000506E0 File Offset: 0x0004E8E0
		internal override void PositionResultSet(IQueryResult queryResult)
		{
			ServiceObjectToFilterConverter serviceObjectToFilterConverter = new ServiceObjectToFilterConverter();
			bool flag = queryResult.SeekToCondition((SeekReference)this.Origin, serviceObjectToFilterConverter.Convert(this.Condition.Item), SeekToConditionFlags.AllowExtendedSeekReferences);
			if (this.Origin == BasePagingType.PagingOrigin.End)
			{
				if (flag)
				{
					base.MaxRows = Math.Min(base.MaxRows, queryResult.CurrentRow + 1);
					queryResult.SeekToOffset(SeekReference.OriginCurrent, -(base.MaxRows - 1));
				}
				else
				{
					queryResult.SeekToOffset(SeekReference.OriginBeginning, queryResult.EstimatedRowCount);
				}
				this.isLastPage = (queryResult.CurrentRow == 0);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00050767 File Offset: 0x0004E967
		internal override bool BudgetInducedTruncationAllowed
		{
			get
			{
				return this.Origin == BasePagingType.PagingOrigin.Beginning;
			}
		}

		// Token: 0x04000C26 RID: 3110
		private bool isLastPage;
	}
}
