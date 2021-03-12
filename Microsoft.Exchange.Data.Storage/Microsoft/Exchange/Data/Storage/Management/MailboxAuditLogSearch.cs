using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009BE RID: 2494
	[Serializable]
	public class MailboxAuditLogSearch : AuditLogSearchBase
	{
		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x06005C15 RID: 23573 RVA: 0x00180105 File Offset: 0x0017E305
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxAuditLogSearch.schema;
			}
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x06005C16 RID: 23574 RVA: 0x0018010C File Offset: 0x0017E30C
		internal override SearchFilter ItemClassFilter
		{
			get
			{
				return new SearchFilter.ContainsSubstring(ItemSchema.ItemClass, "IPM.AuditLogSearch.Mailbox", 1, 0);
			}
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x06005C18 RID: 23576 RVA: 0x00180127 File Offset: 0x0017E327
		// (set) Token: 0x06005C19 RID: 23577 RVA: 0x00180139 File Offset: 0x0017E339
		public MultiValuedProperty<ADObjectId> Mailboxes
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailboxAuditLogSearchEwsSchema.MailboxIds];
			}
			set
			{
				this[MailboxAuditLogSearchEwsSchema.MailboxIds] = value;
			}
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x06005C1A RID: 23578 RVA: 0x00180147 File Offset: 0x0017E347
		// (set) Token: 0x06005C1B RID: 23579 RVA: 0x00180159 File Offset: 0x0017E359
		public MultiValuedProperty<string> LogonTypes
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxAuditLogSearchEwsSchema.LogonTypeStrings];
			}
			set
			{
				this[MailboxAuditLogSearchEwsSchema.LogonTypeStrings] = value;
			}
		}

		// Token: 0x17001943 RID: 6467
		// (get) Token: 0x06005C1C RID: 23580 RVA: 0x00180167 File Offset: 0x0017E367
		// (set) Token: 0x06005C1D RID: 23581 RVA: 0x00180179 File Offset: 0x0017E379
		public MultiValuedProperty<string> Operations
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxAuditLogSearchEwsSchema.Operations];
			}
			set
			{
				this[MailboxAuditLogSearchEwsSchema.Operations] = value;
			}
		}

		// Token: 0x17001944 RID: 6468
		// (get) Token: 0x06005C1E RID: 23582 RVA: 0x00180187 File Offset: 0x0017E387
		// (set) Token: 0x06005C1F RID: 23583 RVA: 0x00180199 File Offset: 0x0017E399
		public bool? ShowDetails
		{
			get
			{
				return (bool?)this[MailboxAuditLogSearchEwsSchema.ShowDetails];
			}
			set
			{
				this[MailboxAuditLogSearchEwsSchema.ShowDetails] = value;
			}
		}

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x06005C20 RID: 23584 RVA: 0x001801AC File Offset: 0x0017E3AC
		internal override string ItemClass
		{
			get
			{
				return "IPM.AuditLogSearch.Mailbox";
			}
		}

		// Token: 0x040032C2 RID: 12994
		private const string ItemClassPrefix = "IPM.AuditLogSearch.Mailbox";

		// Token: 0x040032C3 RID: 12995
		private static ObjectSchema schema = new MailboxAuditLogSearchEwsSchema();
	}
}
