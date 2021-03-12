using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000217 RID: 535
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OSPRole : Role
	{
		// Token: 0x0600125A RID: 4698 RVA: 0x00050E9D File Offset: 0x0004F09D
		public OSPRole()
		{
			this.roleName = "OSPRole";
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00050EB0 File Offset: 0x0004F0B0
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.OSP;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x00050EB7 File Offset: 0x0004F0B7
		public override bool IsDatacenterOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00050EBA File Offset: 0x0004F0BA
		public override Task InstallTask
		{
			get
			{
				return new InstallOSPRole();
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x00050EC1 File Offset: 0x0004F0C1
		public override Task DisasterRecoveryTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00050EC4 File Offset: 0x0004F0C4
		public override Task UninstallTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x00050EC7 File Offset: 0x0004F0C7
		public override ValidatingTask ValidateTask
		{
			get
			{
				return null;
			}
		}
	}
}
