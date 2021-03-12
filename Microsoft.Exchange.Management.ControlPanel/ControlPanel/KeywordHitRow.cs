using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003D8 RID: 984
	[DataContract]
	public class KeywordHitRow
	{
		// Token: 0x17001FC0 RID: 8128
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x0009CF2D File Offset: 0x0009B12D
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x0009CF35 File Offset: 0x0009B135
		private KeywordHit KeywordHit { get; set; }

		// Token: 0x06003287 RID: 12935 RVA: 0x0009CF3E File Offset: 0x0009B13E
		public KeywordHitRow(KeywordHit kwh)
		{
			this.KeywordHit = kwh;
		}

		// Token: 0x17001FC1 RID: 8129
		// (get) Token: 0x06003288 RID: 12936 RVA: 0x0009CF4D File Offset: 0x0009B14D
		public string Phrase
		{
			get
			{
				return this.KeywordHit.Phrase;
			}
		}

		// Token: 0x17001FC2 RID: 8130
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x0009CF5A File Offset: 0x0009B15A
		public int Count
		{
			get
			{
				return this.KeywordHit.Count;
			}
		}

		// Token: 0x17001FC3 RID: 8131
		// (get) Token: 0x0600328A RID: 12938 RVA: 0x0009CF67 File Offset: 0x0009B167
		public int MailboxCount
		{
			get
			{
				return this.KeywordHit.MailboxCount;
			}
		}
	}
}
