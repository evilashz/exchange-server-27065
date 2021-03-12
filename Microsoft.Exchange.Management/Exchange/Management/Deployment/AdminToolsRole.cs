using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000175 RID: 373
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AdminToolsRole : Role
	{
		// Token: 0x06000DF2 RID: 3570 RVA: 0x000400A9 File Offset: 0x0003E2A9
		public AdminToolsRole()
		{
			this.roleName = "AdminToolsRole";
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x000400BC File Offset: 0x0003E2BC
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.None;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x000400BF File Offset: 0x0003E2BF
		public override Task InstallTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x000400C2 File Offset: 0x0003E2C2
		public override Task DisasterRecoveryTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x000400C5 File Offset: 0x0003E2C5
		public override Task UninstallTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x000400C8 File Offset: 0x0003E2C8
		public override ValidatingTask ValidateTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040006A5 RID: 1701
		public static readonly Version FirstConfiguredVersion = new Version(8, 1, 63, 0);
	}
}
