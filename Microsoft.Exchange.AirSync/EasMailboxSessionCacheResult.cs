using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200007E RID: 126
	public class EasMailboxSessionCacheResult
	{
		// Token: 0x060006B2 RID: 1714 RVA: 0x00025CCA File Offset: 0x00023ECA
		public EasMailboxSessionCacheResult()
		{
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00025CD2 File Offset: 0x00023ED2
		internal EasMailboxSessionCacheResult(int count)
		{
			this.Entries = null;
			this.Count = count;
			this.Initialize();
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00025CF0 File Offset: 0x00023EF0
		internal EasMailboxSessionCacheResult(List<MruCacheDiagnosticEntryInfo> entries)
		{
			if (entries != null)
			{
				this.Entries = new EasMailboxSessionCacheResultItem[entries.Count];
				for (int i = 0; i < entries.Count; i++)
				{
					this.Entries[i] = new EasMailboxSessionCacheResultItem(entries[i].Identifier, entries[i].TimeToLive);
				}
				this.Count = entries.Count;
			}
			else
			{
				this.Entries = null;
				this.Count = 0;
			}
			this.Initialize();
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00025D70 File Offset: 0x00023F70
		private void Initialize()
		{
			this.MaxCacheSize = GlobalSettings.MailboxSessionCacheMaxSize;
			this.CacheTimeout = GlobalSettings.MailboxSessionCacheTimeout.ToString();
			this.CacheEfficiency = MailboxSessionCache.GetCacheEfficiency().ToString("P2");
			this.DiscardedSessions = MailboxSessionCache.DiscardedSessions;
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00025DC4 File Offset: 0x00023FC4
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00025DCC File Offset: 0x00023FCC
		public int DiscardedSessions { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00025DD5 File Offset: 0x00023FD5
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00025DDD File Offset: 0x00023FDD
		public int MaxCacheSize { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00025DE6 File Offset: 0x00023FE6
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00025DEE File Offset: 0x00023FEE
		public string CacheTimeout { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00025DF7 File Offset: 0x00023FF7
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00025DFF File Offset: 0x00023FFF
		public string CacheEfficiency { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00025E08 File Offset: 0x00024008
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00025E10 File Offset: 0x00024010
		public int Count { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00025E19 File Offset: 0x00024019
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00025E21 File Offset: 0x00024021
		[XmlArray("CacheEntries")]
		[XmlArrayItem("CacheEntry")]
		public EasMailboxSessionCacheResultItem[] Entries { get; set; }
	}
}
