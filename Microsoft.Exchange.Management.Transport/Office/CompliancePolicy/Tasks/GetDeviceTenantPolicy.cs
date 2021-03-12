using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000107 RID: 263
	[Cmdlet("Get", "DeviceTenantPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetDeviceTenantPolicy : GetDevicePolicyBase
	{
		// Token: 0x06000B9C RID: 2972 RVA: 0x0002C195 File Offset: 0x0002A395
		public GetDeviceTenantPolicy() : base(PolicyScenario.DeviceTenantConditionalAccess)
		{
		}
	}
}
