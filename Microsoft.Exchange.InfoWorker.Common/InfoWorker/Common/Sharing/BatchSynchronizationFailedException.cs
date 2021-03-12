using System;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000268 RID: 616
	[Serializable]
	public sealed class BatchSynchronizationFailedException : SharingSynchronizationException
	{
		// Token: 0x06001195 RID: 4501 RVA: 0x00050DCD File Offset: 0x0004EFCD
		public BatchSynchronizationFailedException() : base(Strings.BatchSynchronizationFailedException)
		{
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00050DDA File Offset: 0x0004EFDA
		public BatchSynchronizationFailedException(Exception innerException) : base(Strings.BatchSynchronizationFailedException, innerException)
		{
		}
	}
}
