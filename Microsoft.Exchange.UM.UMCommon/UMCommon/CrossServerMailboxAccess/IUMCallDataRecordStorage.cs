using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess
{
	// Token: 0x02000082 RID: 130
	internal interface IUMCallDataRecordStorage : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000471 RID: 1137
		void CreateUMCallDataRecord(CDRData cdrData);

		// Token: 0x06000472 RID: 1138
		CDRData[] GetUMCallDataRecordsForUser(string userLegacyExchangeDN);

		// Token: 0x06000473 RID: 1139
		CDRData[] GetUMCallDataRecords(ExDateTime startDateTime, ExDateTime endDateTime, int offset, int numberOfRecordsToRead);

		// Token: 0x06000474 RID: 1140
		UMReportRawCounters[] GetUMCallSummary(Guid dialPlanGuid, Guid gatewayGuid, GroupBy groupby);
	}
}
