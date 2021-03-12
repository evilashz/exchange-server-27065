using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x020000F1 RID: 241
	internal sealed class AppStatusErrorCodes
	{
		// Token: 0x040004A8 RID: 1192
		public const string AppUpdateFailed = "1.0";

		// Token: 0x040004A9 RID: 1193
		public const string AppUpdateFailedPermissionChange = "1.1";

		// Token: 0x040004AA RID: 1194
		public const string AppUpdateFailedInvalidEtoken = "1.2";

		// Token: 0x040004AB RID: 1195
		public const string LicenseUpdateFailed = "2.0";

		// Token: 0x040004AC RID: 1196
		public const string LicenseUpdateFailedAndExpired = "2.1";

		// Token: 0x040004AD RID: 1197
		public const string AppStateUnknown = "3.0";

		// Token: 0x040004AE RID: 1198
		public const string AppStateWithdrawn = "3.1";

		// Token: 0x040004AF RID: 1199
		public const string AppStateFlagged = "3.2";

		// Token: 0x040004B0 RID: 1200
		public const string AppStateWithdrawnSoon = "3.3";

		// Token: 0x040004B1 RID: 1201
		public const string AppDisabledByClient = "4.0";

		// Token: 0x040004B2 RID: 1202
		public const string AppDisabledOutlookPerformance = "4.1";

		// Token: 0x040004B3 RID: 1203
		public const string AppInTrialMode = "5.0";
	}
}
