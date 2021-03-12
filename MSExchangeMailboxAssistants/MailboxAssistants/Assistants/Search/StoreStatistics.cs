using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Search
{
	// Token: 0x02000195 RID: 405
	internal class StoreStatistics
	{
		// Token: 0x06000FE5 RID: 4069 RVA: 0x0005D803 File Offset: 0x0005BA03
		public StoreStatistics(long itemCount, long deletedDocumentCount)
		{
			this.DocumentCount = itemCount;
			this.DeletedDocumentCount = deletedDocumentCount;
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0005D819 File Offset: 0x0005BA19
		// (set) Token: 0x06000FE7 RID: 4071 RVA: 0x0005D821 File Offset: 0x0005BA21
		public long DocumentCount { get; private set; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0005D82A File Offset: 0x0005BA2A
		// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x0005D832 File Offset: 0x0005BA32
		public long DeletedDocumentCount { get; private set; }
	}
}
