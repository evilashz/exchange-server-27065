using System;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x0200004B RID: 75
	internal class DurationTooSmallException : InvalidParameterException
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00008DD2 File Offset: 0x00006FD2
		public DurationTooSmallException() : base(Strings.descMeetingSuggestionsDurationTooSmall)
		{
			base.ErrorCode = 101;
		}
	}
}
