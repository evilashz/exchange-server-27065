using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200017C RID: 380
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CentralAdminFrontEndRole : Role
	{
		// Token: 0x06000E28 RID: 3624 RVA: 0x00040A5C File Offset: 0x0003EC5C
		public CentralAdminFrontEndRole()
		{
			this.roleName = "CentralAdminFrontEndRole";
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x00040A6F File Offset: 0x0003EC6F
		public override bool IsDatacenterOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00040A72 File Offset: 0x0003EC72
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.CentralAdminFrontEnd;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00040A79 File Offset: 0x0003EC79
		public override Task InstallTask
		{
			get
			{
				return new InstallCentralAdminFrontEndRole();
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00040A80 File Offset: 0x0003EC80
		public override Task DisasterRecoveryTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00040A83 File Offset: 0x0003EC83
		public override Task UninstallTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00040A86 File Offset: 0x0003EC86
		public override ValidatingTask ValidateTask
		{
			get
			{
				return null;
			}
		}
	}
}
