using System;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x02000057 RID: 87
	internal class RusDataProviderBase<T>
	{
		// Token: 0x0600023A RID: 570 RVA: 0x0000E500 File Offset: 0x0000C700
		protected RusDataProviderBase(IConfigurationSession session, IConfigurationSession rootOrgSession, ProvisioningCache provisioningCache)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (rootOrgSession == null)
			{
				throw new ArgumentNullException("rootOrgSession");
			}
			if (provisioningCache == null)
			{
				throw new ArgumentNullException("provisioningCache");
			}
			this.configurationSession = session;
			this.rootOrgConfigurationSession = rootOrgSession;
			this.provisioningCache = provisioningCache;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000E552 File Offset: 0x0000C752
		protected IConfigurationSession ConfigurationSession
		{
			get
			{
				return this.configurationSession;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000E55A File Offset: 0x0000C75A
		protected IConfigurationSession RootOrgConfigurationSession
		{
			get
			{
				return this.rootOrgConfigurationSession;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000E562 File Offset: 0x0000C762
		protected ProvisioningCache ProvisioningCache
		{
			get
			{
				return this.provisioningCache;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000E56C File Offset: 0x0000C76C
		public string DomainController
		{
			get
			{
				string text = this.ConfigurationSession.DomainController;
				if (string.IsNullOrEmpty(text))
				{
					string accountOrResourceForestFqdn = this.ConfigurationSession.SessionSettings.GetAccountOrResourceForestFqdn();
					text = this.ConfigurationSession.SessionSettings.ServerSettings.ConfigurationDomainController(accountOrResourceForestFqdn);
					if (string.IsNullOrEmpty(text))
					{
						text = ADSession.GetCurrentConfigDC(accountOrResourceForestFqdn);
					}
				}
				return text;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000E5CA File Offset: 0x0000C7CA
		public ADObjectId OrgContainerId
		{
			get
			{
				if (this.orgContainerId == null)
				{
					this.orgContainerId = this.rootOrgConfigurationSession.GetOrgContainerId();
				}
				return this.orgContainerId;
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000E5EC File Offset: 0x0000C7EC
		public ReadOnlyCollection<T> GetPolicies(OrganizationId organizationId, LogMessageDelegate logger)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			PolicyContainer<T> policyContainer = new PolicyContainer<T>();
			policyContainer.OrganizationId = organizationId;
			this.LoadPolicies(policyContainer, logger);
			return policyContainer.Policies.AsReadOnly();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000E62F File Offset: 0x0000C82F
		protected virtual void LoadPolicies(PolicyContainer<T> container, LogMessageDelegate logger)
		{
		}

		// Token: 0x04000127 RID: 295
		private IConfigurationSession configurationSession;

		// Token: 0x04000128 RID: 296
		private IConfigurationSession rootOrgConfigurationSession;

		// Token: 0x04000129 RID: 297
		private ADObjectId orgContainerId;

		// Token: 0x0400012A RID: 298
		private ProvisioningCache provisioningCache;
	}
}
