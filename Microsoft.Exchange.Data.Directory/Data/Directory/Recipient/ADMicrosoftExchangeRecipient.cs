using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001F4 RID: 500
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ADMicrosoftExchangeRecipient : ADRecipient, IADMailStorage
	{
		// Token: 0x060019D1 RID: 6609 RVA: 0x0006CE9A File Offset: 0x0006B09A
		internal static ADObjectId GetDefaultId(IConfigurationSession configurationSession)
		{
			if (configurationSession == null)
			{
				throw new ArgumentNullException("configurationSession");
			}
			return configurationSession.GetOrgContainerId().GetChildId("Transport Settings").GetChildId(ADMicrosoftExchangeRecipient.DefaultName);
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x0006CEC4 File Offset: 0x0006B0C4
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADMicrosoftExchangeRecipient.schema;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x0006CECB File Offset: 0x0006B0CB
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADMicrosoftExchangeRecipient.MostDerivedClass;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x0006CED2 File Offset: 0x0006B0D2
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x0006CEE5 File Offset: 0x0006B0E5
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2007;
			}
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0006CEEC File Offset: 0x0006B0EC
		internal ADMicrosoftExchangeRecipient(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0006CEF6 File Offset: 0x0006B0F6
		internal ADMicrosoftExchangeRecipient(IRecipientSession session, string commonName, ADObjectId containerId)
		{
			this.m_Session = session;
			base.SetId(containerId.GetChildId("CN", commonName));
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0006CF23 File Offset: 0x0006B123
		public ADMicrosoftExchangeRecipient()
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0006CF37 File Offset: 0x0006B137
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (!ADMicrosoftExchangeRecipient.DefaultDisplayName.Equals(base.DisplayName, StringComparison.OrdinalIgnoreCase))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.MicrosoftExchangeRecipientDisplayNameError(ADMicrosoftExchangeRecipient.DefaultDisplayName), this.Identity, string.Empty));
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x0006CF73 File Offset: 0x0006B173
		// (set) Token: 0x060019DB RID: 6619 RVA: 0x0006CF85 File Offset: 0x0006B185
		public ADObjectId Database
		{
			get
			{
				return (ADObjectId)this[IADMailStorageSchema.Database];
			}
			internal set
			{
				this[IADMailStorageSchema.Database] = value;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x0006CF93 File Offset: 0x0006B193
		// (set) Token: 0x060019DD RID: 6621 RVA: 0x0006CF9B File Offset: 0x0006B19B
		ADObjectId IADMailStorage.Database
		{
			get
			{
				return this.Database;
			}
			set
			{
				this.Database = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x0006CFA4 File Offset: 0x0006B1A4
		// (set) Token: 0x060019DF RID: 6623 RVA: 0x0006CFB6 File Offset: 0x0006B1B6
		public DeletedItemRetention DeletedItemFlags
		{
			get
			{
				return (DeletedItemRetention)this[IADMailStorageSchema.DeletedItemFlags];
			}
			internal set
			{
				this[IADMailStorageSchema.DeletedItemFlags] = value;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x0006CFC9 File Offset: 0x0006B1C9
		// (set) Token: 0x060019E1 RID: 6625 RVA: 0x0006CFD1 File Offset: 0x0006B1D1
		DeletedItemRetention IADMailStorage.DeletedItemFlags
		{
			get
			{
				return this.DeletedItemFlags;
			}
			set
			{
				this.DeletedItemFlags = value;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x0006CFDA File Offset: 0x0006B1DA
		// (set) Token: 0x060019E3 RID: 6627 RVA: 0x0006CFEC File Offset: 0x0006B1EC
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return (bool)this[IADMailStorageSchema.DeliverToMailboxAndForward];
			}
			internal set
			{
				this[IADMailStorageSchema.DeliverToMailboxAndForward] = value;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x0006CFFF File Offset: 0x0006B1FF
		// (set) Token: 0x060019E5 RID: 6629 RVA: 0x0006D007 File Offset: 0x0006B207
		bool IADMailStorage.DeliverToMailboxAndForward
		{
			get
			{
				return this.DeliverToMailboxAndForward;
			}
			set
			{
				this.DeliverToMailboxAndForward = value;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x0006D010 File Offset: 0x0006B210
		// (set) Token: 0x060019E7 RID: 6631 RVA: 0x0006D022 File Offset: 0x0006B222
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[IADMailStorageSchema.ExchangeGuid];
			}
			internal set
			{
				this[IADMailStorageSchema.ExchangeGuid] = value;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x0006D035 File Offset: 0x0006B235
		// (set) Token: 0x060019E9 RID: 6633 RVA: 0x0006D03D File Offset: 0x0006B23D
		Guid IADMailStorage.ExchangeGuid
		{
			get
			{
				return this.ExchangeGuid;
			}
			set
			{
				this.ExchangeGuid = value;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x0006D046 File Offset: 0x0006B246
		// (set) Token: 0x060019EB RID: 6635 RVA: 0x0006D058 File Offset: 0x0006B258
		public RawSecurityDescriptor ExchangeSecurityDescriptor
		{
			get
			{
				return (RawSecurityDescriptor)this[IADMailStorageSchema.ExchangeSecurityDescriptor];
			}
			internal set
			{
				this[IADMailStorageSchema.ExchangeSecurityDescriptor] = value;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x0006D066 File Offset: 0x0006B266
		// (set) Token: 0x060019ED RID: 6637 RVA: 0x0006D06E File Offset: 0x0006B26E
		RawSecurityDescriptor IADMailStorage.ExchangeSecurityDescriptor
		{
			get
			{
				return this.ExchangeSecurityDescriptor;
			}
			set
			{
				this.ExchangeSecurityDescriptor = value;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x0006D077 File Offset: 0x0006B277
		// (set) Token: 0x060019EF RID: 6639 RVA: 0x0006D089 File Offset: 0x0006B289
		public ExternalOofOptions ExternalOofOptions
		{
			get
			{
				return (ExternalOofOptions)this[IADMailStorageSchema.ExternalOofOptions];
			}
			internal set
			{
				this[IADMailStorageSchema.ExternalOofOptions] = value;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x0006D09C File Offset: 0x0006B29C
		// (set) Token: 0x060019F1 RID: 6641 RVA: 0x0006D0A4 File Offset: 0x0006B2A4
		ExternalOofOptions IADMailStorage.ExternalOofOptions
		{
			get
			{
				return this.ExternalOofOptions;
			}
			set
			{
				this.ExternalOofOptions = value;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x0006D0AD File Offset: 0x0006B2AD
		// (set) Token: 0x060019F3 RID: 6643 RVA: 0x0006D0BF File Offset: 0x0006B2BF
		public EnhancedTimeSpan RetainDeletedItemsFor
		{
			get
			{
				return (EnhancedTimeSpan)this[IADMailStorageSchema.RetainDeletedItemsFor];
			}
			internal set
			{
				this[IADMailStorageSchema.RetainDeletedItemsFor] = value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x0006D0D2 File Offset: 0x0006B2D2
		// (set) Token: 0x060019F5 RID: 6645 RVA: 0x0006D0DA File Offset: 0x0006B2DA
		EnhancedTimeSpan IADMailStorage.RetainDeletedItemsFor
		{
			get
			{
				return this.RetainDeletedItemsFor;
			}
			set
			{
				this.RetainDeletedItemsFor = value;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x0006D0E3 File Offset: 0x0006B2E3
		public bool IsMailboxEnabled
		{
			get
			{
				return (bool)this[IADMailStorageSchema.IsMailboxEnabled];
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x0006D0F5 File Offset: 0x0006B2F5
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x0006D107 File Offset: 0x0006B307
		public ADObjectId OfflineAddressBook
		{
			get
			{
				return (ADObjectId)this[IADMailStorageSchema.OfflineAddressBook];
			}
			internal set
			{
				this[IADMailStorageSchema.OfflineAddressBook] = value;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x0006D115 File Offset: 0x0006B315
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x0006D11D File Offset: 0x0006B31D
		ADObjectId IADMailStorage.OfflineAddressBook
		{
			get
			{
				return this.OfflineAddressBook;
			}
			set
			{
				this.OfflineAddressBook = value;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x0006D126 File Offset: 0x0006B326
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x0006D138 File Offset: 0x0006B338
		public Unlimited<ByteQuantifiedSize> ProhibitSendQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[IADMailStorageSchema.ProhibitSendQuota];
			}
			internal set
			{
				this[IADMailStorageSchema.ProhibitSendQuota] = value;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x0006D14B File Offset: 0x0006B34B
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x0006D153 File Offset: 0x0006B353
		Unlimited<ByteQuantifiedSize> IADMailStorage.ProhibitSendQuota
		{
			get
			{
				return this.ProhibitSendQuota;
			}
			set
			{
				this.ProhibitSendQuota = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0006D15C File Offset: 0x0006B35C
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x0006D16E File Offset: 0x0006B36E
		public Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[IADMailStorageSchema.ProhibitSendReceiveQuota];
			}
			internal set
			{
				this[IADMailStorageSchema.ProhibitSendReceiveQuota] = value;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0006D181 File Offset: 0x0006B381
		// (set) Token: 0x06001A02 RID: 6658 RVA: 0x0006D189 File Offset: 0x0006B389
		Unlimited<ByteQuantifiedSize> IADMailStorage.ProhibitSendReceiveQuota
		{
			get
			{
				return this.ProhibitSendReceiveQuota;
			}
			set
			{
				this.ProhibitSendReceiveQuota = value;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0006D192 File Offset: 0x0006B392
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x0006D1A4 File Offset: 0x0006B3A4
		public string ServerLegacyDN
		{
			get
			{
				return (string)this[IADMailStorageSchema.ServerLegacyDN];
			}
			internal set
			{
				this[IADMailStorageSchema.ServerLegacyDN] = value;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0006D1B2 File Offset: 0x0006B3B2
		// (set) Token: 0x06001A06 RID: 6662 RVA: 0x0006D1BA File Offset: 0x0006B3BA
		string IADMailStorage.ServerLegacyDN
		{
			get
			{
				return this.ServerLegacyDN;
			}
			set
			{
				this.ServerLegacyDN = value;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0006D1C3 File Offset: 0x0006B3C3
		public string ServerName
		{
			get
			{
				return (string)this[IADMailStorageSchema.ServerName];
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x0006D1D5 File Offset: 0x0006B3D5
		// (set) Token: 0x06001A09 RID: 6665 RVA: 0x0006D1E7 File Offset: 0x0006B3E7
		public bool? UseDatabaseQuotaDefaults
		{
			get
			{
				return (bool?)this[IADMailStorageSchema.UseDatabaseQuotaDefaults];
			}
			internal set
			{
				this[IADMailStorageSchema.UseDatabaseQuotaDefaults] = value;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0006D1FA File Offset: 0x0006B3FA
		// (set) Token: 0x06001A0B RID: 6667 RVA: 0x0006D202 File Offset: 0x0006B402
		bool? IADMailStorage.UseDatabaseQuotaDefaults
		{
			get
			{
				return this.UseDatabaseQuotaDefaults;
			}
			set
			{
				this.UseDatabaseQuotaDefaults = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x0006D20B File Offset: 0x0006B40B
		// (set) Token: 0x06001A0D RID: 6669 RVA: 0x0006D21D File Offset: 0x0006B41D
		public Unlimited<ByteQuantifiedSize> IssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[IADMailStorageSchema.IssueWarningQuota];
			}
			internal set
			{
				this[IADMailStorageSchema.IssueWarningQuota] = value;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x0006D230 File Offset: 0x0006B430
		// (set) Token: 0x06001A0F RID: 6671 RVA: 0x0006D238 File Offset: 0x0006B438
		Unlimited<ByteQuantifiedSize> IADMailStorage.IssueWarningQuota
		{
			get
			{
				return this.IssueWarningQuota;
			}
			set
			{
				this.IssueWarningQuota = value;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x0006D241 File Offset: 0x0006B441
		// (set) Token: 0x06001A11 RID: 6673 RVA: 0x0006D253 File Offset: 0x0006B453
		public ByteQuantifiedSize RulesQuota
		{
			get
			{
				return (ByteQuantifiedSize)this[IADMailStorageSchema.RulesQuota];
			}
			internal set
			{
				this[IADMailStorageSchema.RulesQuota] = value;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x0006D266 File Offset: 0x0006B466
		// (set) Token: 0x06001A13 RID: 6675 RVA: 0x0006D26E File Offset: 0x0006B46E
		ByteQuantifiedSize IADMailStorage.RulesQuota
		{
			get
			{
				return this.RulesQuota;
			}
			set
			{
				this.RulesQuota = value;
			}
		}

		// Token: 0x04000B66 RID: 2918
		private static ADMicrosoftExchangeRecipientSchema schema = ObjectSchema.GetInstance<ADMicrosoftExchangeRecipientSchema>();

		// Token: 0x04000B67 RID: 2919
		internal static string MostDerivedClass = "msExchExchangeServerRecipient";

		// Token: 0x04000B68 RID: 2920
		public static readonly string DefaultName = "MicrosoftExchange329e71ec88ae4615bbc36ab6ce41109e";

		// Token: 0x04000B69 RID: 2921
		public static readonly string DefaultDisplayName = "Microsoft Outlook";
	}
}
