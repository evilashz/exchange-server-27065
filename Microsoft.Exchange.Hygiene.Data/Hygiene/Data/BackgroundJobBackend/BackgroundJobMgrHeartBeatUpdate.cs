using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000035 RID: 53
	internal sealed class BackgroundJobMgrHeartBeatUpdate : BackgroundJobBackendBase
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00006EA8 File Offset: 0x000050A8
		public BackgroundJobMgrHeartBeatUpdate(Guid roleId, Guid machineId, bool active, Guid syncContext, Guid newSyncContext)
		{
			this[BackgroundJobMgrHeartBeatUpdate.RoleIdProperty] = roleId;
			this[BackgroundJobMgrHeartBeatUpdate.MachineIdProperty] = machineId;
			this.HeartBeat = DateTime.UtcNow;
			this.Active = active;
			this[BackgroundJobMgrHeartBeatUpdate.SyncContextProperty] = syncContext;
			this[BackgroundJobMgrHeartBeatUpdate.NewSyncContextProperty] = newSyncContext;
			this.UpdatedInstance = false;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00006F1A File Offset: 0x0000511A
		public Guid RoleId
		{
			get
			{
				return (Guid)this[BackgroundJobMgrHeartBeatUpdate.RoleIdProperty];
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00006F2C File Offset: 0x0000512C
		public Guid MachineId
		{
			get
			{
				return (Guid)this[BackgroundJobMgrHeartBeatUpdate.MachineIdProperty];
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00006F3E File Offset: 0x0000513E
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00006F50 File Offset: 0x00005150
		public DateTime HeartBeat
		{
			get
			{
				return (DateTime)this[BackgroundJobMgrHeartBeatUpdate.HeartBeatProperty];
			}
			set
			{
				this[BackgroundJobMgrHeartBeatUpdate.HeartBeatProperty] = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00006F63 File Offset: 0x00005163
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00006F75 File Offset: 0x00005175
		public bool Active
		{
			get
			{
				return (bool)this[BackgroundJobMgrHeartBeatUpdate.ActiveProperty];
			}
			set
			{
				this[BackgroundJobMgrHeartBeatUpdate.ActiveProperty] = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006F88 File Offset: 0x00005188
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00006F9A File Offset: 0x0000519A
		public Guid SyncContext
		{
			get
			{
				return (Guid)this[BackgroundJobMgrHeartBeatUpdate.SyncContextProperty];
			}
			set
			{
				this[BackgroundJobMgrHeartBeatUpdate.SyncContextProperty] = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00006FAD File Offset: 0x000051AD
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00006FBF File Offset: 0x000051BF
		public Guid NewSyncContext
		{
			get
			{
				return (Guid)this[BackgroundJobMgrHeartBeatUpdate.NewSyncContextProperty];
			}
			set
			{
				this[BackgroundJobMgrHeartBeatUpdate.NewSyncContextProperty] = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006FD2 File Offset: 0x000051D2
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00006FE4 File Offset: 0x000051E4
		public bool UpdatedInstance
		{
			get
			{
				return (bool)this[BackgroundJobMgrHeartBeatUpdate.UpdatedInstanceProperty];
			}
			set
			{
				this[BackgroundJobMgrHeartBeatUpdate.UpdatedInstanceProperty] = value;
			}
		}

		// Token: 0x0400012A RID: 298
		internal static readonly BackgroundJobBackendPropertyDefinition MachineIdProperty = BackgroundJobMgrInstanceProperties.MachineIdProperty;

		// Token: 0x0400012B RID: 299
		internal static readonly BackgroundJobBackendPropertyDefinition RoleIdProperty = BackgroundJobMgrInstanceProperties.RoleIdProperty;

		// Token: 0x0400012C RID: 300
		internal static readonly BackgroundJobBackendPropertyDefinition HeartBeatProperty = BackgroundJobMgrInstanceProperties.HeartBeatProperty;

		// Token: 0x0400012D RID: 301
		internal static readonly BackgroundJobBackendPropertyDefinition ActiveProperty = BackgroundJobMgrInstanceProperties.ActiveProperty;

		// Token: 0x0400012E RID: 302
		internal static readonly BackgroundJobBackendPropertyDefinition SyncContextProperty = BackgroundJobMgrInstanceProperties.SyncContextProperty;

		// Token: 0x0400012F RID: 303
		internal static readonly BackgroundJobBackendPropertyDefinition NewSyncContextProperty = new BackgroundJobBackendPropertyDefinition("NewSyncContext", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x04000130 RID: 304
		internal static readonly BackgroundJobBackendPropertyDefinition UpdatedInstanceProperty = new BackgroundJobBackendPropertyDefinition("UpdatedInstance", typeof(bool), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, false);
	}
}
