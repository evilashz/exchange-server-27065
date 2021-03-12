using System;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x0200004A RID: 74
	internal class InvalidTimeIntervalException : InvalidParameterException
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00008DBD File Offset: 0x00006FBD
		public InvalidTimeIntervalException() : base(Strings.descMeetingSuggestionsInvalidTimeInterval)
		{
			base.ErrorCode = 100;
		}
	}
}
