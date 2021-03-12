using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200028E RID: 654
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Validate", "MailboxRole")]
	public sealed class ValidateMailboxRole : ValidatingTask
	{
		// Token: 0x060017BF RID: 6079 RVA: 0x000646C8 File Offset: 0x000628C8
		public ValidateMailboxRole()
		{
			base.ValidationTests = new ValidatingCondition[]
			{
				new ValidatingCondition(new ValidationDelegate(this.RoleIsInstalled), Strings.VerifyRoleInstalled, true),
				CommonValidatingConditions.StoreServiceExistsAndIsRunning
			};
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0006470C File Offset: 0x0006290C
		private bool RoleIsInstalled()
		{
			return new RoleIsInstalledCondition
			{
				RoleInQuestion = RoleManager.GetRoleByName("MailboxRole")
			}.Verify();
		}
	}
}
