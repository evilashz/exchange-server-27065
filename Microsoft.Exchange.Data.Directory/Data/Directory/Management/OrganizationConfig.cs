using System;
using System.IO;
using System.IO.Compression;
using System.Security.Principal;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000741 RID: 1857
	[Serializable]
	public sealed class OrganizationConfig : ADPresentationObject
	{
		// Token: 0x17001EF7 RID: 7927
		// (get) Token: 0x06005A22 RID: 23074 RVA: 0x0013D2A9 File Offset: 0x0013B4A9
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return OrganizationConfig.schema;
			}
		}

		// Token: 0x17001EF8 RID: 7928
		// (get) Token: 0x06005A23 RID: 23075 RVA: 0x0013D2B0 File Offset: 0x0013B4B0
		internal MultiValuedProperty<string> AcceptedDomainNames
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationSchema.AcceptedDomainNames];
			}
		}

		// Token: 0x06005A24 RID: 23076 RVA: 0x0013D2C2 File Offset: 0x0013B4C2
		public OrganizationConfig()
		{
		}

		// Token: 0x06005A25 RID: 23077 RVA: 0x0013D2CA File Offset: 0x0013B4CA
		public OrganizationConfig(ADOrganizationConfig dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005A26 RID: 23078 RVA: 0x0013D2D3 File Offset: 0x0013B4D3
		public OrganizationConfig(ADOrganizationConfig dataObject, bool valuesQueriedFromDC) : base(dataObject)
		{
			this.valuesQueriedFromDC = valuesQueriedFromDC;
		}

		// Token: 0x17001EF9 RID: 7929
		// (get) Token: 0x06005A27 RID: 23079 RVA: 0x0013D2E3 File Offset: 0x0013B4E3
		public new OrganizationId OrganizationId
		{
			get
			{
				return (OrganizationId)this[OrganizationConfigSchema.OrganizationId];
			}
		}

		// Token: 0x17001EFA RID: 7930
		// (get) Token: 0x06005A28 RID: 23080 RVA: 0x0013D2F5 File Offset: 0x0013B4F5
		public new string Name
		{
			get
			{
				if (base.DataObject.OrganizationalUnitRoot != null)
				{
					return base.DataObject.OrganizationId.OrganizationalUnit.Name;
				}
				return base.Name;
			}
		}

		// Token: 0x17001EFB RID: 7931
		// (get) Token: 0x06005A29 RID: 23081 RVA: 0x0013D320 File Offset: 0x0013B520
		public new ObjectId Identity
		{
			get
			{
				if (base.DataObject.OrganizationalUnitRoot == null)
				{
					return base.Identity;
				}
				ObjectId objectId = base.DataObject.OrganizationId.OrganizationalUnit;
				object obj;
				if (base.TryConvertOutputProperty(objectId, "Identity", out obj))
				{
					objectId = (ObjectId)obj;
				}
				return objectId;
			}
		}

		// Token: 0x17001EFC RID: 7932
		// (get) Token: 0x06005A2A RID: 23082 RVA: 0x0013D36A File Offset: 0x0013B56A
		public new Guid Guid
		{
			get
			{
				if (base.DataObject.OrganizationalUnitRoot != null)
				{
					return base.DataObject.OrganizationId.OrganizationalUnit.ObjectGuid;
				}
				return base.Guid;
			}
		}

		// Token: 0x17001EFD RID: 7933
		// (get) Token: 0x06005A2B RID: 23083 RVA: 0x0013D395 File Offset: 0x0013B595
		public int ObjectVersion
		{
			get
			{
				return (int)this.propertyBag[OrganizationConfigSchema.ObjectVersion];
			}
		}

		// Token: 0x17001EFE RID: 7934
		// (get) Token: 0x06005A2C RID: 23084 RVA: 0x0013D3AC File Offset: 0x0013B5AC
		public EnhancedTimeSpan? DefaultPublicFolderAgeLimit
		{
			get
			{
				return (EnhancedTimeSpan?)this[OrganizationConfigSchema.DefaultPublicFolderAgeLimit];
			}
		}

		// Token: 0x17001EFF RID: 7935
		// (get) Token: 0x06005A2D RID: 23085 RVA: 0x0013D3BE File Offset: 0x0013B5BE
		public Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[OrganizationConfigSchema.DefaultPublicFolderIssueWarningQuota];
			}
		}

		// Token: 0x17001F00 RID: 7936
		// (get) Token: 0x06005A2E RID: 23086 RVA: 0x0013D3D0 File Offset: 0x0013B5D0
		public Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[OrganizationConfigSchema.DefaultPublicFolderProhibitPostQuota];
			}
		}

		// Token: 0x17001F01 RID: 7937
		// (get) Token: 0x06005A2F RID: 23087 RVA: 0x0013D3E2 File Offset: 0x0013B5E2
		public Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[OrganizationConfigSchema.DefaultPublicFolderMaxItemSize];
			}
		}

		// Token: 0x17001F02 RID: 7938
		// (get) Token: 0x06005A30 RID: 23088 RVA: 0x0013D3F4 File Offset: 0x0013B5F4
		public EnhancedTimeSpan? DefaultPublicFolderDeletedItemRetention
		{
			get
			{
				return (EnhancedTimeSpan?)this[OrganizationConfigSchema.DefaultPublicFolderDeletedItemRetention];
			}
		}

		// Token: 0x17001F03 RID: 7939
		// (get) Token: 0x06005A31 RID: 23089 RVA: 0x0013D406 File Offset: 0x0013B606
		public EnhancedTimeSpan? DefaultPublicFolderMovedItemRetention
		{
			get
			{
				return (EnhancedTimeSpan?)this[OrganizationConfigSchema.DefaultPublicFolderMovedItemRetention];
			}
		}

		// Token: 0x17001F04 RID: 7940
		// (get) Token: 0x06005A32 RID: 23090 RVA: 0x0013D418 File Offset: 0x0013B618
		public bool PublicFoldersLockedForMigration
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.PublicFoldersLockedForMigration];
			}
		}

		// Token: 0x17001F05 RID: 7941
		// (get) Token: 0x06005A33 RID: 23091 RVA: 0x0013D42A File Offset: 0x0013B62A
		public bool PublicFolderMigrationComplete
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.PublicFolderMigrationComplete];
			}
		}

		// Token: 0x17001F06 RID: 7942
		// (get) Token: 0x06005A34 RID: 23092 RVA: 0x0013D43C File Offset: 0x0013B63C
		public bool PublicFolderMailboxesLockedForNewConnections
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.PublicFolderMailboxesLockedForNewConnections];
			}
		}

		// Token: 0x17001F07 RID: 7943
		// (get) Token: 0x06005A35 RID: 23093 RVA: 0x0013D44E File Offset: 0x0013B64E
		public bool PublicFolderMailboxesMigrationComplete
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.PublicFolderMailboxesMigrationComplete];
			}
		}

		// Token: 0x17001F08 RID: 7944
		// (get) Token: 0x06005A36 RID: 23094 RVA: 0x0013D460 File Offset: 0x0013B660
		public PublicFoldersDeployment PublicFoldersEnabled
		{
			get
			{
				return (PublicFoldersDeployment)this[OrganizationConfigSchema.PublicFoldersEnabled];
			}
		}

		// Token: 0x17001F09 RID: 7945
		// (get) Token: 0x06005A37 RID: 23095 RVA: 0x0013D472 File Offset: 0x0013B672
		public bool ActivityBasedAuthenticationTimeoutEnabled
		{
			get
			{
				return !(bool)this[OrganizationConfigSchema.ActivityBasedAuthenticationTimeoutDisabled];
			}
		}

		// Token: 0x17001F0A RID: 7946
		// (get) Token: 0x06005A38 RID: 23096 RVA: 0x0013D487 File Offset: 0x0013B687
		public EnhancedTimeSpan ActivityBasedAuthenticationTimeoutInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[OrganizationConfigSchema.ActivityBasedAuthenticationTimeoutInterval];
			}
		}

		// Token: 0x17001F0B RID: 7947
		// (get) Token: 0x06005A39 RID: 23097 RVA: 0x0013D499 File Offset: 0x0013B699
		public bool ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled
		{
			get
			{
				return !(bool)this[OrganizationConfigSchema.ActivityBasedAuthenticationTimeoutWithSingleSignOnDisabled];
			}
		}

		// Token: 0x17001F0C RID: 7948
		// (get) Token: 0x06005A3A RID: 23098 RVA: 0x0013D4AE File Offset: 0x0013B6AE
		public bool AppsForOfficeEnabled
		{
			get
			{
				return !(bool)this[OrganizationConfigSchema.AppsForOfficeDisabled];
			}
		}

		// Token: 0x17001F0D RID: 7949
		// (get) Token: 0x06005A3B RID: 23099 RVA: 0x0013D4C3 File Offset: 0x0013B6C3
		public ProtocolConnectionSettings AVAuthenticationService
		{
			get
			{
				return (ProtocolConnectionSettings)this[OrganizationConfigSchema.AVAuthenticationService];
			}
		}

		// Token: 0x17001F0E RID: 7950
		// (get) Token: 0x06005A3C RID: 23100 RVA: 0x0013D4D5 File Offset: 0x0013B6D5
		public bool? CustomerFeedbackEnabled
		{
			get
			{
				return (bool?)this[OrganizationConfigSchema.CustomerFeedbackEnabled];
			}
		}

		// Token: 0x17001F0F RID: 7951
		// (get) Token: 0x06005A3D RID: 23101 RVA: 0x0013D4E7 File Offset: 0x0013B6E7
		public ADObjectId DistributionGroupDefaultOU
		{
			get
			{
				return (ADObjectId)this[OrganizationConfigSchema.DistributionGroupDefaultOU];
			}
		}

		// Token: 0x17001F10 RID: 7952
		// (get) Token: 0x06005A3E RID: 23102 RVA: 0x0013D4F9 File Offset: 0x0013B6F9
		public MultiValuedProperty<string> DistributionGroupNameBlockedWordsList
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationConfigSchema.DistributionGroupNameBlockedWordsList];
			}
		}

		// Token: 0x17001F11 RID: 7953
		// (get) Token: 0x06005A3F RID: 23103 RVA: 0x0013D50B File Offset: 0x0013B70B
		public DistributionGroupNamingPolicy DistributionGroupNamingPolicy
		{
			get
			{
				return (DistributionGroupNamingPolicy)this[OrganizationConfigSchema.DistributionGroupNamingPolicy];
			}
		}

		// Token: 0x17001F12 RID: 7954
		// (get) Token: 0x06005A40 RID: 23104 RVA: 0x0013D51D File Offset: 0x0013B71D
		public bool? EwsAllowEntourage
		{
			get
			{
				return (bool?)this[OrganizationConfigSchema.EwsAllowEntourage];
			}
		}

		// Token: 0x17001F13 RID: 7955
		// (get) Token: 0x06005A41 RID: 23105 RVA: 0x0013D530 File Offset: 0x0013B730
		public MultiValuedProperty<string> EwsAllowList
		{
			get
			{
				if ((EwsApplicationAccessPolicy?)this[OrganizationConfigSchema.EwsApplicationAccessPolicy] == Microsoft.Exchange.Data.Directory.EwsApplicationAccessPolicy.EnforceAllowList)
				{
					return (MultiValuedProperty<string>)this[OrganizationConfigSchema.EwsExceptions];
				}
				return null;
			}
		}

		// Token: 0x17001F14 RID: 7956
		// (get) Token: 0x06005A42 RID: 23106 RVA: 0x0013D577 File Offset: 0x0013B777
		public bool? EwsAllowMacOutlook
		{
			get
			{
				return (bool?)this[OrganizationConfigSchema.EwsAllowMacOutlook];
			}
		}

		// Token: 0x17001F15 RID: 7957
		// (get) Token: 0x06005A43 RID: 23107 RVA: 0x0013D589 File Offset: 0x0013B789
		public bool? EwsAllowOutlook
		{
			get
			{
				return (bool?)this[OrganizationConfigSchema.EwsAllowOutlook];
			}
		}

		// Token: 0x17001F16 RID: 7958
		// (get) Token: 0x06005A44 RID: 23108 RVA: 0x0013D59B File Offset: 0x0013B79B
		public EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
		{
			get
			{
				return (EwsApplicationAccessPolicy?)this[OrganizationConfigSchema.EwsApplicationAccessPolicy];
			}
		}

		// Token: 0x17001F17 RID: 7959
		// (get) Token: 0x06005A45 RID: 23109 RVA: 0x0013D5B0 File Offset: 0x0013B7B0
		public MultiValuedProperty<string> EwsBlockList
		{
			get
			{
				if ((EwsApplicationAccessPolicy?)this[OrganizationConfigSchema.EwsApplicationAccessPolicy] == Microsoft.Exchange.Data.Directory.EwsApplicationAccessPolicy.EnforceBlockList)
				{
					return (MultiValuedProperty<string>)this[OrganizationConfigSchema.EwsExceptions];
				}
				return null;
			}
		}

		// Token: 0x17001F18 RID: 7960
		// (get) Token: 0x06005A46 RID: 23110 RVA: 0x0013D5F8 File Offset: 0x0013B7F8
		public bool? EwsEnabled
		{
			get
			{
				return CASMailboxHelper.ToBooleanNullable((int?)this[OrganizationConfigSchema.EwsEnabled]);
			}
		}

		// Token: 0x17001F19 RID: 7961
		// (get) Token: 0x06005A47 RID: 23111 RVA: 0x0013D60F File Offset: 0x0013B80F
		public bool ExchangeNotificationEnabled
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.ExchangeNotificationEnabled];
			}
		}

		// Token: 0x17001F1A RID: 7962
		// (get) Token: 0x06005A48 RID: 23112 RVA: 0x0013D621 File Offset: 0x0013B821
		public MultiValuedProperty<SmtpAddress> ExchangeNotificationRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[OrganizationConfigSchema.ExchangeNotificationRecipients];
			}
		}

		// Token: 0x17001F1B RID: 7963
		// (get) Token: 0x06005A49 RID: 23113 RVA: 0x0013D633 File Offset: 0x0013B833
		public ADObjectId HierarchicalAddressBookRoot
		{
			get
			{
				return (ADObjectId)this[OrganizationConfigSchema.HABRootDepartmentLink];
			}
		}

		// Token: 0x17001F1C RID: 7964
		// (get) Token: 0x06005A4A RID: 23114 RVA: 0x0013D645 File Offset: 0x0013B845
		public IndustryType Industry
		{
			get
			{
				return (IndustryType)this[OrganizationConfigSchema.Industry];
			}
		}

		// Token: 0x17001F1D RID: 7965
		// (get) Token: 0x06005A4B RID: 23115 RVA: 0x0013D657 File Offset: 0x0013B857
		public bool MailTipsAllTipsEnabled
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.MailTipsAllTipsEnabled];
			}
		}

		// Token: 0x17001F1E RID: 7966
		// (get) Token: 0x06005A4C RID: 23116 RVA: 0x0013D669 File Offset: 0x0013B869
		public bool MailTipsExternalRecipientsTipsEnabled
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.MailTipsExternalRecipientsTipsEnabled];
			}
		}

		// Token: 0x17001F1F RID: 7967
		// (get) Token: 0x06005A4D RID: 23117 RVA: 0x0013D67B File Offset: 0x0013B87B
		public bool MailTipsGroupMetricsEnabled
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.MailTipsGroupMetricsEnabled];
			}
		}

		// Token: 0x17001F20 RID: 7968
		// (get) Token: 0x06005A4E RID: 23118 RVA: 0x0013D68D File Offset: 0x0013B88D
		public uint MailTipsLargeAudienceThreshold
		{
			get
			{
				return (uint)((long)this[OrganizationConfigSchema.MailTipsLargeAudienceThreshold]);
			}
		}

		// Token: 0x17001F21 RID: 7969
		// (get) Token: 0x06005A4F RID: 23119 RVA: 0x0013D6A0 File Offset: 0x0013B8A0
		public bool MailTipsMailboxSourcedTipsEnabled
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.MailTipsMailboxSourcedTipsEnabled];
			}
		}

		// Token: 0x17001F22 RID: 7970
		// (get) Token: 0x06005A50 RID: 23120 RVA: 0x0013D6B2 File Offset: 0x0013B8B2
		public string ManagedFolderHomepage
		{
			get
			{
				return (string)this[OrganizationConfigSchema.ManagedFolderHomepage];
			}
		}

		// Token: 0x17001F23 RID: 7971
		// (get) Token: 0x06005A51 RID: 23121 RVA: 0x0013D6C4 File Offset: 0x0013B8C4
		public ProxyAddressCollection MicrosoftExchangeRecipientEmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[OrganizationConfigSchema.MicrosoftExchangeRecipientEmailAddresses];
			}
		}

		// Token: 0x17001F24 RID: 7972
		// (get) Token: 0x06005A52 RID: 23122 RVA: 0x0013D6D6 File Offset: 0x0013B8D6
		public bool MicrosoftExchangeRecipientEmailAddressPolicyEnabled
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.MicrosoftExchangeRecipientEmailAddressPolicyEnabled];
			}
		}

		// Token: 0x17001F25 RID: 7973
		// (get) Token: 0x06005A53 RID: 23123 RVA: 0x0013D6E8 File Offset: 0x0013B8E8
		public SmtpAddress MicrosoftExchangeRecipientPrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[OrganizationConfigSchema.MicrosoftExchangeRecipientPrimarySmtpAddress];
			}
		}

		// Token: 0x17001F26 RID: 7974
		// (get) Token: 0x06005A54 RID: 23124 RVA: 0x0013D6FA File Offset: 0x0013B8FA
		public ADObjectId MicrosoftExchangeRecipientReplyRecipient
		{
			get
			{
				return (ADObjectId)this[OrganizationConfigSchema.MicrosoftExchangeRecipientReplyRecipient];
			}
		}

		// Token: 0x17001F27 RID: 7975
		// (get) Token: 0x06005A55 RID: 23125 RVA: 0x0013D70C File Offset: 0x0013B90C
		public bool ForwardSyncLiveIdBusinessInstance
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.ForwardSyncLiveIdBusinessInstance];
			}
		}

		// Token: 0x17001F28 RID: 7976
		// (get) Token: 0x06005A56 RID: 23126 RVA: 0x0013D71E File Offset: 0x0013B91E
		public MultiValuedProperty<OrganizationSummaryEntry> OrganizationSummary
		{
			get
			{
				return (MultiValuedProperty<OrganizationSummaryEntry>)this[OrganizationConfigSchema.OrganizationSummary];
			}
		}

		// Token: 0x17001F29 RID: 7977
		// (get) Token: 0x06005A57 RID: 23127 RVA: 0x0013D730 File Offset: 0x0013B930
		public bool ReadTrackingEnabled
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.ReadTrackingEnabled];
			}
		}

		// Token: 0x17001F2A RID: 7978
		// (get) Token: 0x06005A58 RID: 23128 RVA: 0x0013D742 File Offset: 0x0013B942
		public int SCLJunkThreshold
		{
			get
			{
				return (int)this[OrganizationConfigSchema.SCLJunkThreshold];
			}
		}

		// Token: 0x17001F2B RID: 7979
		// (get) Token: 0x06005A59 RID: 23129 RVA: 0x0013D754 File Offset: 0x0013B954
		public ProtocolConnectionSettings SIPAccessService
		{
			get
			{
				return (ProtocolConnectionSettings)this[OrganizationConfigSchema.SIPAccessService];
			}
		}

		// Token: 0x17001F2C RID: 7980
		// (get) Token: 0x06005A5A RID: 23130 RVA: 0x0013D766 File Offset: 0x0013B966
		public ProtocolConnectionSettings SIPSessionBorderController
		{
			get
			{
				return (ProtocolConnectionSettings)this[OrganizationConfigSchema.SIPSessionBorderController];
			}
		}

		// Token: 0x17001F2D RID: 7981
		// (get) Token: 0x06005A5B RID: 23131 RVA: 0x0013D778 File Offset: 0x0013B978
		public Unlimited<int> MaxConcurrentMigrations
		{
			get
			{
				return (Unlimited<int>)(this[OrganizationConfigSchema.MaxConcurrentMigrations] ?? Unlimited<int>.UnlimitedValue);
			}
		}

		// Token: 0x17001F2E RID: 7982
		// (get) Token: 0x06005A5C RID: 23132 RVA: 0x0013D798 File Offset: 0x0013B998
		public int? MaxAddressBookPolicies
		{
			get
			{
				return (int?)this[OrganizationConfigSchema.MaxAddressBookPolicies];
			}
		}

		// Token: 0x17001F2F RID: 7983
		// (get) Token: 0x06005A5D RID: 23133 RVA: 0x0013D7AA File Offset: 0x0013B9AA
		public int? MaxOfflineAddressBooks
		{
			get
			{
				return (int?)this[OrganizationConfigSchema.MaxOfflineAddressBooks];
			}
		}

		// Token: 0x17001F30 RID: 7984
		// (get) Token: 0x06005A5E RID: 23134 RVA: 0x0013D7BC File Offset: 0x0013B9BC
		public bool IsExcludedFromOnboardMigration
		{
			get
			{
				return (bool)(this[OrganizationConfigSchema.IsExcludedFromOnboardMigration] ?? false);
			}
		}

		// Token: 0x17001F31 RID: 7985
		// (get) Token: 0x06005A5F RID: 23135 RVA: 0x0013D7D8 File Offset: 0x0013B9D8
		public bool IsExcludedFromOffboardMigration
		{
			get
			{
				return (bool)(this[OrganizationConfigSchema.IsExcludedFromOffboardMigration] ?? false);
			}
		}

		// Token: 0x17001F32 RID: 7986
		// (get) Token: 0x06005A60 RID: 23136 RVA: 0x0013D7F4 File Offset: 0x0013B9F4
		public bool IsFfoMigrationInProgress
		{
			get
			{
				return (bool)(this[OrganizationConfigSchema.IsFfoMigrationInProgress] ?? false);
			}
		}

		// Token: 0x17001F33 RID: 7987
		// (get) Token: 0x06005A61 RID: 23137 RVA: 0x0013D810 File Offset: 0x0013BA10
		public bool IsProcessEhaMigratedMessagesEnabled
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.IsProcessEhaMigratedMessagesEnabled];
			}
		}

		// Token: 0x17001F34 RID: 7988
		// (get) Token: 0x06005A62 RID: 23138 RVA: 0x0013D822 File Offset: 0x0013BA22
		public bool TenantRelocationsAllowed
		{
			get
			{
				return (bool)(this[OrganizationSchema.TenantRelocationsAllowed] ?? false);
			}
		}

		// Token: 0x17001F35 RID: 7989
		// (get) Token: 0x06005A63 RID: 23139 RVA: 0x0013D83E File Offset: 0x0013BA3E
		public bool ACLableSyncedObjectEnabled
		{
			get
			{
				return (bool)(this[OrganizationSchema.ACLableSyncedObjectEnabled] ?? false);
			}
		}

		// Token: 0x17001F36 RID: 7990
		// (get) Token: 0x06005A64 RID: 23140 RVA: 0x0013D85A File Offset: 0x0013BA5A
		public int PreferredInternetCodePageForShiftJis
		{
			get
			{
				return Organization.MapIntToPreferredInternetCodePageForShiftJis((int)this[OrganizationConfigSchema.PreferredInternetCodePageForShiftJis]);
			}
		}

		// Token: 0x17001F37 RID: 7991
		// (get) Token: 0x06005A65 RID: 23141 RVA: 0x0013D871 File Offset: 0x0013BA71
		public int RequiredCharsetCoverage
		{
			get
			{
				return (int)this[OrganizationConfigSchema.RequiredCharsetCoverage];
			}
		}

		// Token: 0x17001F38 RID: 7992
		// (get) Token: 0x06005A66 RID: 23142 RVA: 0x0013D883 File Offset: 0x0013BA83
		public int ByteEncoderTypeFor7BitCharsets
		{
			get
			{
				return (int)this[OrganizationConfigSchema.ByteEncoderTypeFor7BitCharsets];
			}
		}

		// Token: 0x17001F39 RID: 7993
		// (get) Token: 0x06005A67 RID: 23143 RVA: 0x0013D895 File Offset: 0x0013BA95
		public bool PublicComputersDetectionEnabled
		{
			get
			{
				return (bool)(this[OrganizationConfigSchema.PublicComputersDetectionEnabled] ?? false);
			}
		}

		// Token: 0x17001F3A RID: 7994
		// (get) Token: 0x06005A68 RID: 23144 RVA: 0x0013D8B1 File Offset: 0x0013BAB1
		public RmsoSubscriptionStatusFlags RmsoSubscriptionStatus
		{
			get
			{
				return (RmsoSubscriptionStatusFlags)this[OrganizationConfigSchema.RmsoSubscriptionStatus];
			}
		}

		// Token: 0x17001F3B RID: 7995
		// (get) Token: 0x06005A69 RID: 23145 RVA: 0x0013D8C3 File Offset: 0x0013BAC3
		public bool IntuneManagedStatus
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.IntuneManagedStatus];
			}
		}

		// Token: 0x17001F3C RID: 7996
		// (get) Token: 0x06005A6A RID: 23146 RVA: 0x0013D8D5 File Offset: 0x0013BAD5
		public HybridConfigurationStatusFlags HybridConfigurationStatus
		{
			get
			{
				return (HybridConfigurationStatusFlags)this[OrganizationConfigSchema.HybridConfigurationStatus];
			}
		}

		// Token: 0x17001F3D RID: 7997
		// (get) Token: 0x06005A6B RID: 23147 RVA: 0x0013D8E7 File Offset: 0x0013BAE7
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return (ReleaseTrack?)this[OrganizationConfigSchema.ReleaseTrack];
			}
		}

		// Token: 0x17001F3E RID: 7998
		// (get) Token: 0x06005A6C RID: 23148 RVA: 0x0013D8F9 File Offset: 0x0013BAF9
		public Uri SharePointUrl
		{
			get
			{
				return (Uri)this[OrganizationConfigSchema.SharePointUrl];
			}
		}

		// Token: 0x17001F3F RID: 7999
		// (get) Token: 0x06005A6D RID: 23149 RVA: 0x0013D90B File Offset: 0x0013BB0B
		public bool MapiHttpEnabled
		{
			get
			{
				return (bool)(this[OrganizationConfigSchema.MapiHttpEnabled] ?? false);
			}
		}

		// Token: 0x17001F40 RID: 8000
		// (get) Token: 0x06005A6E RID: 23150 RVA: 0x0013D927 File Offset: 0x0013BB27
		public bool OAuth2ClientProfileEnabled
		{
			get
			{
				return (bool)(this[OrganizationConfigSchema.OAuth2ClientProfileEnabled] ?? false);
			}
		}

		// Token: 0x17001F41 RID: 8001
		// (get) Token: 0x06005A6F RID: 23151 RVA: 0x0013D944 File Offset: 0x0013BB44
		public string OrganizationConfigHash
		{
			get
			{
				if (!this.valuesQueriedFromDC)
				{
					return string.Empty;
				}
				string value = this.PreviousAdminDisplayVersion.ToString();
				StringBuilder stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
				{
					xmlWriter.WriteStartDocument();
					xmlWriter.WriteStartElement("DCHybridConfiguration");
					xmlWriter.WriteElementString("IsDataCenter", this.valuesQueriedFromDC.ToString());
					xmlWriter.WriteElementString("AdminDisplayVersion", this.AdminDisplayVersion.ToString());
					xmlWriter.WriteElementString("IsUpgradingOrganization", this.IsUpgradingOrganization.ToString());
					xmlWriter.WriteElementString("PreviousAdminDisplayVersion", value);
					xmlWriter.WriteStartElement("AcceptedDomain");
					if (this.AcceptedDomainNames == null || this.AcceptedDomainNames.ToArray().Length == 0)
					{
						return string.Empty;
					}
					foreach (string value2 in this.AcceptedDomainNames.ToArray())
					{
						xmlWriter.WriteElementString("DomainName", value2);
					}
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndDocument();
				}
				byte[] inArray;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress))
					{
						using (StreamWriter streamWriter = new StreamWriter(deflateStream, Encoding.UTF8))
						{
							streamWriter.Write(stringBuilder.ToString());
						}
					}
					inArray = memoryStream.ToArray();
				}
				return Convert.ToBase64String(inArray);
			}
		}

		// Token: 0x17001F42 RID: 8002
		// (get) Token: 0x06005A70 RID: 23152 RVA: 0x0013DAFC File Offset: 0x0013BCFC
		public string LegacyExchangeDN
		{
			get
			{
				return (string)this[OrganizationConfigSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x17001F43 RID: 8003
		// (get) Token: 0x06005A71 RID: 23153 RVA: 0x0013DB0E File Offset: 0x0013BD0E
		public HeuristicsFlags Heuristics
		{
			get
			{
				return (HeuristicsFlags)this[OrganizationConfigSchema.Heuristics];
			}
		}

		// Token: 0x17001F44 RID: 8004
		// (get) Token: 0x06005A72 RID: 23154 RVA: 0x0013DB20 File Offset: 0x0013BD20
		public MultiValuedProperty<ADObjectId> ResourceAddressLists
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[OrganizationConfigSchema.ResourceAddressLists];
			}
		}

		// Token: 0x17001F45 RID: 8005
		// (get) Token: 0x06005A73 RID: 23155 RVA: 0x0013DB32 File Offset: 0x0013BD32
		public bool IsMixedMode
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.IsMixedMode];
			}
		}

		// Token: 0x17001F46 RID: 8006
		// (get) Token: 0x06005A74 RID: 23156 RVA: 0x0013DB44 File Offset: 0x0013BD44
		public ExchangeObjectVersion PreviousAdminDisplayVersion
		{
			get
			{
				MailboxRelease mailboxRelease;
				if (!Enum.TryParse<MailboxRelease>((string)this[OrganizationConfigSchema.PreviousAdminDisplayVersion], true, out mailboxRelease))
				{
					mailboxRelease = MailboxRelease.E14;
				}
				if (mailboxRelease == MailboxRelease.E15)
				{
					return ExchangeObjectVersion.Exchange2012;
				}
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001F47 RID: 8007
		// (get) Token: 0x06005A75 RID: 23157 RVA: 0x0013DB7E File Offset: 0x0013BD7E
		public bool IsAddressListPagingEnabled
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.IsAddressListPagingEnabled];
			}
		}

		// Token: 0x17001F48 RID: 8008
		// (get) Token: 0x06005A76 RID: 23158 RVA: 0x0013DB90 File Offset: 0x0013BD90
		public MultiValuedProperty<string> ForeignForestFQDN
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationConfigSchema.ForeignForestFQDN];
			}
		}

		// Token: 0x17001F49 RID: 8009
		// (get) Token: 0x06005A77 RID: 23159 RVA: 0x0013DBA2 File Offset: 0x0013BDA2
		public SecurityIdentifier ForeignForestOrgAdminUSGSid
		{
			get
			{
				return (SecurityIdentifier)this[OrganizationConfigSchema.ForeignForestOrgAdminUSGSid];
			}
		}

		// Token: 0x17001F4A RID: 8010
		// (get) Token: 0x06005A78 RID: 23160 RVA: 0x0013DBB4 File Offset: 0x0013BDB4
		public SecurityIdentifier ForeignForestRecipientAdminUSGSid
		{
			get
			{
				return (SecurityIdentifier)this[OrganizationConfigSchema.ForeignForestRecipientAdminUSGSid];
			}
		}

		// Token: 0x17001F4B RID: 8011
		// (get) Token: 0x06005A79 RID: 23161 RVA: 0x0013DBC6 File Offset: 0x0013BDC6
		public SecurityIdentifier ForeignForestViewOnlyAdminUSGSid
		{
			get
			{
				return (SecurityIdentifier)this[OrganizationConfigSchema.ForeignForestViewOnlyAdminUSGSid];
			}
		}

		// Token: 0x17001F4C RID: 8012
		// (get) Token: 0x06005A7A RID: 23162 RVA: 0x0013DBD8 File Offset: 0x0013BDD8
		public MultiValuedProperty<string> MimeTypes
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationConfigSchema.MimeTypes];
			}
		}

		// Token: 0x17001F4D RID: 8013
		// (get) Token: 0x06005A7B RID: 23163 RVA: 0x0013DBEA File Offset: 0x0013BDEA
		public bool IsLicensingEnforced
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.IsLicensingEnforced];
			}
		}

		// Token: 0x17001F4E RID: 8014
		// (get) Token: 0x06005A7C RID: 23164 RVA: 0x0013DBFC File Offset: 0x0013BDFC
		public bool IsTenantAccessBlocked
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.IsTenantAccessBlocked];
			}
		}

		// Token: 0x17001F4F RID: 8015
		// (get) Token: 0x06005A7D RID: 23165 RVA: 0x0013DC0E File Offset: 0x0013BE0E
		public bool IsDehydrated
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.IsDehydrated];
			}
		}

		// Token: 0x17001F50 RID: 8016
		// (get) Token: 0x06005A7E RID: 23166 RVA: 0x0013DC20 File Offset: 0x0013BE20
		public bool IsGuidPrefixedLegacyDnDisabled
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.IsGuidPrefixedLegacyDnDisabled];
			}
		}

		// Token: 0x17001F51 RID: 8017
		// (get) Token: 0x06005A7F RID: 23167 RVA: 0x0013DC32 File Offset: 0x0013BE32
		public bool IsMailboxForcedReplicationDisabled
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.IsMailboxForcedReplicationDisabled];
			}
		}

		// Token: 0x17001F52 RID: 8018
		// (get) Token: 0x06005A80 RID: 23168 RVA: 0x0013DC44 File Offset: 0x0013BE44
		public ExchangeObjectVersion RBACConfigurationVersion
		{
			get
			{
				return (ExchangeObjectVersion)this[OrganizationConfigSchema.RBACConfigurationVersion];
			}
		}

		// Token: 0x17001F53 RID: 8019
		// (get) Token: 0x06005A81 RID: 23169 RVA: 0x0013DC56 File Offset: 0x0013BE56
		public bool IsSyncPropertySetUpgradeAllowed
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.IsSyncPropertySetUpgradeAllowed];
			}
		}

		// Token: 0x17001F54 RID: 8020
		// (get) Token: 0x06005A82 RID: 23170 RVA: 0x0013DC68 File Offset: 0x0013BE68
		public PublicFolderInformation RootPublicFolderMailbox
		{
			get
			{
				return (PublicFolderInformation)this[OrganizationConfigSchema.DefaultPublicFolderMailbox];
			}
		}

		// Token: 0x17001F55 RID: 8021
		// (get) Token: 0x06005A83 RID: 23171 RVA: 0x0013DC7A File Offset: 0x0013BE7A
		public MultiValuedProperty<ADObjectId> RemotePublicFolderMailboxes
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[OrganizationConfigSchema.RemotePublicFolderMailboxes];
			}
		}

		// Token: 0x17001F56 RID: 8022
		// (get) Token: 0x06005A84 RID: 23172 RVA: 0x0013DC8C File Offset: 0x0013BE8C
		public ExchangeObjectVersion AdminDisplayVersion
		{
			get
			{
				return (ExchangeObjectVersion)this[OrganizationConfigSchema.AdminDisplayVersion];
			}
		}

		// Token: 0x17001F57 RID: 8023
		// (get) Token: 0x06005A85 RID: 23173 RVA: 0x0013DC9E File Offset: 0x0013BE9E
		public bool IsUpgradingOrganization
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.IsUpgradingOrganization];
			}
		}

		// Token: 0x17001F58 RID: 8024
		// (get) Token: 0x06005A86 RID: 23174 RVA: 0x0013DCB0 File Offset: 0x0013BEB0
		public bool IsUpdatingServicePlan
		{
			get
			{
				return (bool)this[OrganizationConfigSchema.IsUpdatingServicePlan];
			}
		}

		// Token: 0x17001F59 RID: 8025
		// (get) Token: 0x06005A87 RID: 23175 RVA: 0x0013DCC2 File Offset: 0x0013BEC2
		public string ServicePlan
		{
			get
			{
				return (string)this[OrganizationConfigSchema.ServicePlan];
			}
		}

		// Token: 0x17001F5A RID: 8026
		// (get) Token: 0x06005A88 RID: 23176 RVA: 0x0013DCD4 File Offset: 0x0013BED4
		public string TargetServicePlan
		{
			get
			{
				return (string)this[OrganizationConfigSchema.TargetServicePlan];
			}
		}

		// Token: 0x17001F5B RID: 8027
		// (get) Token: 0x06005A89 RID: 23177 RVA: 0x0013DCE6 File Offset: 0x0013BEE6
		public string WACDiscoveryEndpoint
		{
			get
			{
				return (string)this[OrganizationConfigSchema.WACDiscoveryEndpoint];
			}
		}

		// Token: 0x17001F5C RID: 8028
		// (get) Token: 0x06005A8A RID: 23178 RVA: 0x0013DCF8 File Offset: 0x0013BEF8
		public MultiValuedProperty<UMLanguage> UMAvailableLanguages
		{
			get
			{
				return (MultiValuedProperty<UMLanguage>)this[OrganizationConfigSchema.UMAvailableLanguages];
			}
		}

		// Token: 0x17001F5D RID: 8029
		// (get) Token: 0x06005A8B RID: 23179 RVA: 0x0013DD0C File Offset: 0x0013BF0C
		public string AdfsAuthenticationConfiguration
		{
			get
			{
				string result = null;
				AdfsAuthenticationConfig.TryDecode((string)this[OrganizationSchema.AdfsAuthenticationRawConfiguration], out result);
				return result;
			}
		}

		// Token: 0x17001F5E RID: 8030
		// (get) Token: 0x06005A8C RID: 23180 RVA: 0x0013DD34 File Offset: 0x0013BF34
		public Uri AdfsIssuer
		{
			get
			{
				return (Uri)this[OrganizationConfigSchema.AdfsIssuer];
			}
		}

		// Token: 0x17001F5F RID: 8031
		// (get) Token: 0x06005A8D RID: 23181 RVA: 0x0013DD46 File Offset: 0x0013BF46
		public MultiValuedProperty<Uri> AdfsAudienceUris
		{
			get
			{
				return (MultiValuedProperty<Uri>)this[OrganizationConfigSchema.AdfsAudienceUris];
			}
		}

		// Token: 0x17001F60 RID: 8032
		// (get) Token: 0x06005A8E RID: 23182 RVA: 0x0013DD58 File Offset: 0x0013BF58
		public MultiValuedProperty<string> AdfsSignCertificateThumbprints
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationConfigSchema.AdfsSignCertificateThumbprints];
			}
		}

		// Token: 0x17001F61 RID: 8033
		// (get) Token: 0x06005A8F RID: 23183 RVA: 0x0013DD6A File Offset: 0x0013BF6A
		public string AdfsEncryptCertificateThumbprint
		{
			get
			{
				return (string)this[OrganizationConfigSchema.AdfsEncryptCertificateThumbprint];
			}
		}

		// Token: 0x17001F62 RID: 8034
		// (get) Token: 0x06005A90 RID: 23184 RVA: 0x0013DD7C File Offset: 0x0013BF7C
		public Uri SiteMailboxCreationURL
		{
			get
			{
				return (Uri)this[OrganizationConfigSchema.SiteMailboxCreationURL];
			}
		}

		// Token: 0x04003CCA RID: 15562
		private readonly bool valuesQueriedFromDC;

		// Token: 0x04003CCB RID: 15563
		private static readonly OrganizationConfigSchema schema = ObjectSchema.GetInstance<OrganizationConfigSchema>();
	}
}
