using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000E4 RID: 228
	public class SingleCookieRemoveResult
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0002582D File Offset: 0x00023A2D
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00025835 File Offset: 0x00023A35
		public string Cookie { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x0002583E File Offset: 0x00023A3E
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x00025846 File Offset: 0x00023A46
		public bool Removed { get; set; }
	}
}
