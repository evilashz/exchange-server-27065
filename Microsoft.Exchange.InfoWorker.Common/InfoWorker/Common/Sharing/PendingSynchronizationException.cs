using System;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000267 RID: 615
	[Serializable]
	public sealed class PendingSynchronizationException : SharingSynchronizationException
	{
		// Token: 0x06001193 RID: 4499 RVA: 0x00050DB2 File Offset: 0x0004EFB2
		public PendingSynchronizationException() : base(Strings.PendingSynchronizationException)
		{
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00050DBF File Offset: 0x0004EFBF
		public PendingSynchronizationException(Exception innerException) : base(Strings.PendingSynchronizationException, innerException)
		{
		}
	}
}
