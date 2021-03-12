using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200010A RID: 266
	[Cmdlet("Remove", "DeviceTenantPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDeviceTenantPolicy : RemoveDevicePolicyBase
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002C2E5 File Offset: 0x0002A4E5
		public RemoveDeviceTenantPolicy() : base(PolicyScenario.DeviceTenantConditionalAccess)
		{
		}
	}
}
