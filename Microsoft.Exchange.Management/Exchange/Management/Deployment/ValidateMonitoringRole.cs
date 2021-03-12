using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200028F RID: 655
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Validate", "MonitoringRole")]
	public sealed class ValidateMonitoringRole : ValidatingTask
	{
		// Token: 0x060017C1 RID: 6081 RVA: 0x00064738 File Offset: 0x00062938
		public ValidateMonitoringRole()
		{
			base.ValidationTests = new ValidatingCondition[]
			{
				new ValidatingCondition(new ValidationDelegate(this.RoleIsInstalled), Strings.VerifyRoleInstalled, true)
			};
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00064774 File Offset: 0x00062974
		private bool RoleIsInstalled()
		{
			return new RoleIsInstalledCondition
			{
				RoleInQuestion = RoleManager.GetRoleByName("MonitoringRole")
			}.Verify();
		}
	}
}
