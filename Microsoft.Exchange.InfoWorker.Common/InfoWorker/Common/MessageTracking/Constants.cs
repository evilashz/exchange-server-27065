using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000296 RID: 662
	internal static class Constants
	{
		// Token: 0x04000C63 RID: 3171
		public const int ExpectedNodesInFailoverGroup = 5;

		// Token: 0x04000C64 RID: 3172
		public const int DefaultClientSideTimeoutMilliseconds = 240000;

		// Token: 0x04000C65 RID: 3173
		public const int DefaultServerSideCallOverheadMilliseconds = 3000;

		// Token: 0x04000C66 RID: 3174
		public const int DefaultServerSideTimeoutMilliseconds = 237000;

		// Token: 0x04000C67 RID: 3175
		public static readonly TimeSpan DefaultServerSideTimeout = TimeSpan.FromMilliseconds(237000.0);

		// Token: 0x04000C68 RID: 3176
		public static readonly TimeSpan MaximumTimeout = TimeSpan.FromDays(1.0);

		// Token: 0x04000C69 RID: 3177
		public static readonly int E14SP1ModerationReferralSupportVersion = new ServerVersion(14, 1, 176, 0).ToInt();
	}
}
