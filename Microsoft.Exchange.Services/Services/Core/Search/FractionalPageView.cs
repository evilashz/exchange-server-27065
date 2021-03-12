using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200026A RID: 618
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "FractionalPageFolderView", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class FractionalPageView : BasePagingType
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0004DD0C File Offset: 0x0004BF0C
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x0004DD14 File Offset: 0x0004BF14
		[XmlAttribute]
		[DataMember(Name = "Numerator", IsRequired = true)]
		public int Numerator { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x0004DD1D File Offset: 0x0004BF1D
		// (set) Token: 0x06001028 RID: 4136 RVA: 0x0004DD25 File Offset: 0x0004BF25
		[DataMember(Name = "Denominator", IsRequired = true)]
		[XmlAttribute]
		public int Denominator { get; set; }

		// Token: 0x06001029 RID: 4137 RVA: 0x0004DD2E File Offset: 0x0004BF2E
		internal override BasePageResult CreatePageResult(IQueryResult queryResult, BaseQueryView view)
		{
			return new FractionalPageResult(view, queryResult.CurrentRow, queryResult.EstimatedRowCount);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0004DD44 File Offset: 0x0004BF44
		internal override void PositionResultSet(IQueryResult queryResult)
		{
			if (this.Numerator > this.Denominator || this.Numerator < 0 || this.Denominator <= 0)
			{
				throw new InvalidFractionalPagingParametersException();
			}
			int offset = (int)((float)this.Numerator / (float)this.Denominator * (float)queryResult.EstimatedRowCount);
			queryResult.SeekToOffset(SeekReference.OriginBeginning, offset);
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0004DD99 File Offset: 0x0004BF99
		internal override bool BudgetInducedTruncationAllowed
		{
			get
			{
				return true;
			}
		}
	}
}
