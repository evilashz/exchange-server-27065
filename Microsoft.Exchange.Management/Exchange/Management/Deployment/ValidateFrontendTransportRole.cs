using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200028C RID: 652
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Validate", "FrontendTransportRole")]
	public sealed class ValidateFrontendTransportRole : ValidatingTask
	{
		// Token: 0x060017BB RID: 6075 RVA: 0x000645F8 File Offset: 0x000627F8
		public ValidateFrontendTransportRole()
		{
			base.ValidationTests = new ValidatingCondition[]
			{
				new ValidatingCondition(new ValidationDelegate(this.RoleIsInstalled), Strings.VerifyRoleInstalled, true)
			};
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00064634 File Offset: 0x00062834
		private bool RoleIsInstalled()
		{
			return new RoleIsInstalledCondition
			{
				RoleInQuestion = RoleManager.GetRoleByName("FrontendTransportRole")
			}.Verify();
		}
	}
}
