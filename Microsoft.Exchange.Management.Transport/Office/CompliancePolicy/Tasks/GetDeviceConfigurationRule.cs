using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200010F RID: 271
	[Cmdlet("Get", "DeviceConfigurationRule", DefaultParameterSetName = "Identity")]
	public sealed class GetDeviceConfigurationRule : GetDeviceRuleBase
	{
		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002C6C0 File Offset: 0x0002A8C0
		public GetDeviceConfigurationRule() : base(PolicyScenario.DeviceSettings)
		{
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002C6C9 File Offset: 0x0002A8C9
		protected override bool IsDeviceRule(string identity)
		{
			return DevicePolicyUtility.IsDeviceConfigurationRule(identity);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002C6D1 File Offset: 0x0002A8D1
		protected override DeviceRuleBase CreateDeviceRuleObject(RuleStorage ruleStorage)
		{
			return new DeviceConfigurationRule(ruleStorage);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002C6D9 File Offset: 0x0002A8D9
		protected override string GetCanOnlyManipulateErrorString()
		{
			return Strings.CanOnlyManipulateDeviceConfigurationRule;
		}
	}
}
