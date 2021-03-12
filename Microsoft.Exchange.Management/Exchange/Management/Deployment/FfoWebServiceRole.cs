using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001CF RID: 463
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FfoWebServiceRole : Role
	{
		// Token: 0x06001016 RID: 4118 RVA: 0x000482EC File Offset: 0x000464EC
		public FfoWebServiceRole()
		{
			this.roleName = "FfoWebServiceRole";
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x000482FF File Offset: 0x000464FF
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.FfoWebService;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00048306 File Offset: 0x00046506
		public override Task InstallTask
		{
			get
			{
				return new InstallFfoWebServiceRole();
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0004830D File Offset: 0x0004650D
		public override Task DisasterRecoveryTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00048310 File Offset: 0x00046510
		public override Task UninstallTask
		{
			get
			{
				return new UninstallFfoWebServiceRoleRole();
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00048317 File Offset: 0x00046517
		public override ValidatingTask ValidateTask
		{
			get
			{
				return new ValidateFfoWebServiceRole();
			}
		}
	}
}
