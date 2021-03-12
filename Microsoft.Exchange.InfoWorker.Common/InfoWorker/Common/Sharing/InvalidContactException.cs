using System;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000264 RID: 612
	[Serializable]
	public sealed class InvalidContactException : SharingSynchronizationException
	{
		// Token: 0x06001189 RID: 4489 RVA: 0x00050C5F File Offset: 0x0004EE5F
		public InvalidContactException() : base(Strings.InvalidContactException)
		{
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00050C6C File Offset: 0x0004EE6C
		public InvalidContactException(Exception innerException) : base(Strings.InvalidContactException, innerException)
		{
		}
	}
}
