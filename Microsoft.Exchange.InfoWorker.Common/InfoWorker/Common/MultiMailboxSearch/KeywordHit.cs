using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001EE RID: 494
	internal class KeywordHit : IKeywordHit
	{
		// Token: 0x06000CE7 RID: 3303 RVA: 0x00036BB8 File Offset: 0x00034DB8
		public KeywordHit(string phrase, ulong count, ByteQuantifiedSize size)
		{
			Util.ThrowOnNull(phrase, "phrase");
			if (string.IsNullOrEmpty(phrase))
			{
				throw new ArgumentException("Phrase cannot be empty");
			}
			if (count == 0UL && size != ByteQuantifiedSize.Zero)
			{
				throw new ArgumentException("count is zero but size is not zero");
			}
			this.phrase = phrase;
			this.count = count;
			this.size = size;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00036C28 File Offset: 0x00034E28
		public KeywordHit(string phrase, MailboxInfo mailbox, Exception exception)
		{
			Util.ThrowOnNull(phrase, "phrase");
			Util.ThrowOnNull(mailbox, "mailbox");
			Util.ThrowOnNull(exception, "exception");
			if (string.IsNullOrEmpty(phrase))
			{
				throw new ArgumentException("Phrase cannot be empty");
			}
			this.phrase = phrase;
			this.errors.Add(new Pair<MailboxInfo, Exception>(mailbox, exception));
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00036C94 File Offset: 0x00034E94
		public string Phrase
		{
			get
			{
				return this.phrase;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00036C9C File Offset: 0x00034E9C
		public ulong Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00036CA4 File Offset: 0x00034EA4
		public ByteQuantifiedSize Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00036CAC File Offset: 0x00034EAC
		public IList<Pair<MailboxInfo, Exception>> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00036CB4 File Offset: 0x00034EB4
		public void Merge(IKeywordHit hits)
		{
			if (hits == null)
			{
				return;
			}
			if (string.Compare(this.phrase, hits.Phrase, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException("Keyword hits: Invalid merge");
			}
			checked
			{
				if (hits.Count != 0UL)
				{
					this.count += hits.Count;
					this.size += hits.Size;
				}
				this.errors.AddRange(((KeywordHit)hits).Errors);
			}
		}

		// Token: 0x04000923 RID: 2339
		public const string UnsearchablePhrase = "652beee2-75f7-4ca0-8a02-0698a3919cb9";

		// Token: 0x04000924 RID: 2340
		private string phrase;

		// Token: 0x04000925 RID: 2341
		private ulong count;

		// Token: 0x04000926 RID: 2342
		private ByteQuantifiedSize size;

		// Token: 0x04000927 RID: 2343
		private List<Pair<MailboxInfo, Exception>> errors = new List<Pair<MailboxInfo, Exception>>(1);
	}
}
