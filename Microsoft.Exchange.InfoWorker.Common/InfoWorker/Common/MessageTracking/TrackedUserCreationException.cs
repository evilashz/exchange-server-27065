using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002FA RID: 762
	internal class TrackedUserCreationException : Exception
	{
		// Token: 0x0600168C RID: 5772 RVA: 0x0006912D File Offset: 0x0006732D
		public TrackedUserCreationException(string format, params object[] args) : base(string.Format(format, args))
		{
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0006913C File Offset: 0x0006733C
		public TrackedUserCreationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
