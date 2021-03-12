using System;

namespace Microsoft.Exchange.Configuration.PswsProxy
{
	// Token: 0x020000D1 RID: 209
	internal class ResponseContent
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001CE88 File Offset: 0x0001B088
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0001CE90 File Offset: 0x0001B090
		internal string Id { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001CE99 File Offset: 0x0001B099
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0001CEA1 File Offset: 0x0001B0A1
		internal string Command { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0001CEAA File Offset: 0x0001B0AA
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x0001CEB2 File Offset: 0x0001B0B2
		internal ExecutionStatus Status { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001CEBB File Offset: 0x0001B0BB
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0001CEC3 File Offset: 0x0001B0C3
		internal string OutputFormat { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0001CECC File Offset: 0x0001B0CC
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x0001CED4 File Offset: 0x0001B0D4
		internal ResponseErrorRecord Error { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001CEDD File Offset: 0x0001B0DD
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0001CEE5 File Offset: 0x0001B0E5
		internal DateTime ExpirationTime { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0001CEEE File Offset: 0x0001B0EE
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0001CEF6 File Offset: 0x0001B0F6
		internal int WaitMsec { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0001CEFF File Offset: 0x0001B0FF
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0001CF07 File Offset: 0x0001B107
		internal string OutputXml { get; set; }
	}
}
