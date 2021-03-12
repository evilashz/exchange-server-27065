using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200028A RID: 650
	[Cmdlet("Validate", "ClientAccessRole")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class ValidateClientAccessRole : ValidatingTask
	{
		// Token: 0x060017B7 RID: 6071 RVA: 0x00064528 File Offset: 0x00062728
		public ValidateClientAccessRole()
		{
			base.ValidationTests = new ValidatingCondition[]
			{
				new ValidatingCondition(new ValidationDelegate(this.RoleIsInstalled), Strings.VerifyRoleInstalled, true)
			};
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00064564 File Offset: 0x00062764
		private bool RoleIsInstalled()
		{
			return new RoleIsInstalledCondition
			{
				RoleInQuestion = RoleManager.GetRoleByName("ClientAccessRole")
			}.Verify();
		}
	}
}
