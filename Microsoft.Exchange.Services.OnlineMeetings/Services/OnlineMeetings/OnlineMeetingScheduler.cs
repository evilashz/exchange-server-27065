using System;
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x0200001E RID: 30
	internal abstract class OnlineMeetingScheduler : UcwaServerToServerClient
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000032C0 File Offset: 0x000014C0
		protected OnlineMeetingScheduler(string ucwaUrl, ICredentials oauthCredentials) : base(ucwaUrl, oauthCredentials)
		{
		}

		// Token: 0x060000A2 RID: 162
		public abstract Task<OnlineMeetingResult> GetMeetingAsync(string meetingId);

		// Token: 0x060000A3 RID: 163
		public abstract Task<OnlineMeetingResult> CreateMeetingAsync(OnlineMeetingSettings meetingSettings);

		// Token: 0x060000A4 RID: 164
		public abstract Task<OnlineMeetingResult> UpdateMeetingAsync(string meetingId, OnlineMeetingSettings meetingSettings);

		// Token: 0x060000A5 RID: 165
		public abstract Task DeleteMeetingAsync(string meetingId);
	}
}
