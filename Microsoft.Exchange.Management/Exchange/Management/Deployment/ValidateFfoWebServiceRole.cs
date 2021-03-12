using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200028B RID: 651
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Validate", "FfoWebServiceRole")]
	public sealed class ValidateFfoWebServiceRole : ValidatingTask
	{
		// Token: 0x060017B9 RID: 6073 RVA: 0x00064590 File Offset: 0x00062790
		public ValidateFfoWebServiceRole()
		{
			base.ValidationTests = new ValidatingCondition[]
			{
				new ValidatingCondition(new ValidationDelegate(this.RoleIsInstalled), Strings.VerifyRoleInstalled, true)
			};
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x000645CC File Offset: 0x000627CC
		private bool RoleIsInstalled()
		{
			return new RoleIsInstalledCondition
			{
				RoleInQuestion = RoleManager.GetRoleByName("FfoWebServiceRole")
			}.Verify();
		}
	}
}
