using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200027B RID: 635
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SeekToConditionWithOffsetPageView : SeekToConditionPageView
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x0005077A File Offset: 0x0004E97A
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x00050782 File Offset: 0x0004E982
		[DataMember]
		public int Offset { get; set; }

		// Token: 0x06001097 RID: 4247 RVA: 0x0005078B File Offset: 0x0004E98B
		internal override BasePageResult ApplyPostQueryPaging(IQueryResult queryResult)
		{
			throw new NotSupportedException("ApplyPostQueryPaging is not supported on SeekToConditionWithOffsetPageView.");
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00050797 File Offset: 0x0004E997
		internal override BasePageResult CreatePageResult(IQueryResult queryResult, BaseQueryView view)
		{
			throw new NotSupportedException("ApplyPostQueryPaging is not supported on SeekToConditionWithOffsetPageView.");
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000507A3 File Offset: 0x0004E9A3
		internal override void PositionResultSet(IQueryResult queryResult)
		{
			throw new NotSupportedException("ApplyPostQueryPaging is not supported on SeekToConditionWithOffsetPageView.");
		}
	}
}
