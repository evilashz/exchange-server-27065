using System;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x02000048 RID: 72
	internal class ConfigOptions
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00008D75 File Offset: 0x00006F75
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00008D7D File Offset: 0x00006F7D
		public MeetingFrequencyEnum MeetingFrequency
		{
			get
			{
				return this.meetingFrequency;
			}
			set
			{
				this.meetingFrequency = value;
			}
		}

		// Token: 0x04000105 RID: 261
		public const int MaximumResults = 48;

		// Token: 0x04000106 RID: 262
		public const int MinimumMeetingDuration = 30;

		// Token: 0x04000107 RID: 263
		public const int MaximumMeetingDuration = 1440;

		// Token: 0x04000108 RID: 264
		public const int DefaultFreeBusyInterval = 30;

		// Token: 0x04000109 RID: 265
		public const int DefaultSuggestionInterval = 30;

		// Token: 0x0400010A RID: 266
		public const int DefaultMaximumResultsPerDay = 10;

		// Token: 0x0400010B RID: 267
		public const int DefaultMaximumNonWorkHourResultsPerDay = 0;

		// Token: 0x0400010C RID: 268
		public const SuggestionQuality DefaultMinimumSuggestionQuality = SuggestionQuality.Fair;

		// Token: 0x0400010D RID: 269
		public const int DefaultGoodThreshold = 25;

		// Token: 0x0400010E RID: 270
		public const int MaximumGoodThreshold = 49;

		// Token: 0x0400010F RID: 271
		public const int MinimumGoodThreshold = 1;

		// Token: 0x04000110 RID: 272
		internal int FreeBusyInterval = 30;

		// Token: 0x04000111 RID: 273
		internal int SuggestionInterval = 30;

		// Token: 0x04000112 RID: 274
		internal int MaximumResultsPerDay = 10;

		// Token: 0x04000113 RID: 275
		internal int MaximumNonWorkHourResultsPerDay;

		// Token: 0x04000114 RID: 276
		internal SuggestionQuality MinimumSuggestionQuality = SuggestionQuality.Fair;

		// Token: 0x04000115 RID: 277
		internal int GoodThreshold = 25;

		// Token: 0x04000116 RID: 278
		private MeetingFrequencyEnum meetingFrequency = MeetingFrequencyEnum.ThirtyMinutes;
	}
}
