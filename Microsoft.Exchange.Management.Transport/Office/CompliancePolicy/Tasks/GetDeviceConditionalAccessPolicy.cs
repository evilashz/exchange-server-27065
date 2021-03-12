using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000F7 RID: 247
	[Cmdlet("Get", "DeviceConditionalAccessPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetDeviceConditionalAccessPolicy : GetDevicePolicyBase
	{
		// Token: 0x06000A26 RID: 2598 RVA: 0x000293A8 File Offset: 0x000275A8
		public GetDeviceConditionalAccessPolicy() : base(PolicyScenario.DeviceConditionalAccess)
		{
		}
	}
}
