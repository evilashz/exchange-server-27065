using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.OutlookProtectionRules
{
	// Token: 0x02000AF5 RID: 2805
	[Cmdlet("Disable", "OutlookProtectionRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableOutlookProtectionRule : SystemConfigurationObjectActionTask<RuleIdParameter, TransportRule>
	{
		// Token: 0x17001E50 RID: 7760
		// (get) Token: 0x060063CA RID: 25546 RVA: 0x001A13FF File Offset: 0x0019F5FF
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableOutlookProtectionRule(this.Identity.ToString());
			}
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x001A1414 File Offset: 0x0019F614
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = RuleIdParameter.GetRuleCollectionRdn("OutlookProtectionRules");
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!Utils.IsChildOfOutlookProtectionRuleContainer(this.Identity))
			{
				throw new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound((this.Identity != null) ? this.Identity.ToString() : null, typeof(RuleIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null));
			}
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x001A14A4 File Offset: 0x0019F6A4
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = (TransportRule)base.PrepareDataObject();
			transportRule.Xml = new OutlookProtectionRulePresentationObject(transportRule)
			{
				Enabled = false
			}.Serialize();
			TaskLogger.LogExit();
			return transportRule;
		}
	}
}
