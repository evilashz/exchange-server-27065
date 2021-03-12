using System;

namespace Microsoft.Exchange.Configuration.PswsProxy
{
	// Token: 0x020000D3 RID: 211
	internal class ResponseErrorDetail
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0001CF75 File Offset: 0x0001B175
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x0001CF7D File Offset: 0x0001B17D
		internal string Message { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0001CF86 File Offset: 0x0001B186
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x0001CF8E File Offset: 0x0001B18E
		internal string RecommendedAction { get; set; }
	}
}
