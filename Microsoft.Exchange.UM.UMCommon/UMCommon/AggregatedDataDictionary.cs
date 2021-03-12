using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000140 RID: 320
	[CollectionDataContract(Namespace = "http://schemas.microsoft.com/v1.0/UMReportAggregatedData", ItemName = "AggregatedDataEntry", KeyName = "UMReportTuple", ValueName = "UMReportTupleData")]
	internal class AggregatedDataDictionary : Dictionary<UMReportTuple, UMReportTupleData>
	{
		// Token: 0x06000A29 RID: 2601 RVA: 0x000268DF File Offset: 0x00024ADF
		public AggregatedDataDictionary()
		{
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000268E7 File Offset: 0x00024AE7
		public AggregatedDataDictionary(int count) : base(count)
		{
		}
	}
}
