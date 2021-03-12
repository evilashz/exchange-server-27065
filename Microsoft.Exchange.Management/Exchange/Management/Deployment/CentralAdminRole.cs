using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200017D RID: 381
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CentralAdminRole : Role
	{
		// Token: 0x06000E2F RID: 3631 RVA: 0x00040A89 File Offset: 0x0003EC89
		public CentralAdminRole()
		{
			this.roleName = "CentralAdminRole";
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00040A9C File Offset: 0x0003EC9C
		public override bool IsDatacenterOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00040A9F File Offset: 0x0003EC9F
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.CentralAdmin;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00040AA6 File Offset: 0x0003ECA6
		public override Task InstallTask
		{
			get
			{
				return new InstallCentralAdminRole();
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x00040AAD File Offset: 0x0003ECAD
		public override Task DisasterRecoveryTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x00040AB0 File Offset: 0x0003ECB0
		public override Task UninstallTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x00040AB3 File Offset: 0x0003ECB3
		public override ValidatingTask ValidateTask
		{
			get
			{
				return null;
			}
		}
	}
}
