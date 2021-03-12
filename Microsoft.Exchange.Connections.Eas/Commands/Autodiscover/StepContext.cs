using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class StepContext
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00003F85 File Offset: 0x00002185
		public StepContext(AutodiscoverRequest request, EasConnectionSettings easConnectionSettings)
		{
			this.Request = request;
			this.EasConnectionSettings = easConnectionSettings;
			this.ProbeStack = new Stack<string>();
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003FA6 File Offset: 0x000021A6
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00003FAE File Offset: 0x000021AE
		internal AutodiscoverRequest Request { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003FB7 File Offset: 0x000021B7
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003FBF File Offset: 0x000021BF
		internal EasConnectionSettings EasConnectionSettings { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003FC8 File Offset: 0x000021C8
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003FD0 File Offset: 0x000021D0
		internal Stack<string> ProbeStack { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003FD9 File Offset: 0x000021D9
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003FE1 File Offset: 0x000021E1
		internal AutodiscoverResponse Response { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003FEA File Offset: 0x000021EA
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00003FF2 File Offset: 0x000021F2
		internal HttpStatusCode HttpStatusCode { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003FFB File Offset: 0x000021FB
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004003 File Offset: 0x00002203
		internal Exception Error { get; set; }
	}
}
