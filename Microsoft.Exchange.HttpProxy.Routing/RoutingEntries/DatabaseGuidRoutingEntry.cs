using System;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingEntries
{
	// Token: 0x0200001C RID: 28
	internal abstract class DatabaseGuidRoutingEntry : RoutingEntryBase
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000031CF File Offset: 0x000013CF
		protected DatabaseGuidRoutingEntry(DatabaseGuidRoutingKey key, long timestamp)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.key = key;
			this.timestamp = timestamp;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000031F9 File Offset: 0x000013F9
		public override IRoutingKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003201 File Offset: 0x00001401
		public override long Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x0400002D RID: 45
		private readonly DatabaseGuidRoutingKey key;

		// Token: 0x0400002E RID: 46
		private readonly long timestamp;
	}
}
