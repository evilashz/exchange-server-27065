using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200072E RID: 1838
	[Serializable]
	public class MailboxAuditLogRecord : ConfigurableObject
	{
		// Token: 0x060057E2 RID: 22498 RVA: 0x00139E69 File Offset: 0x00138069
		public MailboxAuditLogRecord() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x17001DC5 RID: 7621
		// (get) Token: 0x060057E3 RID: 22499 RVA: 0x00139E76 File Offset: 0x00138076
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x00139E80 File Offset: 0x00138080
		public MailboxAuditLogRecord(MailboxAuditLogRecordId identity, string mailboxResolvedName, string guid, DateTime? lastAccessed) : this()
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (string.IsNullOrEmpty(mailboxResolvedName))
			{
				throw new ArgumentNullException("mailbox resolved name");
			}
			this.propertyBag[SimpleProviderObjectSchema.Identity] = identity;
			this.MailboxResolvedOwnerName = ((mailboxResolvedName == null) ? null : mailboxResolvedName);
			this.MailboxGuid = guid;
			this.LastAccessed = lastAccessed;
		}

		// Token: 0x17001DC6 RID: 7622
		// (get) Token: 0x060057E5 RID: 22501 RVA: 0x00139EE1 File Offset: 0x001380E1
		// (set) Token: 0x060057E6 RID: 22502 RVA: 0x00139EF8 File Offset: 0x001380F8
		public string MailboxGuid
		{
			get
			{
				return this.propertyBag[MailboxAuditLogRecordSchema.MailboxGuid] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogRecordSchema.MailboxGuid] = value;
			}
		}

		// Token: 0x17001DC7 RID: 7623
		// (get) Token: 0x060057E7 RID: 22503 RVA: 0x00139F0B File Offset: 0x0013810B
		// (set) Token: 0x060057E8 RID: 22504 RVA: 0x00139F22 File Offset: 0x00138122
		public string MailboxResolvedOwnerName
		{
			get
			{
				return this.propertyBag[MailboxAuditLogRecordSchema.MailboxResolvedOwnerName] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogRecordSchema.MailboxResolvedOwnerName] = value;
			}
		}

		// Token: 0x17001DC8 RID: 7624
		// (get) Token: 0x060057E9 RID: 22505 RVA: 0x00139F35 File Offset: 0x00138135
		// (set) Token: 0x060057EA RID: 22506 RVA: 0x00139F4C File Offset: 0x0013814C
		public DateTime? LastAccessed
		{
			get
			{
				return (DateTime?)this.propertyBag[MailboxAuditLogRecordSchema.LastAccessed];
			}
			private set
			{
				this.propertyBag[MailboxAuditLogRecordSchema.LastAccessed] = value;
			}
		}

		// Token: 0x17001DC9 RID: 7625
		// (get) Token: 0x060057EB RID: 22507 RVA: 0x00139F64 File Offset: 0x00138164
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxAuditLogRecord.schema;
			}
		}

		// Token: 0x04003B64 RID: 15204
		private static readonly ObjectSchema schema = ObjectSchema.GetInstance<MailboxAuditLogRecordSchema>();
	}
}
