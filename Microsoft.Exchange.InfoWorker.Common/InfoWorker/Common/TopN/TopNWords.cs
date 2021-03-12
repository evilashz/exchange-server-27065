using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.TopN
{
	// Token: 0x0200027C RID: 636
	internal class TopNWords
	{
		// Token: 0x0600123F RID: 4671 RVA: 0x000550E9 File Offset: 0x000532E9
		internal TopNWords(MailboxSession mailboxSession)
		{
			this.mailboxSession = mailboxSession;
			this.config = new TopNConfiguration(mailboxSession);
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00055104 File Offset: 0x00053304
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x0005510C File Offset: 0x0005330C
		internal int Version
		{
			get
			{
				if (this.version == 0)
				{
					this.GetMetaData();
				}
				return this.version;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00055122 File Offset: 0x00053322
		internal ExDateTime LastScanTime
		{
			get
			{
				if (this.lastScanTime == null)
				{
					this.GetMetaData();
				}
				return this.lastScanTime.Value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x00055142 File Offset: 0x00053342
		internal List<KeyValuePair<string, int>> WordList
		{
			get
			{
				if (this.wordList == null)
				{
					this.wordList = this.GetWordList();
				}
				return this.wordList;
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0005515E File Offset: 0x0005335E
		private void GetMetaData()
		{
			this.config.ReadMetaData();
			this.version = this.config.Version;
			this.lastScanTime = new ExDateTime?(this.config.LastScanTime);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00055194 File Offset: 0x00053394
		private List<KeyValuePair<string, int>> GetWordList()
		{
			List<KeyValuePair<string, int>> list = null;
			if (this.config.ReadWordFrequencyMap())
			{
				if (this.config.WordFrequency == null || this.config.WordFrequency.Length == 0)
				{
					return null;
				}
				list = new List<KeyValuePair<string, int>>(10);
				foreach (KeyValuePair<string, int> keyValuePair in this.config.WordFrequency)
				{
					list.Add(new KeyValuePair<string, int>(keyValuePair.Key, keyValuePair.Value));
				}
			}
			if (list == null || this.config.LastScanTime + TopNConfiguration.UpdateInterval < ExDateTime.Now)
			{
				this.config.ScanRequested = true;
				this.config.Save(true);
			}
			return list;
		}

		// Token: 0x04000BF8 RID: 3064
		private MailboxSession mailboxSession;

		// Token: 0x04000BF9 RID: 3065
		private TopNConfiguration config;

		// Token: 0x04000BFA RID: 3066
		private int version;

		// Token: 0x04000BFB RID: 3067
		private ExDateTime? lastScanTime;

		// Token: 0x04000BFC RID: 3068
		private List<KeyValuePair<string, int>> wordList;
	}
}
