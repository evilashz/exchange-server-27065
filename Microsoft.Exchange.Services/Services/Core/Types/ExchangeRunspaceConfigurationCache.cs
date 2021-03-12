using System;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.PartnerToken;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200076A RID: 1898
	internal class ExchangeRunspaceConfigurationCache : BaseWebCache<string, ExchangeRunspaceConfiguration>
	{
		// Token: 0x060038A6 RID: 14502 RVA: 0x000C86C3 File Offset: 0x000C68C3
		internal ExchangeRunspaceConfigurationCache() : base(ExchangeRunspaceConfigurationCache.ExchangeRunspaceConfigurationCacheKeyPrefix, SlidingOrAbsoluteTimeout.Absolute, ExchangeRunspaceConfigurationCache.CacheTimeoutInMinutes)
		{
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x060038A7 RID: 14503 RVA: 0x000C86D6 File Offset: 0x000C68D6
		public static ExchangeRunspaceConfigurationCache Singleton
		{
			get
			{
				return ExchangeRunspaceConfigurationCache.singleton;
			}
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x000C86E0 File Offset: 0x000C68E0
		public ExchangeRunspaceConfiguration Get(IIdentity impersonatingIdentity, OrganizationId impersonatedOrganizationId, bool forceNewRunspace = false)
		{
			string impersonatingUserId = string.Empty;
			if (impersonatingIdentity is PartnerIdentity)
			{
				PartnerIdentity partnerIdentity = impersonatingIdentity as PartnerIdentity;
				DelegatedPrincipal delegatedPrincipal = partnerIdentity.DelegatedPrincipal;
				impersonatingUserId = delegatedPrincipal.ToString();
				impersonatingIdentity = delegatedPrincipal.Identity;
			}
			else if (impersonatingIdentity is WindowsIdentity)
			{
				WindowsIdentity windowsIdentity = impersonatingIdentity as WindowsIdentity;
				impersonatingUserId = windowsIdentity.User.ToString();
			}
			else if (impersonatingIdentity is ClientSecurityContextIdentity)
			{
				ClientSecurityContextIdentity clientSecurityContextIdentity = impersonatingIdentity as ClientSecurityContextIdentity;
				impersonatingUserId = clientSecurityContextIdentity.Sid.ToString();
			}
			return this.Get(impersonatingIdentity, impersonatingUserId, impersonatedOrganizationId, forceNewRunspace);
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000C8760 File Offset: 0x000C6960
		public ExchangeRunspaceConfiguration Get(AuthZClientInfo clientInfo, OrganizationId impersonatedOrganizationId, bool forceNewRunspace = false)
		{
			if (clientInfo == null || clientInfo.ObjectSid == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<bool, bool>((long)this.GetHashCode(), "[ExchangeRunspaceConfigurationCache::Get] Either clientInfo was null ({0}) or clientInfo.ObjectSid was null ({1})", clientInfo == null, clientInfo != null && clientInfo.ObjectSid == null);
				throw new ImpersonateUserDeniedException();
			}
			IIdentity impersonatingIdentity = new SidWithGroupsIdentity(clientInfo.ObjectSid.ToString(), string.Empty, clientInfo.ClientSecurityContext);
			string impersonatingUserId = clientInfo.ObjectSid.ToString();
			return this.Get(impersonatingIdentity, impersonatingUserId, impersonatedOrganizationId, forceNewRunspace);
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000C87E4 File Offset: 0x000C69E4
		private ExchangeRunspaceConfiguration Get(IIdentity impersonatingIdentity, string impersonatingUserId, OrganizationId impersonatedOrganizationId, bool forceNewRunspace)
		{
			string text = this.ConstructKey(impersonatingUserId, impersonatedOrganizationId);
			ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = null;
			if (!forceNewRunspace)
			{
				exchangeRunspaceConfiguration = base.Get(text);
			}
			if (exchangeRunspaceConfiguration == null)
			{
				ExchangeRunspaceConfigurationSettings settings;
				if (impersonatedOrganizationId == null)
				{
					settings = ExchangeRunspaceConfigurationSettings.GetDefaultInstance();
				}
				else
				{
					string tenantOrganization = null;
					if (impersonatedOrganizationId.ConfigurationUnit != null)
					{
						tenantOrganization = impersonatedOrganizationId.ConfigurationUnit.Parent.Name;
					}
					settings = new ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication.EWS, tenantOrganization, ExchangeRunspaceConfigurationSettings.SerializationLevel.None);
				}
				exchangeRunspaceConfiguration = new ExchangeRunspaceConfiguration(impersonatingIdentity, settings);
				this.ForceAdd(text, exchangeRunspaceConfiguration);
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "[ExchangeRunspaceConfigurationCache::Get] No ExchangeRunspaceConfiguration with key: {0}", text);
			}
			return exchangeRunspaceConfiguration;
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000C886B File Offset: 0x000C6A6B
		private string ConstructKey(string sidOrName, OrganizationId organizationId)
		{
			return sidOrName + ((organizationId != null) ? (":" + organizationId.ToString()) : string.Empty);
		}

		// Token: 0x04001F6A RID: 8042
		private static readonly string ExchangeRunspaceConfigurationCacheKeyPrefix = "_ERCKP_";

		// Token: 0x04001F6B RID: 8043
		private static readonly int CacheTimeoutInMinutes = Global.GetAppSettingAsInt("ExchangeRunspaceConfigurationCacheTimeoutInMinutes", 5);

		// Token: 0x04001F6C RID: 8044
		private static ExchangeRunspaceConfigurationCache singleton = new ExchangeRunspaceConfigurationCache();
	}
}
