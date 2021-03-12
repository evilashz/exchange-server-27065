using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000F9 RID: 249
	[Cmdlet("Set", "DeviceConditionalAccessPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDeviceConditionalAccessPolicy : SetDevicePolicyBase
	{
		// Token: 0x06000A2A RID: 2602 RVA: 0x00029483 File Offset: 0x00027683
		public SetDeviceConditionalAccessPolicy() : base(PolicyScenario.DeviceConditionalAccess)
		{
		}
	}
}
