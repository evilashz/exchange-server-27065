using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200004F RID: 79
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SessionContextInfo
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x000118A8 File Offset: 0x0000FAA8
		public SessionContextInfo(ExDateTime creationTime, SessionRundownReason? rundownReason, ExDateTime rundownTime, int activityCount, ExDateTime lastActivityTime, string contextCookie, AsyncOperationInfo[] activeAsyncOperations, AsyncOperationInfo[] completedAsyncOperations, AsyncOperationInfo[] failedAsyncOperations)
		{
			this.CreationTime = creationTime;
			this.RundownReason = rundownReason;
			this.RundownTime = rundownTime;
			this.ActivityCount = activityCount;
			this.LastActivityTime = lastActivityTime;
			this.ContextCookie = contextCookie;
			this.ActiveAsyncOperations = activeAsyncOperations;
			this.CompletedAsyncOperations = completedAsyncOperations;
			this.FailedAsyncOperations = failedAsyncOperations;
		}

		// Token: 0x0400014A RID: 330
		public readonly ExDateTime CreationTime;

		// Token: 0x0400014B RID: 331
		public readonly SessionRundownReason? RundownReason;

		// Token: 0x0400014C RID: 332
		public readonly ExDateTime RundownTime;

		// Token: 0x0400014D RID: 333
		public readonly int ActivityCount;

		// Token: 0x0400014E RID: 334
		public readonly ExDateTime LastActivityTime;

		// Token: 0x0400014F RID: 335
		public readonly string ContextCookie;

		// Token: 0x04000150 RID: 336
		public readonly AsyncOperationInfo[] ActiveAsyncOperations;

		// Token: 0x04000151 RID: 337
		public readonly AsyncOperationInfo[] CompletedAsyncOperations;

		// Token: 0x04000152 RID: 338
		public readonly AsyncOperationInfo[] FailedAsyncOperations;
	}
}
