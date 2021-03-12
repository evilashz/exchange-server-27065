using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007C6 RID: 1990
	internal interface IFullSyncPageToken : ISyncCookie
	{
		// Token: 0x1700231A RID: 8986
		// (get) Token: 0x060062C3 RID: 25283
		bool MoreData { get; }

		// Token: 0x1700231B RID: 8987
		// (get) Token: 0x060062C4 RID: 25284
		BackSyncOptions SyncOptions { get; }

		// Token: 0x060062C5 RID: 25285
		byte[] ToByteArray();

		// Token: 0x1700231C RID: 8988
		// (get) Token: 0x060062C6 RID: 25286
		// (set) Token: 0x060062C7 RID: 25287
		DateTime Timestamp { get; set; }

		// Token: 0x1700231D RID: 8989
		// (get) Token: 0x060062C8 RID: 25288
		// (set) Token: 0x060062C9 RID: 25289
		DateTime LastReadFailureStartTime { get; set; }

		// Token: 0x060062CA RID: 25290
		void PrepareForFailover();
	}
}
