using System;
using System.Collections.Generic;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Search
{
	// Token: 0x0200018F RID: 399
	internal class ErrorStatistics
	{
		// Token: 0x06000FB6 RID: 4022 RVA: 0x0005D0C7 File Offset: 0x0005B2C7
		public ErrorStatistics(long itemCount, IDictionary<EvaluationErrors, long> failedDocuments)
		{
			this.DocumentCount = itemCount;
			this.FailedDocuments = failedDocuments;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0005D0DD File Offset: 0x0005B2DD
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x0005D0E5 File Offset: 0x0005B2E5
		public long DocumentCount { get; private set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0005D0EE File Offset: 0x0005B2EE
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x0005D0F6 File Offset: 0x0005B2F6
		public IDictionary<EvaluationErrors, long> FailedDocuments { get; private set; }
	}
}
