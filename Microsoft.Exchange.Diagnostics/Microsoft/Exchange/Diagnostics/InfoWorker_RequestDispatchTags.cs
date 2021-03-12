using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000243 RID: 579
	public struct InfoWorker_RequestDispatchTags
	{
		// Token: 0x04000F34 RID: 3892
		public const int RequestRouting = 0;

		// Token: 0x04000F35 RID: 3893
		public const int DistributionListHandling = 1;

		// Token: 0x04000F36 RID: 3894
		public const int ProxyWebRequest = 2;

		// Token: 0x04000F37 RID: 3895
		public const int FaultInjection = 3;

		// Token: 0x04000F38 RID: 3896
		public const int GetFolderRequest = 4;

		// Token: 0x04000F39 RID: 3897
		public static Guid guid = new Guid("92915F00-6982-4d61-818A-6931EBA87182");
	}
}
