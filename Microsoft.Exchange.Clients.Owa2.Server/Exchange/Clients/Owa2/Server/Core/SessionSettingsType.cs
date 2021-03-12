using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Configuration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003FD RID: 1021
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SessionSettingsType
	{
		// Token: 0x06002119 RID: 8473 RVA: 0x0007950C File Offset: 0x0007770C
		internal SessionSettingsType()
		{
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0007953C File Offset: 0x0007773C
		internal SessionSettingsType(UserContext userContext, MailboxSession mailboxSession, UserAgent userAgent, CallContext callContext, UMSettingsData umSettings, OwaHelpUrlData helpUrlData)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (userContext.ExchangePrincipal == null)
			{
				throw new OwaInvalidRequestException("userContext.ExchangePrincipal is null");
			}
			StorePerformanceCountersCapture countersCapture = StorePerformanceCountersCapture.Start(mailboxSession);
			this.userDisplayName = userContext.ExchangePrincipal.MailboxInfo.DisplayName;
			this.userEmailAddress = userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			this.userLegacyExchangeDN = userContext.ExchangePrincipal.LegacyDn;
			this.hasArchive = this.UserHasArchive(userContext.ExchangePrincipal);
			this.archiveDisplayName = (this.hasArchive ? userContext.ExchangePrincipal.GetArchiveMailbox().ArchiveName : string.Empty);
			IEnumerable<string> source = from emailAddress in userContext.ExchangePrincipal.MailboxInfo.EmailAddresses
			select emailAddress.AddressString;
			if (source.Any<string>())
			{
				this.userProxyAddresses = source.ToArray<string>();
			}
			this.UpdateMailboxQuotaLimits(mailboxSession);
			this.isBposUser = userContext.IsBposUser;
			this.userSipUri = userContext.SipUri;
			this.userPrincipalName = userContext.UserPrincipalName;
			this.isGallatin = SessionSettingsType.GetIsGallatin();
			if (userContext.ExchangePrincipal.MailboxInfo.OrganizationId != null)
			{
				this.TenantGuid = userContext.ExchangePrincipal.MailboxInfo.OrganizationId.GetTenantGuid().ToString();
			}
			if (userContext.LogEventCommonData != null)
			{
				this.TenantDomain = userContext.LogEventCommonData.TenantDomain;
			}
			OwaUserConfigurationLogUtilities.LogAndResetPerfCapture(OwaUserConfigurationLogType.SessionSettingsMisc, countersCapture, true);
			int? maximumMessageSize = SessionSettingsType.GetMaximumMessageSize(mailboxSession);
			this.maxMessageSizeInKb = ((maximumMessageSize != null) ? maximumMessageSize.Value : 5120);
			OwaUserConfigurationLogUtilities.LogAndResetPerfCapture(OwaUserConfigurationLogType.SessionSettingsMessageSize, countersCapture, true);
			this.isPublicLogon = UserContextUtilities.IsPublicRequest(callContext.HttpContext.Request);
			OwaUserConfigurationLogUtilities.LogAndResetPerfCapture(OwaUserConfigurationLogType.SessionSettingsIsPublicLogon, countersCapture, true);
			ADUser aduser = null;
			if (userContext.IsExplicitLogon)
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, userContext.ExchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 303, ".ctor", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\types\\SessionSettingsType.cs");
				aduser = (DirectoryHelper.ReadADRecipient(userContext.ExchangePrincipal.MailboxInfo.MailboxGuid, userContext.ExchangePrincipal.MailboxInfo.IsArchive, tenantOrRootOrgRecipientSession) as ADUser);
				if (aduser != null && aduser.SharePointUrl != null)
				{
					this.sharePointUrl = aduser.SharePointUrl.ToString();
					this.sharePointTitle = aduser.DisplayName;
				}
			}
			OwaUserConfigurationLogUtilities.LogAndResetPerfCapture(OwaUserConfigurationLogType.TeamMailbox, countersCapture, true);
			if (userContext.LogonIdentity != null)
			{
				OWAMiniRecipient owaminiRecipient = userContext.LogonIdentity.GetOWAMiniRecipient();
				this.LogonEmailAddress = string.Empty;
				if (owaminiRecipient != null)
				{
					SmtpAddress primarySmtpAddress = owaminiRecipient.PrimarySmtpAddress;
					this.LogonEmailAddress = owaminiRecipient.PrimarySmtpAddress.ToString();
				}
				OwaUserConfigurationLogUtilities.LogAndResetPerfCapture(OwaUserConfigurationLogType.GetOWAMiniRecipient, countersCapture, false);
			}
			this.MailboxGuid = userContext.ExchangePrincipal.MailboxInfo.MailboxGuid.ToString();
			this.isExplicitLogon = userContext.IsExplicitLogon;
			this.isExplicitLogonOthersMailbox = false;
			this.canActAsOwner = true;
			countersCapture = StorePerformanceCountersCapture.Start(mailboxSession);
			this.SetDefaultFolderMapping(mailboxSession);
			OwaUserConfigurationLogUtilities.LogAndResetPerfCapture(OwaUserConfigurationLogType.SetDefaultFolderMapping, countersCapture, false);
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			this.userCulture = currentUICulture.Name;
			this.isUserCultureSpeechEnabled = Culture.IsCultureSpeechEnabled(currentUICulture);
			this.isUserCultureRightToLeft = currentUICulture.TextInfo.IsRightToLeft;
			countersCapture = StorePerformanceCountersCapture.Start(mailboxSession);
			this.playOnPhoneDialString = umSettings.PlayOnPhoneDialString;
			this.isRequireProtectedPlayOnPhone = umSettings.IsRequireProtectedPlayOnPhone;
			this.isUMEnabled = umSettings.IsUMEnabled;
			if (SyncUtilities.IsDatacenterMode())
			{
				SendAsSubscriptionsAndPeopleConnectResult allSendAsSubscriptionsAndPeopleConnect = SubscriptionManager.GetAllSendAsSubscriptionsAndPeopleConnect(mailboxSession);
				List<PimAggregationSubscription> pimSendAsAggregationSubscriptionList = allSendAsSubscriptionsAndPeopleConnect.PimSendAsAggregationSubscriptionList;
				this.PeopleConnectionsExist = allSendAsSubscriptionsAndPeopleConnect.PeopleConnectionsExist;
				List<AggregatedAccountInfo> list = null;
				if (aduser == null && userContext.ExchangePrincipal != null)
				{
					IRecipientSession tenantOrRootOrgRecipientSession2 = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, userContext.ExchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 375, ".ctor", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\types\\SessionSettingsType.cs");
					aduser = (DirectoryHelper.ReadADRecipient(userContext.ExchangePrincipal.MailboxInfo.MailboxGuid, userContext.ExchangePrincipal.MailboxInfo.IsArchive, tenantOrRootOrgRecipientSession2) as ADUser);
				}
				if (aduser != null)
				{
					AggregatedAccountHelper aggregatedAccountHelper = new AggregatedAccountHelper(mailboxSession, aduser);
					list = aggregatedAccountHelper.GetListOfAccounts();
				}
				int capacity = pimSendAsAggregationSubscriptionList.Count + ((list != null) ? list.Count : 0);
				List<ConnectedAccountInfo> list2 = new List<ConnectedAccountInfo>(capacity);
				foreach (PimAggregationSubscription pimAggregationSubscription in pimSendAsAggregationSubscriptionList)
				{
					list2.Add(new ConnectedAccountInfo
					{
						SubscriptionGuid = pimAggregationSubscription.SubscriptionGuid,
						EmailAddress = SessionSettingsType.DecodeIdnDomain(pimAggregationSubscription.UserEmailAddress),
						DisplayName = pimAggregationSubscription.UserDisplayName
					});
				}
				if (list != null)
				{
					foreach (AggregatedAccountInfo aggregatedAccountInfo in list)
					{
						bool flag = false;
						string aggregatedAccountEmail = SessionSettingsType.DecodeIdnDomain(aggregatedAccountInfo.SmtpAddress);
						if (!string.IsNullOrWhiteSpace(aggregatedAccountEmail))
						{
							if (list2.Find((ConnectedAccountInfo account) => StringComparer.InvariantCultureIgnoreCase.Equals(account.EmailAddress, aggregatedAccountEmail)) != null)
							{
								break;
							}
							if (!flag)
							{
								list2.Add(new ConnectedAccountInfo
								{
									SubscriptionGuid = aggregatedAccountInfo.RequestGuid,
									EmailAddress = aggregatedAccountEmail,
									DisplayName = aggregatedAccountEmail
								});
							}
						}
					}
				}
				this.connectedAccountInfos = list2.ToArray();
			}
			OwaUserConfigurationLogUtilities.LogAndResetPerfCapture(OwaUserConfigurationLogType.IsDatacenterMode, countersCapture, true);
			this.helpUrl = helpUrlData.HelpUrl;
			this.isPublicComputerSession = UserContextUtilities.IsPublicComputerSession(callContext.HttpContext);
			string errorString = string.Empty;
			try
			{
				IMailboxInfo mailboxInfo = userContext.ExchangePrincipal.MailboxInfo;
				TenantPublicFolderConfiguration tenantPublicFolderConfiguration = null;
				if (TenantPublicFolderConfigurationCache.Instance.TryGetValue(mailboxInfo.OrganizationId, out tenantPublicFolderConfiguration))
				{
					ADObjectId defaultPublicFolderMailbox = userContext.ExchangePrincipal.DefaultPublicFolderMailbox;
					PublicFolderRecipient publicFolderRecipient = tenantPublicFolderConfiguration.GetPublicFolderRecipient(mailboxInfo.MailboxGuid, defaultPublicFolderMailbox);
					if (publicFolderRecipient != null)
					{
						if (publicFolderRecipient.IsLocal)
						{
							this.DefaultPublicFolderMailbox = publicFolderRecipient.PrimarySmtpAddress.ToString();
						}
						else if (publicFolderRecipient.ObjectId == null)
						{
							errorString = "publicFolderRecipient not local and ObjectId null";
						}
						else
						{
							errorString = "publicFolderRecipient not local and ObjectId " + publicFolderRecipient.ObjectId.ObjectGuid;
						}
					}
					else
					{
						errorString = "publicFolderRecipient null";
					}
				}
			}
			catch (LocalizedException ex)
			{
				errorString = ex.ToString();
			}
			finally
			{
				OwaUserConfigurationLogUtilities.LogAndResetPerfCapture(OwaUserConfigurationLogType.DefaultPublicFolderMailbox, countersCapture, true, errorString);
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x00079C04 File Offset: 0x00077E04
		// (set) Token: 0x0600211C RID: 8476 RVA: 0x00079C0C File Offset: 0x00077E0C
		[DataMember]
		public bool IsExplicitLogon
		{
			get
			{
				return this.isExplicitLogon;
			}
			set
			{
				this.isExplicitLogon = value;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x00079C15 File Offset: 0x00077E15
		// (set) Token: 0x0600211E RID: 8478 RVA: 0x00079C1D File Offset: 0x00077E1D
		[DataMember]
		public bool IsExplicitLogonOthersMailbox
		{
			get
			{
				return this.isExplicitLogonOthersMailbox;
			}
			set
			{
				this.isExplicitLogonOthersMailbox = value;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x00079C26 File Offset: 0x00077E26
		// (set) Token: 0x06002120 RID: 8480 RVA: 0x00079C2E File Offset: 0x00077E2E
		[DataMember]
		public bool IsPublicLogon
		{
			get
			{
				return this.isPublicLogon;
			}
			set
			{
				this.isPublicLogon = value;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x00079C37 File Offset: 0x00077E37
		// (set) Token: 0x06002122 RID: 8482 RVA: 0x00079C3F File Offset: 0x00077E3F
		[DataMember]
		public bool IsPublicComputerSession
		{
			get
			{
				return this.isPublicComputerSession;
			}
			set
			{
				this.isPublicComputerSession = value;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x00079C48 File Offset: 0x00077E48
		// (set) Token: 0x06002124 RID: 8484 RVA: 0x00079C50 File Offset: 0x00077E50
		[DataMember]
		public bool CanActAsOwner
		{
			get
			{
				return this.canActAsOwner;
			}
			set
			{
				this.canActAsOwner = value;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x00079C59 File Offset: 0x00077E59
		// (set) Token: 0x06002126 RID: 8486 RVA: 0x00079C61 File Offset: 0x00077E61
		[DataMember]
		public bool IsBposUser
		{
			get
			{
				return this.isBposUser;
			}
			set
			{
				this.isBposUser = value;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x00079C6A File Offset: 0x00077E6A
		// (set) Token: 0x06002128 RID: 8488 RVA: 0x00079C72 File Offset: 0x00077E72
		[DataMember]
		public bool IsGallatin
		{
			get
			{
				return this.isGallatin;
			}
			set
			{
				this.isGallatin = value;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x00079C7B File Offset: 0x00077E7B
		// (set) Token: 0x0600212A RID: 8490 RVA: 0x00079C83 File Offset: 0x00077E83
		[DataMember]
		public string UserDisplayName
		{
			get
			{
				return this.userDisplayName;
			}
			set
			{
				this.userDisplayName = value;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x00079C8C File Offset: 0x00077E8C
		// (set) Token: 0x0600212C RID: 8492 RVA: 0x00079C94 File Offset: 0x00077E94
		[DataMember]
		public string UserPrincipalName
		{
			get
			{
				return this.userPrincipalName;
			}
			set
			{
				this.userPrincipalName = value;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x00079C9D File Offset: 0x00077E9D
		// (set) Token: 0x0600212E RID: 8494 RVA: 0x00079CA5 File Offset: 0x00077EA5
		[DataMember]
		public string UserEmailAddress
		{
			get
			{
				return this.userEmailAddress;
			}
			set
			{
				this.userEmailAddress = value;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x00079CAE File Offset: 0x00077EAE
		// (set) Token: 0x06002130 RID: 8496 RVA: 0x00079CB6 File Offset: 0x00077EB6
		[DataMember]
		public string UserLegacyExchangeDN
		{
			get
			{
				return this.userLegacyExchangeDN;
			}
			set
			{
				this.userLegacyExchangeDN = value;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x00079CBF File Offset: 0x00077EBF
		// (set) Token: 0x06002132 RID: 8498 RVA: 0x00079CC7 File Offset: 0x00077EC7
		[DataMember]
		public string[] UserProxyAddresses
		{
			get
			{
				return this.userProxyAddresses;
			}
			set
			{
				this.userProxyAddresses = value;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x00079CD0 File Offset: 0x00077ED0
		// (set) Token: 0x06002134 RID: 8500 RVA: 0x00079CD8 File Offset: 0x00077ED8
		[DataMember]
		public string LogonEmailAddress { get; set; }

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x00079CE1 File Offset: 0x00077EE1
		// (set) Token: 0x06002136 RID: 8502 RVA: 0x00079CE9 File Offset: 0x00077EE9
		[DataMember]
		public string MailboxGuid { get; set; }

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x00079CF2 File Offset: 0x00077EF2
		// (set) Token: 0x06002138 RID: 8504 RVA: 0x00079CFA File Offset: 0x00077EFA
		[DataMember]
		public string PlayOnPhoneDialString
		{
			get
			{
				return this.playOnPhoneDialString;
			}
			set
			{
				this.playOnPhoneDialString = value;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002139 RID: 8505 RVA: 0x00079D03 File Offset: 0x00077F03
		// (set) Token: 0x0600213A RID: 8506 RVA: 0x00079D0B File Offset: 0x00077F0B
		[DataMember]
		public bool IsRequireProtectedPlayOnPhone
		{
			get
			{
				return this.isRequireProtectedPlayOnPhone;
			}
			set
			{
				this.isRequireProtectedPlayOnPhone = value;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x00079D14 File Offset: 0x00077F14
		// (set) Token: 0x0600213C RID: 8508 RVA: 0x00079D1C File Offset: 0x00077F1C
		[DataMember]
		public bool IsUMEnabled
		{
			get
			{
				return this.isUMEnabled;
			}
			set
			{
				this.isUMEnabled = value;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x00079D25 File Offset: 0x00077F25
		// (set) Token: 0x0600213E RID: 8510 RVA: 0x00079D2D File Offset: 0x00077F2D
		[DataMember]
		public bool HasArchive
		{
			get
			{
				return this.hasArchive;
			}
			set
			{
				this.hasArchive = value;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x00079D36 File Offset: 0x00077F36
		// (set) Token: 0x06002140 RID: 8512 RVA: 0x00079D3E File Offset: 0x00077F3E
		[DataMember]
		public string ArchiveDisplayName
		{
			get
			{
				return this.archiveDisplayName;
			}
			set
			{
				this.archiveDisplayName = value;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x00079D47 File Offset: 0x00077F47
		// (set) Token: 0x06002142 RID: 8514 RVA: 0x00079D4F File Offset: 0x00077F4F
		[DataMember]
		public int MaxMessageSizeInKb
		{
			get
			{
				return this.maxMessageSizeInKb;
			}
			set
			{
				this.maxMessageSizeInKb = value;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x00079D58 File Offset: 0x00077F58
		// (set) Token: 0x06002144 RID: 8516 RVA: 0x00079D60 File Offset: 0x00077F60
		[DataMember]
		public FolderId[] DefaultFolderIds
		{
			get
			{
				return this.defaultFolderIds;
			}
			set
			{
				this.defaultFolderIds = value;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x00079D69 File Offset: 0x00077F69
		// (set) Token: 0x06002146 RID: 8518 RVA: 0x00079D71 File Offset: 0x00077F71
		[DataMember]
		public string[] DefaultFolderNames
		{
			get
			{
				return this.defaultFolderNames;
			}
			set
			{
				this.defaultFolderNames = value;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x00079D7A File Offset: 0x00077F7A
		// (set) Token: 0x06002148 RID: 8520 RVA: 0x00079D82 File Offset: 0x00077F82
		[DataMember]
		public string DefaultPublicFolderMailbox { get; set; }

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x00079D8B File Offset: 0x00077F8B
		// (set) Token: 0x0600214A RID: 8522 RVA: 0x00079D93 File Offset: 0x00077F93
		[DataMember]
		public string UserCulture
		{
			get
			{
				return this.userCulture;
			}
			set
			{
				this.userCulture = value;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x00079D9C File Offset: 0x00077F9C
		// (set) Token: 0x0600214C RID: 8524 RVA: 0x00079DA4 File Offset: 0x00077FA4
		[DataMember]
		public string TenantDomain { get; set; }

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x00079DAD File Offset: 0x00077FAD
		// (set) Token: 0x0600214E RID: 8526 RVA: 0x00079DB5 File Offset: 0x00077FB5
		[DataMember]
		public string TenantGuid { get; set; }

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x00079DBE File Offset: 0x00077FBE
		// (set) Token: 0x06002150 RID: 8528 RVA: 0x00079DC6 File Offset: 0x00077FC6
		[DataMember]
		public string SharePointUrl
		{
			get
			{
				return this.sharePointUrl;
			}
			set
			{
				this.sharePointUrl = value;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x00079DCF File Offset: 0x00077FCF
		// (set) Token: 0x06002152 RID: 8530 RVA: 0x00079DD7 File Offset: 0x00077FD7
		[DataMember]
		public string SharePointTitle
		{
			get
			{
				return this.sharePointTitle;
			}
			set
			{
				this.sharePointTitle = value;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x00079DE0 File Offset: 0x00077FE0
		// (set) Token: 0x06002154 RID: 8532 RVA: 0x00079DE8 File Offset: 0x00077FE8
		[DataMember]
		public bool IsUserCultureSpeechEnabled
		{
			get
			{
				return this.isUserCultureSpeechEnabled;
			}
			set
			{
				this.isUserCultureSpeechEnabled = value;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x00079DF1 File Offset: 0x00077FF1
		// (set) Token: 0x06002156 RID: 8534 RVA: 0x00079DF9 File Offset: 0x00077FF9
		[DataMember]
		public bool IsUserCultureRightToLeft
		{
			get
			{
				return this.isUserCultureRightToLeft;
			}
			set
			{
				this.isUserCultureRightToLeft = value;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x00079E02 File Offset: 0x00078002
		// (set) Token: 0x06002158 RID: 8536 RVA: 0x00079E0A File Offset: 0x0007800A
		[DataMember]
		public long QuotaSend
		{
			get
			{
				return this.quotaSend;
			}
			set
			{
				this.quotaSend = value;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x00079E13 File Offset: 0x00078013
		// (set) Token: 0x0600215A RID: 8538 RVA: 0x00079E1B File Offset: 0x0007801B
		[DataMember]
		public long QuotaWarning
		{
			get
			{
				return this.quotaWarning;
			}
			set
			{
				this.quotaWarning = value;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x00079E24 File Offset: 0x00078024
		// (set) Token: 0x0600215C RID: 8540 RVA: 0x00079E2C File Offset: 0x0007802C
		[DataMember]
		public long QuotaUsed
		{
			get
			{
				return this.quotaUsed;
			}
			set
			{
				this.quotaUsed = value;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x00079E35 File Offset: 0x00078035
		// (set) Token: 0x0600215E RID: 8542 RVA: 0x00079E3D File Offset: 0x0007803D
		[DataMember(EmitDefaultValue = false)]
		public ConnectedAccountInfo[] ConnectedAccountInfos
		{
			get
			{
				return this.connectedAccountInfos;
			}
			set
			{
				this.connectedAccountInfos = value;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x0600215F RID: 8543 RVA: 0x00079E46 File Offset: 0x00078046
		// (set) Token: 0x06002160 RID: 8544 RVA: 0x00079E4E File Offset: 0x0007804E
		[DataMember]
		public bool PeopleConnectionsExist
		{
			get
			{
				return this.peopleConnectionsExist;
			}
			set
			{
				this.peopleConnectionsExist = value;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x00079E57 File Offset: 0x00078057
		// (set) Token: 0x06002162 RID: 8546 RVA: 0x00079E5F File Offset: 0x0007805F
		[DataMember]
		public string UserSipUri
		{
			get
			{
				return this.userSipUri;
			}
			set
			{
				this.userSipUri = value;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x00079E68 File Offset: 0x00078068
		// (set) Token: 0x06002164 RID: 8548 RVA: 0x00079E70 File Offset: 0x00078070
		[DataMember]
		public string HelpUrl
		{
			get
			{
				return this.helpUrl;
			}
			set
			{
				this.helpUrl = value;
			}
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00079E7C File Offset: 0x0007807C
		private static string DecodeIdnDomain(SmtpAddress smtpAddress)
		{
			string domain = smtpAddress.Domain;
			if (!string.IsNullOrEmpty(domain))
			{
				IdnMapping idnMapping = new IdnMapping();
				string unicode = idnMapping.GetUnicode(domain);
				return smtpAddress.Local + "@" + unicode;
			}
			return smtpAddress.ToString();
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00079EC8 File Offset: 0x000780C8
		private static int? GetMaximumMessageSize(MailboxSession mailboxSession)
		{
			object obj = mailboxSession.Mailbox.TryGetProperty(MailboxSchema.MaxUserMessageSize);
			if (!(obj is PropertyError))
			{
				return new int?((int)obj);
			}
			return null;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x00079F04 File Offset: 0x00078104
		private bool UserHasArchive(ExchangePrincipal principal)
		{
			IMailboxInfo archiveMailbox = principal.GetArchiveMailbox();
			return archiveMailbox != null && (archiveMailbox.ArchiveState == ArchiveState.Local || archiveMailbox.ArchiveState == ArchiveState.HostedProvisioned);
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x00079F34 File Offset: 0x00078134
		private void SetDefaultFolderMapping(MailboxSession session)
		{
			DefaultFolderType[] array = (DefaultFolderType[])Enum.GetValues(typeof(DefaultFolderType));
			this.defaultFolderIds = new FolderId[array.Length];
			this.defaultFolderNames = new string[array.Length];
			int num = 0;
			Dictionary<DefaultFolderType, string> defaultFolderTypeToFolderNameMapForMailbox = IdConverter.GetDefaultFolderTypeToFolderNameMapForMailbox();
			DefaultFolderType[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				DefaultFolderType defaultFolderType = array2[i];
				if (defaultFolderType == DefaultFolderType.None)
				{
					this.defaultFolderNames[num] = Enum.GetName(typeof(DefaultFolderType), defaultFolderType);
					goto IL_7E;
				}
				if (defaultFolderTypeToFolderNameMapForMailbox.TryGetValue(defaultFolderType, out this.defaultFolderNames[num]))
				{
					goto Block_2;
				}
				IL_B8:
				i++;
				continue;
				Block_2:
				try
				{
					IL_7E:
					StoreObjectId defaultFolderId = session.GetDefaultFolderId(defaultFolderType);
					if (defaultFolderId == null)
					{
						this.defaultFolderIds[num] = null;
					}
					else
					{
						this.defaultFolderIds[num] = IdConverter.ConvertStoreFolderIdToFolderId(defaultFolderId, session);
					}
				}
				catch (InvalidOperationException)
				{
					this.defaultFolderIds[num] = null;
				}
				num++;
				goto IL_B8;
			}
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0007A01C File Offset: 0x0007821C
		private void UpdateMailboxQuotaLimits(MailboxSession mailboxSession)
		{
			mailboxSession.Mailbox.ForceReload(new PropertyDefinition[]
			{
				MailboxSchema.QuotaUsedExtended,
				MailboxSchema.QuotaProhibitSend,
				MailboxSchema.StorageQuotaLimit
			});
			this.quotaUsed = 0L;
			object obj = mailboxSession.Mailbox.TryGetProperty(MailboxSchema.QuotaUsedExtended);
			if (!(obj is PropertyError))
			{
				this.quotaUsed = (long)obj;
			}
			this.quotaSend = 0L;
			obj = mailboxSession.Mailbox.TryGetProperty(MailboxSchema.QuotaProhibitSend);
			if (!(obj is PropertyError))
			{
				this.quotaSend = (long)((int)obj) * 1024L;
			}
			this.quotaWarning = 0L;
			obj = mailboxSession.Mailbox.TryGetProperty(MailboxSchema.StorageQuotaLimit);
			if (!(obj is PropertyError))
			{
				this.quotaWarning = (long)((int)obj) * 1024L;
			}
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0007A0EC File Offset: 0x000782EC
		private static bool GetIsGallatin()
		{
			bool flag = false;
			return bool.TryParse(ConfigurationManager.AppSettings["IsGallatin"], out flag) && flag;
		}

		// Token: 0x0400128E RID: 4750
		private const int DefaultMaxMessageSizeInKb = 5120;

		// Token: 0x0400128F RID: 4751
		private const string IsGallatinConfigKey = "IsGallatin";

		// Token: 0x04001290 RID: 4752
		private bool isExplicitLogon;

		// Token: 0x04001291 RID: 4753
		private bool isExplicitLogonOthersMailbox;

		// Token: 0x04001292 RID: 4754
		private bool isPublicLogon;

		// Token: 0x04001293 RID: 4755
		private bool isPublicComputerSession;

		// Token: 0x04001294 RID: 4756
		private bool canActAsOwner;

		// Token: 0x04001295 RID: 4757
		private bool isBposUser;

		// Token: 0x04001296 RID: 4758
		private bool isGallatin;

		// Token: 0x04001297 RID: 4759
		private string userDisplayName;

		// Token: 0x04001298 RID: 4760
		private string userEmailAddress;

		// Token: 0x04001299 RID: 4761
		private string userLegacyExchangeDN;

		// Token: 0x0400129A RID: 4762
		private string[] userProxyAddresses;

		// Token: 0x0400129B RID: 4763
		private string userSipUri;

		// Token: 0x0400129C RID: 4764
		private bool hasArchive;

		// Token: 0x0400129D RID: 4765
		private string archiveDisplayName;

		// Token: 0x0400129E RID: 4766
		private int maxMessageSizeInKb;

		// Token: 0x0400129F RID: 4767
		private FolderId[] defaultFolderIds;

		// Token: 0x040012A0 RID: 4768
		private string[] defaultFolderNames;

		// Token: 0x040012A1 RID: 4769
		private string userCulture;

		// Token: 0x040012A2 RID: 4770
		private string sharePointUrl;

		// Token: 0x040012A3 RID: 4771
		private string sharePointTitle;

		// Token: 0x040012A4 RID: 4772
		private string playOnPhoneDialString;

		// Token: 0x040012A5 RID: 4773
		private bool isRequireProtectedPlayOnPhone;

		// Token: 0x040012A6 RID: 4774
		private bool isUMEnabled;

		// Token: 0x040012A7 RID: 4775
		private bool isUserCultureSpeechEnabled;

		// Token: 0x040012A8 RID: 4776
		private bool isUserCultureRightToLeft;

		// Token: 0x040012A9 RID: 4777
		private long quotaSend;

		// Token: 0x040012AA RID: 4778
		private long quotaWarning;

		// Token: 0x040012AB RID: 4779
		private long quotaUsed;

		// Token: 0x040012AC RID: 4780
		private ConnectedAccountInfo[] connectedAccountInfos;

		// Token: 0x040012AD RID: 4781
		private bool peopleConnectionsExist;

		// Token: 0x040012AE RID: 4782
		private string helpUrl;

		// Token: 0x040012AF RID: 4783
		private string userPrincipalName;
	}
}
