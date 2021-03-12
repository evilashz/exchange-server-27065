using System;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	public sealed class ADSystemMailbox : ADRecipient, IADMailStorage
	{
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0007378A File Offset: 0x0007198A
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADSystemMailbox.schema;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x00073791 File Offset: 0x00071991
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADSystemMailbox.MostDerivedClass;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x00073798 File Offset: 0x00071998
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x000737AB File Offset: 0x000719AB
		internal ADSystemMailbox(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000737B5 File Offset: 0x000719B5
		internal ADSystemMailbox(IRecipientSession session, string commonName, ADObjectId containerId)
		{
			this.m_Session = session;
			base.SetId(containerId.GetChildId("CN", commonName));
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x000737E2 File Offset: 0x000719E2
		public ADSystemMailbox()
		{
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x000737EA File Offset: 0x000719EA
		// (set) Token: 0x06001B7E RID: 7038 RVA: 0x000737FC File Offset: 0x000719FC
		public DeliveryMechanisms DeliveryMechanism
		{
			get
			{
				return (DeliveryMechanisms)this[ADSystemMailboxSchema.DeliveryMechanism];
			}
			internal set
			{
				this[ADSystemMailboxSchema.DeliveryMechanism] = value;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x0007380F File Offset: 0x00071A0F
		// (set) Token: 0x06001B80 RID: 7040 RVA: 0x00073821 File Offset: 0x00071A21
		public ADObjectId Database
		{
			get
			{
				return (ADObjectId)this[ADSystemMailboxSchema.Database];
			}
			set
			{
				this[ADSystemMailboxSchema.Database] = value;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x0007382F File Offset: 0x00071A2F
		// (set) Token: 0x06001B82 RID: 7042 RVA: 0x00073841 File Offset: 0x00071A41
		public DeletedItemRetention DeletedItemFlags
		{
			get
			{
				return (DeletedItemRetention)this[ADSystemMailboxSchema.DeletedItemFlags];
			}
			set
			{
				this[ADSystemMailboxSchema.DeletedItemFlags] = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x00073854 File Offset: 0x00071A54
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x00073866 File Offset: 0x00071A66
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return (bool)this[ADSystemMailboxSchema.DeliverToMailboxAndForward];
			}
			set
			{
				this[ADSystemMailboxSchema.DeliverToMailboxAndForward] = value;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x00073879 File Offset: 0x00071A79
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x0007388B File Offset: 0x00071A8B
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[ADSystemMailboxSchema.ExchangeGuid];
			}
			set
			{
				this[ADSystemMailboxSchema.ExchangeGuid] = value;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x0007389E File Offset: 0x00071A9E
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x000738B0 File Offset: 0x00071AB0
		public RawSecurityDescriptor ExchangeSecurityDescriptor
		{
			get
			{
				return (RawSecurityDescriptor)this[ADSystemMailboxSchema.ExchangeSecurityDescriptor];
			}
			set
			{
				this[ADSystemMailboxSchema.ExchangeSecurityDescriptor] = value;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x000738BE File Offset: 0x00071ABE
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x000738D0 File Offset: 0x00071AD0
		public ExternalOofOptions ExternalOofOptions
		{
			get
			{
				return (ExternalOofOptions)this[ADSystemMailboxSchema.ExternalOofOptions];
			}
			set
			{
				this[ADSystemMailboxSchema.ExternalOofOptions] = value;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x000738E3 File Offset: 0x00071AE3
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x000738F5 File Offset: 0x00071AF5
		public EnhancedTimeSpan RetainDeletedItemsFor
		{
			get
			{
				return (EnhancedTimeSpan)this[ADSystemMailboxSchema.RetainDeletedItemsFor];
			}
			set
			{
				this[ADSystemMailboxSchema.RetainDeletedItemsFor] = value;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x00073908 File Offset: 0x00071B08
		public bool IsMailboxEnabled
		{
			get
			{
				return (bool)this[ADSystemMailboxSchema.IsMailboxEnabled];
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0007391A File Offset: 0x00071B1A
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x0007392C File Offset: 0x00071B2C
		public ADObjectId OfflineAddressBook
		{
			get
			{
				return (ADObjectId)this[ADSystemMailboxSchema.OfflineAddressBook];
			}
			set
			{
				this[ADSystemMailboxSchema.OfflineAddressBook] = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0007393A File Offset: 0x00071B3A
		// (set) Token: 0x06001B91 RID: 7057 RVA: 0x0007394C File Offset: 0x00071B4C
		public Unlimited<ByteQuantifiedSize> ProhibitSendQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADSystemMailboxSchema.ProhibitSendQuota];
			}
			set
			{
				this[ADSystemMailboxSchema.ProhibitSendQuota] = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x0007395F File Offset: 0x00071B5F
		// (set) Token: 0x06001B93 RID: 7059 RVA: 0x00073971 File Offset: 0x00071B71
		public Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADSystemMailboxSchema.ProhibitSendReceiveQuota];
			}
			set
			{
				this[ADSystemMailboxSchema.ProhibitSendReceiveQuota] = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x00073984 File Offset: 0x00071B84
		// (set) Token: 0x06001B95 RID: 7061 RVA: 0x00073996 File Offset: 0x00071B96
		public string ServerLegacyDN
		{
			get
			{
				return (string)this[ADSystemMailboxSchema.ServerLegacyDN];
			}
			set
			{
				this[ADSystemMailboxSchema.ServerLegacyDN] = value;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x000739A4 File Offset: 0x00071BA4
		public string ServerName
		{
			get
			{
				return (string)this[ADSystemMailboxSchema.ServerName];
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x000739B6 File Offset: 0x00071BB6
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x000739C8 File Offset: 0x00071BC8
		public bool? UseDatabaseQuotaDefaults
		{
			get
			{
				return (bool?)this[ADSystemMailboxSchema.UseDatabaseQuotaDefaults];
			}
			set
			{
				this[ADSystemMailboxSchema.UseDatabaseQuotaDefaults] = value;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x000739DB File Offset: 0x00071BDB
		// (set) Token: 0x06001B9A RID: 7066 RVA: 0x000739ED File Offset: 0x00071BED
		public Unlimited<ByteQuantifiedSize> IssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADSystemMailboxSchema.IssueWarningQuota];
			}
			set
			{
				this[ADSystemMailboxSchema.IssueWarningQuota] = value;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x00073A00 File Offset: 0x00071C00
		// (set) Token: 0x06001B9C RID: 7068 RVA: 0x00073A12 File Offset: 0x00071C12
		public ByteQuantifiedSize RulesQuota
		{
			get
			{
				return (ByteQuantifiedSize)this[ADSystemMailboxSchema.RulesQuota];
			}
			set
			{
				this[ADSystemMailboxSchema.RulesQuota] = value;
			}
		}

		// Token: 0x04000BD1 RID: 3025
		internal static string MostDerivedClass = "msExchSystemMailbox";

		// Token: 0x04000BD2 RID: 3026
		private static readonly ADSystemMailboxSchema schema = ObjectSchema.GetInstance<ADSystemMailboxSchema>();
	}
}
