using System;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001DB RID: 475
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class MiniRecipient : MiniObject
	{
		// Token: 0x060013BB RID: 5051 RVA: 0x0005FB09 File Offset: 0x0005DD09
		internal MiniRecipient(IRecipientSession session, PropertyBag propertyBag)
		{
			this.m_Session = session;
			this.propertyBag = (ADPropertyBag)propertyBag;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0005FB24 File Offset: 0x0005DD24
		public MiniRecipient()
		{
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0005FB2C File Offset: 0x0005DD2C
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ADRecipient.PublicFolderMailboxObjectVersion;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0005FB33 File Offset: 0x0005DD33
		internal override ADObjectSchema Schema
		{
			get
			{
				return MiniRecipient.schema;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0005FB3A File Offset: 0x0005DD3A
		internal override string MostDerivedObjectClass
		{
			get
			{
				throw new InvalidADObjectOperationException(DirectoryStrings.ExceptionMostDerivedOnBase("MiniRecipient"));
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0005FB4B File Offset: 0x0005DD4B
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return Filters.DefaultRecipientFilter;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0005FB52 File Offset: 0x0005DD52
		public ADObjectId ArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[MiniRecipientSchema.ArchiveDatabase];
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0005FB64 File Offset: 0x0005DD64
		public Guid ArchiveGuid
		{
			get
			{
				return (Guid)this[MiniRecipientSchema.ArchiveGuid];
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0005FB76 File Offset: 0x0005DD76
		public MultiValuedProperty<string> ArchiveName
		{
			get
			{
				return (MultiValuedProperty<string>)this[MiniRecipientSchema.ArchiveName];
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0005FB88 File Offset: 0x0005DD88
		public ArchiveState ArchiveState
		{
			get
			{
				return (ArchiveState)this[MiniRecipientSchema.ArchiveState];
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0005FB9A File Offset: 0x0005DD9A
		public SmtpAddress JournalArchiveAddress
		{
			get
			{
				return (SmtpAddress)this[MiniRecipientSchema.JournalArchiveAddress];
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0005FBAC File Offset: 0x0005DDAC
		public ADObjectId Database
		{
			get
			{
				return (ADObjectId)this[MiniRecipientSchema.Database];
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0005FBBE File Offset: 0x0005DDBE
		public string DisplayName
		{
			get
			{
				return (string)this[MiniRecipientSchema.DisplayName];
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x0005FBD0 File Offset: 0x0005DDD0
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[MiniRecipientSchema.ExchangeGuid];
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0005FBE2 File Offset: 0x0005DDE2
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x0005FBF4 File Offset: 0x0005DDF4
		public Guid? MailboxContainerGuid
		{
			get
			{
				return (Guid?)this[ADUserSchema.MailboxContainerGuid];
			}
			set
			{
				this[ADUserSchema.MailboxContainerGuid] = value;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0005FC07 File Offset: 0x0005DE07
		public MultiValuedProperty<Guid> AggregatedMailboxGuids
		{
			get
			{
				return (MultiValuedProperty<Guid>)this[MiniRecipientSchema.AggregatedMailboxGuids];
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x0005FC19 File Offset: 0x0005DE19
		public RawSecurityDescriptor ExchangeSecurityDescriptor
		{
			get
			{
				return (RawSecurityDescriptor)this[MiniRecipientSchema.ExchangeSecurityDescriptor];
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0005FC2B File Offset: 0x0005DE2B
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return (ProxyAddress)this[MiniRecipientSchema.ExternalEmailAddress];
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0005FC3D File Offset: 0x0005DE3D
		public MultiValuedProperty<ADObjectId> GrantSendOnBehalfTo
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MiniRecipientSchema.GrantSendOnBehalfTo];
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0005FC4F File Offset: 0x0005DE4F
		public MultiValuedProperty<CultureInfo> Languages
		{
			get
			{
				return (MultiValuedProperty<CultureInfo>)this[MiniRecipientSchema.Languages];
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x0005FC61 File Offset: 0x0005DE61
		public string LegacyExchangeDN
		{
			get
			{
				return (string)this[MiniRecipientSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0005FC73 File Offset: 0x0005DE73
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				return (SecurityIdentifier)this[MiniRecipientSchema.MasterAccountSid];
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0005FC85 File Offset: 0x0005DE85
		public bool MAPIEnabled
		{
			get
			{
				return (bool)this[MiniRecipientSchema.MAPIEnabled];
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0005FC97 File Offset: 0x0005DE97
		public bool OWAEnabled
		{
			get
			{
				return (bool)this[MiniRecipientSchema.OWAEnabled];
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0005FCA9 File Offset: 0x0005DEA9
		public bool MOWAEnabled
		{
			get
			{
				return (bool)this[MiniRecipientSchema.MOWAEnabled];
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0005FCBB File Offset: 0x0005DEBB
		public ADObjectId OwaMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.OwaMailboxPolicy];
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x0005FCCD File Offset: 0x0005DECD
		public ADObjectId MobileDeviceMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[MiniRecipientSchema.MobileDeviceMailboxPolicy];
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0005FCDF File Offset: 0x0005DEDF
		public ADObjectId AddressBookPolicy
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.AddressBookPolicy];
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0005FCF1 File Offset: 0x0005DEF1
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[MiniRecipientSchema.PrimarySmtpAddress];
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0005FD03 File Offset: 0x0005DF03
		public ADObjectId QueryBaseDN
		{
			get
			{
				return (ADObjectId)this[MiniRecipientSchema.QueryBaseDN];
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x0005FD15 File Offset: 0x0005DF15
		public RecipientType RecipientType
		{
			get
			{
				return (RecipientType)this[MiniRecipientSchema.RecipientType];
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0005FD27 File Offset: 0x0005DF27
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[MiniRecipientSchema.RecipientTypeDetails];
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0005FD39 File Offset: 0x0005DF39
		public bool IsResource
		{
			get
			{
				return (bool)this[MiniRecipientSchema.IsResource];
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0005FD4B File Offset: 0x0005DF4B
		public ADObjectId DefaultPublicFolderMailbox
		{
			get
			{
				return (ADObjectId)this[MiniRecipientSchema.DefaultPublicFolderMailbox];
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x0005FD5D File Offset: 0x0005DF5D
		public string ServerLegacyDN
		{
			get
			{
				return (string)this[MiniRecipientSchema.ServerLegacyDN];
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0005FD6F File Offset: 0x0005DF6F
		public SecurityIdentifier Sid
		{
			get
			{
				return (SecurityIdentifier)this[MiniRecipientSchema.Sid];
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0005FD81 File Offset: 0x0005DF81
		public MultiValuedProperty<SecurityIdentifier> SidHistory
		{
			get
			{
				return (MultiValuedProperty<SecurityIdentifier>)this[MiniRecipientSchema.SidHistory];
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0005FD93 File Offset: 0x0005DF93
		public string UserPrincipalName
		{
			get
			{
				return (string)this[MiniRecipientSchema.UserPrincipalName];
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0005FDA5 File Offset: 0x0005DFA5
		public SmtpAddress WindowsLiveID
		{
			get
			{
				return (SmtpAddress)this[MiniRecipientSchema.WindowsLiveID];
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0005FDB7 File Offset: 0x0005DFB7
		public NetID NetID
		{
			get
			{
				return (NetID)this[MiniRecipientSchema.NetID];
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0005FDC9 File Offset: 0x0005DFC9
		public bool IsPersonToPersonTextMessagingEnabled
		{
			get
			{
				return (bool)this[MiniRecipientSchema.IsPersonToPersonTextMessagingEnabled];
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0005FDDB File Offset: 0x0005DFDB
		public bool IsMachineToPersonTextMessagingEnabled
		{
			get
			{
				return (bool)this[MiniRecipientSchema.IsMachineToPersonTextMessagingEnabled];
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0005FDED File Offset: 0x0005DFED
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x0005FDFF File Offset: 0x0005DFFF
		public MultiValuedProperty<Capability> PersistedCapabilities
		{
			get
			{
				return (MultiValuedProperty<Capability>)this[MiniRecipientSchema.PersistedCapabilities];
			}
			set
			{
				this[MiniRecipientSchema.PersistedCapabilities] = value;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0005FE0D File Offset: 0x0005E00D
		internal string ExternalDirectoryObjectId
		{
			get
			{
				return (string)this[MiniRecipientSchema.ExternalDirectoryObjectId];
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0005FE1F File Offset: 0x0005E01F
		internal Capability? SKUCapability
		{
			get
			{
				return CapabilityHelper.GetSKUCapability(this.PersistedCapabilities);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x0005FE2C File Offset: 0x0005E02C
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x0005FE3E File Offset: 0x0005E03E
		public bool? SKUAssigned
		{
			get
			{
				return (bool?)this[MiniRecipientSchema.SKUAssigned];
			}
			set
			{
				this[MiniRecipientSchema.SKUAssigned] = value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x0005FE51 File Offset: 0x0005E051
		public Uri SharePointUrl
		{
			get
			{
				return (Uri)this[MiniRecipientSchema.SharePointUrl];
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0005FE63 File Offset: 0x0005E063
		public DateTime? WhenMailboxCreated
		{
			get
			{
				return (DateTime?)this[MiniRecipientSchema.WhenMailboxCreated];
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x0005FE75 File Offset: 0x0005E075
		public bool MailboxAuditEnabled
		{
			get
			{
				return (bool)this[MiniRecipientSchema.AuditEnabled];
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0005FE87 File Offset: 0x0005E087
		public EnhancedTimeSpan MailboxAuditLogAgeLimit
		{
			get
			{
				return (EnhancedTimeSpan)this[MiniRecipientSchema.AuditLogAgeLimit];
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0005FE99 File Offset: 0x0005E099
		public MailboxAuditOperations AuditAdminOperations
		{
			get
			{
				return (MailboxAuditOperations)this[MiniRecipientSchema.AuditAdminFlags];
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0005FEAB File Offset: 0x0005E0AB
		public MailboxAuditOperations AuditDelegateOperations
		{
			get
			{
				return (MailboxAuditOperations)this[MiniRecipientSchema.AuditDelegateFlags];
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0005FEBD File Offset: 0x0005E0BD
		public MailboxAuditOperations AuditDelegateAdminOperations
		{
			get
			{
				return (MailboxAuditOperations)this[MiniRecipientSchema.AuditDelegateAdminFlags];
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0005FECF File Offset: 0x0005E0CF
		public MailboxAuditOperations AuditOwnerOperations
		{
			get
			{
				return (MailboxAuditOperations)this[MiniRecipientSchema.AuditOwnerFlags];
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0005FEE1 File Offset: 0x0005E0E1
		public bool BypassAudit
		{
			get
			{
				return (bool)this[MiniRecipientSchema.AuditBypassEnabled];
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0005FEF3 File Offset: 0x0005E0F3
		public DateTime? AuditLastAdminAccess
		{
			get
			{
				return (DateTime?)this[MiniRecipientSchema.AuditLastAdminAccess];
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0005FF05 File Offset: 0x0005E105
		public DateTime? AuditLastDelegateAccess
		{
			get
			{
				return (DateTime?)this[MiniRecipientSchema.AuditLastDelegateAccess];
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0005FF17 File Offset: 0x0005E117
		public DateTime? AuditLastExternalAccess
		{
			get
			{
				return (DateTime?)this[MiniRecipientSchema.AuditLastExternalAccess];
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x0005FF29 File Offset: 0x0005E129
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[MiniRecipientSchema.EmailAddresses];
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0005FF3B File Offset: 0x0005E13B
		public ModernGroupObjectType ModernGroupType
		{
			get
			{
				return (ModernGroupObjectType)this[MiniRecipientSchema.ModernGroupType];
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x0005FF50 File Offset: 0x0005E150
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				ReleaseTrack? result;
				try
				{
					result = (this.propertyBag.Contains(MiniRecipientSchema.ConfigurationXML) ? ((ReleaseTrack?)this[MiniRecipientSchema.ReleaseTrack]) : null);
				}
				catch (Exception ex)
				{
					if (!(ex is DataValidationException) && !(ex is ValueNotPresentException))
					{
						throw;
					}
					result = null;
				}
				return result;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0005FFC0 File Offset: 0x0005E1C0
		public MultiValuedProperty<SecurityIdentifier> PublicToGroupSids
		{
			get
			{
				return (MultiValuedProperty<SecurityIdentifier>)this[MiniRecipientSchema.PublicToGroupSids];
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x0005FFD2 File Offset: 0x0005E1D2
		public ADObjectId ThrottlingPolicy
		{
			get
			{
				return (ADObjectId)this[MiniRecipientSchema.ThrottlingPolicy];
			}
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0005FFE4 File Offset: 0x0005E1E4
		internal IThrottlingPolicy ReadThrottlingPolicy()
		{
			if (this.ThrottlingPolicy != null)
			{
				return ThrottlingPolicyCache.Singleton.Get(base.OrganizationId, this.ThrottlingPolicy);
			}
			return this.ReadDefaultThrottlingPolicy();
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0006000B File Offset: 0x0005E20B
		internal IThrottlingPolicy ReadDefaultThrottlingPolicy()
		{
			return ThrottlingPolicyCache.Singleton.Get(base.OrganizationId);
		}

		// Token: 0x04000AE7 RID: 2791
		private static readonly MiniRecipientSchema schema = ObjectSchema.GetInstance<MiniRecipientSchema>();
	}
}
