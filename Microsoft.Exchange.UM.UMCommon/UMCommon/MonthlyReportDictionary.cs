using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000143 RID: 323
	[CollectionDataContract(Namespace = "http://schemas.microsoft.com/v1.0/UMReportAggregatedData", ItemName = "MonthlyReportEntry", KeyName = "Month", ValueName = "UMReportRawCounters")]
	internal class MonthlyReportDictionary : UMReportDictionaryBase
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00026904 File Offset: 0x00024B04
		public override int MaxItemsInDictionary
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x0400058F RID: 1423
		private const int MaxMonthlyData = 12;
	}
}
