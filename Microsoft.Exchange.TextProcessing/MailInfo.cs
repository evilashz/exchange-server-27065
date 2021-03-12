using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000019 RID: 25
	internal class MailInfo
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000B3D6 File Offset: 0x000095D6
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000B3DE File Offset: 0x000095DE
		public DateTime DocumentTime { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000B3E7 File Offset: 0x000095E7
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0000B3EF File Offset: 0x000095EF
		public ulong Key { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000B3F8 File Offset: 0x000095F8
		// (set) Token: 0x060000EC RID: 236 RVA: 0x0000B400 File Offset: 0x00009600
		public LshFingerprint Fingerprint { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000B409 File Offset: 0x00009609
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000B411 File Offset: 0x00009611
		public ulong SenderDomainHash { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000B41A File Offset: 0x0000961A
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x0000B422 File Offset: 0x00009622
		public ulong SenderHash { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000B42B File Offset: 0x0000962B
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x0000B433 File Offset: 0x00009633
		public ulong[] RecipientsDomainHash { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000B43C File Offset: 0x0000963C
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x0000B444 File Offset: 0x00009644
		public int RecipientNumber { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000B44D File Offset: 0x0000964D
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000B455 File Offset: 0x00009655
		public ulong SubjectHash { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000B45E File Offset: 0x0000965E
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000B466 File Offset: 0x00009666
		public ulong ClientIpHash { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000B46F File Offset: 0x0000966F
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000B477 File Offset: 0x00009677
		public ulong ClientIp24Hash { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000B480 File Offset: 0x00009680
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000B488 File Offset: 0x00009688
		public DirectionEnum EmailDirection { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000B491 File Offset: 0x00009691
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000B499 File Offset: 0x00009699
		public bool SenFeed { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000B4A2 File Offset: 0x000096A2
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000B4AA File Offset: 0x000096AA
		public bool HoneypotFeed { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000B4B3 File Offset: 0x000096B3
		// (set) Token: 0x06000102 RID: 258 RVA: 0x0000B4BB File Offset: 0x000096BB
		public bool FnFeed { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000B4C4 File Offset: 0x000096C4
		// (set) Token: 0x06000104 RID: 260 RVA: 0x0000B4CC File Offset: 0x000096CC
		public bool ThirdPartyFeed { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000B4D5 File Offset: 0x000096D5
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000B4DD File Offset: 0x000096DD
		public bool SewrFeed { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000B4E6 File Offset: 0x000096E6
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000B4EE File Offset: 0x000096EE
		public bool SpamVerdict { get; set; }
	}
}
