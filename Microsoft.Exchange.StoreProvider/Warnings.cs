using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class Warnings
	{
		// Token: 0x0400018A RID: 394
		internal const int MapiNoService = 262659;

		// Token: 0x0400018B RID: 395
		internal const int MapiErrorsReturned = 263040;

		// Token: 0x0400018C RID: 396
		internal const int MapiPositionChanged = 263297;

		// Token: 0x0400018D RID: 397
		internal const int MapiApproxCount = 263298;

		// Token: 0x0400018E RID: 398
		internal const int MapiCancelMessage = 263552;

		// Token: 0x0400018F RID: 399
		internal const int MapiPartialCompletion = 263808;

		// Token: 0x04000190 RID: 400
		internal const int MapiSecurityRequiredLow = 263809;

		// Token: 0x04000191 RID: 401
		internal const int MapiSecuirtyRequiredMedium = 263810;

		// Token: 0x04000192 RID: 402
		internal const int MapiPartialItems = 263815;

		// Token: 0x04000193 RID: 403
		internal const int SyncProgress = 264224;

		// Token: 0x04000194 RID: 404
		internal const int SyncClientChangeNewer = 264225;
	}
}
