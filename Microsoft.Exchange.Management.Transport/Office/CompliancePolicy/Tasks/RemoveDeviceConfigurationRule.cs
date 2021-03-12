using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000112 RID: 274
	[Cmdlet("Remove", "DeviceConfigurationRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDeviceConfigurationRule : RemoveDeviceRuleBase
	{
		// Token: 0x06000CAE RID: 3246 RVA: 0x0002E054 File Offset: 0x0002C254
		public RemoveDeviceConfigurationRule() : base(PolicyScenario.DeviceSettings)
		{
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002E05D File Offset: 0x0002C25D
		protected override DeviceRuleBase CreateDeviceRule(RuleStorage ruleStorage)
		{
			return new DeviceConfigurationRule(base.DataObject);
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0002E06A File Offset: 0x0002C26A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.RemoveDeviceConfiguationRuleConfirmation(this.Identity.ToString());
			}
		}
	}
}
