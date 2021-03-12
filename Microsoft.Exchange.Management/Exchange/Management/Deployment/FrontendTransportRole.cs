using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001D0 RID: 464
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FrontendTransportRole : Role
	{
		// Token: 0x0600101C RID: 4124 RVA: 0x0004831E File Offset: 0x0004651E
		public FrontendTransportRole()
		{
			this.roleName = "FrontendTransportRole";
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00048331 File Offset: 0x00046531
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.FrontendTransport;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00048338 File Offset: 0x00046538
		public override Task InstallTask
		{
			get
			{
				return new InstallFrontendTransportRole();
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x0004833F File Offset: 0x0004653F
		public override Task DisasterRecoveryTask
		{
			get
			{
				return new DisasterRecoveryFrontendTransportRole();
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x00048346 File Offset: 0x00046546
		public override Task UninstallTask
		{
			get
			{
				return new UninstallFrontendTransportRole();
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x0004834D File Offset: 0x0004654D
		public override ValidatingTask ValidateTask
		{
			get
			{
				return new ValidateFrontendTransportRole();
			}
		}
	}
}
