using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000206 RID: 518
	internal sealed class AddressLists
	{
		// Token: 0x06001425 RID: 5157 RVA: 0x00048A20 File Offset: 0x00046C20
		public AddressLists(ClientSecurityContext clientSecurityContext, IExchangePrincipal exchangePrincipal, UserContext userContext)
		{
			if (clientSecurityContext == null)
			{
				throw new ArgumentNullException("clientSecurityContext");
			}
			if (exchangePrincipal == null)
			{
				throw new ArgumentNullException("exchangePrincipal");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.clientSecurityContext = clientSecurityContext;
			this.exchangePrincipal = exchangePrincipal;
			this.globalAddressListId = userContext.GlobalAddressListId;
			this.isModernGroupsFeatureEnabled = userContext.FeaturesManager.ClientServerSettings.ModernGroups.Enabled;
			this.configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.exchangePrincipal.MailboxInfo.OrganizationId), 90, ".ctor", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\people\\AddressLists.cs");
			this.configurationContext = new ConfigurationContext(userContext);
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00048AD4 File Offset: 0x00046CD4
		public AddressBookBase GlobalAddressList
		{
			get
			{
				if (!this.globalAddressListLoaded)
				{
					if (this.ShouldFeatureBeVisible(Feature.GlobalAddressList))
					{
						this.globalAddressList = AddressBookBase.GetGlobalAddressList(this.clientSecurityContext, this.configurationSession, this.GetRecipientSession(), this.globalAddressListId);
					}
					this.globalAddressListLoaded = true;
				}
				return this.globalAddressList;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x00048B24 File Offset: 0x00046D24
		public AddressBookBase AllRoomsAddressList
		{
			get
			{
				if (!this.allRoomsAddressListLoaded)
				{
					if (this.ShouldFeatureBeVisible(Feature.AddressLists))
					{
						this.allRoomsAddressList = AddressBookBase.GetAllRoomsAddressList(this.clientSecurityContext, this.configurationSession, this.exchangePrincipal.MailboxInfo.Configuration.AddressBookPolicy);
					}
					this.allRoomsAddressListLoaded = true;
				}
				return this.allRoomsAddressList;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00048B80 File Offset: 0x00046D80
		public IEnumerable<AddressBookBase> AllAddressLists
		{
			get
			{
				if (this.allAddressLists == null)
				{
					if (this.ShouldFeatureBeVisible(Feature.AddressLists))
					{
						this.allAddressLists = AddressBookBase.GetAllAddressLists(this.clientSecurityContext, this.configurationSession, this.exchangePrincipal.MailboxInfo.Configuration.AddressBookPolicy, this.isModernGroupsFeatureEnabled);
					}
					else
					{
						this.allAddressLists = Array<AddressBookBase>.Empty;
					}
				}
				return this.allAddressLists;
			}
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00048BE8 File Offset: 0x00046DE8
		private bool ShouldFeatureBeVisible(Feature feature)
		{
			return this.configurationContext.IsFeatureEnabled(feature);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00048BF8 File Offset: 0x00046DF8
		private IRecipientSession GetRecipientSession()
		{
			ADSessionSettings sessionSettings;
			if (this.exchangePrincipal.MailboxInfo.Configuration.AddressBookPolicy != null)
			{
				sessionSettings = ADSessionSettings.FromOrganizationIdWithAddressListScopeServiceOnly(this.exchangePrincipal.MailboxInfo.OrganizationId, this.globalAddressListId);
			}
			else
			{
				sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.exchangePrincipal.MailboxInfo.OrganizationId);
			}
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.IgnoreInvalid, null, sessionSettings, 210, "GetRecipientSession", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\people\\AddressLists.cs");
		}

		// Token: 0x04000AE4 RID: 2788
		private readonly ClientSecurityContext clientSecurityContext;

		// Token: 0x04000AE5 RID: 2789
		private readonly IExchangePrincipal exchangePrincipal;

		// Token: 0x04000AE6 RID: 2790
		private readonly IConfigurationSession configurationSession;

		// Token: 0x04000AE7 RID: 2791
		private readonly ConfigurationContext configurationContext;

		// Token: 0x04000AE8 RID: 2792
		private readonly bool isModernGroupsFeatureEnabled;

		// Token: 0x04000AE9 RID: 2793
		private AddressBookBase globalAddressList;

		// Token: 0x04000AEA RID: 2794
		private ADObjectId globalAddressListId;

		// Token: 0x04000AEB RID: 2795
		private bool globalAddressListLoaded;

		// Token: 0x04000AEC RID: 2796
		private AddressBookBase allRoomsAddressList;

		// Token: 0x04000AED RID: 2797
		private bool allRoomsAddressListLoaded;

		// Token: 0x04000AEE RID: 2798
		private IEnumerable<AddressBookBase> allAddressLists;
	}
}
