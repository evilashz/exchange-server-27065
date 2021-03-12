using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000A1B RID: 2587
	public abstract class RemoveRuleTaskBase : RemoveSystemConfigurationObjectTask<RuleIdParameter, TransportRule>
	{
		// Token: 0x06005CCA RID: 23754 RVA: 0x00186FAA File Offset: 0x001851AA
		public RemoveRuleTaskBase(string ruleCollectionName)
		{
			this.ruleCollectionName = ruleCollectionName;
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x00186FBC File Offset: 0x001851BC
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

		// Token: 0x04003475 RID: 13429
		private readonly string ruleCollectionName;
	}
}
