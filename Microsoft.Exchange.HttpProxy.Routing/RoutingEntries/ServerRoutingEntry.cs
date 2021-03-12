using System;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingEntries
{
	// Token: 0x02000020 RID: 32
	internal abstract class ServerRoutingEntry : RoutingEntryBase
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00003297 File Offset: 0x00001497
		protected ServerRoutingEntry(ServerRoutingKey key, long timestamp)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.key = key;
			this.timestamp = timestamp;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000032C1 File Offset: 0x000014C1
		public override IRoutingKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000032C9 File Offset: 0x000014C9
		public override long Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x04000033 RID: 51
		private readonly ServerRoutingKey key;

		// Token: 0x04000034 RID: 52
		private readonly long timestamp;
	}
}
