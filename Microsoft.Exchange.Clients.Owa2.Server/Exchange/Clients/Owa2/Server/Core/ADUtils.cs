using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200022C RID: 556
	internal static class ADUtils
	{
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0004B180 File Offset: 0x00049380
		private static TenantConfigurationCache<PolicyTipRulesPerTenantSettings> PolicyTipRulesPerTenantSettingsCache
		{
			get
			{
				if (ADUtils.policyTipRulesPerTenantSettingsCache == null)
				{
					lock (ADUtils.lockObjectRules)
					{
						if (ADUtils.policyTipRulesPerTenantSettingsCache == null)
						{
							ADUtils.policyTipRulesPerTenantSettingsCache = new TenantConfigurationCache<PolicyTipRulesPerTenantSettings>(ADUtils.policyTipRulesCacheSize, ADUtils.policyTipRulesCacheExpirationInterval, ADUtils.policyTipRulesCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.OwaPerTenantCacheTracer, "PolicyTipRulesPerTenantSettings"), new PerTenantCachePerformanceCounters("PolicyTipRulesPerTenantSettings"));
						}
					}
				}
				return ADUtils.policyTipRulesPerTenantSettingsCache;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0004B200 File Offset: 0x00049400
		private static TenantConfigurationCache<PerTenantPolicyTipMessageConfig> PerTenantPolicyTipMessageConfigCache
		{
			get
			{
				if (ADUtils.perTenantPolicyTipMessageConfigCache == null)
				{
					lock (ADUtils.lockObjectMessageConfig)
					{
						if (ADUtils.perTenantPolicyTipMessageConfigCache == null)
						{
							ADUtils.perTenantPolicyTipMessageConfigCache = new TenantConfigurationCache<PerTenantPolicyTipMessageConfig>(ADUtils.policyTipMessageConfigCacheSize, ADUtils.policyTipMessageConfigCacheExpirationInterval, ADUtils.policyTipMessageConfigCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.OwaPerTenantCacheTracer, "PerTenantPolicyTipMessageConfig"), new PerTenantCachePerformanceCounters("PerTenantPolicyTipMessageConfig"));
						}
					}
				}
				return ADUtils.perTenantPolicyTipMessageConfigCache;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0004B280 File Offset: 0x00049480
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x0004B29D File Offset: 0x0004949D
		internal static ADRecipientSessionContext ADRecipientSessionContext
		{
			get
			{
				if (ADUtils.adRecipientSessionContext == null)
				{
					return CallContext.Current.ADRecipientSessionContext;
				}
				return ADUtils.adRecipientSessionContext;
			}
			set
			{
				ADUtils.adRecipientSessionContext = value;
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0004B2A8 File Offset: 0x000494A8
		public static void ResetPolicyTipMessageConfigCache()
		{
			lock (ADUtils.lockObjectMessageConfig)
			{
				ADUtils.perTenantPolicyTipMessageConfigCache = null;
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0004B2E8 File Offset: 0x000494E8
		public static void ResetPolicyTipRulesCache()
		{
			lock (ADUtils.lockObjectRules)
			{
				ADUtils.policyTipRulesPerTenantSettingsCache = null;
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0004B328 File Offset: 0x00049528
		public static PolicyTipRulesPerTenantSettings GetPolicyTipRulesPerTenantSettings(OrganizationId organizationId)
		{
			return ADUtils.PolicyTipRulesPerTenantSettingsCache.GetValue(organizationId);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0004B338 File Offset: 0x00049538
		internal static PolicyTipCustomizedStrings GetPolicyTipStrings(OrganizationId organizationId, string locale)
		{
			if (string.IsNullOrEmpty(locale))
			{
				throw new ArgumentNullException("locale");
			}
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			PerTenantPolicyTipMessageConfig value = ADUtils.PerTenantPolicyTipMessageConfigCache.GetValue(organizationId);
			PolicyTipCustomizedStrings policyTipCustomizedStrings = null;
			string policyTipMessage = value.GetPolicyTipMessage(string.Empty, PolicyTipMessageConfigAction.Url);
			string policyTipMessage2 = value.GetPolicyTipMessage(locale, PolicyTipMessageConfigAction.NotifyOnly);
			string policyTipMessage3 = value.GetPolicyTipMessage(locale, PolicyTipMessageConfigAction.RejectOverride);
			string policyTipMessage4 = value.GetPolicyTipMessage(locale, PolicyTipMessageConfigAction.Reject);
			if (!string.IsNullOrEmpty(policyTipMessage) || !string.IsNullOrEmpty(policyTipMessage2) || !string.IsNullOrEmpty(policyTipMessage3) || !string.IsNullOrEmpty(policyTipMessage4))
			{
				policyTipCustomizedStrings = new PolicyTipCustomizedStrings();
				policyTipCustomizedStrings.ComplianceURL = policyTipMessage;
				policyTipCustomizedStrings.PolicyTipMessageNotifyString = policyTipMessage2;
				policyTipCustomizedStrings.PolicyTipMessageOverrideString = policyTipMessage3;
				policyTipCustomizedStrings.PolicyTipMessageBlockString = policyTipMessage4;
			}
			return policyTipCustomizedStrings;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0004B3EC File Offset: 0x000495EC
		public static ShortList<string> GetAllEmailAddresses(ShortList<string> addressesToExpand, OrganizationId organizationId)
		{
			if (addressesToExpand == null)
			{
				return null;
			}
			ShortList<string> shortList = new ShortList<string>();
			foreach (string emailAddress in addressesToExpand)
			{
				string[] allEmailAddresses = ADUtils.GetAllEmailAddresses(emailAddress, organizationId);
				if (allEmailAddresses != null)
				{
					foreach (string item in allEmailAddresses)
					{
						shortList.Add(item);
					}
				}
			}
			return shortList;
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0004B498 File Offset: 0x00049698
		public static string[] GetAllEmailAddresses(string emailAddress, OrganizationId organizationId)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentNullException("emailAddress");
			}
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			if (emailAddress.IndexOf('@') > 0)
			{
				if (!SmtpAddress.IsValidSmtpAddress(emailAddress))
				{
					throw new ArgumentException(string.Format("emailAddress:{0} is not a valid ProxyAddress", emailAddress));
				}
				ProxyAddress proxyAddress = new SmtpProxyAddress(emailAddress, false);
				IRecipientSession recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 345, "GetAllEmailAddresses", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\PolicyTips\\ADUtils.cs");
				ADRawEntry lookupResult = null;
				ADNotificationAdapter.RunADOperation(delegate()
				{
					lookupResult = recipientSession.FindByProxyAddress(proxyAddress, ADUtils.PropertiesToGet);
				});
				if (lookupResult != null)
				{
					ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)lookupResult[ADRecipientSchema.EmailAddresses];
					if (proxyAddressCollection != null && proxyAddressCollection.Count > 0)
					{
						string[] array = new string[proxyAddressCollection.Count];
						int num = 0;
						foreach (ProxyAddress proxyAddress2 in proxyAddressCollection)
						{
							array[num++] = proxyAddress2.AddressString;
						}
						return array;
					}
				}
			}
			return new string[]
			{
				emailAddress
			};
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0004B5DC File Offset: 0x000497DC
		public static bool IsMemberOf(string emailAddress, RoutingAddress distributionListRoutingAddress, OrganizationId organizationId)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentNullException("emailAddress");
			}
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			if (!SmtpAddress.IsValidSmtpAddress(emailAddress))
			{
				throw new ArgumentException(string.Format("emailAddress:{0} is not a valid ProxyAddress", emailAddress));
			}
			UserIdentity userIdentity = ADIdentityInformationCache.Singleton.GetUserIdentity(emailAddress, ADUtils.ADRecipientSessionContext);
			Guid objectGuid = userIdentity.ObjectGuid;
			CachedOrganizationConfiguration cachedOrganizationConfiguration = ADUtils.GetCachedOrganizationConfiguration(organizationId);
			return cachedOrganizationConfiguration.GroupsConfiguration.IsMemberOf(objectGuid, distributionListRoutingAddress);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0004B658 File Offset: 0x00049858
		public static bool IsAnyInternal(List<RoutingAddress> routingAddresses, OrganizationId organizationId, ConditionEvaluationMode mode, ref List<string> internalPropertyValues)
		{
			if (routingAddresses == null)
			{
				throw new ArgumentNullException("routingAddresses");
			}
			if (internalPropertyValues == null)
			{
				internalPropertyValues = new List<string>();
			}
			bool result = false;
			foreach (RoutingAddress routingAddress in routingAddresses)
			{
				if (ADUtils.IsInternal(routingAddress, organizationId))
				{
					if (mode == ConditionEvaluationMode.Optimized)
					{
						return true;
					}
					internalPropertyValues.Add(routingAddress.ToString());
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0004B6E4 File Offset: 0x000498E4
		public static bool IsInternal(RoutingAddress routingAddress, OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			if (string.IsNullOrEmpty(routingAddress.DomainPart))
			{
				throw new ArgumentException(string.Format("routingAddress.DomainPart is null or empty for the routingaddress:{0}.", routingAddress));
			}
			OwaPerTenantAcceptedDomains owaPerTenantAcceptedDomains = ADCacheUtils.GetOwaPerTenantAcceptedDomains(organizationId);
			OwaPerTenantRemoteDomains owaPerTenantRemoteDomains = ADCacheUtils.GetOwaPerTenantRemoteDomains(organizationId);
			IsInternalResolver isInternalResolver = new IsInternalResolver(organizationId, new IsInternalResolver.GetAcceptedDomainCollectionDelegate(owaPerTenantAcceptedDomains.GetAcceptedDomainMap), new IsInternalResolver.GetRemoteDomainCollectionDelegate(owaPerTenantRemoteDomains.GetRemoteDomainMap));
			return isInternalResolver.IsInternal(new RoutingDomain(routingAddress.DomainPart));
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0004B768 File Offset: 0x00049968
		public static bool IsAnyExternal(List<RoutingAddress> routingAddresses, OrganizationId organizationId, ConditionEvaluationMode mode, ref List<string> externalPropertyValues)
		{
			if (routingAddresses == null)
			{
				throw new ArgumentNullException("routingAddresses");
			}
			if (externalPropertyValues == null)
			{
				externalPropertyValues = new List<string>();
			}
			bool result = false;
			foreach (RoutingAddress routingAddress in routingAddresses)
			{
				if (!ADUtils.IsInternal(routingAddress, organizationId))
				{
					if (mode == ConditionEvaluationMode.Optimized)
					{
						return true;
					}
					externalPropertyValues.Add(routingAddress.ToString());
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0004B7F4 File Offset: 0x000499F4
		public static bool IsExternal(RoutingAddress routingAddress, OrganizationId organizationId)
		{
			return !ADUtils.IsInternal(routingAddress, organizationId);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0004B800 File Offset: 0x00049A00
		public static bool IsAnyExternalPartner(List<RoutingAddress> routingAddresses, OrganizationId organizationId, ConditionEvaluationMode mode, ref List<string> externalPartnerPropertyValues)
		{
			if (routingAddresses == null)
			{
				throw new ArgumentNullException("routingAddresses");
			}
			if (externalPartnerPropertyValues == null)
			{
				externalPartnerPropertyValues = new List<string>();
			}
			bool result = false;
			foreach (RoutingAddress routingAddress in routingAddresses)
			{
				if (ADUtils.IsExternalPartner(routingAddress, organizationId))
				{
					if (mode == ConditionEvaluationMode.Optimized)
					{
						return true;
					}
					externalPartnerPropertyValues.Add(routingAddress.ToString());
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0004B88C File Offset: 0x00049A8C
		public static bool IsExternalPartner(RoutingAddress routingAddress, OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("emailAddress");
			}
			if (string.IsNullOrEmpty(routingAddress.DomainPart))
			{
				throw new ArgumentException(string.Format("routingAddress.DomainPart is null or empty for the routingaddress:{0}.", routingAddress));
			}
			OwaPerTenantTransportSettings owaPerTenantTransportSettings = ADCacheUtils.GetOwaPerTenantTransportSettings(organizationId);
			return owaPerTenantTransportSettings.IsTLSSendSecureDomain(routingAddress.DomainPart);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0004B8E8 File Offset: 0x00049AE8
		public static bool IsAnyExternalNonPartner(List<RoutingAddress> routingAddresses, OrganizationId organizationId, ConditionEvaluationMode mode, ref List<string> externalNonPartnerPropertyValues)
		{
			bool result = false;
			if (externalNonPartnerPropertyValues == null)
			{
				externalNonPartnerPropertyValues = new List<string>();
			}
			foreach (RoutingAddress routingAddress in routingAddresses)
			{
				if (ADUtils.IsExternal(routingAddress, organizationId) && !ADUtils.IsExternalPartner(routingAddress, organizationId))
				{
					if (mode == ConditionEvaluationMode.Optimized)
					{
						return true;
					}
					externalNonPartnerPropertyValues.Add(routingAddress.ToString());
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0004B970 File Offset: 0x00049B70
		private static CachedOrganizationConfiguration GetCachedOrganizationConfiguration(OrganizationId organizationId)
		{
			return CachedOrganizationConfiguration.GetInstance(organizationId, CachedOrganizationConfiguration.ConfigurationTypes.GroupsConfiguration);
		}

		// Token: 0x04000B6B RID: 2923
		private const string PerTenantCachePolicyTipRulesName = "PolicyTipRulesPerTenantSettings";

		// Token: 0x04000B6C RID: 2924
		private const string PerTenantCachePolicyTipMessageConfigName = "PerTenantPolicyTipMessageConfig";

		// Token: 0x04000B6D RID: 2925
		private static long policyTipRulesCacheSize = BaseApplication.GetAppSetting<long>("DlpPolicyTipRulesCacheSize", 50L) * 1024L * 1024L;

		// Token: 0x04000B6E RID: 2926
		private static TimeSpan policyTipRulesCacheExpirationInterval = TimeSpan.FromMinutes((double)BaseApplication.GetAppSetting<int>("DlpPolicyTipRulesCacheExpirationInterval", 15));

		// Token: 0x04000B6F RID: 2927
		private static TimeSpan policyTipRulesCacheCleanupInterval = TimeSpan.FromMinutes((double)BaseApplication.GetAppSetting<int>("DlpPolicyTipRulesCacheCleanupInterval", 15));

		// Token: 0x04000B70 RID: 2928
		private static TenantConfigurationCache<PolicyTipRulesPerTenantSettings> policyTipRulesPerTenantSettingsCache;

		// Token: 0x04000B71 RID: 2929
		private static long policyTipMessageConfigCacheSize = BaseApplication.GetAppSetting<long>("DlpPolicyTipMessageConfigCacheSize", 50L) * 1024L * 1024L;

		// Token: 0x04000B72 RID: 2930
		private static TimeSpan policyTipMessageConfigCacheExpirationInterval = TimeSpan.FromMinutes((double)BaseApplication.GetAppSetting<int>("DlpPolicyTipMessageConfigCacheExpirationInterval", 60));

		// Token: 0x04000B73 RID: 2931
		private static TimeSpan policyTipMessageConfigCacheCleanupInterval = TimeSpan.FromMinutes((double)BaseApplication.GetAppSetting<int>("DlpPolicyTipMessageConfigCacheCleanupInterval", 60));

		// Token: 0x04000B74 RID: 2932
		private static TenantConfigurationCache<PerTenantPolicyTipMessageConfig> perTenantPolicyTipMessageConfigCache;

		// Token: 0x04000B75 RID: 2933
		private static readonly List<PropertyDefinition> PropertiesToGet = new List<PropertyDefinition>
		{
			ADRecipientSchema.EmailAddresses
		};

		// Token: 0x04000B76 RID: 2934
		private static object lockObjectRules = new object();

		// Token: 0x04000B77 RID: 2935
		private static object lockObjectMessageConfig = new object();

		// Token: 0x04000B78 RID: 2936
		private static volatile ADRecipientSessionContext adRecipientSessionContext;
	}
}
