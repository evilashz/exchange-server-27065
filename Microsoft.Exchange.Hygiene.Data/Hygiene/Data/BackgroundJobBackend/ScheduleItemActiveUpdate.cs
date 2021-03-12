using System;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000040 RID: 64
	internal sealed class ScheduleItemActiveUpdate : BackgroundJobBackendBase
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00008221 File Offset: 0x00006421
		public ScheduleItemActiveUpdate(Guid backgroundJobId, Guid scheduleId, bool active)
		{
			this.ScheduleId = scheduleId;
			this.BackgroundJobId = backgroundJobId;
			this.Active = active;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000823E File Offset: 0x0000643E
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00008250 File Offset: 0x00006450
		public Guid ScheduleId
		{
			get
			{
				return (Guid)this[ScheduleItemActiveUpdate.ScheduleIdProperty];
			}
			set
			{
				this[ScheduleItemActiveUpdate.ScheduleIdProperty] = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00008263 File Offset: 0x00006463
		// (set) Token: 0x06000256 RID: 598 RVA: 0x00008275 File Offset: 0x00006475
		public Guid BackgroundJobId
		{
			get
			{
				return (Guid)this[ScheduleItemActiveUpdate.BackgroundJobIdProperty];
			}
			set
			{
				this[ScheduleItemActiveUpdate.BackgroundJobIdProperty] = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00008288 File Offset: 0x00006488
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000829A File Offset: 0x0000649A
		public bool Active
		{
			get
			{
				return (bool)this[ScheduleItemActiveUpdate.ActiveProperty];
			}
			set
			{
				this[ScheduleItemActiveUpdate.ActiveProperty] = value;
			}
		}

		// Token: 0x0400017F RID: 383
		internal static readonly BackgroundJobBackendPropertyDefinition ScheduleIdProperty = ScheduleItemProperties.ScheduleIdProperty;

		// Token: 0x04000180 RID: 384
		internal static readonly BackgroundJobBackendPropertyDefinition BackgroundJobIdProperty = ScheduleItemProperties.BackgroundJobIdProperty;

		// Token: 0x04000181 RID: 385
		internal static readonly BackgroundJobBackendPropertyDefinition ActiveProperty = ScheduleItemProperties.ActiveProperty;
	}
}
