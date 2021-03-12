using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000381 RID: 897
	internal class PeopleSpeechPersonObject
	{
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x00073444 File Offset: 0x00071644
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x0007344C File Offset: 0x0007164C
		internal float Confidence { get; set; }

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00073455 File Offset: 0x00071655
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x0007345D File Offset: 0x0007165D
		internal string Identifier { get; set; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x00073466 File Offset: 0x00071666
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x0007346E File Offset: 0x0007166E
		internal string GALLinkId { get; set; }
	}
}
