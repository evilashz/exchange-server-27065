using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200005B RID: 91
	internal struct DisclaimerEntry
	{
		// Token: 0x040001F6 RID: 502
		public string TextHash;

		// Token: 0x040001F7 RID: 503
		public string AppendedHtmlText;

		// Token: 0x040001F8 RID: 504
		public string AppendedPlainText;

		// Token: 0x040001F9 RID: 505
		public string PrependedHtmlText;

		// Token: 0x040001FA RID: 506
		public string PrependedPlainText;

		// Token: 0x040001FB RID: 507
		public int[] ValidCodePages;
	}
}
