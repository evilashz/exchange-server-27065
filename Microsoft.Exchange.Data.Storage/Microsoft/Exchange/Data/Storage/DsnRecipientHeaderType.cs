using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005CF RID: 1487
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class DsnRecipientHeaderType
	{
		// Token: 0x040020DD RID: 8413
		internal const string OriginalRecipient = "Original-recipient";

		// Token: 0x040020DE RID: 8414
		internal const string FinalRecipient = "Final-recipient";

		// Token: 0x040020DF RID: 8415
		internal const string Action = "Action";

		// Token: 0x040020E0 RID: 8416
		internal const string Status = "Status";

		// Token: 0x040020E1 RID: 8417
		internal const string RemoteMta = "Remote-MTA";

		// Token: 0x040020E2 RID: 8418
		internal const string DiagnosticCode = "Diagnostic-code";

		// Token: 0x040020E3 RID: 8419
		internal const string DisplayName = "X-Display-Name";

		// Token: 0x040020E4 RID: 8420
		internal const string SupplementaryInfo = "X-Supplementary-Info";
	}
}
