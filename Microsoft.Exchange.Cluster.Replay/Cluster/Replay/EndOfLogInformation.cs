using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002D5 RID: 725
	internal class EndOfLogInformation
	{
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x0007DD03 File Offset: 0x0007BF03
		// (set) Token: 0x06001C94 RID: 7316 RVA: 0x0007DD0B File Offset: 0x0007BF0B
		public bool E00Exists { get; set; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x0007DD14 File Offset: 0x0007BF14
		// (set) Token: 0x06001C96 RID: 7318 RVA: 0x0007DD1C File Offset: 0x0007BF1C
		public long Generation { get; set; }

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x0007DD25 File Offset: 0x0007BF25
		// (set) Token: 0x06001C98 RID: 7320 RVA: 0x0007DD2D File Offset: 0x0007BF2D
		public int ByteOffset { get; set; }

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x0007DD36 File Offset: 0x0007BF36
		// (set) Token: 0x06001C9A RID: 7322 RVA: 0x0007DD3E File Offset: 0x0007BF3E
		public DateTime LastWriteUtc { get; set; }

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x0007DD47 File Offset: 0x0007BF47
		// (set) Token: 0x06001C9C RID: 7324 RVA: 0x0007DD4F File Offset: 0x0007BF4F
		public EseLogRecordPosition LastLogRecPos { get; set; }
	}
}
