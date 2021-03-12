using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200017B RID: 379
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CentralAdminDatabaseRole : Role
	{
		// Token: 0x06000E21 RID: 3617 RVA: 0x00040A2F File Offset: 0x0003EC2F
		public CentralAdminDatabaseRole()
		{
			this.roleName = "CentralAdminDatabaseRole";
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00040A42 File Offset: 0x0003EC42
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.CentralAdminDatabase;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x00040A49 File Offset: 0x0003EC49
		public override bool IsDatacenterOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x00040A4C File Offset: 0x0003EC4C
		public override Task InstallTask
		{
			get
			{
				return new InstallCentralAdminDatabaseRole();
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x00040A53 File Offset: 0x0003EC53
		public override Task DisasterRecoveryTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00040A56 File Offset: 0x0003EC56
		public override Task UninstallTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x00040A59 File Offset: 0x0003EC59
		public override ValidatingTask ValidateTask
		{
			get
			{
				return null;
			}
		}
	}
}
