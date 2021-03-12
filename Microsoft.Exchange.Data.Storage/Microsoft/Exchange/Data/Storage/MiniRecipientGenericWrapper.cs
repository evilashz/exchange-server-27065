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
	// Token: 0x020002F2 RID: 754
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MiniRecipientGenericWrapper : IGenericADUser, IFederatedIdentityParameters
	{
		// Token: 0x060021E8 RID: 8680 RVA: 0x0008980D File Offset: 0x00087A0D
		public MiniRecipientGenericWrapper(StorageMiniRecipient storageMiniRecipient)
		{
			ArgumentValidator.ThrowIfNull("storageMiniRecipient", storageMiniRecipient);
			this.storageMiniRecipient = storageMiniRecipient;
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x00089834 File Offset: 0x00087A34
		public string DisplayName
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.storageMiniRecipient.DisplayName);
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x00089854 File Offset: 0x00087A54
		public string UserPrincipalName
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.storageMiniRecipient.UserPrincipalName);
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x00089874 File Offset: 0x00087A74
		public string LegacyDn
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.storageMiniRecipient.LegacyExchangeDN);
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x00089894 File Offset: 0x00087A94
		public string Alias
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.storageMiniRecipient.Alias);
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x000898B4 File Offset: 0x00087AB4
		public ADObjectId DefaultPublicFolderMailbox
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.storageMiniRecipient.DefaultPublicFolderMailbox);
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x000898D4 File Offset: 0x00087AD4
		public SecurityIdentifier Sid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SecurityIdentifier>(() => this.storageMiniRecipient.Sid);
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x000898F4 File Offset: 0x00087AF4
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SecurityIdentifier>(() => this.storageMiniRecipient.MasterAccountSid);
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x00089914 File Offset: 0x00087B14
		public IEnumerable<SecurityIdentifier> SidHistory
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<SecurityIdentifier>>(() => this.storageMiniRecipient.SidHistory);
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x00089934 File Offset: 0x00087B34
		public IEnumerable<ADObjectId> GrantSendOnBehalfTo
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<ADObjectId>>(() => this.storageMiniRecipient.GrantSendOnBehalfTo);
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x00089954 File Offset: 0x00087B54
		public IEnumerable<CultureInfo> Languages
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<CultureInfo>>(() => this.storageMiniRecipient.Languages);
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x00089974 File Offset: 0x00087B74
		public RecipientType RecipientType
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<RecipientType>(() => this.storageMiniRecipient.RecipientType);
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x00089994 File Offset: 0x00087B94
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<RecipientTypeDetails>(() => this.storageMiniRecipient.RecipientTypeDetails);
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x000899B4 File Offset: 0x00087BB4
		public bool? IsResource
		{
			get
			{
				return new bool?(DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.storageMiniRecipient.IsResource));
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x000899D9 File Offset: 0x00087BD9
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SmtpAddress>(() => this.storageMiniRecipient.PrimarySmtpAddress);
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x000899F9 File Offset: 0x00087BF9
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ProxyAddress>(() => this.storageMiniRecipient.ExternalEmailAddress);
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x00089A19 File Offset: 0x00087C19
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ProxyAddressCollection>(() => this.storageMiniRecipient.EmailAddresses);
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x00089A39 File Offset: 0x00087C39
		public ADObjectId ObjectId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.storageMiniRecipient.Id);
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x00089A59 File Offset: 0x00087C59
		public OrganizationId OrganizationId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<OrganizationId>(() => this.storageMiniRecipient.OrganizationId);
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x00089A79 File Offset: 0x00087C79
		public string ImmutableId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.storageMiniRecipient.ImmutableId);
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x00089A99 File Offset: 0x00087C99
		public Guid MailboxGuid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<Guid>(() => this.storageMiniRecipient.ExchangeGuid);
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x00089AB9 File Offset: 0x00087CB9
		public ADObjectId MailboxDatabase
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.storageMiniRecipient.Database);
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x00089AD9 File Offset: 0x00087CD9
		public DateTime? WhenMailboxCreated
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.storageMiniRecipient.WhenMailboxCreated);
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x00089AF9 File Offset: 0x00087CF9
		public SmtpAddress WindowsLiveID
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SmtpAddress>(() => this.storageMiniRecipient.WindowsLiveID);
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x00089B19 File Offset: 0x00087D19
		public string ImmutableIdPartial
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.storageMiniRecipient.ImmutableIdPartial);
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x00089B39 File Offset: 0x00087D39
		public NetID NetId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<NetID>(() => this.storageMiniRecipient.NetID);
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x00089B59 File Offset: 0x00087D59
		public ModernGroupObjectType ModernGroupType
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ModernGroupObjectType>(() => this.storageMiniRecipient.ModernGroupType);
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x00089B79 File Offset: 0x00087D79
		public IEnumerable<SecurityIdentifier> PublicToGroupSids
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<SecurityIdentifier>>(() => this.storageMiniRecipient.PublicToGroupSids);
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x00089B99 File Offset: 0x00087D99
		public string ExternalDirectoryObjectId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.storageMiniRecipient.ExternalDirectoryObjectId);
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x00089BB9 File Offset: 0x00087DB9
		public ADObjectId ArchiveDatabase
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.storageMiniRecipient.ArchiveDatabase);
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x00089BD9 File Offset: 0x00087DD9
		public Guid ArchiveGuid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<Guid>(() => this.storageMiniRecipient.ArchiveGuid);
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x00089BF9 File Offset: 0x00087DF9
		public IEnumerable<string> ArchiveName
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<string>>(() => this.storageMiniRecipient.ArchiveName);
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x00089C19 File Offset: 0x00087E19
		public ArchiveState ArchiveState
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ArchiveState>(() => this.storageMiniRecipient.ArchiveState);
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x00089C39 File Offset: 0x00087E39
		public ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ArchiveStatusFlags>(() => this.storageMiniRecipient.ArchiveStatus);
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x00089C59 File Offset: 0x00087E59
		public SmtpDomain ArchiveDomain
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SmtpDomain>(() => this.storageMiniRecipient.ArchiveDomain);
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x00089C79 File Offset: 0x00087E79
		public IEnumerable<Guid> AggregatedMailboxGuids
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<Guid>>(() => this.storageMiniRecipient.AggregatedMailboxGuids);
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x00089C99 File Offset: 0x00087E99
		public Uri SharePointUrl
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<Uri>(() => this.storageMiniRecipient.SharePointUrl);
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x00089CB9 File Offset: 0x00087EB9
		public bool IsMapiEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.storageMiniRecipient.MAPIEnabled);
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x00089CD9 File Offset: 0x00087ED9
		public bool IsOwaEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.storageMiniRecipient.OWAEnabled);
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x00089CF9 File Offset: 0x00087EF9
		public bool IsMowaEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.storageMiniRecipient.MOWAEnabled);
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x00089D19 File Offset: 0x00087F19
		public ADObjectId ThrottlingPolicy
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.storageMiniRecipient.ThrottlingPolicy);
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x00089D39 File Offset: 0x00087F39
		public ADObjectId OwaMailboxPolicy
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.storageMiniRecipient.OwaMailboxPolicy);
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x00089D59 File Offset: 0x00087F59
		public ADObjectId MobileDeviceMailboxPolicy
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.storageMiniRecipient.MobileDeviceMailboxPolicy);
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x00089D79 File Offset: 0x00087F79
		public ADObjectId AddressBookPolicy
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.storageMiniRecipient.AddressBookPolicy);
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x00089D99 File Offset: 0x00087F99
		public bool IsPersonToPersonMessagingEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.storageMiniRecipient.IsPersonToPersonTextMessagingEnabled);
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x00089DB9 File Offset: 0x00087FB9
		public bool IsMachineToPersonMessagingEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.storageMiniRecipient.IsMachineToPersonTextMessagingEnabled);
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x00089DD9 File Offset: 0x00087FD9
		public Capability? SkuCapability
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<Capability?>(() => this.storageMiniRecipient.SKUCapability);
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x00089DF9 File Offset: 0x00087FF9
		public bool? SkuAssigned
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool?>(() => this.storageMiniRecipient.SKUAssigned);
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x00089E19 File Offset: 0x00088019
		public bool IsMailboxAuditEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.storageMiniRecipient.MailboxAuditEnabled);
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x00089E39 File Offset: 0x00088039
		public bool BypassAudit
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.storageMiniRecipient.BypassAudit);
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600221A RID: 8730 RVA: 0x00089E59 File Offset: 0x00088059
		public EnhancedTimeSpan MailboxAuditLogAgeLimit
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<EnhancedTimeSpan>(() => this.storageMiniRecipient.MailboxAuditLogAgeLimit);
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x00089E79 File Offset: 0x00088079
		public MailboxAuditOperations AuditAdminOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.storageMiniRecipient.AuditAdminOperations);
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x00089E99 File Offset: 0x00088099
		public MailboxAuditOperations AuditDelegateOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.storageMiniRecipient.AuditDelegateOperations);
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x00089EB9 File Offset: 0x000880B9
		public MailboxAuditOperations AuditDelegateAdminOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.storageMiniRecipient.AuditDelegateAdminOperations);
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x00089ED9 File Offset: 0x000880D9
		public MailboxAuditOperations AuditOwnerOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.storageMiniRecipient.AuditOwnerOperations);
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x00089EF9 File Offset: 0x000880F9
		public DateTime? AuditLastAdminAccess
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.storageMiniRecipient.AuditLastAdminAccess);
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x00089F19 File Offset: 0x00088119
		public DateTime? AuditLastDelegateAccess
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.storageMiniRecipient.AuditLastDelegateAccess);
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x00089F39 File Offset: 0x00088139
		public DateTime? AuditLastExternalAccess
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.storageMiniRecipient.AuditLastExternalAccess);
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x00089F59 File Offset: 0x00088159
		public ADObjectId QueryBaseDN
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.storageMiniRecipient.QueryBaseDN);
			}
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x00089F6C File Offset: 0x0008816C
		public SmtpAddress GetFederatedSmtpAddress()
		{
			return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SmtpAddress>(new Func<SmtpAddress>(this.storageMiniRecipient.GetFederatedSmtpAddress));
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00089F84 File Offset: 0x00088184
		public FederatedIdentity GetFederatedIdentity()
		{
			return DirectoryExtensions.GetWithDirectoryExceptionTranslation<FederatedIdentity>(new Func<FederatedIdentity>(this.storageMiniRecipient.GetFederatedIdentity));
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x00089F9C File Offset: 0x0008819C
		public IEnumerable<IMailboxLocationInfo> MailboxLocations
		{
			get
			{
				return Enumerable.Empty<IMailboxLocationInfo>();
			}
		}

		// Token: 0x040013F0 RID: 5104
		private readonly StorageMiniRecipient storageMiniRecipient;
	}
}
