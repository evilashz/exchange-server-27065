using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200020A RID: 522
	[Serializable]
	public class ADSystemAttendantMailbox : ADRecipient, IADMailStorage
	{
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x000734AB File Offset: 0x000716AB
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADSystemAttendantMailbox.schema;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x000734B2 File Offset: 0x000716B2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADSystemAttendantMailbox.MostDerivedClass;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x000734B9 File Offset: 0x000716B9
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x000734CC File Offset: 0x000716CC
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2007;
			}
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x000734D4 File Offset: 0x000716D4
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			errors.AddRange(Microsoft.Exchange.Data.Directory.SystemConfiguration.Database.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
			{
				IADMailStorageSchema.IssueWarningQuota,
				IADMailStorageSchema.ProhibitSendQuota,
				IADMailStorageSchema.ProhibitSendReceiveQuota
			}, this.Identity));
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0007351F File Offset: 0x0007171F
		internal ADSystemAttendantMailbox(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00073529 File Offset: 0x00071729
		internal ADSystemAttendantMailbox(IRecipientSession session, string commonName, ADObjectId containerId)
		{
			this.m_Session = session;
			base.SetId(containerId.GetChildId("CN", commonName));
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00073556 File Offset: 0x00071756
		public ADSystemAttendantMailbox()
		{
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0007355E File Offset: 0x0007175E
		// (set) Token: 0x06001B59 RID: 7001 RVA: 0x00073570 File Offset: 0x00071770
		public ADObjectId Database
		{
			get
			{
				return (ADObjectId)this[IADMailStorageSchema.Database];
			}
			set
			{
				this[IADMailStorageSchema.Database] = value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0007357E File Offset: 0x0007177E
		// (set) Token: 0x06001B5B RID: 7003 RVA: 0x00073590 File Offset: 0x00071790
		public DeletedItemRetention DeletedItemFlags
		{
			get
			{
				return (DeletedItemRetention)this[IADMailStorageSchema.DeletedItemFlags];
			}
			set
			{
				this[IADMailStorageSchema.DeletedItemFlags] = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x000735A3 File Offset: 0x000717A3
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x000735B5 File Offset: 0x000717B5
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return (bool)this[IADMailStorageSchema.DeliverToMailboxAndForward];
			}
			set
			{
				this[IADMailStorageSchema.DeliverToMailboxAndForward] = value;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x000735C8 File Offset: 0x000717C8
		// (set) Token: 0x06001B5F RID: 7007 RVA: 0x000735DA File Offset: 0x000717DA
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[IADMailStorageSchema.ExchangeGuid];
			}
			set
			{
				this[IADMailStorageSchema.ExchangeGuid] = value;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x000735ED File Offset: 0x000717ED
		// (set) Token: 0x06001B61 RID: 7009 RVA: 0x000735FF File Offset: 0x000717FF
		public RawSecurityDescriptor ExchangeSecurityDescriptor
		{
			get
			{
				return (RawSecurityDescriptor)this[IADMailStorageSchema.ExchangeSecurityDescriptor];
			}
			set
			{
				this[IADMailStorageSchema.ExchangeSecurityDescriptor] = value;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0007360D File Offset: 0x0007180D
		// (set) Token: 0x06001B63 RID: 7011 RVA: 0x0007361F File Offset: 0x0007181F
		public ExternalOofOptions ExternalOofOptions
		{
			get
			{
				return (ExternalOofOptions)this[IADMailStorageSchema.ExternalOofOptions];
			}
			set
			{
				this[IADMailStorageSchema.ExternalOofOptions] = value;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x00073632 File Offset: 0x00071832
		// (set) Token: 0x06001B65 RID: 7013 RVA: 0x00073644 File Offset: 0x00071844
		public EnhancedTimeSpan RetainDeletedItemsFor
		{
			get
			{
				return (EnhancedTimeSpan)this[IADMailStorageSchema.RetainDeletedItemsFor];
			}
			set
			{
				this[IADMailStorageSchema.RetainDeletedItemsFor] = value;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x00073657 File Offset: 0x00071857
		public bool IsMailboxEnabled
		{
			get
			{
				return (bool)this[IADMailStorageSchema.IsMailboxEnabled];
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x00073669 File Offset: 0x00071869
		// (set) Token: 0x06001B68 RID: 7016 RVA: 0x0007367B File Offset: 0x0007187B
		public ADObjectId OfflineAddressBook
		{
			get
			{
				return (ADObjectId)this[IADMailStorageSchema.OfflineAddressBook];
			}
			set
			{
				this[IADMailStorageSchema.OfflineAddressBook] = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x00073689 File Offset: 0x00071889
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x0007369B File Offset: 0x0007189B
		public Unlimited<ByteQuantifiedSize> ProhibitSendQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[IADMailStorageSchema.ProhibitSendQuota];
			}
			set
			{
				this[IADMailStorageSchema.ProhibitSendQuota] = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x000736AE File Offset: 0x000718AE
		// (set) Token: 0x06001B6C RID: 7020 RVA: 0x000736C0 File Offset: 0x000718C0
		public Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[IADMailStorageSchema.ProhibitSendReceiveQuota];
			}
			set
			{
				this[IADMailStorageSchema.ProhibitSendReceiveQuota] = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x000736D3 File Offset: 0x000718D3
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x000736E5 File Offset: 0x000718E5
		public string ServerLegacyDN
		{
			get
			{
				return (string)this[IADMailStorageSchema.ServerLegacyDN];
			}
			set
			{
				this[IADMailStorageSchema.ServerLegacyDN] = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x000736F3 File Offset: 0x000718F3
		public string ServerName
		{
			get
			{
				return (string)this[IADMailStorageSchema.ServerName];
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x00073705 File Offset: 0x00071905
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x00073717 File Offset: 0x00071917
		public bool? UseDatabaseQuotaDefaults
		{
			get
			{
				return (bool?)this[IADMailStorageSchema.UseDatabaseQuotaDefaults];
			}
			set
			{
				this[IADMailStorageSchema.UseDatabaseQuotaDefaults] = value;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x0007372A File Offset: 0x0007192A
		// (set) Token: 0x06001B73 RID: 7027 RVA: 0x0007373C File Offset: 0x0007193C
		public Unlimited<ByteQuantifiedSize> IssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[IADMailStorageSchema.IssueWarningQuota];
			}
			set
			{
				this[IADMailStorageSchema.IssueWarningQuota] = value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x0007374F File Offset: 0x0007194F
		// (set) Token: 0x06001B75 RID: 7029 RVA: 0x00073761 File Offset: 0x00071961
		public ByteQuantifiedSize RulesQuota
		{
			get
			{
				return (ByteQuantifiedSize)this[IADMailStorageSchema.RulesQuota];
			}
			set
			{
				this[IADMailStorageSchema.RulesQuota] = value;
			}
		}

		// Token: 0x04000BCF RID: 3023
		private static readonly ADSystemAttendantMailboxSchema schema = ObjectSchema.GetInstance<ADSystemAttendantMailboxSchema>();

		// Token: 0x04000BD0 RID: 3024
		internal static string MostDerivedClass = "exchangeAdminService";
	}
}
