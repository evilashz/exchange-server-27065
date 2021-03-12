using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200010B RID: 267
	[Cmdlet("Get", "DeviceTenantRule", DefaultParameterSetName = "Identity")]
	public sealed class GetDeviceTenantRule : GetDeviceRuleBase
	{
		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002C2EE File Offset: 0x0002A4EE
		public GetDeviceTenantRule() : base(PolicyScenario.DeviceTenantConditionalAccess)
		{
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002C2F7 File Offset: 0x0002A4F7
		protected override bool IsDeviceRule(string identity)
		{
			return DevicePolicyUtility.IsDeviceTenantRule(identity);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002C2FF File Offset: 0x0002A4FF
		protected override DeviceRuleBase CreateDeviceRuleObject(RuleStorage ruleStorage)
		{
			return new DeviceTenantRule(ruleStorage);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002C307 File Offset: 0x0002A507
		protected override string GetCanOnlyManipulateErrorString()
		{
			return Strings.CanOnlyManipulateDeviceTenantRule;
		}
	}
}
