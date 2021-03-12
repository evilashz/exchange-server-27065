using System;
using System.Threading;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000088 RID: 136
	internal class NamespaceStats
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00026EE4 File Offset: 0x000250E4
		public bool IsBlacklisted
		{
			get
			{
				DateTime? dateTime = (DateTime?)this.blacklistEnds;
				return dateTime != null && DateTime.UtcNow < dateTime;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00026F28 File Offset: 0x00025128
		public bool IsTarpitted
		{
			get
			{
				DateTime? dateTime = (DateTime?)this.tarpitEnds;
				return dateTime != null || DateTime.UtcNow < dateTime;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00026F6C File Offset: 0x0002516C
		public bool IsExpired
		{
			get
			{
				DateTime? dateTime = (DateTime?)this.expires;
				return dateTime == null || DateTime.UtcNow > dateTime;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00026FB0 File Offset: 0x000251B0
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00026FDF File Offset: 0x000251DF
		public DateTime BlacklistExpires
		{
			get
			{
				DateTime? dateTime = (DateTime?)this.blacklistEnds;
				if (dateTime == null)
				{
					return DateTime.UtcNow;
				}
				return dateTime.GetValueOrDefault();
			}
			set
			{
				this.blacklistEnds = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00026FF0 File Offset: 0x000251F0
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0002701F File Offset: 0x0002521F
		public DateTime TarpitExpires
		{
			get
			{
				DateTime? dateTime = (DateTime?)this.tarpitEnds;
				if (dateTime == null)
				{
					return DateTime.UtcNow;
				}
				return dateTime.GetValueOrDefault();
			}
			set
			{
				this.tarpitEnds = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00027030 File Offset: 0x00025230
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0002705F File Offset: 0x0002525F
		public DateTime Expires
		{
			get
			{
				DateTime? dateTime = (DateTime?)this.expires;
				if (dateTime == null)
				{
					return DateTime.UtcNow;
				}
				return dateTime.GetValueOrDefault();
			}
			set
			{
				this.expires = value;
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0002706D File Offset: 0x0002526D
		public bool ClaimExpiration()
		{
			return Interlocked.Exchange(ref this.expires, null) != null;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00027081 File Offset: 0x00025281
		public bool ClaimBlacklistExpired()
		{
			return Interlocked.Exchange(ref this.blacklistEnds, null) != null;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00027095 File Offset: 0x00025295
		public bool ClaimTarpitExpired()
		{
			return Interlocked.Exchange(ref this.tarpitEnds, null) != null;
		}

		// Token: 0x04000518 RID: 1304
		private object blacklistEnds;

		// Token: 0x04000519 RID: 1305
		private object expires;

		// Token: 0x0400051A RID: 1306
		private object tarpitEnds;

		// Token: 0x0400051B RID: 1307
		public string Fqdn;

		// Token: 0x0400051C RID: 1308
		public int Count;

		// Token: 0x0400051D RID: 1309
		public int TimedOut;

		// Token: 0x0400051E RID: 1310
		public int Failed;

		// Token: 0x0400051F RID: 1311
		public int BadPassword;

		// Token: 0x04000520 RID: 1312
		public int TokenSize;

		// Token: 0x04000521 RID: 1313
		public DateTime Created = DateTime.UtcNow;

		// Token: 0x04000522 RID: 1314
		public string User = string.Empty;

		// Token: 0x04000523 RID: 1315
		public bool VerifiedNamespace;

		// Token: 0x04000524 RID: 1316
		public int ADFSRulesDeny;
	}
}
