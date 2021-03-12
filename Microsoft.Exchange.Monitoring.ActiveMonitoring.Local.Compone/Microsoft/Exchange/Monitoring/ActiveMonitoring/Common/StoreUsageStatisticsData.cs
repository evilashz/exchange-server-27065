using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000108 RID: 264
	public class StoreUsageStatisticsData
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x0002F630 File Offset: 0x0002D830
		public StoreUsageStatisticsData(string digestCategory, int sampleId, DateTime sampleTime, string mailboxGuid, string displayName, int timeInServer, int ropCount, int logRecordBytes, int logRecordCount, int timeInCpu, int pageRead, int pagePreRead, int ldapReads, int ldapSearches, bool isMailboxQuarantined)
		{
			this.DigestCategory = digestCategory;
			this.SampleId = sampleId;
			this.SampleTime = sampleTime;
			this.MailboxGuid = mailboxGuid;
			this.DisplayName = displayName;
			this.TimeInServer = timeInServer;
			this.RopCount = ropCount;
			this.LogRecordBytes = logRecordBytes;
			this.LogRecordCount = logRecordCount;
			this.TimeInCPU = timeInCpu;
			this.PageRead = pageRead;
			this.PagePreread = pagePreRead;
			this.LdapReads = ldapReads;
			this.LdapSearches = ldapSearches;
			this.IsMailboxQuarantined = isMailboxQuarantined;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0002F6B8 File Offset: 0x0002D8B8
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0002F6C0 File Offset: 0x0002D8C0
		public string DigestCategory { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0002F6C9 File Offset: 0x0002D8C9
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x0002F6D1 File Offset: 0x0002D8D1
		public int SampleId { get; private set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0002F6DA File Offset: 0x0002D8DA
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0002F6E2 File Offset: 0x0002D8E2
		public DateTime SampleTime { get; private set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0002F6EB File Offset: 0x0002D8EB
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0002F6F3 File Offset: 0x0002D8F3
		public string MailboxGuid { get; private set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0002F6FC File Offset: 0x0002D8FC
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0002F704 File Offset: 0x0002D904
		public string DisplayName { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0002F70D File Offset: 0x0002D90D
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x0002F715 File Offset: 0x0002D915
		public int TimeInServer { get; private set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0002F71E File Offset: 0x0002D91E
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0002F726 File Offset: 0x0002D926
		public int RopCount { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0002F72F File Offset: 0x0002D92F
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0002F737 File Offset: 0x0002D937
		public int LogRecordBytes { get; private set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0002F740 File Offset: 0x0002D940
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0002F748 File Offset: 0x0002D948
		public int LogRecordCount { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0002F751 File Offset: 0x0002D951
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x0002F759 File Offset: 0x0002D959
		public int TimeInCPU { get; private set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0002F762 File Offset: 0x0002D962
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x0002F76A File Offset: 0x0002D96A
		public int PageRead { get; private set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0002F773 File Offset: 0x0002D973
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x0002F77B File Offset: 0x0002D97B
		public int PagePreread { get; private set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0002F784 File Offset: 0x0002D984
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0002F78C File Offset: 0x0002D98C
		public int LdapReads { get; private set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0002F795 File Offset: 0x0002D995
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0002F79D File Offset: 0x0002D99D
		public int LdapSearches { get; private set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0002F7A6 File Offset: 0x0002D9A6
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x0002F7AE File Offset: 0x0002D9AE
		public bool IsMailboxQuarantined { get; private set; }
	}
}
