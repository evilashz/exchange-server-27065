using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200087C RID: 2172
	internal static class ClientAccessRulesStorageManager
	{
		// Token: 0x06004B3F RID: 19263 RVA: 0x00137DDF File Offset: 0x00135FDF
		public static IEnumerable<ADClientAccessRule> GetClientAccessRules(IConfigurationSession session)
		{
			if (session.SessionSettings.ConfigScopes == ConfigScopes.AllTenants)
			{
				throw new ArgumentException(Strings.AllTenantsScopedSessionNotSupported);
			}
			return session.FindPaged<ADClientAccessRule>(null, ClientAccessRulesStorageManager.GetRulesADContainer(session), false, null, 0);
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x00137E10 File Offset: 0x00136010
		public static void SaveRules(IConfigurationSession session, IEnumerable<ADClientAccessRule> rules)
		{
			if (session.SessionSettings.ConfigScopes == ConfigScopes.AllTenants)
			{
				throw new ArgumentException(Strings.AllTenantsScopedSessionNotSupported);
			}
			foreach (ADClientAccessRule instanceToSave in rules)
			{
				session.Save(instanceToSave);
			}
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x00137E78 File Offset: 0x00136078
		public static bool IsADRuleValid(ADClientAccessRule rule)
		{
			return ClientAccessRulesStorageManager.IsAuthenticationTypeParameterValid(rule) && rule.ValidateUserRecipientFilterParsesWithSchema();
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x00137E8A File Offset: 0x0013608A
		private static ADObjectId GetRulesADContainer(IConfigurationSession session)
		{
			return session.GetOrgContainerId().GetChildId(ADClientAccessRuleCollection.ContainerName);
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x00137E9C File Offset: 0x0013609C
		private static bool IsAuthenticationTypeParameterValid(ADClientAccessRule rule)
		{
			if (rule.HasAnyOfSpecificProtocolsPredicate(new List<ClientAccessProtocol>
			{
				ClientAccessProtocol.RemotePowerShell
			}))
			{
				return !rule.HasAuthenticationMethodPredicate(ClientAccessAuthenticationMethod.AdfsAuthentication);
			}
			if (rule.HasAnyOfSpecificProtocolsPredicate(new List<ClientAccessProtocol>
			{
				ClientAccessProtocol.OutlookWebApp,
				ClientAccessProtocol.ExchangeAdminCenter
			}))
			{
				return !rule.HasAuthenticationMethodPredicate(ClientAccessAuthenticationMethod.NonBasicAuthentication);
			}
			return !rule.HasAnyAuthenticationMethodPredicate();
		}
	}
}
