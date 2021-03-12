using System;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000106 RID: 262
	internal class PAAConstants
	{
		// Token: 0x040004E4 RID: 1252
		internal static readonly Version CurrentVersion = new Version(14, 0, 0, 0);

		// Token: 0x040004E5 RID: 1253
		internal static readonly TimeSpan PAAEvaluationTimeout = TimeSpan.FromSeconds(3.0);

		// Token: 0x040004E6 RID: 1254
		internal static readonly TimeSpan PAAGreetingDownloadTimeout = TimeSpan.FromSeconds(1.0);

		// Token: 0x040004E7 RID: 1255
		internal static readonly int PhoneNumberCallerIdEvaluationCost = 0;

		// Token: 0x040004E8 RID: 1256
		internal static readonly int ADContactCallerIdEvaluationCost = 1;

		// Token: 0x040004E9 RID: 1257
		internal static readonly int ContactItemCallerIdEvaluationCost = 2;

		// Token: 0x040004EA RID: 1258
		internal static readonly int PersonaContactCallerIdEvaluationCost = 2;

		// Token: 0x040004EB RID: 1259
		internal static readonly int ContactFolderCallerIdEvaluationCost = 2;
	}
}
