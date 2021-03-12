using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ClientAccessRules;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000633 RID: 1587
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ClientAccessRuleCollectionCacheableItem : TenantConfigurationCacheableItem<Organization>
	{
		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x06004B20 RID: 19232 RVA: 0x001151A9 File Offset: 0x001133A9
		public override long ItemSize
		{
			get
			{
				return (long)this.estimatedSize;
			}
		}

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x06004B21 RID: 19233 RVA: 0x001151B2 File Offset: 0x001133B2
		// (set) Token: 0x06004B22 RID: 19234 RVA: 0x001151BA File Offset: 0x001133BA
		public ClientAccessRuleCollection ClientAccessRuleCollection { get; private set; }

		// Token: 0x06004B23 RID: 19235 RVA: 0x001151C4 File Offset: 0x001133C4
		public override void ReadData(IConfigurationSession configurationSession)
		{
			IEnumerable<ADClientAccessRule> enumerable = this.ReadRawData(configurationSession);
			this.ClientAccessRuleCollection = new ClientAccessRuleCollection(configurationSession.GetOrgContainerId().ToString());
			this.estimatedSize = 0;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.ClientAccessRulesCommon.ImplicitAllowLocalClientAccessRulesEnabled.Enabled && (null == configurationSession.SessionSettings.CurrentOrganizationId || OrganizationId.ForestWideOrgId.Equals(configurationSession.SessionSettings.CurrentOrganizationId)))
			{
				ClientAccessRule allowLocalClientAccessRule = ClientAccessRulesUtils.GetAllowLocalClientAccessRule();
				if (allowLocalClientAccessRule != null)
				{
					this.ClientAccessRuleCollection.AddWithoutNameCheck(allowLocalClientAccessRule);
					this.estimatedSize += allowLocalClientAccessRule.GetEstimatedSize();
				}
			}
			foreach (ADClientAccessRule adclientAccessRule in enumerable)
			{
				ClientAccessRule clientAccessRule = adclientAccessRule.GetClientAccessRule();
				this.ClientAccessRuleCollection.AddWithoutNameCheck(clientAccessRule);
				this.estimatedSize += clientAccessRule.GetEstimatedSize();
			}
		}

		// Token: 0x06004B24 RID: 19236 RVA: 0x001152C4 File Offset: 0x001134C4
		private IEnumerable<ADClientAccessRule> ReadRawData(IConfigurationSession configurationSession)
		{
			IEnumerable<ADClientAccessRule> adClientAccessRules = configurationSession.FindAllPaged<ADClientAccessRule>().ReadAllPages();
			ClientAccessRulesPriorityManager clientAccessRulesPriorityManager = new ClientAccessRulesPriorityManager(adClientAccessRules);
			return clientAccessRulesPriorityManager.ADClientAccessRules;
		}

		// Token: 0x040033A3 RID: 13219
		private int estimatedSize;
	}
}
