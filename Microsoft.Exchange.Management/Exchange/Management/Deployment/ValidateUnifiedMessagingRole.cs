using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000290 RID: 656
	[Cmdlet("Validate", "UnifiedMessagingRole")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class ValidateUnifiedMessagingRole : ValidatingTask
	{
		// Token: 0x060017C3 RID: 6083 RVA: 0x000647A0 File Offset: 0x000629A0
		public ValidateUnifiedMessagingRole()
		{
			base.ValidationTests = new ValidatingCondition[]
			{
				new ValidatingCondition(new ValidationDelegate(this.RoleIsInstalled), Strings.VerifyRoleInstalled, true)
			};
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x000647DC File Offset: 0x000629DC
		private bool RoleIsInstalled()
		{
			return new RoleIsInstalledCondition
			{
				RoleInQuestion = RoleManager.GetRoleByName("UnifiedMessagingRole")
			}.Verify();
		}
	}
}
