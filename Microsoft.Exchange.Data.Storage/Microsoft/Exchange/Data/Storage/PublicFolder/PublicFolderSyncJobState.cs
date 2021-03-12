using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000955 RID: 2389
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PublicFolderSyncJobState
	{
		// Token: 0x060058BF RID: 22719 RVA: 0x0016CFB2 File Offset: 0x0016B1B2
		public PublicFolderSyncJobState(PublicFolderSyncJobState.Status status, LocalizedException lastError)
		{
			this.JobStatus = status;
			this.LastError = lastError;
		}

		// Token: 0x0400307A RID: 12410
		public PublicFolderSyncJobState.Status JobStatus;

		// Token: 0x0400307B RID: 12411
		public LocalizedException LastError;

		// Token: 0x02000956 RID: 2390
		public enum Status
		{
			// Token: 0x0400307D RID: 12413
			None,
			// Token: 0x0400307E RID: 12414
			Queued,
			// Token: 0x0400307F RID: 12415
			Completed
		}
	}
}
