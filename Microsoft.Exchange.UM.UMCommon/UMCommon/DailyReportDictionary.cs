using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000142 RID: 322
	[CollectionDataContract(Namespace = "http://schemas.microsoft.com/v1.0/UMReportAggregatedData", ItemName = "DailyReportEntry", KeyName = "Day", ValueName = "UMReportRawCounters")]
	internal class DailyReportDictionary : UMReportDictionaryBase
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x000268F8 File Offset: 0x00024AF8
		public override int MaxItemsInDictionary
		{
			get
			{
				return 90;
			}
		}

		// Token: 0x0400058E RID: 1422
		private const int MaxDailyData = 90;
	}
}
