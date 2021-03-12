using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001D0 RID: 464
	internal class SendNotifyMessageCompletedEventArgs : EventArgs
	{
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0003C2F6 File Offset: 0x0003A4F6
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x0003C2FE File Offset: 0x0003A4FE
		public Exception Error { get; set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0003C307 File Offset: 0x0003A507
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x0003C30F File Offset: 0x0003A50F
		public int ResponseCode { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0003C318 File Offset: 0x0003A518
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x0003C320 File Offset: 0x0003A520
		public string ResponseReason { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0003C329 File Offset: 0x0003A529
		// (set) Token: 0x06000D8C RID: 3468 RVA: 0x0003C331 File Offset: 0x0003A531
		public object UserState { get; set; }
	}
}
