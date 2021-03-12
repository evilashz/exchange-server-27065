using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001CE RID: 462
	internal struct TraceLogData
	{
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x0002B41C File Offset: 0x0002961C
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x0002B424 File Offset: 0x00029624
		public string TracerName { get; set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x0002B42D File Offset: 0x0002962D
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x0002B435 File Offset: 0x00029635
		public TraceType TraceType { get; set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x0002B43E File Offset: 0x0002963E
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x0002B446 File Offset: 0x00029646
		public string TraceMessage { get; set; }
	}
}
