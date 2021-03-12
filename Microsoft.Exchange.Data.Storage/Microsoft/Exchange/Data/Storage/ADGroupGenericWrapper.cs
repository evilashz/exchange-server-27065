using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002EC RID: 748
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ADGroupGenericWrapper : IGenericADUser, IFederatedIdentityParameters
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x00087555 File Offset: 0x00085755
		public ADGroupGenericWrapper(IADGroup adGroup)
		{
			ArgumentValidator.ThrowIfNull("adGroup", adGroup);
			this.adGroup = adGroup;
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x0008757C File Offset: 0x0008577C
		public string DisplayName
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adGroup.DisplayName);
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x0008758F File Offset: 0x0008578F
		public string UserPrincipalName
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x000875A8 File Offset: 0x000857A8
		public string LegacyDn
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adGroup.LegacyExchangeDN);
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x000875C8 File Offset: 0x000857C8
		public string Alias
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adGroup.Alias);
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x000875E8 File Offset: 0x000857E8
		public ADObjectId DefaultPublicFolderMailbox
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adGroup.DefaultPublicFolderMailbox);
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x00087608 File Offset: 0x00085808
		public SecurityIdentifier Sid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SecurityIdentifier>(() => this.adGroup.Sid);
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x00087628 File Offset: 0x00085828
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SecurityIdentifier>(() => this.adGroup.MasterAccountSid);
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x00087648 File Offset: 0x00085848
		public IEnumerable<SecurityIdentifier> SidHistory
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<SecurityIdentifier>>(() => this.adGroup.SidHistory);
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x00087668 File Offset: 0x00085868
		public IEnumerable<ADObjectId> GrantSendOnBehalfTo
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<ADObjectId>>(() => this.adGroup.GrantSendOnBehalfTo);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x0008767C File Offset: 0x0008587C
		public IEnumerable<CultureInfo> Languages
		{
			get
			{
				return new CultureInfo[]
				{
					new CultureInfo("en-us")
				};
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x000876AB File Offset: 0x000858AB
		public ADObjectId Id
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adGroup.Id);
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x000876CB File Offset: 0x000858CB
		public RecipientType RecipientType
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<RecipientType>(() => this.adGroup.RecipientType);
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x000876EB File Offset: 0x000858EB
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<RecipientTypeDetails>(() => this.adGroup.RecipientTypeDetails);
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x0008770B File Offset: 0x0008590B
		public bool? IsResource
		{
			get
			{
				return new bool?(DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adGroup.IsResource));
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x00087730 File Offset: 0x00085930
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SmtpAddress>(() => this.adGroup.PrimarySmtpAddress);
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x00087750 File Offset: 0x00085950
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ProxyAddress>(() => this.adGroup.ExternalEmailAddress);
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x00087770 File Offset: 0x00085970
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ProxyAddressCollection>(() => this.adGroup.EmailAddresses);
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x00087790 File Offset: 0x00085990
		public ADObjectId ObjectId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adGroup.Id);
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x000877B0 File Offset: 0x000859B0
		public OrganizationId OrganizationId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<OrganizationId>(() => this.adGroup.OrganizationId);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060020E3 RID: 8419 RVA: 0x000877D0 File Offset: 0x000859D0
		public string ImmutableId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adGroup.ImmutableId);
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x000877F0 File Offset: 0x000859F0
		public Guid MailboxGuid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<Guid>(() => this.adGroup.ExchangeGuid);
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x00087810 File Offset: 0x00085A10
		public ADObjectId MailboxDatabase
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adGroup.Database);
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x00087830 File Offset: 0x00085A30
		public DateTime? WhenMailboxCreated
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.adGroup.WhenMailboxCreated);
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x00087843 File Offset: 0x00085A43
		public SmtpAddress WindowsLiveID
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x0008785C File Offset: 0x00085A5C
		public string ImmutableIdPartial
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adGroup.ImmutableIdPartial);
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x0008786F File Offset: 0x00085A6F
		public NetID NetId
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x00087888 File Offset: 0x00085A88
		public ModernGroupObjectType ModernGroupType
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ModernGroupObjectType>(() => this.adGroup.ModernGroupType);
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x000878A8 File Offset: 0x00085AA8
		public IEnumerable<SecurityIdentifier> PublicToGroupSids
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<SecurityIdentifier>>(() => this.adGroup.PublicToGroupSids);
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x000878C8 File Offset: 0x00085AC8
		public string ExternalDirectoryObjectId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adGroup.ExternalDirectoryObjectId);
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x000878DB File Offset: 0x00085ADB
		public ADObjectId ArchiveDatabase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x000878DE File Offset: 0x00085ADE
		public Guid ArchiveGuid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x000878E5 File Offset: 0x00085AE5
		public IEnumerable<string> ArchiveName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060020F0 RID: 8432 RVA: 0x000878E8 File Offset: 0x00085AE8
		public ArchiveState ArchiveState
		{
			get
			{
				return ArchiveState.None;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x000878EB File Offset: 0x00085AEB
		public ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				return ArchiveStatusFlags.None;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060020F2 RID: 8434 RVA: 0x000878EE File Offset: 0x00085AEE
		public SmtpDomain ArchiveDomain
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x000878F1 File Offset: 0x00085AF1
		public IEnumerable<Guid> AggregatedMailboxGuids
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x00087901 File Offset: 0x00085B01
		public Uri SharePointUrl
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<Uri>(() => this.adGroup.SharePointUrl);
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x00087921 File Offset: 0x00085B21
		public bool IsMapiEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adGroup.MAPIEnabled);
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x00087941 File Offset: 0x00085B41
		public bool IsOwaEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adGroup.OWAEnabled);
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x00087961 File Offset: 0x00085B61
		public bool IsMowaEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adGroup.MOWAEnabled);
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x00087981 File Offset: 0x00085B81
		public ADObjectId ThrottlingPolicy
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adGroup.ThrottlingPolicy);
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x00087994 File Offset: 0x00085B94
		public ADObjectId OwaMailboxPolicy
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060020FA RID: 8442 RVA: 0x00087997 File Offset: 0x00085B97
		public ADObjectId MobileDeviceMailboxPolicy
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x000879A7 File Offset: 0x00085BA7
		public ADObjectId AddressBookPolicy
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adGroup.AddressBookPolicy);
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x000879C7 File Offset: 0x00085BC7
		public bool IsPersonToPersonMessagingEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adGroup.IsPersonToPersonTextMessagingEnabled);
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x000879E7 File Offset: 0x00085BE7
		public bool IsMachineToPersonMessagingEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adGroup.IsMachineToPersonTextMessagingEnabled);
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x000879FC File Offset: 0x00085BFC
		public Capability? SkuCapability
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x00087A14 File Offset: 0x00085C14
		public bool? SkuAssigned
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x00087A37 File Offset: 0x00085C37
		public bool IsMailboxAuditEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adGroup.MailboxAuditEnabled);
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x00087A57 File Offset: 0x00085C57
		public bool BypassAudit
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adGroup.BypassAudit);
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x00087A77 File Offset: 0x00085C77
		public EnhancedTimeSpan MailboxAuditLogAgeLimit
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<EnhancedTimeSpan>(() => this.adGroup.MailboxAuditLogAgeLimit);
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x00087A97 File Offset: 0x00085C97
		public MailboxAuditOperations AuditAdminOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.adGroup.AuditAdminOperations);
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x00087AB7 File Offset: 0x00085CB7
		public MailboxAuditOperations AuditDelegateOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.adGroup.AuditDelegateOperations);
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x00087AD7 File Offset: 0x00085CD7
		public MailboxAuditOperations AuditDelegateAdminOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.adGroup.AuditDelegateAdminOperations);
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x00087AF7 File Offset: 0x00085CF7
		public MailboxAuditOperations AuditOwnerOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.adGroup.AuditOwnerOperations);
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x00087B17 File Offset: 0x00085D17
		public DateTime? AuditLastAdminAccess
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.adGroup.AuditLastAdminAccess);
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x00087B37 File Offset: 0x00085D37
		public DateTime? AuditLastDelegateAccess
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.adGroup.AuditLastDelegateAccess);
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x00087B57 File Offset: 0x00085D57
		public DateTime? AuditLastExternalAccess
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.adGroup.AuditLastExternalAccess);
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x00087B6A File Offset: 0x00085D6A
		public ADObjectId QueryBaseDN
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x00087B6D File Offset: 0x00085D6D
		public SmtpAddress GetFederatedSmtpAddress()
		{
			return SmtpAddress.Empty;
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x00087B74 File Offset: 0x00085D74
		public FederatedIdentity GetFederatedIdentity()
		{
			return null;
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x00087BAC File Offset: 0x00085DAC
		public IEnumerable<IMailboxLocationInfo> MailboxLocations
		{
			get
			{
				if (this.adGroup.ExchangeGuid != Guid.Empty && this.adGroup.Database != null)
				{
					return DirectoryExtensions.GetWithDirectoryExceptionTranslation<IMailboxLocationInfo[]>(() => new IMailboxLocationInfo[]
					{
						new MailboxLocationInfo(this.adGroup.ExchangeGuid, this.adGroup.Database, MailboxLocationType.Primary)
					});
				}
				return Enumerable.Empty<IMailboxLocationInfo>();
			}
		}

		// Token: 0x040013B0 RID: 5040
		private readonly IADGroup adGroup;
	}
}
