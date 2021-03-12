using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200001F RID: 31
	internal sealed class DeliveryRecipientThreadMap : SynchronizedThreadMap<RoutingAddress>
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00009A4D File Offset: 0x00007C4D
		public DeliveryRecipientThreadMap(Trace tracer) : base(600, tracer, "Delivery recipient", 100, DeliveryConfiguration.Instance.Throttling.RecipientThreadLimit, AckReason.RecipientThreadLimitExceeded, true)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00009A77 File Offset: 0x00007C77
		protected override void LogLimitExceeded(RoutingAddress key, int threadLimit, ulong sessionId, string mdb)
		{
			ConnectionLog.MapiDeliveryConnectionRecipientThreadLimitReached(sessionId, key, mdb, threadLimit);
		}

		// Token: 0x040000B8 RID: 184
		private const string KeyDisplayName = "Delivery recipient";

		// Token: 0x040000B9 RID: 185
		private const int EstimatedCapacity = 600;

		// Token: 0x040000BA RID: 186
		private const int EstimatedEntrySize = 100;
	}
}
