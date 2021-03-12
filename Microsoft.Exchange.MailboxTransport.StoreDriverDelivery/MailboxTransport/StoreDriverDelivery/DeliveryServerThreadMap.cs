using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000020 RID: 32
	internal sealed class DeliveryServerThreadMap : SynchronizedThreadMap<string>
	{
		// Token: 0x060001BA RID: 442 RVA: 0x00009A84 File Offset: 0x00007C84
		public DeliveryServerThreadMap(Trace tracer) : base(1, StringComparer.OrdinalIgnoreCase, tracer, "Delivery server", 100, DeliveryConfiguration.Instance.Throttling.MailboxServerThreadLimit, AckReason.MailboxServerThreadLimitExceeded, false)
		{
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009ABA File Offset: 0x00007CBA
		protected override void LogLimitExceeded(string key, int threadLimit, ulong sessionId, string mdb)
		{
			ConnectionLog.MapiDeliveryConnectionServerThreadLimitReached(mdb, key, threadLimit);
		}

		// Token: 0x040000BB RID: 187
		private const string KeyDisplayName = "Delivery server";

		// Token: 0x040000BC RID: 188
		private const int EstimatedEntrySize = 100;
	}
}
