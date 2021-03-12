using System;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.MessageResubmission
{
	// Token: 0x02000132 RID: 306
	internal interface IMessageResubmissionPerfCounters
	{
		// Token: 0x06000D7F RID: 3455
		void ResetCounters();

		// Token: 0x06000D80 RID: 3456
		void UpdateResubmissionCount(int count, bool isShadowResubmit);

		// Token: 0x06000D81 RID: 3457
		ITimerCounter ResubmitMessagesLatencyCounter();

		// Token: 0x06000D82 RID: 3458
		void UpdateResubmitRequestCount(ResubmitRequestState state, int changeAmount);

		// Token: 0x06000D83 RID: 3459
		void ChangeResubmitRequestState(ResubmitRequestState oldState, ResubmitRequestState newState);

		// Token: 0x06000D84 RID: 3460
		void IncrementRecentRequestCount(bool isShadowResubmit);

		// Token: 0x06000D85 RID: 3461
		void RecordResubmitRequestTimeSpan(TimeSpan timeSpan);
	}
}
