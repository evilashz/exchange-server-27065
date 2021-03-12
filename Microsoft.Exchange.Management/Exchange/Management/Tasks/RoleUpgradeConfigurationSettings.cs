using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000657 RID: 1623
	internal class RoleUpgradeConfigurationSettings
	{
		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x060038CC RID: 14540 RVA: 0x000EE9E4 File Offset: 0x000ECBE4
		// (set) Token: 0x060038CD RID: 14541 RVA: 0x000EE9EC File Offset: 0x000ECBEC
		public RoleEntry[] AvailableRoleEntries { get; set; }

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x060038CE RID: 14542 RVA: 0x000EE9F5 File Offset: 0x000ECBF5
		// (set) Token: 0x060038CF RID: 14543 RVA: 0x000EE9FD File Offset: 0x000ECBFD
		public IConfigurationSession ConfigurationSession { get; set; }

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x000EEA06 File Offset: 0x000ECC06
		// (set) Token: 0x060038D1 RID: 14545 RVA: 0x000EEA0E File Offset: 0x000ECC0E
		public ADObjectId OrgContainerId { get; set; }

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000EEA17 File Offset: 0x000ECC17
		// (set) Token: 0x060038D3 RID: 14547 RVA: 0x000EEA1F File Offset: 0x000ECC1F
		public OrganizationIdParameter Organization { get; set; }

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000EEA28 File Offset: 0x000ECC28
		// (set) Token: 0x060038D5 RID: 14549 RVA: 0x000EEA30 File Offset: 0x000ECC30
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x000EEA39 File Offset: 0x000ECC39
		// (set) Token: 0x060038D7 RID: 14551 RVA: 0x000EEA41 File Offset: 0x000ECC41
		public ADObjectId RolesContainerId { get; set; }

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x000EEA4A File Offset: 0x000ECC4A
		// (set) Token: 0x060038D9 RID: 14553 RVA: 0x000EEA52 File Offset: 0x000ECC52
		public ServicePlan ServicePlanSettings { get; set; }

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x000EEA5B File Offset: 0x000ECC5B
		// (set) Token: 0x060038DB RID: 14555 RVA: 0x000EEA63 File Offset: 0x000ECC63
		public Task.TaskVerboseLoggingDelegate WriteVerbose { get; set; }

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x000EEA6C File Offset: 0x000ECC6C
		// (set) Token: 0x060038DD RID: 14557 RVA: 0x000EEA74 File Offset: 0x000ECC74
		public Task.TaskErrorLoggingDelegate WriteError { get; set; }

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000EEA7D File Offset: 0x000ECC7D
		// (set) Token: 0x060038DF RID: 14559 RVA: 0x000EEA85 File Offset: 0x000ECC85
		public Task.TaskWarningLoggingDelegate WriteWarning { get; set; }

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x000EEA8E File Offset: 0x000ECC8E
		// (set) Token: 0x060038E1 RID: 14561 RVA: 0x000EEA96 File Offset: 0x000ECC96
		public RoleUpgradeConfigurationSettings.LogWriteObjectDelegate LogWriteObject { get; set; }

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x060038E2 RID: 14562 RVA: 0x000EEA9F File Offset: 0x000ECC9F
		// (set) Token: 0x060038E3 RID: 14563 RVA: 0x000EEAA7 File Offset: 0x000ECCA7
		public RoleUpgradeConfigurationSettings.LogReadObjectDelegate LogReadObject { get; set; }

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x060038E4 RID: 14564 RVA: 0x000EEAB0 File Offset: 0x000ECCB0
		// (set) Token: 0x060038E5 RID: 14565 RVA: 0x000EEAB8 File Offset: 0x000ECCB8
		public RoleUpgradeConfigurationSettings.RemoveRoleAndAssignmentsDelegate RemoveRoleAndAssignments { get; set; }

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x000EEAC1 File Offset: 0x000ECCC1
		// (set) Token: 0x060038E7 RID: 14567 RVA: 0x000EEAC9 File Offset: 0x000ECCC9
		public InstallCannedRbacRoles Task { get; set; }

		// Token: 0x02000658 RID: 1624
		// (Invoke) Token: 0x060038EA RID: 14570
		public delegate void LogReadObjectDelegate(ADRawEntry obj);

		// Token: 0x02000659 RID: 1625
		// (Invoke) Token: 0x060038EE RID: 14574
		public delegate void LogWriteObjectDelegate(ADObject obj);

		// Token: 0x0200065A RID: 1626
		// (Invoke) Token: 0x060038F2 RID: 14578
		public delegate void RemoveRoleAndAssignmentsDelegate(ADObjectId roleId);
	}
}
