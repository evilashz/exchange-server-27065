using System;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000076 RID: 118
	internal sealed class MailboxCrawlerState : MailboxState
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x00009DF7 File Offset: 0x00007FF7
		public MailboxCrawlerState(int mailboxNumber, int lastDocumentIdIndexed, int attemptCount = 0) : base(mailboxNumber, lastDocumentIdIndexed)
		{
			if (lastDocumentIdIndexed == -2)
			{
				this.RecrawlMailbox = true;
			}
			this.AttemptCount = attemptCount;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00009E14 File Offset: 0x00008014
		private MailboxCrawlerState()
		{
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00009E1C File Offset: 0x0000801C
		public static int MaxCrawlAttemptCount
		{
			get
			{
				return MailboxCrawlerState.maxCrawlAttemptCount;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00009E23 File Offset: 0x00008023
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00009E2B File Offset: 0x0000802B
		public int LastDocumentIdIndexed
		{
			get
			{
				return base.RawState;
			}
			set
			{
				base.RawState = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00009E34 File Offset: 0x00008034
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00009E3C File Offset: 0x0000803C
		public bool RecrawlMailbox { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00009E45 File Offset: 0x00008045
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00009E4D File Offset: 0x0000804D
		public int AttemptCount { get; set; }

		// Token: 0x04000145 RID: 325
		private static int maxCrawlAttemptCount = SearchConfig.Instance.MaxCrawlAttemptCount;
	}
}
