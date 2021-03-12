using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200026D RID: 621
	[XmlType(TypeName = "IndexedPageFolderView", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class IndexedPageView : BasePagingType
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0004E134 File Offset: 0x0004C334
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x0004E13C File Offset: 0x0004C33C
		[XmlAttribute]
		[DataMember]
		public int Offset { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0004E145 File Offset: 0x0004C345
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x0004E14D File Offset: 0x0004C34D
		[XmlAttribute(AttributeName = "BasePoint")]
		[IgnoreDataMember]
		public BasePagingType.PagingOrigin Origin { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0004E156 File Offset: 0x0004C356
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x0004E163 File Offset: 0x0004C363
		[XmlIgnore]
		[DataMember(Name = "BasePoint", IsRequired = true)]
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

		// Token: 0x0600103E RID: 4158 RVA: 0x0004E174 File Offset: 0x0004C374
		internal override BasePageResult CreatePageResult(IQueryResult queryResult, BaseQueryView view)
		{
			if (this.Origin == BasePagingType.PagingOrigin.End)
			{
				view.RetrievedLastItem = (this.startPoint == queryResult.EstimatedRowCount);
			}
			int indexedOffset;
			if (this.Origin == BasePagingType.PagingOrigin.Beginning)
			{
				indexedOffset = (base.RowsFetchedSpecified ? (base.RowsFetched + this.Offset) : queryResult.CurrentRow);
			}
			else
			{
				indexedOffset = this.startPoint;
			}
			return new IndexedPageResult(view, indexedOffset);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0004E1D4 File Offset: 0x0004C3D4
		internal override void PositionResultSet(IQueryResult queryResult)
		{
			if (this.Offset < 0)
			{
				throw new InvalidIndexedPagingParametersException();
			}
			if (this.Origin == BasePagingType.PagingOrigin.Beginning)
			{
				if (this.Offset != 0)
				{
					queryResult.SeekToOffset((SeekReference)this.Origin, this.Offset);
				}
				this.startPoint = 0;
				return;
			}
			int num = queryResult.EstimatedRowCount - this.Offset - base.MaxRows;
			if (num < 0)
			{
				base.MaxRows += num;
			}
			queryResult.SeekToOffset((SeekReference)this.Origin, -this.Offset - base.MaxRows);
			this.startPoint = queryResult.EstimatedRowCount - queryResult.CurrentRow;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0004E270 File Offset: 0x0004C470
		internal override bool BudgetInducedTruncationAllowed
		{
			get
			{
				return this.Origin == BasePagingType.PagingOrigin.Beginning;
			}
		}

		// Token: 0x04000C09 RID: 3081
		private int startPoint;
	}
}
