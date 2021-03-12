using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000EC RID: 236
	[Cmdlet("Get", "DeviceConfigurationPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetDeviceConfigurationPolicy : GetDevicePolicyBase
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x00026D60 File Offset: 0x00024F60
		public GetDeviceConfigurationPolicy() : base(PolicyScenario.DeviceSettings)
		{
		}
	}
}
