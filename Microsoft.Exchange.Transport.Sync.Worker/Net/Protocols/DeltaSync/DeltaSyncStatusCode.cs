using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x02000065 RID: 101
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class DeltaSyncStatusCode
	{
		// Token: 0x0400026A RID: 618
		internal const int SuccessfulOperation = 1;

		// Token: 0x0400026B RID: 619
		internal const int InvalidSyncKey = 4104;

		// Token: 0x0400026C RID: 620
		internal const int MessageSizeLimitExceeded = 4309;

		// Token: 0x0400026D RID: 621
		internal const int FolderDoesnotExist = 4402;

		// Token: 0x0400026E RID: 622
		internal const int SyncConflict = 4404;

		// Token: 0x0400026F RID: 623
		internal const int PartnerAuthFailureLowerLimit = 3100;

		// Token: 0x04000270 RID: 624
		internal const int PartnerAuthFailureUpperLimit = 3199;

		// Token: 0x04000271 RID: 625
		internal const int UserAccessFailureLowerLimit = 3200;

		// Token: 0x04000272 RID: 626
		internal const int UserDoesNotExist = 3201;

		// Token: 0x04000273 RID: 627
		internal const int NoSystemAccessForUser = 3202;

		// Token: 0x04000274 RID: 628
		internal const int AuthFailure = 3204;

		// Token: 0x04000275 RID: 629
		internal const int OutOfMailboxQuotaDiskSpace = 3205;

		// Token: 0x04000276 RID: 630
		internal const int MaxedOutSyncRelationships = 3206;

		// Token: 0x04000277 RID: 631
		internal const int UserAccessFailureUpperLimit = 3299;

		// Token: 0x04000278 RID: 632
		internal const int RequestFormatErrorLowerLimit = 4100;

		// Token: 0x04000279 RID: 633
		internal const int RequestFormatErrorUpperLimit = 4199;

		// Token: 0x0400027A RID: 634
		internal const int RequestContentErrorLowerLimit = 4200;

		// Token: 0x0400027B RID: 635
		internal const int RequestContentErrorUpperLimit = 4299;

		// Token: 0x0400027C RID: 636
		internal const int SettingsErrorLowerLimit = 4300;

		// Token: 0x0400027D RID: 637
		internal const int SettingsErrorUpperLimit = 4399;

		// Token: 0x0400027E RID: 638
		internal const int DataOutOfSyncErrorLowerLimit = 4400;

		// Token: 0x0400027F RID: 639
		internal const int DataOutOfSyncErrorUpperLimit = 4499;

		// Token: 0x04000280 RID: 640
		internal const int ServerErrorLowerLimit = 5000;

		// Token: 0x04000281 RID: 641
		internal const int ServerErrorUpperLimit = 5999;
	}
}
