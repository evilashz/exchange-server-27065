using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200005C RID: 92
	internal struct HtmlDisclaimerEntry
	{
		// Token: 0x040001FC RID: 508
		public string TextHash;

		// Token: 0x040001FD RID: 509
		public string[] HtmlTextSegments;

		// Token: 0x040001FE RID: 510
		public string[] PlainTextSegments;

		// Token: 0x040001FF RID: 511
		public int[] ValidCodePages;
	}
}
