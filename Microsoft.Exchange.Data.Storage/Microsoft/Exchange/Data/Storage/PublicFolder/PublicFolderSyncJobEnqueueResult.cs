using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000951 RID: 2385
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PublicFolderSyncJobEnqueueResult : EnqueueResult
	{
		// Token: 0x060058AC RID: 22700 RVA: 0x0016CAFE File Offset: 0x0016ACFE
		public PublicFolderSyncJobEnqueueResult(EnqueueResultType result, PublicFolderSyncJobState syncJobState) : base(result)
		{
			this.PublicFolderSyncJobState = syncJobState;
			this.Type = QueueType.PublicFolder;
		}

		// Token: 0x0400306B RID: 12395
		public PublicFolderSyncJobState PublicFolderSyncJobState;
	}
}
