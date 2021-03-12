using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200001E RID: 30
	internal sealed class DeliveryDatabaseThreadMap : SynchronizedThreadMap<string>
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00009988 File Offset: 0x00007B88
		public DeliveryDatabaseThreadMap(Trace tracer) : base(DeliveryDatabaseThreadMap.StartingCapacity, StringComparer.OrdinalIgnoreCase, tracer, "Delivery database", 100, new Dictionary<string, int>(DeliveryDatabaseThreadMap.StartingCapacity), AckReason.MailboxDatabaseThreadLimitExceeded, true)
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000099BD File Offset: 0x00007BBD
		public bool TryCheckAndIncrement(string mdbGuid, int threadLimit, ulong sessionId)
		{
			return base.TryCheckThreadLimit(mdbGuid, threadLimit, sessionId, mdbGuid, true);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000099CA File Offset: 0x00007BCA
		public void Check(string key, int threadLimit, ulong sessionId, string mdb)
		{
			base.CheckThreadLimit(key, threadLimit, sessionId, mdb, false);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000099D8 File Offset: 0x00007BD8
		protected override void LogLimitExceeded(string key, int threadLimit, ulong sessionId, string mdb)
		{
			ConnectionLog.MapiDeliveryConnectionMdbThreadLimitReached(mdb, threadLimit);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000099E4 File Offset: 0x00007BE4
		public void UpdateMdbThreadCounters()
		{
			foreach (KeyValuePair<string, int> keyValuePair in base.ThreadMap)
			{
				StoreDriverDatabasePerfCounters.AddDeliveryThreadSample(keyValuePair.Key, (long)keyValuePair.Value);
			}
		}

		// Token: 0x040000B5 RID: 181
		internal const string KeyDisplayName = "Delivery database";

		// Token: 0x040000B6 RID: 182
		private const int EstimatedEntrySize = 100;

		// Token: 0x040000B7 RID: 183
		private static readonly int StartingCapacity = 16;
	}
}
