using System;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AddressBook.Service;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000034 RID: 52
	internal class NspiPrincipal
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000E344 File Offset: 0x0000C544
		private NspiPrincipal(MiniRecipient miniRecipient)
		{
			this.LegacyDistinguishedName = miniRecipient.LegacyExchangeDN;
			if (string.IsNullOrEmpty(this.LegacyDistinguishedName))
			{
				this.LegacyDistinguishedName = LegacyDN.FormatLegacyDnFromGuid(Guid.Empty, (Guid)miniRecipient[ADObjectSchema.Guid]);
			}
			this.AddressBookPolicy = miniRecipient.AddressBookPolicy;
			this.OrganizationId = miniRecipient.OrganizationId;
			this.DirectorySearchRoot = miniRecipient.QueryBaseDN;
			this.PrimarySmtpAddress = miniRecipient.PrimarySmtpAddress;
			this.ExchangeGuid = miniRecipient.ExchangeGuid;
			this.MAPIEnabled = miniRecipient.MAPIEnabled;
			this.Database = miniRecipient.Database;
			this.ExchangeVersion = miniRecipient.ExchangeVersion;
			if (miniRecipient.Languages != null && miniRecipient.Languages.Count > 0)
			{
				this.PreferredCulture = miniRecipient.Languages[0];
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000E418 File Offset: 0x0000C618
		private NspiPrincipal(ADUser adUser)
		{
			this.LegacyDistinguishedName = adUser.LegacyExchangeDN;
			if (string.IsNullOrEmpty(this.LegacyDistinguishedName))
			{
				this.LegacyDistinguishedName = LegacyDN.FormatLegacyDnFromGuid(Guid.Empty, adUser.Guid);
			}
			this.AddressBookPolicy = adUser.AddressBookPolicy;
			this.OrganizationId = adUser.OrganizationId;
			this.DirectorySearchRoot = adUser.QueryBaseDN;
			this.PrimarySmtpAddress = adUser.PrimarySmtpAddress;
			this.ExchangeGuid = adUser.ExchangeGuid;
			this.MAPIEnabled = adUser.MAPIEnabled;
			this.Database = adUser.Database;
			this.ExchangeVersion = adUser.ExchangeVersion;
			if (adUser.Languages != null && adUser.Languages.Count > 0)
			{
				this.PreferredCulture = adUser.Languages[0];
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000E4E2 File Offset: 0x0000C6E2
		private NspiPrincipal(SecurityIdentifier sid)
		{
			this.LegacyDistinguishedName = "/SID=" + sid.ToString();
			this.OrganizationId = OrganizationId.ForestWideOrgId;
			this.MAPIEnabled = true;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000E512 File Offset: 0x0000C712
		public ADObjectId GlobalAddressListFromAddressBookPolicy
		{
			get
			{
				if (this.AddressBookPolicy != null && this.globalAddressListFromAddressBookPolicy == null)
				{
					this.PopulateDataFromAddressBookPolicy();
				}
				return this.globalAddressListFromAddressBookPolicy;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000E530 File Offset: 0x0000C730
		public ADObjectId AllRoomsListFromAddressBookPolicy
		{
			get
			{
				if (this.AddressBookPolicy != null && this.allRoomsListFromAddressBookPolicy == null)
				{
					this.PopulateDataFromAddressBookPolicy();
				}
				return this.allRoomsListFromAddressBookPolicy;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000E54E File Offset: 0x0000C74E
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000E556 File Offset: 0x0000C756
		public ADObjectId AddressBookPolicy { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000E55F File Offset: 0x0000C75F
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000E567 File Offset: 0x0000C767
		public string LegacyDistinguishedName { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000E570 File Offset: 0x0000C770
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000E578 File Offset: 0x0000C778
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000E581 File Offset: 0x0000C781
		public ADObjectId ConfigurationUnit
		{
			get
			{
				if (!(this.OrganizationId != null))
				{
					return null;
				}
				return this.OrganizationId.ConfigurationUnit;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000E59E File Offset: 0x0000C79E
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000E5A6 File Offset: 0x0000C7A6
		public ADObjectId DirectorySearchRoot { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000E5AF File Offset: 0x0000C7AF
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000E5B7 File Offset: 0x0000C7B7
		public SmtpAddress PrimarySmtpAddress { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		public Guid ExchangeGuid { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000E5D1 File Offset: 0x0000C7D1
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000E5D9 File Offset: 0x0000C7D9
		public CultureInfo PreferredCulture { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000E5E2 File Offset: 0x0000C7E2
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000E5EA File Offset: 0x0000C7EA
		public bool MAPIEnabled { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000E5F3 File Offset: 0x0000C7F3
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000E5FB File Offset: 0x0000C7FB
		public ADObjectId Database { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000E604 File Offset: 0x0000C804
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000E60C File Offset: 0x0000C80C
		public ExchangeObjectVersion ExchangeVersion { get; private set; }

		// Token: 0x06000222 RID: 546 RVA: 0x0000E648 File Offset: 0x0000C848
		public static NspiPrincipal FromUserSid(SecurityIdentifier sid, string userDomain)
		{
			NspiPrincipal principal = null;
			if (!string.IsNullOrEmpty(userDomain))
			{
				MiniRecipient miniRecipient = NspiPrincipal.FindMiniRecipientBySid(ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(userDomain), sid);
				if (miniRecipient != null)
				{
					principal = new NspiPrincipal(miniRecipient);
				}
			}
			else if (Configuration.IsDatacenter)
			{
				ExTraceGlobals.NspiTracer.TraceWarning<SecurityIdentifier>(0L, "We have to do a fan out query for user {0} because of legacy client.", sid);
				DirectoryHelper.DoAdCallAndTranslateExceptions(delegate
				{
					MiniRecipient miniRecipientFromUserId = PartitionDataAggregator.GetMiniRecipientFromUserId(sid);
					if (miniRecipientFromUserId != null)
					{
						principal = new NspiPrincipal(miniRecipientFromUserId);
					}
				}, "ADAccountPartitionLocator::GetAllAccountPartitionIds");
			}
			else
			{
				principal = NspiPrincipal.FromUserSid(ADSessionSettings.FromRootOrgScopeSet(), sid);
			}
			return principal ?? new NspiPrincipal(sid);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		public static NspiPrincipal FromUserSid(ADSessionSettings sessionSettings, SecurityIdentifier sid)
		{
			MiniRecipient miniRecipient = NspiPrincipal.FindMiniRecipientBySid(sessionSettings, sid);
			if (miniRecipient != null)
			{
				return new NspiPrincipal(miniRecipient);
			}
			return new NspiPrincipal(sid);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000E721 File Offset: 0x0000C921
		public static NspiPrincipal FromADUser(ADUser adUser)
		{
			return new NspiPrincipal(adUser);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000E72C File Offset: 0x0000C92C
		private static MiniRecipient FindMiniRecipientBySid(ADSessionSettings sessionSettings, SecurityIdentifier sid)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 303, "FindMiniRecipientBySid", "f:\\15.00.1497\\sources\\dev\\DoMT\\src\\Service\\NspiPrincipal.cs");
			try
			{
				return tenantOrRootOrgRecipientSession.FindMiniRecipientBySid<MiniRecipient>(sid, null);
			}
			catch (NonUniqueRecipientException)
			{
			}
			catch (ObjectNotFoundException)
			{
			}
			return null;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000E788 File Offset: 0x0000C988
		private void PopulateDataFromAddressBookPolicy()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.OrganizationId), 329, "PopulateDataFromAddressBookPolicy", "f:\\15.00.1497\\sources\\dev\\DoMT\\src\\Service\\NspiPrincipal.cs");
			if (tenantOrTopologyConfigurationSession != null)
			{
				AddressBookMailboxPolicy addressBookMailboxPolicy = tenantOrTopologyConfigurationSession.Read<AddressBookMailboxPolicy>(this.AddressBookPolicy);
				if (addressBookMailboxPolicy != null)
				{
					this.globalAddressListFromAddressBookPolicy = addressBookMailboxPolicy.GlobalAddressList;
					this.allRoomsListFromAddressBookPolicy = addressBookMailboxPolicy.RoomList;
				}
			}
		}

		// Token: 0x04000133 RID: 307
		private ADObjectId globalAddressListFromAddressBookPolicy;

		// Token: 0x04000134 RID: 308
		private ADObjectId allRoomsListFromAddressBookPolicy;
	}
}
