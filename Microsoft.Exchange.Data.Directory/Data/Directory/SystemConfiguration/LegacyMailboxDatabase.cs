using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200049C RID: 1180
	[Serializable]
	public sealed class LegacyMailboxDatabase : LegacyDatabase
	{
		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x000D3C7F File Offset: 0x000D1E7F
		internal override ADObjectSchema Schema
		{
			get
			{
				return LegacyMailboxDatabase.schema;
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x060035D1 RID: 13777 RVA: 0x000D3C86 File Offset: 0x000D1E86
		internal override string MostDerivedObjectClass
		{
			get
			{
				return LegacyMailboxDatabase.MostDerivedClass;
			}
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x000D3C8D File Offset: 0x000D1E8D
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000D3CA0 File Offset: 0x000D1EA0
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			errors.AddRange(LegacyDatabase.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
			{
				LegacyDatabaseSchema.IssueWarningQuota,
				LegacyMailboxDatabaseSchema.ProhibitSendQuota,
				LegacyMailboxDatabaseSchema.ProhibitSendReceiveQuota
			}, this.Identity));
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000D3CEC File Offset: 0x000D1EEC
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(LegacyMailboxDatabaseSchema.ProhibitSendReceiveQuota))
			{
				this.ProhibitSendReceiveQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromMB(2355UL));
			}
			if (!base.IsModified(LegacyMailboxDatabaseSchema.ProhibitSendQuota))
			{
				this.ProhibitSendQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromGB(2UL));
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x060035D5 RID: 13781 RVA: 0x000D3D41 File Offset: 0x000D1F41
		// (set) Token: 0x060035D6 RID: 13782 RVA: 0x000D3D53 File Offset: 0x000D1F53
		public ADObjectId JournalRecipient
		{
			get
			{
				return (ADObjectId)this[LegacyMailboxDatabaseSchema.JournalRecipient];
			}
			set
			{
				this[LegacyMailboxDatabaseSchema.JournalRecipient] = value;
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x060035D7 RID: 13783 RVA: 0x000D3D61 File Offset: 0x000D1F61
		// (set) Token: 0x060035D8 RID: 13784 RVA: 0x000D3D73 File Offset: 0x000D1F73
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MailboxRetention
		{
			get
			{
				return (EnhancedTimeSpan)this[LegacyMailboxDatabaseSchema.MailboxRetention];
			}
			set
			{
				this[LegacyMailboxDatabaseSchema.MailboxRetention] = value;
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x060035D9 RID: 13785 RVA: 0x000D3D86 File Offset: 0x000D1F86
		// (set) Token: 0x060035DA RID: 13786 RVA: 0x000D3D98 File Offset: 0x000D1F98
		public ADObjectId OfflineAddressBook
		{
			get
			{
				return (ADObjectId)this[LegacyMailboxDatabaseSchema.OfflineAddressBook];
			}
			set
			{
				this[LegacyMailboxDatabaseSchema.OfflineAddressBook] = value;
			}
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x060035DB RID: 13787 RVA: 0x000D3DA6 File Offset: 0x000D1FA6
		// (set) Token: 0x060035DC RID: 13788 RVA: 0x000D3DB8 File Offset: 0x000D1FB8
		public ADObjectId OriginalDatabase
		{
			get
			{
				return (ADObjectId)this[LegacyMailboxDatabaseSchema.OriginalDatabase];
			}
			internal set
			{
				this[LegacyMailboxDatabaseSchema.OriginalDatabase] = value;
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x060035DD RID: 13789 RVA: 0x000D3DC6 File Offset: 0x000D1FC6
		// (set) Token: 0x060035DE RID: 13790 RVA: 0x000D3DD8 File Offset: 0x000D1FD8
		public ADObjectId PublicFolderDatabase
		{
			get
			{
				return (ADObjectId)this[LegacyMailboxDatabaseSchema.PublicFolderDatabase];
			}
			set
			{
				this[LegacyMailboxDatabaseSchema.PublicFolderDatabase] = value;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x000D3DE6 File Offset: 0x000D1FE6
		// (set) Token: 0x060035E0 RID: 13792 RVA: 0x000D3DF8 File Offset: 0x000D1FF8
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[LegacyMailboxDatabaseSchema.ProhibitSendReceiveQuota];
			}
			set
			{
				this[LegacyMailboxDatabaseSchema.ProhibitSendReceiveQuota] = value;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x000D3E0B File Offset: 0x000D200B
		// (set) Token: 0x060035E2 RID: 13794 RVA: 0x000D3E1D File Offset: 0x000D201D
		public bool Recovery
		{
			get
			{
				return (bool)this[LegacyMailboxDatabaseSchema.Recovery];
			}
			internal set
			{
				this[LegacyMailboxDatabaseSchema.Recovery] = value;
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x060035E3 RID: 13795 RVA: 0x000D3E30 File Offset: 0x000D2030
		// (set) Token: 0x060035E4 RID: 13796 RVA: 0x000D3E42 File Offset: 0x000D2042
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ProhibitSendQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[LegacyMailboxDatabaseSchema.ProhibitSendQuota];
			}
			set
			{
				this[LegacyMailboxDatabaseSchema.ProhibitSendQuota] = value;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x000D3E55 File Offset: 0x000D2055
		// (set) Token: 0x060035E6 RID: 13798 RVA: 0x000D3E67 File Offset: 0x000D2067
		[Parameter(Mandatory = false)]
		public bool IndexEnabled
		{
			get
			{
				return (bool)this[LegacyMailboxDatabaseSchema.IndexEnabled];
			}
			set
			{
				this[LegacyMailboxDatabaseSchema.IndexEnabled] = value;
			}
		}

		// Token: 0x04002459 RID: 9305
		private static LegacyMailboxDatabaseSchema schema = ObjectSchema.GetInstance<LegacyMailboxDatabaseSchema>();

		// Token: 0x0400245A RID: 9306
		internal static readonly string MostDerivedClass = "msExchPrivateMDB";
	}
}
