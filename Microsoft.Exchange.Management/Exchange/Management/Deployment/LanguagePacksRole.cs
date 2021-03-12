using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001FE RID: 510
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LanguagePacksRole : Role
	{
		// Token: 0x06001168 RID: 4456 RVA: 0x0004CD02 File Offset: 0x0004AF02
		public LanguagePacksRole()
		{
			this.roleName = LanguagePacksRole.ClassRoleName;
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0004CD15 File Offset: 0x0004AF15
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.LanguagePacks;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0004CD1C File Offset: 0x0004AF1C
		public override Task InstallTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0004CD1F File Offset: 0x0004AF1F
		public override Task DisasterRecoveryTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0004CD22 File Offset: 0x0004AF22
		public override Task UninstallTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0004CD25 File Offset: 0x0004AF25
		public override ValidatingTask ValidateTask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000797 RID: 1943
		public static readonly string ClassRoleName = "LanguagePacksRole";
	}
}
