using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000179 RID: 377
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CafeRole : Role
	{
		// Token: 0x06000E11 RID: 3601 RVA: 0x000404DA File Offset: 0x0003E6DA
		public CafeRole()
		{
			this.roleName = "CafeRole";
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x000404ED File Offset: 0x0003E6ED
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.Cafe;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x000404F0 File Offset: 0x0003E6F0
		public override Task InstallTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x000404F3 File Offset: 0x0003E6F3
		public override Task DisasterRecoveryTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x000404F6 File Offset: 0x0003E6F6
		public override Task UninstallTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x000404F9 File Offset: 0x0003E6F9
		public override ValidatingTask ValidateTask
		{
			get
			{
				return null;
			}
		}
	}
}
