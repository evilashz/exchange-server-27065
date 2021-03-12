using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000019 RID: 25
	internal class ExchangeOperationContext
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005065 File Offset: 0x00003265
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000506D File Offset: 0x0000326D
		public bool IsMoveUser { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005076 File Offset: 0x00003276
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000507E File Offset: 0x0000327E
		public bool IsMoveSource { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005087 File Offset: 0x00003287
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000508F File Offset: 0x0000328F
		public bool IsXForestMove { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005098 File Offset: 0x00003298
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000050A0 File Offset: 0x000032A0
		public bool IsOlcMoveDestination { get; set; }
	}
}
