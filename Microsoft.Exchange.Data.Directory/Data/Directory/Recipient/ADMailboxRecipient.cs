using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001E7 RID: 487
	[Serializable]
	public abstract class ADMailboxRecipient : ADRecipient, IADMailboxRecipient, IADRecipient, IADObject, IADRawEntry, IConfigurable, IPropertyBag, IReadOnlyPropertyBag, IADMailStorage, IADSecurityPrincipal
	{
		// Token: 0x0600167E RID: 5758 RVA: 0x00066D2E File Offset: 0x00064F2E
		internal ADMailboxRecipient(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00066D38 File Offset: 0x00064F38
		internal ADMailboxRecipient()
		{
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x00066D40 File Offset: 0x00064F40
		// (set) Token: 0x06001681 RID: 5761 RVA: 0x00066D52 File Offset: 0x00064F52
		public ADObjectId Database
		{
			get
			{
				return (ADObjectId)this[ADMailboxRecipientSchema.Database];
			}
			set
			{
				this[ADMailboxRecipientSchema.Database] = value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x00066D60 File Offset: 0x00064F60
		// (set) Token: 0x06001683 RID: 5763 RVA: 0x00066D72 File Offset: 0x00064F72
		public DeletedItemRetention DeletedItemFlags
		{
			get
			{
				return (DeletedItemRetention)this[ADMailboxRecipientSchema.DeletedItemFlags];
			}
			set
			{
				this[ADMailboxRecipientSchema.DeletedItemFlags] = value;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00066D85 File Offset: 0x00064F85
		// (set) Token: 0x06001685 RID: 5765 RVA: 0x00066D97 File Offset: 0x00064F97
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return (bool)this[ADMailboxRecipientSchema.DeliverToMailboxAndForward];
			}
			set
			{
				this[ADMailboxRecipientSchema.DeliverToMailboxAndForward] = value;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00066DAA File Offset: 0x00064FAA
		// (set) Token: 0x06001687 RID: 5767 RVA: 0x00066DBC File Offset: 0x00064FBC
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[ADMailboxRecipientSchema.ExchangeGuid];
			}
			set
			{
				this[ADMailboxRecipientSchema.ExchangeGuid] = value;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x00066DCF File Offset: 0x00064FCF
		// (set) Token: 0x06001689 RID: 5769 RVA: 0x00066DE1 File Offset: 0x00064FE1
		public RawSecurityDescriptor ExchangeSecurityDescriptor
		{
			get
			{
				return (RawSecurityDescriptor)this[ADMailboxRecipientSchema.ExchangeSecurityDescriptor];
			}
			set
			{
				this[ADMailboxRecipientSchema.ExchangeSecurityDescriptor] = value;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x00066DEF File Offset: 0x00064FEF
		// (set) Token: 0x0600168B RID: 5771 RVA: 0x00066E01 File Offset: 0x00065001
		public ExternalOofOptions ExternalOofOptions
		{
			get
			{
				return (ExternalOofOptions)this[ADMailboxRecipientSchema.ExternalOofOptions];
			}
			set
			{
				this[ADMailboxRecipientSchema.ExternalOofOptions] = value;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x00066E14 File Offset: 0x00065014
		public bool IsMailboxEnabled
		{
			get
			{
				return (bool)this[ADMailboxRecipientSchema.IsMailboxEnabled];
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x00066E26 File Offset: 0x00065026
		// (set) Token: 0x0600168E RID: 5774 RVA: 0x00066E38 File Offset: 0x00065038
		public Unlimited<ByteQuantifiedSize> IssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADMailboxRecipientSchema.IssueWarningQuota];
			}
			set
			{
				this[ADMailboxRecipientSchema.IssueWarningQuota] = value;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x00066E4B File Offset: 0x0006504B
		// (set) Token: 0x06001690 RID: 5776 RVA: 0x00066E5D File Offset: 0x0006505D
		public ADObjectId OfflineAddressBook
		{
			get
			{
				return (ADObjectId)this[ADMailboxRecipientSchema.OfflineAddressBook];
			}
			set
			{
				this[ADMailboxRecipientSchema.OfflineAddressBook] = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x00066E6B File Offset: 0x0006506B
		// (set) Token: 0x06001692 RID: 5778 RVA: 0x00066E7D File Offset: 0x0006507D
		public Unlimited<ByteQuantifiedSize> ProhibitSendQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADMailboxRecipientSchema.ProhibitSendQuota];
			}
			set
			{
				this[ADMailboxRecipientSchema.ProhibitSendQuota] = value;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x00066E90 File Offset: 0x00065090
		// (set) Token: 0x06001694 RID: 5780 RVA: 0x00066EA2 File Offset: 0x000650A2
		public Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADMailboxRecipientSchema.ProhibitSendReceiveQuota];
			}
			set
			{
				this[ADMailboxRecipientSchema.ProhibitSendReceiveQuota] = value;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x00066EB5 File Offset: 0x000650B5
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x00066EC7 File Offset: 0x000650C7
		public EnhancedTimeSpan RetainDeletedItemsFor
		{
			get
			{
				return (EnhancedTimeSpan)this[ADMailboxRecipientSchema.RetainDeletedItemsFor];
			}
			set
			{
				this[ADMailboxRecipientSchema.RetainDeletedItemsFor] = value;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x00066EDA File Offset: 0x000650DA
		// (set) Token: 0x06001698 RID: 5784 RVA: 0x00066EEC File Offset: 0x000650EC
		public ByteQuantifiedSize RulesQuota
		{
			get
			{
				return (ByteQuantifiedSize)this[ADMailboxRecipientSchema.RulesQuota];
			}
			set
			{
				this[ADMailboxRecipientSchema.RulesQuota] = value;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x00066EFF File Offset: 0x000650FF
		// (set) Token: 0x0600169A RID: 5786 RVA: 0x00066F11 File Offset: 0x00065111
		public string ServerLegacyDN
		{
			get
			{
				return (string)this[ADMailboxRecipientSchema.ServerLegacyDN];
			}
			set
			{
				this[ADMailboxRecipientSchema.ServerLegacyDN] = value;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x00066F1F File Offset: 0x0006511F
		public string ServerName
		{
			get
			{
				return (string)this[ADMailboxRecipientSchema.ServerName];
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00066F31 File Offset: 0x00065131
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x00066F43 File Offset: 0x00065143
		public bool? UseDatabaseQuotaDefaults
		{
			get
			{
				return (bool?)this[ADMailboxRecipientSchema.UseDatabaseQuotaDefaults];
			}
			set
			{
				this[ADMailboxRecipientSchema.UseDatabaseQuotaDefaults] = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00066F56 File Offset: 0x00065156
		public bool IsSecurityPrincipal
		{
			get
			{
				return (bool)this[ADMailboxRecipientSchema.IsSecurityPrincipal];
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x00066F68 File Offset: 0x00065168
		// (set) Token: 0x060016A0 RID: 5792 RVA: 0x00066F7A File Offset: 0x0006517A
		public string SamAccountName
		{
			get
			{
				return (string)this[ADMailboxRecipientSchema.SamAccountName];
			}
			set
			{
				this[ADMailboxRecipientSchema.SamAccountName] = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00066F88 File Offset: 0x00065188
		public SecurityIdentifier Sid
		{
			get
			{
				return (SecurityIdentifier)this[ADMailboxRecipientSchema.Sid];
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00066F9A File Offset: 0x0006519A
		public MultiValuedProperty<SecurityIdentifier> SidHistory
		{
			get
			{
				return (MultiValuedProperty<SecurityIdentifier>)this[ADMailboxRecipientSchema.SidHistory];
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00066FAC File Offset: 0x000651AC
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x00066FBE File Offset: 0x000651BE
		public ModernGroupObjectType ModernGroupType
		{
			get
			{
				return (ModernGroupObjectType)this[ADRecipientSchema.ModernGroupType];
			}
			set
			{
				this[ADRecipientSchema.ModernGroupType] = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x00066FD1 File Offset: 0x000651D1
		// (set) Token: 0x060016A6 RID: 5798 RVA: 0x00066FE3 File Offset: 0x000651E3
		public MultiValuedProperty<ADObjectId> DelegateListLink
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADMailboxRecipientSchema.DelegateListLink];
			}
			internal set
			{
				this[ADMailboxRecipientSchema.DelegateListLink] = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00066FF1 File Offset: 0x000651F1
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x00067003 File Offset: 0x00065203
		public MultiValuedProperty<ADObjectId> DelegateListBL
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADMailboxRecipientSchema.DelegateListBL];
			}
			internal set
			{
				this[ADMailboxRecipientSchema.DelegateListBL] = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x00067011 File Offset: 0x00065211
		public MultiValuedProperty<SecurityIdentifier> PublicToGroupSids
		{
			get
			{
				return (MultiValuedProperty<SecurityIdentifier>)this[ADMailboxRecipientSchema.PublicToGroupSids];
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x00067023 File Offset: 0x00065223
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x00067035 File Offset: 0x00065235
		public MultiValuedProperty<string> SharePointResources
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADMailboxRecipientSchema.SharePointResources];
			}
			internal set
			{
				this[ADMailboxRecipientSchema.SharePointResources] = value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x00067043 File Offset: 0x00065243
		public string SharePointSiteUrl
		{
			get
			{
				return (string)this[ADMailboxRecipientSchema.GroupMailboxSharePointSiteUrl];
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00067055 File Offset: 0x00065255
		public string SharePointDocumentsUrl
		{
			get
			{
				return (string)this[ADMailboxRecipientSchema.GroupMailboxSharePointDocumentsUrl];
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x00067067 File Offset: 0x00065267
		// (set) Token: 0x060016AF RID: 5807 RVA: 0x00067079 File Offset: 0x00065279
		public Uri SharePointUrl
		{
			get
			{
				return (Uri)this[ADMailboxRecipientSchema.SharePointUrl];
			}
			internal set
			{
				this[ADMailboxRecipientSchema.SharePointUrl] = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x00067087 File Offset: 0x00065287
		public DateTime? WhenMailboxCreated
		{
			get
			{
				return (DateTime?)this[ADMailboxRecipientSchema.WhenMailboxCreated];
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x00067099 File Offset: 0x00065299
		// (set) Token: 0x060016B2 RID: 5810 RVA: 0x000670AB File Offset: 0x000652AB
		public string YammerGroupAddress
		{
			get
			{
				return this[ADMailboxRecipientSchema.YammerGroupAddress] as string;
			}
			set
			{
				this[ADMailboxRecipientSchema.YammerGroupAddress] = value;
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x000670BC File Offset: 0x000652BC
		internal bool SetWhenMailboxCreatedIfNotSet()
		{
			if (this.WhenMailboxCreated == null)
			{
				this[ADMailboxRecipientSchema.WhenMailboxCreated] = DateTime.UtcNow;
				return true;
			}
			return false;
		}
	}
}
