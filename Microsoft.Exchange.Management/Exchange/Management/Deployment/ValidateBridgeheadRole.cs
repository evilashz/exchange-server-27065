using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000289 RID: 649
	[Cmdlet("Validate", "BridgeheadRole")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class ValidateBridgeheadRole : ValidatingTask
	{
		// Token: 0x060017B5 RID: 6069 RVA: 0x000644C0 File Offset: 0x000626C0
		public ValidateBridgeheadRole()
		{
			base.ValidationTests = new ValidatingCondition[]
			{
				new ValidatingCondition(new ValidationDelegate(this.RoleIsInstalled), Strings.VerifyRoleInstalled, true)
			};
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x000644FC File Offset: 0x000626FC
		private bool RoleIsInstalled()
		{
			return new RoleIsInstalledCondition
			{
				RoleInQuestion = RoleManager.GetRoleByName("BridgeheadRole")
			}.Verify();
		}
	}
}
