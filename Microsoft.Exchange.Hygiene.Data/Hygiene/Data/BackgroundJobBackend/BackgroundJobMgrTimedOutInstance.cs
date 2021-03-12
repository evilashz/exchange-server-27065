using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000038 RID: 56
	internal sealed class BackgroundJobMgrTimedOutInstance : BackgroundJobBackendBase
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x0000732C File Offset: 0x0000552C
		public BackgroundJobMgrTimedOutInstance(Guid roleId, Guid machineId, Guid syncContext, Guid newSyncContext)
		{
			this[BackgroundJobMgrTimedOutInstance.RoleIdProperty] = roleId;
			this[BackgroundJobMgrTimedOutInstance.MachineIdProperty] = machineId;
			this[BackgroundJobMgrTimedOutInstance.SyncContextProperty] = syncContext;
			this[BackgroundJobMgrTimedOutInstance.NewSyncContextProperty] = newSyncContext;
			this.UpdatedInstance = false;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000738B File Offset: 0x0000558B
		public Guid RoleId
		{
			get
			{
				return (Guid)this[BackgroundJobMgrTimedOutInstance.RoleIdProperty];
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000739D File Offset: 0x0000559D
		public Guid MachineId
		{
			get
			{
				return (Guid)this[BackgroundJobMgrTimedOutInstance.MachineIdProperty];
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000073AF File Offset: 0x000055AF
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x000073C1 File Offset: 0x000055C1
		public Guid SyncContext
		{
			get
			{
				return (Guid)this[BackgroundJobMgrTimedOutInstance.SyncContextProperty];
			}
			set
			{
				this[BackgroundJobMgrTimedOutInstance.SyncContextProperty] = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000073D4 File Offset: 0x000055D4
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000073E6 File Offset: 0x000055E6
		public Guid NewSyncContext
		{
			get
			{
				return (Guid)this[BackgroundJobMgrTimedOutInstance.NewSyncContextProperty];
			}
			set
			{
				this[BackgroundJobMgrTimedOutInstance.NewSyncContextProperty] = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000073F9 File Offset: 0x000055F9
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000740B File Offset: 0x0000560B
		public bool UpdatedInstance
		{
			get
			{
				return (bool)this[BackgroundJobMgrTimedOutInstance.UpdatedInstanceProperty];
			}
			set
			{
				this[BackgroundJobMgrTimedOutInstance.UpdatedInstanceProperty] = value;
			}
		}

		// Token: 0x04000141 RID: 321
		internal static readonly BackgroundJobBackendPropertyDefinition MachineIdProperty = BackgroundJobMgrInstanceProperties.MachineIdProperty;

		// Token: 0x04000142 RID: 322
		internal static readonly BackgroundJobBackendPropertyDefinition RoleIdProperty = BackgroundJobMgrInstanceProperties.RoleIdProperty;

		// Token: 0x04000143 RID: 323
		internal static readonly BackgroundJobBackendPropertyDefinition SyncContextProperty = BackgroundJobMgrInstanceProperties.SyncContextProperty;

		// Token: 0x04000144 RID: 324
		internal static readonly BackgroundJobBackendPropertyDefinition NewSyncContextProperty = new BackgroundJobBackendPropertyDefinition("NewSyncContext", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x04000145 RID: 325
		internal static readonly BackgroundJobBackendPropertyDefinition UpdatedInstanceProperty = new BackgroundJobBackendPropertyDefinition("UpdatedInstance", typeof(bool), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, false);
	}
}
