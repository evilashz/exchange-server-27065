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
	// Token: 0x020002EA RID: 746
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ADSystemMailboxGenericWrapper : IGenericADUser, IFederatedIdentityParameters
	{
		// Token: 0x06001FEF RID: 8175 RVA: 0x0008677F File Offset: 0x0008497F
		public ADSystemMailboxGenericWrapper(ADSystemMailbox systemMailbox)
		{
			ArgumentValidator.ThrowIfNull("systemMailbox", systemMailbox);
			this.systemMailbox = systemMailbox;
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x000867A6 File Offset: 0x000849A6
		public string DisplayName
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.systemMailbox.DisplayName);
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x000867B9 File Offset: 0x000849B9
		public string UserPrincipalName
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x000867D2 File Offset: 0x000849D2
		public string LegacyDn
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.systemMailbox.LegacyExchangeDN);
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x000867F2 File Offset: 0x000849F2
		public string Alias
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.systemMailbox.Alias);
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x00086812 File Offset: 0x00084A12
		public ADObjectId DefaultPublicFolderMailbox
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.systemMailbox.DefaultPublicFolderMailbox);
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x00086825 File Offset: 0x00084A25
		public SecurityIdentifier Sid
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x0008683E File Offset: 0x00084A3E
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SecurityIdentifier>(() => this.systemMailbox.MasterAccountSid);
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x00086851 File Offset: 0x00084A51
		public IEnumerable<SecurityIdentifier> SidHistory
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x0008686A File Offset: 0x00084A6A
		public IEnumerable<ADObjectId> GrantSendOnBehalfTo
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MultiValuedProperty<ADObjectId>>(() => this.systemMailbox.GrantSendOnBehalfTo);
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x0008687D File Offset: 0x00084A7D
		public IEnumerable<CultureInfo> Languages
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x00086896 File Offset: 0x00084A96
		public ADObjectId Id
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.systemMailbox.Id);
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x000868B6 File Offset: 0x00084AB6
		public RecipientType RecipientType
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<RecipientType>(() => this.systemMailbox.RecipientType);
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x000868D6 File Offset: 0x00084AD6
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<RecipientTypeDetails>(() => this.systemMailbox.RecipientTypeDetails);
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x000868F6 File Offset: 0x00084AF6
		public bool? IsResource
		{
			get
			{
				return new bool?(DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.systemMailbox.IsResource));
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x0008691B File Offset: 0x00084B1B
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<SmtpAddress>(() => this.systemMailbox.PrimarySmtpAddress);
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x0008693B File Offset: 0x00084B3B
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ProxyAddress>(() => this.systemMailbox.ExternalEmailAddress);
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x0008695B File Offset: 0x00084B5B
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ProxyAddressCollection>(() => this.systemMailbox.EmailAddresses);
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0008697B File Offset: 0x00084B7B
		public ADObjectId ObjectId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.systemMailbox.Id);
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x0008699B File Offset: 0x00084B9B
		public OrganizationId OrganizationId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<OrganizationId>(() => this.systemMailbox.OrganizationId);
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x000869BB File Offset: 0x00084BBB
		public string ImmutableId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.systemMailbox.ImmutableId);
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x000869DB File Offset: 0x00084BDB
		public Guid MailboxGuid
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<Guid>(() => this.systemMailbox.ExchangeGuid);
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x000869FB File Offset: 0x00084BFB
		public ADObjectId MailboxDatabase
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.systemMailbox.Database);
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002006 RID: 8198 RVA: 0x00086A0E File Offset: 0x00084C0E
		public DateTime? WhenMailboxCreated
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x00086A1A File Offset: 0x00084C1A
		public SmtpAddress WindowsLiveID
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x00086A33 File Offset: 0x00084C33
		public string ImmutableIdPartial
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.systemMailbox.ImmutableIdPartial);
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x00086A46 File Offset: 0x00084C46
		public NetID NetId
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x00086A52 File Offset: 0x00084C52
		public ModernGroupObjectType ModernGroupType
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x00086A5E File Offset: 0x00084C5E
		public IEnumerable<SecurityIdentifier> PublicToGroupSids
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x00086A77 File Offset: 0x00084C77
		public string ExternalDirectoryObjectId
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<string>(() => this.systemMailbox.ExternalDirectoryObjectId);
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x00086A8A File Offset: 0x00084C8A
		public ADObjectId ArchiveDatabase
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x00086A96 File Offset: 0x00084C96
		public Guid ArchiveGuid
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x00086AA2 File Offset: 0x00084CA2
		public IEnumerable<string> ArchiveName
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x00086AAE File Offset: 0x00084CAE
		public ArchiveState ArchiveState
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x00086ABA File Offset: 0x00084CBA
		public ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x00086AC6 File Offset: 0x00084CC6
		public SmtpDomain ArchiveDomain
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x00086AD2 File Offset: 0x00084CD2
		public IEnumerable<Guid> AggregatedMailboxGuids
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x00086ADE File Offset: 0x00084CDE
		public Uri SharePointUrl
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x00086AF7 File Offset: 0x00084CF7
		public bool IsMapiEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.systemMailbox.MAPIEnabled);
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x00086B17 File Offset: 0x00084D17
		public bool IsOwaEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.systemMailbox.OWAEnabled);
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x00086B37 File Offset: 0x00084D37
		public bool IsMowaEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.systemMailbox.MOWAEnabled);
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x00086B57 File Offset: 0x00084D57
		public ADObjectId ThrottlingPolicy
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.systemMailbox.ThrottlingPolicy);
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x00086B6A File Offset: 0x00084D6A
		public ADObjectId OwaMailboxPolicy
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x00086B76 File Offset: 0x00084D76
		public ADObjectId MobileDeviceMailboxPolicy
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x00086B8F File Offset: 0x00084D8F
		public ADObjectId AddressBookPolicy
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<ADObjectId>(() => this.systemMailbox.AddressBookPolicy);
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x00086BAF File Offset: 0x00084DAF
		public bool IsPersonToPersonMessagingEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.systemMailbox.IsPersonToPersonTextMessagingEnabled);
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x00086BCF File Offset: 0x00084DCF
		public bool IsMachineToPersonMessagingEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.systemMailbox.IsMachineToPersonTextMessagingEnabled);
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x00086BE2 File Offset: 0x00084DE2
		public Capability? SkuCapability
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x00086BEE File Offset: 0x00084DEE
		public bool? SkuAssigned
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x00086C07 File Offset: 0x00084E07
		public bool IsMailboxAuditEnabled
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.systemMailbox.MailboxAuditEnabled);
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x00086C27 File Offset: 0x00084E27
		public bool BypassAudit
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<bool>(() => this.systemMailbox.BypassAudit);
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x00086C47 File Offset: 0x00084E47
		public EnhancedTimeSpan MailboxAuditLogAgeLimit
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<EnhancedTimeSpan>(() => this.systemMailbox.MailboxAuditLogAgeLimit);
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x00086C67 File Offset: 0x00084E67
		public MailboxAuditOperations AuditAdminOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.systemMailbox.AuditAdminOperations);
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x00086C87 File Offset: 0x00084E87
		public MailboxAuditOperations AuditDelegateOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.systemMailbox.AuditDelegateOperations);
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x00086CA7 File Offset: 0x00084EA7
		public MailboxAuditOperations AuditDelegateAdminOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.systemMailbox.AuditDelegateAdminOperations);
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x00086CC7 File Offset: 0x00084EC7
		public MailboxAuditOperations AuditOwnerOperations
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<MailboxAuditOperations>(() => this.systemMailbox.AuditOwnerOperations);
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x00086CE7 File Offset: 0x00084EE7
		public DateTime? AuditLastAdminAccess
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.systemMailbox.AuditLastAdminAccess);
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x00086D07 File Offset: 0x00084F07
		public DateTime? AuditLastDelegateAccess
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.systemMailbox.AuditLastDelegateAccess);
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x00086D27 File Offset: 0x00084F27
		public DateTime? AuditLastExternalAccess
		{
			get
			{
				return DirectoryExtensions.GetWithDirectoryExceptionTranslation<DateTime?>(() => this.systemMailbox.AuditLastExternalAccess);
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x00086D3A File Offset: 0x00084F3A
		public ADObjectId QueryBaseDN
		{
			get
			{
				throw new InvalidOperationException("Property not supported");
			}
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00086D46 File Offset: 0x00084F46
		public SmtpAddress GetFederatedSmtpAddress()
		{
			throw new InvalidOperationException("Operation not supported");
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00086D52 File Offset: 0x00084F52
		public FederatedIdentity GetFederatedIdentity()
		{
			throw new InvalidOperationException("Operation not supported");
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x00086D5E File Offset: 0x00084F5E
		public IEnumerable<IMailboxLocationInfo> MailboxLocations
		{
			get
			{
				return Enumerable.Empty<IMailboxLocationInfo>();
			}
		}

		// Token: 0x040013AE RID: 5038
		private readonly ADSystemMailbox systemMailbox;
	}
}
