using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200028D RID: 653
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Validate", "GatewayRole")]
	public sealed class ValidateGatewayRole : ValidatingTask
	{
		// Token: 0x060017BD RID: 6077 RVA: 0x00064660 File Offset: 0x00062860
		public ValidateGatewayRole()
		{
			base.ValidationTests = new ValidatingCondition[]
			{
				new ValidatingCondition(new ValidationDelegate(this.RoleIsInstalled), Strings.VerifyRoleInstalled, true)
			};
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0006469C File Offset: 0x0006289C
		private bool RoleIsInstalled()
		{
			return new RoleIsInstalledCondition
			{
				RoleInQuestion = RoleManager.GetRoleByName("GatewayRole")
			}.Verify();
		}
	}
}
