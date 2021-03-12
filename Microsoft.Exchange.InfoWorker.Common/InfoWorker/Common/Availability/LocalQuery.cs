using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000A7 RID: 167
	internal abstract class LocalQuery
	{
		// Token: 0x060003AB RID: 939 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
		internal LocalQuery(ClientContext clientContext, DateTime deadline)
		{
			this.clientContext = clientContext;
			this.deadline = deadline;
		}

		// Token: 0x060003AC RID: 940
		internal abstract BaseQueryResult GetData(BaseQuery query);

		// Token: 0x0400022C RID: 556
		protected ClientContext clientContext;

		// Token: 0x0400022D RID: 557
		protected DateTime deadline;
	}
}
