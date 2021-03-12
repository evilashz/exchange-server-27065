using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002F1 RID: 753
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GenericADUser : IGenericADUser, IFederatedIdentityParameters
	{
		// Token: 0x06002165 RID: 8549 RVA: 0x000888F4 File Offset: 0x00086AF4
		public GenericADUser()
		{
			this.Alias = string.Empty;
			this.RecipientType = RecipientType.Invalid;
			this.RecipientTypeDetails = RecipientTypeDetails.None;
			this.GrantSendOnBehalfTo = new ADMultiValuedProperty<ADObjectId>();
			this.Languages = new PreferredCultures();
			this.IsResource = null;
			this.EmailAddresses = new ProxyAddressCollection();
			this.AggregatedMailboxGuids = Array<Guid>.Empty;
			this.MailboxLocations = Enumerable.Empty<IMailboxLocationInfo>();
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00088968 File Offset: 0x00086B68
		public GenericADUser(IGenericADUser adUser)
		{
			ArgumentValidator.ThrowIfNull("adUser", adUser);
			this.ObjectId = adUser.ObjectId;
			this.DisplayName = adUser.DisplayName;
			this.UserPrincipalName = adUser.UserPrincipalName;
			this.LegacyDn = adUser.LegacyDn;
			this.Alias = adUser.Alias;
			this.DefaultPublicFolderMailbox = adUser.DefaultPublicFolderMailbox;
			this.Sid = adUser.Sid;
			this.MasterAccountSid = adUser.MasterAccountSid;
			this.SidHistory = adUser.SidHistory;
			this.GrantSendOnBehalfTo = adUser.GrantSendOnBehalfTo;
			this.Languages = adUser.Languages;
			this.RecipientType = adUser.RecipientType;
			this.RecipientTypeDetails = adUser.RecipientTypeDetails;
			this.IsResource = adUser.IsResource;
			this.PrimarySmtpAddress = adUser.PrimarySmtpAddress;
			this.ExternalEmailAddress = adUser.ExternalEmailAddress;
			this.EmailAddresses = adUser.EmailAddresses;
			this.OrganizationId = adUser.OrganizationId;
			this.MailboxGuid = adUser.MailboxGuid;
			this.MailboxDatabase = adUser.MailboxDatabase;
			this.WhenMailboxCreated = adUser.WhenMailboxCreated;
			this.WindowsLiveID = adUser.WindowsLiveID;
			this.NetId = adUser.NetId;
			this.ModernGroupType = adUser.ModernGroupType;
			this.PublicToGroupSids = adUser.PublicToGroupSids;
			this.ExternalDirectoryObjectId = adUser.ExternalDirectoryObjectId;
			this.ArchiveDatabase = adUser.ArchiveDatabase;
			this.ArchiveGuid = adUser.ArchiveGuid;
			this.ArchiveName = adUser.ArchiveName;
			this.ArchiveState = adUser.ArchiveState;
			this.ArchiveStatus = adUser.ArchiveStatus;
			this.ArchiveDomain = adUser.ArchiveDomain;
			this.AggregatedMailboxGuids = adUser.AggregatedMailboxGuids;
			this.SharePointUrl = adUser.SharePointUrl;
			this.IsMapiEnabled = adUser.IsMapiEnabled;
			this.IsOwaEnabled = adUser.IsOwaEnabled;
			this.IsMowaEnabled = adUser.IsMowaEnabled;
			this.ThrottlingPolicy = adUser.ThrottlingPolicy;
			this.OwaMailboxPolicy = adUser.OwaMailboxPolicy;
			this.MobileDeviceMailboxPolicy = adUser.MobileDeviceMailboxPolicy;
			this.AddressBookPolicy = adUser.AddressBookPolicy;
			this.IsPersonToPersonMessagingEnabled = adUser.IsPersonToPersonMessagingEnabled;
			this.IsMachineToPersonMessagingEnabled = adUser.IsMachineToPersonMessagingEnabled;
			this.SkuCapability = adUser.SkuCapability;
			this.SkuAssigned = adUser.SkuAssigned;
			this.IsMailboxAuditEnabled = adUser.IsMailboxAuditEnabled;
			this.BypassAudit = adUser.BypassAudit;
			this.MailboxAuditLogAgeLimit = adUser.MailboxAuditLogAgeLimit;
			this.AuditAdminOperations = adUser.AuditAdminOperations;
			this.AuditDelegateOperations = adUser.AuditDelegateOperations;
			this.AuditDelegateAdminOperations = adUser.AuditDelegateAdminOperations;
			this.AuditOwnerOperations = adUser.AuditOwnerOperations;
			this.AuditLastAdminAccess = adUser.AuditLastAdminAccess;
			this.AuditLastDelegateAccess = adUser.AuditLastDelegateAccess;
			this.AuditLastExternalAccess = adUser.AuditLastExternalAccess;
			this.QueryBaseDN = adUser.QueryBaseDN;
			this.MailboxLocations = adUser.MailboxLocations;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x00088C3C File Offset: 0x00086E3C
		public GenericADUser(IExchangePrincipal exchangePrincipal)
		{
			ArgumentValidator.ThrowIfNull("exchangePrincipal", exchangePrincipal);
			this.LegacyDn = exchangePrincipal.LegacyDn;
			this.Alias = exchangePrincipal.Alias;
			this.DefaultPublicFolderMailbox = exchangePrincipal.DefaultPublicFolderMailbox;
			this.Sid = exchangePrincipal.Sid;
			this.MasterAccountSid = exchangePrincipal.MasterAccountSid;
			this.SidHistory = exchangePrincipal.SidHistory;
			this.GrantSendOnBehalfTo = exchangePrincipal.Delegates;
			this.Languages = exchangePrincipal.PreferredCultures;
			this.ObjectId = exchangePrincipal.ObjectId;
			this.RecipientType = exchangePrincipal.RecipientType;
			this.RecipientTypeDetails = exchangePrincipal.RecipientTypeDetails;
			this.IsResource = exchangePrincipal.IsResource;
			this.AggregatedMailboxGuids = exchangePrincipal.AggregatedMailboxGuids;
			this.ModernGroupType = exchangePrincipal.ModernGroupType;
			this.PublicToGroupSids = exchangePrincipal.PublicToGroupSids;
			this.ExternalDirectoryObjectId = exchangePrincipal.ExternalDirectoryObjectId;
			if (exchangePrincipal.MailboxInfo != null)
			{
				this.DisplayName = exchangePrincipal.MailboxInfo.DisplayName;
				this.PrimarySmtpAddress = exchangePrincipal.MailboxInfo.PrimarySmtpAddress;
				this.ExternalEmailAddress = exchangePrincipal.MailboxInfo.ExternalEmailAddress;
				this.EmailAddresses = new ProxyAddressCollection();
				foreach (ProxyAddress item in exchangePrincipal.MailboxInfo.EmailAddresses)
				{
					this.EmailAddresses.Add(item);
				}
				this.OrganizationId = exchangePrincipal.MailboxInfo.OrganizationId;
				this.MailboxGuid = exchangePrincipal.MailboxInfo.MailboxGuid;
				this.MailboxDatabase = exchangePrincipal.MailboxInfo.MailboxDatabase;
				this.WhenMailboxCreated = exchangePrincipal.MailboxInfo.WhenMailboxCreated;
				IUserPrincipal userPrincipal = exchangePrincipal as IUserPrincipal;
				if (userPrincipal != null)
				{
					this.WindowsLiveID = userPrincipal.WindowsLiveId;
					this.NetId = userPrincipal.NetId;
				}
				IMailboxInfo mailboxInfo = exchangePrincipal.AllMailboxes.FirstOrDefault((IMailboxInfo mailbox) => mailbox.IsArchive);
				if (mailboxInfo != null)
				{
					this.ArchiveGuid = mailboxInfo.MailboxGuid;
					this.ArchiveDatabase = mailboxInfo.MailboxDatabase;
					this.ArchiveName = new string[]
					{
						mailboxInfo.ArchiveName
					};
					this.ArchiveState = mailboxInfo.ArchiveState;
					this.ArchiveStatus = mailboxInfo.ArchiveStatus;
				}
				this.SharePointUrl = exchangePrincipal.MailboxInfo.Configuration.SharePointUrl;
				this.IsMapiEnabled = exchangePrincipal.MailboxInfo.Configuration.IsMapiEnabled;
				this.IsOwaEnabled = exchangePrincipal.MailboxInfo.Configuration.IsOwaEnabled;
				this.IsMowaEnabled = exchangePrincipal.MailboxInfo.Configuration.IsMowaEnabled;
				this.ThrottlingPolicy = exchangePrincipal.MailboxInfo.Configuration.ThrottlingPolicy;
				this.OwaMailboxPolicy = exchangePrincipal.MailboxInfo.Configuration.OwaMailboxPolicy;
				this.MobileDeviceMailboxPolicy = exchangePrincipal.MailboxInfo.Configuration.MobileDeviceMailboxPolicy;
				this.AddressBookPolicy = exchangePrincipal.MailboxInfo.Configuration.AddressBookPolicy;
				this.IsPersonToPersonMessagingEnabled = exchangePrincipal.MailboxInfo.Configuration.IsPersonToPersonMessagingEnabled;
				this.IsMachineToPersonMessagingEnabled = exchangePrincipal.MailboxInfo.Configuration.IsMachineToPersonMessagingEnabled;
				this.SkuCapability = exchangePrincipal.MailboxInfo.Configuration.SkuCapability;
				this.SkuAssigned = exchangePrincipal.MailboxInfo.Configuration.SkuAssigned;
				this.IsMailboxAuditEnabled = exchangePrincipal.MailboxInfo.Configuration.IsMailboxAuditEnabled;
				this.BypassAudit = exchangePrincipal.MailboxInfo.Configuration.BypassAudit;
				this.MailboxAuditLogAgeLimit = exchangePrincipal.MailboxInfo.Configuration.MailboxAuditLogAgeLimit;
				this.AuditAdminOperations = exchangePrincipal.MailboxInfo.Configuration.AuditAdminOperations;
				this.AuditDelegateOperations = exchangePrincipal.MailboxInfo.Configuration.AuditDelegateOperations;
				this.AuditDelegateAdminOperations = exchangePrincipal.MailboxInfo.Configuration.AuditDelegateAdminOperations;
				this.AuditOwnerOperations = exchangePrincipal.MailboxInfo.Configuration.AuditOwnerOperations;
				this.AuditLastAdminAccess = exchangePrincipal.MailboxInfo.Configuration.AuditLastAdminAccess;
				this.AuditLastDelegateAccess = exchangePrincipal.MailboxInfo.Configuration.AuditLastDelegateAccess;
				this.AuditLastExternalAccess = exchangePrincipal.MailboxInfo.Configuration.AuditLastExternalAccess;
				this.MailboxLocations = Enumerable.Empty<IMailboxLocationInfo>();
			}
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x00089078 File Offset: 0x00087278
		public GenericADUser(MiniRecipient miniRecipient)
		{
			ArgumentValidator.ThrowIfNull("miniRecipient", miniRecipient);
			this.UserPrincipalName = miniRecipient.UserPrincipalName;
			this.LegacyDn = miniRecipient.LegacyExchangeDN;
			this.DefaultPublicFolderMailbox = miniRecipient.DefaultPublicFolderMailbox;
			this.Sid = miniRecipient.Sid;
			this.MasterAccountSid = miniRecipient.MasterAccountSid;
			this.SidHistory = miniRecipient.SidHistory;
			this.GrantSendOnBehalfTo = miniRecipient.GrantSendOnBehalfTo;
			this.Languages = miniRecipient.Languages;
			this.ObjectId = miniRecipient.Id;
			this.RecipientType = miniRecipient.RecipientType;
			this.RecipientTypeDetails = miniRecipient.RecipientTypeDetails;
			this.IsResource = new bool?(miniRecipient.IsResource);
			this.AggregatedMailboxGuids = miniRecipient.AggregatedMailboxGuids;
			this.ModernGroupType = miniRecipient.ModernGroupType;
			this.PublicToGroupSids = miniRecipient.PublicToGroupSids;
			this.ExternalDirectoryObjectId = miniRecipient.ExternalDirectoryObjectId;
			this.DisplayName = miniRecipient.DisplayName;
			this.PrimarySmtpAddress = miniRecipient.PrimarySmtpAddress;
			this.ExternalEmailAddress = miniRecipient.ExternalEmailAddress;
			this.EmailAddresses = new ProxyAddressCollection();
			foreach (ProxyAddress item in miniRecipient.EmailAddresses)
			{
				this.EmailAddresses.Add(item);
			}
			this.OrganizationId = miniRecipient.OrganizationId;
			this.MailboxGuid = miniRecipient.ExchangeGuid;
			this.MailboxDatabase = miniRecipient.Database;
			this.WhenMailboxCreated = miniRecipient.WhenMailboxCreated;
			this.WindowsLiveID = miniRecipient.WindowsLiveID;
			this.NetId = miniRecipient.NetID;
			this.ArchiveGuid = miniRecipient.ArchiveGuid;
			this.ArchiveDatabase = miniRecipient.ArchiveDatabase;
			this.ArchiveName = miniRecipient.ArchiveName;
			this.ArchiveState = miniRecipient.ArchiveState;
			this.SharePointUrl = miniRecipient.SharePointUrl;
			this.IsMapiEnabled = miniRecipient.MAPIEnabled;
			this.IsOwaEnabled = miniRecipient.OWAEnabled;
			this.IsMowaEnabled = miniRecipient.MOWAEnabled;
			this.ThrottlingPolicy = miniRecipient.ThrottlingPolicy;
			this.OwaMailboxPolicy = miniRecipient.OwaMailboxPolicy;
			this.MobileDeviceMailboxPolicy = miniRecipient.MobileDeviceMailboxPolicy;
			this.AddressBookPolicy = miniRecipient.AddressBookPolicy;
			this.IsPersonToPersonMessagingEnabled = miniRecipient.IsPersonToPersonTextMessagingEnabled;
			this.IsMachineToPersonMessagingEnabled = miniRecipient.IsMachineToPersonTextMessagingEnabled;
			this.SkuCapability = miniRecipient.SKUCapability;
			this.SkuAssigned = miniRecipient.SKUAssigned;
			this.IsMailboxAuditEnabled = miniRecipient.MailboxAuditEnabled;
			this.BypassAudit = miniRecipient.BypassAudit;
			this.MailboxAuditLogAgeLimit = miniRecipient.MailboxAuditLogAgeLimit;
			this.AuditAdminOperations = miniRecipient.AuditAdminOperations;
			this.AuditDelegateOperations = miniRecipient.AuditDelegateOperations;
			this.AuditDelegateAdminOperations = miniRecipient.AuditDelegateAdminOperations;
			this.AuditOwnerOperations = miniRecipient.AuditOwnerOperations;
			this.AuditLastAdminAccess = miniRecipient.AuditLastAdminAccess;
			this.AuditLastDelegateAccess = miniRecipient.AuditLastDelegateAccess;
			this.AuditLastExternalAccess = miniRecipient.AuditLastExternalAccess;
			this.MailboxLocations = Enumerable.Empty<IMailboxLocationInfo>();
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x00089360 File Offset: 0x00087560
		// (set) Token: 0x0600216A RID: 8554 RVA: 0x00089368 File Offset: 0x00087568
		public string DisplayName { get; set; }

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x0600216B RID: 8555 RVA: 0x00089371 File Offset: 0x00087571
		// (set) Token: 0x0600216C RID: 8556 RVA: 0x00089379 File Offset: 0x00087579
		public string UserPrincipalName { get; set; }

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x00089382 File Offset: 0x00087582
		// (set) Token: 0x0600216E RID: 8558 RVA: 0x0008938A File Offset: 0x0008758A
		public string LegacyDn { get; set; }

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600216F RID: 8559 RVA: 0x00089393 File Offset: 0x00087593
		// (set) Token: 0x06002170 RID: 8560 RVA: 0x0008939B File Offset: 0x0008759B
		public string Alias { get; set; }

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x000893A4 File Offset: 0x000875A4
		// (set) Token: 0x06002172 RID: 8562 RVA: 0x000893AC File Offset: 0x000875AC
		public ADObjectId DefaultPublicFolderMailbox { get; set; }

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002173 RID: 8563 RVA: 0x000893B5 File Offset: 0x000875B5
		// (set) Token: 0x06002174 RID: 8564 RVA: 0x000893BD File Offset: 0x000875BD
		public SecurityIdentifier Sid { get; set; }

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x000893C6 File Offset: 0x000875C6
		// (set) Token: 0x06002176 RID: 8566 RVA: 0x000893CE File Offset: 0x000875CE
		public SecurityIdentifier MasterAccountSid { get; set; }

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x000893D7 File Offset: 0x000875D7
		// (set) Token: 0x06002178 RID: 8568 RVA: 0x000893DF File Offset: 0x000875DF
		public IEnumerable<SecurityIdentifier> SidHistory { get; set; }

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x000893E8 File Offset: 0x000875E8
		// (set) Token: 0x0600217A RID: 8570 RVA: 0x000893F0 File Offset: 0x000875F0
		public IEnumerable<ADObjectId> GrantSendOnBehalfTo { get; set; }

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x000893F9 File Offset: 0x000875F9
		// (set) Token: 0x0600217C RID: 8572 RVA: 0x00089401 File Offset: 0x00087601
		public IEnumerable<CultureInfo> Languages { get; set; }

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x0008940A File Offset: 0x0008760A
		// (set) Token: 0x0600217E RID: 8574 RVA: 0x00089412 File Offset: 0x00087612
		public RecipientType RecipientType { get; set; }

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x0008941B File Offset: 0x0008761B
		// (set) Token: 0x06002180 RID: 8576 RVA: 0x00089423 File Offset: 0x00087623
		public RecipientTypeDetails RecipientTypeDetails { get; set; }

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x0008942C File Offset: 0x0008762C
		// (set) Token: 0x06002182 RID: 8578 RVA: 0x00089434 File Offset: 0x00087634
		public bool? IsResource { get; set; }

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x0008943D File Offset: 0x0008763D
		// (set) Token: 0x06002184 RID: 8580 RVA: 0x00089445 File Offset: 0x00087645
		public SmtpAddress PrimarySmtpAddress { get; set; }

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x0008944E File Offset: 0x0008764E
		// (set) Token: 0x06002186 RID: 8582 RVA: 0x00089456 File Offset: 0x00087656
		public ProxyAddress ExternalEmailAddress { get; set; }

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x0008945F File Offset: 0x0008765F
		// (set) Token: 0x06002188 RID: 8584 RVA: 0x00089467 File Offset: 0x00087667
		public ProxyAddressCollection EmailAddresses { get; set; }

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x00089470 File Offset: 0x00087670
		// (set) Token: 0x0600218A RID: 8586 RVA: 0x00089478 File Offset: 0x00087678
		public ADObjectId ObjectId { get; set; }

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x00089481 File Offset: 0x00087681
		// (set) Token: 0x0600218C RID: 8588 RVA: 0x00089489 File Offset: 0x00087689
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x00089492 File Offset: 0x00087692
		// (set) Token: 0x0600218E RID: 8590 RVA: 0x0008949A File Offset: 0x0008769A
		public string ImmutableId { get; set; }

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x000894A3 File Offset: 0x000876A3
		// (set) Token: 0x06002190 RID: 8592 RVA: 0x000894AB File Offset: 0x000876AB
		public Guid MailboxGuid { get; set; }

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x000894B4 File Offset: 0x000876B4
		// (set) Token: 0x06002192 RID: 8594 RVA: 0x000894BC File Offset: 0x000876BC
		public ADObjectId MailboxDatabase { get; set; }

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x000894C5 File Offset: 0x000876C5
		// (set) Token: 0x06002194 RID: 8596 RVA: 0x000894CD File Offset: 0x000876CD
		public DateTime? WhenMailboxCreated { get; set; }

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x000894D6 File Offset: 0x000876D6
		// (set) Token: 0x06002196 RID: 8598 RVA: 0x000894DE File Offset: 0x000876DE
		public SmtpAddress WindowsLiveID { get; set; }

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x000894E7 File Offset: 0x000876E7
		// (set) Token: 0x06002198 RID: 8600 RVA: 0x000894EF File Offset: 0x000876EF
		public string ImmutableIdPartial { get; private set; }

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x000894F8 File Offset: 0x000876F8
		// (set) Token: 0x0600219A RID: 8602 RVA: 0x00089500 File Offset: 0x00087700
		public NetID NetId { get; set; }

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x00089509 File Offset: 0x00087709
		// (set) Token: 0x0600219C RID: 8604 RVA: 0x00089511 File Offset: 0x00087711
		public ModernGroupObjectType ModernGroupType { get; set; }

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x0008951A File Offset: 0x0008771A
		// (set) Token: 0x0600219E RID: 8606 RVA: 0x00089522 File Offset: 0x00087722
		public IEnumerable<SecurityIdentifier> PublicToGroupSids { get; set; }

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x0008952B File Offset: 0x0008772B
		// (set) Token: 0x060021A0 RID: 8608 RVA: 0x00089533 File Offset: 0x00087733
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x0008953C File Offset: 0x0008773C
		// (set) Token: 0x060021A2 RID: 8610 RVA: 0x00089544 File Offset: 0x00087744
		public ADObjectId ArchiveDatabase { get; set; }

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x0008954D File Offset: 0x0008774D
		// (set) Token: 0x060021A4 RID: 8612 RVA: 0x00089555 File Offset: 0x00087755
		public Guid ArchiveGuid { get; set; }

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x0008955E File Offset: 0x0008775E
		// (set) Token: 0x060021A6 RID: 8614 RVA: 0x00089566 File Offset: 0x00087766
		public IEnumerable<string> ArchiveName { get; set; }

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x0008956F File Offset: 0x0008776F
		// (set) Token: 0x060021A8 RID: 8616 RVA: 0x00089577 File Offset: 0x00087777
		public ArchiveState ArchiveState { get; set; }

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x00089580 File Offset: 0x00087780
		// (set) Token: 0x060021AA RID: 8618 RVA: 0x00089588 File Offset: 0x00087788
		public ArchiveStatusFlags ArchiveStatus { get; set; }

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x00089591 File Offset: 0x00087791
		// (set) Token: 0x060021AC RID: 8620 RVA: 0x00089599 File Offset: 0x00087799
		public SmtpDomain ArchiveDomain { get; set; }

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x000895A2 File Offset: 0x000877A2
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x000895AA File Offset: 0x000877AA
		public IEnumerable<Guid> AggregatedMailboxGuids { get; set; }

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x000895B3 File Offset: 0x000877B3
		// (set) Token: 0x060021B0 RID: 8624 RVA: 0x000895BB File Offset: 0x000877BB
		public IEnumerable<IMailboxLocationInfo> MailboxLocations { get; set; }

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x000895C4 File Offset: 0x000877C4
		// (set) Token: 0x060021B2 RID: 8626 RVA: 0x000895CC File Offset: 0x000877CC
		public Uri SharePointUrl { get; set; }

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x000895D5 File Offset: 0x000877D5
		// (set) Token: 0x060021B4 RID: 8628 RVA: 0x000895DD File Offset: 0x000877DD
		public bool IsMapiEnabled { get; set; }

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x000895E6 File Offset: 0x000877E6
		// (set) Token: 0x060021B6 RID: 8630 RVA: 0x000895EE File Offset: 0x000877EE
		public bool IsOwaEnabled { get; set; }

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x000895F7 File Offset: 0x000877F7
		// (set) Token: 0x060021B8 RID: 8632 RVA: 0x000895FF File Offset: 0x000877FF
		public bool IsMowaEnabled { get; set; }

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x00089608 File Offset: 0x00087808
		// (set) Token: 0x060021BA RID: 8634 RVA: 0x00089610 File Offset: 0x00087810
		public ADObjectId ThrottlingPolicy { get; set; }

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x00089619 File Offset: 0x00087819
		// (set) Token: 0x060021BC RID: 8636 RVA: 0x00089621 File Offset: 0x00087821
		public ADObjectId OwaMailboxPolicy { get; set; }

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x0008962A File Offset: 0x0008782A
		// (set) Token: 0x060021BE RID: 8638 RVA: 0x00089632 File Offset: 0x00087832
		public ADObjectId MobileDeviceMailboxPolicy { get; set; }

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x0008963B File Offset: 0x0008783B
		// (set) Token: 0x060021C0 RID: 8640 RVA: 0x00089643 File Offset: 0x00087843
		public ADObjectId AddressBookPolicy { get; set; }

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x0008964C File Offset: 0x0008784C
		// (set) Token: 0x060021C2 RID: 8642 RVA: 0x00089654 File Offset: 0x00087854
		public bool IsPersonToPersonMessagingEnabled { get; set; }

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x0008965D File Offset: 0x0008785D
		// (set) Token: 0x060021C4 RID: 8644 RVA: 0x00089665 File Offset: 0x00087865
		public bool IsMachineToPersonMessagingEnabled { get; set; }

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x0008966E File Offset: 0x0008786E
		// (set) Token: 0x060021C6 RID: 8646 RVA: 0x00089676 File Offset: 0x00087876
		public Capability? SkuCapability { get; set; }

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x0008967F File Offset: 0x0008787F
		// (set) Token: 0x060021C8 RID: 8648 RVA: 0x00089687 File Offset: 0x00087887
		public bool? SkuAssigned { get; set; }

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x00089690 File Offset: 0x00087890
		// (set) Token: 0x060021CA RID: 8650 RVA: 0x00089698 File Offset: 0x00087898
		public bool IsMailboxAuditEnabled { get; set; }

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x000896A1 File Offset: 0x000878A1
		// (set) Token: 0x060021CC RID: 8652 RVA: 0x000896A9 File Offset: 0x000878A9
		public bool BypassAudit { get; set; }

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x000896B2 File Offset: 0x000878B2
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x000896BA File Offset: 0x000878BA
		public EnhancedTimeSpan MailboxAuditLogAgeLimit { get; set; }

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x000896C3 File Offset: 0x000878C3
		// (set) Token: 0x060021D0 RID: 8656 RVA: 0x000896CB File Offset: 0x000878CB
		public MailboxAuditOperations AuditAdminOperations { get; set; }

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x000896D4 File Offset: 0x000878D4
		// (set) Token: 0x060021D2 RID: 8658 RVA: 0x000896DC File Offset: 0x000878DC
		public MailboxAuditOperations AuditDelegateOperations { get; set; }

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x000896E5 File Offset: 0x000878E5
		// (set) Token: 0x060021D4 RID: 8660 RVA: 0x000896ED File Offset: 0x000878ED
		public MailboxAuditOperations AuditDelegateAdminOperations { get; set; }

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x000896F6 File Offset: 0x000878F6
		// (set) Token: 0x060021D6 RID: 8662 RVA: 0x000896FE File Offset: 0x000878FE
		public MailboxAuditOperations AuditOwnerOperations { get; set; }

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x00089707 File Offset: 0x00087907
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x0008970F File Offset: 0x0008790F
		public DateTime? AuditLastAdminAccess { get; set; }

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x00089718 File Offset: 0x00087918
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x00089720 File Offset: 0x00087920
		public DateTime? AuditLastDelegateAccess { get; set; }

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x00089729 File Offset: 0x00087929
		// (set) Token: 0x060021DC RID: 8668 RVA: 0x00089731 File Offset: 0x00087931
		public DateTime? AuditLastExternalAccess { get; set; }

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x0008973A File Offset: 0x0008793A
		// (set) Token: 0x060021DE RID: 8670 RVA: 0x00089742 File Offset: 0x00087942
		public ADObjectId QueryBaseDN { get; set; }

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x0008974B File Offset: 0x0008794B
		// (set) Token: 0x060021E0 RID: 8672 RVA: 0x00089753 File Offset: 0x00087953
		internal SmtpAddress? FederatedSmtpAddress { get; set; }

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x0008975C File Offset: 0x0008795C
		// (set) Token: 0x060021E2 RID: 8674 RVA: 0x00089764 File Offset: 0x00087964
		internal FederatedIdentity FederatedIdentity { get; set; }

		// Token: 0x060021E3 RID: 8675 RVA: 0x0008977C File Offset: 0x0008797C
		public virtual SmtpAddress GetFederatedSmtpAddress()
		{
			if (this.FederatedSmtpAddress == null)
			{
				this.FederatedSmtpAddress = new SmtpAddress?(DirectoryExtensions.GetWithDirectoryExceptionTranslation<SmtpAddress>(() => this.GetFederatedSmtpAddress(this.PrimarySmtpAddress)));
			}
			return this.FederatedSmtpAddress.Value;
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x000897D4 File Offset: 0x000879D4
		public virtual FederatedIdentity GetFederatedIdentity()
		{
			if (this.FederatedIdentity == null)
			{
				this.FederatedIdentity = DirectoryExtensions.GetWithDirectoryExceptionTranslation<FederatedIdentity>(() => FederatedIdentityHelper.GetFederatedIdentity(this));
			}
			return this.FederatedIdentity;
		}
	}
}
