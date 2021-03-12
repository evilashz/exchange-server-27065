using System;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x0200025F RID: 607
	[Serializable]
	public sealed class SubscriptionNotFoundException : SharingSynchronizationException
	{
		// Token: 0x0600117F RID: 4479 RVA: 0x00050BD8 File Offset: 0x0004EDD8
		public SubscriptionNotFoundException() : base(Strings.SubscriptionNotFoundException)
		{
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00050BE5 File Offset: 0x0004EDE5
		public SubscriptionNotFoundException(Exception innerException) : base(Strings.SubscriptionNotFoundException, innerException)
		{
		}
	}
}
