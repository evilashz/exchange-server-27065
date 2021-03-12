using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000FA RID: 250
	[Cmdlet("Remove", "DeviceConditionalAccessPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDeviceConditionalAccessPolicy : RemoveDevicePolicyBase
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x0002948C File Offset: 0x0002768C
		public RemoveDeviceConditionalAccessPolicy() : base(PolicyScenario.DeviceConditionalAccess)
		{
		}
	}
}
