using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A48 RID: 2632
	public abstract class GetHygieneFilterRuleTaskBase : GetMultitenancySystemConfigurationObjectTask<RuleIdParameter, TransportRule>
	{
		// Token: 0x06005E2D RID: 24109 RVA: 0x0018AEC0 File Offset: 0x001890C0
		protected GetHygieneFilterRuleTaskBase(string ruleCollectionName)
		{
			this.ruleCollectionName = ruleCollectionName;
			this.State = RuleState.Enabled;
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x17001C5B RID: 7259
		// (get) Token: 0x06005E2E RID: 24110 RVA: 0x0018AEE1 File Offset: 0x001890E1
		// (set) Token: 0x06005E2F RID: 24111 RVA: 0x0018AEF8 File Offset: 0x001890F8
		[Parameter(Mandatory = false)]
		public RuleState State
		{
			get
			{
				return (RuleState)base.Fields["State"];
			}
			set
			{
				base.Fields["State"] = value;
			}
		}

		// Token: 0x17001C5C RID: 7260
		// (get) Token: 0x06005E30 RID: 24112 RVA: 0x0018AF10 File Offset: 0x00189110
		protected override ObjectId RootId
		{
			get
			{
				if (this.Identity == null)
				{
					return RuleIdParameter.GetRuleCollectionId(base.DataSession, this.ruleCollectionName);
				}
				return null;
			}
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x0018AF2D File Offset: 0x0018912D
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = RuleIdParameter.GetRuleCollectionRdn(this.ruleCollectionName);
			}
			base.InternalValidate();
		}

		// Token: 0x06005E32 RID: 24114
		internal abstract IConfigurable CreateCorruptTaskRule(int priority, TransportRule transportRule, string errorText);

		// Token: 0x06005E33 RID: 24115
		internal abstract IConfigurable CreateTaskRuleFromInternalRule(TransportRule rule, int priority, TransportRule transportRule);

		// Token: 0x06005E34 RID: 24116 RVA: 0x0018AF54 File Offset: 0x00189154
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			try
			{
				if (this.Identity == null)
				{
					ADRuleStorageManager adruleStorageManager = new ADRuleStorageManager(this.ruleCollectionName, base.DataSession);
					adruleStorageManager.LoadRuleCollectionWithoutParsing();
					for (int i = 0; i < adruleStorageManager.Count; i++)
					{
						TransportRule transportRule;
						adruleStorageManager.GetRuleWithoutParsing(i, out transportRule);
						this.OutputRule(i, transportRule);
					}
				}
				else
				{
					List<TransportRule> list = new List<TransportRule>();
					list.AddRange((IEnumerable<TransportRule>)dataObjects);
					Dictionary<OrganizationId, ADRuleStorageManager> ruleCollections = this.GetRuleCollections(list);
					foreach (KeyValuePair<OrganizationId, ADRuleStorageManager> keyValuePair in ruleCollections)
					{
						for (int j = 0; j < keyValuePair.Value.Count; j++)
						{
							TransportRule transportRule;
							keyValuePair.Value.GetRuleWithoutParsing(j, out transportRule);
							if (Utils.IsRuleIdInList(transportRule.Id, list))
							{
								this.OutputRule(j, transportRule);
							}
						}
					}
				}
			}
			catch (RuleCollectionNotInAdException)
			{
			}
		}

		// Token: 0x06005E35 RID: 24117 RVA: 0x0018B054 File Offset: 0x00189254
		private Dictionary<OrganizationId, ADRuleStorageManager> GetRuleCollections(IEnumerable<TransportRule> rules)
		{
			Dictionary<OrganizationId, ADRuleStorageManager> dictionary = new Dictionary<OrganizationId, ADRuleStorageManager>();
			foreach (TransportRule transportRule in rules)
			{
				if (!dictionary.ContainsKey(transportRule.OrganizationId))
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, transportRule.OrganizationId, base.ExecutingUserOrganizationId, false);
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 169, "GetRuleCollections", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageHygiene\\HygieneConfiguration\\GetHygieneFilterRuleTaskBase.cs");
					ADRuleStorageManager adruleStorageManager;
					try
					{
						adruleStorageManager = new ADRuleStorageManager(this.ruleCollectionName, tenantOrTopologyConfigurationSession);
						adruleStorageManager.LoadRuleCollectionWithoutParsing();
					}
					catch (RuleCollectionNotInAdException)
					{
						continue;
					}
					dictionary.Add(transportRule.OrganizationId, adruleStorageManager);
				}
			}
			return dictionary;
		}

		// Token: 0x06005E36 RID: 24118 RVA: 0x0018B128 File Offset: 0x00189328
		private void OutputRule(int priority, TransportRule transportRule)
		{
			TransportRule transportRule2 = null;
			string errorText = string.Empty;
			if (base.NeedSuppressingPiiData)
			{
				base.ExchangeRunspaceConfig.EnablePiiMap = true;
			}
			try
			{
				transportRule2 = (TransportRule)TransportRuleParser.Instance.GetRule(transportRule.Xml);
			}
			catch (ParserException ex)
			{
				errorText = ex.Message;
			}
			if (transportRule2 == null)
			{
				this.WriteResult(this.CreateCorruptTaskRule(priority, transportRule, errorText));
				return;
			}
			if (this.StateMatches(transportRule2))
			{
				IConfigurable configurable = this.CreateTaskRuleFromInternalRule(transportRule2, priority, transportRule);
				if (base.NeedSuppressingPiiData)
				{
					((HygieneFilterRule)configurable).SuppressPiiData(Utils.GetSessionPiiMap(base.ExchangeRunspaceConfig));
				}
				this.WriteResult(configurable);
			}
		}

		// Token: 0x06005E37 RID: 24119 RVA: 0x0018B1D0 File Offset: 0x001893D0
		private bool StateMatches(TransportRule rule)
		{
			return !base.Fields.IsModified("State") || this.State == rule.Enabled;
		}

		// Token: 0x040034C9 RID: 13513
		private readonly string ruleCollectionName;
	}
}
