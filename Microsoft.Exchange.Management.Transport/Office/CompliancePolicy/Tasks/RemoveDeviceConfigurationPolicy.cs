using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000F5 RID: 245
	[Cmdlet("Remove", "DeviceConfigurationPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDeviceConfigurationPolicy : RemoveDevicePolicyBase
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x000292F3 File Offset: 0x000274F3
		public RemoveDeviceConfigurationPolicy() : base(PolicyScenario.DeviceSettings)
		{
		}
	}
}
