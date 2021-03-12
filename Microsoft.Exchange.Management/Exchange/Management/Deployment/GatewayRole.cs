using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001D1 RID: 465
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GatewayRole : Role
	{
		// Token: 0x06001022 RID: 4130 RVA: 0x00048354 File Offset: 0x00046554
		public GatewayRole()
		{
			this.roleName = "GatewayRole";
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x00048367 File Offset: 0x00046567
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.Edge;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0004836B File Offset: 0x0004656B
		public override Task InstallTask
		{
			get
			{
				return new InstallGatewayRole();
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x00048372 File Offset: 0x00046572
		public override Task DisasterRecoveryTask
		{
			get
			{
				return new DisasterRecoveryGatewayRole();
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x00048379 File Offset: 0x00046579
		public override Task UninstallTask
		{
			get
			{
				return new UninstallGatewayRole();
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x00048380 File Offset: 0x00046580
		public override ValidatingTask ValidateTask
		{
			get
			{
				return new ValidateGatewayRole();
			}
		}
	}
}
