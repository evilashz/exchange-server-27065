using System;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x0200003D RID: 61
	internal class SearchTaskContext
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000144B4 File Offset: 0x000126B4
		// (set) Token: 0x060002EA RID: 746 RVA: 0x000144BC File Offset: 0x000126BC
		public IExecutor Executor { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002EB RID: 747 RVA: 0x000144C5 File Offset: 0x000126C5
		// (set) Token: 0x060002EC RID: 748 RVA: 0x000144CD File Offset: 0x000126CD
		public object Item { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000144D6 File Offset: 0x000126D6
		// (set) Token: 0x060002EE RID: 750 RVA: 0x000144DE File Offset: 0x000126DE
		public object TaskContext { get; set; }
	}
}
