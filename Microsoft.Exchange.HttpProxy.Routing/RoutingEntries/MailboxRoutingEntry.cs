using System;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingEntries
{
	// Token: 0x0200001E RID: 30
	internal abstract class MailboxRoutingEntry : RoutingEntryBase
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003236 File Offset: 0x00001436
		protected MailboxRoutingEntry(IRoutingKey key, long timestamp)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.key = key;
			this.timestamp = timestamp;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000325A File Offset: 0x0000145A
		public override IRoutingKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003262 File Offset: 0x00001462
		public override long Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x04000030 RID: 48
		private readonly IRoutingKey key;

		// Token: 0x04000031 RID: 49
		private readonly long timestamp;
	}
}
