using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000F6B RID: 3947
	public class QuotedPart
	{
		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x060063F8 RID: 25592 RVA: 0x00137E21 File Offset: 0x00136021
		// (set) Token: 0x060063F9 RID: 25593 RVA: 0x00137E29 File Offset: 0x00136029
		public string Header { get; set; }

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x060063FA RID: 25594 RVA: 0x00137E32 File Offset: 0x00136032
		// (set) Token: 0x060063FB RID: 25595 RVA: 0x00137E3A File Offset: 0x0013603A
		public string Body { get; set; }

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x060063FC RID: 25596 RVA: 0x00137E43 File Offset: 0x00136043
		// (set) Token: 0x060063FD RID: 25597 RVA: 0x00137E4B File Offset: 0x0013604B
		public string SentTime { get; set; }

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x060063FE RID: 25598 RVA: 0x00137E54 File Offset: 0x00136054
		// (set) Token: 0x060063FF RID: 25599 RVA: 0x00137E5C File Offset: 0x0013605C
		public string Sender { get; set; }
	}
}
