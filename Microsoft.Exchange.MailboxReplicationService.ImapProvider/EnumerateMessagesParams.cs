using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000002 RID: 2
	internal class EnumerateMessagesParams
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public EnumerateMessagesParams(int highFetchValue, int lowFetchValue, FetchMessagesFlags inputFetchFlags = FetchMessagesFlags.None)
		{
			this.FetchMessagesFlags = (FetchMessagesFlags.FetchBySeqNum | inputFetchFlags);
			this.HighFetchValue = highFetchValue;
			this.LowFetchValue = lowFetchValue;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020EF File Offset: 0x000002EF
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020F7 File Offset: 0x000002F7
		public FetchMessagesFlags FetchMessagesFlags { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002100 File Offset: 0x00000300
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002108 File Offset: 0x00000308
		public int HighFetchValue { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002111 File Offset: 0x00000311
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002119 File Offset: 0x00000319
		public int LowFetchValue { get; private set; }

		// Token: 0x04000001 RID: 1
		private const FetchMessagesFlags FetchFlags = FetchMessagesFlags.FetchBySeqNum;
	}
}
