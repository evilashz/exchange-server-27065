using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000075 RID: 117
	internal class ADJournalRuleStorageManager : ADRuleStorageManager
	{
		// Token: 0x060003AE RID: 942 RVA: 0x00014452 File Offset: 0x00012652
		public ADJournalRuleStorageManager(string ruleCollectionName, IConfigDataProvider session) : base(ruleCollectionName, session)
		{
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001445C File Offset: 0x0001265C
		public ADJournalRuleStorageManager(string ruleCollectionName, List<JournalRuleData> rules)
		{
			this.ruleCollectionName = ruleCollectionName;
			if (rules != null)
			{
				foreach (JournalRuleData journalRuleData in rules)
				{
					TransportRule transportRule = new TransportRule();
					transportRule.ImmutableId = journalRuleData.ImmutableId;
					transportRule.Xml = journalRuleData.Xml;
					transportRule.Priority = journalRuleData.Priority;
					transportRule.Name = journalRuleData.Name;
					this.adRules.Add(transportRule);
				}
				this.adRules.Sort(new Comparison<TransportRule>(ADRuleStorageManager.CompareTransportRule));
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00014510 File Offset: 0x00012710
		public static Guid GetLawfulInterceptTenantGuid(string lawfulInterceptTenantName)
		{
			Guid empty = Guid.Empty;
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromTenantAcceptedDomain(lawfulInterceptTenantName), 69, "GetLawfulInterceptTenantGuid", "f:\\15.00.1497\\sources\\dev\\MessagingPolicies\\src\\rules\\Journaling\\ADJournalRuleStorageManager.cs");
			ExchangeConfigurationUnit[] array = tenantOrTopologyConfigurationSession.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, null, null, 1);
			if (array != null && array.Length > 0 && !string.IsNullOrEmpty(array[0].ExternalDirectoryOrganizationId))
			{
				empty = new Guid(array[0].ExternalDirectoryOrganizationId);
			}
			return empty;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00014578 File Offset: 0x00012778
		protected override TransportRuleSerializer Serializer
		{
			get
			{
				return JournalingRuleSerializer.Instance;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0001457F File Offset: 0x0001277F
		protected override TransportRuleParser Parser
		{
			get
			{
				return JournalingRuleParser.Instance;
			}
		}
	}
}
