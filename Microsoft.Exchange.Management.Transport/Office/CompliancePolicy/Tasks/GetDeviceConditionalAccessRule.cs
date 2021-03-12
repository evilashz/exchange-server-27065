using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000FD RID: 253
	[Cmdlet("Get", "DeviceConditionalAccessRule", DefaultParameterSetName = "Identity")]
	public sealed class GetDeviceConditionalAccessRule : GetDeviceRuleBase
	{
		// Token: 0x06000A49 RID: 2633 RVA: 0x000297D7 File Offset: 0x000279D7
		public GetDeviceConditionalAccessRule() : base(PolicyScenario.DeviceConditionalAccess)
		{
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000297E0 File Offset: 0x000279E0
		protected override bool IsDeviceRule(string identity)
		{
			return DevicePolicyUtility.IsDeviceConditionalAccessRule(identity);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000297E8 File Offset: 0x000279E8
		protected override DeviceRuleBase CreateDeviceRuleObject(RuleStorage ruleStorage)
		{
			return new DeviceConditionalAccessRule(ruleStorage);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000297F0 File Offset: 0x000279F0
		protected override string GetCanOnlyManipulateErrorString()
		{
			return Strings.CanOnlyManipulateDeviceConditionalAccessRule;
		}
	}
}
