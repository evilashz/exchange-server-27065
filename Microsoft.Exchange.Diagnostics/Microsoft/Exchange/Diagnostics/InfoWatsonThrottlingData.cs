using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000BF RID: 191
	internal class InfoWatsonThrottlingData
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x00014B94 File Offset: 0x00012D94
		public InfoWatsonThrottlingData(string hash, DateTime nextAllowableLogTimeUtc)
		{
			this.Hash = hash;
			this.NextAllowableLogTimeUtc = nextAllowableLogTimeUtc;
			this.LastAccessTimeUtc = DateTime.UtcNow;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00014BB5 File Offset: 0x00012DB5
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x00014BBD File Offset: 0x00012DBD
		public DateTime NextAllowableLogTimeUtc { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00014BC6 File Offset: 0x00012DC6
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x00014BCE File Offset: 0x00012DCE
		public DateTime LastAccessTimeUtc { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00014BD7 File Offset: 0x00012DD7
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x00014BDF File Offset: 0x00012DDF
		public string Hash { get; private set; }
	}
}
