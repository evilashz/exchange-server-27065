using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001FF RID: 511
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxRole : Role
	{
		// Token: 0x0600116F RID: 4463 RVA: 0x0004CD34 File Offset: 0x0004AF34
		public MailboxRole()
		{
			this.roleName = "MailboxRole";
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0004CD47 File Offset: 0x0004AF47
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.Mailbox;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x0004CD4A File Offset: 0x0004AF4A
		public override Task InstallTask
		{
			get
			{
				return new InstallMailboxRole();
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0004CD51 File Offset: 0x0004AF51
		public override Task DisasterRecoveryTask
		{
			get
			{
				return new DisasterRecoveryMailboxRole();
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0004CD58 File Offset: 0x0004AF58
		public override Task UninstallTask
		{
			get
			{
				return new UninstallMailboxRole();
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0004CD5F File Offset: 0x0004AF5F
		public override ValidatingTask ValidateTask
		{
			get
			{
				return new ValidateMailboxRole();
			}
		}
	}
}
