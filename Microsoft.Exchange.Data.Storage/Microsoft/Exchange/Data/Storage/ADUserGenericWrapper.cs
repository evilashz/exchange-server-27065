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
	// Token: 0x020002EB RID: 747
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ADUserGenericWrapper : IGenericADUser, IFederatedIdentityParameters
	{
		// Token: 0x06002053 RID: 8275 RVA: 0x00086D65 File Offset: 0x00084F65
		public ADUserGenericWrapper(IADUser adUser)
		{
			ArgumentValidator.ThrowIfNull("adUser", adUser);
			this.adUser = adUser;
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x00086D8C File Offset: 0x00084F8C
		public string DisplayName
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adUser.DisplayName);
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x00086DAC File Offset: 0x00084FAC
		public string UserPrincipalName
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adUser.UserPrincipalName);
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x00086DCC File Offset: 0x00084FCC
		public string LegacyDn
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adUser.LegacyExchangeDN);
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x00086DEC File Offset: 0x00084FEC
		public string Alias
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adUser.Alias);
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x00086E0C File Offset: 0x0008500C
		public ADObjectId DefaultPublicFolderMailbox
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adUser.DefaultPublicFolderMailbox);
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x00086E2C File Offset: 0x0008502C
		public SecurityIdentifier Sid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SecurityIdentifier>(() => this.adUser.Sid);
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x00086E4C File Offset: 0x0008504C
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SecurityIdentifier>(() => this.adUser.MasterAccountSid);
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x00086E6C File Offset: 0x0008506C
		public IEnumerable<SecurityIdentifier> SidHistory
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<SecurityIdentifier>>(() => this.adUser.SidHistory);
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x00086E8C File Offset: 0x0008508C
		public IEnumerable<ADObjectId> GrantSendOnBehalfTo
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<ADObjectId>>(() => this.adUser.GrantSendOnBehalfTo);
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00086EAC File Offset: 0x000850AC
		public IEnumerable<CultureInfo> Languages
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<CultureInfo>>(() => this.adUser.Languages);
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x00086ECC File Offset: 0x000850CC
		public ADObjectId Id
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adUser.Id);
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x00086EEC File Offset: 0x000850EC
		public RecipientType RecipientType
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<RecipientType>(() => this.adUser.RecipientType);
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x00086F0C File Offset: 0x0008510C
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<RecipientTypeDetails>(() => this.adUser.RecipientTypeDetails);
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x00086F2C File Offset: 0x0008512C
		public bool? IsResource
		{
			get
			{
				return new bool?(DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adUser.IsResource));
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x00086F51 File Offset: 0x00085151
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SmtpAddress>(() => this.adUser.PrimarySmtpAddress);
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x00086F71 File Offset: 0x00085171
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ProxyAddress>(() => this.adUser.ExternalEmailAddress);
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x00086F91 File Offset: 0x00085191
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ProxyAddressCollection>(() => this.adUser.EmailAddresses);
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x00086FB1 File Offset: 0x000851B1
		public ADObjectId ObjectId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adUser.Id);
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x00086FD1 File Offset: 0x000851D1
		public OrganizationId OrganizationId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<OrganizationId>(() => this.adUser.OrganizationId);
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x00086FF1 File Offset: 0x000851F1
		public string ImmutableId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adUser.ImmutableId);
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x00087011 File Offset: 0x00085211
		public Guid MailboxGuid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<Guid>(() => this.adUser.ExchangeGuid);
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x00087031 File Offset: 0x00085231
		public ADObjectId MailboxDatabase
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adUser.Database);
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x00087051 File Offset: 0x00085251
		public DateTime? WhenMailboxCreated
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.adUser.WhenMailboxCreated);
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x00087071 File Offset: 0x00085271
		public SmtpAddress WindowsLiveID
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SmtpAddress>(() => this.adUser.WindowsLiveID);
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x00087091 File Offset: 0x00085291
		public string ImmutableIdPartial
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adUser.ImmutableIdPartial);
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x000870B1 File Offset: 0x000852B1
		public NetID NetId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<NetID>(() => this.adUser.NetID);
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x000870D1 File Offset: 0x000852D1
		public ModernGroupObjectType ModernGroupType
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ModernGroupObjectType>(() => this.adUser.ModernGroupType);
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x000870F1 File Offset: 0x000852F1
		public IEnumerable<SecurityIdentifier> PublicToGroupSids
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<SecurityIdentifier>>(() => this.adUser.PublicToGroupSids);
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x00087111 File Offset: 0x00085311
		public string ExternalDirectoryObjectId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.adUser.ExternalDirectoryObjectId);
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06002071 RID: 8305 RVA: 0x00087131 File Offset: 0x00085331
		public ADObjectId ArchiveDatabase
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adUser.ArchiveDatabase);
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x00087151 File Offset: 0x00085351
		public Guid ArchiveGuid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<Guid>(() => this.adUser.ArchiveGuid);
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x00087171 File Offset: 0x00085371
		public IEnumerable<string> ArchiveName
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<string>>(() => this.adUser.ArchiveName);
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x00087191 File Offset: 0x00085391
		public ArchiveState ArchiveState
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ArchiveState>(() => this.adUser.ArchiveState);
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x000871B1 File Offset: 0x000853B1
		public ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ArchiveStatusFlags>(() => this.adUser.ArchiveStatus);
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x000871D1 File Offset: 0x000853D1
		public SmtpDomain ArchiveDomain
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SmtpDomain>(() => this.adUser.ArchiveDomain);
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x000871F1 File Offset: 0x000853F1
		public IEnumerable<Guid> AggregatedMailboxGuids
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<Guid>>(() => this.adUser.AggregatedMailboxGuids);
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x00087211 File Offset: 0x00085411
		public Uri SharePointUrl
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<Uri>(() => this.adUser.SharePointUrl);
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x00087231 File Offset: 0x00085431
		public bool IsMapiEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adUser.MAPIEnabled);
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x00087251 File Offset: 0x00085451
		public bool IsOwaEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adUser.OWAEnabled);
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x00087271 File Offset: 0x00085471
		public bool IsMowaEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adUser.MOWAEnabled);
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x00087291 File Offset: 0x00085491
		public ADObjectId ThrottlingPolicy
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adUser.ThrottlingPolicy);
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x000872B1 File Offset: 0x000854B1
		public ADObjectId OwaMailboxPolicy
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adUser.OwaMailboxPolicy);
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x000872D1 File Offset: 0x000854D1
		public ADObjectId MobileDeviceMailboxPolicy
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adUser.ActiveSyncMailboxPolicy);
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x000872F1 File Offset: 0x000854F1
		public ADObjectId AddressBookPolicy
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adUser.AddressBookPolicy);
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x00087311 File Offset: 0x00085511
		public bool IsPersonToPersonMessagingEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adUser.IsPersonToPersonTextMessagingEnabled);
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x00087331 File Offset: 0x00085531
		public bool IsMachineToPersonMessagingEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adUser.IsMachineToPersonTextMessagingEnabled);
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x00087351 File Offset: 0x00085551
		public Capability? SkuCapability
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<Capability?>(() => this.adUser.SKUCapability);
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x00087371 File Offset: 0x00085571
		public bool? SkuAssigned
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool?>(() => this.adUser.SKUAssigned);
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x00087391 File Offset: 0x00085591
		public bool IsMailboxAuditEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adUser.MailboxAuditEnabled);
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x000873B1 File Offset: 0x000855B1
		public bool BypassAudit
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.adUser.BypassAudit);
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x000873D1 File Offset: 0x000855D1
		public EnhancedTimeSpan MailboxAuditLogAgeLimit
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<EnhancedTimeSpan>(() => this.adUser.MailboxAuditLogAgeLimit);
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x000873F1 File Offset: 0x000855F1
		public MailboxAuditOperations AuditAdminOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.adUser.AuditAdminOperations);
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x00087411 File Offset: 0x00085611
		public MailboxAuditOperations AuditDelegateOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.adUser.AuditDelegateOperations);
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x00087431 File Offset: 0x00085631
		public MailboxAuditOperations AuditDelegateAdminOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.adUser.AuditDelegateAdminOperations);
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x00087451 File Offset: 0x00085651
		public MailboxAuditOperations AuditOwnerOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.adUser.AuditOwnerOperations);
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x00087471 File Offset: 0x00085671
		public DateTime? AuditLastAdminAccess
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.adUser.AuditLastAdminAccess);
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x00087491 File Offset: 0x00085691
		public DateTime? AuditLastDelegateAccess
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.adUser.AuditLastDelegateAccess);
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x000874B1 File Offset: 0x000856B1
		public DateTime? AuditLastExternalAccess
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.adUser.AuditLastExternalAccess);
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x000874D1 File Offset: 0x000856D1
		public ADObjectId QueryBaseDN
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.adUser.QueryBaseDN);
			}
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000874E4 File Offset: 0x000856E4
		public SmtpAddress GetFederatedSmtpAddress()
		{
			return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SmtpAddress>(new Func<SmtpAddress>(this.adUser.GetFederatedSmtpAddress));
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x0008750A File Offset: 0x0008570A
		public FederatedIdentity GetFederatedIdentity()
		{
			return DirectoryExtensions.GetWithDirectoryExceptionTranslation<FederatedIdentity>(() => FederatedIdentityHelper.GetFederatedIdentity(this.adUser));
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x00087542 File Offset: 0x00085742
		public IEnumerable<IMailboxLocationInfo> MailboxLocations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<IEnumerable<IMailboxLocationInfo>>(delegate
				{
					if (this.adUser.MailboxLocations == null)
					{
						return Enumerable.Empty<IMailboxLocationInfo>();
					}
					return this.adUser.MailboxLocations.GetMailboxLocations();
				});
			}
		}

		// Token: 0x040013AF RID: 5039
		private readonly IADUser adUser;
	}
}
