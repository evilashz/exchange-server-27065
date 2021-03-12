using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000109 RID: 265
	[Cmdlet("Set", "DeviceTenantPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDeviceTenantPolicy : SetDevicePolicyBase
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002C2DC File Offset: 0x0002A4DC
		public SetDeviceTenantPolicy() : base(PolicyScenario.DeviceTenantConditionalAccess)
		{
		}
	}
}
