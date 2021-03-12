using System;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000036 RID: 54
	internal sealed class BackgroundJobMgrInstance : BackgroundJobBackendBase
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00007085 File Offset: 0x00005285
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00007097 File Offset: 0x00005297
		public Guid MachineId
		{
			get
			{
				return (Guid)this[BackgroundJobMgrInstance.MachineIdProperty];
			}
			set
			{
				this[BackgroundJobMgrInstance.MachineIdProperty] = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000070AA File Offset: 0x000052AA
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x000070BC File Offset: 0x000052BC
		public string MachineName
		{
			get
			{
				return (string)this[BackgroundJobMgrInstance.MachineNameProperty];
			}
			set
			{
				this[BackgroundJobMgrInstance.MachineNameProperty] = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000070CA File Offset: 0x000052CA
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x000070DC File Offset: 0x000052DC
		public Guid RoleId
		{
			get
			{
				return (Guid)this[BackgroundJobMgrInstance.RoleIdProperty];
			}
			set
			{
				this[BackgroundJobMgrInstance.RoleIdProperty] = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000070EF File Offset: 0x000052EF
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00007101 File Offset: 0x00005301
		public DateTime HeartBeat
		{
			get
			{
				return (DateTime)this[BackgroundJobMgrInstance.HeartBeatProperty];
			}
			set
			{
				this[BackgroundJobMgrInstance.HeartBeatProperty] = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00007114 File Offset: 0x00005314
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00007126 File Offset: 0x00005326
		public bool Active
		{
			get
			{
				return (bool)this[BackgroundJobMgrInstance.ActiveProperty];
			}
			set
			{
				this[BackgroundJobMgrInstance.ActiveProperty] = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00007139 File Offset: 0x00005339
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000714B File Offset: 0x0000534B
		public long DataCenter
		{
			get
			{
				return (long)this[BackgroundJobMgrInstance.DCProperty];
			}
			set
			{
				this[BackgroundJobMgrInstance.DCProperty] = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000715E File Offset: 0x0000535E
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00007170 File Offset: 0x00005370
		public Regions Region
		{
			get
			{
				return (Regions)this[BackgroundJobMgrInstance.RegionProperty];
			}
			set
			{
				this[BackgroundJobMgrInstance.RegionProperty] = (int)value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00007183 File Offset: 0x00005383
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00007195 File Offset: 0x00005395
		public Guid SyncContext
		{
			get
			{
				return (Guid)this[BackgroundJobMgrInstance.SyncContextProperty];
			}
			set
			{
				this[BackgroundJobMgrInstance.SyncContextProperty] = value;
			}
		}

		// Token: 0x04000131 RID: 305
		internal static readonly BackgroundJobBackendPropertyDefinition MachineIdProperty = BackgroundJobMgrInstanceProperties.MachineIdProperty;

		// Token: 0x04000132 RID: 306
		internal static readonly BackgroundJobBackendPropertyDefinition MachineNameProperty = BackgroundJobMgrInstanceProperties.MachineNameProperty;

		// Token: 0x04000133 RID: 307
		internal static readonly BackgroundJobBackendPropertyDefinition RoleIdProperty = BackgroundJobMgrInstanceProperties.RoleIdProperty;

		// Token: 0x04000134 RID: 308
		internal static readonly BackgroundJobBackendPropertyDefinition HeartBeatProperty = BackgroundJobMgrInstanceProperties.HeartBeatProperty;

		// Token: 0x04000135 RID: 309
		internal static readonly BackgroundJobBackendPropertyDefinition ActiveProperty = BackgroundJobMgrInstanceProperties.ActiveProperty;

		// Token: 0x04000136 RID: 310
		internal static readonly BackgroundJobBackendPropertyDefinition DCProperty = BackgroundJobMgrInstanceProperties.DCProperty;

		// Token: 0x04000137 RID: 311
		internal static readonly BackgroundJobBackendPropertyDefinition RegionProperty = BackgroundJobMgrInstanceProperties.RegionProperty;

		// Token: 0x04000138 RID: 312
		internal static readonly BackgroundJobBackendPropertyDefinition SyncContextProperty = BackgroundJobMgrInstanceProperties.SyncContextProperty;
	}
}
