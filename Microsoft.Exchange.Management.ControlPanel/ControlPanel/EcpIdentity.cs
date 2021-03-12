using System;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003A6 RID: 934
	internal class EcpIdentity
	{
		// Token: 0x0600314B RID: 12619 RVA: 0x00097986 File Offset: 0x00095B86
		public EcpIdentity(IIdentity logonUserIdentity, string cacheKeySuffix) : this(logonUserIdentity, cacheKeySuffix, false)
		{
			this.accessedUserIdentity = this.LogonUserIdentity;
			this.accessedUserSid = this.logonUserSid;
			this.UserName = logonUserIdentity.GetSafeName(true);
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000979B8 File Offset: 0x00095BB8
		public EcpIdentity(EcpLogonInformation identity, string cacheKeySuffix) : this(identity.LogonUser, cacheKeySuffix, identity.Impersonated)
		{
			this.logonUserSid = identity.LogonMailboxSid;
			if (identity.Impersonated)
			{
				this.accessedUserIdentity = identity.ImpersonatedUser;
				this.accessedUserSid = identity.ImpersonatedUser.GetSecurityIdentifier();
			}
			else
			{
				this.accessedUserIdentity = this.LogonUserIdentity;
				this.accessedUserSid = this.logonUserSid;
			}
			this.UserName = identity.Name;
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x00097A30 File Offset: 0x00095C30
		public EcpIdentity(IPrincipal logonUserPrincipal, string explicitUserSmtpAddress, string cacheKeySuffix) : this(logonUserPrincipal.Identity, cacheKeySuffix, true)
		{
			if (string.IsNullOrEmpty(explicitUserSmtpAddress))
			{
				throw new ArgumentException("ExplicitUserSmtpAddress cannot be null or empty", explicitUserSmtpAddress);
			}
			this.logonUserPrincipal = logonUserPrincipal;
			this.accessedUserSmtpAddress = explicitUserSmtpAddress;
			this.UserName = Strings.OnbehalfOf(this.LogonUserIdentity.GetSafeName(true), explicitUserSmtpAddress);
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x00097A8C File Offset: 0x00095C8C
		private EcpIdentity(IIdentity logonUserIdentity, string cacheKeySuffix, bool isExplicitSignon)
		{
			this.IsExplicitSignon = isExplicitSignon;
			this.identityResolved = !isExplicitSignon;
			this.cacheKeySuffix = cacheKeySuffix;
			this.LogonUserIdentity = logonUserIdentity;
			if (logonUserIdentity.AuthenticationType != DelegatedPrincipal.DelegatedAuthenticationType && !DatacenterRegistry.IsForefrontForOffice())
			{
				this.logonUserSid = logonUserIdentity.GetSecurityIdentifier();
			}
		}

		// Token: 0x17001F63 RID: 8035
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x00097AE3 File Offset: 0x00095CE3
		public IIdentity AccessedUserIdentity
		{
			get
			{
				if (!this.identityResolved)
				{
					this.ResolveAccessedUser();
				}
				return this.accessedUserIdentity;
			}
		}

		// Token: 0x17001F64 RID: 8036
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x00097AF9 File Offset: 0x00095CF9
		public SecurityIdentifier AccessedUserSid
		{
			get
			{
				if (!this.identityResolved)
				{
					this.ResolveAccessedUser();
				}
				return this.accessedUserSid;
			}
		}

		// Token: 0x17001F65 RID: 8037
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x00097B0F File Offset: 0x00095D0F
		public bool HasFullAccess
		{
			get
			{
				if (!this.identityResolved)
				{
					this.ResolveAccessedUser();
				}
				return this.hasFullAccess;
			}
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x00097B28 File Offset: 0x00095D28
		public string GetCacheKey()
		{
			if (this.cacheKey == null)
			{
				if (this.logonUserSid == null)
				{
					this.cacheKey = this.LogonUserIdentity.Name + this.cacheKeySuffix;
				}
				else
				{
					this.cacheKey = this.logonUserSid.Value + this.cacheKeySuffix;
				}
				if (this.IsExplicitSignon)
				{
					if (string.IsNullOrEmpty(this.accessedUserSmtpAddress))
					{
						this.cacheKey += this.accessedUserSid.Value;
					}
					else
					{
						this.cacheKey += this.accessedUserSmtpAddress;
					}
				}
			}
			return this.cacheKey;
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x00097BD8 File Offset: 0x00095DD8
		public ExchangePrincipal GetLogonUserExchangePrincipal()
		{
			if (!this.logonUserResolved)
			{
				try
				{
					if (this.logonUserSid != null)
					{
						this.logonUserExchangePrincipal = ExchangePrincipal.FromUserSid(this.GetOrganizationIdFromIdentity(this.LogonUserIdentity).ToADSessionSettings(), this.logonUserSid);
					}
				}
				catch (ObjectNotFoundException)
				{
				}
				finally
				{
					this.logonUserResolved = true;
				}
			}
			return this.logonUserExchangePrincipal;
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x00097C50 File Offset: 0x00095E50
		public ExchangePrincipal GetAccessedUserExchangePrincipal()
		{
			if (this.IsExplicitSignon)
			{
				if (!this.identityResolved)
				{
					this.ResolveAccessedUser();
				}
				return this.accessedUserExchangePrincipal;
			}
			return this.GetLogonUserExchangePrincipal();
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x00097C78 File Offset: 0x00095E78
		private void ResolveAccessedUser()
		{
			if (string.IsNullOrEmpty(this.accessedUserSmtpAddress))
			{
				this.accessedUserExchangePrincipal = ExchangePrincipal.FromUserSid(this.GetOrganizationIdFromIdentity(this.accessedUserIdentity).ToADSessionSettings(), this.accessedUserSid);
				this.logonUserPrincipal = new GenericPrincipal(this.LogonUserIdentity, null);
			}
			else
			{
				OrganizationId organizationId = OrganizationId.ForestWideOrgId;
				SidBasedIdentity sidBasedIdentity = this.LogonUserIdentity as SidBasedIdentity;
				if (sidBasedIdentity != null)
				{
					organizationId = sidBasedIdentity.UserOrganizationId;
				}
				else
				{
					DelegatedPrincipal delegatedPrincipal = this.logonUserPrincipal as DelegatedPrincipal;
					if (delegatedPrincipal != null)
					{
						SmtpDomain domain;
						if (SmtpDomain.TryParse(delegatedPrincipal.DelegatedOrganization, out domain))
						{
							organizationId = DomainCache.Singleton.Get(new SmtpDomainWithSubdomains(domain, false)).OrganizationId;
						}
					}
					else
					{
						ExchangePrincipal exchangePrincipal = this.GetLogonUserExchangePrincipal();
						if (exchangePrincipal != null)
						{
							organizationId = exchangePrincipal.MailboxInfo.OrganizationId;
						}
					}
				}
				ADSessionSettings adSettings = organizationId.ToADSessionSettings();
				string partitionId = null;
				if (organizationId != null && organizationId != OrganizationId.ForestWideOrgId && organizationId.PartitionId != null)
				{
					partitionId = organizationId.PartitionId.ToString();
				}
				this.accessedUserExchangePrincipal = ExchangePrincipal.FromProxyAddress(adSettings, this.accessedUserSmtpAddress, RemotingOptions.AllowCrossSite);
				this.accessedUserIdentity = new GenericSidIdentity(this.accessedUserExchangePrincipal.Sid.Value, this.LogonUserIdentity.AuthenticationType + "-ExplicitSignOn", this.accessedUserExchangePrincipal.Sid, partitionId);
				this.accessedUserSid = this.accessedUserIdentity.GetSecurityIdentifier();
			}
			this.hasFullAccess = this.CanOpenAccessedUserMailbox();
			this.identityResolved = true;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x00097DEC File Offset: 0x00095FEC
		private static ClientSecurityContext TryMungeTokenFromSlaveAccount(ClientSecurityContext account)
		{
			ClientSecurityContext result;
			try
			{
				result = new SlaveAccountTokenMunger().MungeToken(account, OrganizationId.ForestWideOrgId);
			}
			catch (TokenMungingException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x00097E24 File Offset: 0x00096024
		private bool CanOpenAccessedUserMailbox()
		{
			bool result = false;
			if (this.LogonUserIdentity.AuthenticationType != DelegatedPrincipal.DelegatedAuthenticationType)
			{
				if (this.logonUserSid.Value == this.accessedUserSid.Value)
				{
					result = true;
					this.logonUserEsoSelf = true;
				}
				else
				{
					try
					{
						using (ClientSecurityContext clientSecurityContext = this.logonUserPrincipal.Identity.CreateClientSecurityContext(true))
						{
							using (ClientSecurityContext clientSecurityContext2 = Util.IsDataCenter ? null : EcpIdentity.TryMungeTokenFromSlaveAccount(clientSecurityContext))
							{
								using (MailboxSession.Open(this.accessedUserExchangePrincipal, clientSecurityContext2 ?? clientSecurityContext, CultureInfo.CurrentCulture, "Client=Management;Action=ECP"))
								{
								}
							}
						}
						result = true;
					}
					catch (ConnectionFailedTransientException ex)
					{
						if (!(ex.InnerException is MapiExceptionLogonFailed))
						{
							throw;
						}
					}
					catch (StoragePermanentException)
					{
					}
				}
			}
			return result;
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x00097F34 File Offset: 0x00096134
		private OrganizationId GetOrganizationIdFromIdentity(IIdentity identity)
		{
			OrganizationId result = OrganizationId.ForestWideOrgId;
			SidBasedIdentity sidBasedIdentity = identity as SidBasedIdentity;
			if (sidBasedIdentity != null && sidBasedIdentity.UserOrganizationId != null)
			{
				result = sidBasedIdentity.UserOrganizationId;
			}
			return result;
		}

		// Token: 0x17001F66 RID: 8038
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x00097F67 File Offset: 0x00096167
		public bool LogonUserEsoSelf
		{
			get
			{
				if (!this.identityResolved)
				{
					this.ResolveAccessedUser();
				}
				return this.logonUserEsoSelf;
			}
		}

		// Token: 0x040023E0 RID: 9184
		public readonly bool IsExplicitSignon;

		// Token: 0x040023E1 RID: 9185
		public readonly string UserName;

		// Token: 0x040023E2 RID: 9186
		public readonly IIdentity LogonUserIdentity;

		// Token: 0x040023E3 RID: 9187
		private readonly string cacheKeySuffix;

		// Token: 0x040023E4 RID: 9188
		private readonly SecurityIdentifier logonUserSid;

		// Token: 0x040023E5 RID: 9189
		private readonly string accessedUserSmtpAddress;

		// Token: 0x040023E6 RID: 9190
		private IPrincipal logonUserPrincipal;

		// Token: 0x040023E7 RID: 9191
		private ExchangePrincipal logonUserExchangePrincipal;

		// Token: 0x040023E8 RID: 9192
		private IIdentity accessedUserIdentity;

		// Token: 0x040023E9 RID: 9193
		private SecurityIdentifier accessedUserSid;

		// Token: 0x040023EA RID: 9194
		private ExchangePrincipal accessedUserExchangePrincipal;

		// Token: 0x040023EB RID: 9195
		private bool identityResolved;

		// Token: 0x040023EC RID: 9196
		private bool logonUserResolved;

		// Token: 0x040023ED RID: 9197
		private bool hasFullAccess;

		// Token: 0x040023EE RID: 9198
		private bool logonUserEsoSelf;

		// Token: 0x040023EF RID: 9199
		private string cacheKey;
	}
}
