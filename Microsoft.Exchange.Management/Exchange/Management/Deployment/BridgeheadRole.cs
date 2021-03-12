using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000176 RID: 374
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class BridgeheadRole : Role
	{
		// Token: 0x06000DF9 RID: 3577 RVA: 0x000400DC File Offset: 0x0003E2DC
		public BridgeheadRole()
		{
			this.roleName = "BridgeheadRole";
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x000400EF File Offset: 0x0003E2EF
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.HubTransport;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x000400F3 File Offset: 0x0003E2F3
		public override Task InstallTask
		{
			get
			{
				return new InstallBridgeheadRole();
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x000400FA File Offset: 0x0003E2FA
		public override Task DisasterRecoveryTask
		{
			get
			{
				return new DisasterRecoveryBridgeheadRole();
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00040101 File Offset: 0x0003E301
		public override Task UninstallTask
		{
			get
			{
				return new UninstallBridgeheadRole();
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00040108 File Offset: 0x0003E308
		public override ValidatingTask ValidateTask
		{
			get
			{
				return new ValidateBridgeheadRole();
			}
		}
	}
}
