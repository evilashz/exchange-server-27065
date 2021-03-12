using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000106 RID: 262
	[Cmdlet("Remove", "DeviceConditionalAccessRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDeviceConditionalAccessRule : RemoveDeviceRuleBase
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x0002C16D File Offset: 0x0002A36D
		public RemoveDeviceConditionalAccessRule() : base(PolicyScenario.DeviceConditionalAccess)
		{
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002C176 File Offset: 0x0002A376
		protected override DeviceRuleBase CreateDeviceRule(RuleStorage ruleStorage)
		{
			return new DeviceConditionalAccessRule(base.DataObject);
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002C183 File Offset: 0x0002A383
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.RemoveDeviceConditionalAccessRuleConfirmation(this.Identity.ToString());
			}
		}
	}
}
