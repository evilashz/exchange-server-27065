using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000F2 RID: 242
	[Cmdlet("Set", "DeviceConfigurationPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDeviceConfigurationPolicy : SetDevicePolicyBase
	{
		// Token: 0x06000A09 RID: 2569 RVA: 0x00028EB3 File Offset: 0x000270B3
		public SetDeviceConfigurationPolicy() : base(PolicyScenario.DeviceSettings)
		{
		}
	}
}
