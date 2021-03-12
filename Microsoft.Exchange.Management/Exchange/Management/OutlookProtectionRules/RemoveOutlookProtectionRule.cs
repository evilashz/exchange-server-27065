using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.OutlookProtectionRules
{
	// Token: 0x02000AFE RID: 2814
	[Cmdlet("Remove", "OutlookProtectionRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveOutlookProtectionRule : RemoveSystemConfigurationObjectTask<RuleIdParameter, TransportRule>
	{
		// Token: 0x06006416 RID: 25622 RVA: 0x001A2214 File Offset: 0x001A0414
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

		// Token: 0x17001E63 RID: 7779
		// (get) Token: 0x06006417 RID: 25623 RVA: 0x001A22A1 File Offset: 0x001A04A1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveOutlookProtectionRule(this.Identity.ToString());
			}
		}
	}
}
