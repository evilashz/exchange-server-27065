using System;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x0200002F RID: 47
	internal interface IExecutor
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000219 RID: 537
		ISearchPolicy Policy { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600021A RID: 538
		ExecutorContext Context { get; }

		// Token: 0x0600021B RID: 539
		void EnqueueNext(object item);

		// Token: 0x0600021C RID: 540
		void Fail(Exception ex);
	}
}
