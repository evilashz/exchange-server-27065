using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005D0 RID: 1488
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MdnHeaderType
	{
		// Token: 0x040020E5 RID: 8421
		public const string OriginalRecipient = "Original-recipient";

		// Token: 0x040020E6 RID: 8422
		public const string FinalRecipient = "Final-recipient";

		// Token: 0x040020E7 RID: 8423
		public const string Disposition = "Disposition";

		// Token: 0x040020E8 RID: 8424
		public const string OriginalMessageId = "Original-Message-ID";

		// Token: 0x040020E9 RID: 8425
		public const string CorrelationKey = "X-MSExch-Correlation-Key";

		// Token: 0x040020EA RID: 8426
		public const string DisplayName = "X-Display-Name";
	}
}
