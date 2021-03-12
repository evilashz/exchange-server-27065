using System;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001B7 RID: 439
	internal interface ISearchMailboxTask
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000BB9 RID: 3001
		MailboxInfo Mailbox { get; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000BBA RID: 3002
		SearchType Type { get; }

		// Token: 0x06000BBB RID: 3003
		void Execute(SearchCompletedCallback callback);

		// Token: 0x06000BBC RID: 3004
		bool ShouldRetry(ISearchTaskResult result);

		// Token: 0x06000BBD RID: 3005
		ISearchTaskResult GetErrorResult(Exception ex);

		// Token: 0x06000BBE RID: 3006
		void Abort();
	}
}
