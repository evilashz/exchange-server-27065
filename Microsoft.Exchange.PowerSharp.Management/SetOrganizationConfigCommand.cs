using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200081E RID: 2078
	public class SetOrganizationConfigCommand : SyntheticCommandWithPipelineInputNoOutput<ADOrganizationConfig>
	{
		// Token: 0x0600667D RID: 26237 RVA: 0x0009C47F File Offset: 0x0009A67F
		private SetOrganizationConfigCommand() : base("Set-OrganizationConfig")
		{
		}

		// Token: 0x0600667E RID: 26238 RVA: 0x0009C48C File Offset: 0x0009A68C
		public SetOrganizationConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x0009C49B File Offset: 0x0009A69B
		public virtual SetOrganizationConfigCommand SetParameters(SetOrganizationConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006680 RID: 26240 RVA: 0x0009C4A5 File Offset: 0x0009A6A5
		public virtual SetOrganizationConfigCommand SetParameters(SetOrganizationConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x0009C4AF File Offset: 0x0009A6AF
		public virtual SetOrganizationConfigCommand SetParameters(SetOrganizationConfigCommand.AdfsAuthenticationRawConfigurationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006682 RID: 26242 RVA: 0x0009C4B9 File Offset: 0x0009A6B9
		public virtual SetOrganizationConfigCommand SetParameters(SetOrganizationConfigCommand.AdfsAuthenticationParameterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200081F RID: 2079
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170041FA RID: 16890
			// (set) Token: 0x06006683 RID: 26243 RVA: 0x0009C4C3 File Offset: 0x0009A6C3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170041FB RID: 16891
			// (set) Token: 0x06006684 RID: 26244 RVA: 0x0009C4E1 File Offset: 0x0009A6E1
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170041FC RID: 16892
			// (set) Token: 0x06006685 RID: 26245 RVA: 0x0009C4F4 File Offset: 0x0009A6F4
			public virtual string MicrosoftExchangeRecipientReplyRecipient
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientReplyRecipient"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170041FD RID: 16893
			// (set) Token: 0x06006686 RID: 26246 RVA: 0x0009C512 File Offset: 0x0009A712
			public virtual string HierarchicalAddressBookRoot
			{
				set
				{
					base.PowerSharpParameters["HierarchicalAddressBookRoot"] = ((value != null) ? new UserContactGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170041FE RID: 16894
			// (set) Token: 0x06006687 RID: 26247 RVA: 0x0009C530 File Offset: 0x0009A730
			public virtual string DistributionGroupDefaultOU
			{
				set
				{
					base.PowerSharpParameters["DistributionGroupDefaultOU"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170041FF RID: 16895
			// (set) Token: 0x06006688 RID: 26248 RVA: 0x0009C54E File Offset: 0x0009A74E
			public virtual MultiValuedProperty<RecipientIdParameter> ExchangeNotificationRecipients
			{
				set
				{
					base.PowerSharpParameters["ExchangeNotificationRecipients"] = value;
				}
			}

			// Token: 0x17004200 RID: 16896
			// (set) Token: 0x06006689 RID: 26249 RVA: 0x0009C561 File Offset: 0x0009A761
			public virtual MultiValuedProperty<MailboxOrMailUserIdParameter> RemotePublicFolderMailboxes
			{
				set
				{
					base.PowerSharpParameters["RemotePublicFolderMailboxes"] = value;
				}
			}

			// Token: 0x17004201 RID: 16897
			// (set) Token: 0x0600668A RID: 26250 RVA: 0x0009C574 File Offset: 0x0009A774
			public virtual int SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17004202 RID: 16898
			// (set) Token: 0x0600668B RID: 26251 RVA: 0x0009C58C File Offset: 0x0009A78C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004203 RID: 16899
			// (set) Token: 0x0600668C RID: 26252 RVA: 0x0009C59F File Offset: 0x0009A79F
			public virtual bool PublicFoldersLockedForMigration
			{
				set
				{
					base.PowerSharpParameters["PublicFoldersLockedForMigration"] = value;
				}
			}

			// Token: 0x17004204 RID: 16900
			// (set) Token: 0x0600668D RID: 26253 RVA: 0x0009C5B7 File Offset: 0x0009A7B7
			public virtual bool PublicFolderMigrationComplete
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMigrationComplete"] = value;
				}
			}

			// Token: 0x17004205 RID: 16901
			// (set) Token: 0x0600668E RID: 26254 RVA: 0x0009C5CF File Offset: 0x0009A7CF
			public virtual bool PublicFolderMailboxesLockedForNewConnections
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMailboxesLockedForNewConnections"] = value;
				}
			}

			// Token: 0x17004206 RID: 16902
			// (set) Token: 0x0600668F RID: 26255 RVA: 0x0009C5E7 File Offset: 0x0009A7E7
			public virtual bool PublicFolderMailboxesMigrationComplete
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMailboxesMigrationComplete"] = value;
				}
			}

			// Token: 0x17004207 RID: 16903
			// (set) Token: 0x06006690 RID: 26256 RVA: 0x0009C5FF File Offset: 0x0009A7FF
			public virtual bool PublicComputersDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicComputersDetectionEnabled"] = value;
				}
			}

			// Token: 0x17004208 RID: 16904
			// (set) Token: 0x06006691 RID: 26257 RVA: 0x0009C617 File Offset: 0x0009A817
			public virtual RmsoSubscriptionStatusFlags RmsoSubscriptionStatus
			{
				set
				{
					base.PowerSharpParameters["RmsoSubscriptionStatus"] = value;
				}
			}

			// Token: 0x17004209 RID: 16905
			// (set) Token: 0x06006692 RID: 26258 RVA: 0x0009C62F File Offset: 0x0009A82F
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700420A RID: 16906
			// (set) Token: 0x06006693 RID: 26259 RVA: 0x0009C647 File Offset: 0x0009A847
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x1700420B RID: 16907
			// (set) Token: 0x06006694 RID: 26260 RVA: 0x0009C65A File Offset: 0x0009A85A
			public virtual Uri SiteMailboxCreationURL
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxCreationURL"] = value;
				}
			}

			// Token: 0x1700420C RID: 16908
			// (set) Token: 0x06006695 RID: 26261 RVA: 0x0009C66D File Offset: 0x0009A86D
			public virtual bool? CustomerFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["CustomerFeedbackEnabled"] = value;
				}
			}

			// Token: 0x1700420D RID: 16909
			// (set) Token: 0x06006696 RID: 26262 RVA: 0x0009C685 File Offset: 0x0009A885
			public virtual IndustryType Industry
			{
				set
				{
					base.PowerSharpParameters["Industry"] = value;
				}
			}

			// Token: 0x1700420E RID: 16910
			// (set) Token: 0x06006697 RID: 26263 RVA: 0x0009C69D File Offset: 0x0009A89D
			public virtual string ManagedFolderHomepage
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderHomepage"] = value;
				}
			}

			// Token: 0x1700420F RID: 16911
			// (set) Token: 0x06006698 RID: 26264 RVA: 0x0009C6B0 File Offset: 0x0009A8B0
			public virtual EnhancedTimeSpan? DefaultPublicFolderAgeLimit
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderAgeLimit"] = value;
				}
			}

			// Token: 0x17004210 RID: 16912
			// (set) Token: 0x06006699 RID: 26265 RVA: 0x0009C6C8 File Offset: 0x0009A8C8
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x17004211 RID: 16913
			// (set) Token: 0x0600669A RID: 26266 RVA: 0x0009C6E0 File Offset: 0x0009A8E0
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17004212 RID: 16914
			// (set) Token: 0x0600669B RID: 26267 RVA: 0x0009C6F8 File Offset: 0x0009A8F8
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x17004213 RID: 16915
			// (set) Token: 0x0600669C RID: 26268 RVA: 0x0009C710 File Offset: 0x0009A910
			public virtual EnhancedTimeSpan? DefaultPublicFolderDeletedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderDeletedItemRetention"] = value;
				}
			}

			// Token: 0x17004214 RID: 16916
			// (set) Token: 0x0600669D RID: 26269 RVA: 0x0009C728 File Offset: 0x0009A928
			public virtual EnhancedTimeSpan? DefaultPublicFolderMovedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMovedItemRetention"] = value;
				}
			}

			// Token: 0x17004215 RID: 16917
			// (set) Token: 0x0600669E RID: 26270 RVA: 0x0009C740 File Offset: 0x0009A940
			public virtual MultiValuedProperty<OrganizationSummaryEntry> OrganizationSummary
			{
				set
				{
					base.PowerSharpParameters["OrganizationSummary"] = value;
				}
			}

			// Token: 0x17004216 RID: 16918
			// (set) Token: 0x0600669F RID: 26271 RVA: 0x0009C753 File Offset: 0x0009A953
			public virtual bool ForwardSyncLiveIdBusinessInstance
			{
				set
				{
					base.PowerSharpParameters["ForwardSyncLiveIdBusinessInstance"] = value;
				}
			}

			// Token: 0x17004217 RID: 16919
			// (set) Token: 0x060066A0 RID: 26272 RVA: 0x0009C76B File Offset: 0x0009A96B
			public virtual ProxyAddressCollection MicrosoftExchangeRecipientEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientEmailAddresses"] = value;
				}
			}

			// Token: 0x17004218 RID: 16920
			// (set) Token: 0x060066A1 RID: 26273 RVA: 0x0009C77E File Offset: 0x0009A97E
			public virtual SmtpAddress MicrosoftExchangeRecipientPrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientPrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17004219 RID: 16921
			// (set) Token: 0x060066A2 RID: 26274 RVA: 0x0009C796 File Offset: 0x0009A996
			public virtual bool MicrosoftExchangeRecipientEmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientEmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x1700421A RID: 16922
			// (set) Token: 0x060066A3 RID: 26275 RVA: 0x0009C7AE File Offset: 0x0009A9AE
			public virtual bool MailTipsExternalRecipientsTipsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsExternalRecipientsTipsEnabled"] = value;
				}
			}

			// Token: 0x1700421B RID: 16923
			// (set) Token: 0x060066A4 RID: 26276 RVA: 0x0009C7C6 File Offset: 0x0009A9C6
			public virtual uint MailTipsLargeAudienceThreshold
			{
				set
				{
					base.PowerSharpParameters["MailTipsLargeAudienceThreshold"] = value;
				}
			}

			// Token: 0x1700421C RID: 16924
			// (set) Token: 0x060066A5 RID: 26277 RVA: 0x0009C7DE File Offset: 0x0009A9DE
			public virtual PublicFoldersDeployment PublicFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicFoldersEnabled"] = value;
				}
			}

			// Token: 0x1700421D RID: 16925
			// (set) Token: 0x060066A6 RID: 26278 RVA: 0x0009C7F6 File Offset: 0x0009A9F6
			public virtual bool MailTipsMailboxSourcedTipsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsMailboxSourcedTipsEnabled"] = value;
				}
			}

			// Token: 0x1700421E RID: 16926
			// (set) Token: 0x060066A7 RID: 26279 RVA: 0x0009C80E File Offset: 0x0009AA0E
			public virtual bool MailTipsGroupMetricsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsGroupMetricsEnabled"] = value;
				}
			}

			// Token: 0x1700421F RID: 16927
			// (set) Token: 0x060066A8 RID: 26280 RVA: 0x0009C826 File Offset: 0x0009AA26
			public virtual bool MailTipsAllTipsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsAllTipsEnabled"] = value;
				}
			}

			// Token: 0x17004220 RID: 16928
			// (set) Token: 0x060066A9 RID: 26281 RVA: 0x0009C83E File Offset: 0x0009AA3E
			public virtual bool ReadTrackingEnabled
			{
				set
				{
					base.PowerSharpParameters["ReadTrackingEnabled"] = value;
				}
			}

			// Token: 0x17004221 RID: 16929
			// (set) Token: 0x060066AA RID: 26282 RVA: 0x0009C856 File Offset: 0x0009AA56
			public virtual MultiValuedProperty<string> DistributionGroupNameBlockedWordsList
			{
				set
				{
					base.PowerSharpParameters["DistributionGroupNameBlockedWordsList"] = value;
				}
			}

			// Token: 0x17004222 RID: 16930
			// (set) Token: 0x060066AB RID: 26283 RVA: 0x0009C869 File Offset: 0x0009AA69
			public virtual DistributionGroupNamingPolicy DistributionGroupNamingPolicy
			{
				set
				{
					base.PowerSharpParameters["DistributionGroupNamingPolicy"] = value;
				}
			}

			// Token: 0x17004223 RID: 16931
			// (set) Token: 0x060066AC RID: 26284 RVA: 0x0009C87C File Offset: 0x0009AA7C
			public virtual ProtocolConnectionSettings AVAuthenticationService
			{
				set
				{
					base.PowerSharpParameters["AVAuthenticationService"] = value;
				}
			}

			// Token: 0x17004224 RID: 16932
			// (set) Token: 0x060066AD RID: 26285 RVA: 0x0009C88F File Offset: 0x0009AA8F
			public virtual ProtocolConnectionSettings SIPAccessService
			{
				set
				{
					base.PowerSharpParameters["SIPAccessService"] = value;
				}
			}

			// Token: 0x17004225 RID: 16933
			// (set) Token: 0x060066AE RID: 26286 RVA: 0x0009C8A2 File Offset: 0x0009AAA2
			public virtual ProtocolConnectionSettings SIPSessionBorderController
			{
				set
				{
					base.PowerSharpParameters["SIPSessionBorderController"] = value;
				}
			}

			// Token: 0x17004226 RID: 16934
			// (set) Token: 0x060066AF RID: 26287 RVA: 0x0009C8B5 File Offset: 0x0009AAB5
			public virtual bool ExchangeNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["ExchangeNotificationEnabled"] = value;
				}
			}

			// Token: 0x17004227 RID: 16935
			// (set) Token: 0x060066B0 RID: 26288 RVA: 0x0009C8CD File Offset: 0x0009AACD
			public virtual EnhancedTimeSpan ActivityBasedAuthenticationTimeoutInterval
			{
				set
				{
					base.PowerSharpParameters["ActivityBasedAuthenticationTimeoutInterval"] = value;
				}
			}

			// Token: 0x17004228 RID: 16936
			// (set) Token: 0x060066B1 RID: 26289 RVA: 0x0009C8E5 File Offset: 0x0009AAE5
			public virtual bool ActivityBasedAuthenticationTimeoutEnabled
			{
				set
				{
					base.PowerSharpParameters["ActivityBasedAuthenticationTimeoutEnabled"] = value;
				}
			}

			// Token: 0x17004229 RID: 16937
			// (set) Token: 0x060066B2 RID: 26290 RVA: 0x0009C8FD File Offset: 0x0009AAFD
			public virtual bool ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled
			{
				set
				{
					base.PowerSharpParameters["ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled"] = value;
				}
			}

			// Token: 0x1700422A RID: 16938
			// (set) Token: 0x060066B3 RID: 26291 RVA: 0x0009C915 File Offset: 0x0009AB15
			public virtual string WACDiscoveryEndpoint
			{
				set
				{
					base.PowerSharpParameters["WACDiscoveryEndpoint"] = value;
				}
			}

			// Token: 0x1700422B RID: 16939
			// (set) Token: 0x060066B4 RID: 26292 RVA: 0x0009C928 File Offset: 0x0009AB28
			public virtual bool IsExcludedFromOnboardMigration
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromOnboardMigration"] = value;
				}
			}

			// Token: 0x1700422C RID: 16940
			// (set) Token: 0x060066B5 RID: 26293 RVA: 0x0009C940 File Offset: 0x0009AB40
			public virtual bool IsExcludedFromOffboardMigration
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromOffboardMigration"] = value;
				}
			}

			// Token: 0x1700422D RID: 16941
			// (set) Token: 0x060066B6 RID: 26294 RVA: 0x0009C958 File Offset: 0x0009AB58
			public virtual bool IsFfoMigrationInProgress
			{
				set
				{
					base.PowerSharpParameters["IsFfoMigrationInProgress"] = value;
				}
			}

			// Token: 0x1700422E RID: 16942
			// (set) Token: 0x060066B7 RID: 26295 RVA: 0x0009C970 File Offset: 0x0009AB70
			public virtual bool TenantRelocationsAllowed
			{
				set
				{
					base.PowerSharpParameters["TenantRelocationsAllowed"] = value;
				}
			}

			// Token: 0x1700422F RID: 16943
			// (set) Token: 0x060066B8 RID: 26296 RVA: 0x0009C988 File Offset: 0x0009AB88
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x17004230 RID: 16944
			// (set) Token: 0x060066B9 RID: 26297 RVA: 0x0009C9A0 File Offset: 0x0009ABA0
			public virtual bool IsProcessEhaMigratedMessagesEnabled
			{
				set
				{
					base.PowerSharpParameters["IsProcessEhaMigratedMessagesEnabled"] = value;
				}
			}

			// Token: 0x17004231 RID: 16945
			// (set) Token: 0x060066BA RID: 26298 RVA: 0x0009C9B8 File Offset: 0x0009ABB8
			public virtual bool AppsForOfficeEnabled
			{
				set
				{
					base.PowerSharpParameters["AppsForOfficeEnabled"] = value;
				}
			}

			// Token: 0x17004232 RID: 16946
			// (set) Token: 0x060066BB RID: 26299 RVA: 0x0009C9D0 File Offset: 0x0009ABD0
			public virtual bool? EwsEnabled
			{
				set
				{
					base.PowerSharpParameters["EwsEnabled"] = value;
				}
			}

			// Token: 0x17004233 RID: 16947
			// (set) Token: 0x060066BC RID: 26300 RVA: 0x0009C9E8 File Offset: 0x0009ABE8
			public virtual bool? EwsAllowOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowOutlook"] = value;
				}
			}

			// Token: 0x17004234 RID: 16948
			// (set) Token: 0x060066BD RID: 26301 RVA: 0x0009CA00 File Offset: 0x0009AC00
			public virtual bool? EwsAllowMacOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowMacOutlook"] = value;
				}
			}

			// Token: 0x17004235 RID: 16949
			// (set) Token: 0x060066BE RID: 26302 RVA: 0x0009CA18 File Offset: 0x0009AC18
			public virtual bool? EwsAllowEntourage
			{
				set
				{
					base.PowerSharpParameters["EwsAllowEntourage"] = value;
				}
			}

			// Token: 0x17004236 RID: 16950
			// (set) Token: 0x060066BF RID: 26303 RVA: 0x0009CA30 File Offset: 0x0009AC30
			public virtual EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
			{
				set
				{
					base.PowerSharpParameters["EwsApplicationAccessPolicy"] = value;
				}
			}

			// Token: 0x17004237 RID: 16951
			// (set) Token: 0x060066C0 RID: 26304 RVA: 0x0009CA48 File Offset: 0x0009AC48
			public virtual MultiValuedProperty<string> EwsAllowList
			{
				set
				{
					base.PowerSharpParameters["EwsAllowList"] = value;
				}
			}

			// Token: 0x17004238 RID: 16952
			// (set) Token: 0x060066C1 RID: 26305 RVA: 0x0009CA5B File Offset: 0x0009AC5B
			public virtual MultiValuedProperty<string> EwsBlockList
			{
				set
				{
					base.PowerSharpParameters["EwsBlockList"] = value;
				}
			}

			// Token: 0x17004239 RID: 16953
			// (set) Token: 0x060066C2 RID: 26306 RVA: 0x0009CA6E File Offset: 0x0009AC6E
			public virtual bool CalendarVersionStoreEnabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreEnabled"] = value;
				}
			}

			// Token: 0x1700423A RID: 16954
			// (set) Token: 0x060066C3 RID: 26307 RVA: 0x0009CA86 File Offset: 0x0009AC86
			public virtual bool IsGuidPrefixedLegacyDnDisabled
			{
				set
				{
					base.PowerSharpParameters["IsGuidPrefixedLegacyDnDisabled"] = value;
				}
			}

			// Token: 0x1700423B RID: 16955
			// (set) Token: 0x060066C4 RID: 26308 RVA: 0x0009CA9E File Offset: 0x0009AC9E
			public virtual MultiValuedProperty<UMLanguage> UMAvailableLanguages
			{
				set
				{
					base.PowerSharpParameters["UMAvailableLanguages"] = value;
				}
			}

			// Token: 0x1700423C RID: 16956
			// (set) Token: 0x060066C5 RID: 26309 RVA: 0x0009CAB1 File Offset: 0x0009ACB1
			public virtual bool IsMailboxForcedReplicationDisabled
			{
				set
				{
					base.PowerSharpParameters["IsMailboxForcedReplicationDisabled"] = value;
				}
			}

			// Token: 0x1700423D RID: 16957
			// (set) Token: 0x060066C6 RID: 26310 RVA: 0x0009CAC9 File Offset: 0x0009ACC9
			public virtual int PreferredInternetCodePageForShiftJis
			{
				set
				{
					base.PowerSharpParameters["PreferredInternetCodePageForShiftJis"] = value;
				}
			}

			// Token: 0x1700423E RID: 16958
			// (set) Token: 0x060066C7 RID: 26311 RVA: 0x0009CAE1 File Offset: 0x0009ACE1
			public virtual int RequiredCharsetCoverage
			{
				set
				{
					base.PowerSharpParameters["RequiredCharsetCoverage"] = value;
				}
			}

			// Token: 0x1700423F RID: 16959
			// (set) Token: 0x060066C8 RID: 26312 RVA: 0x0009CAF9 File Offset: 0x0009ACF9
			public virtual int ByteEncoderTypeFor7BitCharsets
			{
				set
				{
					base.PowerSharpParameters["ByteEncoderTypeFor7BitCharsets"] = value;
				}
			}

			// Token: 0x17004240 RID: 16960
			// (set) Token: 0x060066C9 RID: 26313 RVA: 0x0009CB11 File Offset: 0x0009AD11
			public virtual bool IsSyncPropertySetUpgradeAllowed
			{
				set
				{
					base.PowerSharpParameters["IsSyncPropertySetUpgradeAllowed"] = value;
				}
			}

			// Token: 0x17004241 RID: 16961
			// (set) Token: 0x060066CA RID: 26314 RVA: 0x0009CB29 File Offset: 0x0009AD29
			public virtual bool MapiHttpEnabled
			{
				set
				{
					base.PowerSharpParameters["MapiHttpEnabled"] = value;
				}
			}

			// Token: 0x17004242 RID: 16962
			// (set) Token: 0x060066CB RID: 26315 RVA: 0x0009CB41 File Offset: 0x0009AD41
			public virtual bool OAuth2ClientProfileEnabled
			{
				set
				{
					base.PowerSharpParameters["OAuth2ClientProfileEnabled"] = value;
				}
			}

			// Token: 0x17004243 RID: 16963
			// (set) Token: 0x060066CC RID: 26316 RVA: 0x0009CB59 File Offset: 0x0009AD59
			public virtual bool IntuneManagedStatus
			{
				set
				{
					base.PowerSharpParameters["IntuneManagedStatus"] = value;
				}
			}

			// Token: 0x17004244 RID: 16964
			// (set) Token: 0x060066CD RID: 26317 RVA: 0x0009CB71 File Offset: 0x0009AD71
			public virtual HybridConfigurationStatusFlags HybridConfigurationStatus
			{
				set
				{
					base.PowerSharpParameters["HybridConfigurationStatus"] = value;
				}
			}

			// Token: 0x17004245 RID: 16965
			// (set) Token: 0x060066CE RID: 26318 RVA: 0x0009CB89 File Offset: 0x0009AD89
			public virtual bool ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x17004246 RID: 16966
			// (set) Token: 0x060066CF RID: 26319 RVA: 0x0009CBA1 File Offset: 0x0009ADA1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004247 RID: 16967
			// (set) Token: 0x060066D0 RID: 26320 RVA: 0x0009CBB9 File Offset: 0x0009ADB9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004248 RID: 16968
			// (set) Token: 0x060066D1 RID: 26321 RVA: 0x0009CBD1 File Offset: 0x0009ADD1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004249 RID: 16969
			// (set) Token: 0x060066D2 RID: 26322 RVA: 0x0009CBE9 File Offset: 0x0009ADE9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700424A RID: 16970
			// (set) Token: 0x060066D3 RID: 26323 RVA: 0x0009CC01 File Offset: 0x0009AE01
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000820 RID: 2080
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700424B RID: 16971
			// (set) Token: 0x060066D5 RID: 26325 RVA: 0x0009CC21 File Offset: 0x0009AE21
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700424C RID: 16972
			// (set) Token: 0x060066D6 RID: 26326 RVA: 0x0009CC34 File Offset: 0x0009AE34
			public virtual string MicrosoftExchangeRecipientReplyRecipient
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientReplyRecipient"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700424D RID: 16973
			// (set) Token: 0x060066D7 RID: 26327 RVA: 0x0009CC52 File Offset: 0x0009AE52
			public virtual string HierarchicalAddressBookRoot
			{
				set
				{
					base.PowerSharpParameters["HierarchicalAddressBookRoot"] = ((value != null) ? new UserContactGroupIdParameter(value) : null);
				}
			}

			// Token: 0x1700424E RID: 16974
			// (set) Token: 0x060066D8 RID: 26328 RVA: 0x0009CC70 File Offset: 0x0009AE70
			public virtual string DistributionGroupDefaultOU
			{
				set
				{
					base.PowerSharpParameters["DistributionGroupDefaultOU"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700424F RID: 16975
			// (set) Token: 0x060066D9 RID: 26329 RVA: 0x0009CC8E File Offset: 0x0009AE8E
			public virtual MultiValuedProperty<RecipientIdParameter> ExchangeNotificationRecipients
			{
				set
				{
					base.PowerSharpParameters["ExchangeNotificationRecipients"] = value;
				}
			}

			// Token: 0x17004250 RID: 16976
			// (set) Token: 0x060066DA RID: 26330 RVA: 0x0009CCA1 File Offset: 0x0009AEA1
			public virtual MultiValuedProperty<MailboxOrMailUserIdParameter> RemotePublicFolderMailboxes
			{
				set
				{
					base.PowerSharpParameters["RemotePublicFolderMailboxes"] = value;
				}
			}

			// Token: 0x17004251 RID: 16977
			// (set) Token: 0x060066DB RID: 26331 RVA: 0x0009CCB4 File Offset: 0x0009AEB4
			public virtual int SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17004252 RID: 16978
			// (set) Token: 0x060066DC RID: 26332 RVA: 0x0009CCCC File Offset: 0x0009AECC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004253 RID: 16979
			// (set) Token: 0x060066DD RID: 26333 RVA: 0x0009CCDF File Offset: 0x0009AEDF
			public virtual bool PublicFoldersLockedForMigration
			{
				set
				{
					base.PowerSharpParameters["PublicFoldersLockedForMigration"] = value;
				}
			}

			// Token: 0x17004254 RID: 16980
			// (set) Token: 0x060066DE RID: 26334 RVA: 0x0009CCF7 File Offset: 0x0009AEF7
			public virtual bool PublicFolderMigrationComplete
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMigrationComplete"] = value;
				}
			}

			// Token: 0x17004255 RID: 16981
			// (set) Token: 0x060066DF RID: 26335 RVA: 0x0009CD0F File Offset: 0x0009AF0F
			public virtual bool PublicFolderMailboxesLockedForNewConnections
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMailboxesLockedForNewConnections"] = value;
				}
			}

			// Token: 0x17004256 RID: 16982
			// (set) Token: 0x060066E0 RID: 26336 RVA: 0x0009CD27 File Offset: 0x0009AF27
			public virtual bool PublicFolderMailboxesMigrationComplete
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMailboxesMigrationComplete"] = value;
				}
			}

			// Token: 0x17004257 RID: 16983
			// (set) Token: 0x060066E1 RID: 26337 RVA: 0x0009CD3F File Offset: 0x0009AF3F
			public virtual bool PublicComputersDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicComputersDetectionEnabled"] = value;
				}
			}

			// Token: 0x17004258 RID: 16984
			// (set) Token: 0x060066E2 RID: 26338 RVA: 0x0009CD57 File Offset: 0x0009AF57
			public virtual RmsoSubscriptionStatusFlags RmsoSubscriptionStatus
			{
				set
				{
					base.PowerSharpParameters["RmsoSubscriptionStatus"] = value;
				}
			}

			// Token: 0x17004259 RID: 16985
			// (set) Token: 0x060066E3 RID: 26339 RVA: 0x0009CD6F File Offset: 0x0009AF6F
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700425A RID: 16986
			// (set) Token: 0x060066E4 RID: 26340 RVA: 0x0009CD87 File Offset: 0x0009AF87
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x1700425B RID: 16987
			// (set) Token: 0x060066E5 RID: 26341 RVA: 0x0009CD9A File Offset: 0x0009AF9A
			public virtual Uri SiteMailboxCreationURL
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxCreationURL"] = value;
				}
			}

			// Token: 0x1700425C RID: 16988
			// (set) Token: 0x060066E6 RID: 26342 RVA: 0x0009CDAD File Offset: 0x0009AFAD
			public virtual bool? CustomerFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["CustomerFeedbackEnabled"] = value;
				}
			}

			// Token: 0x1700425D RID: 16989
			// (set) Token: 0x060066E7 RID: 26343 RVA: 0x0009CDC5 File Offset: 0x0009AFC5
			public virtual IndustryType Industry
			{
				set
				{
					base.PowerSharpParameters["Industry"] = value;
				}
			}

			// Token: 0x1700425E RID: 16990
			// (set) Token: 0x060066E8 RID: 26344 RVA: 0x0009CDDD File Offset: 0x0009AFDD
			public virtual string ManagedFolderHomepage
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderHomepage"] = value;
				}
			}

			// Token: 0x1700425F RID: 16991
			// (set) Token: 0x060066E9 RID: 26345 RVA: 0x0009CDF0 File Offset: 0x0009AFF0
			public virtual EnhancedTimeSpan? DefaultPublicFolderAgeLimit
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderAgeLimit"] = value;
				}
			}

			// Token: 0x17004260 RID: 16992
			// (set) Token: 0x060066EA RID: 26346 RVA: 0x0009CE08 File Offset: 0x0009B008
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x17004261 RID: 16993
			// (set) Token: 0x060066EB RID: 26347 RVA: 0x0009CE20 File Offset: 0x0009B020
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17004262 RID: 16994
			// (set) Token: 0x060066EC RID: 26348 RVA: 0x0009CE38 File Offset: 0x0009B038
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x17004263 RID: 16995
			// (set) Token: 0x060066ED RID: 26349 RVA: 0x0009CE50 File Offset: 0x0009B050
			public virtual EnhancedTimeSpan? DefaultPublicFolderDeletedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderDeletedItemRetention"] = value;
				}
			}

			// Token: 0x17004264 RID: 16996
			// (set) Token: 0x060066EE RID: 26350 RVA: 0x0009CE68 File Offset: 0x0009B068
			public virtual EnhancedTimeSpan? DefaultPublicFolderMovedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMovedItemRetention"] = value;
				}
			}

			// Token: 0x17004265 RID: 16997
			// (set) Token: 0x060066EF RID: 26351 RVA: 0x0009CE80 File Offset: 0x0009B080
			public virtual MultiValuedProperty<OrganizationSummaryEntry> OrganizationSummary
			{
				set
				{
					base.PowerSharpParameters["OrganizationSummary"] = value;
				}
			}

			// Token: 0x17004266 RID: 16998
			// (set) Token: 0x060066F0 RID: 26352 RVA: 0x0009CE93 File Offset: 0x0009B093
			public virtual bool ForwardSyncLiveIdBusinessInstance
			{
				set
				{
					base.PowerSharpParameters["ForwardSyncLiveIdBusinessInstance"] = value;
				}
			}

			// Token: 0x17004267 RID: 16999
			// (set) Token: 0x060066F1 RID: 26353 RVA: 0x0009CEAB File Offset: 0x0009B0AB
			public virtual ProxyAddressCollection MicrosoftExchangeRecipientEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientEmailAddresses"] = value;
				}
			}

			// Token: 0x17004268 RID: 17000
			// (set) Token: 0x060066F2 RID: 26354 RVA: 0x0009CEBE File Offset: 0x0009B0BE
			public virtual SmtpAddress MicrosoftExchangeRecipientPrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientPrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17004269 RID: 17001
			// (set) Token: 0x060066F3 RID: 26355 RVA: 0x0009CED6 File Offset: 0x0009B0D6
			public virtual bool MicrosoftExchangeRecipientEmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientEmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x1700426A RID: 17002
			// (set) Token: 0x060066F4 RID: 26356 RVA: 0x0009CEEE File Offset: 0x0009B0EE
			public virtual bool MailTipsExternalRecipientsTipsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsExternalRecipientsTipsEnabled"] = value;
				}
			}

			// Token: 0x1700426B RID: 17003
			// (set) Token: 0x060066F5 RID: 26357 RVA: 0x0009CF06 File Offset: 0x0009B106
			public virtual uint MailTipsLargeAudienceThreshold
			{
				set
				{
					base.PowerSharpParameters["MailTipsLargeAudienceThreshold"] = value;
				}
			}

			// Token: 0x1700426C RID: 17004
			// (set) Token: 0x060066F6 RID: 26358 RVA: 0x0009CF1E File Offset: 0x0009B11E
			public virtual PublicFoldersDeployment PublicFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicFoldersEnabled"] = value;
				}
			}

			// Token: 0x1700426D RID: 17005
			// (set) Token: 0x060066F7 RID: 26359 RVA: 0x0009CF36 File Offset: 0x0009B136
			public virtual bool MailTipsMailboxSourcedTipsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsMailboxSourcedTipsEnabled"] = value;
				}
			}

			// Token: 0x1700426E RID: 17006
			// (set) Token: 0x060066F8 RID: 26360 RVA: 0x0009CF4E File Offset: 0x0009B14E
			public virtual bool MailTipsGroupMetricsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsGroupMetricsEnabled"] = value;
				}
			}

			// Token: 0x1700426F RID: 17007
			// (set) Token: 0x060066F9 RID: 26361 RVA: 0x0009CF66 File Offset: 0x0009B166
			public virtual bool MailTipsAllTipsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsAllTipsEnabled"] = value;
				}
			}

			// Token: 0x17004270 RID: 17008
			// (set) Token: 0x060066FA RID: 26362 RVA: 0x0009CF7E File Offset: 0x0009B17E
			public virtual bool ReadTrackingEnabled
			{
				set
				{
					base.PowerSharpParameters["ReadTrackingEnabled"] = value;
				}
			}

			// Token: 0x17004271 RID: 17009
			// (set) Token: 0x060066FB RID: 26363 RVA: 0x0009CF96 File Offset: 0x0009B196
			public virtual MultiValuedProperty<string> DistributionGroupNameBlockedWordsList
			{
				set
				{
					base.PowerSharpParameters["DistributionGroupNameBlockedWordsList"] = value;
				}
			}

			// Token: 0x17004272 RID: 17010
			// (set) Token: 0x060066FC RID: 26364 RVA: 0x0009CFA9 File Offset: 0x0009B1A9
			public virtual DistributionGroupNamingPolicy DistributionGroupNamingPolicy
			{
				set
				{
					base.PowerSharpParameters["DistributionGroupNamingPolicy"] = value;
				}
			}

			// Token: 0x17004273 RID: 17011
			// (set) Token: 0x060066FD RID: 26365 RVA: 0x0009CFBC File Offset: 0x0009B1BC
			public virtual ProtocolConnectionSettings AVAuthenticationService
			{
				set
				{
					base.PowerSharpParameters["AVAuthenticationService"] = value;
				}
			}

			// Token: 0x17004274 RID: 17012
			// (set) Token: 0x060066FE RID: 26366 RVA: 0x0009CFCF File Offset: 0x0009B1CF
			public virtual ProtocolConnectionSettings SIPAccessService
			{
				set
				{
					base.PowerSharpParameters["SIPAccessService"] = value;
				}
			}

			// Token: 0x17004275 RID: 17013
			// (set) Token: 0x060066FF RID: 26367 RVA: 0x0009CFE2 File Offset: 0x0009B1E2
			public virtual ProtocolConnectionSettings SIPSessionBorderController
			{
				set
				{
					base.PowerSharpParameters["SIPSessionBorderController"] = value;
				}
			}

			// Token: 0x17004276 RID: 17014
			// (set) Token: 0x06006700 RID: 26368 RVA: 0x0009CFF5 File Offset: 0x0009B1F5
			public virtual bool ExchangeNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["ExchangeNotificationEnabled"] = value;
				}
			}

			// Token: 0x17004277 RID: 17015
			// (set) Token: 0x06006701 RID: 26369 RVA: 0x0009D00D File Offset: 0x0009B20D
			public virtual EnhancedTimeSpan ActivityBasedAuthenticationTimeoutInterval
			{
				set
				{
					base.PowerSharpParameters["ActivityBasedAuthenticationTimeoutInterval"] = value;
				}
			}

			// Token: 0x17004278 RID: 17016
			// (set) Token: 0x06006702 RID: 26370 RVA: 0x0009D025 File Offset: 0x0009B225
			public virtual bool ActivityBasedAuthenticationTimeoutEnabled
			{
				set
				{
					base.PowerSharpParameters["ActivityBasedAuthenticationTimeoutEnabled"] = value;
				}
			}

			// Token: 0x17004279 RID: 17017
			// (set) Token: 0x06006703 RID: 26371 RVA: 0x0009D03D File Offset: 0x0009B23D
			public virtual bool ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled
			{
				set
				{
					base.PowerSharpParameters["ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled"] = value;
				}
			}

			// Token: 0x1700427A RID: 17018
			// (set) Token: 0x06006704 RID: 26372 RVA: 0x0009D055 File Offset: 0x0009B255
			public virtual string WACDiscoveryEndpoint
			{
				set
				{
					base.PowerSharpParameters["WACDiscoveryEndpoint"] = value;
				}
			}

			// Token: 0x1700427B RID: 17019
			// (set) Token: 0x06006705 RID: 26373 RVA: 0x0009D068 File Offset: 0x0009B268
			public virtual bool IsExcludedFromOnboardMigration
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromOnboardMigration"] = value;
				}
			}

			// Token: 0x1700427C RID: 17020
			// (set) Token: 0x06006706 RID: 26374 RVA: 0x0009D080 File Offset: 0x0009B280
			public virtual bool IsExcludedFromOffboardMigration
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromOffboardMigration"] = value;
				}
			}

			// Token: 0x1700427D RID: 17021
			// (set) Token: 0x06006707 RID: 26375 RVA: 0x0009D098 File Offset: 0x0009B298
			public virtual bool IsFfoMigrationInProgress
			{
				set
				{
					base.PowerSharpParameters["IsFfoMigrationInProgress"] = value;
				}
			}

			// Token: 0x1700427E RID: 17022
			// (set) Token: 0x06006708 RID: 26376 RVA: 0x0009D0B0 File Offset: 0x0009B2B0
			public virtual bool TenantRelocationsAllowed
			{
				set
				{
					base.PowerSharpParameters["TenantRelocationsAllowed"] = value;
				}
			}

			// Token: 0x1700427F RID: 17023
			// (set) Token: 0x06006709 RID: 26377 RVA: 0x0009D0C8 File Offset: 0x0009B2C8
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x17004280 RID: 17024
			// (set) Token: 0x0600670A RID: 26378 RVA: 0x0009D0E0 File Offset: 0x0009B2E0
			public virtual bool IsProcessEhaMigratedMessagesEnabled
			{
				set
				{
					base.PowerSharpParameters["IsProcessEhaMigratedMessagesEnabled"] = value;
				}
			}

			// Token: 0x17004281 RID: 17025
			// (set) Token: 0x0600670B RID: 26379 RVA: 0x0009D0F8 File Offset: 0x0009B2F8
			public virtual bool AppsForOfficeEnabled
			{
				set
				{
					base.PowerSharpParameters["AppsForOfficeEnabled"] = value;
				}
			}

			// Token: 0x17004282 RID: 17026
			// (set) Token: 0x0600670C RID: 26380 RVA: 0x0009D110 File Offset: 0x0009B310
			public virtual bool? EwsEnabled
			{
				set
				{
					base.PowerSharpParameters["EwsEnabled"] = value;
				}
			}

			// Token: 0x17004283 RID: 17027
			// (set) Token: 0x0600670D RID: 26381 RVA: 0x0009D128 File Offset: 0x0009B328
			public virtual bool? EwsAllowOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowOutlook"] = value;
				}
			}

			// Token: 0x17004284 RID: 17028
			// (set) Token: 0x0600670E RID: 26382 RVA: 0x0009D140 File Offset: 0x0009B340
			public virtual bool? EwsAllowMacOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowMacOutlook"] = value;
				}
			}

			// Token: 0x17004285 RID: 17029
			// (set) Token: 0x0600670F RID: 26383 RVA: 0x0009D158 File Offset: 0x0009B358
			public virtual bool? EwsAllowEntourage
			{
				set
				{
					base.PowerSharpParameters["EwsAllowEntourage"] = value;
				}
			}

			// Token: 0x17004286 RID: 17030
			// (set) Token: 0x06006710 RID: 26384 RVA: 0x0009D170 File Offset: 0x0009B370
			public virtual EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
			{
				set
				{
					base.PowerSharpParameters["EwsApplicationAccessPolicy"] = value;
				}
			}

			// Token: 0x17004287 RID: 17031
			// (set) Token: 0x06006711 RID: 26385 RVA: 0x0009D188 File Offset: 0x0009B388
			public virtual MultiValuedProperty<string> EwsAllowList
			{
				set
				{
					base.PowerSharpParameters["EwsAllowList"] = value;
				}
			}

			// Token: 0x17004288 RID: 17032
			// (set) Token: 0x06006712 RID: 26386 RVA: 0x0009D19B File Offset: 0x0009B39B
			public virtual MultiValuedProperty<string> EwsBlockList
			{
				set
				{
					base.PowerSharpParameters["EwsBlockList"] = value;
				}
			}

			// Token: 0x17004289 RID: 17033
			// (set) Token: 0x06006713 RID: 26387 RVA: 0x0009D1AE File Offset: 0x0009B3AE
			public virtual bool CalendarVersionStoreEnabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreEnabled"] = value;
				}
			}

			// Token: 0x1700428A RID: 17034
			// (set) Token: 0x06006714 RID: 26388 RVA: 0x0009D1C6 File Offset: 0x0009B3C6
			public virtual bool IsGuidPrefixedLegacyDnDisabled
			{
				set
				{
					base.PowerSharpParameters["IsGuidPrefixedLegacyDnDisabled"] = value;
				}
			}

			// Token: 0x1700428B RID: 17035
			// (set) Token: 0x06006715 RID: 26389 RVA: 0x0009D1DE File Offset: 0x0009B3DE
			public virtual MultiValuedProperty<UMLanguage> UMAvailableLanguages
			{
				set
				{
					base.PowerSharpParameters["UMAvailableLanguages"] = value;
				}
			}

			// Token: 0x1700428C RID: 17036
			// (set) Token: 0x06006716 RID: 26390 RVA: 0x0009D1F1 File Offset: 0x0009B3F1
			public virtual bool IsMailboxForcedReplicationDisabled
			{
				set
				{
					base.PowerSharpParameters["IsMailboxForcedReplicationDisabled"] = value;
				}
			}

			// Token: 0x1700428D RID: 17037
			// (set) Token: 0x06006717 RID: 26391 RVA: 0x0009D209 File Offset: 0x0009B409
			public virtual int PreferredInternetCodePageForShiftJis
			{
				set
				{
					base.PowerSharpParameters["PreferredInternetCodePageForShiftJis"] = value;
				}
			}

			// Token: 0x1700428E RID: 17038
			// (set) Token: 0x06006718 RID: 26392 RVA: 0x0009D221 File Offset: 0x0009B421
			public virtual int RequiredCharsetCoverage
			{
				set
				{
					base.PowerSharpParameters["RequiredCharsetCoverage"] = value;
				}
			}

			// Token: 0x1700428F RID: 17039
			// (set) Token: 0x06006719 RID: 26393 RVA: 0x0009D239 File Offset: 0x0009B439
			public virtual int ByteEncoderTypeFor7BitCharsets
			{
				set
				{
					base.PowerSharpParameters["ByteEncoderTypeFor7BitCharsets"] = value;
				}
			}

			// Token: 0x17004290 RID: 17040
			// (set) Token: 0x0600671A RID: 26394 RVA: 0x0009D251 File Offset: 0x0009B451
			public virtual bool IsSyncPropertySetUpgradeAllowed
			{
				set
				{
					base.PowerSharpParameters["IsSyncPropertySetUpgradeAllowed"] = value;
				}
			}

			// Token: 0x17004291 RID: 17041
			// (set) Token: 0x0600671B RID: 26395 RVA: 0x0009D269 File Offset: 0x0009B469
			public virtual bool MapiHttpEnabled
			{
				set
				{
					base.PowerSharpParameters["MapiHttpEnabled"] = value;
				}
			}

			// Token: 0x17004292 RID: 17042
			// (set) Token: 0x0600671C RID: 26396 RVA: 0x0009D281 File Offset: 0x0009B481
			public virtual bool OAuth2ClientProfileEnabled
			{
				set
				{
					base.PowerSharpParameters["OAuth2ClientProfileEnabled"] = value;
				}
			}

			// Token: 0x17004293 RID: 17043
			// (set) Token: 0x0600671D RID: 26397 RVA: 0x0009D299 File Offset: 0x0009B499
			public virtual bool IntuneManagedStatus
			{
				set
				{
					base.PowerSharpParameters["IntuneManagedStatus"] = value;
				}
			}

			// Token: 0x17004294 RID: 17044
			// (set) Token: 0x0600671E RID: 26398 RVA: 0x0009D2B1 File Offset: 0x0009B4B1
			public virtual HybridConfigurationStatusFlags HybridConfigurationStatus
			{
				set
				{
					base.PowerSharpParameters["HybridConfigurationStatus"] = value;
				}
			}

			// Token: 0x17004295 RID: 17045
			// (set) Token: 0x0600671F RID: 26399 RVA: 0x0009D2C9 File Offset: 0x0009B4C9
			public virtual bool ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x17004296 RID: 17046
			// (set) Token: 0x06006720 RID: 26400 RVA: 0x0009D2E1 File Offset: 0x0009B4E1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004297 RID: 17047
			// (set) Token: 0x06006721 RID: 26401 RVA: 0x0009D2F9 File Offset: 0x0009B4F9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004298 RID: 17048
			// (set) Token: 0x06006722 RID: 26402 RVA: 0x0009D311 File Offset: 0x0009B511
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004299 RID: 17049
			// (set) Token: 0x06006723 RID: 26403 RVA: 0x0009D329 File Offset: 0x0009B529
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700429A RID: 17050
			// (set) Token: 0x06006724 RID: 26404 RVA: 0x0009D341 File Offset: 0x0009B541
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000821 RID: 2081
		public class AdfsAuthenticationRawConfigurationParameters : ParametersBase
		{
			// Token: 0x1700429B RID: 17051
			// (set) Token: 0x06006726 RID: 26406 RVA: 0x0009D361 File Offset: 0x0009B561
			public virtual string AdfsAuthenticationConfiguration
			{
				set
				{
					base.PowerSharpParameters["AdfsAuthenticationConfiguration"] = value;
				}
			}

			// Token: 0x1700429C RID: 17052
			// (set) Token: 0x06006727 RID: 26407 RVA: 0x0009D374 File Offset: 0x0009B574
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700429D RID: 17053
			// (set) Token: 0x06006728 RID: 26408 RVA: 0x0009D387 File Offset: 0x0009B587
			public virtual string MicrosoftExchangeRecipientReplyRecipient
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientReplyRecipient"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700429E RID: 17054
			// (set) Token: 0x06006729 RID: 26409 RVA: 0x0009D3A5 File Offset: 0x0009B5A5
			public virtual string HierarchicalAddressBookRoot
			{
				set
				{
					base.PowerSharpParameters["HierarchicalAddressBookRoot"] = ((value != null) ? new UserContactGroupIdParameter(value) : null);
				}
			}

			// Token: 0x1700429F RID: 17055
			// (set) Token: 0x0600672A RID: 26410 RVA: 0x0009D3C3 File Offset: 0x0009B5C3
			public virtual string DistributionGroupDefaultOU
			{
				set
				{
					base.PowerSharpParameters["DistributionGroupDefaultOU"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170042A0 RID: 17056
			// (set) Token: 0x0600672B RID: 26411 RVA: 0x0009D3E1 File Offset: 0x0009B5E1
			public virtual MultiValuedProperty<RecipientIdParameter> ExchangeNotificationRecipients
			{
				set
				{
					base.PowerSharpParameters["ExchangeNotificationRecipients"] = value;
				}
			}

			// Token: 0x170042A1 RID: 17057
			// (set) Token: 0x0600672C RID: 26412 RVA: 0x0009D3F4 File Offset: 0x0009B5F4
			public virtual MultiValuedProperty<MailboxOrMailUserIdParameter> RemotePublicFolderMailboxes
			{
				set
				{
					base.PowerSharpParameters["RemotePublicFolderMailboxes"] = value;
				}
			}

			// Token: 0x170042A2 RID: 17058
			// (set) Token: 0x0600672D RID: 26413 RVA: 0x0009D407 File Offset: 0x0009B607
			public virtual int SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170042A3 RID: 17059
			// (set) Token: 0x0600672E RID: 26414 RVA: 0x0009D41F File Offset: 0x0009B61F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170042A4 RID: 17060
			// (set) Token: 0x0600672F RID: 26415 RVA: 0x0009D432 File Offset: 0x0009B632
			public virtual bool PublicFoldersLockedForMigration
			{
				set
				{
					base.PowerSharpParameters["PublicFoldersLockedForMigration"] = value;
				}
			}

			// Token: 0x170042A5 RID: 17061
			// (set) Token: 0x06006730 RID: 26416 RVA: 0x0009D44A File Offset: 0x0009B64A
			public virtual bool PublicFolderMigrationComplete
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMigrationComplete"] = value;
				}
			}

			// Token: 0x170042A6 RID: 17062
			// (set) Token: 0x06006731 RID: 26417 RVA: 0x0009D462 File Offset: 0x0009B662
			public virtual bool PublicFolderMailboxesLockedForNewConnections
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMailboxesLockedForNewConnections"] = value;
				}
			}

			// Token: 0x170042A7 RID: 17063
			// (set) Token: 0x06006732 RID: 26418 RVA: 0x0009D47A File Offset: 0x0009B67A
			public virtual bool PublicFolderMailboxesMigrationComplete
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMailboxesMigrationComplete"] = value;
				}
			}

			// Token: 0x170042A8 RID: 17064
			// (set) Token: 0x06006733 RID: 26419 RVA: 0x0009D492 File Offset: 0x0009B692
			public virtual bool PublicComputersDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicComputersDetectionEnabled"] = value;
				}
			}

			// Token: 0x170042A9 RID: 17065
			// (set) Token: 0x06006734 RID: 26420 RVA: 0x0009D4AA File Offset: 0x0009B6AA
			public virtual RmsoSubscriptionStatusFlags RmsoSubscriptionStatus
			{
				set
				{
					base.PowerSharpParameters["RmsoSubscriptionStatus"] = value;
				}
			}

			// Token: 0x170042AA RID: 17066
			// (set) Token: 0x06006735 RID: 26421 RVA: 0x0009D4C2 File Offset: 0x0009B6C2
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x170042AB RID: 17067
			// (set) Token: 0x06006736 RID: 26422 RVA: 0x0009D4DA File Offset: 0x0009B6DA
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x170042AC RID: 17068
			// (set) Token: 0x06006737 RID: 26423 RVA: 0x0009D4ED File Offset: 0x0009B6ED
			public virtual Uri SiteMailboxCreationURL
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxCreationURL"] = value;
				}
			}

			// Token: 0x170042AD RID: 17069
			// (set) Token: 0x06006738 RID: 26424 RVA: 0x0009D500 File Offset: 0x0009B700
			public virtual bool? CustomerFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["CustomerFeedbackEnabled"] = value;
				}
			}

			// Token: 0x170042AE RID: 17070
			// (set) Token: 0x06006739 RID: 26425 RVA: 0x0009D518 File Offset: 0x0009B718
			public virtual IndustryType Industry
			{
				set
				{
					base.PowerSharpParameters["Industry"] = value;
				}
			}

			// Token: 0x170042AF RID: 17071
			// (set) Token: 0x0600673A RID: 26426 RVA: 0x0009D530 File Offset: 0x0009B730
			public virtual string ManagedFolderHomepage
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderHomepage"] = value;
				}
			}

			// Token: 0x170042B0 RID: 17072
			// (set) Token: 0x0600673B RID: 26427 RVA: 0x0009D543 File Offset: 0x0009B743
			public virtual EnhancedTimeSpan? DefaultPublicFolderAgeLimit
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderAgeLimit"] = value;
				}
			}

			// Token: 0x170042B1 RID: 17073
			// (set) Token: 0x0600673C RID: 26428 RVA: 0x0009D55B File Offset: 0x0009B75B
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x170042B2 RID: 17074
			// (set) Token: 0x0600673D RID: 26429 RVA: 0x0009D573 File Offset: 0x0009B773
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x170042B3 RID: 17075
			// (set) Token: 0x0600673E RID: 26430 RVA: 0x0009D58B File Offset: 0x0009B78B
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x170042B4 RID: 17076
			// (set) Token: 0x0600673F RID: 26431 RVA: 0x0009D5A3 File Offset: 0x0009B7A3
			public virtual EnhancedTimeSpan? DefaultPublicFolderDeletedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderDeletedItemRetention"] = value;
				}
			}

			// Token: 0x170042B5 RID: 17077
			// (set) Token: 0x06006740 RID: 26432 RVA: 0x0009D5BB File Offset: 0x0009B7BB
			public virtual EnhancedTimeSpan? DefaultPublicFolderMovedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMovedItemRetention"] = value;
				}
			}

			// Token: 0x170042B6 RID: 17078
			// (set) Token: 0x06006741 RID: 26433 RVA: 0x0009D5D3 File Offset: 0x0009B7D3
			public virtual MultiValuedProperty<OrganizationSummaryEntry> OrganizationSummary
			{
				set
				{
					base.PowerSharpParameters["OrganizationSummary"] = value;
				}
			}

			// Token: 0x170042B7 RID: 17079
			// (set) Token: 0x06006742 RID: 26434 RVA: 0x0009D5E6 File Offset: 0x0009B7E6
			public virtual bool ForwardSyncLiveIdBusinessInstance
			{
				set
				{
					base.PowerSharpParameters["ForwardSyncLiveIdBusinessInstance"] = value;
				}
			}

			// Token: 0x170042B8 RID: 17080
			// (set) Token: 0x06006743 RID: 26435 RVA: 0x0009D5FE File Offset: 0x0009B7FE
			public virtual ProxyAddressCollection MicrosoftExchangeRecipientEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientEmailAddresses"] = value;
				}
			}

			// Token: 0x170042B9 RID: 17081
			// (set) Token: 0x06006744 RID: 26436 RVA: 0x0009D611 File Offset: 0x0009B811
			public virtual SmtpAddress MicrosoftExchangeRecipientPrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientPrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170042BA RID: 17082
			// (set) Token: 0x06006745 RID: 26437 RVA: 0x0009D629 File Offset: 0x0009B829
			public virtual bool MicrosoftExchangeRecipientEmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientEmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x170042BB RID: 17083
			// (set) Token: 0x06006746 RID: 26438 RVA: 0x0009D641 File Offset: 0x0009B841
			public virtual bool MailTipsExternalRecipientsTipsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsExternalRecipientsTipsEnabled"] = value;
				}
			}

			// Token: 0x170042BC RID: 17084
			// (set) Token: 0x06006747 RID: 26439 RVA: 0x0009D659 File Offset: 0x0009B859
			public virtual uint MailTipsLargeAudienceThreshold
			{
				set
				{
					base.PowerSharpParameters["MailTipsLargeAudienceThreshold"] = value;
				}
			}

			// Token: 0x170042BD RID: 17085
			// (set) Token: 0x06006748 RID: 26440 RVA: 0x0009D671 File Offset: 0x0009B871
			public virtual PublicFoldersDeployment PublicFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicFoldersEnabled"] = value;
				}
			}

			// Token: 0x170042BE RID: 17086
			// (set) Token: 0x06006749 RID: 26441 RVA: 0x0009D689 File Offset: 0x0009B889
			public virtual bool MailTipsMailboxSourcedTipsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsMailboxSourcedTipsEnabled"] = value;
				}
			}

			// Token: 0x170042BF RID: 17087
			// (set) Token: 0x0600674A RID: 26442 RVA: 0x0009D6A1 File Offset: 0x0009B8A1
			public virtual bool MailTipsGroupMetricsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsGroupMetricsEnabled"] = value;
				}
			}

			// Token: 0x170042C0 RID: 17088
			// (set) Token: 0x0600674B RID: 26443 RVA: 0x0009D6B9 File Offset: 0x0009B8B9
			public virtual bool MailTipsAllTipsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsAllTipsEnabled"] = value;
				}
			}

			// Token: 0x170042C1 RID: 17089
			// (set) Token: 0x0600674C RID: 26444 RVA: 0x0009D6D1 File Offset: 0x0009B8D1
			public virtual bool ReadTrackingEnabled
			{
				set
				{
					base.PowerSharpParameters["ReadTrackingEnabled"] = value;
				}
			}

			// Token: 0x170042C2 RID: 17090
			// (set) Token: 0x0600674D RID: 26445 RVA: 0x0009D6E9 File Offset: 0x0009B8E9
			public virtual MultiValuedProperty<string> DistributionGroupNameBlockedWordsList
			{
				set
				{
					base.PowerSharpParameters["DistributionGroupNameBlockedWordsList"] = value;
				}
			}

			// Token: 0x170042C3 RID: 17091
			// (set) Token: 0x0600674E RID: 26446 RVA: 0x0009D6FC File Offset: 0x0009B8FC
			public virtual DistributionGroupNamingPolicy DistributionGroupNamingPolicy
			{
				set
				{
					base.PowerSharpParameters["DistributionGroupNamingPolicy"] = value;
				}
			}

			// Token: 0x170042C4 RID: 17092
			// (set) Token: 0x0600674F RID: 26447 RVA: 0x0009D70F File Offset: 0x0009B90F
			public virtual ProtocolConnectionSettings AVAuthenticationService
			{
				set
				{
					base.PowerSharpParameters["AVAuthenticationService"] = value;
				}
			}

			// Token: 0x170042C5 RID: 17093
			// (set) Token: 0x06006750 RID: 26448 RVA: 0x0009D722 File Offset: 0x0009B922
			public virtual ProtocolConnectionSettings SIPAccessService
			{
				set
				{
					base.PowerSharpParameters["SIPAccessService"] = value;
				}
			}

			// Token: 0x170042C6 RID: 17094
			// (set) Token: 0x06006751 RID: 26449 RVA: 0x0009D735 File Offset: 0x0009B935
			public virtual ProtocolConnectionSettings SIPSessionBorderController
			{
				set
				{
					base.PowerSharpParameters["SIPSessionBorderController"] = value;
				}
			}

			// Token: 0x170042C7 RID: 17095
			// (set) Token: 0x06006752 RID: 26450 RVA: 0x0009D748 File Offset: 0x0009B948
			public virtual bool ExchangeNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["ExchangeNotificationEnabled"] = value;
				}
			}

			// Token: 0x170042C8 RID: 17096
			// (set) Token: 0x06006753 RID: 26451 RVA: 0x0009D760 File Offset: 0x0009B960
			public virtual EnhancedTimeSpan ActivityBasedAuthenticationTimeoutInterval
			{
				set
				{
					base.PowerSharpParameters["ActivityBasedAuthenticationTimeoutInterval"] = value;
				}
			}

			// Token: 0x170042C9 RID: 17097
			// (set) Token: 0x06006754 RID: 26452 RVA: 0x0009D778 File Offset: 0x0009B978
			public virtual bool ActivityBasedAuthenticationTimeoutEnabled
			{
				set
				{
					base.PowerSharpParameters["ActivityBasedAuthenticationTimeoutEnabled"] = value;
				}
			}

			// Token: 0x170042CA RID: 17098
			// (set) Token: 0x06006755 RID: 26453 RVA: 0x0009D790 File Offset: 0x0009B990
			public virtual bool ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled
			{
				set
				{
					base.PowerSharpParameters["ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled"] = value;
				}
			}

			// Token: 0x170042CB RID: 17099
			// (set) Token: 0x06006756 RID: 26454 RVA: 0x0009D7A8 File Offset: 0x0009B9A8
			public virtual string WACDiscoveryEndpoint
			{
				set
				{
					base.PowerSharpParameters["WACDiscoveryEndpoint"] = value;
				}
			}

			// Token: 0x170042CC RID: 17100
			// (set) Token: 0x06006757 RID: 26455 RVA: 0x0009D7BB File Offset: 0x0009B9BB
			public virtual bool IsExcludedFromOnboardMigration
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromOnboardMigration"] = value;
				}
			}

			// Token: 0x170042CD RID: 17101
			// (set) Token: 0x06006758 RID: 26456 RVA: 0x0009D7D3 File Offset: 0x0009B9D3
			public virtual bool IsExcludedFromOffboardMigration
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromOffboardMigration"] = value;
				}
			}

			// Token: 0x170042CE RID: 17102
			// (set) Token: 0x06006759 RID: 26457 RVA: 0x0009D7EB File Offset: 0x0009B9EB
			public virtual bool IsFfoMigrationInProgress
			{
				set
				{
					base.PowerSharpParameters["IsFfoMigrationInProgress"] = value;
				}
			}

			// Token: 0x170042CF RID: 17103
			// (set) Token: 0x0600675A RID: 26458 RVA: 0x0009D803 File Offset: 0x0009BA03
			public virtual bool TenantRelocationsAllowed
			{
				set
				{
					base.PowerSharpParameters["TenantRelocationsAllowed"] = value;
				}
			}

			// Token: 0x170042D0 RID: 17104
			// (set) Token: 0x0600675B RID: 26459 RVA: 0x0009D81B File Offset: 0x0009BA1B
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x170042D1 RID: 17105
			// (set) Token: 0x0600675C RID: 26460 RVA: 0x0009D833 File Offset: 0x0009BA33
			public virtual bool IsProcessEhaMigratedMessagesEnabled
			{
				set
				{
					base.PowerSharpParameters["IsProcessEhaMigratedMessagesEnabled"] = value;
				}
			}

			// Token: 0x170042D2 RID: 17106
			// (set) Token: 0x0600675D RID: 26461 RVA: 0x0009D84B File Offset: 0x0009BA4B
			public virtual bool AppsForOfficeEnabled
			{
				set
				{
					base.PowerSharpParameters["AppsForOfficeEnabled"] = value;
				}
			}

			// Token: 0x170042D3 RID: 17107
			// (set) Token: 0x0600675E RID: 26462 RVA: 0x0009D863 File Offset: 0x0009BA63
			public virtual bool? EwsEnabled
			{
				set
				{
					base.PowerSharpParameters["EwsEnabled"] = value;
				}
			}

			// Token: 0x170042D4 RID: 17108
			// (set) Token: 0x0600675F RID: 26463 RVA: 0x0009D87B File Offset: 0x0009BA7B
			public virtual bool? EwsAllowOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowOutlook"] = value;
				}
			}

			// Token: 0x170042D5 RID: 17109
			// (set) Token: 0x06006760 RID: 26464 RVA: 0x0009D893 File Offset: 0x0009BA93
			public virtual bool? EwsAllowMacOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowMacOutlook"] = value;
				}
			}

			// Token: 0x170042D6 RID: 17110
			// (set) Token: 0x06006761 RID: 26465 RVA: 0x0009D8AB File Offset: 0x0009BAAB
			public virtual bool? EwsAllowEntourage
			{
				set
				{
					base.PowerSharpParameters["EwsAllowEntourage"] = value;
				}
			}

			// Token: 0x170042D7 RID: 17111
			// (set) Token: 0x06006762 RID: 26466 RVA: 0x0009D8C3 File Offset: 0x0009BAC3
			public virtual EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
			{
				set
				{
					base.PowerSharpParameters["EwsApplicationAccessPolicy"] = value;
				}
			}

			// Token: 0x170042D8 RID: 17112
			// (set) Token: 0x06006763 RID: 26467 RVA: 0x0009D8DB File Offset: 0x0009BADB
			public virtual MultiValuedProperty<string> EwsAllowList
			{
				set
				{
					base.PowerSharpParameters["EwsAllowList"] = value;
				}
			}

			// Token: 0x170042D9 RID: 17113
			// (set) Token: 0x06006764 RID: 26468 RVA: 0x0009D8EE File Offset: 0x0009BAEE
			public virtual MultiValuedProperty<string> EwsBlockList
			{
				set
				{
					base.PowerSharpParameters["EwsBlockList"] = value;
				}
			}

			// Token: 0x170042DA RID: 17114
			// (set) Token: 0x06006765 RID: 26469 RVA: 0x0009D901 File Offset: 0x0009BB01
			public virtual bool CalendarVersionStoreEnabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreEnabled"] = value;
				}
			}

			// Token: 0x170042DB RID: 17115
			// (set) Token: 0x06006766 RID: 26470 RVA: 0x0009D919 File Offset: 0x0009BB19
			public virtual bool IsGuidPrefixedLegacyDnDisabled
			{
				set
				{
					base.PowerSharpParameters["IsGuidPrefixedLegacyDnDisabled"] = value;
				}
			}

			// Token: 0x170042DC RID: 17116
			// (set) Token: 0x06006767 RID: 26471 RVA: 0x0009D931 File Offset: 0x0009BB31
			public virtual MultiValuedProperty<UMLanguage> UMAvailableLanguages
			{
				set
				{
					base.PowerSharpParameters["UMAvailableLanguages"] = value;
				}
			}

			// Token: 0x170042DD RID: 17117
			// (set) Token: 0x06006768 RID: 26472 RVA: 0x0009D944 File Offset: 0x0009BB44
			public virtual bool IsMailboxForcedReplicationDisabled
			{
				set
				{
					base.PowerSharpParameters["IsMailboxForcedReplicationDisabled"] = value;
				}
			}

			// Token: 0x170042DE RID: 17118
			// (set) Token: 0x06006769 RID: 26473 RVA: 0x0009D95C File Offset: 0x0009BB5C
			public virtual int PreferredInternetCodePageForShiftJis
			{
				set
				{
					base.PowerSharpParameters["PreferredInternetCodePageForShiftJis"] = value;
				}
			}

			// Token: 0x170042DF RID: 17119
			// (set) Token: 0x0600676A RID: 26474 RVA: 0x0009D974 File Offset: 0x0009BB74
			public virtual int RequiredCharsetCoverage
			{
				set
				{
					base.PowerSharpParameters["RequiredCharsetCoverage"] = value;
				}
			}

			// Token: 0x170042E0 RID: 17120
			// (set) Token: 0x0600676B RID: 26475 RVA: 0x0009D98C File Offset: 0x0009BB8C
			public virtual int ByteEncoderTypeFor7BitCharsets
			{
				set
				{
					base.PowerSharpParameters["ByteEncoderTypeFor7BitCharsets"] = value;
				}
			}

			// Token: 0x170042E1 RID: 17121
			// (set) Token: 0x0600676C RID: 26476 RVA: 0x0009D9A4 File Offset: 0x0009BBA4
			public virtual bool IsSyncPropertySetUpgradeAllowed
			{
				set
				{
					base.PowerSharpParameters["IsSyncPropertySetUpgradeAllowed"] = value;
				}
			}

			// Token: 0x170042E2 RID: 17122
			// (set) Token: 0x0600676D RID: 26477 RVA: 0x0009D9BC File Offset: 0x0009BBBC
			public virtual bool MapiHttpEnabled
			{
				set
				{
					base.PowerSharpParameters["MapiHttpEnabled"] = value;
				}
			}

			// Token: 0x170042E3 RID: 17123
			// (set) Token: 0x0600676E RID: 26478 RVA: 0x0009D9D4 File Offset: 0x0009BBD4
			public virtual bool OAuth2ClientProfileEnabled
			{
				set
				{
					base.PowerSharpParameters["OAuth2ClientProfileEnabled"] = value;
				}
			}

			// Token: 0x170042E4 RID: 17124
			// (set) Token: 0x0600676F RID: 26479 RVA: 0x0009D9EC File Offset: 0x0009BBEC
			public virtual bool IntuneManagedStatus
			{
				set
				{
					base.PowerSharpParameters["IntuneManagedStatus"] = value;
				}
			}

			// Token: 0x170042E5 RID: 17125
			// (set) Token: 0x06006770 RID: 26480 RVA: 0x0009DA04 File Offset: 0x0009BC04
			public virtual HybridConfigurationStatusFlags HybridConfigurationStatus
			{
				set
				{
					base.PowerSharpParameters["HybridConfigurationStatus"] = value;
				}
			}

			// Token: 0x170042E6 RID: 17126
			// (set) Token: 0x06006771 RID: 26481 RVA: 0x0009DA1C File Offset: 0x0009BC1C
			public virtual bool ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x170042E7 RID: 17127
			// (set) Token: 0x06006772 RID: 26482 RVA: 0x0009DA34 File Offset: 0x0009BC34
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170042E8 RID: 17128
			// (set) Token: 0x06006773 RID: 26483 RVA: 0x0009DA4C File Offset: 0x0009BC4C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170042E9 RID: 17129
			// (set) Token: 0x06006774 RID: 26484 RVA: 0x0009DA64 File Offset: 0x0009BC64
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170042EA RID: 17130
			// (set) Token: 0x06006775 RID: 26485 RVA: 0x0009DA7C File Offset: 0x0009BC7C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170042EB RID: 17131
			// (set) Token: 0x06006776 RID: 26486 RVA: 0x0009DA94 File Offset: 0x0009BC94
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000822 RID: 2082
		public class AdfsAuthenticationParameterParameters : ParametersBase
		{
			// Token: 0x170042EC RID: 17132
			// (set) Token: 0x06006778 RID: 26488 RVA: 0x0009DAB4 File Offset: 0x0009BCB4
			public virtual Uri AdfsIssuer
			{
				set
				{
					base.PowerSharpParameters["AdfsIssuer"] = value;
				}
			}

			// Token: 0x170042ED RID: 17133
			// (set) Token: 0x06006779 RID: 26489 RVA: 0x0009DAC7 File Offset: 0x0009BCC7
			public virtual MultiValuedProperty<Uri> AdfsAudienceUris
			{
				set
				{
					base.PowerSharpParameters["AdfsAudienceUris"] = value;
				}
			}

			// Token: 0x170042EE RID: 17134
			// (set) Token: 0x0600677A RID: 26490 RVA: 0x0009DADA File Offset: 0x0009BCDA
			public virtual MultiValuedProperty<string> AdfsSignCertificateThumbprints
			{
				set
				{
					base.PowerSharpParameters["AdfsSignCertificateThumbprints"] = value;
				}
			}

			// Token: 0x170042EF RID: 17135
			// (set) Token: 0x0600677B RID: 26491 RVA: 0x0009DAED File Offset: 0x0009BCED
			public virtual string AdfsEncryptCertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["AdfsEncryptCertificateThumbprint"] = value;
				}
			}

			// Token: 0x170042F0 RID: 17136
			// (set) Token: 0x0600677C RID: 26492 RVA: 0x0009DB00 File Offset: 0x0009BD00
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170042F1 RID: 17137
			// (set) Token: 0x0600677D RID: 26493 RVA: 0x0009DB13 File Offset: 0x0009BD13
			public virtual string MicrosoftExchangeRecipientReplyRecipient
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientReplyRecipient"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170042F2 RID: 17138
			// (set) Token: 0x0600677E RID: 26494 RVA: 0x0009DB31 File Offset: 0x0009BD31
			public virtual string HierarchicalAddressBookRoot
			{
				set
				{
					base.PowerSharpParameters["HierarchicalAddressBookRoot"] = ((value != null) ? new UserContactGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170042F3 RID: 17139
			// (set) Token: 0x0600677F RID: 26495 RVA: 0x0009DB4F File Offset: 0x0009BD4F
			public virtual string DistributionGroupDefaultOU
			{
				set
				{
					base.PowerSharpParameters["DistributionGroupDefaultOU"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170042F4 RID: 17140
			// (set) Token: 0x06006780 RID: 26496 RVA: 0x0009DB6D File Offset: 0x0009BD6D
			public virtual MultiValuedProperty<RecipientIdParameter> ExchangeNotificationRecipients
			{
				set
				{
					base.PowerSharpParameters["ExchangeNotificationRecipients"] = value;
				}
			}

			// Token: 0x170042F5 RID: 17141
			// (set) Token: 0x06006781 RID: 26497 RVA: 0x0009DB80 File Offset: 0x0009BD80
			public virtual MultiValuedProperty<MailboxOrMailUserIdParameter> RemotePublicFolderMailboxes
			{
				set
				{
					base.PowerSharpParameters["RemotePublicFolderMailboxes"] = value;
				}
			}

			// Token: 0x170042F6 RID: 17142
			// (set) Token: 0x06006782 RID: 26498 RVA: 0x0009DB93 File Offset: 0x0009BD93
			public virtual int SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170042F7 RID: 17143
			// (set) Token: 0x06006783 RID: 26499 RVA: 0x0009DBAB File Offset: 0x0009BDAB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170042F8 RID: 17144
			// (set) Token: 0x06006784 RID: 26500 RVA: 0x0009DBBE File Offset: 0x0009BDBE
			public virtual bool PublicFoldersLockedForMigration
			{
				set
				{
					base.PowerSharpParameters["PublicFoldersLockedForMigration"] = value;
				}
			}

			// Token: 0x170042F9 RID: 17145
			// (set) Token: 0x06006785 RID: 26501 RVA: 0x0009DBD6 File Offset: 0x0009BDD6
			public virtual bool PublicFolderMigrationComplete
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMigrationComplete"] = value;
				}
			}

			// Token: 0x170042FA RID: 17146
			// (set) Token: 0x06006786 RID: 26502 RVA: 0x0009DBEE File Offset: 0x0009BDEE
			public virtual bool PublicFolderMailboxesLockedForNewConnections
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMailboxesLockedForNewConnections"] = value;
				}
			}

			// Token: 0x170042FB RID: 17147
			// (set) Token: 0x06006787 RID: 26503 RVA: 0x0009DC06 File Offset: 0x0009BE06
			public virtual bool PublicFolderMailboxesMigrationComplete
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMailboxesMigrationComplete"] = value;
				}
			}

			// Token: 0x170042FC RID: 17148
			// (set) Token: 0x06006788 RID: 26504 RVA: 0x0009DC1E File Offset: 0x0009BE1E
			public virtual bool PublicComputersDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicComputersDetectionEnabled"] = value;
				}
			}

			// Token: 0x170042FD RID: 17149
			// (set) Token: 0x06006789 RID: 26505 RVA: 0x0009DC36 File Offset: 0x0009BE36
			public virtual RmsoSubscriptionStatusFlags RmsoSubscriptionStatus
			{
				set
				{
					base.PowerSharpParameters["RmsoSubscriptionStatus"] = value;
				}
			}

			// Token: 0x170042FE RID: 17150
			// (set) Token: 0x0600678A RID: 26506 RVA: 0x0009DC4E File Offset: 0x0009BE4E
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x170042FF RID: 17151
			// (set) Token: 0x0600678B RID: 26507 RVA: 0x0009DC66 File Offset: 0x0009BE66
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x17004300 RID: 17152
			// (set) Token: 0x0600678C RID: 26508 RVA: 0x0009DC79 File Offset: 0x0009BE79
			public virtual Uri SiteMailboxCreationURL
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxCreationURL"] = value;
				}
			}

			// Token: 0x17004301 RID: 17153
			// (set) Token: 0x0600678D RID: 26509 RVA: 0x0009DC8C File Offset: 0x0009BE8C
			public virtual bool? CustomerFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["CustomerFeedbackEnabled"] = value;
				}
			}

			// Token: 0x17004302 RID: 17154
			// (set) Token: 0x0600678E RID: 26510 RVA: 0x0009DCA4 File Offset: 0x0009BEA4
			public virtual IndustryType Industry
			{
				set
				{
					base.PowerSharpParameters["Industry"] = value;
				}
			}

			// Token: 0x17004303 RID: 17155
			// (set) Token: 0x0600678F RID: 26511 RVA: 0x0009DCBC File Offset: 0x0009BEBC
			public virtual string ManagedFolderHomepage
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderHomepage"] = value;
				}
			}

			// Token: 0x17004304 RID: 17156
			// (set) Token: 0x06006790 RID: 26512 RVA: 0x0009DCCF File Offset: 0x0009BECF
			public virtual EnhancedTimeSpan? DefaultPublicFolderAgeLimit
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderAgeLimit"] = value;
				}
			}

			// Token: 0x17004305 RID: 17157
			// (set) Token: 0x06006791 RID: 26513 RVA: 0x0009DCE7 File Offset: 0x0009BEE7
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x17004306 RID: 17158
			// (set) Token: 0x06006792 RID: 26514 RVA: 0x0009DCFF File Offset: 0x0009BEFF
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17004307 RID: 17159
			// (set) Token: 0x06006793 RID: 26515 RVA: 0x0009DD17 File Offset: 0x0009BF17
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x17004308 RID: 17160
			// (set) Token: 0x06006794 RID: 26516 RVA: 0x0009DD2F File Offset: 0x0009BF2F
			public virtual EnhancedTimeSpan? DefaultPublicFolderDeletedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderDeletedItemRetention"] = value;
				}
			}

			// Token: 0x17004309 RID: 17161
			// (set) Token: 0x06006795 RID: 26517 RVA: 0x0009DD47 File Offset: 0x0009BF47
			public virtual EnhancedTimeSpan? DefaultPublicFolderMovedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMovedItemRetention"] = value;
				}
			}

			// Token: 0x1700430A RID: 17162
			// (set) Token: 0x06006796 RID: 26518 RVA: 0x0009DD5F File Offset: 0x0009BF5F
			public virtual MultiValuedProperty<OrganizationSummaryEntry> OrganizationSummary
			{
				set
				{
					base.PowerSharpParameters["OrganizationSummary"] = value;
				}
			}

			// Token: 0x1700430B RID: 17163
			// (set) Token: 0x06006797 RID: 26519 RVA: 0x0009DD72 File Offset: 0x0009BF72
			public virtual bool ForwardSyncLiveIdBusinessInstance
			{
				set
				{
					base.PowerSharpParameters["ForwardSyncLiveIdBusinessInstance"] = value;
				}
			}

			// Token: 0x1700430C RID: 17164
			// (set) Token: 0x06006798 RID: 26520 RVA: 0x0009DD8A File Offset: 0x0009BF8A
			public virtual ProxyAddressCollection MicrosoftExchangeRecipientEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientEmailAddresses"] = value;
				}
			}

			// Token: 0x1700430D RID: 17165
			// (set) Token: 0x06006799 RID: 26521 RVA: 0x0009DD9D File Offset: 0x0009BF9D
			public virtual SmtpAddress MicrosoftExchangeRecipientPrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientPrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700430E RID: 17166
			// (set) Token: 0x0600679A RID: 26522 RVA: 0x0009DDB5 File Offset: 0x0009BFB5
			public virtual bool MicrosoftExchangeRecipientEmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["MicrosoftExchangeRecipientEmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x1700430F RID: 17167
			// (set) Token: 0x0600679B RID: 26523 RVA: 0x0009DDCD File Offset: 0x0009BFCD
			public virtual bool MailTipsExternalRecipientsTipsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsExternalRecipientsTipsEnabled"] = value;
				}
			}

			// Token: 0x17004310 RID: 17168
			// (set) Token: 0x0600679C RID: 26524 RVA: 0x0009DDE5 File Offset: 0x0009BFE5
			public virtual uint MailTipsLargeAudienceThreshold
			{
				set
				{
					base.PowerSharpParameters["MailTipsLargeAudienceThreshold"] = value;
				}
			}

			// Token: 0x17004311 RID: 17169
			// (set) Token: 0x0600679D RID: 26525 RVA: 0x0009DDFD File Offset: 0x0009BFFD
			public virtual PublicFoldersDeployment PublicFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicFoldersEnabled"] = value;
				}
			}

			// Token: 0x17004312 RID: 17170
			// (set) Token: 0x0600679E RID: 26526 RVA: 0x0009DE15 File Offset: 0x0009C015
			public virtual bool MailTipsMailboxSourcedTipsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsMailboxSourcedTipsEnabled"] = value;
				}
			}

			// Token: 0x17004313 RID: 17171
			// (set) Token: 0x0600679F RID: 26527 RVA: 0x0009DE2D File Offset: 0x0009C02D
			public virtual bool MailTipsGroupMetricsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsGroupMetricsEnabled"] = value;
				}
			}

			// Token: 0x17004314 RID: 17172
			// (set) Token: 0x060067A0 RID: 26528 RVA: 0x0009DE45 File Offset: 0x0009C045
			public virtual bool MailTipsAllTipsEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsAllTipsEnabled"] = value;
				}
			}

			// Token: 0x17004315 RID: 17173
			// (set) Token: 0x060067A1 RID: 26529 RVA: 0x0009DE5D File Offset: 0x0009C05D
			public virtual bool ReadTrackingEnabled
			{
				set
				{
					base.PowerSharpParameters["ReadTrackingEnabled"] = value;
				}
			}

			// Token: 0x17004316 RID: 17174
			// (set) Token: 0x060067A2 RID: 26530 RVA: 0x0009DE75 File Offset: 0x0009C075
			public virtual MultiValuedProperty<string> DistributionGroupNameBlockedWordsList
			{
				set
				{
					base.PowerSharpParameters["DistributionGroupNameBlockedWordsList"] = value;
				}
			}

			// Token: 0x17004317 RID: 17175
			// (set) Token: 0x060067A3 RID: 26531 RVA: 0x0009DE88 File Offset: 0x0009C088
			public virtual DistributionGroupNamingPolicy DistributionGroupNamingPolicy
			{
				set
				{
					base.PowerSharpParameters["DistributionGroupNamingPolicy"] = value;
				}
			}

			// Token: 0x17004318 RID: 17176
			// (set) Token: 0x060067A4 RID: 26532 RVA: 0x0009DE9B File Offset: 0x0009C09B
			public virtual ProtocolConnectionSettings AVAuthenticationService
			{
				set
				{
					base.PowerSharpParameters["AVAuthenticationService"] = value;
				}
			}

			// Token: 0x17004319 RID: 17177
			// (set) Token: 0x060067A5 RID: 26533 RVA: 0x0009DEAE File Offset: 0x0009C0AE
			public virtual ProtocolConnectionSettings SIPAccessService
			{
				set
				{
					base.PowerSharpParameters["SIPAccessService"] = value;
				}
			}

			// Token: 0x1700431A RID: 17178
			// (set) Token: 0x060067A6 RID: 26534 RVA: 0x0009DEC1 File Offset: 0x0009C0C1
			public virtual ProtocolConnectionSettings SIPSessionBorderController
			{
				set
				{
					base.PowerSharpParameters["SIPSessionBorderController"] = value;
				}
			}

			// Token: 0x1700431B RID: 17179
			// (set) Token: 0x060067A7 RID: 26535 RVA: 0x0009DED4 File Offset: 0x0009C0D4
			public virtual bool ExchangeNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["ExchangeNotificationEnabled"] = value;
				}
			}

			// Token: 0x1700431C RID: 17180
			// (set) Token: 0x060067A8 RID: 26536 RVA: 0x0009DEEC File Offset: 0x0009C0EC
			public virtual EnhancedTimeSpan ActivityBasedAuthenticationTimeoutInterval
			{
				set
				{
					base.PowerSharpParameters["ActivityBasedAuthenticationTimeoutInterval"] = value;
				}
			}

			// Token: 0x1700431D RID: 17181
			// (set) Token: 0x060067A9 RID: 26537 RVA: 0x0009DF04 File Offset: 0x0009C104
			public virtual bool ActivityBasedAuthenticationTimeoutEnabled
			{
				set
				{
					base.PowerSharpParameters["ActivityBasedAuthenticationTimeoutEnabled"] = value;
				}
			}

			// Token: 0x1700431E RID: 17182
			// (set) Token: 0x060067AA RID: 26538 RVA: 0x0009DF1C File Offset: 0x0009C11C
			public virtual bool ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled
			{
				set
				{
					base.PowerSharpParameters["ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled"] = value;
				}
			}

			// Token: 0x1700431F RID: 17183
			// (set) Token: 0x060067AB RID: 26539 RVA: 0x0009DF34 File Offset: 0x0009C134
			public virtual string WACDiscoveryEndpoint
			{
				set
				{
					base.PowerSharpParameters["WACDiscoveryEndpoint"] = value;
				}
			}

			// Token: 0x17004320 RID: 17184
			// (set) Token: 0x060067AC RID: 26540 RVA: 0x0009DF47 File Offset: 0x0009C147
			public virtual bool IsExcludedFromOnboardMigration
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromOnboardMigration"] = value;
				}
			}

			// Token: 0x17004321 RID: 17185
			// (set) Token: 0x060067AD RID: 26541 RVA: 0x0009DF5F File Offset: 0x0009C15F
			public virtual bool IsExcludedFromOffboardMigration
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromOffboardMigration"] = value;
				}
			}

			// Token: 0x17004322 RID: 17186
			// (set) Token: 0x060067AE RID: 26542 RVA: 0x0009DF77 File Offset: 0x0009C177
			public virtual bool IsFfoMigrationInProgress
			{
				set
				{
					base.PowerSharpParameters["IsFfoMigrationInProgress"] = value;
				}
			}

			// Token: 0x17004323 RID: 17187
			// (set) Token: 0x060067AF RID: 26543 RVA: 0x0009DF8F File Offset: 0x0009C18F
			public virtual bool TenantRelocationsAllowed
			{
				set
				{
					base.PowerSharpParameters["TenantRelocationsAllowed"] = value;
				}
			}

			// Token: 0x17004324 RID: 17188
			// (set) Token: 0x060067B0 RID: 26544 RVA: 0x0009DFA7 File Offset: 0x0009C1A7
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x17004325 RID: 17189
			// (set) Token: 0x060067B1 RID: 26545 RVA: 0x0009DFBF File Offset: 0x0009C1BF
			public virtual bool IsProcessEhaMigratedMessagesEnabled
			{
				set
				{
					base.PowerSharpParameters["IsProcessEhaMigratedMessagesEnabled"] = value;
				}
			}

			// Token: 0x17004326 RID: 17190
			// (set) Token: 0x060067B2 RID: 26546 RVA: 0x0009DFD7 File Offset: 0x0009C1D7
			public virtual bool AppsForOfficeEnabled
			{
				set
				{
					base.PowerSharpParameters["AppsForOfficeEnabled"] = value;
				}
			}

			// Token: 0x17004327 RID: 17191
			// (set) Token: 0x060067B3 RID: 26547 RVA: 0x0009DFEF File Offset: 0x0009C1EF
			public virtual bool? EwsEnabled
			{
				set
				{
					base.PowerSharpParameters["EwsEnabled"] = value;
				}
			}

			// Token: 0x17004328 RID: 17192
			// (set) Token: 0x060067B4 RID: 26548 RVA: 0x0009E007 File Offset: 0x0009C207
			public virtual bool? EwsAllowOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowOutlook"] = value;
				}
			}

			// Token: 0x17004329 RID: 17193
			// (set) Token: 0x060067B5 RID: 26549 RVA: 0x0009E01F File Offset: 0x0009C21F
			public virtual bool? EwsAllowMacOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowMacOutlook"] = value;
				}
			}

			// Token: 0x1700432A RID: 17194
			// (set) Token: 0x060067B6 RID: 26550 RVA: 0x0009E037 File Offset: 0x0009C237
			public virtual bool? EwsAllowEntourage
			{
				set
				{
					base.PowerSharpParameters["EwsAllowEntourage"] = value;
				}
			}

			// Token: 0x1700432B RID: 17195
			// (set) Token: 0x060067B7 RID: 26551 RVA: 0x0009E04F File Offset: 0x0009C24F
			public virtual EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
			{
				set
				{
					base.PowerSharpParameters["EwsApplicationAccessPolicy"] = value;
				}
			}

			// Token: 0x1700432C RID: 17196
			// (set) Token: 0x060067B8 RID: 26552 RVA: 0x0009E067 File Offset: 0x0009C267
			public virtual MultiValuedProperty<string> EwsAllowList
			{
				set
				{
					base.PowerSharpParameters["EwsAllowList"] = value;
				}
			}

			// Token: 0x1700432D RID: 17197
			// (set) Token: 0x060067B9 RID: 26553 RVA: 0x0009E07A File Offset: 0x0009C27A
			public virtual MultiValuedProperty<string> EwsBlockList
			{
				set
				{
					base.PowerSharpParameters["EwsBlockList"] = value;
				}
			}

			// Token: 0x1700432E RID: 17198
			// (set) Token: 0x060067BA RID: 26554 RVA: 0x0009E08D File Offset: 0x0009C28D
			public virtual bool CalendarVersionStoreEnabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreEnabled"] = value;
				}
			}

			// Token: 0x1700432F RID: 17199
			// (set) Token: 0x060067BB RID: 26555 RVA: 0x0009E0A5 File Offset: 0x0009C2A5
			public virtual bool IsGuidPrefixedLegacyDnDisabled
			{
				set
				{
					base.PowerSharpParameters["IsGuidPrefixedLegacyDnDisabled"] = value;
				}
			}

			// Token: 0x17004330 RID: 17200
			// (set) Token: 0x060067BC RID: 26556 RVA: 0x0009E0BD File Offset: 0x0009C2BD
			public virtual MultiValuedProperty<UMLanguage> UMAvailableLanguages
			{
				set
				{
					base.PowerSharpParameters["UMAvailableLanguages"] = value;
				}
			}

			// Token: 0x17004331 RID: 17201
			// (set) Token: 0x060067BD RID: 26557 RVA: 0x0009E0D0 File Offset: 0x0009C2D0
			public virtual bool IsMailboxForcedReplicationDisabled
			{
				set
				{
					base.PowerSharpParameters["IsMailboxForcedReplicationDisabled"] = value;
				}
			}

			// Token: 0x17004332 RID: 17202
			// (set) Token: 0x060067BE RID: 26558 RVA: 0x0009E0E8 File Offset: 0x0009C2E8
			public virtual int PreferredInternetCodePageForShiftJis
			{
				set
				{
					base.PowerSharpParameters["PreferredInternetCodePageForShiftJis"] = value;
				}
			}

			// Token: 0x17004333 RID: 17203
			// (set) Token: 0x060067BF RID: 26559 RVA: 0x0009E100 File Offset: 0x0009C300
			public virtual int RequiredCharsetCoverage
			{
				set
				{
					base.PowerSharpParameters["RequiredCharsetCoverage"] = value;
				}
			}

			// Token: 0x17004334 RID: 17204
			// (set) Token: 0x060067C0 RID: 26560 RVA: 0x0009E118 File Offset: 0x0009C318
			public virtual int ByteEncoderTypeFor7BitCharsets
			{
				set
				{
					base.PowerSharpParameters["ByteEncoderTypeFor7BitCharsets"] = value;
				}
			}

			// Token: 0x17004335 RID: 17205
			// (set) Token: 0x060067C1 RID: 26561 RVA: 0x0009E130 File Offset: 0x0009C330
			public virtual bool IsSyncPropertySetUpgradeAllowed
			{
				set
				{
					base.PowerSharpParameters["IsSyncPropertySetUpgradeAllowed"] = value;
				}
			}

			// Token: 0x17004336 RID: 17206
			// (set) Token: 0x060067C2 RID: 26562 RVA: 0x0009E148 File Offset: 0x0009C348
			public virtual bool MapiHttpEnabled
			{
				set
				{
					base.PowerSharpParameters["MapiHttpEnabled"] = value;
				}
			}

			// Token: 0x17004337 RID: 17207
			// (set) Token: 0x060067C3 RID: 26563 RVA: 0x0009E160 File Offset: 0x0009C360
			public virtual bool OAuth2ClientProfileEnabled
			{
				set
				{
					base.PowerSharpParameters["OAuth2ClientProfileEnabled"] = value;
				}
			}

			// Token: 0x17004338 RID: 17208
			// (set) Token: 0x060067C4 RID: 26564 RVA: 0x0009E178 File Offset: 0x0009C378
			public virtual bool IntuneManagedStatus
			{
				set
				{
					base.PowerSharpParameters["IntuneManagedStatus"] = value;
				}
			}

			// Token: 0x17004339 RID: 17209
			// (set) Token: 0x060067C5 RID: 26565 RVA: 0x0009E190 File Offset: 0x0009C390
			public virtual HybridConfigurationStatusFlags HybridConfigurationStatus
			{
				set
				{
					base.PowerSharpParameters["HybridConfigurationStatus"] = value;
				}
			}

			// Token: 0x1700433A RID: 17210
			// (set) Token: 0x060067C6 RID: 26566 RVA: 0x0009E1A8 File Offset: 0x0009C3A8
			public virtual bool ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x1700433B RID: 17211
			// (set) Token: 0x060067C7 RID: 26567 RVA: 0x0009E1C0 File Offset: 0x0009C3C0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700433C RID: 17212
			// (set) Token: 0x060067C8 RID: 26568 RVA: 0x0009E1D8 File Offset: 0x0009C3D8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700433D RID: 17213
			// (set) Token: 0x060067C9 RID: 26569 RVA: 0x0009E1F0 File Offset: 0x0009C3F0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700433E RID: 17214
			// (set) Token: 0x060067CA RID: 26570 RVA: 0x0009E208 File Offset: 0x0009C408
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700433F RID: 17215
			// (set) Token: 0x060067CB RID: 26571 RVA: 0x0009E220 File Offset: 0x0009C420
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
