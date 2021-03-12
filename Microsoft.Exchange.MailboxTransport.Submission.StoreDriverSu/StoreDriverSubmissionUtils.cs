using System;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200003E RID: 62
	internal static class StoreDriverSubmissionUtils
	{
		// Token: 0x06000246 RID: 582 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		public static uint MapSubmissionStatusErrorCodeToPoisonErrorCode(uint errorCode)
		{
			if (errorCode == 1U)
			{
				return 22U;
			}
			if (errorCode == 5U)
			{
				return 23U;
			}
			return 24U;
		}
	}
}
