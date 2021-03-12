using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000296 RID: 662
	public struct ManagedStore_FastTransferTags
	{
		// Token: 0x0400119F RID: 4511
		public const int SourceSend = 0;

		// Token: 0x040011A0 RID: 4512
		public const int IcsDownload = 1;

		// Token: 0x040011A1 RID: 4513
		public const int IcsDownloadState = 2;

		// Token: 0x040011A2 RID: 4514
		public const int IcsUploadState = 3;

		// Token: 0x040011A3 RID: 4515
		public const int FaultInjection = 20;

		// Token: 0x040011A4 RID: 4516
		public static Guid guid = new Guid("e8d090ac-ab71-4752-b432-0b86b6e380e6");
	}
}
