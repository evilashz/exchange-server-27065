using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200010E RID: 270
	[Cmdlet("Remove", "DeviceTenantRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDeviceTenantRule : RemoveDeviceRuleBase
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002C698 File Offset: 0x0002A898
		public RemoveDeviceTenantRule() : base(PolicyScenario.DeviceTenantConditionalAccess)
		{
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002C6A1 File Offset: 0x0002A8A1
		protected override DeviceRuleBase CreateDeviceRule(RuleStorage ruleStorage)
		{
			return new DeviceTenantRule(base.DataObject);
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0002C6AE File Offset: 0x0002A8AE
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.RemoveDeviceTenantRuleConfirmation(this.Identity.ToString());
			}
		}
	}
}
