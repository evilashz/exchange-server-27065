using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000C9 RID: 201
	internal class CalendarSyncMailboxInterestingLogEntry
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0003B061 File Offset: 0x00039261
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x0003B069 File Offset: 0x00039269
		public Guid MailboxGuid { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0003B072 File Offset: 0x00039272
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x0003B07A File Offset: 0x0003927A
		public bool IsMailboxInteresting { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0003B083 File Offset: 0x00039283
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x0003B08B File Offset: 0x0003928B
		public int ConsumerCalendarsCount { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0003B094 File Offset: 0x00039294
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x0003B09C File Offset: 0x0003929C
		public int ExternalCalendarsCount { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0003B0A5 File Offset: 0x000392A5
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x0003B0AD File Offset: 0x000392AD
		public int InternetCalendarsCount { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0003B0B6 File Offset: 0x000392B6
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0003B0BE File Offset: 0x000392BE
		public int ExternalContactsCount { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0003B0C7 File Offset: 0x000392C7
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0003B0CF File Offset: 0x000392CF
		public object MailboxType { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0003B0D8 File Offset: 0x000392D8
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0003B0E0 File Offset: 0x000392E0
		public object MailboxFlags { get; set; }

		// Token: 0x06000883 RID: 2179 RVA: 0x0003B0EC File Offset: 0x000392EC
		public List<KeyValuePair<string, object>> FormatCustomData()
		{
			return new List<KeyValuePair<string, object>>(5)
			{
				new KeyValuePair<string, object>("MailboxGuid", string.Format("{0}", this.MailboxGuid)),
				new KeyValuePair<string, object>("IsMailboxInteresting", string.Format("{0}", this.IsMailboxInteresting)),
				new KeyValuePair<string, object>("ConsumerCalendarsCount", string.Format("{0}", this.ConsumerCalendarsCount)),
				new KeyValuePair<string, object>("ExternalCalendarsCount", string.Format("{0}", this.ExternalCalendarsCount)),
				new KeyValuePair<string, object>("InternetCalendarsCount", string.Format("{0}", this.InternetCalendarsCount)),
				new KeyValuePair<string, object>("ExternalContactsCount", string.Format("{0}", this.ExternalContactsCount)),
				new KeyValuePair<string, object>("MailboxType", string.Format("{0}", this.MailboxType)),
				new KeyValuePair<string, object>("MailboxFlags", string.Format("{0}", this.MailboxFlags))
			};
		}
	}
}
