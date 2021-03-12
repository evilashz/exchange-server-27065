using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000023 RID: 35
	public class DatabaseSnapshot
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00009964 File Offset: 0x00007B64
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000996C File Offset: 0x00007B6C
		public Guid MdbGuid { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00009975 File Offset: 0x00007B75
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000997D File Offset: 0x00007B7D
		public string DatabaseName { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00009986 File Offset: 0x00007B86
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000998E File Offset: 0x00007B8E
		public int TotalMailboxCount { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00009997 File Offset: 0x00007B97
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000999F File Offset: 0x00007B9F
		public int SubscribedMailboxCount { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000099A8 File Offset: 0x00007BA8
		// (set) Token: 0x06000181 RID: 385 RVA: 0x000099B0 File Offset: 0x00007BB0
		[XmlArrayItem("Mailbox")]
		[XmlArray("Mailboxes")]
		public List<MailboxSnapshot> Mailboxes { get; set; }
	}
}
