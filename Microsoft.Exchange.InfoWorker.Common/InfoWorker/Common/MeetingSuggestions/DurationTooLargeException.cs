using System;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x0200004C RID: 76
	internal class DurationTooLargeException : InvalidParameterException
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00008DE7 File Offset: 0x00006FE7
		public DurationTooLargeException() : base(Strings.descMeetingSuggestionsDurationTooLarge)
		{
			base.ErrorCode = 102;
		}
	}
}
