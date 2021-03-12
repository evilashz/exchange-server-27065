using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000034 RID: 52
	internal class ThrottleSession
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000C236 File Offset: 0x0000A436
		public ThrottleSession(long sessionId)
		{
			this.sessionId = sessionId;
			this.recipients = new Dictionary<RoutingAddress, int>();
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000C250 File Offset: 0x0000A450
		public long SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000C258 File Offset: 0x0000A458
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000C260 File Offset: 0x0000A460
		public Guid? Mdb { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000C269 File Offset: 0x0000A469
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000C271 File Offset: 0x0000A471
		public long MessageSize { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000C27A File Offset: 0x0000A47A
		public Dictionary<RoutingAddress, int> Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x0400010D RID: 269
		private readonly long sessionId;

		// Token: 0x0400010E RID: 270
		private Dictionary<RoutingAddress, int> recipients;
	}
}
