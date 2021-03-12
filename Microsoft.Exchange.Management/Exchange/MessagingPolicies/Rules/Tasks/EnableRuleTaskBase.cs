using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000A1D RID: 2589
	public abstract class EnableRuleTaskBase : SystemConfigurationObjectActionTask<RuleIdParameter, TransportRule>
	{
		// Token: 0x06005CD1 RID: 23761 RVA: 0x0018716C File Offset: 0x0018536C
		public EnableRuleTaskBase(string ruleCollectionName)
		{
			this.ruleCollectionName = ruleCollectionName;
		}

		// Token: 0x17001BD7 RID: 7127
		// (get) Token: 0x06005CD2 RID: 23762 RVA: 0x0018717B File Offset: 0x0018537B
		public string RuleCollectionName
		{
			get
			{
				return this.ruleCollectionName;
			}
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x00187184 File Offset: 0x00185384
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = RuleIdParameter.GetRuleCollectionRdn(this.ruleCollectionName);
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!Utils.IsChildOfRuleContainer(this.Identity, this.ruleCollectionName))
			{
				throw new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound((this.Identity != null) ? this.Identity.ToString() : null, typeof(RuleIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null));
			}
		}

		// Token: 0x04003476 RID: 13430
		private readonly string ruleCollectionName;
	}
}
