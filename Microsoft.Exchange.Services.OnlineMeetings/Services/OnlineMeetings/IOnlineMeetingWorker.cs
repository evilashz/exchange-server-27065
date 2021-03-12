using System;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000015 RID: 21
	internal interface IOnlineMeetingWorker
	{
		// Token: 0x06000048 RID: 72
		Task<OnlineMeetingResult> CreateDefaultMeetingAsync(OnlineMeetingSettings meetingSettings);

		// Token: 0x06000049 RID: 73
		Task<OnlineMeetingResult> CreatePrivateMeetingAsync(OnlineMeetingSettings meetingSettings);

		// Token: 0x0600004A RID: 74
		Task DeleteMeetingAsync(string meetingId);

		// Token: 0x0600004B RID: 75
		Task<OnlineMeetingResult> GetMeetingAsync(string meetingId);

		// Token: 0x0600004C RID: 76
		Task<OnlineMeetingResult> UpdatePrivateMeetingAsync(string meetingId, OnlineMeetingSettings meetingSettings);

		// Token: 0x0600004D RID: 77
		Task<OnlineMeetingResult> GetOrCreatePublicMeetingAsync();
	}
}
