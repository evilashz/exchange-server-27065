using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000207 RID: 519
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MonitoringRole : Role
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x0004E68D File Offset: 0x0004C88D
		public MonitoringRole()
		{
			this.roleName = "MonitoringRole";
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0004E6A0 File Offset: 0x0004C8A0
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.Monitoring;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0004E6A7 File Offset: 0x0004C8A7
		public override bool IsDatacenterOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0004E6AA File Offset: 0x0004C8AA
		public override Task InstallTask
		{
			get
			{
				return new InstallMonitoringRole();
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0004E6B1 File Offset: 0x0004C8B1
		public override Task DisasterRecoveryTask
		{
			get
			{
				return new DisasterRecoveryMonitoringRole();
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0004E6B8 File Offset: 0x0004C8B8
		public override Task UninstallTask
		{
			get
			{
				return new UninstallMonitoringRole();
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0004E6BF File Offset: 0x0004C8BF
		public override ValidatingTask ValidateTask
		{
			get
			{
				return new ValidateMonitoringRole();
			}
		}
	}
}
