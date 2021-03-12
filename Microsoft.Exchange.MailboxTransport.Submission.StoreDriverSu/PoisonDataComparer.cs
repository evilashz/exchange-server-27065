using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200000E RID: 14
	internal class PoisonDataComparer : IComparer<KeyValuePair<SubmissionPoisonContext, DateTime>>
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00004270 File Offset: 0x00002470
		public int Compare(KeyValuePair<SubmissionPoisonContext, DateTime> x, KeyValuePair<SubmissionPoisonContext, DateTime> y)
		{
			return DateTime.Compare(x.Value, y.Value);
		}
	}
}
