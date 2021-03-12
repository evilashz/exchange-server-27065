using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200017E RID: 382
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ClientAccessRole : Role
	{
		// Token: 0x06000E36 RID: 3638 RVA: 0x00040AB6 File Offset: 0x0003ECB6
		public ClientAccessRole()
		{
			this.roleName = "ClientAccessRole";
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x00040AC9 File Offset: 0x0003ECC9
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.ClientAccess;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x00040ACC File Offset: 0x0003ECCC
		public override Task InstallTask
		{
			get
			{
				return new InstallClientAccessRole();
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00040AD3 File Offset: 0x0003ECD3
		public override Task DisasterRecoveryTask
		{
			get
			{
				return new DisasterRecoveryClientAccessRole();
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00040ADA File Offset: 0x0003ECDA
		public override Task UninstallTask
		{
			get
			{
				return new UninstallClientAccessRole();
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x00040AE1 File Offset: 0x0003ECE1
		public override ValidatingTask ValidateTask
		{
			get
			{
				return new ValidateClientAccessRole();
			}
		}
	}
}
