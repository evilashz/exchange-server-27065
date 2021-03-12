using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000A1F RID: 2591
	public abstract class DisableRuleTaskBase : SystemConfigurationObjectActionTask<RuleIdParameter, TransportRule>
	{
		// Token: 0x06005CD9 RID: 23769 RVA: 0x00187390 File Offset: 0x00185590
		public DisableRuleTaskBase(string ruleCollectionName)
		{
			this.ruleCollectionName = ruleCollectionName;
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x001873A0 File Offset: 0x001855A0
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

		// Token: 0x04003477 RID: 13431
		private readonly string ruleCollectionName;
	}
}
