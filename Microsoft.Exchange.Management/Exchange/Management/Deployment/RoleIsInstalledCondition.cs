using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200022F RID: 559
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class RoleIsInstalledCondition : Condition
	{
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x00052B5C File Offset: 0x00050D5C
		// (set) Token: 0x060012E6 RID: 4838 RVA: 0x00052B64 File Offset: 0x00050D64
		public Role RoleInQuestion
		{
			get
			{
				return this.role;
			}
			set
			{
				this.role = value;
			}
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00052B70 File Offset: 0x00050D70
		public override bool Verify()
		{
			TaskLogger.LogEnter();
			bool isInstalled = this.RoleInQuestion.IsInstalled;
			TaskLogger.LogExit();
			return isInstalled;
		}

		// Token: 0x040007F8 RID: 2040
		private Role role;
	}
}
