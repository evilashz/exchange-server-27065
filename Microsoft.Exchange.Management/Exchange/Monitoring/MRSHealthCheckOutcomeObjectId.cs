using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005AA RID: 1450
	[Serializable]
	public class MRSHealthCheckOutcomeObjectId : ObjectId
	{
		// Token: 0x06003306 RID: 13062 RVA: 0x000D0377 File Offset: 0x000CE577
		public MRSHealthCheckOutcomeObjectId(string server)
		{
			this.serverName = server;
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000D0386 File Offset: 0x000CE586
		public override byte[] GetBytes()
		{
			return CommonUtils.ByteSerialize(new LocalizedString(this.serverName));
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000D0398 File Offset: 0x000CE598
		public override string ToString()
		{
			return this.serverName;
		}

		// Token: 0x0400239D RID: 9117
		private readonly string serverName;
	}
}
