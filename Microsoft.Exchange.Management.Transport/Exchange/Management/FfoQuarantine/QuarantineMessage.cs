using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.FfoQuarantine
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class QuarantineMessage
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00005653 File Offset: 0x00003853
		public QuarantineMessage()
		{
			this.RecipientAddress = new List<string>();
			this.QuarantinedUser = new List<string>();
			this.ReleasedUser = new List<string>();
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000567C File Offset: 0x0000387C
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00005684 File Offset: 0x00003884
		public string Identity { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000568D File Offset: 0x0000388D
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00005695 File Offset: 0x00003895
		public DateTime ReceivedTime { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000569E File Offset: 0x0000389E
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000056A6 File Offset: 0x000038A6
		public string Organization { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000056AF File Offset: 0x000038AF
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000056B7 File Offset: 0x000038B7
		public string MessageId { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000056C0 File Offset: 0x000038C0
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000056C8 File Offset: 0x000038C8
		public string SenderAddress { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000056D1 File Offset: 0x000038D1
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000056D9 File Offset: 0x000038D9
		public List<string> RecipientAddress { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000056E2 File Offset: 0x000038E2
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000056EA File Offset: 0x000038EA
		public string Subject { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000056F3 File Offset: 0x000038F3
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000056FB File Offset: 0x000038FB
		public int Size { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005704 File Offset: 0x00003904
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000570C File Offset: 0x0000390C
		public QuarantineMessageType Type { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005715 File Offset: 0x00003915
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000571D File Offset: 0x0000391D
		public DateTime Expires { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005726 File Offset: 0x00003926
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000572E File Offset: 0x0000392E
		public List<string> QuarantinedUser { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005737 File Offset: 0x00003937
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000573F File Offset: 0x0000393F
		public List<string> ReleasedUser { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005748 File Offset: 0x00003948
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00005750 File Offset: 0x00003950
		public bool Reported { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005759 File Offset: 0x00003959
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00005761 File Offset: 0x00003961
		public QuarantineMessageDirection Direction { get; set; }
	}
}
