using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x02000212 RID: 530
	internal class SearchMailboxResult : ISearchTaskResult, ISearchResult
	{
		// Token: 0x06000E61 RID: 3681 RVA: 0x0003EE78 File Offset: 0x0003D078
		public SearchMailboxResult(MailboxInfo mailbox, SortedResultPage result, ulong totalResultCount)
		{
			Util.ThrowOnNull(mailbox, "mailbox");
			Util.ThrowOnNull(result, "result");
			int resultCount = result.ResultCount;
			this.resultType = SearchType.Preview;
			this.mailbox = mailbox;
			this.result = result;
			this.totalResultCount = totalResultCount;
			this.mailboxStats = new List<MailboxStatistics>
			{
				new MailboxStatistics(mailbox, this.totalResultCount, this.TotalResultSize)
			};
			this.success = true;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0003EEF4 File Offset: 0x0003D0F4
		public SearchMailboxResult(MailboxInfo mailbox, Exception error)
		{
			Util.ThrowOnNull(mailbox, "mailbox");
			Util.ThrowOnNull(error, "error");
			this.mailbox = mailbox;
			this.success = false;
			this.resultType = SearchType.Preview;
			this.totalResultCount = 0UL;
			this.exception = error;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003EF44 File Offset: 0x0003D144
		public SearchMailboxResult(MailboxInfo mailbox, IKeywordHit keyword)
		{
			Util.ThrowOnNull(mailbox, "mailbox");
			Util.ThrowOnNull(keyword, "keyword");
			this.mailbox = mailbox;
			this.resultType = SearchType.Statistics;
			this.keywordHit = keyword;
			this.success = true;
			if (keyword.Errors.Count != 0)
			{
				this.success = false;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0003EF9D File Offset: 0x0003D19D
		public MailboxInfo Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0003EFA5 File Offset: 0x0003D1A5
		public SearchType ResultType
		{
			get
			{
				return this.resultType;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0003EFAD File Offset: 0x0003D1AD
		public SortedResultPage PreviewResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0003EFB5 File Offset: 0x0003D1B5
		public IProtocolLog ProtocolLog
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0003EFB8 File Offset: 0x0003D1B8
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0003EFC0 File Offset: 0x0003D1C0
		public List<Pair<MailboxInfo, Exception>> PreviewErrors
		{
			get
			{
				List<Pair<MailboxInfo, Exception>> list = null;
				if (this.exception != null)
				{
					list = new List<Pair<MailboxInfo, Exception>>(1);
					list.Add(new Pair<MailboxInfo, Exception>(this.mailbox, this.exception));
				}
				return list;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0003EFF6 File Offset: 0x0003D1F6
		public bool Success
		{
			get
			{
				return this.success;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0003EFFE File Offset: 0x0003D1FE
		public IKeywordHit KeywordHit
		{
			get
			{
				return this.keywordHit;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0003F006 File Offset: 0x0003D206
		public ByteQuantifiedSize TotalResultSize
		{
			get
			{
				return ByteQuantifiedSize.Zero;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0003F00D File Offset: 0x0003D20D
		public List<MailboxStatistics> MailboxStats
		{
			get
			{
				return this.mailboxStats;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0003F018 File Offset: 0x0003D218
		public IDictionary<string, IKeywordHit> KeywordStatistics
		{
			get
			{
				Dictionary<string, IKeywordHit> dictionary = null;
				if (this.keywordHit != null)
				{
					dictionary = new Dictionary<string, IKeywordHit>(1);
					dictionary.Add(this.keywordHit.Phrase, this.keywordHit);
				}
				return dictionary;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0003F04E File Offset: 0x0003D24E
		public ulong TotalResultCount
		{
			get
			{
				return this.totalResultCount;
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0003F056 File Offset: 0x0003D256
		public void MergeSearchResult(ISearchResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0003F05D File Offset: 0x0003D25D
		public Dictionary<string, List<IRefinerResult>> RefinersResult
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040009E4 RID: 2532
		private readonly MailboxInfo mailbox;

		// Token: 0x040009E5 RID: 2533
		private readonly SortedResultPage result;

		// Token: 0x040009E6 RID: 2534
		private readonly Exception exception;

		// Token: 0x040009E7 RID: 2535
		private readonly bool success;

		// Token: 0x040009E8 RID: 2536
		private readonly SearchType resultType;

		// Token: 0x040009E9 RID: 2537
		private readonly IKeywordHit keywordHit;

		// Token: 0x040009EA RID: 2538
		private readonly ulong totalResultCount;

		// Token: 0x040009EB RID: 2539
		private List<MailboxStatistics> mailboxStats;
	}
}
