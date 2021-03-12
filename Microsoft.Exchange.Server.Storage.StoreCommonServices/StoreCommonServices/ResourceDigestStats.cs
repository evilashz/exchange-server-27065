using System;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200010A RID: 266
	public struct ResourceDigestStats
	{
		// Token: 0x06000ACA RID: 2762 RVA: 0x00037ACC File Offset: 0x00035CCC
		public ResourceDigestStats(ref JET_THREADSTATS databaseThreadStats)
		{
			this.PageRead = databaseThreadStats.cPageRead;
			this.PagePreread = databaseThreadStats.cPagePreread;
			this.LogRecordCount = databaseThreadStats.cLogRecord;
			this.LogRecordBytes = ((databaseThreadStats.cbLogRecord >= 0) ? databaseThreadStats.cbLogRecord : int.MaxValue);
			this.MailboxGuid = default(Guid);
			this.MailboxNumber = 0;
			this.TimeInServer = default(TimeSpan);
			this.TimeInCPU = default(TimeSpan);
			this.ROPCount = 0;
			this.LdapReads = 0;
			this.LdapSearches = 0;
			this.SampleTime = default(DateTime);
		}

		// Token: 0x040005CD RID: 1485
		public Guid MailboxGuid;

		// Token: 0x040005CE RID: 1486
		public int MailboxNumber;

		// Token: 0x040005CF RID: 1487
		public TimeSpan TimeInServer;

		// Token: 0x040005D0 RID: 1488
		public TimeSpan TimeInCPU;

		// Token: 0x040005D1 RID: 1489
		public int ROPCount;

		// Token: 0x040005D2 RID: 1490
		public int PageRead;

		// Token: 0x040005D3 RID: 1491
		public int PagePreread;

		// Token: 0x040005D4 RID: 1492
		public int LogRecordCount;

		// Token: 0x040005D5 RID: 1493
		public int LogRecordBytes;

		// Token: 0x040005D6 RID: 1494
		public int LdapReads;

		// Token: 0x040005D7 RID: 1495
		public int LdapSearches;

		// Token: 0x040005D8 RID: 1496
		public DateTime SampleTime;
	}
}
