using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000024 RID: 36
	internal class UcwaOnlineMeetingScheduler : OnlineMeetingScheduler
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00005372 File Offset: 0x00003572
		public UcwaOnlineMeetingScheduler(string ucwaUrl, OAuthCredentials oauthCredentials, CultureInfo culture) : this(new Uri(ucwaUrl, UriKind.RelativeOrAbsolute), oauthCredentials, culture)
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005384 File Offset: 0x00003584
		public UcwaOnlineMeetingScheduler(Uri ucwaUrl, OAuthCredentials oauthCredentials, CultureInfo culture) : base(ucwaUrl.AbsoluteUri, oauthCredentials)
		{
			if (!Uri.UriSchemeHttps.Equals(ucwaUrl.Scheme))
			{
				throw new ArgumentException("The UCWA URL scheme must be '" + Uri.UriSchemeHttps + "'");
			}
			this.worker = new UcwaNewOnlineMeetingWorker(ucwaUrl, oauthCredentials, culture);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000053D8 File Offset: 0x000035D8
		public UcwaOnlineMeetingScheduler(Uri ucwaUrl, string webTicket) : base(ucwaUrl.AbsoluteUri, null)
		{
			if (!Uri.UriSchemeHttps.Equals(ucwaUrl.Scheme))
			{
				throw new ArgumentException("The UCWA URL scheme must be '" + Uri.UriSchemeHttps + "'");
			}
			this.worker = new UcwaNewOnlineMeetingWorker(ucwaUrl, webTicket);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000542C File Offset: 0x0000362C
		public UcwaOnlineMeetingScheduler(Uri ucwaUrl, UcwaRequestFactory requestFactory) : base(ucwaUrl.AbsoluteUri, null)
		{
			if (!Uri.UriSchemeHttps.Equals(ucwaUrl.Scheme))
			{
				throw new ArgumentException("The UCWA URL scheme must be '" + Uri.UriSchemeHttps + "'");
			}
			this.worker = new UcwaNewOnlineMeetingWorker(ucwaUrl, requestFactory);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000547F File Offset: 0x0000367F
		public override Task<OnlineMeetingResult> GetMeetingAsync(string meetingId)
		{
			if (string.IsNullOrWhiteSpace(meetingId))
			{
				throw new ArgumentNullException("meetingId");
			}
			return this.worker.GetMeetingAsync(meetingId);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000054A0 File Offset: 0x000036A0
		public override Task<OnlineMeetingResult> CreateMeetingAsync(OnlineMeetingSettings meetingSettings)
		{
			return this.worker.CreateDefaultMeetingAsync(meetingSettings);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000054AE File Offset: 0x000036AE
		public override Task<OnlineMeetingResult> UpdateMeetingAsync(string meetingId, OnlineMeetingSettings meetingSettings)
		{
			if (string.IsNullOrWhiteSpace(meetingId))
			{
				throw new ArgumentNullException("meetingId");
			}
			if (meetingSettings == null)
			{
				throw new ArgumentNullException("meetingSettings");
			}
			return this.worker.UpdatePrivateMeetingAsync(meetingId, meetingSettings);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000054DE File Offset: 0x000036DE
		public override Task DeleteMeetingAsync(string meetingId)
		{
			if (string.IsNullOrWhiteSpace(meetingId))
			{
				throw new ArgumentNullException("meetingId");
			}
			return this.worker.DeleteMeetingAsync(meetingId);
		}

		// Token: 0x04000104 RID: 260
		private readonly IOnlineMeetingWorker worker;
	}
}
