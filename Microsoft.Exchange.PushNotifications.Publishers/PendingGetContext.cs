using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000B2 RID: 178
	internal class PendingGetContext
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x000139E9 File Offset: 0x00011BE9
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x000139F1 File Offset: 0x00011BF1
		internal AsyncResult AsyncResult { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x000139FA File Offset: 0x00011BFA
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x00013A02 File Offset: 0x00011C02
		internal IPendingGetResponse Response { get; set; }
	}
}
