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
	// Token: 0x02000AF6 RID: 2806
	[Cmdlet("Enable", "OutlookProtectionRule", SupportsShouldProcess = true)]
	public sealed class EnableOutlookProtectionRule : SystemConfigurationObjectActionTask<RuleIdParameter, TransportRule>
	{
		// Token: 0x17001E51 RID: 7761
		// (get) Token: 0x060063CE RID: 25550 RVA: 0x001A14EA File Offset: 0x0019F6EA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableOutlookProtectionRule(this.Identity.ToString());
			}
		}

		// Token: 0x060063CF RID: 25551 RVA: 0x001A14FC File Offset: 0x0019F6FC
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

		// Token: 0x060063D0 RID: 25552 RVA: 0x001A158C File Offset: 0x0019F78C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = (TransportRule)base.PrepareDataObject();
			transportRule.Xml = new OutlookProtectionRulePresentationObject(transportRule)
			{
				Enabled = true
			}.Serialize();
			TaskLogger.LogExit();
			return transportRule;
		}
	}
}
